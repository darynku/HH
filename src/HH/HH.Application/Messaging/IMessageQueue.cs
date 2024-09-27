namespace HH.Application.Messaging
{
    public interface IMessageQueue<TMessage>
    {
        Task<TMessage> ReadAsync(CancellationToken cancellationToken = default);
        Task WriteAsync(TMessage message, CancellationToken cancellationToken = default);
    }
}