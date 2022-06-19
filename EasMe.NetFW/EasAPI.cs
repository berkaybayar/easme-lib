using EasMe.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace EasMe
{
    public class EasAPI
    {

        //Install-Package Microsoft.AspNet.WebApi.Client -Version 5.2.8

        /// <summary>
        /// Gets one of the values from Json API response.
        /// </summary>
        /// <param name="response"></param>
        /// <param name="parse"></param>
        /// <returns></returns>
        public string ParsefromJsonResponse(string response, string parse)
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

        /// <summary>
        /// Sends get request
        /// </summary>
        /// <param name="URL"></param>
        /// <param name="TOKEN"></param>
        /// <returns></returns>
        public APIResponseModel Get(string URL, string TOKEN = null)
        {
            HttpClient client = new HttpClient();
            var Response = new APIResponseModel();
            try
            {
                if(!string.IsNullOrEmpty(TOKEN))
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

        /// <summary>
        /// Sends post request
        /// </summary>
        /// <param name="URL"></param>
        /// <param name="Data"></param>
        /// <param name="TOKEN"></param>
        /// <returns></returns>
        public APIResponseModel PostAsJson(string URL, object Data, string TOKEN = null)
        {
            HttpClient client = new HttpClient();            
            var Response = new APIResponseModel();
            try
            {
                if (!string.IsNullOrEmpty(TOKEN))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);
                
                client.BaseAddress = new Uri(URL);
                var content = new StringContent(Data.ToString(), Encoding.UTF8, "application/json");
                var postTask = client.PostAsync(URL, content);
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

        public HttpResponseMessage SendGetRequest(string URL, string TOKEN = null)
        {
            HttpClient client = new HttpClient();
            if (!string.IsNullOrEmpty(TOKEN))
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);

            client.BaseAddress = new Uri(URL);
            var postTask = client.GetAsync(client.BaseAddress);
            postTask.Wait();
            return postTask.Result;
        }


        
        public HttpResponseMessage SendPostRequest(string URL, HttpContent Content, string TOKEN = null)
        {
            HttpClient client = new HttpClient();
            if (!string.IsNullOrEmpty(TOKEN))
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);
            client.BaseAddress = new Uri(URL);
            var postTask = client.PostAsync(URL, Content);
            postTask.Wait();
            return postTask.Result;
        }
       
        
        public HttpResponseMessage SendPutRequest(string URL, HttpContent Content, string TOKEN = null)
        {
            HttpClient client = new HttpClient();
            if (!string.IsNullOrEmpty(TOKEN))
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);
            client.BaseAddress = new Uri(URL);
            var postTask = client.PutAsync(URL, Content);
            postTask.Wait();
            return postTask.Result;
        }
        
        
    }
    


}
