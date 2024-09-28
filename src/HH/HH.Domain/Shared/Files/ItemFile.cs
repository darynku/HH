using System.Text.Json.Serialization;

namespace HH.Domain.Shared.Files
{
    public record ItemFile
    {
        [JsonConstructor]
        public ItemFile(FilePath pathtoStorage)
        {
            PathToStorage = pathtoStorage;
        }
        public FilePath PathToStorage { get; }

    }
}
