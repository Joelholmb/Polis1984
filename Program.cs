using System.Text.Json;

namespace Polis_1984;

class Program
{
    static void Main(string[] args)
    {
        List<Utryckning> listUtr = new List<Utryckning>();
        Utryckning.nyUtryckning();
        

    }
}

class Utryckning
{
    public int Rapportnummer { get; set; }
    public string Utryckningsdatum { get; set; }
    public string Polisstation { get; set; }
    public string typ {get; set;}
    public string plats {get; set;}
    public string tidpunkt {get; set;}
    public string rapport { get; set; }
    public new List<Polis> poliser = new List<Polis>();

    public static void nyUtryckning()
    {
        List<Polis> poliser = new List<Polis>();
        Console.Write("Vilken typ av utryckning är det?: ");
        string typ = Console.ReadLine()!;
        Console.Write("Vilken plats har utryckningen skett på?: ");
        string plats = Console.ReadLine()!;
        Console.Write("Ange tid för utryckningen (HH:mm): ");
        string tidpunkt = Console.ReadLine()!;
        Console.Write("Hur många poliser var närvarande vid utryckningen?: ");
        int antal = Convert.ToInt32(Console.ReadLine());
        for(int i = 1; i <= antal; i++)
        {
            Console.Write($"Ange namn för polis {i}: ");
            string namn = Console.ReadLine()!;
            Console.Write($"Ange tjänstenummer för polis {i}: ");
            int tjanstenummer = Convert.ToInt32(Console.ReadLine());
            poliser.Add(new Polis(namn, tjanstenummer));
            
        }
        
        Console.Clear();
        
        Console.WriteLine($"En utryckning av typen {typ} skedde på platsen {plats} vid tid {tidpunkt}. Poliser närvarande var:");
        for(int i = 0; i < antal; i++)
        {
            Console.WriteLine(poliser[i].namn + " tjänstenumret: " + poliser[i].tjanstenummer);
        }
            Console.WriteLine("\nDags att skriva en rapport");
            Console.Write("Ange rapportnummer: ");
            int rapportnummer = Convert.ToInt32(Console.ReadLine());

            Console.Write("Ange datum för utryckningen (yyyy-MM-dd): ");
            string utryckningsdatum =Console.ReadLine();


            Console.Write("Ange namnet på polisstationen som hanterar ärendet: ");
            string polisstation = Console.ReadLine();

            Console.Write("Beskriv vad som skett under utryckningen: ");
            string rapport = Console.ReadLine()!;
        

            Console.WriteLine($"\nRapportnr: {rapportnummer},\n{utryckningsdatum},\nAnsvarig station: {polisstation},\nBeskrivning: {rapport}");

        var nyUtryckning = new Utryckning();
        {
        nyUtryckning.Rapportnummer = rapportnummer;
        nyUtryckning.Utryckningsdatum = utryckningsdatum;
        nyUtryckning.typ = typ;
        nyUtryckning.plats = plats;
        nyUtryckning.tidpunkt = tidpunkt;
        nyUtryckning.poliser = poliser;
        nyUtryckning.rapport = rapport;
        }
        string fileName = "Utryckning.json";
        string jsonString = JsonSerializer.Serialize(nyUtryckning);
        File.WriteAllText(fileName, jsonString);
    }
}

class Polis
{
    public string namn;

    public int tjanstenummer;

    public Polis(string namn, int tjanstenummer)
    {
        this.namn = namn;
        this.tjanstenummer = tjanstenummer;
    }
}
