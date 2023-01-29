using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Feleves_HAZI_NEW
{
    class BeosztasKeszito
    {
        private Eloadas[] elosztandoEloadasok;
        private Napirend napirendElso;
        private Napirend napirendMasodik;

        public Napirend NapirendMasodik
        {
            get { return napirendMasodik; }
            set { napirendMasodik = value; }
        }

        public Napirend NapirendElso
        {
            get { return napirendElso; }
            set { napirendElso = value; }
        }

        public Eloadas[] ElosztandoEloadasok
        {
            get { return elosztandoEloadasok; }
            set { elosztandoEloadasok = value; }
        }
        private Napirend[] osszLehetNR;

        public Napirend[] OsszLehetNR
        {
            get { return osszLehetNR; }
            set { osszLehetNR = value; }
        }


        public BeosztasKeszito(string fajlNev)
        {
            string[] rows = File.ReadAllLines(fajlNev);
            Eloadas[] eloadasok = new Eloadas[int.Parse(rows[0])];
            for (int i = 0; i < eloadasok.Length; i++)
            {
                string[] row = rows[i + 1].Split(' ');
                eloadasok[i] = new Eloadas(int.Parse(row[0]), int.Parse(row[1]), i + 1);
            }
            this.elosztandoEloadasok = eloadasok;
            EloadasokKiir();
            string lehetsegesek = LehetsegesNapirendekString();

            OsszLehetNR = DuplikatumokTorlese(lehetsegesek.Split('\n'));


            //ideiglenes
            for (int i = 0; i < OsszLehetNR.Length; i++)
            {
                Console.WriteLine(OsszLehetNR[i]);
            }
            int eloadasErtek = OsszLehetNR[0].OsszEloadasIdo();
            //eddig

            LegjobbNapirendekMegkeres();

        }

        private Napirend[] DuplikatumokTorlese(string[] teljes)
        {
            Napirend[] ret = new Napirend[teljes.Length];
            int counter = 0;
            for (int i = 0; i < teljes.Length; i++)
            {
                if (teljes[i] != "")
                {
                    string[] sor = teljes[i].Split(',');
                    Napirend tempNR = new Napirend(sor, elosztandoEloadasok);
                    int j = 0;
                    bool canAdd = true;
                    while (j < ret.Length && ret[j] != null && canAdd)
                    {
                        if (ret[j].Equals(tempNR))
                        {
                            canAdd = false;
                        }
                        j++;
                    }
                    if (canAdd)
                    {
                        ret[counter] = new Napirend(tempNR.Eloadasok);
                        counter++;
                    }
                }
            }
            Napirend[] vissza = new Napirend[counter];
            for (int i = 0; i < vissza.Length; i++)
            {
                vissza[i] = new Napirend(ret[i].Eloadasok);
            }
            return vissza;
        }

        private string LehetsegesNapirendekString()
        {
            string lehet = "";
            for (int i = 0; i < elosztandoEloadasok.Length; i++)
            {
                Napirend nr = new Napirend(new Eloadas[] { this.ElosztandoEloadasok[i] });
                string templehet = this.LehetsegesEloadasokNapirendhez(nr);
                lehet += templehet;
            }
            return lehet;
        }

        public string LehetsegesEloadasokNapirendhez(Napirend napirend)
        {
            string lehet = "";
            for (int i = 0; i < napirend.Eloadasok.Length; i++)
            {
                lehet += (napirend.Eloadasok[i].ID);
                if (i != napirend.Eloadasok.Length - 1)
                {
                    lehet += ",";
                }
            }
            lehet += "\n";

            for (int i = 0; i < elosztandoEloadasok.Length; i++)
            {
                if (napirend.EloadasBefer(elosztandoEloadasok[i]))
                {
                    Napirend uj = new Napirend(napirend.Eloadasok);
                    uj.EloadasHozzaAd(elosztandoEloadasok[i]);
                    lehet += LehetsegesEloadasokNapirendhez(uj);
                }
            }
            return lehet;
        }

        public void EloadasokKiir()
        {
            Console.Clear();
            int maxidopont = 0;
            for (int i = 0; i < elosztandoEloadasok.Length; i++)
            {
                if (elosztandoEloadasok[i].VegIdo > maxidopont)
                {
                    maxidopont = elosztandoEloadasok[i].VegIdo;
                }
            }
            for (int i = 1; i < maxidopont; i++)
            {
                if (i % 5 != 0)
                {
                    Console.Write("--");
                }
                else
                {
                    if (i.ToString().Length == 1)
                    {
                        Console.Write("0" + i);
                    }
                    else
                    {
                        Console.Write(i);
                    }
                }
            }
            Console.WriteLine();
            for (int i = 0; i < elosztandoEloadasok.Length; i++)
            {
                Console.SetCursorPosition((elosztandoEloadasok[i].KezdIdo * 2 - 2), (i + 1) * 2);
                for (int j = 0; j < elosztandoEloadasok[i].Hossz() * 2; j++)
                {
                    Console.Write("▄");
                }

            }
        }

        public Napirend SzovegAlapjanNapirend(string szoveg)
        {
            Napirend nr = new Napirend();
            string[] szovegSplit = szoveg.Split(',');
            for (int i = 0; i < szovegSplit.Length; i++)
            {
                nr.EloadasHozzaAd(elosztandoEloadasok[int.Parse(szovegSplit[i]) - 1]);
            }
            return nr;
        }

        private void LegjobbNapirendekMegkeres()
        {
            int maxIindex = 0;
            int saveJindex = 0;
            for (int i = 1; i < OsszLehetNR.Length; i++)
            {
                int maxJindex = 0;
                for (int j = 1; j < OsszLehetNR.Length; j++)
                {
                    if (j != i)
                    {
                        if (OsszLehetNR[j].Eloadasok.Length > OsszLehetNR[maxJindex].Eloadasok.Length && OsszLehetNR[i].NapirendekOsszefernek(OsszLehetNR[j]))
                        {
                            maxJindex = j;
                        }
                    }
                }
                if (OsszLehetNR[i].Eloadasok.Length + OsszLehetNR[maxJindex].Eloadasok.Length > OsszLehetNR[maxIindex].Eloadasok.Length + OsszLehetNR[saveJindex].Eloadasok.Length)
                {
                    maxIindex = i;
                    saveJindex = maxJindex;
                }
            }

            Console.WriteLine("LEGJOBB NAPIRENDEK:");
            OsszLehetNR[maxIindex].Rendez();
            OsszLehetNR[saveJindex].Rendez();
            Console.WriteLine(OsszLehetNR[maxIindex]);
            Console.WriteLine(OsszLehetNR[saveJindex]);
            NapirendElso = new Napirend(OsszLehetNR[maxIindex].Eloadasok);
            NapirendMasodik = new Napirend(OsszLehetNR[saveJindex].Eloadasok);
            FajlbaMent();
        }

        private void FajlbaMent()
        {
            StreamWriter sw = new StreamWriter("rendez.ki");
            sw.WriteLine("{0} {1}", NapirendElso.Eloadasok.Length, NapirendMasodik.Eloadasok.Length);
            sw.WriteLine(NapirendElso.ToString().Replace(',', ' '));
            sw.WriteLine(NapirendMasodik.ToString().Replace(',', ' '));
            sw.Flush();
            sw.Close();
        }
    }
}
