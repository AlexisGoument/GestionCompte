// See https://aka.ms/new-console-template for more information

using GestionCompte;

const string csvPath = "../../../../ressources/account_20230228.csv";
var reader = new HistoriqueCompteReader(new FileReader());
var analyseur = new CompteAnalyseur(reader, csvPath);

var hasOnlyOneArg = args.Length == 1;
bool ArgIsNotDate(out DateOnly date) => !DateOnly.TryParse(args[0], out date);
if (!hasOnlyOneArg || ArgIsNotDate(out DateOnly maxDate))
{
    WriteUsage();
    return;
}

var rapport = analyseur.GetValeurADate(maxDate);
Console.WriteLine(rapport.ToString());

void WriteUsage()
{
    Console.WriteLine("Usage: GestionCompte.exe 01/01/2022");
}