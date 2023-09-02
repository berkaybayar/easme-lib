using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace EasMe;

public static class EasAPI
{
  public static HttpResponseMessage Get(string url,
                                        string? token = null,
                                        int timeout = 10) {
    var client = new HttpClient();
    if (!string.IsNullOrEmpty(token))
      client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    client.Timeout = TimeSpan.FromSeconds(timeout);
    client.BaseAddress = new Uri(url);
    var postTask = client.GetAsync(client.BaseAddress);
    postTask.Wait();
    return postTask.Result;
  }

  public static T? GetAndReadJson<T>(string url,
                                     string? token = null,
                                     int timeout = 10) {
    var client = new HttpClient();
    if (!string.IsNullOrEmpty(token))
      client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    client.Timeout = TimeSpan.FromSeconds(timeout);
    client.BaseAddress = new Uri(url);
    var postTask = client.GetAsync(client.BaseAddress);
    postTask.Wait();
    var content = postTask.Result.Content.ReadFromJsonAsync<T>().GetAwaiter().GetResult();
    return content;
  }

  public static T? PostAsJsonAndReadJson<T>(string url,
                                            object data,
                                            string? token = null,
                                            int timeout = 10) {
    HttpClient client = new();
    if (!string.IsNullOrEmpty(token))
      client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    client.BaseAddress = new Uri(url);
    client.Timeout = TimeSpan.FromSeconds(timeout);
    var postTask = client.PostAsJsonAsync(url, data);
    postTask.Wait();
    var content = postTask.Result.Content.ReadFromJsonAsync<T>().GetAwaiter().GetResult();
    return content;
  }

  public static HttpResponseMessage PostAsJson(string url,
                                               object data,
                                               string? token = null,
                                               int timeout = 10) {
    HttpClient client = new();
    if (!string.IsNullOrEmpty(token))
      client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    client.BaseAddress = new Uri(url);
    client.Timeout = TimeSpan.FromSeconds(timeout);
    var postTask = client.PostAsJsonAsync(url, data);
    postTask.Wait();
    return postTask.Result;
  }


  public static HttpResponseMessage Delete(string url,
                                           string? token = null,
                                           int timeout = 10) {
    HttpClient client = new();
    if (!string.IsNullOrEmpty(token))
      client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    client.BaseAddress = new Uri(url);
    client.Timeout = TimeSpan.FromSeconds(timeout);
    var postTask = client.DeleteAsync(url);
    postTask.Wait();
    return postTask.Result;
  }


  public static HttpResponseMessage PutAsJson(string url,
                                              object data,
                                              string? token = null,
                                              int timeout = 10) {
    var client = new HttpClient();
    if (!string.IsNullOrEmpty(token))
      client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    client.BaseAddress = new Uri(url);
    client.Timeout = TimeSpan.FromSeconds(timeout);
    var postTask = client.PutAsJsonAsync(url, data);
    postTask.Wait();
    return postTask.Result;
  }

  public static HttpResponseMessage Send(string url,
                                         HttpRequestMessage data,
                                         string? token = null,
                                         int timeout = 10) {
    var client = new HttpClient();
    if (!string.IsNullOrEmpty(token))
      client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    client.BaseAddress = new Uri(url);
    client.Timeout = TimeSpan.FromSeconds(timeout);
    var postTask = client.Send(data);
    return postTask;
  }
}