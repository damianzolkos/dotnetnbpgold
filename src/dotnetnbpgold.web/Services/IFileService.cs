namespace dotnetnbpgold.web.Services
{
    public interface IFileService
    {
     Task SaveTextFileAsync(string directoryName, string fileName, string content);   
    }
}