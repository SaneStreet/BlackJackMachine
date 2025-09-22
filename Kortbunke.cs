// Class : Kortbunke
/* 
    Indhold:
        Liste med kort fra Kort klassen (List<Kort>)
        BlandKort funktion der tager kortbunken og blander med det næste tilfældige kort (rng.Next)
        TrækKort funktion der trækker det næste kort fra bunken
*/

class Kortbunke
{
    List<Kort> kortbunke = new List<Kort>();
    private Random rng = new Random();

    public Kortbunke()
    {
        string[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
        int[] værdier = { 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10, 1 };

        foreach (Kulør k in Enum.GetValues(typeof(Kulør)))
        {
            for (int i = 0; i < ranks.Length; i++)
            {
                kortbunke.Add(new Kort(k, ranks[i], værdier[i]));
            }
        }
        BlandKort();
    }

    public void BlandKort()
    {
        for (int i = kortbunke.Count - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);
            (kortbunke[i], kortbunke[j]) = (kortbunke[j], kortbunke[i]);
        }
    }

    public Kort TrækKort()
    {
        if (kortbunke.Count == 0) throw new InvalidOperationException("Kortbunken er tom.");
        var k = kortbunke[0];
        kortbunke.RemoveAt(0);
        return k;
    }
}