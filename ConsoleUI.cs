namespace Polis_1984;

public class ConsoleUI
{
    public static void UIMenu()
    {
        Console.OutputEncoding = Console.InputEncoding = System.Text.Encoding.Unicode;
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
                            PolisValjare.ValjPolis(valdaPoliser);
                            Console.Clear();
                        }
                        Utryckning.NyUtryckning(typ, plats, tidpunkt, valdaPoliser, out Utryckning nyUtryckning);
                        Console.WriteLine("======Detaljer för rapport======");
                        Console.Write("Datum för rapportskrivning (dd/mm/yy): ");
                        string utryckningsdatum = Console.ReadLine()!;
                        Console.Write("Ansvarande polisstation: ");
                        string polisstation = Console.ReadLine()!;
                        Rapport.NyRapport(utryckningsdatum, polisstation, nyUtryckning);
                    break;
                    case 2:
                        PolisLista.NyPersonal();
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