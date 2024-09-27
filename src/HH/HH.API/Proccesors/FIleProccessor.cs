
using HH.Common.Contracts.DTO;

namespace HH.API.Proccesors;

public class FIleProccessor : IAsyncDisposable
{
    private readonly List<UploadFileDto> _files = [];

    public List<UploadFileDto> Proccess(IFormFileCollection files)
    {
        foreach(var file in files)
        {
            var stream = file.OpenReadStream();
            var fileDto = new UploadFileDto(stream, file.FileName);
            _files.Add(fileDto);
        }

        return _files;
    }

    public async ValueTask DisposeAsync()
    {
        foreach (var file in _files)
        {
            await file.Stream.DisposeAsync();
        }

    }
}
