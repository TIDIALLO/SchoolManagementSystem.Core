using AutoMapper;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Core.Controllers;
using SchoolManagementSystem.Portal.Shared.Response;
using SchoolManagementSystem.Portal.Shared.Result;

namespace SchoolManagementSystem.Core.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChatController : ControllerBase
{
    public readonly ILogger<CoursesController> _logger;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    
    public ChatController(IServiceProvider serviceProvider)
    {
        _mapper = serviceProvider.GetRequiredService<IMapper>();
    }

    private static List<ChatResponse> _chat = new();
    

    //Save chat
    [HttpPost]
    [Route("send")]
    public async Task<IActionResult> Send(ChatResponse request)
    {
        await Task.CompletedTask;
        var chatMessage = _mapper.Map<ChatResponse>(request);
        _chat.Add(chatMessage);
        return Ok();
    }

    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> All() {
        await Task.CompletedTask;
        return Ok(await Result<List<ChatResponse>>.SuccessAsync(_chat));
    }

    [HttpGet]
    [Route("clear")]
    public async Task<IActionResult> Clear()
    {
        await Task.CompletedTask;
        _chat.Clear();
        return Ok();
    }


}
