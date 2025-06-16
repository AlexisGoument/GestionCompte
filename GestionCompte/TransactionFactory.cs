using GestionCompte.models;

namespace GestionCompte;

public interface ITransactionFactory
{
    Transaction GetTransaction(string line);
}

public class TransactionFactory : ITransactionFactory
{
    public Transaction GetTransaction(string line)
    {
        throw new NotImplementedException();
    }
}