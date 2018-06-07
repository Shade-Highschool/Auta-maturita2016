using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Auta
{
    public partial class FormulářPřidatAuto : Form
    {
        public FormulářPřidatAuto()
        {
            InitializeComponent();
        }
        private bool isOk() // Ověří
        {
            int number;
            if(int.TryParse(textBox10.Text,out number) && int.TryParse(textBox9.Text,out number))
            { } //Pokud to je číslo - pokračuj
            else
            {
                MessageBox.Show("Vyplńte správně ceny!");
                return false;
            }


            foreach(Control c in Controls) //Zkontroluje zdali jsou vyplněny všechny údaje
            {
                if(c is TextBox)
                {
                    TextBox txt = (TextBox)c;
                    if (txt.Text == string.Empty)
                    {
                        MessageBox.Show("Vyplňte všechny údaje!");
                        return false;
                    }
                }
            }
            return true;
        }
        private Auto makeCar() //Vytvoří auto
        {
            Auto car = new Auto(textBox1.Text + ";" + textBox2.Text + ";" + textBox3.Text + ";" + textBox4.Text + ";" + textBox5.Text + ";"
                                   + textBox6.Text + ";" + textBox7.Text + ";" + textBox8.Text + ";" + textBox9.Text + ";" + textBox10.Text);
            return car;
        }
        private void btn_pridatAuto_Click(object sender, EventArgs e)
        {
            if(isOk())
            {
                AutoBazar.PridejAuto(makeCar());
                MessageBox.Show("Auto přidáno!");
            }

        }
    }
}
