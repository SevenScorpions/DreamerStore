using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text;
namespace DreamerStore2.Api;

public class ApiClient
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public ApiClient()
    {
        _httpClient = new HttpClient();
        //_baseUrl = baseUrl.TrimEnd('/');
        _baseUrl = GetBaseUrl();
    }

    private string GetBaseUrl()
    {
        IConfiguration config = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true)
                    .Build();
        var baseUrl = config["AppSettings:ApiRoot"];

        return baseUrl;
    }

    public async Task<T> GetAsync<T>(string endpoint)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_baseUrl + "/" + endpoint);
            response.EnsureSuccessStatusCode(); // Throws if not successful

            string jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
        catch (HttpRequestException ex)
        {
            return await GetHomeDataAsync<T>();
        }
    }

    public async Task<T> PostAsync<T>(string endpoint, object data)
    {
        try
        {
            string jsonData = JsonConvert.SerializeObject(data);
            HttpResponseMessage response = await _httpClient.PostAsync(_baseUrl + "/" + endpoint, new StringContent(jsonData, Encoding.UTF8, "application/json"));

            string jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
        catch (HttpRequestException ex)
        {
            throw new ApiException("An error occurred while calling the API.", ex);
        }
    }

    public async Task<T> PutAsync<T>(string endpoint, object data)
    {
        try
        {
            string jsonData = JsonConvert.SerializeObject(data);
            HttpResponseMessage response = await _httpClient.PutAsync(_baseUrl + "/" + endpoint, new StringContent(jsonData, Encoding.UTF8, "application/json"));

            string jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
        catch (HttpRequestException ex)
        {
            throw new ApiException("An error occurred while calling the API.", ex);
        }
    }

    public async Task DeleteAsync(string endpoint)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync(_baseUrl + "/" + endpoint);
        }
        catch (HttpRequestException ex)
        {
            throw new ApiException("An error occurred while calling the API.", ex);
        }
    }

    private async Task<T> GetHomeDataAsync<T>()
    {
        return default(T);
    }
}

    public class ApiException : Exception
{
    public ApiException(string message) : base(message) { }

    public ApiException(string message, Exception innerException) : base(message, innerException) { }
}


/*
static async Task Main(string[] args)
    {
        ApiClient apiClient = new ApiClient("https://api.example.com");

        try
        {
            string response = await apiClient.GetAsync("endpoint");
            Console.WriteLine("Response: " + response);
        }
        catch (ApiException ex)
        {
            Console.WriteLine("API Error: " + ex.Message);
        }
    } 
 */