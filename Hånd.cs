// Class : Hånd

/*
    Indeholder logik omkring hvad der er i en hånd
    Værdiberegninger
    Bust/Blackjack
*/

class Hånd()
{
    //Personlig liste af kort på hånden
    public List<Kort> kortPåHånden = new List<Kort>();
    //Lægger kort på hånden
    public void Add(Kort k) => kortPåHånden.Add(k);

    // Udregn bedste værdi for spiller. Anse altid Es som værende 1, medmindre 11 passer bedre
    public int VinderVærdi()
    {
        //Nuværende sum på hånden og antallet af Esser ("A")
        int kortSum = kortPåHånden.Sum(k => k.Værdi);
        int antalEsser = kortPåHånden.Count(k => k.Rank == "A");

        // Laver Esser om til 11, hvis det er bedre værdi
        while (antalEsser > 0 && kortSum + 10 <= 21)
        {
            kortSum += 10;
            antalEsser--;
        }
        return kortSum;
    }

    public bool ErBust() => VinderVærdi() > 21;
    public bool ErBlackjack() => kortPåHånden.Count == 2 && VinderVærdi() == 21;

    public override string ToString()
    {
        return string.Join(" ", kortPåHånden);
    }
}