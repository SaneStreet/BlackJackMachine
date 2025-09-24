// Class : Spil

/*
    Indeholder spillets logik og flow

*/

#nullable enable
class Spil
{
    private Kortbunke kortbunke = new Kortbunke();
    private Spiller dealer = new Spiller("Dealer");
    private Spiller? spiller;
    string titel = "🎴 BlackJackMachine 1.0 🃏\n"; 

    public void Run()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine(titel);
            Console.WriteLine("Vælg en mulighed: (1-3)");
            Console.WriteLine(" 1. Nyt spil");
            Console.WriteLine(" 2. Se regler");
            Console.WriteLine(" 3. Afslut\n");
            int valg = Convert.ToInt32(Console.ReadLine());

            switch (valg)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine(titel);
                    Console.WriteLine("Navn på spiller?: \n");
                    string? nySpiller = Console.ReadLine();
                    if (nySpiller == "" || nySpiller == null)
                    {
                        nySpiller = "Spiller";
                    }

                    spiller = new Spiller(nySpiller);

                    Console.Clear();
                    bool iSpil = true;

                    while (iSpil)
                    {
                        SpilRunde();

                        Console.WriteLine("\nVil du spille igen? (J/N): ");
                        string nytspil = Console.ReadLine()?.Trim().ToLower() ?? "n";
                        iSpil = nytspil == "j";
                    }
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine(titel);
                    Console.WriteLine("Standardregler for Blackjack: ");
                    Console.WriteLine("- Målet er at få en håndværdi tættere på 21 end dealeren, uden at overskride 21.");
                    Console.WriteLine("- Kort 2-10 har deres pålydende værdi, billedkort (J, Q, K) giver 10 point, og et es (A) kan være 1 eller 11, afhængigt af hvad der er bedst for hånden.");
                    Console.WriteLine("- Hvis spillereneller dealeren overstiger 21 (bust), taber de øjeblikkeligt.");
                    Console.WriteLine("- Når spilleren vælger stand, trækker dealeren kort, indtil hånden har en værdi på mindst 17.");
                    Console.WriteLine("- Hvis ingen går bust, vinder den med den højeste værdi. Uafgjort resulterer i en push (ingen vinder)");
                    Console.WriteLine("\nTryk på en tast for at fortsætte..");
                    Console.ReadKey();
                    break;
                case 3:
                    Console.Clear();
                    Console.WriteLine("Tak for nu. Byebye. 👋");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Ugyldigt valg. Tryk på en tast for at fortsætte..");
                    Console.ReadLine();
                    break;
            }

        }
    }

    public void SpilRunde()
    {
        if (spiller != null)
        {
            spiller.TømHånd();
        }
        dealer.TømHånd();
        kortbunke = new Kortbunke();

        // start deal
        spiller.Hånd.Add(kortbunke.TrækKort());
        dealer.Hånd.Add(kortbunke.TrækKort());
        spiller.Hånd.Add(kortbunke.TrækKort());
        dealer.Hånd.Add(kortbunke.TrækKort());

        Console.WriteLine($"\n  Dealers hånd: {dealer.Hånd.kortPåHånden[0]} 🂠");
        Console.WriteLine($"  {spiller.Navn}'s hånd: {spiller.Hånd} ({spiller.Hånd.VinderVærdi()})");

        // check for blackjack
        if (spiller.Hånd.ErBlackjack() || dealer.Hånd.ErBlackjack())
        {
            DealerHånd();
            if (spiller.Hånd.ErBlackjack() && !dealer.Hånd.ErBlackjack())
            {
                Console.WriteLine("Blackjack! Du vinder! 👏");
            }
            else if (!spiller.Hånd.ErBlackjack() && dealer.Hånd.ErBlackjack())
            {
                Console.WriteLine("Dealer har Blackjack! Du taber! 😞");
            }
            else
            {
                Console.WriteLine("Begge har Blackjack! Uafgjort! 🤝");
            }
            return;
        }

        bool spillerHandling = false;
        while (!spillerHandling)
        {
            Console.WriteLine("Handling? (hit/stand)");
            string handling = Console.ReadLine()?.Trim().ToLower() ?? "stand";

            if (handling == "hit")
            {
                spiller.Hånd.Add(kortbunke.TrækKort());
                Console.WriteLine($"\n  {spiller.Navn}'s hånd: {spiller.Hånd} ({spiller.Hånd.VinderVærdi()})");
                if (spiller.Hånd.ErBust())
                {
                    Console.WriteLine("Bust! Du tabte 🙁");
                    return;
                }
            }
            else
            {
                spillerHandling = true;
            }
        }
        DealerHånd();

        while (dealer.Hånd.VinderVærdi() < 17)
        {
            var k = kortbunke.TrækKort();
            dealer.Hånd.Add(k);
            Console.WriteLine($"   Dealer trak: {k} ({dealer.Hånd.VinderVærdi()})");
        }

        int spillerVærdi = spiller.Hånd.VinderVærdi();
        int dealerVærdi = dealer.Hånd.VinderVærdi();
        Console.WriteLine($"\nResultat: {spiller.Navn} {spillerVærdi} vs Dealer {dealerVærdi}");

        if (dealer.Hånd.ErBust() || spillerVærdi > dealerVærdi)
        {
            Console.WriteLine("Tillykke du vandt! 👏");
        }
        else if (spillerVærdi == dealerVærdi)
        {
            Console.WriteLine("Uafgjort.");
        }
        else
        {
            Console.WriteLine("Dealer vandt ");
        }
    }

    public void DealerHånd()
    {
        Console.WriteLine($"\n  Dealers hånd: {dealer.Hånd} ({dealer.Hånd.VinderVærdi()})");
    }
}