using dotnetnbpgold.web.Models.Settings;
using Microsoft.Extensions.Options;

namespace dotnetnbpgold.web.Services
{
    public class FileService : IFileService
    {
        private readonly ILogger<FileService> _logger;
        private readonly FileServiceSettings _settings;

        public FileService(
            ILogger<FileService> logger,
            IOptions<FileServiceSettings> settings)
        {
            _logger = logger;
            _settings = settings.Value;
        }

        public async Task SaveTextFileAsync(string directoryName, string fileName, string content)
        {
            string newFileDirecroryPath = Path.Combine(_settings.Path, directoryName);
            if (!Directory.Exists(newFileDirecroryPath))
            {
                Directory.CreateDirectory(newFileDirecroryPath);
            }
            
            var filePath = Path.Combine(_settings.Path, directoryName, fileName);
            await File.WriteAllTextAsync(filePath, content);

            _logger.LogInformation("File: {fileName} saved to: {directoryName}", fileName, newFileDirecroryPath);
        }
    }
} 