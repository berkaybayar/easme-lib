using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace EasMe
{
    public class EasAPI
    {
        
        ////Install-Package Microsoft.AspNet.WebApi.Client -Version 5.2.8
        public class APIResponse 
        {
            public bool Status { get; set; }
            public string Content { get; set; }
        }
        //API response must be anon class 
        //Parsing will only return one of the items as string
        public string ParsefromAPIResponse(object obj, string parse,bool isThrow = true)
        {
            try
            {
                var rMessage = JObject.Parse(obj.ToString())[parse];
                return rMessage.ToString();
            }
            catch (Exception ex)
            {
                if (isThrow)
                {
                    throw ex;
                }
                return "";
            }
            
            
        }
        public APIResponse SendGetRequest(string URL, string TOKEN = null)
        {
            var Response = new APIResponse();
            try
            {
                using (var client = new HttpClient())
                {
                    if (TOKEN != null)
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);
                    }
                    client.BaseAddress = new Uri(URL);
                    var postTask = client.GetAsync(client.BaseAddress);
                    postTask.Wait();
                    var r = postTask.Result;
                    Response.Status = r.IsSuccessStatusCode;
                    Response.Content = r.Content.ReadAsStringAsync().Result;

                }
            }
            catch (Exception e)
            {
                Response.Status = false;
                Response.Content = e.Message;
            }
            return Response;
        }

        public APIResponse SendPostRequest(string URL, string ActionName, object Data, string TOKEN = null)
        {
            var Response = new APIResponse();
            try
            {
                using (var client = new HttpClient())
                {
                    if (TOKEN != null)
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);
                    }
                    client.BaseAddress = new Uri(URL);
                    var postTask = client.PostAsJsonAsync(ActionName, Data);
                    postTask.Wait();
                    var r = postTask.Result;
                    Response.Status = r.IsSuccessStatusCode;
                    Response.Content = r.Content.ReadAsStringAsync().Result.ToString();

                }
            }
            catch (Exception e)
            {
                Response.Status = false;
                Response.Content = e.Message;

            }
            return Response;
        }
    }


}
