namespace dotnetnbpgold.web.Services
{
    public interface IFileService
    {
     Task<bool> SaveTextFileAsync(string directoryName, string fileName, string content);   
    }
}