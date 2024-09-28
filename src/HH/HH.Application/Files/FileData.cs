using HH.Domain.Shared.Files;

namespace HH.Application.Files
{
    public record FileData(Stream Stream, FileInfo Info);
    public record FileInfo(FilePath FilePath, string BucketName);
}
