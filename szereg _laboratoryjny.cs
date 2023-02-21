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
    public partial class szereg__laboratoryjny : Form
    {
        //deklaracja stałych okreslajacych przedzial zbieznoci zeregu
        const float DgX = float.MinValue; //dolna granica
        const float GgX = float.MaxValue; //górna granica

        //deklaracja mzmiennych referencyjnych w tablicy dwuwymiarowej
        float[,] TWS; //tablica wartosci szeregu 
        

        public szereg__laboratoryjny() //konstruktor
        {
            InitializeComponent();
        }

        private void szereg__laboratoryjny_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult OknoMessage
               = MessageBox.Show("Czy chcesz rzeczywiście zamknąć ten formularz i zakonczyc dzialanie programu?", 
               this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
            //odczytanie wybranego przycisku
            if (OknoMessage == DialogResult.Yes)
            {
                //ptwierdzenie zamkniecia formularza
                e.Cancel = false;
                foreach(Form Formularz in Application.OpenForms)
                    if(Formularz.Name == "Kokpit")
                        {
                        this.Hide();
                        Formularz.Show();
                        return;
                    }
                KokpitProjektu2_pPlutka62026_Lab FormularzGłówny = new KokpitProjektu2_pPlutka62026_Lab();
                this.Hide();
                FormularzGłówny.Show();
            }
            else

                //anulowanie zdarzenia 'zamknij'
                e.Cancel = true;
        }

        private void dokladnoscobliczeneps_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ObliczWartośćSzeregu_Click(object sender, EventArgs e)
        {
            errorProvider1.Dispose();
            //1 pobranie danych wejsciowych (x i Eps) z kontrolek formularza 
            //deklaracja zmiennych dla przechowania pobranych danych float 
            float X, Eps;
            
            if (!PobranieDanych_X_Eps(out X, out Eps))
            {
                //był bład to przerywamy obsluge zdarzenia Click
                return;
            }
            //2 obliczenie wartosci szeregu
            //deklaracje zmiennych dla przechowania wyniku obliczen
            float Suma;
            ushort LicznikZsumowanychWyrazów;
            Suma = ObliczenieWartościSzeregu(X, Eps, out LicznikZsumowanychWyrazów);
            //3 wpisanie do odpowiednich kontrolek wyniku obliczen
            txtSumaSzeregu.Text = Suma.ToString();
            txtLicznikWyrazów.Text = LicznikZsumowanychWyrazów.ToString();
            //ustawienie stanu braku aktywnosci dla przycisku polecen: ObliczWartośćSzeregu
            btnObliczWartośćSzeregu.Enabled = false;

        }
        #region Deklaracje metod pomocniczych

        bool PobranieDanych_X_Eps(out float X, out float Eps)
        {
             //pomocnicze przypisanie  wrartosci parametrom wyjsciowym:X, Eps
             X = Eps = 0.0F;
            //pobranie wartosci zmiene=nej niezaleznej X
            if (!float.TryParse(txtX.Text, out X))
            {
                //byl blad
                errorProvider1.SetError(txtX, "Error: w zapisie wartosci zmiennej niezaleznej x wystapil niedozwolony znak");
                //przerwanie pobierania kolejnych danych
                return false;
            }
            //sprawdzenie czy pobrane x miesci sie w przedziale zbieznosci szeregu
            if ((X < DgX) || (X > GgX))
            {
                //jest blad
                errorProvider1.SetError(txtX, "Error: podana wartosc zmiennej niezaleznej X nie nalezy do przedzialu zbieznosci szeregu");
                //przerwanie pobierania kolejnych danych
                return false;

            }
            //ustawienie stanu braku aktywnosci dla kontrolki txtX
            txtX.Enabled = false;
            
            //pobranie dokladnosci obliczen
            if(!float.TryParse(txtEps.Text, out Eps))
            {
                //byl blad
                errorProvider1.SetError(txtEps, "Error: w zapisie dokladnosci obliczen Eps wystapil niedozwolony znak");
                //przerwanie metody ppobierania danych wyjsciowych
                return false;
            }
            //sprawdzenie tzw warunku wejsciowego dla eps
            if ((Eps <= 0.0F) || (Eps >= 1.0F))
            {
                errorProvider1.SetError(txtEps, "Error: podana dokladnosc niespelnia warunku wyjsciowego: 0.0 < Eps< 1.0");
                return false;
            }
            //ustawienie stanu braku aktywnosci dla kontrolki txtEp
            txtEps.Enabled = false;
            
            //zwrotne przekazanie info o poprawnosci pobrania danych
            return true;
        }



        float ObliczenieWartościSzeregu(float X, float Eps, out ushort n)
        {
            //pomocnicze wypisanie wartosci parametrowi wyjsciowemu n
            n = 0;
            //ustalenie warunkow bregowych
            float Suma = 0.0F;
            float w = 1.0F;
            do
            {
                Suma = Suma + w;
                n++;
                w = w * (-1) * X / n;

            } while(Math.Abs(w) > Eps);

            //zwrotne przekazanie wyniuku obliczen
            return Suma;


        }
        bool PobranieDanych_Xd_Xg_h_Eps(out float Xd, out float Xg, out float Eps, out float h)
        {
            //przypisanie domyslnych testowych parametrom wyjsciowym
            Xd = Xg = h = Eps = 0.0F;

            if (!float.TryParse(txtXd.Text, out Xd))
            {
                errorProvider1.SetError(txtXd, "ERROR: w zapisie Xd wystąpił niedozwolony znak!");
                return false;
            }
            //sprawdzenie czy Xd nalezy do przedzialu zbieznosci "mojego" szergu
            if ((Xd < DgX) || (Xd > GgX))
            {
                //jest blad
                errorProvider1.SetError(txtXd, "Error: podana wartosc zmiennej niezaleznej Xd nie nalezy do przedzialu zbieznosci szeregu");
                //przerwanie pobierania kolejnych danych
                return false;

            }
            if (!float.TryParse(txtXg.Text, out Xg))
            {
                errorProvider1.SetError(txtXg, "ERROR: w zapisie Xg wystąpił niedozwolony znak!");
                return false;
            }
            //sprawdzenie czy Xg nalezy do przedzialu zbieznosci "mojego" szergu
            if ((Xg < DgX) || (Xg > GgX))
            {
                //jest blad
                errorProvider1.SetError(txtXg, "Error: podana wartosc zmiennej niezaleznej Xg nie nalezy do przedzialu zbieznosci szeregu");
                //przerwanie pobierania kolejnych danych
                return false;

            }
            //sprawdzenie poprawnosci podania granic przedzialu Xd oraz Xg
            if (Xd > Xg)
            {
                errorProvider1.SetError(txtXg, "ERROR: granice przedziału Xd, Xg zpstały zapisane w odwrotnej kolejnoci(popraw to)");
                return false;
            }
            //tuttaj jest wszystko ok
            //ustawianie stanu brakuy aktywnosci  dla kontrolek txtXd txtXg
            txtXd.Enabled = false; txtXg.Enabled = false;
            //ppobranie h
            if (!float.TryParse(txtH.Text, out h))
            {
                //blad
                errorProvider1.SetError(txtH, "Error: w zapisie przyrostu h wystapił błąd");
                return false;

            }
            //sprawdznei tzw warunku wejsciowego 
            if ((h <= 0.0F) || (h >= (Xg - Xd)))
            {
                //blad
                errorProvider1.SetError(txtH, "Error: podana wartosc h nie spelnia warunku wejsciowego dla h: (h > 0) && (h < (Xg - Xd)");
                return false;
            }
            //ustawienie stanu braku aktywnosci dla txtH
            txtH.Enabled = false;

            //pobranie dokladnosci obiczen eps
            if (!float.TryParse(txtEps.Text, out Eps))
            {
                //blad
                errorProvider1.SetError(txtEps, "Error: w zapisie Eps wystapił błąd");
                return false;
            }
            //sprawdzenie warunku wyjsciowego Eps
             if((Eps <= 0.0F) || (Eps >= 1.0F))
            {
                //blad
                errorProvider1.SetError(txtEps, "Error: podana wartosc Eps nie spelnia warunku wejsciowego");
                return false;
            }
            //ustawienie stanu braku aktywnosci dla kontrolki txtEps
            txtEps.Enabled = false;

            //zwrotne przekazanie informascji(true) nie bylo bledu
            return true;
        }

        void TablicowanieWartosciSzeregu(float Xd, float Xg, float h, float Eps, out float[,] TWS)
        {
            /*TWS = null;*/ //top nie jest konieczne
            //wyznaczenie liczby wierszy egzemplarza tablicy TWS
            int n = (int)((Xg -Xd) / h +1);
            //utworzenie egzemplarza tablicy TWS
            TWS = new float[n, 3];
            //tablicowanie wartosci szeregu
            //wymagane deklaracje pomocnicze
            float X;
            int i; //numer podprzedziału dla przedzialu Xd,Xg
            ushort LicznikZsumowanychWyrazów;
            for(X = Xd, i = 0; i < TWS.GetLength(0); i++, X = Xd+i*h) //nie piszemy tak: X = X+h
            {
                //wpisywanie wyników tablicowanie=a do egzempllarza tablicy TWS
                TWS[i, 0] = X;
                TWS[i, 1] = ObliczenieWartościSzeregu(X, Eps, out LicznikZsumowanychWyrazów);
                TWS[i, 2] = LicznikZsumowanychWyrazów;
            }

        }
        void WpisanieWynikówDoKontrolkiDataGrindView(float[,] TWS, DataGridView dgvTWS)
        {
            //wyczyszczenie kontrolki dgvTWS
            dgvTWS.Rows.Clear();
            //wpisujemy dane z kontroli
            for (int i = 0; i < TWS.GetLength(0); i++)
            {
                dgvTWS.Rows.Add();
                //dodajemy do kontrolki DataGrindView nowy i pusty wiersz
                dgvTWS.Rows[i].Cells[0].Value = string.Format("{0:0.00}", TWS[i, 0]);
                dgvTWS.Rows[i].Cells[1].Value = string.Format("{0:0.0000}", TWS[i, 1]);
                dgvTWS.Rows[i].Cells[2].Value = string.Format("{0}", TWS[i, 2]);

            }
        }
        #endregion

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void btnWizualizacjaTabelaryczna_Click(object sender, EventArgs e)
        {
            //errorProvider1.Dispose();
            //1 pobieranie danych wejsciowych (eps, xd, xg,h)
            //deklaracja zmiennych
            // ! - operator negacji
            float Xd, Xg, Eps, h;
            if (!PobranieDanych_Xd_Xg_h_Eps(out Xd, out Xg, out Eps, out h))

                //blad
                return;
            //2stablicowanie szeregu w przedziale Xd Xg z przyrostem h
            //sprawdzenie czyz ostal juz utworony egzemplarz tablicy tws 
            if (TWS is null)
                //utworzenie tablicy tws i stablicowanie szeregu
                TablicowanieWartosciSzeregu(Xd, Xg, h, Eps, out TWS);

            //deklaracja tablicy dla przechowania stablicowanyc wartosci szeregu 
            //float[,] TWS; //Tablica wartosci szeregu
            ////wywolanie metody tablicowania wartosci szeregu
            //TablicowanieWartosciSzeregu(Xd, Xg, h, Eps, out TWS);

            //3 przepisanie danych z tablicy TWS do kontrolki DataGridView
            //tutaj wpiszemy odpowiednia sekwencje instrukcji
            WpisanieWynikówDoKontrolkiDataGrindView(TWS, dgvTWS);

            //odsloniecie kontrolki DataGrindView
            dgvTWS.Visible = true;

            //ustawienie stanu braku aktywnosci dlA przycisku polecen btnWizualizacjaTabelaryczna
            btnWizualizacjaTabelaryczna.Enabled = false;


        }

        private void txtX_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnWizualizacjaGraficzna_Click(object sender, EventArgs e)
        {
            {// "zgaszenie" kontrolki errorProvider, ktora mogla zostac zapalona podczas pobierania danych
                errorProvider1.Dispose();
                // 1) Pobranie danych wejsciowych z formularza
                // deklaracja zmiennych dla przechowania pobranych danych wejsciowych

                float Xd, Xg, h, Eps;
                if (!PobranieDanych_Xd_Xg_h_Eps(out Xd, out Xg, out h, out Eps))
                    // byl blad, to przerywamy dalsza oblsuge zdarzenia Click przycisku btnWizualizacjaGraficzna
                    return;
                // 2) tablicowanie wartosci szeregu
                // sprawdzenie czy tablica TWS była już utworzona
                if (TWS is null) // operator 'is' umożliwia sprawdzenie czy w czasie wykonywania programu wartosc zmiennej TWS jest zgodna z wartoscia 'null' (pusty wskaźnik)
                                 // egzemplarz tablicy TWS nie został jeszcze utworzony i szereg nie został stablicowany, to musimy go teraz utworzyć i stablicować wartości szeregu
                    TablicowanieWartosciSzeregu(Xd, Xg, h, Eps, out TWS);
                // 3) przepisanie danych z tablicy TWS do kontrolki Chart umieszczonej na formularzu
                WpisanieWynikowDoKontrolkiChart(TWS, chtWykresSzeregu);
                // 4) odsłonięcie i ukrycie kontrolek oraz ustawienie stanu braku aktywnosci kontrolek
                // ukrycie kontrolki DataGridView
                dgvTWS.Visible = false;
                // odsłonięcie kontrolki Chart
                chtWykresSzeregu.Visible = true;
                // ustawienie stanu braku aktywnosci przyciusku polecen Wizualizacja graficzna zmian wartosci szeregu
                btnWizualizacjaGraficzna.Enabled = false;
            }
            void WpisanieWynikowDoKontrolkiChart(float[,] TWS, Chart chtWykresSzeregu)
            {// 1. formatowanie (ustawienei atrybutów) kontrolki Chart,
             // obramowanie kontrlki Chart
             //chartWykresSzeregu.BorderlineWidth = 2;
             //chartWykresSzeregu.BorderlineColor = Color.Red;
             //chartWykresSzeregu.BorderlineDashStyle = ChartDashStyle.DashDotDot;
             // skonfigurowanie kontrolki Chart
             // ustalenie tytułu wykresu
                chtWykresSzeregu.Titles.Add("Wykres zmian wartości szeregu S(X)");
                // umieszczenie legendy pod wykresem
                chtWykresSzeregu.Legends.FindByName("Legend1").Docking = Docking.Bottom;
                // ustawienie koloru tła
                //chartWykresSzeregu.BackColor = Color.LightSkyBlue;
                chtWykresSzeregu.ChartAreas[0].AxisX.Title = "Wartości X";
                chtWykresSzeregu.ChartAreas[0].AxisY.Title = "Wartości S(X)";
                // sformatowanie opisu osi X (kontrolki Chart)
                chtWykresSzeregu.ChartAreas[0].AxisX.LabelStyle.Format = "{0:f2}";
                // sformatowanie opisu osi Y (kontrolki Chart)
                chtWykresSzeregu.ChartAreas[0].AxisY.LabelStyle.Format = "{0:f2}";
                // 2. formatowanie serii danych dodanej do kontrolki Chart,
                // "wyzerowanie" serii danych kontrolki Chart
                chtWykresSzeregu.Series.Clear();
                // dodanie nowej serii danych
                chtWykresSzeregu.Series.Add("Seria 0");
                // ustalenie nazw osi układu współrzędnych
                chtWykresSzeregu.Series[0].XValueMember = "X";
                chtWykresSzeregu.Series[0].YValueMembers = "Y";
                // ustalenie widoczności legendy
                chtWykresSzeregu.Series[0].IsVisibleInLegend = true;
                // ustalenie nazwy legendy (opisu linii wykresu)
                chtWykresSzeregu.Series[0].Name = "Wartość szeregu potęgowego S(X)"; // legenda
                                                                                     // ustalenie typu wykresu
                /*chtWykresSzeregu.Series[0].ChartType = SeriesChartType.Line;*/ // liniowy
                //                                                               // ustawienie koloru linii
                //chartWykresSzeregu.Series[0].Color = Color.Black;
                //// ustalenie stylu linii
                //chartWykresSzeregu.Series[0].BorderDashStyle = ChartDashStyle.Solid;
                //// ustalenie grubości linii
                //chartWykresSzeregu.Series[0].BorderWidth = 1;
                /* 3.wpisanie współrzędnych (X oraz Y) linii wykresu szeregu (na podstawie danych zapisanych w tablicy TWS) */
                // dodanie do serii danych (kontrolki Chart) współrzędnych punktów wykresu (wartości X oraz Wartości Y, czyli wartości szeregu: S (X))
                for (int i = 0; i < TWS.GetLength(0); i++)
                    chtWykresSzeregu.Series[0].Points.AddXY(TWS[i, 0], TWS[i, 1]);
            }
        }

        private void plikToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void restartProgramuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void zmianaKoloruTłaFormularzaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //utworzenie egzemplarza palety koloru
            ColorDialog PaletaKolorów = new ColorDialog();
            //zaznaczenie w palecie kolorow biezacego koloru formularza
            PaletaKolorów.Color = this.BackColor;
            //wyswietlenie  palrty kolorów i sprawdzenie dokonanego wyboru przez uzytkownika programu
            if (PaletaKolorów.ShowDialog() == DialogResult.OK)
                //dokonujemy zmiany koloru tla formularza zgodnie z zyczeniem uzytkownika 
                this.BackColor = PaletaKolorów.Color;
            //zwolnienie niepotzrebnego juz azsobu, czyli palety kolorow
            PaletaKolorów.Dispose();
        }

        private void zmianaFontuFormularzaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //utworzenie okna dialogowego OknoCzcionki
            FontDialog OknoCzcionki = new FontDialog();
            //zaznaczenie atrybutow biezacej czcionki formularza
            OknoCzcionki.Font = this.Font;
            //wyswietlenie oknoCzcionki i sprawdzenie dokonanego wyboru przez uzytkownika programu
            if (OknoCzcionki.ShowDialog() == DialogResult.OK)
                this.Font = OknoCzcionki.Font;
            //zwolnienie niepotzrebnego juz azsobu, czyli palety kolorow
            OknoCzcionki.Dispose();
        }

        private void zapisanieTablicyTWSWPlikuToolStripMenuItem_Click(object sender, EventArgs e)
        {// zgaszenie kontrolki errorProvider, która mogła być zapalona
            errorProvider1.Dispose();
            // sprawdzenie, czy egzemplarz tablicy TWS został utworzony i szereg został stablicowany
            if (TWS is null)
            {// w takim razie musimy go utworzyć i stablicować szereg
                // 1) Pobranie danych z formularza
                // deklaracja zmiennych dla przechowania pobranych danych wejściowych z formularza
                float Xd, Xg, h, Eps;
                if (!PobranieDanych_Xd_Xg_h_Eps(out Xd, out Xg, out h, out Eps))
                {// wyświetlenie okna MessageBox dla poinformowania użytkownika co powinien zrobić
                    MessageBox.Show("UWAGA: podczas poberania danych wejściowych z kontrolek TextBox został wykryty niedozwoony znak w ich zapisie!" +
                        " Popraw tą danę i ponownie wybierz (kliknięciem) polecenie z menu Plik");
                    // przerwanie obsługi polecenia z menu Plik
                    return;
                }
                // tutaj wszystko jest OK (z danymi wejściowymi)
                // tablicujemy szreg
                TablicowanieWartosciSzeregu(Xd, Xg, h, Eps, out TWS);
            }
            // egzemplarz tablicy TWS został utworzony i szereg jest tablicowany
            // zapisanie tablicy TWS w pliku
            // utworzenie egzemplarza okna dialogowego dla wyboru pliku do zapisu
            SaveFileDialog OknoWyboruPlikuDoZapisu = new SaveFileDialog();
            // ustawienie wartości atrybutów OknaWyboruPlikuDoZapisu
            OknoWyboruPlikuDoZapisu.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            // ustawienie filtru domyślnego
            OknoWyboruPlikuDoZapisu.FilterIndex = 1; // filtr: *.txt
            // przywrócenie bieżącego foldera
            OknoWyboruPlikuDoZapisu.RestoreDirectory = true;
            // ustawienie dysku (i folderu) do zapisu pliku
            OknoWyboruPlikuDoZapisu.InitialDirectory = "E:\\"; // folder główny dysku E:
            // ustalenie tytułów okna dialogowego OknoWyboruPlikuDoZapisu
            OknoWyboruPlikuDoZapisu.Title = "Wybór pliku do zapisu tablicy TWS (Tablica Wartości Szeregu)";
            // wyświetlenie okna dialogowego i "odczytanie" dokonanego wyboru przez Użytkownika
            if (OknoWyboruPlikuDoZapisu.ShowDialog() == DialogResult.OK)
            {// utworzenie egzemplarza opisu pliku znakowego do zapisu
                System.IO.StreamWriter PlikZnakowy = new System.IO.StreamWriter(OknoWyboruPlikuDoZapisu.FileName);
                try
                {
                    for (int i = 0; i < TWS.GetLength(0); i++)
                    {
                        PlikZnakowy.Write(TWS[i, 0].ToString()); // wartość x
                        PlikZnakowy.Write(";"); // ';' będzie oddzielał dane w wierszu pliku znakowego
                        PlikZnakowy.Write(TWS[i, 1].ToString()); // wartość szeregu S(X)
                        PlikZnakowy.Write(";"); // ';' będzie oddzielał dane w wierszu pliku znakowego
                        PlikZnakowy.WriteLine(TWS[i, 2].ToString()); // wartość licznika zsumowanych wyrazów
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR: podczas zapisywania tablicy TWS w pliku wystąpił błąd: " + ex.Message);
                }
                finally
                {// zamknięcie pliku
                    PlikZnakowy.Close();
                }
            }
            else
                MessageBox.Show("UWAGA: nie dokonano wyboru pliku i polecenia zapisu tablicy TWS i nie zostało zrealizowane");
        }

        
        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void exitZFormularzaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // wyświetlenie okna dialogowego z pytaniem: "Czy rzeczywiście ..." z ikoną pytajnika i trzema przyciskami Yes, No i Cancel
            DialogResult PytanieDoUżytkownika = MessageBox.Show("Czy napewno chcesz zamknąć formularz (co może skutkować utratą danych zapisanych na formularzu?)", 
                this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
            // rozpoznanie odpowiedzi użytkownika aplikacji i wykonanie stosownego działania; formularz może być zamknięty gdy użytkownik wybrał (kliknął) przycisk poleceń Tak
            if (PytanieDoUżytkownika == DialogResult.Yes)
                // formularz musi być zamknięty
                Close();
        }

        private void odczytanieTablicyTWSZPlikuToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            // "zgaszenie" kontrolki errorProvider, która mogła zostać zapalona podczas pobierania danych
            errorProvider1.Dispose();
            // sprawdzenie, czy zmienna referenycyjna ma wartość null (tzw. Pusta refencja do egzemplarza tablicy)
            if (!(TWS is null))
            {// poinformowania Użytkownika, że egzemplarz tablicy TWS już jest utworzony i czy ma być skasowany dla wczytania elementów tablicy TWS z pliku ma być kontynuowane
                DialogResult OknoMessage = MessageBox.Show("UWAGA: egzemplarz tablicy TWS już istnieje \r\nCzy bieżący egzemplarz tablicy TWS ma być skasowany i w jego miejsce ma być utworzony nowy egzemplarz, do którego mają zostać 'wczytane' elementy TWS z pliku?" +
                    "\r\n - kliknij przycisk poleceń 'Tak' dla potwierdzenia wczytania elementów tablicy TWS z pliku" +
                    "\r\n - kliknij przycisk poleceń 'Nie' dla skasowania polecenia wczytania elementów tablicy TWS z pliku", "Okno ostrzeżenia", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                // rozpoznanie wybranego przycisku poleceń w oknie dialogowym 'MessageBox'
                if (OknoMessage == DialogResult.Yes)
                    TWS = null; /* przypisanie wartości 'null' zmiennej referencyjnej jest sygnałem dla CLR (Common Language Runtime: wspólne środowisko uruchomieniowe dla platformy .NET)
                                 że egzemplarz tablicy TWS jest już niepotrzebny i może być zwolniony automatycznie przez GC(Garbage Collector): tzw. "odśmiecacz", którego zadaniem jest usuwanie 'martwych' (niepotrzebnych) egzemplarzy tablicy i obiektów */
                else
                {// czyli: if (OknoMessage == DialogResult.NO)
                    MessageBox.Show("Polecenie odczytania (pobrania) elementów tablicy TWS z pliku zostało anulowane (skasowane!)");
                    // przerwanie obsługi polecenia odczytania (pobrania) elementów tablicy TWS z pliku
                    return;
                }
            }
            // utworzenie egzemplarza okna dialogowego dla wyboru (lub utworzenia) pliku do odczytania
            OpenFileDialog OknoWyboruPlikuDoOdczytu = new OpenFileDialog();
            // ustawienie filtru dla wyboru plików do wyświetlenia w oknie dialogowym
            OknoWyboruPlikuDoOdczytu.Filter = "txtfiles (*.txt)|*.txt|All files(*.*)|*.*";
            // ustawienie filtru domyślnego
            OknoWyboruPlikuDoOdczytu.FilterIndex = 1; // czyli filtru: *.txt
            // przywrócenie bieżącego folderu po zamknięciu okna dialogowego wyboru plików
            OknoWyboruPlikuDoOdczytu.RestoreDirectory = true;
            // ustawienie domyślnego dysku do zapisu
            OknoWyboruPlikuDoOdczytu.InitialDirectory = "H:\\";
            // ustalenie tytułu okna dialogowego wyboru pliku
            OknoWyboruPlikuDoOdczytu.Title = "Wybór pliku do odczytu TWS (Tablicy Wartości Szeregu)";
            // wyświetlenie okna dialogowego i sprawdzenie, czy Użytkownik wybrał plik do odczytu
            if (OknoWyboruPlikuDoOdczytu.ShowDialog() == DialogResult.OK)
            {// wczytanie wierzy pliku i ich wpisanie do tablicy TWS zreazlizujemy w kilku "krokach", które ponumerujemy!
                // deklaracje pomocnicze
                string WierszDanych; // dla przechowania wiersza danych (łańcucha znaków) wczytanie z pliku znakowego
                string[] DaneWiersza; // dla podzielenia wiersza danych na pojedynicze dane (liczby) 
                ushort LicznikWierszy; // licznik wiersza pliku znakowego (zawierającego zapisane wiersze tablicy TWS)
                // Krok 1: Utworzenie i otwarcie egzemplarza pliku znakowego jako stumienia znaków, co umożliwi wykonywanie na nim operacji "podobnych" do operacji wykonywanych na oknie konsoli Console...
                System.IO.StreamReader PlikZnakowy = new System.IO.StreamReader(OknoWyboruPlikuDoOdczytu.FileName);
                // lub: new System.IO.StreamReader(OknoOdczytuPliku.OpenFile());
                // przy tworzeniu egzemplarza (będą w nim zapisane dane opisujące atrybuty otwieranego pliku znakowego) klasy StreamReader, musimy podać nazwę wybranego już wcześniej pliku do odczytu

                // Krok 2: policzenie liczby wierszy w pliku: PlikZnakowy, Zliczający wczytane wiersze pliku aż do napotkania znacznika końcu pliku: null
                LicznikWierszy = 0;
                while (!((WierszDanych = PlikZnakowy.ReadLine()) is null))
                    LicznikWierszy++;

                // Krok 3: zamknięcie pliku (ale go nie zwalniamy, gdyż będziemy "zaraz" wczytywali wiersze tablicy TWS)
                PlikZnakowy.Close();

                // Krok 4: utworzenie egzemplarza tablicy TWS (wiemy już ile musi mieć wierszy!!!)
                TWS = new float[LicznikWierszy, 3];

                // Krok 5: powtórne otwarcie pliku znakowego do odczytu
                PlikZnakowy = new System.IO.StreamReader(OknoWyboruPlikuDoOdczytu.FileName);

                // Krok 6: odczytanie pliku znakowego: "wiersz po wierszu"
                try
                // instrukcja 'try' umożliwa zapis sekwencji instukcji w której mogą wystąpić wyjątki (błędy!) i może zawierać jedną lub więcej sekcji 'catch', w których zapisujemy działania w "odpowiedzi" na "zgłoszony" wyjątek
                {// ustalenie warunków brzegowych
                    int NrWiersza = 0;
                    // wczytanie kolejnych wierszy pliku znakowego, aż do napotkania znacznika końca pliku: null
                    while (!((WierszDanych = PlikZnakowy.ReadLine()) is null))
                    {// wczytanie wiersza pliku znakowego i jego podział na "części", które są rozdzielone seperatorami (znakiem) ';'
                        DaneWiersza = WierszDanych.Split(';');
                        // usunięcie 'spacji' z poszczególnych "części" wczytanego wiersza danych
                        DaneWiersza[0].Trim(); DaneWiersza[1].Trim(); DaneWiersza[2].Trim();
                        // wpisanie danych ("części" zapisanych znakowo) do tablicy TWS, co wymaga ich konwersji na wartość typu 'float'
                        TWS[NrWiersza, 0] = float.Parse(DaneWiersza[0]);
                        TWS[NrWiersza, 1] = float.Parse(DaneWiersza[1]);
                        TWS[NrWiersza, 2] = float.Parse(DaneWiersza[2]);
                        NrWiersza++;
                    }
                    /* Klasa String udostępnia metodę Split ('separator'), która zwraca tablicę łańcuchów znaków DaneWiersza (jako wynik swojego działania) utworzoną
                     * z podzielenia WierszaDanych na pojedyncze "części" każdorazowo, gdy w WierszuDanych wystąpi podany 'separator' (znak: ";") danych. Gdy parametr 'separator' metodyt
                     * Split(...) jest listą separatorów (podanych jako tablica typu char: WierszDanych.Split(new char[]{';', ':', '|'}) ), to zwracna jest tablica (DaneWiersza)
                     * łańcucha znaków (typu: string), w której znajdują się wydzielone podłańcuchy znaków (substringi) rodzielone od siebie jednym z podanych separatorów */

                    // Krok 7: przepisanie danych z tablicy TWS do kontrolki DataGridView umieszczonej na formularzu, co umożliwi nam wcześniej zaprojektowana (przy obsłudze przycisku poleceń: 'Wizualizacja tabelaryczna zmian wartości szeregu' metoda: WpisanieWynikówDoKontrolkiDataGridView
                    WpisanieWynikówDoKontrolkiDataGrindView(TWS, dgvTWS);
                    // wczytanie wiersza tablicy TWS wpisujemy do kontrolki DataGridView dla "wzrokowego" potwierdzenia wykonania operacji odczytania wiersz danych tablicy TWS z pliku

                    // Krok 8: odsłonięcie i ukrycie kontrolek odpowiedznich kontrolek oraz ustawienie stanu braku aktywności kontrolek
                    // ukrycie kontrolki Chart
                    chtWykresSzeregu.Visible = false;
                    // odsłonięcie kontrolki DataGridView
                    dgvTWS.Visible = true;
                }
                catch (Exception ex) // obsługi wyjątku (wykrytego błędu) podczas wykonywania operacji na pliku który został "zgłoszony" w sekcji try
                {
                    MessageBox.Show("ERROR: błąd operacji (działania) na pliku (wyświetlony komunikat): ---> " + ex.Message);
                }
                finally
                /* sekcja 'finally' jest stosowana do zapisu sekwencji (jednego lub więcej) instrukcji kończących wykonywanie działań na pliku (niezależnie od tego, czy wyjątek został "zgłoszony"
                 czy nie!
                dla przykładu, jeżeli został otwarty plik musi zostać on zamknięty i zwolniony, niezależnie od faktu, czy błąd został "zgłoszony" czy nie*/
                {// zamknięcie pliku i zwolnienie zasobów z nim związanych
                    PlikZnakowy.Close(); // zamknięcie pliku
                    PlikZnakowy.Dispose(); // zwolnienie pliku
                }
            }
            else
                MessageBox.Show("Plik do odczytu tablicy TWS nie został wybrany i obsługa polecenia: 'Odczytanie stablicowanego szeregu z pliku' (z menu poziomu Plik) nie może być zrealizowana");
        }

        private void zmianaKoloruCzcionkiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void zmianaKoloruToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // sprawdzenie czy kontrolka DataGridView jest odsłonięta 
            if (dgvTWS.Visible)
            {// utworzenie paletyKolorow
                ColorDialog PaletaKolorow = new ColorDialog();
                // "zaznaczenie" w palecice kolorow, koloru lini siatki kontrolki DataGridView
                PaletaKolorow.Color = dgvTWS.GridColor;
                // wyświetlenie okna dialogowego wyboru koloru dla siatki kontrolki DataGridView i odczytanie dokonanego wyboru przez użytkownika
                if (PaletaKolorow.ShowDialog() == DialogResult.OK)
                    // Dokonanie zmiany koloru siatki kontorlki DataGridView
                    dgvTWS.GridColor = PaletaKolorow.Color;
                // zwolnienie zasobów
                PaletaKolorow.Dispose();
            }
            else
                MessageBox.Show("UWAGA: Kontrolka DataGridView jest ukryta i zmiana jej koloru lini nie została dokonana");
        }

        private void zmianaCzcionkiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // sprawdzenie czy kontrolka DataGridView jest odsłonięta 
            if (dgvTWS.Visible)
            {// utworzenie okna dialogowego
                FontDialog OknoCzcionki = new FontDialog();
                // ustawienie w OknoCzcionki akutalnego Fontu dla kontrolki DataGridView
                OknoCzcionki.Font = dgvTWS.Font;
                if (OknoCzcionki.ShowDialog() == DialogResult.OK)
                    // dokonanie zmiany Fontu dla kontrolki DataGridView
                    dgvTWS.Font = OknoCzcionki.Font;
                // zwolnienie zasobów, który nie jest już potrzebny
                OknoCzcionki.Dispose();
            }
            else
                MessageBox.Show("UWAGA: Kontrolka DataGridView jest ukryta i zmiana jej czcionki nie została dokonana");
        }

        private void zmianaTypuWykresuToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void zmianaKoloruCzcionkiToolStripMenuItem1_Click(object sender, EventArgs e)
        {
        }

        private void zmianaStyluLiniToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void zmianaGrubościLiniToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void atrybutyKontrolkiDataGrindViewToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void zmianaKoloruTłaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // sprawdzenie czy kontrolka DataGridView jest odsłonięta 
            if (dgvTWS.Visible)
            {// utworzenie paletyKolorow
                ColorDialog PaletaKolorow = new ColorDialog();
                // "zaznaczenie" w palecice kolorow, koloru lini siatki kontrolki DataGridView
                PaletaKolorow.Color = dgvTWS.BackgroundColor;
                // wyświetlenie okna dialogowego wyboru koloru dla siatki kontrolki DataGridView i odczytanie dokonanego wyboru przez użytkownika
                if (PaletaKolorow.ShowDialog() == DialogResult.OK)
                    // Dokonanie zmiany koloru siatki kontorlki DataGridView
                    dgvTWS.BackgroundColor = PaletaKolorow.Color;
                // zwolnienie zasobów
                PaletaKolorow.Dispose();
            }
            else
                MessageBox.Show("UWAGA: Kontrolka DataGridView jest ukryta i zmiana jej koloru nie została dokonana");
        }

        private void czcionkaToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // sprawdzenie czy kontrolka DataGridView jest odsłonięta 
            if (chtWykresSzeregu.Visible)
            {// utworzenie okna dialogowego
                FontDialog OknoCzcionki = new FontDialog();
                // ustawienie w OknoCzcionki akutalnego Fontu dla kontrolki DataGridView
                OknoCzcionki.Font =  chtWykresSzeregu.Font;
                if (OknoCzcionki.ShowDialog() == DialogResult.OK)
                    // dokonanie zmiany Fontu dla kontrolki DataGridView
                    chtWykresSzeregu.Font = OknoCzcionki.Font;
                // zwolnienie zasobów, który nie jest już potrzebny
                OknoCzcionki.Dispose();
            }
            else
                MessageBox.Show("UWAGA: Kontrolka Chart jest ukryta i zmiana jej czcionki nie została dokonana");
        }

        private void kolorTłaToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // sprawdzenie czy kontrolka DataGridView jest odsłonięta 
            if (chtWykresSzeregu.Visible)
            {// utworzenie paletyKolorow
                ColorDialog PaletaKolorow = new ColorDialog();
                // "zaznaczenie" w palecice kolorow, koloru lini siatki kontrolki DataGridView
                PaletaKolorow.Color = chtWykresSzeregu.BackColor;
                // wyświetlenie okna dialogowego wyboru koloru dla siatki kontrolki DataGridView i odczytanie dokonanego wyboru przez użytkownika
                if (PaletaKolorow.ShowDialog() == DialogResult.OK)
                    // Dokonanie zmiany koloru siatki kontorlki DataGridView
                    chtWykresSzeregu.BackColor = PaletaKolorow.Color;
                // zwolnienie zasobów
                PaletaKolorow.Dispose();
            }
            else
                MessageBox.Show("UWAGA: Kontrolka Chart jest ukryta i zmiana jej koloru nie została dokonana");
        }

        private void liniaKreskowaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chtWykresSzeregu.Series[0].BorderDashStyle = ChartDashStyle.Dash;
        }

        private void kolorCzcionkiToolStripMenuItem_Click(object sender, EventArgs e)
        {


            // sprawdzenie czy kontrolka DataGridView jest odsłonięta 
            if (chtWykresSzeregu.Visible)
            {// utworzenie paletyKolorow
                ColorDialog PaletaKolorow = new ColorDialog();
                // "zaznaczenie" w palecice kolorow, koloru lini siatki kontrolki DataGridView
                PaletaKolorow.Color = chtWykresSzeregu.ForeColor;
                // wyświetlenie okna dialogowego wyboru koloru dla siatki kontrolki DataGridView i odczytanie dokonanego wyboru przez użytkownika
                if (PaletaKolorow.ShowDialog() == DialogResult.OK)
                    // Dokonanie zmiany koloru siatki kontorlki DataGridView
                    chtWykresSzeregu.ForeColor = PaletaKolorow.Color;
                // zwolnienie zasobów
                PaletaKolorow.Dispose();
            }
            else
                MessageBox.Show("UWAGA: Kontrolka Chart jest ukryta i zmiana jej koloru nie została dokonana");
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            chtWykresSzeregu.Series[0].BorderWidth = 1;
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            chtWykresSzeregu.Series[0].BorderWidth = 2;
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            chtWykresSzeregu.Series[0].BorderWidth = 3;
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            chtWykresSzeregu.Series[0].BorderWidth = 4;
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            chtWykresSzeregu.Series[0].BorderWidth = 5;
        }

        private void liniaKreskowoKropkowaKropkowaDashDotDotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chtWykresSzeregu.Series[0].BorderDashStyle = ChartDashStyle.DashDotDot;
        }

        private void liniaKreskowoKropkowaDashDotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chtWykresSzeregu.Series[0].BorderDashStyle = ChartDashStyle.DashDot;
        }

        private void liniaKropkowaDotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chtWykresSzeregu.Series[0].BorderDashStyle = ChartDashStyle.Dot;
        }

        private void ciagłaSolidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chtWykresSzeregu.Series[0].BorderDashStyle = ChartDashStyle.Solid;
        }

        private void wykresLiniowyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chtWykresSzeregu.Series[0].ChartType = SeriesChartType.Line;
        }

        private void kolorLiniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //chatWykresSzeregu.Series[0].Color = Color.Black;
            // sprawdzenie czy kontrolka DataGridView jest odsłonięta 
            if (chtWykresSzeregu.Visible)
            {// utworzenie paletyKolorow
                ColorDialog PaletaKolorow = new ColorDialog();
                // "zaznaczenie" w palecice kolorow, koloru lini siatki kontrolki DataGridView
                PaletaKolorow.Color = chtWykresSzeregu.Series[0].Color;
                // wyświetlenie okna dialogowego wyboru koloru dla siatki kontrolki DataGridView i odczytanie dokonanego wyboru przez użytkownika
                if (PaletaKolorow.ShowDialog() == DialogResult.OK)
                    // Dokonanie zmiany koloru siatki kontorlki DataGridView
                    chtWykresSzeregu.Series[0].Color = PaletaKolorow.Color;
                // zwolnienie zasobów
                PaletaKolorow.Dispose();
            }
            else
                MessageBox.Show("UWAGA: Kontrolka Chart jest ukryta i zmiana jej koloru nie została dokonana");
        }

        private void wykresPunktowyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chtWykresSzeregu.Series[0].ChartType = SeriesChartType.Point;
        }

        private void wykresKolumnowyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chtWykresSzeregu.Series[0].ChartType = SeriesChartType.Column;
        }

        private void wykresSłupkowyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chtWykresSzeregu.Series[0].ChartType = SeriesChartType.Bar;
        }
    }
         
    
}
