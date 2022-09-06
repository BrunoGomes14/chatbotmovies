using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text.Json;
using Twilio;
using Twilio.AspNet.Mvc;
using api.Interfaces;
using api.Business;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class WhatsappReceiverController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> ReceiveMessage([FromForm] Models.WhatsappMessage receive)
    {
        await new MessageProcessor(new WhatsappSendMessage()).Process(new Models.Message{Id = receive.From, Content = receive.Body, UserName = receive.ProfileName});
        
        return Ok();
    }
}
