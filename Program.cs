using System.IO.Enumeration;
using System.Text.Json;



namespace Polis_1984;

    
    

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
        }

        static void Main(string[] args)
        {
            ConsoleUI.UIMenu();
        }
    }