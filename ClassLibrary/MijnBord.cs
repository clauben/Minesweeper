using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{

    public class MijnBord
    {
        private int _lengte;
        private int _breedte;
        private int _mijnen;
        private int _mijn = -1;
        private int[][] _spelbord;
        private bool[][] _zichtbaar;
        private Stapel<bool[][]> _stapel = new Stapel<bool[][]>();

        public MijnBord(int lengte, int breedte, int mijnen)
        {
            _lengte = lengte;
            _breedte = breedte;
            _mijnen = mijnen;
        }

        public bool StartSpel(bool speler)
        {
            bool eindeSpel = false;
            Point volgendeZet;
            Zichtbaarheid();

            if (speler)
            {
                volgendeZet = spelerZet();
                GenereerBord(volgendeZet);
                Doezet(volgendeZet);
                Print();
            }
            else
            {
                volgendeZet = AlgoritmeZet();
                GenereerBord(volgendeZet);
                Doezet(volgendeZet);
            }
            while (!Dood() && !Gewonnen())
            {
                if (speler)
                {
                    volgendeZet = spelerZet();
                    Doezet(volgendeZet);
                    Print();
                }
                else
                {
                    volgendeZet = AlgoritmeZet();
                    Doezet(volgendeZet);
                }

            }
            if(Dood() && speler)
            {
                Console.WriteLine("Helaas, u heeft verloren!");
                eindeSpel = Dood();
            }
            else
            {
                Console.WriteLine("Hoera, u heeft het spel gewonnen");
                eindeSpel = Gewonnen();
            }

            return eindeSpel;
        }

        private bool Dood()
        {
            for (int i = 0; i < _zichtbaar.Length; i++)
            {
                for (int j = 0; j < _zichtbaar.Length; j++)
                {
                    if (_zichtbaar[i][j] && _spelbord[i][j] == _mijn)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool Gewonnen()
        {
            int count = 0;
            for (int i = 0; i < _zichtbaar.Length; i++)
            {
                for (int j = 0; j < _zichtbaar.Length; j++)
                {
                    if (_zichtbaar[i][j])
                    {
                        count++;
                    }
                }
            }
            if (count == (_lengte * _breedte - _mijnen) && !Dood())
            {
                return true;
            }
            return false;
        }

        private void Undo()
        {
            _zichtbaar = _stapel.Pop();
        }

        private Point spelerZet()
        { 
            string u = "";
            int x = 0;
            int y = 0;

            while(u.Equals("") && !_stapel.IsLeeg())
            {
                Console.WriteLine("Typ 'u' voor undo of 'enter' om door te gaan");
                u = Console.ReadLine();
                Console.WriteLine("");
                if(u.Equals("u"))
                {
                    Undo();
                    u = "";
                }
                else
                {
                    break;
                }                
            }

            Console.WriteLine("Maak een zet!");
            Console.Write("X: ");
            x = int.Parse(Console.ReadLine());
            Console.Write("Y: ");
            y = int.Parse(Console.ReadLine());
            Console.WriteLine("");

            return new Point(x, y);
        }

        private void Print()
        {
            string x = "";

            for (int i = 0; i < _lengte; ++i)
            {
                for (int j = 0; j < _breedte; j++)
                {
                    if (_zichtbaar[i][j])
                    {
                        x = x + "[ " + _spelbord[i][j] + " ]";
                    }
                    else
                    {
                        x = x + "[   ]";
                    }
                }

                Console.WriteLine(x);
                x = "";
            }
            Console.WriteLine("");
        }

        public int TelMijnen(Point hier)
        {
            int x = hier.X;
            int y = hier.Y;
            int counter = 0;

            if (_spelbord[hier.X][hier.Y] != _mijn)
            {
                for (int i = x - 1; i < x + 2; i++)
                {
                    for (int j = y - 1; j < y + 2; j++)
                    {
                        if (i < _lengte && j < _breedte && i >= 0 && j >= 0)
                        {
                            if (_spelbord[i][j] == _mijn)
                            {
                                counter++;
                            }                           
                        }
                    }
                }
                return counter;
            }
            return _mijn;
        }

        private void GenereerBord(Point verboden)
        {
            _spelbord = new int[_lengte][];
            Random rand = new Random();
            int bomCounter = 0;

            //Initialiseer een leeg spelbord
            for (int i = 0; i < _lengte; i++)
            {
                int[] arr = new int[_lengte];

                for (int j = 0; j < _breedte; j++)
                {
                    arr[j] = 0;
                }
                _spelbord[i] = arr;
            }

            //Plaats het aantal mijnen op het spelbord behalve op het beginpunt of op een geplaatste mijn
            while (bomCounter < _mijnen)
            {
                int randX = rand.Next(_lengte);
                int randY = rand.Next(_breedte);
                Point randPoint = new Point(randX, randY);

                if (randPoint != verboden && _spelbord[randX][randY] != _mijn)
                {
                    _spelbord[randX][randY] = _mijn;
                    bomCounter++;
                }
            }

            //Plaats het aantal getelde mijnen rondom het punt als int op het gegeven spelboardpunt
            for (int i = 0; i < _lengte; i++)
            {
                for (int j = 0; j < _breedte; j++)
                {
                    _spelbord[i][j] = TelMijnen(new Point(i, j));
                }
            }
            onthulRanden(verboden);
        }

        private void Zichtbaarheid()
        {
            _zichtbaar = new bool[_lengte][];

            for (int i = 0; i < _lengte; i++)
            {
                bool[] arr = new bool[_lengte];

                for (int j = 0; j < _breedte; j++)
                {
                    arr[j] = false;
                }
                _zichtbaar[i] = arr;
            }
        }

        private void onthulRanden(Point hier)
        {
            int x = hier.X;
            int y = hier.Y;

            if (_spelbord[hier.X][hier.Y] != _mijn)
            {
                for (int i = x - 1; i < x + 2; i++)
                {
                    for (int j = y - 1; j < y + 2; j++)
                    {
                        if (i < _lengte && j < _breedte && i >= 0 && j >= 0)
                        {
                            if (_spelbord[i][j] != _mijn && _zichtbaar[i][j] == false)
                            {
                                _zichtbaar[i][j] = true;

                                if (_spelbord[i][j] == 0)
                                {
                                    onthulRanden(new Point(i, j));
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Doezet(Point zet)
        {
            if(zet.X < _lengte && zet.Y < _breedte)
            {
                _zichtbaar[zet.X][zet.Y] = true;

                if (_spelbord[zet.X][zet.Y] == 0)
                {
                    onthulRanden(zet);
                    _stapel.Push(_zichtbaar);
                }
            }
        }

        private Point AlgoritmeZet()
        { return new Point(0, 0); }
    }
}
