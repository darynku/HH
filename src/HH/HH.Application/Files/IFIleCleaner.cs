namespace HH.Application.Files
{
    public interface IFIleCleaner
    {
        Task Clean(CancellationToken cancellationToken);
    }
}