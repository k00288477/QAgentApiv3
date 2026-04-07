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
    public async Task LoginVithInvalidCreds_ReturnsNotFound()
    {
        var payload = JsonSerializer.Serialize(new
        {
            email = "invalid@email.com",
            password = "password"
        });

        var content = new StringContent(payload, System.Text.Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("api/Auth/Login", content);
        var body = await response.Content.ReadAsStringAsync();

        Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task LoginWithValidCreds_ReturnsOkWithToken()
    {
        var payload = JsonSerializer.Serialize(new
        {
            email = Environment.GetEnvironmentVariable("TEST_USER_EMAIL") ?? "test@account.com",
            password = Environment.GetEnvironmentVariable("TEST_USER_PASSWORD") ?? "password"
        });

        var content = new StringContent(payload, System.Text.Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("api/Auth/Login", content);
        var body = await response.Content.ReadAsStringAsync();

        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        Assert.False(string.IsNullOrEmpty(body), "Expected a JWT token in the response body");
    }

    // Try to access protected endpoint without token
    [Fact]
    public async Task GetUserProfile_WithoutToken_ReturnsUnauthorized()
    {
        var response = await _httpClient.GetAsync("api/User/getUserProfile");
        Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
    }

    // Use an invalid token to access protected endpoint
    [Fact]
    public async Task GetUserProfile_WithInvalidToken_ReturnsUnauthorized()
    {
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "invalidtoken123");
        var response = await _httpClient.GetAsync("api/User/getUserProfile");
        Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
        _httpClient.DefaultRequestHeaders.Authorization = null;
    }

    // Checks login, recieve token, use token to access protected endpoint
    [Fact]
    public async Task GetUserProfile_WithValidToken_ReturnsOkWithProfile()
    {
        // Login first to get a token
        var payload = JsonSerializer.Serialize(new
        {
            email = Environment.GetEnvironmentVariable("TEST_USER_EMAIL") ?? "test@account.com",
            password = Environment.GetEnvironmentVariable("TEST_USER_PASSWORD") ?? "password"
        });
        var content = new StringContent(payload, System.Text.Encoding.UTF8, "application/json");
        var loginResponse = await _httpClient.PostAsync("api/Auth/Login", content);
        var token = (await loginResponse.Content.ReadAsStringAsync()).Trim('"');

        // Use token to access profile
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.GetAsync("api/User/getUserProfile");
        var body = await response.Content.ReadAsStringAsync();

        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        Assert.Contains("email", body.ToLower());
        _httpClient.DefaultRequestHeaders.Authorization = null;
    }

    // Tests that the Backend can connect to the AI Engine
    [Fact]
    public async Task TestAIEngineConnection_ReturnsOK()
    {
        var response = await _httpClient.GetAsync("api/TestExecution/CheckAIEngineConnection");
        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
    }
}
