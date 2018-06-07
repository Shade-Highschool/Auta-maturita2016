using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auta
{
    class Auto
    {
       #region Variables
        private string znacka,
            model,
            karoserie,
            palivo,
            obsahMotoru,
            barva,
            najetoKm,
            rokVyroby;
        private int nakupniCena, prodejniCena;

        public int NakupniCena
        {
            get { return nakupniCena; }
            set { nakupniCena = value;}
        }
        public int ProdejniCena
        {
            get { return prodejniCena; }
            set { prodejniCena = value; }
        }
    #endregion

        public Auto(string line)
        {
            string[] data = line.Split(';');
            this.znacka = data[0];
            this.model = data[1];
            this.karoserie = data[2];
            this.palivo = data[3];
            this.obsahMotoru = data[4];
            this.barva = data[5];
            this.najetoKm = data[6];
            this.rokVyroby = data[7];
            this.nakupniCena = Convert.ToInt32(data[8]);
            this.prodejniCena = Convert.ToInt32(data[9]); //vytvoří auto z csv řádku
        }
        public string ToCSV()
        {
            return znacka + ";" + model + ";" + karoserie + ";" + palivo + ";" + obsahMotoru + ";" + barva + ";" + najetoKm + ";" + rokVyroby + ";" + nakupniCena.ToString() + ";" + prodejniCena.ToString() + ";";
        } //vrátí csv řádek
    }
}
