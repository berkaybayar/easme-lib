using EasMe.Models;
using EasMe.Exceptions;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace EasMe
{
    public static class EasAPI
    {

        //Install-Package Microsoft.AspNet.WebApi.Client -Version 5.2.8


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
                throw new ApiSendFailedToSendException("Failed to get response from API.", e);
            }
        }


        public static HttpResponseMessage SendPostRequestAsJson(string URL, object Data, string? TOKEN = null)
        {
            try
            {
                HttpClient client = new();
                if (!string.IsNullOrEmpty(TOKEN))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);
                client.BaseAddress = new Uri(URL);
                var postTask = client.PostAsJsonAsync(URL, Data);
                postTask.Wait();
                return postTask.Result;
            }
            catch (Exception e)
            {
                throw new ApiSendFailedToPostException("Failed to post json to API.", e);
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
                throw new ApiSendFailedToPostException( "Failed to post request to API.", e);
            }
        }
        public static HttpResponseMessage SendDeleteRequest(string URL, string? TOKEN = null)
        {
            try{
                HttpClient client = new();
                if (!string.IsNullOrEmpty(TOKEN))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);
                client.BaseAddress = new Uri(URL);
                var postTask = client.DeleteAsync(URL);
                postTask.Wait();
                return postTask.Result;
            }
            catch (Exception e)
            {
                throw new ApiSendFailedToDeleteException( "Failed to send delete request to API.", e);
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
                throw new ApiSendFailedToPatchException("Failed to send patch request to API.", e);
            }
        }
        public static HttpResponseMessage SendPutRequest(string URL, HttpContent Content, string? TOKEN = null)
        {
            try
            {
                HttpClient client = new();
                if (!string.IsNullOrEmpty(TOKEN))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);
                client.BaseAddress = new Uri(URL);
                var postTask = client.PutAsync(URL, Content);
                postTask.Wait();
                return postTask.Result;
            }
            catch (Exception e)
            {
                throw new ApiSendFailedToPutException( "Failed to send put request to API.", e);
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
                throw new ApiSendFailedToPutException("Failed to send put json request to API.", e);
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
                var postTask = client.Send(Data);
                return postTask;
            }
            catch (Exception e)
            {
                throw new ApiSendFailedToSendException("Failed to send request to API.", e);
            }
        }
    }



}
