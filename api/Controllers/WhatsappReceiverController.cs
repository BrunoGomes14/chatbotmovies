using Microsoft.AspNetCore.Mvc;
using api.Interfaces.Message;
using api.Business;
using api.Services;
using System.Globalization;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class WhatsappReceiverController : ControllerBase
{
    private readonly WhatsappSendMessage _sendmessage;
    private readonly MessageProcessor _messageProcessor;

    public WhatsappReceiverController(
        MessageProcessor messageProcessor,
        WhatsappSendMessage sendmessage)
    {
        _sendmessage = sendmessage;
        _messageProcessor = messageProcessor;
    }

    [HttpPost]
    public async Task<ActionResult> ReceiveMessage([FromForm] Models.WhatsappMessage receive)
    {
        _messageProcessor.Setup(
            _sendmessage,
            new Models.Message
            {
                Id = receive.From,
                Content = receive.Body,
                UserName = receive.ProfileName,
                Latitude = Convert.ToDecimal(receive.Latitude ?? "0", new CultureInfo("en-US")),
                Longitude = Convert.ToDecimal(receive.Longitude ?? "0", new CultureInfo("en-US")),
            },
            Models.Plataform.WHATSAPP);

        await _messageProcessor.Process();

        return Ok();
    }

    [HttpGet("test")]
    public ActionResult test()
    {
        return Ok();
    }

}
