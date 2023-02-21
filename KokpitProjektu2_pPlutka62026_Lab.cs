using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projekt2_Plutka62026_Laboratorium
{
    public partial class KokpitProjektu2_pPlutka62026_Lab : Form
    {
        public KokpitProjektu2_pPlutka62026_Lab() //konstruktor
        {
            InitializeComponent();
        }

        private void KokpitProjektu2_pPlutka62026_Lab_Load(object sender, EventArgs e)
        {
           
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
       

        private void SzeregIndywidualny_Click(object sender, EventArgs e)
        {
            //sprawdzenie czy egzemplarz formularza KokpitProjektu2_pPlutka62026_Lab_Load byl juz utworzony i jest w kolekcji OpenForms
            foreach (Form Formularz in Application.OpenForms)
                if (Formularz.Name == "Szereg Indywidualny")
                {
                    //ukrywamy biezacy formularz
                    this.Hide();
                    //odsloniecie formularza SzeregLab
                    Formularz.Show();
                    //zakonczenie obslugi
                    return;
                }
            //utworzenie egzemplarza
            szereg_indywidualny AnalizatorSzeregu = new szereg_indywidualny();
            this.Hide();
            AnalizatorSzeregu.Show();
        }

        private void SzeregLaboratoryjny_Click(object sender, EventArgs e)
        {
                //sprawdzenie czy egzemplarz formularza KokpitProjektu2_pPlutka62026_Lab_Load byl juz utworzony i jest w kolekcji OpenForms
                foreach (Form Formularz in Application.OpenForms)
                    if (Formularz.Name == "Szereg Laboratoryjny")
                    {
                        //ukrywamy biezacy formularz
                        this.Hide();
                        //odsloniecie formularza SzeregLab
                        Formularz.Show();
                        //zakonczenie obslugi
                        return;
                    }
            //utworzenie egzemplarza
            szereg__laboratoryjny AnalizatorSzeregu = new szereg__laboratoryjny();
                this.Hide();
                AnalizatorSzeregu.Show();
            
        }

        private void KokpitProjektu2_pPlutka62026_Lab_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult OknoMessage 
                = MessageBox.Show("CZy chcesz zmienic ten formularz? i zakonczyc dzialanie programu?", this.Text,MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
            //odczytanie wybranego przycisku
            if (OknoMessage == DialogResult.Yes)
            {
                //ptwierdzenie zamkniecia formularza
                e.Cancel = true;
                //zakonczenie dzialania programu i wzystkich wątkow(procesow) w nim zawartych
                Application.ExitThread();

            }
            else
            
                //anulowanie zdarzenia 'zamknij'
                e.Cancel= false;    

        }
    }
}
