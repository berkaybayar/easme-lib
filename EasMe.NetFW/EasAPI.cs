using EasMe.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace EasMe
{
    public static class EasAPI
    {

        //Install-Package Microsoft.AspNet.WebApi.Client -Version 5.2.8

        /// <summary>
        /// Gets one of the values from Json API response.
        /// </summary>
        /// <param name="response"></param>
        /// <param name="parse"></param>
        /// <returns></returns>
        public static string ParsefromJsonResponse(string response, string parse)
        {
            try
            {
                var rMessage = JObject.Parse(response.ToString())[parse];
                return rMessage.ToString();
            }
            catch (Exception ex)
            {
                throw new EasException(Error.FAILED_TO_PARSE, "Failed to parse from Json response.", ex);
            }


        }

        /// <summary>
        /// Sends get request.
        /// </summary>
        /// <param name="URL"></param>
        /// <param name="TOKEN"></param>
        /// <returns></returns>
        public static APIResponseModel Get(string URL, string? TOKEN = null)
        {
            HttpClient client = new();
            var Response = new APIResponseModel();
            try
            {
                if (!string.IsNullOrEmpty(TOKEN))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);

                client.BaseAddress = new Uri(URL);
                var postTask = client.GetAsync(client.BaseAddress);
                postTask.Wait();
                var r = postTask.Result;
                Response.Status = r.IsSuccessStatusCode;
                Response.Content = r.Content.ReadAsStringAsync().Result;
                return Response;
                
            }
            catch (Exception ex)
            {
                throw new EasException(Error.FAILED_TO_SEND_GET, "Failed to get response from API.", ex);
            }
        }
        
        /// <summary>
        /// Sends post request.
        /// </summary>
        /// <param name="URL"></param>
        /// <param name="Data"></param>
        /// <param name="TOKEN"></param>
        /// <returns></returns>
        public static APIResponseModel PostAsJson(string URL, object Data, string? TOKEN = null)
        {
            HttpClient client = new();
            var Response = new APIResponseModel();
            try
            {
                if (!string.IsNullOrEmpty(TOKEN))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);

                client.BaseAddress = new Uri(URL);
                var postTask = client.PostAsync(URL, Data);
                postTask.Wait();
                var r = postTask.Result;
                Response.Status = r.IsSuccessStatusCode;
                Response.Content = r.Content.ReadAsStringAsync().Result.ToString();
                return Response;
                
            }
            catch (Exception e)
            {
                throw new EasException(Error.FAILED_TO_SEND_POST, "Failed to post response to API.", e);
            }

        }

        public static HttpResponseMessage SendGetRequest(string URL, string? TOKEN = null)
        {
            try
            {
                HttpClient client = new HttpClient();
                if (!string.IsNullOrEmpty(TOKEN))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);

                client.BaseAddress = new Uri(URL);
                var postTask = client.GetAsync(client.BaseAddress);
                postTask.Wait();
                return postTask.Result;
            }
            catch (Exception e)
            {
                throw new EasException(Error.FAILED_TO_SEND_GET, "Failed to get response from API.", e);
            }
        }


        public static HttpResponseMessage SendPostRequestAsJson(string URL, object Data, string? TOKEN = null)
        {
            try
            {
                HttpClient client = new HttpClient client();
                if (!string.IsNullOrEmpty(TOKEN))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);
                client.BaseAddress = new Uri(URL);
                var postTask = client.PostAsync(URL, Data);
                postTask.Wait();
                return postTask.Result;
            }
            catch (Exception e)
            {
                throw new EasException(Error.FAILED_TO_SEND_POST, "Failed to post json to API.", e);
            }
        }
        public static HttpResponseMessage SendPostRequest(string URL, HttpContent Content, string? TOKEN = null)
        {
            try
            {
                HttpClient client = new();
                if (!string.IsNullOrEmpty(TOKEN))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);
                client.BaseAddress = new Uri(URL);
                var postTask = client.PostAsync(URL, Content);
                postTask.Wait();
                return postTask.Result;
            }
            catch (Exception e)
            {
                throw new EasException(Error.FAILED_TO_SEND_POST, "Failed to post request to API.", e);
            }
        }
        public static HttpResponseMessage SendDeleteRequest(string URL, string? TOKEN = null)
        {
            try{
                HttpClient client = new HttpClient client();
                if (!string.IsNullOrEmpty(TOKEN))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);
                client.BaseAddress = new Uri(URL);
                var postTask = client.DeleteAsync(URL);
                postTask.Wait();
                return postTask.Result;
            }
            catch (Exception e)
            {
                throw new EasException(Error.FAILED_TO_SEND_DELETE, "Failed to send delete request to API.", e);
            }
        }
        public static HttpResponseMessage SendPatchRequest(string URL, HttpContent Content, string? TOKEN = null)
        {
            try
            {
                HttpClient client = new HttpClient();
                if (!string.IsNullOrEmpty(TOKEN))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);
                client.BaseAddress = new Uri(URL);
                var postTask = client.PatchAsync(URL, Content);
                postTask.Wait();
                return postTask.Result;
            }
            catch (Exception e)
            {
                throw new EasException(Error.FAILED_TO_SEND_PATCH, "Failed to send patch request to API.", e);
            }
        }
        public static HttpResponseMessage SendPutRequest(string URL, HttpContent Content, string? TOKEN = null)
        {
            try
            {
                HttpClient client = new HttpClient client();
                if (!string.IsNullOrEmpty(TOKEN))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);
                client.BaseAddress = new Uri(URL);
                var postTask = client.PutAsync(URL, Content);
                postTask.Wait();
                return postTask.Result;
            }
            catch (Exception e)
            {
                throw new EasException(Error.FAILED_TO_SEND_PUT, "Failed to send put request to API.", e);
            }
        }
        public static HttpResponseMessage SendPutRequestAsJson(string URL, object Data, string? TOKEN = null)
        {
            try
            {
                HttpClient client = new HttpClient();
                if (!string.IsNullOrEmpty(TOKEN))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);
                client.BaseAddress = new Uri(URL);
                var postTask = client.PutAsJsonAsync(URL, Data);
                postTask.Wait();
                return postTask.Result;
            }
            catch (Exception e)
            {
                throw new EasException(Error.FAILED_TO_SEND_PUT, "Failed to send put json request to API.", e);
            }
        }
        public static HttpResponseMessage Send(string URL, HttpRequestMessage Data, string? TOKEN = null)
        {
            try
            {
                HttpClient client = new HttpClient();
                if (!string.IsNullOrEmpty(TOKEN))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);
                client.BaseAddress = new Uri(URL);
                var postTask = client.SendAsync(Data);
                return postTask;
            }
            catch (Exception e)
            {
                throw new EasException(Error.FAILED_TO_SEND_REQUEST, "Failed to send request to API.", e);
            }
        }
    }



}
