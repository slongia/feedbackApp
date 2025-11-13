using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace feedbackApp.Tests.Controllers;

public class FeedbackControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    public FeedbackControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Get_ReturnFeedbackMesssage()
    {
        // act
        var response = await _client.GetAsync("/api/feedback");
        //assert status code
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        //assert response body
        var payload = await response.Content.ReadFromJsonAsync<FeedbackResponse>();
        Assert.NotNull(payload);
        Assert.Equal("We have received your feedback. Thankyou.", payload!.Message);
    }

    private sealed class FeedbackResponse
    {
        public string? Message { get; set; }
    }
}