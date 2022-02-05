namespace Mijnenveger
{
    class Program
    {
        private static MijnBord _heeft = new MijnBord(9,9,10);

        static void Main(string[] args)
        {
            Menu();
        }

        static void Menu()
        {
            bool eindeSpel;
            Console.Write("Voer uw naam in: ");
            string naam = Console.ReadLine();
            Console.WriteLine("Welcome {0} bij Mijnenveger!", naam);
            Console.WriteLine("");
            Console.WriteLine("Keuzemenu");
            Console.WriteLine("1: Spelersspel");
            Console.WriteLine("2: AlgortimeSpel");
            Console.Write("Maak uw keuze: ");
            int keuze = int.Parse(Console.ReadLine());
            if (keuze == 1)
            {
                Console.WriteLine("Het spel is begonnen!");
                eindeSpel = _heeft.StartSpel(true);
                if(eindeSpel)
                {
                    Console.WriteLine("Het spel is afgelopen");
                }
            }
            if (keuze == 2)
            {
                Console.WriteLine("Hoevaak wilt u het algortime spel spelen?");
                Console.Write("Aantal keert: ");
                int aantalKeer = int.Parse(Console.ReadLine());
                Console.WriteLine("Uw winstpercentage van %{0}.",AlgortimeSpel(aantalKeer));
            }
        }

        static int AlgortimeSpel(int aantal)
        {
            double winst = 0;
            double winstpercentage;
            bool gewonnen;

            while(aantal > 0)
            {
                gewonnen = _heeft.StartSpel(false);
                if (gewonnen)
                {
                    winst++;
                }
            }

            winstpercentage = winst / aantal * 100;

            return (int)winstpercentage;
        }
    }
}
