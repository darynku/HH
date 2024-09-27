namespace HH.Domain.Shared.Files
{
    public record ItemFile
    {
        public ItemFile(FilePath pathtoStorage)
        {
            PathToStorage = pathtoStorage;
        }
        public FilePath PathToStorage { get; }

    }
}
