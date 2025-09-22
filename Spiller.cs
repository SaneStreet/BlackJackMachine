// Class : Spiller

/*
    Opretter spillere 
    Tømrer kort på hånden ved hver runde
*/

class Spiller
{
    // Henter navnet på spiller
    public string Navn { get; }
    // Laver en ny hånd til spilleren
    public Hånd Hånd { get; } = new Hånd();

    public Spiller(string navn)
    {
        Navn = navn;
    }

    // Funktion der tømmer hånden på spilleren
    public void TømHånd() => Hånd.kortPåHånden.Clear();
}