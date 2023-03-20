using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace EasMe;

public static class EasAPI
{
    //Install-Package Microsoft.AspNet.WebApi.Client -Version 5.2.8


    public static HttpResponseMessage SendGetRequest(string URL, string? TOKEN = null, int timeout = 10)
    {
        var client = new HttpClient();
        if (!string.IsNullOrEmpty(TOKEN))
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);
        client.Timeout = TimeSpan.FromSeconds(timeout);
        client.BaseAddress = new Uri(URL);
        var postTask = client.GetAsync(client.BaseAddress);
        postTask.Wait();
        return postTask.Result;
    }

    public static T? SendGetRequestAndGetResponseAsJson<T>(string URL, string? TOKEN = null, int timeout = 10)
    {
        var client = new HttpClient();
        if (!string.IsNullOrEmpty(TOKEN))
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);
        client.Timeout = TimeSpan.FromSeconds(timeout);
        client.BaseAddress = new Uri(URL);
        var postTask = client.GetAsync(client.BaseAddress);
        postTask.Wait();
        var content = postTask.Result.Content.ReadFromJsonAsync<T>().GetAwaiter().GetResult();
        return content;
    }

    public static T? SendPostRequestAsJsonAndGetResponseAsJson<T>(string URL, object Data, string? TOKEN = null,
        int timeout = 10)
    {
        HttpClient client = new();
        if (!string.IsNullOrEmpty(TOKEN))
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);
        client.BaseAddress = new Uri(URL);
        client.Timeout = TimeSpan.FromSeconds(timeout);
        var postTask = client.PostAsJsonAsync(URL, Data);
        postTask.Wait();
        var content = postTask.Result.Content.ReadFromJsonAsync<T>().GetAwaiter().GetResult();
        return content;
    }

    public static HttpResponseMessage SendPostRequestAsJson(string URL, object Data, string? TOKEN = null,
        int timeout = 10)
    {
        HttpClient client = new();
        if (!string.IsNullOrEmpty(TOKEN))
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);
        client.BaseAddress = new Uri(URL);
        client.Timeout = TimeSpan.FromSeconds(timeout);
        var postTask = client.PostAsJsonAsync(URL, Data);
        postTask.Wait();
        return postTask.Result;
    }

    public static HttpResponseMessage SendPostRequest(string URL, HttpContent Content, string? TOKEN = null,
        int timeout = 10)
    {
        HttpClient client = new();
        if (!string.IsNullOrEmpty(TOKEN))
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);
        client.BaseAddress = new Uri(URL);
        client.Timeout = TimeSpan.FromSeconds(timeout);
        var postTask = client.PostAsync(URL, Content);
        postTask.Wait();
        return postTask.Result;
    }

    public static HttpResponseMessage SendDeleteRequest(string URL, string? TOKEN = null, int timeout = 10)
    {
        HttpClient client = new();
        if (!string.IsNullOrEmpty(TOKEN))
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);
        client.BaseAddress = new Uri(URL);
        client.Timeout = TimeSpan.FromSeconds(timeout);
        var postTask = client.DeleteAsync(URL);
        postTask.Wait();
        return postTask.Result;
    }

    public static HttpResponseMessage SendPatchRequest(string URL, HttpContent Content, string? TOKEN = null,
        int timeout = 10)
    {
        var client = new HttpClient();
        if (!string.IsNullOrEmpty(TOKEN))
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);
        client.BaseAddress = new Uri(URL);
        client.Timeout = TimeSpan.FromSeconds(timeout);
        var postTask = client.PatchAsync(URL, Content);
        postTask.Wait();
        return postTask.Result;
    }

    public static HttpResponseMessage SendPutRequest(string URL, HttpContent Content, string? TOKEN = null,
        int timeout = 10)
    {
        HttpClient client = new();
        if (!string.IsNullOrEmpty(TOKEN))
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);
        client.BaseAddress = new Uri(URL);
        client.Timeout = TimeSpan.FromSeconds(timeout);
        var postTask = client.PutAsync(URL, Content);
        postTask.Wait();
        return postTask.Result;
    }

    public static HttpResponseMessage SendPutRequestAsJson(string URL, object Data, string? TOKEN = null,
        int timeout = 10)
    {
        var client = new HttpClient();
        if (!string.IsNullOrEmpty(TOKEN))
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);
        client.BaseAddress = new Uri(URL);
        client.Timeout = TimeSpan.FromSeconds(timeout);
        var postTask = client.PutAsJsonAsync(URL, Data);
        postTask.Wait();
        return postTask.Result;
    }

    public static HttpResponseMessage Send(string URL, HttpRequestMessage Data, string? TOKEN = null, int timeout = 10)
    {
        var client = new HttpClient();
        if (!string.IsNullOrEmpty(TOKEN))
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);
        client.BaseAddress = new Uri(URL);
        client.Timeout = TimeSpan.FromSeconds(timeout);
        var postTask = client.Send(Data);
        return postTask;
    }
}