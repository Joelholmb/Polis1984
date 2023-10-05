using System.Text.Json;


namespace Polis_1984;

    public class Polis
    {
        public string Namn { get; set;}
        public int Tjanstenummer { get; set;}

        public Polis(string namn, int tjanstenummer)
        {
            Namn = namn;
            Tjanstenummer = tjanstenummer;
        }
    }
    class PolisLista
    {
        public static List<Polis> listaAvPoliser = new List<Polis>();

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
            public static PolisLista polisLista = new PolisLista();

            static void Main(string[] args)
            {
                List<Utryckning> listUtr = new List<Utryckning>();
                Utryckning.nyUtryckning();
            }
            
        public static Polis ValjPolis(List<Polis> valdaPoliser)
        {
            Console.WriteLine("Välj en polis från listan:");
            
            List<Polis> listaAvPoliser = polisLista.HämtaPoliser();
            for (int i = 0; i < listaAvPoliser.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {listaAvPoliser[i].Namn} (Tjänstenummer: {listaAvPoliser[i].Tjanstenummer})");
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
                            listaAvPoliser.RemoveAt(val - 1);
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
    public string Typ {get; set;}
    public string Plats {get; set;}
    public string Tidpunkt {get; set;}
    public string Poliser {get; set;}

    

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

        var nyUtryckning = new Utryckning();
        {
            nyUtryckning.Typ = typ;
            nyUtryckning.Plats = plats;
            nyUtryckning.Tidpunkt = tidpunkt;
            for(int i = 0; i < valdaPoliser.Count; i++)
            {
                nyUtryckning.Poliser += $"{valdaPoliser[i].Namn} {valdaPoliser[i].Tjanstenummer}";
            }
        }
        listUtr.Add(nyUtryckning);
        jsonString = JsonSerializer.Serialize(listUtr);
        File.WriteAllText(fileName, jsonString);
        Rapport.nyRapport(nyUtryckning);
    }
}

class Rapport
{
    public int Rapportnummer { get; set; }
    public string Utryckningsdatum { get; set; }
    public string Polisstation { get; set; }
    public string Beskrivning { get; set; }
    public static void nyRapport(Utryckning nyUtryckning)
    {
        string fileName = "Rapport.json";
        string jsonString = File.ReadAllText(fileName);
        List<Rapport> listRap = JsonSerializer.Deserialize<List<Rapport>>(jsonString)!;
        var nyRapport = new Rapport();
        {
            nyRapport.Rapportnummer = listRap.Count + 1;
            nyRapport.Utryckningsdatum = "2023/10/04";
            nyRapport.Polisstation = "Centrala polisstationen";
            nyRapport.Beskrivning = $"En utryckning av typen {nyUtryckning.Typ} skedde på platsen {nyUtryckning.Plats} vid tid {nyUtryckning.Tidpunkt}. Poliser närvarande var: {nyUtryckning.Poliser}";
        }
        Console.WriteLine($"\nRapportnr: {nyRapport.Rapportnummer}.\nDatum: {nyRapport.Utryckningsdatum}\nAnsvarig station: {nyRapport.Polisstation}\nBeskrivning: {nyRapport.Beskrivning}");
        listRap.Add(nyRapport);
        jsonString = JsonSerializer.Serialize(listRap);
        File.WriteAllText(fileName, jsonString);
    }

}

