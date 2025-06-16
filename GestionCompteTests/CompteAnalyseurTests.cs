using GestionCompte;
using GestionCompte.models;
using NSubstitute;

namespace GestionCompteTests;

public class CompteAnalyseurTests
{
    private const double Tolerance = 0.005;
    
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Analyseur_doit_calculer_la_balance_sur_1_jour()
    {
        const string csvPath = "../../../../ressources/account_20230228.csv";
        const double expectedBalance = 25929.40;
        var reader = new HistoriqueCompteReader(new FileReader());
        var analyseur = new CompteAnalyseur(reader, csvPath);

        var result = analyseur.GetValeurADate(new DateOnly(2022, 01, 01));

        Assert.That(result.Balance, Is.EqualTo(expectedBalance).Within(Tolerance));
    }

    [Test]
    public void Analyseur_doit_calculer_la_balance_sur_2_jours()
    {
        const string csvPath = "../../../../ressources/account_20230228.csv";
        const double expectedBalance = 20212.72;
        var reader = new HistoriqueCompteReader(new FileReader());
        var analyseur = new CompteAnalyseur(reader, csvPath);

        var result = analyseur.GetValeurADate(new DateOnly(2022, 01, 02));

        Assert.That(result.Balance, Is.EqualTo(expectedBalance).Within(Tolerance));
    }

    [Test]
    public void Analyseur_retourne_rapport_pour_0_transaction()
    {
        var reader = Substitute.For<IHistoriqueCompteReader>();
        var analyseur = new CompteAnalyseur(reader, string.Empty);
        
        var result = analyseur.GetValeurADate(new DateOnly(2022, 01, 01));
        
        Assert.That(result.Balance, Is.EqualTo(0));
    }

    [Test]
    public void Analyseur_retourne_rapport_pour_1_transaction()
    {
        var reader = Substitute.For<IHistoriqueCompteReader>();
        var transactions = new[]
        {
            new Transaction(new DateOnly(2022, 01, 01), 5401.38, "EUR", "Primes")
        };
        reader.GetTransactions(Arg.Any<string>()).Returns(transactions);
        var analyseur = new CompteAnalyseur(reader, string.Empty);
        
        var result = analyseur.GetValeurADate(new DateOnly(2022, 01, 01));
        
        Assert.That(result.Balance, Is.EqualTo(5401.38));
    }

    [Test]
    public void Analyseur_retourne_rapport_pour_2_transactions()
    {
        var reader = Substitute.For<IHistoriqueCompteReader>();

        var transactions = new[]
        {
            new Transaction(new DateOnly(2022, 01, 01), 5401.38, "EUR", "Primes"),
            new Transaction(new DateOnly(2022, 01, 01), 1000, "EUR", "Hotel")

        };
        reader.GetTransactions(Arg.Any<string>()).Returns(transactions);
        var analyseur = new CompteAnalyseur(reader, string.Empty);
        
        var result = analyseur.GetValeurADate(new DateOnly(2022, 01, 01));
        
        Assert.That(result.Balance, Is.EqualTo(6401.38));
    }

    [Test]
    public void Analyseur_retourne_rapport_pour_2_jours()
    {
        var reader = Substitute.For<IHistoriqueCompteReader>();
        var transactions = new[]
        {
            new Transaction(new DateOnly(2022, 01, 01), 5401.38, "EUR", "Primes"),
            new Transaction(new DateOnly(2022, 01, 02), -1000, "EUR", "Hotel")

        };
        reader.GetTransactions(Arg.Any<string>()).Returns(transactions);
        var analyseur = new CompteAnalyseur(reader, string.Empty);
        
        var result = analyseur.GetValeurADate(new DateOnly(2022, 01, 02));
        
        Assert.That(result.Balance, Is.EqualTo(4401.38));
    }

    [Test]
    public void Analyseur_ne_prend_pas_en_compte_les_jours_en_dehors_de_la_période_calculee()
    {
        var reader = Substitute.For<IHistoriqueCompteReader>();
        var transactions = new[]
        {
            new Transaction(new DateOnly(2022, 01, 01), 5401.38, "EUR", "Primes"),
            new Transaction(new DateOnly(2022, 01, 02), -1000, "EUR", "Hotel")

        };
        reader.GetTransactions(Arg.Any<string>()).Returns(transactions);
        var analyseur = new CompteAnalyseur(reader, string.Empty);
        
        var result = analyseur.GetValeurADate(new DateOnly(2022, 01, 01));
        
        Assert.That(result.Balance, Is.EqualTo(5401.38));
    }

    [Test]
    public void Analyseur_converti_USD_en_EUR()
    {
        var reader = Substitute.For<IHistoriqueCompteReader>();
        var transactions = new[]
        {
            new Transaction(new DateOnly(2022, 01, 01), 5401.38, "USD", "Primes"),

        };
        reader.GetTransactions(Arg.Any<string>()).Returns(transactions);
        var analyseur = new CompteAnalyseur(reader, string.Empty);
        
        var result = analyseur.GetValeurADate(new DateOnly(2022, 01, 01));
        
        Assert.That(result.Balance, Is.EqualTo(7804.9941).Within(Tolerance));
    }

    [Test]
    public void Analyseur_converti_JPY_en_EUR()
    {
        var reader = Substitute.For<IHistoriqueCompteReader>();
        var transactions = new[]
        {
            new Transaction(new DateOnly(2022, 01, 01), -889.11, "JPY", "Loisir"),

        };
        reader.GetTransactions(Arg.Any<string>()).Returns(transactions);
        var analyseur = new CompteAnalyseur(reader, string.Empty);
        
        var result = analyseur.GetValeurADate(new DateOnly(2022, 01, 01));
        
        Assert.That(result.Balance, Is.EqualTo(-428.55102).Within(Tolerance));
    }

    [Test]
    public void Analyseur_ne_prend_pas_en_compte_les_devises_autres_que_EUR_JPY_USD()
    {
        var reader = Substitute.For<IHistoriqueCompteReader>();
        var transactions = new[]
        {
            new Transaction(new DateOnly(2022, 01, 01), -889.11, "devise inconnue", "Loisir"),

        };
        reader.GetTransactions(Arg.Any<string>()).Returns(transactions);
        var analyseur = new CompteAnalyseur(reader, string.Empty);
        
        Assert.Throws<ArgumentException>(() => analyseur.GetValeurADate(new DateOnly(2022, 01, 01)));
    }

    [Test]
    public void Analyseur_fait_la_somme_des_devises_converti_en_EUR()
    {
        var reader = Substitute.For<IHistoriqueCompteReader>();
        var transactions = new[]
        {
            new Transaction(new DateOnly(2022, 01, 01), 5401.38, "USD", "Primes"),
            new Transaction(new DateOnly(2022, 01, 01), -1000, "EUR", "Hotel"),
            new Transaction(new DateOnly(2022, 01, 01), -889.11, "JPY", "Loisir"),
        };
        reader.GetTransactions(Arg.Any<string>()).Returns(transactions);
        var analyseur = new CompteAnalyseur(reader, string.Empty);
        
        var result = analyseur.GetValeurADate(new DateOnly(2022, 01, 01));
        
        Assert.That(result.Balance, Is.EqualTo(6376.44308).Within(Tolerance));
    }
}