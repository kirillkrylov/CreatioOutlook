using CreatioDataService.DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Terrasoft.Nui.ServiceModel.DataContract;

namespace CreatioDataService
{
    public class Creatio
    {
        private HttpClientHandler Handler { get; set; }
        private HttpClient Client { get;  set; }
        private CookieContainer  AuthCookies{ get; set; }
        private  Uri Domain{ get;  set; }
        private string UserName {get; set; }
        private string UserPassword {get; set; }

        /// <summary>
        /// Main Creatio Class that allows working with data
        /// </summary>
        /// <param name="domain">Url to the remote Creatio Site</param>
        /// <param name="userName">UserName to connect with</param>
        /// <param name="userPassword">UserPassword to connect with</param>
        public Creatio(Uri domain, string userName, string userPassword)
        {
            Domain = domain;
            UserName = userName;
            UserPassword = userPassword;
            Handler = new HttpClientHandler();
            Client = new HttpClient(Handler);
            AuthCookies = new CookieContainer();
            Handler.CookieContainer = AuthCookies;
        }

        /// <summary>
        /// Login to Creatio
        /// </summary>
        /// <returns>Result of Operation</returns>
        public async Task<AuthResponseDTO> Login()
        {
            Uri logInUri = new Uri(Domain, "ServiceModel/AuthService.svc/Login");
            var body = new AuthRequest { UserName = UserName, UserPassword = UserPassword };
            HttpContent content = new StringContent(JsonConvert.SerializeObject(body));

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var message = await Client.PostAsync(logInUri, content);
            message.EnsureSuccessStatusCode();
            var text = await message.Content.ReadAsStringAsync();
            AuthResponseDTO authResponseDTO = JsonConvert.DeserializeObject<AuthResponseDTO>(text);

            Console.WriteLine(authResponseDTO.Code);
            var bpmcsrf = Handler.CookieContainer.GetCookies(logInUri)["BPMCSRF"].Value;

            if (authResponseDTO.Code == 0)
            {
                Client.DefaultRequestHeaders.Add("BPMCSRF", bpmcsrf);
            }
            return authResponseDTO;
        }
        private async Task<string> ExecuteSelect(SelectQuery querry)
        {
            SelectQuery select = querry;
            string text = JsonConvert.SerializeObject(select);

            HttpContent content = new StringContent(text);
            Uri selectUri = new Uri(Domain, @"0/DataService/json/SyncReply/SelectQuery");

            var message = await Client.PostAsync(selectUri, content);
            message.EnsureSuccessStatusCode();

            return await message.Content.ReadAsStringAsync();
        }

        private async Task<string> ExecuteInsert(InsertQuery querry)
        {
            InsertQuery select = querry;
            string text = JsonConvert.SerializeObject(select);

            HttpContent content = new StringContent(text);
            Uri selectUri = new Uri(Domain, @"0/DataService/json/SyncReply/InsertQuery");

            var message = await Client.PostAsync(selectUri, content);
            message.EnsureSuccessStatusCode();

            return await message.Content.ReadAsStringAsync();
        }
        public async Task<CurrentUserContact> GetCurrentUserContact()
        {
            var querry = SelectQuerryBuilder.GetCurrentContact(UserName);
            string resultJson = await ExecuteSelect(querry);
            return GetRows<CurrentUserContact>(resultJson).FirstOrDefault();
        }



        public async Task<bool> UploadFileAsync(byte[] postBytes, string mimeType, string fileName, string parentColumnName,
            string parentColumnValue, string entitySchemaName, string columnName)
        {
            Guid id = Guid.NewGuid(); //Create new File Id;

            string uploadUrl =  Domain + @"/0/rest/FileApiService/Upload?fileapi";
            uploadUrl += ("&totalFileLength=" + postBytes.Length.ToString());
            uploadUrl += "&fileId=" + id.ToString();
            uploadUrl += "&mimeType=" + mimeType;
            uploadUrl += "&fileName=" + fileName;

            uploadUrl += "&columnName=" + columnName;
            uploadUrl += "&parentColumnName=" + parentColumnName;
            uploadUrl += "&parentColumnValue=" + parentColumnValue;
            uploadUrl += "&entitySchemaName=" + entitySchemaName;

            var content = new MultipartFormDataContent();
            var fileContent = new ByteArrayContent(postBytes);
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(mimeType);
            content.Add(fileContent, "attachment", fileName);

            await Client.PostAsync(uploadUrl, content);



            return false;
        }





        public async Task<string> InsertLead(Lead lead)
        {
            var querry = InsertQuerryBuilder.GetLeadInsertQuery(lead);
            string resultJson = await ExecuteInsert(querry);
            return resultJson;
        }

        private List<T> GetRows<T>(string json) where T : class
        {
            JObject jo = JObject.Parse(json);
            IList<JToken> results = jo["rows"].Children().ToList();
            List<T> result = new List<T>();

            if(results?.Count != null)
            {
                foreach(JToken jt in results)
                {
                    result.Add(jt.ToObject<T>());
                }
            }
            return result;
        }
    }
}
