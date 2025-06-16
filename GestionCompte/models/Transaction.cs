namespace GestionCompte.models;

public record Transaction
{
    public DateOnly Date;
    public double Montant;
    public string Devise;
    public string Categorie;

    public Transaction(DateOnly Date, double Montant, string Devise, string Categorie)
    {
        this.Date = Date;
        this.Montant = Montant;
        this.Devise = Devise;
        this.Categorie = Categorie;
    }
}