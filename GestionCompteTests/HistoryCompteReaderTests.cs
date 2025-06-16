using GestionCompte;
using NSubstitute;

namespace GestionCompteTests;

public class HistoryCompteReaderTests
{
    [SetUp]
    public void Setup()
    {
    }
    
    private Stream GenerateStreamFromString(string s)
    {
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        writer.Write(s);
        writer.Flush();
        stream.Position = 0;
        return stream;
    }

    [Test]
    public void GetTransaction_doit_lire_les_infos()
    {
        var fileContent =
            """
            Date;Montant;Devise;Categorie
            01/01/2022;5401.38;USD;Primes
            """;
        using var stream = GenerateStreamFromString(fileContent);
        using var streamReader = new StreamReader(stream);
        var fileReader = Substitute.For<IFileReader>();
        fileReader.GetStream(Arg.Any<string>()).Returns(streamReader);
        var reader = new HistoriqueCompteReader(fileReader);
        var transactions = reader.GetTransactions("");
        
        var transaction = transactions.Single();
        Assert.That(transaction.Date, Is.EqualTo(new DateOnly(2022, 01, 01)));
        Assert.That(transaction.Montant, Is.EqualTo(5401.38));
        Assert.That(transaction.Devise, Is.EqualTo("USD"));
        Assert.That(transaction.Categorie, Is.EqualTo("Primes"));
    }
}