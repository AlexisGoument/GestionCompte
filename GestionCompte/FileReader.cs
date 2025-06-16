namespace GestionCompte;

public interface IFileReader
{
    StreamReader GetStream(string csvFilePath);
}

public class FileReader : IFileReader
{
    public StreamReader GetStream(string csvFilePath)
    {
        return new StreamReader(csvFilePath);
    }
}