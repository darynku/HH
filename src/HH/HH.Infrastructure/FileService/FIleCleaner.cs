using FileInfo = HH.Application.Files.FileInfo;
using HH.Application.Messaging;
using Microsoft.Extensions.Logging;
using HH.Application.Files;

namespace HH.Infrastructure.FileService
{
    public class FIleCleaner : IFIleCleaner
    {
        private readonly IMinioProvider _minioProvider;
        private readonly IMessageQueue<IEnumerable<FileInfo>> _queue;
        private readonly ILogger<FIleCleaner> _logger;
        public FIleCleaner(
            IMinioProvider minioProvider,
            IMessageQueue<IEnumerable<FileInfo>> queue,
            ILogger<FIleCleaner> logger)
        {
            _minioProvider = minioProvider;
            _queue = queue;
            _logger = logger;
        }

        public async Task Clean(CancellationToken cancellationToken)
        {
            var fileInfos = await _queue.ReadAsync(cancellationToken);
            foreach (var fileInfo in fileInfos)
            {
                await _minioProvider.RemoveFile(fileInfo, cancellationToken);
                _logger.LogInformation($"removed file {fileInfo}");
            }
        }

    }
}
