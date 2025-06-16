namespace GestionCompte;

public interface ICsvReader
{
    string[] GetLines(string csvFilePath);
}

public class CsvReader : ICsvReader
{
    public string[] GetLines(string csvFilePath)
    {
        return File.ReadAllLines(csvFilePath);
    }
}