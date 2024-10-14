namespace HH.Infrastructure.Options
{
    public class MailOptions
    {
        public string SenderEmail { get; init; } = string.Empty;
        public string Sender {  get; init; } = string.Empty;
        public string Host { get; init; } = string.Empty;
        public int Port { get; init; }
    }
}
