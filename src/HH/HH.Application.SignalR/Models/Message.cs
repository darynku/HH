namespace HH.Application.Chat.Models
{
    public record Message(string UserName, string MessageText, DateTime CreatedDate);
}
