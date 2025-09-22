// Class : Kort
/*
    Indeholder logik til at finde kulør på de forskellige kort
*/

// Enums til Kulør
enum Kulør { Klør, Spar, Hjerter, Ruder }
class Kort
{
    public Kulør Kulør { get; }
    public string Rank { get; }
    public int Værdi { get; }

    public Kort(Kulør kulør, string rank, int værdi)
    {
        Kulør = kulør;
        Rank = rank;
        Værdi = værdi;
    }

    public override string ToString()
    {
        string symbol = Kulør switch
        {
            Kulør.Spar => "♠",
            Kulør.Klør => "♣",
            Kulør.Hjerter => "♥",
            Kulør.Ruder => "♦",
            _ => "?"
        };
        return $"{Rank}{symbol}";
    }
}