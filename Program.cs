using System.IO.Enumeration;
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
        public static void PersonalLista()
        {
            PolisLista polisLista = new PolisLista();
            List<Polis> listaAvPoliser = polisLista.HämtaPoliser();
            for(int i = 0; i < listaAvPoliser.Count; i++)
            {
                Console.WriteLine($"{listaAvPoliser[i].Namn}. Tjänstenummer: {listaAvPoliser[i].Tjanstenummer}");
            }
        }
        
    }

    class Program
    {
        public static PolisLista polisLista = new PolisLista();

        public static Polis ValjPolis(List<Polis> valdaPoliser)
        {
            Console.WriteLine("Välj en polis från listan:");

            List<Polis> listaAvPoliser = polisLista.HämtaPoliser();
            for (int i = 0; i < listaAvPoliser.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {listaAvPoliser[i].Namn} (Tjänstenummer: {listaAvPoliser[i].Tjanstenummer}){Environment.NewLine}");
            }

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int val) && val >= 1 && val <= listaAvPoliser.Count)
                {
                    Polis valdPolis = listaAvPoliser[val - 1];
                    if (!valdaPoliser.Contains(valdPolis))
                    {
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

        static void Main(string[] args)
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine("=====Välj alternativ=====");
                Console.WriteLine("[1] Registrera ny utryckning");
                Console.WriteLine("[2] Registrera ny personal");
                Console.WriteLine("[3] Visa lista av tidigare utryckningar");
                Console.WriteLine("[4] Visa lista av rapporter");
                Console.WriteLine("[5] Visa personallista");
                Console.WriteLine("[a] Avsluta");
                Console.Write("Val: ");
                string strPick = Console.ReadLine()!;
                if (int.TryParse(strPick, out int intPick) && intPick > 0 && intPick < 6)
                {
                    switch(intPick)
                    {
                        case 1:
                            Utryckning.NyUtryckning();
                        break;
                        case 2:
                        break;
                        case 3:
                            Utryckning.UtryckningsLista();
                        break;
                        case 4:
                            Rapport.RapportLista();
                        break;
                        case 5:
                            PolisLista.PersonalLista();
                        break;
                        default:
                        break;
                    }
                }
                else if (strPick.ToLower() == "a")
                {
                    isRunning = false;
                    Console.WriteLine("Avslutar...");
                }
                else
                {
                    Console.WriteLine("Ogiltigt val");
                }
            }
    
        }
    }

class Utryckning
{
    public string? Typ {get; set;}
    public string? Plats {get; set;}
    public string? Tidpunkt {get; set;}
    public string? Poliser {get; set;}

    

    public static void NyUtryckning()
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
                if (i == 0)
                {
                    nyUtryckning.Poliser += $"{valdaPoliser[i].Namn} {valdaPoliser[i].Tjanstenummer}";
                }
                else
                {
                    nyUtryckning.Poliser += $", {valdaPoliser[i].Namn} {valdaPoliser[i].Tjanstenummer}";
                }
            }
        }
        listUtr.Add(nyUtryckning);
        jsonString = JsonSerializer.Serialize(listUtr);
        File.WriteAllText(fileName, jsonString);
        Rapport.NyRapport(nyUtryckning);
    }
    public static void UtryckningsLista()
    {
        string fileName = "Utryckning.json";
        string jsonString = File.ReadAllText(fileName);
        List<Utryckning> listUtr = JsonSerializer.Deserialize<List<Utryckning>>(jsonString)!;
        for(int i = 0; i < listUtr.Count; i++)
        {
            Console.WriteLine($"{i+1}.\nTyp: {listUtr[i].Typ}\nPlats: {listUtr[i].Plats}\nTidpunkt: {listUtr[i].Tidpunkt}\nNärvarande poliser: {listUtr[i].Poliser}");
        }
    }
}

class Rapport
{
    public int Rapportnummer { get; set; }
    public string? Utryckningsdatum { get; set; }
    public string? Polisstation { get; set; }
    public string? Beskrivning { get; set; }
    public static void NyRapport(Utryckning nyUtryckning)
    {
        string fileName = "Rapport.json";
        string jsonString = File.ReadAllText(fileName);
        List<Rapport> listRap = JsonSerializer.Deserialize<List<Rapport>>(jsonString)!;
        var nyRapport = new Rapport();
        {
            nyRapport.Rapportnummer = listRap.Count + 1;
            nyRapport.Utryckningsdatum = "2023/10/04";
            nyRapport.Polisstation = "Centrala polisstationen";
            nyRapport.Beskrivning = $"En utryckning av typen {nyUtryckning.Typ} skedde på platsen {nyUtryckning.Plats} vid tid {nyUtryckning.Tidpunkt}. \nPoliser närvarande var: {nyUtryckning.Poliser}";
        }
        Console.WriteLine($"\nRapportnr: {nyRapport.Rapportnummer}.\nDatum: {nyRapport.Utryckningsdatum}\nAnsvarig station: {nyRapport.Polisstation}\nBeskrivning: {nyRapport.Beskrivning}");
        listRap.Add(nyRapport);
        jsonString = JsonSerializer.Serialize(listRap);
        File.WriteAllText(fileName, jsonString);
    }
    public static void RapportLista()
    {
        string fileName = "Rapport.json";
        string jsonString = File.ReadAllText(fileName);
        List<Rapport> listRap = JsonSerializer.Deserialize<List<Rapport>>(jsonString)!;
        for(int i = 0; i < listRap.Count; i++)
        {
            Console.WriteLine($"Rapportnr: {listRap[i].Rapportnummer}.\nDatum: {listRap[i].Utryckningsdatum}\nAnsvarig station: {listRap[i].Polisstation}\nBeskrivning: {listRap[i].Beskrivning}");
        }
    }

}