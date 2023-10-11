
using System.Text.Json;

namespace Polis_1984;
public class Utryckning
{
    public string? Typ {get; set;}
    public string? Plats {get; set;}
    public string? Tidpunkt {get; set;}
    public string? Poliser {get; set;}
    public static void NyUtryckning(string typ, string plats, string tidpunkt, List<Polis> valdaPoliser, out Utryckning nyUtryckning)
    {
        string fileName = "Utryckning.json";
        string jsonString = File.ReadAllText(fileName);
        List<Utryckning> listUtr = JsonSerializer.Deserialize<List<Utryckning>>(jsonString)!;
        Console.Clear();

        nyUtryckning = new Utryckning();
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