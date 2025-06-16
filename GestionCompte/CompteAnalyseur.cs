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
        var operations = _factory.GetTransactions(lines);

        var rapport = operations
            .Where(operation => operation.Date <= date)
            .Aggregate(new CompteRapport(),
                (rapport, transaction) =>
                {
                    rapport.Balance += transaction.Devise switch
                    {
                        "USD" => transaction.Montant * 1.445,
                        "JPY" => transaction.Montant * 0.482,
                        "EUR" => transaction.Montant,
                        _ => throw new ArgumentException($"La devise {transaction.Devise} n'est pas pris en charge par le système.")
                    };
                    return rapport;
                });
        return rapport;
    }
}