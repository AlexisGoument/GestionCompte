using GestionCompte;

namespace GestionCompteTests;

public class TransactionFactoryTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void GetTransaction_doit_lire_les_infos()
    {
        var parseur = new TransactionFactory();

        var transaction = parseur.GetTransaction("01/01/2022\t5401.38\tUSD\tPrimes");
        
        Assert.That(transaction.Date, Is.EqualTo(new DateOnly(2022, 01, 01)));
        Assert.That(transaction.Montant, Is.EqualTo(5401.38));
        Assert.That(transaction.Devise, Is.EqualTo("USD"));
        Assert.That(transaction.Categorie, Is.EqualTo("Primes"));
    }
}