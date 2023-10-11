using System.Text.Json;

namespace Polis_1984;
public class Rapport
{
    public int Rapportnummer {get; set;}
    public string? Utryckningsdatum {get; set;}
    public string? Polisstation {get; set;}
    public string? Beskrivning {get; set;}
    public static void NyRapport(string utryckningsdatum, string polisstation, Utryckning nyUtryckning)
    {
        string fileName = "Rapport.json";
        string jsonString = File.ReadAllText(fileName);
        List<Rapport> listRap = JsonSerializer.Deserialize<List<Rapport>>(jsonString)!;
        var nyRapport = new Rapport();
        {
            nyRapport.Rapportnummer = listRap.Count + 1;
            nyRapport.Utryckningsdatum = utryckningsdatum;
            nyRapport.Polisstation = polisstation;
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