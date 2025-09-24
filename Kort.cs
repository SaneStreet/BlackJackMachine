// Class : Kort
/*
    Indeholder logik til at finde kulør på de forskellige kort
*/

// Enums til Kulør varianter
enum Kulør { Klør, Spar, Hjerter, Ruder }
class Kort
{
    public Kulør Kulør { get; }
    public string Rank { get; }
    public int Værdi { get; }

    // Instantiere Kulør med rang og værdi
    public Kort(Kulør kulør, string rank, int værdi)
    {
        Kulør = kulør;
        Rank = rank;
        Værdi = værdi;
    }

    public override string ToString()
    {
        // Lægger kulør symbol til string
        string symbol = Kulør switch
        {
            Kulør.Spar => "♠",
            Kulør.Klør => "♣",
            Kulør.Hjerter => "♥",
            Kulør.Ruder => "♦",
            _ => "?"
        };
        // returnere f.eks. "5♠", "Q♥", etc
        return $"{Rank}{symbol}";
    }
}