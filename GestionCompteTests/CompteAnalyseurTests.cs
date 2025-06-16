using GestionCompte;
using NSubstitute;

namespace GestionCompteTests;

public class CompteAnalyseurTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Analyseur_doit_calculer_la_balance_sur_1_jour()
    {
        const string csvPath = "../ressources/account_20230228.csv";
        const double expectedBalance = 15376.45;
        var reader = new CsvReader();
        var factory = new TransactionFactory();
        var analyseur = new CompteAnalyseur(reader, factory, csvPath);

        var result = analyseur.GetValeurADate(new DateOnly(2022, 01, 01));

        Assert.That(result.Balance, Is.EqualTo(expectedBalance));
    }

    [Test]
    public void Analyseur_retourne_0_quand_fichier_vide()
    {
        var reader = Substitute.For<ICsvReader>();
        var factory = Substitute.For<ITransactionFactory>();
        var analyseur = new CompteAnalyseur(reader, factory, string.Empty);
        
        var result = analyseur.GetValeurADate(new DateOnly(2022, 01, 01));
        
        Assert.That(result.Balance, Is.EqualTo(0));
    }
}