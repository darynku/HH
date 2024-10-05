using FluentResults;
using HH.Application.Files;
using FileInfo = HH.Application.Files.FileInfo;
using HH.Application.UnitOfWork;
using HH.Common.Contracts.Handlers;
using HH.Application.Messaging;
using HH.Domain.Shared.Files;

namespace HH.Application.Features.Vacancies.UploadFile
{
    public class UploadFileCommandHandler : ICommandHandler<UploadFileCommand, Result<Guid>>
    {
        private readonly IMinioProvider _minio;
        private readonly IVacancyService _vacancyService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessageQueue<IEnumerable<FileInfo>> _queue;
        private const string BucketName = "files";

        public UploadFileCommandHandler(
            IMinioProvider minio, 
            IVacancyService vacancyService, 
            IUnitOfWork unitOfWork, 
            IMessageQueue<IEnumerable<FileInfo>> queue)
        {
            _minio = minio;
            _vacancyService = vacancyService;
            _unitOfWork = unitOfWork;
            _queue = queue;
        }

        public async Task<Result<Guid>> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            var vacancy = await _vacancyService.GetById(request.VacancyId, cancellationToken);
            if (vacancy.IsFailed)
                return vacancy.ToResult();
           
            List<FileData> files = [];

            foreach(var file in request.Files)
            {
                
                var filePath = FilePath.Create(file.FileName);
                
                if (filePath.IsFailed)
                    return filePath.ToResult();

                var fileData = new FileData(file.Stream, new FileInfo(filePath.Value, BucketName));

                files.Add(fileData);    

            }

            var result = await _minio.UploadFiles(files, cancellationToken);

            if (result.IsFailed)
            {
                await _queue.WriteAsync(files.Select(f => f.Info), cancellationToken);
                return result.ToResult();
            }
            var itemFile = result.Value
                .Select(f => new ItemFile(f))
                .ToList();

            vacancy.Value.AddFiles(itemFile);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return vacancy.Value.Id;
        }
    }
}
