namespace Mijnenveger
{
    class Program
    {
        private static MijnBord _heeft = new MijnBord(9, 9, 10);

        static void Main(string[] args)
        {
            Menu();
        }

        static void Menu()
        {
            bool eindeSpel = true;
            Console.WriteLine("Welcome bij Mijnenveger!");
            Console.WriteLine("");
            Console.WriteLine("Welk spel wilt u spelen?");
            Console.WriteLine("1: Spelersspel");
            Console.WriteLine("2: AlgortimeSpel");
            Console.Write("Maak uw keuze: ");
            Console.WriteLine("");
            int keuze = int.Parse(Console.ReadLine());
            while (eindeSpel)
            {
                if (keuze == 1)
                {
                    Console.WriteLine("");
                    Console.WriteLine("Het spel is begonnen!");
                    eindeSpel = _heeft.StartSpel(true);
                    if (eindeSpel)
                    {
                        Console.WriteLine("");
                    }
                }
                if (keuze == 2)
                {
                    Console.WriteLine("Hoevaak wilt u het algortime spel spelen?");
                    Console.Write("Aantal keert: ");
                    int aantalKeer = int.Parse(Console.ReadLine());
                    Console.WriteLine("Uw winstpercentage van %{0}.", AlgortimeSpel(aantalKeer));
                }
                Console.WriteLine("Wilt u opnieuw spelen? Typ 'yes' of 'no'.");
                string opnieuw = Console.ReadLine();
                if (opnieuw == "yes")
                {
                    eindeSpel = true;
                }
                else
                {
                    eindeSpel = false;
                }
            }
        }

        static int AlgortimeSpel(int aantal)
        {
            double winst = 0;
            double winstpercentage;
            bool gewonnen;

            while (aantal > 0)
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
