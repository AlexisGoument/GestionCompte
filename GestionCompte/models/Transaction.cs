namespace GestionCompte.models;

public record Transaction
{
    public DateOnly Date;
    public double Montant;
    public string Devise;
    public string Categorie;
}