using System.Text.Json;

namespace Polis_1984;

class Program
{
    static void TaBortPolis(Polis polis)
{
    listaAvPoliser.Remove(polis);
}
    static List<Polis> listaAvPoliser = new List<Polis>();

    static void Main(string[] args)
    {
        listaAvPoliser.Add(new Polis("Kalle, tjänstenummer: 4371.", 1));
        listaAvPoliser.Add(new Polis("Sudden, tjänstenummer: 1344.", 2));
        listaAvPoliser.Add(new Polis("Majoren, tjänstenummer: 5776.", 3));
        
        
        List<Utryckning> listUtr = new List<Utryckning>();
        Utryckning.nyUtryckning();
        

    }
    
    static public Polis ValjPolis(List<Polis> valdaPoliser)
    {
        Console.WriteLine("Välj en polis från listan:");
        for (int i = 0; i < listaAvPoliser.Count; i++)
        {
        Console.WriteLine($"{i + 1}. {listaAvPoliser[i].namn}");
        }

            int val;
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out val) && val >= 1 && val <= listaAvPoliser.Count)
                {
                    Polis valdPolis = listaAvPoliser[val - 1];
                    if (!valdaPoliser.Contains(valdPolis))
                    {
                        valdaPoliser.Add(valdPolis);
                        TaBortPolis(valdPolis);
                        return valdPolis;
                    }
                    else
                    {
                        Console.WriteLine("Denna polis har redan valts. Välj en annan.");
                    }
                }    
                else
                {
                    Console.WriteLine("Personen finns inte i listan. Försök igen.");
                }
            }

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
        List<Polis> valdaPoliser = new List<Polis>();
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
            Polis valdPolis = Program.ValjPolis(valdaPoliser);
            valdaPoliser.Add(valdPolis);
            Console.Clear();
        }
        
        Console.Clear();
        
        Console.WriteLine($"En utryckning av typen {typ} skedde på platsen {plats} vid tid {tidpunkt}. Poliser närvarande var:");
        for(int i = 0; i < antal; i++)
        {
            Console.WriteLine($"{valdaPoliser[i].namn} {valdaPoliser[i].tjanstenummer}");
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
        nyUtryckning.poliser = valdaPoliser;
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
