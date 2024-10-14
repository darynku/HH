using FluentEmail.Core;

namespace HH.Infrastructure.Providers
{
    public class MailProvider
    {
        private readonly IFluentEmail _fluentEmail;

        public MailProvider(IFluentEmail fluentEmail)
        {
            _fluentEmail = fluentEmail;
        }

    }
}
