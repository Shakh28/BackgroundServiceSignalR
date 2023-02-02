using Microsoft.AspNetCore.Mvc;

namespace PaymentApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    [HttpPost]
    public IActionResult Pay([FromBody] PaymentDto paymentDto)
    {
        return Ok(new { success = true });
    }
}

public class PaymentDto
{
    public int UserId { get; set; }
    public int CardId { get; set; }
    public int Amount { get; set; }
}