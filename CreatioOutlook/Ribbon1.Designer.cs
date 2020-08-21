namespace CreatioOutlook
{
    partial class Ribbon1 : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public Ribbon1()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.CreatioTab = this.Factory.CreateRibbonTab();
            this.group1 = this.Factory.CreateRibbonGroup();
            this.btnLogin = this.Factory.CreateRibbonButton();
            this.btnCurrentUser = this.Factory.CreateRibbonButton();
            this.btnCreateLead = this.Factory.CreateRibbonButton();
            this.CreatioTab.SuspendLayout();
            this.group1.SuspendLayout();
            this.SuspendLayout();
            // 
            // CreatioTab
            // 
            this.CreatioTab.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.CreatioTab.Groups.Add(this.group1);
            this.CreatioTab.Label = "Creatio Add In";
            this.CreatioTab.Name = "CreatioTab";
            // 
            // group1
            // 
            this.group1.Items.Add(this.btnLogin);
            this.group1.Items.Add(this.btnCurrentUser);
            this.group1.Items.Add(this.btnCreateLead);
            this.group1.Label = "Group 1";
            this.group1.Name = "group1";
            // 
            // btnLogin
            // 
            this.btnLogin.Description = "Login to Creatio";
            this.btnLogin.Label = "Log In";
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnLogin_Click);
            // 
            // btnCurrentUser
            // 
            this.btnCurrentUser.Enabled = false;
            this.btnCurrentUser.Label = "Get Current User";
            this.btnCurrentUser.Name = "btnCurrentUser";
            this.btnCurrentUser.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnCurrentUser_Click);
            // 
            // btnCreateLead
            // 
            this.btnCreateLead.Enabled = false;
            this.btnCreateLead.Label = "Create Lead";
            this.btnCreateLead.Name = "btnCreateLead";
            this.btnCreateLead.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnCreateLead_Click);
            // 
            // Ribbon1
            // 
            this.Name = "Ribbon1";
            this.RibbonType = "Microsoft.Outlook.Explorer, Microsoft.Outlook.Mail.Read";
            this.Tabs.Add(this.CreatioTab);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.Ribbon1_Load);
            this.CreatioTab.ResumeLayout(false);
            this.CreatioTab.PerformLayout();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab CreatioTab;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnLogin;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnCurrentUser;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnCreateLead;
    }

    partial class ThisRibbonCollection
    {
        internal Ribbon1 Ribbon1
        {
            get { return this.GetRibbon<Ribbon1>(); }
        }
    }
}
