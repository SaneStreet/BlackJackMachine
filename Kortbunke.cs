// Class : Kortbunke
/* 
    Indhold:
        Liste med kort fra Kort klassen (List<Kort>)
        BlandKort funktion der tager kortbunken og blander med det næste tilfældige kort (rng.Next)
        TrækKort funktion der trækker det næste kort fra bunken
*/

class Kortbunke
{
    // Laver kortbunken
    List<Kort> kortbunke = new List<Kort>();
    // Random generator
    private Random rng = new Random();

    public Kortbunke()
    {
        // Lister med rang og værdier til kortene
        string[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
        int[] værdier = { 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10, 1 };

        // Tilføjer kulør til de forskellige kort i bunken
        foreach (Kulør k in Enum.GetValues(typeof(Kulør)))
        {
            for (int i = 0; i < ranks.Length; i++)
            {
                kortbunke.Add(new Kort(k, ranks[i], værdier[i]));
            }
        }

        BlandKort();
    }

    // Funktion der blander kort værdierne tilfældigt
    public void BlandKort()
    {
        // Tager kortene én efter én og blander sammen med tilfældige andre kort
        for (int i = kortbunke.Count - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);
            (kortbunke[i], kortbunke[j]) = (kortbunke[j], kortbunke[i]);
        }
    }

    // Funktion der trækker kort
    public Kort TrækKort()
    {
        // Lille exception handling
        if (kortbunke.Count == 0) throw new InvalidOperationException("Kortbunken er tom.");
        var k = kortbunke[0];
        // Fjerner kortet fra bunken, så det ikke trækkes igen
        kortbunke.RemoveAt(0);
        return k;
    }
}