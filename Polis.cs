
namespace Polis_1984
{
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
}
