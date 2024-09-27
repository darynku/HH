﻿using HH.Application.Messaging;
using System.Threading.Channels;

namespace HH.Infrastructure.MessageQueues
{
    public class MessageQueue<TMessage> : IMessageQueue<TMessage>
    {
        private readonly Channel<TMessage> _channel = Channel.CreateUnbounded<TMessage>();

        public async Task WriteAsync(TMessage message, CancellationToken cancellationToken = default)
        {
            await _channel.Writer.WriteAsync(message, cancellationToken);
        }

        public async Task<TMessage> ReadAsync(CancellationToken cancellationToken = default)
        {
            return await _channel.Reader.ReadAsync(cancellationToken);
        }
    }
}   
