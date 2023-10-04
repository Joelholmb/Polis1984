using System.Text.Json;

namespace Polis_1984;

    class PolisLista
    {
        public List<Polis> listaAvPoliser = new List<Polis>();

        public PolisLista()
        {
            
            listaAvPoliser.Add(new Polis("Kalle", 4334));
            listaAvPoliser.Add(new Polis("Sudden", 7754));
            listaAvPoliser.Add(new Polis("Majoren", 1239));
        }

        public List<Polis> HämtaPoliser()
        {
            return listaAvPoliser;
        }

        
    }

    class Program
    {
            static PolisLista polisLista = new PolisLista();

            static void Main(string[] args)
            {
                List<Utryckning> listUtr = new List<Utryckning>();
                Utryckning.nyUtryckning();
            }
            
        static public Polis ValjPolis(List<Polis> valdaPoliser)
        {
            Console.WriteLine("Välj en polis från listan:");
            
            List<Polis> listaAvPoliser = polisLista.HämtaPoliser();
            for (int i = 0; i < listaAvPoliser.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {listaAvPoliser[i].namn}");
            }

                int val;
                while (true)
                break;

                {
                    if (int.TryParse(Console.ReadLine(), out val) && val >= 1 && val <= listaAvPoliser.Count)
                    {
                        Polis valdPolis = listaAvPoliser[val - 1];
                        if (!valdaPoliser.Contains(valdPolis))
                        {
                            valdaPoliser.Add(valdPolis);
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
                return null;
        }
    
    }

class Utryckning
{
    public int Rapportnummer { get; set; }
    public string Utryckningsdatum { get; set; }
    public string Polisstation { get; set; }
    public string Typ {get; set;}
    public string Plats {get; set;}
    public string Tidpunkt {get; set;}
    public string Rapport { get; set; }
    public string poliser {get; set;}

    public static void nyUtryckning()
    {
        string fileName = "Utryckning.json";
        string jsonString = File.ReadAllText(fileName);
        List<Utryckning> listUtr = JsonSerializer.Deserialize<List<Utryckning>>(jsonString)!;
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
        nyUtryckning.Typ = typ;
        nyUtryckning.Plats = plats;
        nyUtryckning.Tidpunkt = tidpunkt;
        for(int i = 0; i < valdaPoliser.Count; i++)
        {
        nyUtryckning.poliser += $"|{valdaPoliser[i].namn} {valdaPoliser[i].tjanstenummer}|";
        }
        nyUtryckning.Rapport = rapport;
        }
        listUtr.Add(nyUtryckning);
        jsonString = JsonSerializer.Serialize(listUtr);
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
