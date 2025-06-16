using GestionCompte.models;

namespace GestionCompte;

public class CompteAnalyseur
{
    private readonly ICsvReader _reader;
    private readonly ITransactionFactory _factory;
    private readonly string _csvFilePath;

    public CompteAnalyseur(ICsvReader reader, ITransactionFactory factory, string csvFilePath)
    {
        _reader = reader;
        _factory = factory;
        _csvFilePath = csvFilePath;
    }

    public CompteRapport GetValeurADate(DateOnly date)
    {
        var lines = _reader.GetLines(_csvFilePath);
        var operations = lines.Select(line => _factory.GetTransaction(line));

        var rapport = operations
            .Where(operation => operation.Date <= date)
            .Aggregate(new CompteRapport(),
                (rapport, transaction) =>
                {
                    rapport.Balance += transaction.Montant;
                    return rapport;
                });
        return rapport;
    }
}