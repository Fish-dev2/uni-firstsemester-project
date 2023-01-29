using System;
using System.Collections.Generic;
using System.Text;

namespace Feleves_HAZI_NEW
{
    class Eloadas
    {
        private int kezdIdo;
        private int vegIdo;
        private int iD;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }


        public int KezdIdo
        {
            get { return kezdIdo; }
            set { kezdIdo = value; }
        }
        public int VegIdo
        {
            get { return vegIdo; }
            set { vegIdo = value; }
        }


        public Eloadas()
        {
            this.kezdIdo = 0;
            this.vegIdo = 24;
            this.iD = -1;
        }
        public Eloadas(int kezdIdo, int vegIdo, int sorSzam)
        {
            this.kezdIdo = kezdIdo;
            this.vegIdo = vegIdo;
            this.iD = sorSzam;
            if (vegIdo <= kezdIdo)
            {
                throw new Exception("Eloadas Vegido bigger than Kezdido");
            }
        }
        public Eloadas(Eloadas eloadas) : this(eloadas.kezdIdo, eloadas.vegIdo, eloadas.iD)
        {

        }
        public int Hossz()
        {
            return vegIdo - kezdIdo;
        }
        public bool Osszeegyeztetheto(Eloadas vizsgalt)
        {
            for (int i = this.KezdIdo; i < this.VegIdo + 1; i++)
            {
                if (i > vizsgalt.KezdIdo && i < vizsgalt.VegIdo)
                {
                    return false;
                }
            }
            if (this.KezdIdo > vizsgalt.VegIdo || this.VegIdo < vizsgalt.KezdIdo)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
