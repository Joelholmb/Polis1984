namespace Polis_1984;

class Program
{
    static void Main(string[] args)
    {
        Utryckning.nyUtryckning();
    }
}

class Utryckning
{

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
        Console.WriteLine($"En utryckning av typen {typ} skedde på platsen {plats} vid tid {tidpunkt}. Poliser närvarande var:");
        for(int i = 0; i < antal; i++)
        {
            Console.WriteLine(poliser[i].namn + " tjänstenummer: " + poliser[i].tjanstenummer);
        }
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
