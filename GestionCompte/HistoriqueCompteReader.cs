using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using GestionCompte.models;

namespace GestionCompte;

public interface IHistoriqueCompteReader
{
    Transaction[] GetTransactions(string csvFilePath);
}

public class HistoriqueCompteReader : IHistoriqueCompteReader
{
    private readonly IFileReader _fileReader;

    public HistoriqueCompteReader(IFileReader fileReader)
    {
        _fileReader = fileReader;
    }
    
    public Transaction[] GetTransactions(string csvFilePath)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ";"
        };
        using var reader = _fileReader.GetStream(csvFilePath);
        using var csv = new CsvReader(reader, config);
        
        var options = new TypeConverterOptions { Formats = new[] { "dd/MM/yyyy" } };
        csv.Context.TypeConverterOptionsCache.AddOptions<DateOnly>(options);
        
        return csv.GetRecords<Transaction>().ToArray();
    }
}