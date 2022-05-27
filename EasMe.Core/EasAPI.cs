using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace EasMe.Core
{
    public class EasAPI
    {

        //Install-Package Microsoft.AspNet.WebApi.Client -Version 5.2.8
        public class APIResponse
        {
            public bool Status { get; set; }
            public string Content { get; set; }
        }
        //API response is json
        //Parsing will only return one of the items as string
        public string ParsefromAPIResponse(string response, string parse)
        {
            try
            {
                var rMessage = JObject.Parse(response.ToString())[parse];
                return  rMessage.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public APIResponse Get(string URL, string? TOKEN = null)
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

        public APIResponse PostAsJson(string URL, object Data, string? TOKEN = null)
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
                    var postTask = client.PostAsJsonAsync(URL, Data);
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
