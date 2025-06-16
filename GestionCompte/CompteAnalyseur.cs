using GestionCompte.models;

namespace GestionCompte;

public class CompteAnalyseur
{
    private readonly IHistoriqueCompteReader _reader;
    private readonly string _csvFilePath;

    public CompteAnalyseur(IHistoriqueCompteReader reader, string csvFilePath)
    {
        _reader = reader;
        _csvFilePath = csvFilePath;
    }

    public CompteRapport GetValeurADate(DateOnly date)
    {
        var transactions = _reader.GetTransactions(_csvFilePath);

        var rapport = transactions
            .Where(transaction => transaction.Date <= date)
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