namespace SchoolManagementSystem.Core.Api.Hubs;
using Microsoft.AspNetCore.SignalR;

public class ChatHub: Hub   
{
    public async Task SendMessage(string username, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", username, message);
    }

    public async Task ClearMessage(string username, string message)
    {
        await Clients.All.SendAsync("CleardMessages", username, message);
    }
    public async Task SendMessageToCaller(string user, string message)
    => await Clients.Caller.SendAsync("ReceiveMessage", user, message);

    public async Task SendMessageToGroup(string user, string message)
        => await Clients.Group("SignalR Users").SendAsync("ReceiveMessage", user, message);
    /*    private string ProcessMessageWithChatGPT(string message)
        {
            // Call the ChatGPT API here and return the response
            // You may use an API key for authentication

            // For simplicity, let's assume a synchronous method for illustration
            string responseFromChatGPT = ChatGPTService.GetResponse(message);

            return responseFromChatGPT;
        }*/
}
