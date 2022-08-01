namespace dotnetnbpgold.web.Services
{
    public class FileService : IFileService
    {
        private const string _filesPath = "files";
        private readonly ILogger<FileService> _logger;

        public FileService(ILogger<FileService> logger)
        {
            _logger = logger;
        }

        public async Task<bool> SaveTextFileAsync(string directoryName, string fileName, string content)
        {
            string newFileDirecroryPath = _filesPath + Path.DirectorySeparatorChar + directoryName;
            if (!Directory.Exists(newFileDirecroryPath)) {
                Directory.CreateDirectory(newFileDirecroryPath);
            }

            var path = string.Join(Path.DirectorySeparatorChar, _filesPath, directoryName, fileName);
            await File.WriteAllTextAsync(path, content);

            _logger.LogInformation("File: {fileName} saved to: {directoryName}", fileName, newFileDirecroryPath);
            return true;
        }
    }
} 