namespace dotnetnbpgold.web.Services
{
    public class FileService : IFileService
    {
        private const string _filesPath = "files";

        public FileService()
        {
            
        }       

        public async Task<bool> SaveTextFileAsync(string directoryName, string fileName, string content)
        {
            string newFileDirecroryPath = _filesPath + Path.DirectorySeparatorChar + directoryName;
            if (!Directory.Exists(newFileDirecroryPath)) {
                Directory.CreateDirectory(newFileDirecroryPath);
            }

            var path = string.Join(Path.DirectorySeparatorChar, _filesPath, directoryName, fileName);
            await File.WriteAllTextAsync(path, content);

            return true;
        }
    }
}