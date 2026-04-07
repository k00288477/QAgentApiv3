using System;
using System.Net.Http;
using System.Text.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace QAgentApi.Tests;

public class ApiSmokeTests : IDisposable
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public ApiSmokeTests()
    {
        _baseUrl = Environment.GetEnvironmentVariable("API_BASE_URL")
                  ?? "http://localhost:5078";
        _httpClient = new HttpClient { BaseAddress = new Uri(_baseUrl) };
    }
    public void Dispose()
    {
        _httpClient.Dispose();
    }

    [Fact]
    public async Task HealthCheck_ReturnsOK()
    {
        var response = await _httpClient.GetAsync("/");
        Assert.True(
            response.StatusCode == System.Net.HttpStatusCode.OK ||
            response.StatusCode == System.Net.HttpStatusCode.NotFound,
            $"SAPI unreachable. Status code: {response.StatusCode}"
            );
    }

    [Fact]
    public async Task LoginWithValidCreds_ReturnsOkWithToken()
    {
        var payload = JsonSerializer.Serialize(new
        {
            email = "test@account.com",
            password = "password"
        });

        var content = new StringContent(payload, System.Text.Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("api/Auth/Login", content);
        var body = await response.Content.ReadAsStringAsync();

        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        Assert.False(string.IsNullOrEmpty(body), "Expected a JWT token in the response body");
    }
}
