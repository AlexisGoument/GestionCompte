namespace GestionCompte.models;

public record CompteRapport(DateOnly DateFinIncluse)
{
    public DateOnly DateFinIncluse = DateFinIncluse;
    public double Balance = 0;
}