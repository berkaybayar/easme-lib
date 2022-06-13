using EasMe.Models;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace EasMe
{
    public class EasAPI
    {

        //Install-Package Microsoft.AspNet.WebApi.Client -Version 5.2.8

        //Parsing will only return one of the items as string
        private static readonly HttpClient client = new HttpClient();
        public  string ParsefromJsonResponse(string response, string parse)
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
        public  APIResponseModel Get(string URL, string? TOKEN = null)
        {
            var Response = new APIResponseModel();
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);
                
                client.BaseAddress = new Uri(URL);
                var postTask = client.GetAsync(client.BaseAddress);
                postTask.Wait();
                var r = postTask.Result;
                Response.Status = r.IsSuccessStatusCode;
                Response.Content = r.Content.ReadAsStringAsync().Result;
            }
            catch (Exception e)
            {
                Response.Status = false;
                Response.Content = e.Message;
            }
            return Response;
        }

        public  APIResponseModel PostAsJson(string URL, object Data, string? TOKEN = null)
        {
            var Response = new APIResponseModel();
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);
                
                client.BaseAddress = new Uri(URL);
                var postTask = client.PostAsJsonAsync(URL, Data);
                postTask.Wait();
                var r = postTask.Result;
                Response.Status = r.IsSuccessStatusCode;
                Response.Content = r.Content.ReadAsStringAsync().Result.ToString();
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
