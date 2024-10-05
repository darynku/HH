using FluentResults;
using HH.Domain.Shared.Files;

namespace HH.Application.Files;
public interface IMinioProvider
{
    Task<string> CreatePresignedUrl(Application.Files.FileInfo fileInfo, int expirationTime);
    Task<Result> RemoveFile(FileInfo fileInfo, CancellationToken cancellationToken = default);
    Task<Result<IReadOnlyList<FilePath>>> UploadFiles(IEnumerable<FileData> filesData, CancellationToken cancellationToken = default);
}