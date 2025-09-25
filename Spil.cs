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

    // Funktionen der kører hele programmet
    public void Run()
    {
        // Menu med valgmuligheder
        while (true)
        {
            Console.Clear();
            Console.WriteLine(titel);
            Console.WriteLine("Vælg en mulighed: (1-3)");
            Console.WriteLine(" 1. Nyt spil");
            Console.WriteLine(" 2. Se regler");
            Console.WriteLine(" 3. Afslut\n");
            int valg = Convert.ToInt32(Console.ReadLine());

            // Switch case der håndtere valg fra menuen
            switch (valg)
            {
                // Starter spillet og spilrunder
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
                        Console.Clear();
                    }
                    break;
                // Fremviser reglerne for Blackjack
                case 2:
                    Console.Clear();
                    Console.WriteLine(titel);
                    Console.WriteLine("Regler for Blackjack: ");
                    Console.WriteLine("- Målet er at få en håndværdi tættere på 21 end dealeren, uden at overskride 21.");
                    Console.WriteLine("- Kort 2-10 har deres pålydende værdi, billedkort (J, Q, K) giver 10 point, og et es (A) kan være 1 eller 11, afhængigt af hvad der er bedst for hånden.");
                    Console.WriteLine("- Hvis spillereneller dealeren overstiger 21 (bust), taber de øjeblikkeligt.");
                    Console.WriteLine("- Når spilleren vælger stand, trækker dealeren kort, indtil hånden har en værdi på mindst 17.");
                    Console.WriteLine("- Hvis ingen går bust, vinder den med den højeste værdi. Uafgjort resulterer i en push (ingen vinder)");
                    Console.WriteLine("\nTryk på en tast for at fortsætte..");
                    Console.ReadKey();
                    break;
                // Afslutter programmet
                case 3:
                    Console.Clear();
                    Console.WriteLine("Tak for nu. Byebye. 👋");
                    Environment.Exit(0);
                    break;
                // Ved andre valg end mulighederne, sender fejlbesked
                default:
                    Console.WriteLine("Ugyldigt valg. Tryk på en tast for at fortsætte..");
                    Console.ReadLine();
                    break;
            }

        }
    }

    // Funktion der kører spilrunderne
    public void SpilRunde()
    {
        // Tjek for hvis spillernavn er tom, og derefter tøm begges hænder til ny runde
        Console.WriteLine(titel);
        if (spiller != null) // hvis spiller ikke er null, så fortsæt
        {
            spiller.TømHånd();
        }
        dealer.TømHånd();
        kortbunke = new Kortbunke();

        // starter kortuddeling (2 til hver)
        spiller.Hånd.Add(kortbunke.TrækKort());
        dealer.Hånd.Add(kortbunke.TrækKort());
        spiller.Hånd.Add(kortbunke.TrækKort());
        dealer.Hånd.Add(kortbunke.TrækKort());

        // Fortæller spiller hvilke kort dealer og spiller har
        Console.WriteLine($" - Dealers hånd: {dealer.Hånd.kortPåHånden[0]} 🂠");
        Console.WriteLine($" - {spiller.Navn}'s hånd: {spiller.Hånd} ({spiller.Hånd.VinderVærdi()})");

        // Checker for blackjack
        if (spiller.Hånd.ErBlackjack() || dealer.Hånd.ErBlackjack())
        {
            DealerHånd();
            // Spiller vinder ved blackjack
            if (spiller.Hånd.ErBlackjack() && !dealer.Hånd.ErBlackjack())
            {
                Console.WriteLine("Blackjack! Du vandt! 👏");
            }
            // Dealer vinder ved blakcjack
            else if (!spiller.Hånd.ErBlackjack() && dealer.Hånd.ErBlackjack())
            {
                Console.WriteLine("Dealer har Blackjack! Du tabte! 😞");
            }
            // Uafgjort hvis begge har blackjack
            else
            {
                Console.WriteLine("Begge har Blackjack! Uafgjort! 🤝");
            }
            return;
        }

        // Sætter hanlding som ubestemt
        bool spillerHandling = false;
        // While løkke til når spiller tager et valg
        while (!spillerHandling)
        {
            // Trimmer svar fra spiller til små bogstaver
            Console.WriteLine("Handling? (hit/stand)");
            string handling = Console.ReadLine()?.Trim().ToLower() ?? "stand";
            
            // Hvis "hit"
            if (handling == "hit")
            {
                // Spiller trækker kort fra bunken og ny værdi tilføjes
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                var k = kortbunke.TrækKort();
                spiller.Hånd.Add(k);
                Console.WriteLine($"   {spiller.Navn} trak: {k}");
                Console.WriteLine($" - {spiller.Navn}'s hånd: {spiller.Hånd} ({spiller.Hånd.VinderVærdi()})");
                // hvis spillers værdi er for høj, så er de bust
                if (spiller.Hånd.ErBust())
                {
                    DealerHånd();
                    Console.WriteLine("Bust! Du tabte 🙁");
                    return;
                }
            }
            else
            {
                // Når "hit" kaldes, tilsæt spacing
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                spillerHandling = true;
            }
        }

        DealerHånd();

        // Så længe dealers hånd er under 17, så træk kort, ellers så "stand"
        while (dealer.Hånd.VinderVærdi() < 17)
        {
            var k = kortbunke.TrækKort();
            dealer.Hånd.Add(k);
            Console.WriteLine($"   Dealer trak: {k} ({dealer.Hånd.VinderVærdi()})");
        }

        // Tæl håndværdi på spiller og dealer. Udskriver resultatet.
        int spillerVærdi = spiller.Hånd.VinderVærdi();
        int dealerVærdi = dealer.Hånd.VinderVærdi();
        Console.WriteLine($"\nResultat: {spiller.Navn} {spillerVærdi} vs Dealer {dealerVærdi}");

        // Hvis dealer er bust, og spillers værdi er over dealer, så vinder spiller
        if (dealer.Hånd.ErBust() || spillerVærdi > dealerVærdi)
        {
            Console.WriteLine("Tillykke du vandt! 👏");
        }
        // Hvis spiller og dealer håndværdi er det samme, så uafgjort
        else if (spillerVærdi == dealerVærdi)
        {
            Console.WriteLine("Uafgjort.");
        }
        // Hvis dealers værdi er større end spiller, så vinder dealer
        else
        {
            Console.WriteLine("Dealer vandt ");
        }
    }

    // Funktion til at fremvise Dealers hånd og værdi
    public void DealerHånd()
    {
        Console.WriteLine($"\n - Dealers hånd: {dealer.Hånd} ({dealer.Hånd.VinderVærdi()})");
    }
}