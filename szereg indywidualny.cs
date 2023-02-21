using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Projekt2_Plutka62026_Laboratorium
{
    public partial class szereg_indywidualny : Form
    {
        const float kpDgX = 1;
        const float kpGgX = float.MaxValue;
        float[,] kpTWS;

        public szereg_indywidualny()
        {
            InitializeComponent();
        }

        private void szereg_indywidualny_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void kpbtnWartoscSzeregu_Click(object sender, EventArgs e)
        {
            errorProvider1.Dispose();
            float kpEps;
            float kpX;
            if (!kpPobranieDanych_X_Eps(out kpX, out kpEps))
            {
                return;
            }
            float kpSuma;
            ushort kpLicznikZsumowanychWyrazów;
            kpSuma = kpObliczenieWartościSzeregu(kpX, kpEps, out kpLicznikZsumowanychWyrazów);
            kptxtSumaSzeregu.Text = kpSuma.ToString();
            kptxtLicznikZsumowanychWyrazowSzeregu.Text = kpLicznikZsumowanychWyrazów.ToString();
            kpbtnWartoscSzeregu.Enabled = false;
        }
        #region
        bool kpPobranieDanych_X_Eps(out float kpX, out float kpEps)
        {
            kpX = kpEps = 0.0F;
            if (!float.TryParse(kptxtX.Text, out kpX))
            {
                errorProvider1.SetError(kptxtX, "Error: w zapisie wartosci zmiennej niezaleznej x wystapil niedozwolony znak");
                return false;
            }
            if ((kpX < kpDgX) || (kpX > kpGgX))
            {
                errorProvider1.SetError(kptxtX, "Error: podana wartosc zmiennej niezaleznej X nie nalezy do przedzialu zbieznosci szeregu");
                return false;

            }
            kptxtX.Enabled = false;

            if (!float.TryParse(kptxtEps.Text, out kpEps))
            {
                errorProvider1.SetError(kptxtEps, "Error: w zapisie dokladnosci obliczen Eps wystapil niedozwolony znak");
                return false;
            }
            if ((kpEps <= 0.0F) || (kpEps >= 1.0F))
            {
                errorProvider1.SetError(kptxtEps, "Error: podana dokladnosc niespelnia warunku wyjsciowego: 0.0 < Eps< 1.0");
                return false;
            }
            kptxtEps.Enabled = false;

            return true;
        }



        float kpObliczenieWartościSzeregu(float kpX, float kpEps, out ushort kpn)
        {
            kpn = 1;
            float kpSuma = 0.0F;
            float kpw = 1.0F;
            do
            {
                kpSuma = kpSuma + kpw;
                kpn++;
                kpw = kpw * (1/(5*kpX));
               

            } while (Math.Abs(kpw) > kpEps);

            return kpSuma;


        }
        bool kpPobranieDanych_Xd_Xg_h_Eps(out float kpXd, out float kpXg, out float kpEps, out float kph)
        {
            kpXd = kpXg = kph = kpEps = 0.0F;

            if (!float.TryParse(kptxtXd.Text, out kpXd))
            {
                errorProvider1.SetError(kptxtXd, "ERROR: w zapisie Xd wystąpił niedozwolony znak!");
                return false;
            }
            if ((kpXd < kpDgX) || (kpXd > kpGgX))
            {
                errorProvider1.SetError(kptxtXd, "Error: podana wartosc zmiennej niezaleznej Xd nie nalezy do przedzialu zbieznosci szeregu");
                return false;

            }
            if (!float.TryParse(kptxtXg.Text, out kpXg))
            {
                errorProvider1.SetError(kptxtXg, "ERROR: w zapisie Xg wystąpił niedozwolony znak!");
                return false;
            }
            if ((kpXg < kpDgX) || (kpXg > kpGgX))
            {
                errorProvider1.SetError(kptxtXg, "Error: podana wartosc zmiennej niezaleznej Xg nie nalezy do przedzialu zbieznosci szeregu");
                return false;

            }
            if (kpXd > kpXg)
            {
                errorProvider1.SetError(kptxtXg, "ERROR: granice przedziału Xd, Xg zpstały zapisane w odwrotnej kolejnoci(popraw to)");
                return false;
            }
            kptxtXd.Enabled = false; kptxtXg.Enabled = false;
            if (!float.TryParse(kptxtH.Text, out kph))
            {
                errorProvider1.SetError(kptxtH, "Erroar: w zapisie przyrostu h wystapił błąd");
                return false;

            } 
            if ((kph <= 0.0F) || (kph >= (kpXg - kpXd)))
            {
                errorProvider1.SetError(kptxtH, "Erroar: podana wartosc h nie spelnia warunku wejsciowego dla h: (h > 0) && (h < (Xg - Xd)");
                return false;
            }
            kptxtH.Enabled = false;

            if (!float.TryParse(kptxtEps.Text, out kpEps))
            {
                errorProvider1.SetError(kptxtEps, "Error: w zapisie Eps wystapił błąd");
                return false;
            }
            if ((kpEps <= 0.0F) || (kpEps >= 1.0F))
            {
                errorProvider1.SetError(kptxtEps, "Error: podana wartosc Eps nie spelnia warunku wejsciowego");
                return false;
            }
            kptxtEps.Enabled = false;

            return true;
        }

        void kpTablicowanieWartosciSzeregu(float kpXd, float kpXg, float kph, float kpEps, out float[,] kpTWS)
        {
            int kpn = (int)((kpXg - kpXd) / kph + 1);
            kpTWS = new float[kpn, 3];
            float kpX;
            int kpi; 
            ushort kpLicznikZsumowanychWyrazów;
            for (kpX = kpXd, kpi = 0; kpi < kpTWS.GetLength(0); kpi++, kpX = kpXd + kpi * kph) 
            {
                kpTWS[kpi, 0] = kpX;
                kpTWS[kpi, 1] = kpObliczenieWartościSzeregu(kpX, kpEps, out kpLicznikZsumowanychWyrazów);
                kpTWS[kpi, 2] = kpLicznikZsumowanychWyrazów;
            }

        }
        void kpWpisanieWynikówDoKontrolkiDataGrindView(float[,] kpTWS, DataGridView kpdgvTWS)
        {
            kpdgvTWS.Rows.Clear();
            for (int kpi = 0; kpi < kpTWS.GetLength(0); kpi++)
            {
                kpdgvTWS.Rows.Add();
                kpdgvTWS.Rows[kpi].Cells[0].Value = string.Format("{0:0.00}", kpTWS[kpi, 0]);
                kpdgvTWS.Rows[kpi].Cells[1].Value = string.Format("{0:0.0000}", kpTWS[kpi, 1]);
                kpdgvTWS.Rows[kpi].Cells[2].Value = string.Format("{0}", kpTWS[kpi, 2]);

            }
        }
        #endregion

        private void kpbtnWizualizacjaTabelaryczna_Click(object sender, EventArgs e)
        {

            float kpXd, kpXg, kpEps, kph;
            if (!kpPobranieDanych_Xd_Xg_h_Eps(out kpXd, out kpXg, out kpEps, out kph))

                return;
            if (kpTWS is null)
                kpTablicowanieWartosciSzeregu(kpXd, kpXg, kph, kpEps, out kpTWS);

            kpWpisanieWynikówDoKontrolkiDataGrindView(kpTWS, kpdgvTWS);

            kpdgvTWS.Visible = true;

            kpbtnWizualizacjaTabelaryczna.Enabled = false;

        }

        private void kpbtnWizualizacjaGraficzna_Click(object sender, EventArgs e)
        {

            {
                errorProvider1.Dispose();

                float kpXd, kpXg, kph, kpEps;
                if (!kpPobranieDanych_Xd_Xg_h_Eps(out kpXd, out kpXg, out kph, out kpEps))
                    
                    return;
               
                if (kpTWS is null) 
                    kpTablicowanieWartosciSzeregu(kpXd, kpXg, kph, kpEps, out kpTWS);
                kpWpisanieWynikowDoKontrolkiChart(kpTWS, kpchtWykresSzeregu);
                kpdgvTWS.Visible = true;
                kpchtWykresSzeregu.Visible = true;
                kpbtnWizualizacjaGraficzna.Enabled = false;
            }
            void kpWpisanieWynikowDoKontrolkiChart(float[,] kpTWS, Chart kpchartWykresSzeregu)
            {
                kpchartWykresSzeregu.BorderlineColor = Color.Red;
                kpchartWykresSzeregu.BorderlineDashStyle = ChartDashStyle.DashDotDot;
                kpchartWykresSzeregu.Titles.Add("Wykres zmian wartości szeregu S(X)");
                kpchartWykresSzeregu.Legends.FindByName("Legend1").Docking = Docking.Bottom;
                kpchartWykresSzeregu.BackColor = Color.LightSkyBlue;
                kpchartWykresSzeregu.ChartAreas[0].AxisX.Title = "Wartości X";
                kpchartWykresSzeregu.ChartAreas[0].AxisY.Title = "Wartości S(X)";
                kpchartWykresSzeregu.ChartAreas[0].AxisX.LabelStyle.Format = "{0:f2}";
                kpchartWykresSzeregu.ChartAreas[0].AxisY.LabelStyle.Format = "{0:f2}";
                kpchartWykresSzeregu.Series.Clear();
                kpchartWykresSzeregu.Series.Add("Seria 0");
                kpchartWykresSzeregu.Series[0].XValueMember = "X";
                kpchartWykresSzeregu.Series[0].YValueMembers = "Y";
                kpchartWykresSzeregu.Series[0].IsVisibleInLegend = true;
                kpchartWykresSzeregu.Series[0].Name = "Wartość szeregu potęgowego S(X)";
                kpchartWykresSzeregu.Series[0].ChartType = SeriesChartType.Line; 
                kpchartWykresSzeregu.Series[0].Color = Color.Black;
                kpchartWykresSzeregu.Series[0].BorderDashStyle = ChartDashStyle.Solid;
                kpchartWykresSzeregu.Series[0].BorderWidth = 1;
                for (int kpi = 0; kpi < kpTWS.GetLength(0); kpi++)
                    kpchartWykresSzeregu.Series[0].Points.AddXY(kpTWS[kpi, 0], kpTWS[kpi, 1]);
            }
        }

        private void zapiszTabelaryczneZestawienieSzereguToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void zapiszWPlikuToolStripMenuItem_Click(object sender, EventArgs e)
        {

            errorProvider1.Dispose();
            
            if (kpTWS is null)
            {
                float kpXd, kpXg, kph, kpEps;
                if (!kpPobranieDanych_Xd_Xg_h_Eps(out kpXd, out kpXg, out kph, out kpEps))
                {
                    MessageBox.Show("UWAGA: podczas poberania danych wejściowych z kontrolek TextBox został wykryty niedozwoony znak w ich zapisie!" +
                        " Popraw tą danę i ponownie wybierz (kliknięciem) polecenie z menu Plik");
                    return;
                }
                kpTablicowanieWartosciSzeregu(kpXd, kpXg, kph, kpEps, out kpTWS);
            }
            SaveFileDialog kpOknoWyboruPlikuDoZapisu = new SaveFileDialog();
            kpOknoWyboruPlikuDoZapisu.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            kpOknoWyboruPlikuDoZapisu.FilterIndex = 1; 
            kpOknoWyboruPlikuDoZapisu.RestoreDirectory = true;
            kpOknoWyboruPlikuDoZapisu.InitialDirectory = "E:\\"; 
            kpOknoWyboruPlikuDoZapisu.Title = "Wybór pliku do zapisu tablicy TWS (Tablica Wartości Szeregu)";
            if (kpOknoWyboruPlikuDoZapisu.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamWriter kpPlikZnakowy = new System.IO.StreamWriter(kpOknoWyboruPlikuDoZapisu.FileName);
                try
                {
                    for (int kpi = 0; kpi < kpTWS.GetLength(0); kpi++)
                    {
                        kpPlikZnakowy.Write(kpTWS[kpi, 0].ToString());
                        kpPlikZnakowy.Write(";"); 
                        kpPlikZnakowy.Write(kpTWS[kpi, 1].ToString()); 
                        kpPlikZnakowy.Write(";"); 
                        kpPlikZnakowy.WriteLine(kpTWS[kpi, 2].ToString()); 
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR: podczas zapisywania tablicy TWS w pliku wystąpił błąd: " + ex.Message);
                }
                finally
                {
                    kpPlikZnakowy.Close();
                }
            }
            else
                MessageBox.Show("UWAGA: nie dokonano wyboru pliku i polecenia zapisu tablicy TWS i nie zostało zrealizowane");
        }

        private void wczytajZPlikuToolStripMenuItem_Click(object sender, EventArgs e)
        {

            errorProvider1.Dispose();
            if (!(kpTWS is null))
            {
                DialogResult kpOknoMessage = MessageBox.Show("UWAGA: egzemplarz tablicy TWS już istnieje \r\nCzy bieżący egzemplarz tablicy TWS ma być skasowany i w jego miejsce " +
                    "ma być utworzony nowy egzemplarz, do którego mają zostać 'wczytane' elementy TWS z pliku?" +
                    "\r\n - kliknij przycisk poleceń 'Tak' dla potwierdzenia wczytania elementów tablicy TWS z pliku" +
                    "\r\n - kliknij przycisk poleceń 'Nie' dla skasowania polecenia wczytania elementów tablicy TWS z pliku", "Okno ostrzeżenia", MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (kpOknoMessage == DialogResult.Yes)
                    kpTWS = null; 
                else
                {
                    MessageBox.Show("Polecenie odczytania (pobrania) elementów tablicy TWS z pliku zostało anulowane (skasowane!)");
                    return;
                }
            }
            OpenFileDialog kpOknoWyboruPlikuDoOdczytu = new OpenFileDialog();
            kpOknoWyboruPlikuDoOdczytu.Filter = "txtfiles (*.txt)|*.txt|All files(*.*)|*.*";
            kpOknoWyboruPlikuDoOdczytu.FilterIndex = 1; 
            kpOknoWyboruPlikuDoOdczytu.RestoreDirectory = true;
            kpOknoWyboruPlikuDoOdczytu.InitialDirectory = "H:\\";
            kpOknoWyboruPlikuDoOdczytu.Title = "Wybór pliku do odczytu TWS (Tablicy Wartości Szeregu)";
            if (kpOknoWyboruPlikuDoOdczytu.ShowDialog() == DialogResult.OK)
            {
                string kpWierszDanych; 
                string[] kpDaneWiersza;
                ushort kpLicznikWierszy; 
                System.IO.StreamReader kpPlikZnakowy = new System.IO.StreamReader(kpOknoWyboruPlikuDoOdczytu.FileName);
                kpLicznikWierszy = 0;
                while (!((kpWierszDanych = kpPlikZnakowy.ReadLine()) is null))
                    kpLicznikWierszy++;
                kpPlikZnakowy.Close();
                kpTWS = new float[kpLicznikWierszy, 3];
                kpPlikZnakowy = new System.IO.StreamReader(kpOknoWyboruPlikuDoOdczytu.FileName);
                try
                {
                    int kpNrWiersza = 0;
                    while (!((kpWierszDanych = kpPlikZnakowy.ReadLine()) is null))
                    {
                        kpDaneWiersza = kpWierszDanych.Split(';');
                        kpDaneWiersza[0].Trim(); kpDaneWiersza[1].Trim(); kpDaneWiersza[2].Trim();
                        kpTWS[kpNrWiersza, 0] = float.Parse(kpDaneWiersza[0]);
                        kpTWS[kpNrWiersza, 1] = float.Parse(kpDaneWiersza[1]);
                        kpTWS[kpNrWiersza, 2] = float.Parse(kpDaneWiersza[2]);
                        kpNrWiersza++;
                    }

                    kpWpisanieWynikówDoKontrolkiDataGrindView(kpTWS, kpdgvTWS);
                    kpchtWykresSzeregu.Visible = false;
                    kpdgvTWS.Visible = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR: błąd operacji (działania) na pliku (wyświetlony komunikat): ---> " + ex.Message);
                }
                finally
               
                {
                    kpPlikZnakowy.Close(); 
                    kpPlikZnakowy.Dispose(); 
                }
            }
            else
                MessageBox.Show("Plik do odczytu tablicy TWS nie został wybrany i obsługa polecenia: " +
                    "'Odczytanie stablicowanego szeregu z pliku' (z menu poziomu Plik) nie może być zrealizowana");

        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult kpPytanieDoUżytkownika = MessageBox.Show("Czy napewno chcesz zamknąć formularz (co może skutkować utratą danych zapisanych na formularzu?)",
                this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
            if (kpPytanieDoUżytkownika == DialogResult.Yes)
                Close();
        }

        private void zmianaKoloruTłaToolStripMenuItem_Click(object sender, EventArgs e)
        {

            ColorDialog kpPaletaKolorów = new ColorDialog();
            kpPaletaKolorów.Color = this.BackColor;
            if (kpPaletaKolorów.ShowDialog() == DialogResult.OK)
                this.BackColor = kpPaletaKolorów.Color;
            kpPaletaKolorów.Dispose();
        }

        private void zmianaCzcionkiFormularzaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog kpOknoCzcionki = new FontDialog();
            kpOknoCzcionki.Font = this.Font;
            if (kpOknoCzcionki.ShowDialog() == DialogResult.OK)
                this.Font = kpOknoCzcionki.Font;
            kpOknoCzcionki.Dispose();
        }

        private void zmianaCzcionkiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (kpdgvTWS.Visible)
            {
                FontDialog kpOknoCzcionki = new FontDialog();
                kpOknoCzcionki.Font = kpdgvTWS.Font;
                if (kpOknoCzcionki.ShowDialog() == DialogResult.OK)
                    kpdgvTWS.Font = kpOknoCzcionki.Font;
                kpOknoCzcionki.Dispose();
            }
            else
                MessageBox.Show("UWAGA: Kontrolka DataGridView jest ukryta i zmiana jej czcionki nie została dokonana");
        }

        private void zmianaKoloruLiniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (kpdgvTWS.Visible)
            {
                ColorDialog kpPaletaKolorow = new ColorDialog();
                kpPaletaKolorow.Color = kpdgvTWS.GridColor;
                if (kpPaletaKolorow.ShowDialog() == DialogResult.OK)
                    kpdgvTWS.GridColor = kpPaletaKolorow.Color;
                kpPaletaKolorow.Dispose();
            }
            else
                MessageBox.Show("UWAGA: Kontrolka DataGridView jest ukryta i zmiana jej koloru lini nie została dokonana");
        }

        private void zmianaKoloruTłaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (kpdgvTWS.Visible)
            {
                ColorDialog kpPaletaKolorow = new ColorDialog();
                kpPaletaKolorow.Color = kpdgvTWS.BackgroundColor;
                if (kpPaletaKolorow.ShowDialog() == DialogResult.OK)
                    kpdgvTWS.BackgroundColor = kpPaletaKolorow.Color;
                kpPaletaKolorow.Dispose();
            }
            else
                MessageBox.Show("UWAGA: Kontrolka DataGridView jest ukryta i zmiana jej koloru nie została dokonana");
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            kpchtWykresSzeregu.Series[0].BorderWidth = 1;
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            kpchtWykresSzeregu.Series[0].BorderWidth = 2;
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            kpchtWykresSzeregu.Series[0].BorderWidth = 3;
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            kpchtWykresSzeregu.Series[0].BorderWidth = 4;
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            kpchtWykresSzeregu.Series[0].BorderWidth = 5;
        }

        private void wykresLiniowyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kpchtWykresSzeregu.Series[0].ChartType = SeriesChartType.Line;
        }

        private void wykresPunktowyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kpchtWykresSzeregu.Series[0].ChartType = SeriesChartType.Point;
        }

        private void wykresKolumnowyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kpchtWykresSzeregu.Series[0].ChartType = SeriesChartType.Column;
        }

        private void wykresSłupkowyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kpchtWykresSzeregu.Series[0].ChartType = SeriesChartType.Bar;
        }

        private void liniaKreskowaDashToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kpchtWykresSzeregu.Series[0].BorderDashStyle = ChartDashStyle.Dash;
        }

        private void liniaKreskowoKropkowaKropkowaDashDotDotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kpchtWykresSzeregu.Series[0].BorderDashStyle = ChartDashStyle.DashDotDot;
        }

        private void liniaKreskowoKropkowaDashDotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kpchtWykresSzeregu.Series[0].BorderDashStyle = ChartDashStyle.DashDot;
        }

        private void ciagłaSolidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kpchtWykresSzeregu.Series[0].BorderDashStyle = ChartDashStyle.Solid;
        }

        private void czcionkaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (kpchtWykresSzeregu.Visible)
            {
                FontDialog kpOknoCzcionki = new FontDialog();
                kpOknoCzcionki.Font = kpchtWykresSzeregu.Font;
                if (kpOknoCzcionki.ShowDialog() == DialogResult.OK)
                    kpchtWykresSzeregu.Font = kpOknoCzcionki.Font;
                kpOknoCzcionki.Dispose();
            }
            else
                MessageBox.Show("UWAGA: Kontrolka Chart jest ukryta i zmiana jej czcionki nie została dokonana");
        }

        private void kolorTłaToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (kpchtWykresSzeregu.Visible)
            {
                ColorDialog kpPaletaKolorow = new ColorDialog();
                kpPaletaKolorow.Color = kpchtWykresSzeregu.BackColor;
                if (kpPaletaKolorow.ShowDialog() == DialogResult.OK)
                    kpchtWykresSzeregu.BackColor = kpPaletaKolorow.Color;
                kpPaletaKolorow.Dispose();
            }
            else
                MessageBox.Show("UWAGA: Kontrolka Chart jest ukryta i zmiana jej koloru nie została dokonana");
        }

        private void kolorLiniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (kpchtWykresSzeregu.Visible)
            {
                ColorDialog kpPaletaKolorow = new ColorDialog();
                kpPaletaKolorow.Color = kpchtWykresSzeregu.Series[0].Color;
                if (kpPaletaKolorow.ShowDialog() == DialogResult.OK)
                    kpchtWykresSzeregu.Series[0].Color = kpPaletaKolorow.Color;
                kpPaletaKolorow.Dispose();
            }
            else
                MessageBox.Show("UWAGA: Kontrolka Chart jest ukryta i zmiana jej koloru nie została dokonana");
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void szereg_indywidualny_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult kpOknoMessage = MessageBox.Show("Czy napewno chcesz zamknąć ten formularz i 'przejść' do formularza glównego",
                this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (kpOknoMessage == DialogResult.Yes)
            {
                e.Cancel = false; 
                foreach (Form Formularz in Application.OpenForms)
                    if (Formularz.Name == "Kokpit")
                    {
                        this.Hide();
                        Formularz.Show();
                        return;
                    }
                KokpitProjektu2_pPlutka62026_Lab kpFormularzKokpitProjektuNr2 = new KokpitProjektu2_pPlutka62026_Lab();
                kpFormularzKokpitProjektuNr2.Show();
                this.Hide();
            }
            else
                e.Cancel = true;
        }

        private void kpbtnReset_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
    
}
