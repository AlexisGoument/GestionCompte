using GestionCompte.models;

namespace GestionCompte;

public interface ITransactionFactory
{
    Transaction[] GetTransactions(string[] lines);
}

public class TransactionFactory : ITransactionFactory
{
    public Transaction[] GetTransactions(string[] lines)
    {
        return lines.Select(GetTransaction).ToArray();
    }

    public Transaction GetTransaction(string line)
    {
        throw new NotImplementedException();
    }
}