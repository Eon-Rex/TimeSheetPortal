using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Web.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Data;
using System.Text;
using System.Net.Sockets;
using System.Collections.Generic;

namespace samco.D365ServiceRefrence
{
    public class D365ServiceCall
    {


        private static readonly string clientid = WebConfigurationManager.AppSettings["clientidD365"];
        private static readonly string clientsecret = WebConfigurationManager.AppSettings["seckeyD365"];
        private static readonly string tenant_id = WebConfigurationManager.AppSettings["TenetIdD365"];
        private static readonly string resource = WebConfigurationManager.AppSettings["ResourceD365"];
        private static readonly string D365DataUrl = WebConfigurationManager.AppSettings["D365DataUrl"];

        public async Task<Object> PostD365Service(string apiAction, object jsonContent)
        {
            string BaseAddress = $"{D365DataUrl}{apiAction}";
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                var token = await GetTokenAsync();

                CServiceStatus cServiceStatus = new CServiceStatus();

                if (token == null)
                {
                    cServiceStatus.ReturnMessage = "Login validate failed. Not able to authenticate. Please try again!!!";
                    cServiceStatus.RequestStatus = HttpStatusCode.Unauthorized;
                    return null;
                }

                using (var client = new HttpClient())
                {
                    try
                    {
                        //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        var jsonContentString = string.Empty;
                        client.BaseAddress = new Uri(BaseAddress);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Access_token);

                        // Serialize the jsonContent object to a JSON string
                        if (jsonContent != null)
                        {
                             jsonContentString = JsonConvert.SerializeObject(jsonContent);
                        }


                        var content = new StringContent(jsonContentString, Encoding.UTF8, "application/json");
                        var responseTask = await client.PostAsync("", content);
                        cServiceStatus.RequestStatus = responseTask.StatusCode;
                        cServiceStatus.ReturnMessage = responseTask.RequestMessage.ToString();

                        if (responseTask.IsSuccessStatusCode)
                        {
                            var resultData = await responseTask.Content.ReadAsStringAsync();
                            cServiceStatus.ReturnJson = resultData;
                            return resultData;
                        }
                    }
                    catch (Exception ex)
                    {
                        cServiceStatus.RequestStatus = HttpStatusCode.ServiceUnavailable;
                        cServiceStatus.ReturnMessage = ex.Message;
                    }
                }
            }catch(Exception ex)
            {

            }
            return null;

        }



        public async Task<Object> GetD365Service(string apiAction, string filter = "")
        {
            var token = await GetTokenAsync();
            string BaseAddress = $"{D365DataUrl}{apiAction}";
            if (!string.IsNullOrWhiteSpace(filter))
                BaseAddress += filter;
            CServiceStatus cServiceStatus = new CServiceStatus();
            if (token == null)
            {
                cServiceStatus.ReturnMessage = "Login validate failed. Not able to authenticate. Please try again!!!";
                cServiceStatus.RequestStatus = HttpStatusCode.Unauthorized;
                return null;
            }
            using (var client = new HttpClient())
            {
                try
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;


                    client.BaseAddress = new Uri(BaseAddress);
                    client.DefaultRequestHeaders.Authorization
                             = new AuthenticationHeaderValue("Bearer", token.Access_token);
                    var responseTask = client.GetAsync("");
                    responseTask.Wait();
                    var result = responseTask.Result;
                    cServiceStatus.RequestStatus = result.StatusCode;
                    cServiceStatus.ReturnMessage = result.RequestMessage.ToString();
                    if (result.IsSuccessStatusCode)
                    {
                        var result_data = await result.Content.ReadAsStringAsync();
                        cServiceStatus.ReturnJson = result_data;
                        var jsonLinq = JObject.Parse(result_data);
                        return result_data;
                    }

                }
                catch (Exception ex)
                {
                    cServiceStatus.RequestStatus = HttpStatusCode.ServiceUnavailable;
                    cServiceStatus.ReturnMessage = ex.Message;
                }

            }
            return null;
        }

        public async Task<Token> GetTokenAsync()
        {
            string result_data = string.Empty;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;


            using (var client = new HttpClient())
            {
                var tokenEndpoint = $"https://login.microsoftonline.com/{tenant_id}/oauth2/token";
                client.BaseAddress = new Uri(tokenEndpoint);

                            var requestData = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("grant_type", "client_credentials"),
                        new KeyValuePair<string, string>("client_secret", clientsecret),
                        new KeyValuePair<string, string>("resource", resource),
                        new KeyValuePair<string, string>("tenant_id", tenant_id),
                        new KeyValuePair<string, string>("client_id", clientid)
                    };

                var httpContent = new FormUrlEncodedContent(requestData);

                try
                {
                    var response = await client.PostAsync(tokenEndpoint, httpContent);

                    if (response.IsSuccessStatusCode)
                    {
                        result_data = await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        // Log or handle the error response
                    }
                }
                catch (Exception ex)
                {
                    // Log or handle the exception
                    Console.WriteLine(ex.ToString());
                }
            }

            return JsonConvert.DeserializeObject<Token>(result_data);
        }

        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri($"https://login.microsoftonline.com/{tenant_id}/oauth2/token");
        //        var httpContent = new StringContent($"grant_type=client_credentials&client_secret=" + clientsecret + "&resource=" + resource + "&tenant_id=" + tenant_id + "&bearerToken=undefined&client_id=" + clientid, Encoding.UTF8, "application/x-www-form-urlencoded");
        //        client.Timeout = TimeSpan.FromMinutes(10);
        //        try
        //        {
        //            var responseTask = client.GetAsync("");
        //            responseTask.Wait();
        //            var result = responseTask.Result;
        //            if (result.IsSuccessStatusCode)
        //            {
        //                result_data = await result.Content.ReadAsStringAsync();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.ToString());
        //        }
        //    }
        //    return JsonConvert.DeserializeObject<Token>(result_data);
        //}


        public class Token
        {
            public string Access_token { get; set; }
            public string Token_type { get; set; }
            public int Expires_in { get; set; }
        }

        public class CServiceStatus
        {
            private HttpStatusCode requestStatus = HttpStatusCode.NotFound;
            private string returnJson = string.Empty;
            private string returnMessage = string.Empty;
            public HttpStatusCode RequestStatus
            {
                get { return requestStatus; }
                set { requestStatus = value; }
            }
            public string ReturnJson
            {
                get { return returnJson; }
                set { returnJson = value; }
            }
            public string ReturnMessage
            {
                get { return returnMessage; }
                set { returnMessage = value; }
            }
        }
    }
}