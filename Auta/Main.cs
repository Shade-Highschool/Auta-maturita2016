using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

namespace Auta
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        string hlavicka;
        public void updateDataGrid()
        {
            dataGridView1.Rows.Clear(); //Clear datagrid

            foreach (Auto car in AutoBazar.carList)
                dataGridView1.Rows.Add(car.ToCSV().Split(';')); 
            dataGridView1.Refresh(); //Vyplní a refreshne datagrid
        } //Update Datagrid
        public void loadCSV()
        {
            using(StreamReader reader = new StreamReader("cars.csv",Encoding.Default))
            {
                hlavicka = reader.ReadLine();
                string line;
                while((line = reader.ReadLine()) !=null) 
                {
                    Auto car = new Auto(line);
                    AutoBazar.carList.Add(car);
                    dataGridView1.Rows.Add(line.Split(';'));
                }
            } //načte data

            foreach (string item in hlavicka.Split(';'))
                comboBox1.Items.Add(item);
            comboBox1.SelectedIndex = 0; //načte filtr a vybere první item
        } //Přečte data, vytvoří auto, přidá auto do listu, data do datagridView
        public void saveCSV()
        {
            using (StreamWriter writer = new StreamWriter("cars.csv",false,Encoding.Default))
            {
                writer.WriteLine(hlavicka);
                foreach (Auto car in AutoBazar.carList)
                    writer.WriteLine(car.ToCSV());
            }
        } //Uloží auta do csv
        private void Form1_Load(object sender, EventArgs e)
        {
            loadCSV();
            AutoBazar.vydaje = Properties.Settings.Default.vydaje;
            AutoBazar.prodano = Properties.Settings.Default.prodano;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            saveCSV();
            Properties.Settings.Default.prodano = AutoBazar.prodano;
            Properties.Settings.Default.vydaje = AutoBazar.vydaje;
            Properties.Settings.Default.Save();
        }//Přivypnutí aplikace se uloží

        private void formulářToolStripMenuItem_Click(object sender, EventArgs e) //Otevře formulář - PřidejAuto
        {
            FormulářPřidatAuto formular = new FormulářPřidatAuto();
            formular.ShowDialog();
            updateDataGrid();
        }

        private void odstranitAutoToolStripMenuItem_Click(object sender, EventArgs e) //OdstraňAuto
        {
            if(dataGridView1.CurrentCell == null)
            { MessageBox.Show("Nemáte vybrané auto"); return; }

           int postion = dataGridView1.CurrentCell.RowIndex;
           AutoBazar.OdstranAuto(postion);
           updateDataGrid();
        }

        private void vratAutoToolStripMenuItem_Click(object sender, EventArgs e) //VraťAuto
        {
            if (dataGridView1.CurrentCell == null)
            { MessageBox.Show("Nemáte vybrané auto"); return; }

            int postion = dataGridView1.CurrentCell.RowIndex;
            AutoBazar.VratAuto(postion);
            updateDataGrid();
        }

        private void prodejAutoToolStripMenuItem_Click(object sender, EventArgs e) //ProdejAuto
        {
            if (dataGridView1.CurrentCell == null)
            { MessageBox.Show("Nemáte vybrané auto"); return; }

            int postion = dataGridView1.CurrentCell.RowIndex;
            AutoBazar.ProdejAuto(postion);
            updateDataGrid();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e) //ZměnProdejníCenu
        {
            if (dataGridView1.CurrentCell == null)
            { MessageBox.Show("Nemáte vybrané auto"); return; }

            int postion = dataGridView1.CurrentCell.RowIndex;
            try { AutoBazar.carList[postion].ProdejniCena = Convert.ToInt32(toolStripTextBox1.Text); } //nastavíme prodejní cenu
            catch { MessageBox.Show("Špatně zadaná hodnota"); }
            updateDataGrid();
        }

        private void zobrazNákupníCenuAutaToolStripMenuItem_Click(object sender, EventArgs e) //ZobrazNákupníCenuAuta
        {
            if (dataGridView1.CurrentCell == null)
            { MessageBox.Show("Nemáte vybrané auto"); return; }

            int postion = dataGridView1.CurrentCell.RowIndex;
            MessageBox.Show(AutoBazar.carList[postion].NakupniCena.ToString());

        }

        private void zobrazProdejníCenuAutaToolStripMenuItem_Click(object sender, EventArgs e) //Zobraz ProdejníCenuAuta
        {
            if (dataGridView1.CurrentCell == null)
            { MessageBox.Show("Nemáte vybrané auto"); return; }

            int postion = dataGridView1.CurrentCell.RowIndex;
            MessageBox.Show(AutoBazar.carList[postion].ProdejniCena.ToString());
        }

        private void výdělekToolStripMenuItem_Click(object sender, EventArgs e) //Zobraz výdělek
        {
            MessageBox.Show(AutoBazar.Vydelek.ToString());
        }

        private void zobrazCenuVšechAutToolStripMenuItem_Click(object sender, EventArgs e) //ZobrazCenuVšechAut
        {
            MessageBox.Show(AutoBazar.ZobrazCenuVsechAut().ToString());
        }

        private void penízeToolStripMenuItem_Click(object sender, EventArgs e) //ZlevniVšechnyAutaOcenu
        {
            int number;
            if (int.TryParse(toolStripTextBox3.Text, out number))
                AutoBazar.ZlevniVsechnyAuta(Convert.ToInt32(toolStripTextBox3.Text));
            else
                MessageBox.Show("Zadejte správnou hodnotu!");
            updateDataGrid();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e) //ZlevniOprocenta
        {
            int number;
            if (toolStripTextBox4.Text.Contains('%'))
               toolStripTextBox4.Text = toolStripTextBox4.Text.Replace("%",""); //Pokud se tam nachází procento, odstraň

            if (int.TryParse(toolStripTextBox4.Text, out number))
            {
                AutoBazar.ZlevniProcenta(Convert.ToInt32(toolStripTextBox4.Text));
            }
            else
                MessageBox.Show("Zadejte správnou hodnotu!");

            updateDataGrid();
        }

        private void procenaToolStripMenuItem_Click(object sender, EventArgs e) //ZdražOprocenta
        {
            int number;
            if (toolStripTextBox6.Text.Contains('%'))
                toolStripTextBox6.Text = toolStripTextBox6.Text.Replace("%", ""); //Pokud se tam nachází procento, odstraň

            if (int.TryParse(toolStripTextBox6.Text, out number))
            {
                AutoBazar.ZdrazProcenta(Convert.ToInt32(toolStripTextBox6.Text));
            }
            else
                MessageBox.Show("Zadejte správnou hodnotu!");

            updateDataGrid();
        }

        private void penízeToolStripMenuItem1_Click(object sender, EventArgs e) //ZdražOhodnotu
        {
            int number;
            if (int.TryParse(toolStripTextBox5.Text, out number))
                AutoBazar.ZdrazVsechnyAuta(Convert.ToInt32(toolStripTextBox5.Text));
            else
                MessageBox.Show("Zadejte správnou hodnotu!");
            updateDataGrid();
        }

        private void textBox1_TextChanged(object sender, EventArgs e) //Filtr
        {
            dataGridView1.ClearSelection(); //vyčistí
            if(textBox1.Text!= string.Empty)
            {
                for(int i = 0;i<dataGridView1.Rows.Count;i++)
                {
                    if (dataGridView1.Rows[i].Cells[comboBox1.SelectedIndex].Value.ToString().ToLower().Contains(textBox1.Text.ToLower()))  //Tady by jste nám mohl někdy vysvětlit jestli je to správně, nebo špatně. Takhle házet metody furt za sebou - .ToString().Tolower().Containst()
                        dataGridView1.Rows[i].Selected = true; //projede všechny řádky a zjistí jestli neobsahují něco z textboxu. Pokud ano, vybere je
                }
            }
        }
    }
}
