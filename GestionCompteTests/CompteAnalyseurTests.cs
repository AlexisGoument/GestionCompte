using GestionCompte;
using GestionCompte.models;
using NSubstitute;

namespace GestionCompteTests;

public class CompteAnalyseurTests
{
    ICsvReader _reader;
    
    [SetUp]
    public void Setup()
    {
        _reader = Substitute.For<ICsvReader>();
    }

    [Test]
    public void Analyseur_doit_calculer_la_balance_sur_1_jour()
    {
        const string csvPath = "../../../../ressources/account_20230228.csv";
        const double expectedBalance = 15376.45;
        var factory = new TransactionFactory();
        var analyseur = new CompteAnalyseur(_reader, factory, csvPath);

        var result = analyseur.GetValeurADate(new DateOnly(2022, 01, 01));

        Assert.That(result.Balance, Is.EqualTo(expectedBalance));
    }

    [Test]
    public void Analyseur_retourne_rapport_pour_0_transaction()
    {
        var factory = Substitute.For<ITransactionFactory>();
        var analyseur = new CompteAnalyseur(_reader, factory, string.Empty);
        
        var result = analyseur.GetValeurADate(new DateOnly(2022, 01, 01));
        
        Assert.That(result.Balance, Is.EqualTo(0));
    }

    [Test]
    public void Analyseur_retourne_rapport_pour_1_transaction()
    {
        var factory = Substitute.For<ITransactionFactory>();
        var transactions = new[]
        {
            new Transaction(new DateOnly(2022, 01, 01), 5401.38, "EUR", "Primes")
        };
        factory.GetTransactions(Arg.Any<string[]>()).Returns(transactions);
        var analyseur = new CompteAnalyseur(_reader, factory, string.Empty);
        
        var result = analyseur.GetValeurADate(new DateOnly(2022, 01, 01));
        
        Assert.That(result.Balance, Is.EqualTo(5401.38));
    }

    [Test]
    public void Analyseur_retourne_rapport_pour_2_transactions()
    {
        var factory = Substitute.For<ITransactionFactory>();
        var transactions = new[]
        {
            new Transaction(new DateOnly(2022, 01, 01), 5401.38, "EUR", "Primes"),
            new Transaction(new DateOnly(2022, 01, 01), 1000, "EUR", "Hotel")

        };
        factory.GetTransactions(Arg.Any<string[]>()).Returns(transactions);
        var analyseur = new CompteAnalyseur(_reader, factory, string.Empty);
        
        var result = analyseur.GetValeurADate(new DateOnly(2022, 01, 01));
        
        Assert.That(result.Balance, Is.EqualTo(6401.38));
    }

    [Test]
    public void Analyseur_retourne_rapport_pour_2_jours()
    {
        var factory = Substitute.For<ITransactionFactory>();
        var transactions = new[]
        {
            new Transaction(new DateOnly(2022, 01, 01), 5401.38, "EUR", "Primes"),
            new Transaction(new DateOnly(2022, 01, 02), -1000, "EUR", "Hotel")

        };
        factory.GetTransactions(Arg.Any<string[]>()).Returns(transactions);
        var analyseur = new CompteAnalyseur(_reader, factory, string.Empty);
        
        var result = analyseur.GetValeurADate(new DateOnly(2022, 01, 02));
        
        Assert.That(result.Balance, Is.EqualTo(4401.38));
    }

    [Test]
    public void Analyseur_ne_prend_pas_en_compte_les_jours_en_dehors_de_la_période_calculee()
    {
        var factory = Substitute.For<ITransactionFactory>();
        var transactions = new[]
        {
            new Transaction(new DateOnly(2022, 01, 01), 5401.38, "EUR", "Primes"),
            new Transaction(new DateOnly(2022, 01, 02), -1000, "EUR", "Hotel")

        };
        factory.GetTransactions(Arg.Any<string[]>()).Returns(transactions);
        var analyseur = new CompteAnalyseur(_reader, factory, string.Empty);
        
        var result = analyseur.GetValeurADate(new DateOnly(2022, 01, 01));
        
        Assert.That(result.Balance, Is.EqualTo(5401.38));
    }
}