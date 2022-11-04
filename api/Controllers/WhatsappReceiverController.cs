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
    private readonly GetNearestTheater getNearestTheater;
    private GetMoviesInfo getMoviesInfo;

    public WhatsappReceiverController(
        MessageProcessor messageProcessor,
        GetNearestTheater getNearestTheater,
        WhatsappSendMessage sendmessage,
        GetMoviesInfo getMoviesInfo)
    {
        _sendmessage = sendmessage;
        _messageProcessor = messageProcessor;
        this.getNearestTheater = getNearestTheater;
        this.getMoviesInfo = getMoviesInfo;
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
            });

        Console.WriteLine($"recebeu: {receive.Body}");

        await _messageProcessor.Process();

        return Ok();
    }

    [HttpPost("test")]
    public async Task<ActionResult> test()
    {
        await getMoviesInfo.GetMovie(354912);
        return Ok();
    }

}
