using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Stapel<T>
    {
        private Vakje<T> _top;

        private bool IsLeeg()
        {
            if (_top == null)
                return true;
            else
                return false;
        }

        public void Push(T inhoud)
        {
            _top = new Vakje<T>(inhoud, _top);
        }

        public T Pop()
        {
            T huidige = _top.Inhoud;
            _top = _top.Vorige;
            return huidige;
        }

        public int Zoek(T deze)
        {
            int positie = 1;
            Vakje<T> locatie = _top;
            while (locatie != null)
            {
                if (deze.Equals(locatie.Inhoud))
                {
                    return positie;
                }
                locatie = locatie.Vorige;
                positie++;
            }
            return -1;
        }

        public bool Insert(int deze, int locatie)
        { return false; }

        public string Print()
        {
            string a = "";

            Vakje<T> vakje = _top;
            while (vakje != null)
            {
                if (vakje.Vorige != null)
                {
                    a = a + vakje.Inhoud + " ";
                }
                if (vakje.Vorige == null)
                {
                    a = a + vakje.Inhoud;
                }
                vakje = vakje.Vorige;
            }
            return a;
        }

    }

    public class Vakje<T>
    {
        public T Inhoud { get; private set; }
        public Vakje<T> Vorige { get; set; }

        public Vakje(T inhoud, Vakje<T> vorige)
        {
            Inhoud = inhoud;
            Vorige = vorige;
        }
    }
}
