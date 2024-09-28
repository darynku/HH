using FluentResults;

namespace HH.Domain.Shared.Files
{
    public class FilePath
    {
        public FilePath(string path)
        {
            Path = path;
        }
        public string Path { get; }

        public static Result<FilePath> Create(string fullPath)
        {
            if (string.IsNullOrEmpty(fullPath))
                return Result.Fail("Null puth");

            return new FilePath(fullPath);
        }

        public static Result<FilePath> Create(string path, string extension)
        {
            if (string.IsNullOrEmpty(path))
                return Result.Fail("Null puth");

            var fullPath = path + extension;

            return new FilePath(fullPath);
        }
    }
}
