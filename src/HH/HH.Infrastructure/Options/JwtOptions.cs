namespace HH.Infrastructure.Options
{
    public sealed class JwtOptions
    {
        public const string Jwt = nameof(Jwt);
        public string Key { get; init; } = string.Empty;
        public string Audience { get; init; } = string.Empty;
        public string Issuer { get; init; } = string.Empty;
    }
}
