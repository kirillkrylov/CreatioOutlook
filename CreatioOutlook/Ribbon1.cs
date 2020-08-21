using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CreatioDataService;
using CreatioDataService.DTO;
using Microsoft.Office.Interop.Outlook;
using Microsoft.Office.Tools.Ribbon;

namespace CreatioOutlook
{
    public partial class Ribbon1
    {

        /***         
        const string baseUrl = "https://077275-crm-bundle.creatio.com";
        const string userName = "APetrunenko";
        const string userPassword = "lulu2020";
        */

        const string baseUrl = "http://k_krylov_nb:7020";
        const string userName = "Supervisor";
        const string userPassword = "Supervisor";


        public Creatio creatio { get; set; }
        private CurrentUserContact CurrentUserContact { get; set;}

        private void Ribbon1_Load(object sender, RibbonUIEventArgs e)
        {
            btnLogin_Click(sender, null);
        }

        private void btnLogin_Click(object sender, RibbonControlEventArgs e)
        {

            creatio = new Creatio(new Uri(baseUrl), userName, userPassword);
            AuthResponseDTO loginResponse = null;
            Task.Run(async () =>
            {
                loginResponse = await creatio.Login();
            }).Wait();

            if (loginResponse.Code == 0)
            {
                //MessageBox.Show("Login Success !");
            }

            btnCurrentUser_Click(sender, null);

        }

        private void btnCurrentUser_Click(object sender, RibbonControlEventArgs e)
        {
            Task.Run(async () =>
            {
                CurrentUserContact = await creatio.GetCurrentUserContact();
                //MessageBox.Show($"You are: {CurrentUserContact?.Name} Your Email Is: {CurrentUserContact?.Email}");
            }).Wait();

        }

        private void btnCreateLead_Click(object sender, RibbonControlEventArgs e)
        {
            var m = e.Control.Context as Inspector;
            if (m == null) return;
            var mailitem = m.CurrentItem as MailItem;

            string senderName = string.Empty;
            string senderEmail = string.Empty;
            string subject = string.Empty;
            string emailBody = string.Empty;
            if (mailitem != null)
            {    
                senderName = mailitem.Sender.Name;
                senderEmail = getSenderEmailAddress(mailitem);
                subject = mailitem.Subject;
                emailBody = mailitem.HTMLBody;


                Guid id = Guid.NewGuid();
                Lead lead = new Lead
                {
                    Id = id,
                    Commentary = subject,
                    Notes = emailBody,
                    Email = senderEmail,
                    Contact = senderName,
                    Owner = CurrentUserContact.ContactId,
                    LeadType = Guid.Parse("E12C4C83-97D3-42C7-B785-17B14179E879") //Need for our services (Select Id from LeadType)

                };

                string result = string.Empty;
                Task.Run(async () =>
                {
                    result = await creatio.InsertLead(lead);

                }).Wait();

                if (mailitem.Attachments != null && mailitem.Attachments?.Count > 0)
                {

                    foreach (Attachment attachment in mailitem.Attachments)
                    {
                        string filename = attachment.FileName;
                        const string PR_ATTACH_DATA_BIN = "http://schemas.microsoft.com/mapi/proptag/0x37010102";
                        byte[] attachmentData = attachment.PropertyAccessor.GetProperty(PR_ATTACH_DATA_BIN);

                        if (filename.EndsWith(".pdf") || filename.EndsWith(".xls") || filename.EndsWith(".xlsx") || filename.EndsWith(".doc") || filename.EndsWith(".docx")
                            || filename.EndsWith(".pptx") || filename.EndsWith(".ppt") || filename.EndsWith(".pdf"))
                        {
                            Task.Run(async () =>
                            {
                                string mimeType = creatio.GetMimeType(filename);
                                await creatio.UploadFileAsync(attachmentData, mimeType, filename, "Lead", id.ToString(), "FileLead", "Data");
                            }).Wait();
                        }
                    }
                }

                DialogResult dialogResult = MessageBox.Show("Success", "Would you like to open this lead now", MessageBoxButtons.YesNo);
                if(dialogResult == DialogResult.Yes)
                {
                    string LeadUrl = $"{baseUrl}/0/Nui/ViewModule.aspx#CardModuleV2/LeadPageV2/edit/{id}";
                    Process.Start(LeadUrl);
                }
            }
        }

        private string getSenderEmailAddress(MailItem mail)
        {
            AddressEntry sender = mail.Sender;
            string SenderEmailAddress = "";

            if (sender.AddressEntryUserType == OlAddressEntryUserType.olExchangeUserAddressEntry || sender.AddressEntryUserType == OlAddressEntryUserType.olExchangeRemoteUserAddressEntry)
            {
                ExchangeUser exchUser = sender.GetExchangeUser();
                if (exchUser != null)
                {
                    SenderEmailAddress = exchUser.PrimarySmtpAddress;
                }
            }
            else
            {
                SenderEmailAddress = mail.SenderEmailAddress;
            }

            return SenderEmailAddress;
        }
    }
}
