using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text.Json;
using Twilio;
using Twilio.AspNet.Mvc;
using api.Interfaces.Message;
using api.Business;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class WhatsappReceiverController : ControllerBase
{
    private readonly WhatsappSendMessage _sendmessage;

    public WhatsappReceiverController(WhatsappSendMessage sendmessage)
    {
        _sendmessage = sendmessage;
    }

    [HttpPost]
    public async Task<ActionResult> ReceiveMessage([FromForm] Models.WhatsappMessage receive)
    {
        var message = new Models.Message {
            Id = receive.From, 
            Content = receive.Body
            , UserName = receive.ProfileName
        };

        await new MessageProcessor(_sendmessage, message).Process();
        return Ok();
    }
}
