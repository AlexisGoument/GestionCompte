namespace GestionCompte.models;

public record Transaction
{
    public DateOnly Date;
    public double Montant;
    public string Devise;
    public string Categorie;

    public Transaction(DateOnly date, double montant, string devise, string categorie)
    {
        Date = date;
        Montant = montant;
        Devise = devise;
        Categorie = categorie;
    }
}