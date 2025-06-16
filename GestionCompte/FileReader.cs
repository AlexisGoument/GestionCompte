namespace GestionCompte;

public interface IFileReader
{
    StreamReader GetStream(string csvFilePath);
}

public class FileReader : IFileReader
{
    public StreamReader GetStream(string csvFilePath)
    {
        var reader = new StreamReader(csvFilePath);
        
        //Ignore first 3 lines
        reader.ReadLine();
        reader.ReadLine();
        reader.ReadLine();
        
        return reader;
    }
}