using System;
using System.Collections.Generic;
using System.Text;

namespace Feleves_HAZI_NEW
{
    class Napirend
    {
        private Eloadas[] eloadasok;

        public Eloadas[] Eloadasok
        {
            get { return eloadasok; }
            set { eloadasok = value; }
        }

        public Napirend()
        {
            eloadasok = new Eloadas[0];
        }
        public Napirend(Eloadas[] eloadasok)
        {
            this.Eloadasok = NapirendMasol(eloadasok);
        }
        public Napirend(string[] sorSzamok, Eloadas[] osszEloadas) : this()
        {
            for (int i = 0; i < sorSzamok.Length; i++)
            {
                EloadasHozzaAd(osszEloadas[int.Parse(sorSzamok[i]) - 1]);
            }
        }
        public void EloadasHozzaAd(Eloadas eloadas)
        {
            if (!VanEloadas(eloadas))
            {
                Eloadas[] eloadasoktemp = new Eloadas[eloadasok.Length + 1];
                for (int i = 0; i < eloadasok.Length; i++)
                {
                    eloadasoktemp[i] = eloadasok[i];
                }
                eloadasoktemp[eloadasok.Length] = eloadas;
                eloadasok = NapirendMasol(eloadasoktemp);
            }

        }
        public Eloadas[] NapirendMasol(Eloadas[] masoladno)
        {
            Eloadas[] retNapiRend = new Eloadas[masoladno.Length];
            for (int i = 0; i < masoladno.Length; i++)
            {
                retNapiRend[i] = masoladno[i];
            }
            return retNapiRend;
        }

        public int OsszEloadasIdo()
        {
            int sum = 0;
            for (int i = 0; i < eloadasok.Length; i++)
            {
                sum += eloadasok[i].Hossz();
            }
            return sum;
        }

        public bool VanEloadas(int keresettID)
        {
            if (EloadasMegkeres(keresettID) != -1)
            {
                return true;
            }
            return false;
        }
        public bool VanEloadas(Eloadas keresett)
        {
            if (EloadasMegkeres(keresett.ID) != -1)
            {
                return true;
            }
            return false;
        }
        public bool EloadasBefer(Eloadas vizsgalt)
        {
            //vizsgálja h egy adott előadás befér e az előadások közé időrend alapján
            if (VanEloadas(vizsgalt.ID))
            {
                return false;
            }
            for (int i = 0; i < eloadasok.Length; i++)
            {
                if (!eloadasok[i].Osszeegyeztetheto(vizsgalt))
                {
                    return false;
                }
            }
            return true;
        }

        private int EloadasMegkeres(int keresettID)
        {
            for (int i = 0; i < eloadasok.Length; i++)
            {
                if (eloadasok[i].ID == keresettID)
                {
                    return i;
                }
            }
            return -1;
        }
        public bool Equals(Napirend vizsgalt)
        {
            if (vizsgalt.Eloadasok.Length == this.Eloadasok.Length)
            {
                for (int i = 0; i < vizsgalt.Eloadasok.Length; i++)
                {
                    if (!this.VanEloadas(vizsgalt.Eloadasok[i]))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
        public override string ToString()
        {
            string output = "";
            for (int i = 0; i < Eloadasok.Length; i++)
            {
                output += Eloadasok[i].ID;
                if (i != Eloadasok.Length - 1)
                {
                    output += ",";
                }
            }
            return output;
        }
        public void Rendez()
        {
            for (int i = 0; i < Eloadasok.Length - 1; i++)
            {
                for (int j = i + 1; j < Eloadasok.Length; j++)
                {
                    if (Eloadasok[i].KezdIdo > Eloadasok[j].KezdIdo)
                    {
                        Eloadas temp = new Eloadas(Eloadasok[i]);
                        Eloadasok[i] = new Eloadas(Eloadasok[j]);
                        Eloadasok[j] = new Eloadas(temp);
                    }
                }
            }
        }
        public bool NapirendekOsszefernek(Napirend vizsgalt)
        {
            if (this.Equals(vizsgalt))
            {
                return false;
            }
            for (int i = 0; i < this.Eloadasok.Length; i++)
            {
                for (int j = 0; j < vizsgalt.Eloadasok.Length; j++)
                {
                    if (this.Eloadasok[i].ID == vizsgalt.Eloadasok[j].ID)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
