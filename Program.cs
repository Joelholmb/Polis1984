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
        Console.Write("Vilken tid skedde utryckningen?: ");
        string tidpunkt = Console.ReadLine()!;
        Console.Write("Hur många poliser var vid utryckningen?: ");
        int antal = Convert.ToInt32(Console.ReadLine());
        for(int i = 1; i <= antal; i++)
        {
            Console.Write($"Ange namn för polis {i}: ");
            string namn = Console.ReadLine()!;
            Console.Write($"Ange tjänstenummer för polis {i}: ");
            int tjanstenummer = Convert.ToInt32(Console.ReadLine());
            poliser.Add(new Polis(namn, tjanstenummer));
            
        }
        Console.Write("Skriv en rapport om utryckningen: ");
        string rapport = Console.ReadLine()!;
        Console.Clear();

        Console.WriteLine($"En utryckning av typen {typ} skedde på platsen {plats} vid tid {tidpunkt}. Poliser närvarande var:");
        for(int i = 0; i < antal; i++)
        {
            Console.WriteLine(poliser[i].namn + " tjänstenumret: " + poliser[i].tjanstenummer);
        }

            Console.WriteLine($"\nBeskrivning: {rapport}");

        var nyUtryckning = new Utryckning();
        {
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
