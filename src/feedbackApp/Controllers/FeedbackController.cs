using Microsoft.AspNetCore.Mvc;
namespace feedbackApp.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class FeedbackController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { Message = "We have received your feedback. Thankyou." });
    }
}