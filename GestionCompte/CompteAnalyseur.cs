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
        throw new NotImplementedException();
    }
}