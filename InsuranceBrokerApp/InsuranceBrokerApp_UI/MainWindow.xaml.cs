using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Modele;
using StocareDate;

namespace InsuranceBrokerApp_UI
{
    public partial class MainWindow : Window
    {
        IStocareClienti admin;
        string modCurent = "Adaugare";
        public MainWindow()
        {
            InitializeComponent();
            admin = StocareFactory.GetStocare();

            AfiseazaClienti();
            IncarcaClientiCombo();
        }
        const int MAX_LUNGIME = 30;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Valideaza(out string mesaj))
            {

                string tipSelectat = "";

                if (rbRCA.IsChecked == true) tipSelectat = "RCA";
                else if (rbCASCO.IsChecked == true) tipSelectat = "CASCO";
                else if (rbLocuinta.IsChecked == true) tipSelectat = "Locuinta";
                else if (rbViata.IsChecked == true) tipSelectat = "Viata";

                if (tipSelectat == "")
                {
                    txtRezultat.Text = "Selecteaza tip polita!";
                    return;
                }
                OptiuniPolita optiuni = OptiuniPolita.None;

                if (chkUrgenta.IsChecked == true)
                    optiuni |= OptiuniPolita.Urgenta;

                if (chkSuport.IsChecked == true)
                    optiuni |= OptiuniPolita.Suport24_7;

                if (chkAsistenta.IsChecked == true)
                    optiuni |= OptiuniPolita.AsistentaRutiera;
                Polita p = new Polita
                {
                    Tip = (TipPolita)Enum.Parse(typeof(TipPolita), tipSelectat),
                    Optiuni = optiuni
                };

                Client c = new Client
                {
                    Nume = txtNume.Text,
                    Telefon = txtTelefon.Text,
                    CNP = txtCNP.Text
                };

                c.Polite.Add(p);

                bool adaugat = admin.AdaugaClient(c); 

                if (adaugat)
                {
                    txtRezultat.Text = "Client adaugat!";
                    AfiseazaClienti();
                    txtNume.Clear();
                    txtTelefon.Clear();
                    txtCNP.Clear();
                    
                }
                else
                {
                    txtRezultat.Text = "CNP sau telefon deja existent!";
                }
            }
            else
            {
                txtRezultat.Text = mesaj;
            }
        }
        bool Valideaza(out string mesaj)
        {
            mesaj = "";

            bool valid = true;

            
            txtNume.Background = System.Windows.Media.Brushes.White;
            txtTelefon.Background = System.Windows.Media.Brushes.White;

            if (string.IsNullOrWhiteSpace(txtNume.Text))
            {
                mesaj += "Numele este obligatoriu.\n";
                txtNume.Background = System.Windows.Media.Brushes.LightPink;
                valid = false;
            }
            else if (txtNume.Text.Length > MAX_LUNGIME)
            {
                mesaj += "Numele prea lung.\n";
                txtNume.Background = System.Windows.Media.Brushes.LightPink;
                valid = false;
            }

            if (string.IsNullOrWhiteSpace(txtTelefon.Text))
            {
                mesaj += "Telefonul este obligatoriu.\n";
                txtTelefon.Background = System.Windows.Media.Brushes.LightPink;
                valid = false;
            }

            return valid;
        }
        void AfiseazaClienti()
        {
            lstClienti.Items.Clear();

            foreach (var c in admin.GetAll())
            {
                string tipPolita = c.Polite.Count > 0 ? c.Polite[0].Tip.ToString() : "Fara polita";
                lstClienti.Items.Add($"{c.Id}. {c.Nume} - {c.Telefon} | {tipPolita}");
            }
        }
        private void Modifica_Click(object sender, RoutedEventArgs e)
        {
            if (lstClienti.SelectedItem == null)
            {
                MessageBox.Show("Selecteaza un client!");
                return;
            }

            Client client = cmbClienti.SelectedItem as Client;

            if (client == null)
            {
                MessageBox.Show("Selecteaza un client!");
                return;
            }

            int id = client.Id;
            string tipSelectat = "";
            OptiuniPolita optiuni = OptiuniPolita.None;
            if (rbRCA.IsChecked == true) tipSelectat = "RCA";
            else if (rbCASCO.IsChecked == true) tipSelectat = "CASCO";
            else if (rbLocuinta.IsChecked == true) tipSelectat = "Locuinta";
            else if (rbViata.IsChecked == true) tipSelectat = "Viata";

            if (tipSelectat == "")
            {
                MessageBox.Show("Selecteaza tip polita!");
                return;
            }
            if (chkUrgenta.IsChecked == true)
                optiuni |= OptiuniPolita.Urgenta;

            if (chkSuport.IsChecked == true)
                optiuni |= OptiuniPolita.Suport24_7;

            if (chkAsistenta.IsChecked == true)
                optiuni |= OptiuniPolita.AsistentaRutiera;
            Polita p = new Polita
            {
                Tip = (TipPolita)Enum.Parse(typeof(TipPolita), tipSelectat),
                Optiuni = optiuni
            };

            Client c = new Client
            {
                Id = id,
                Nume = txtNume.Text,
                Telefon = txtTelefon.Text,
                CNP = txtCNP.Text,
                Polite = new List<Polita> { p }
            };

            admin.ModificaClient(c);

            AfiseazaClienti();
            IncarcaClientiCombo();

            txtRezultat.Text = "Client modificat!";
        }
        private void lstClienti_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstClienti.SelectedItem == null)
                return;

            int id = int.Parse(lstClienti.SelectedItem.ToString().Split('.')[0]);

            var client = admin.GetAll().FirstOrDefault(c => c.Id == id);

            if (client != null)
            {
                txtNume.Text = client.Nume;
                txtTelefon.Text = client.Telefon;
                txtCNP.Text = client.CNP;

            }
            if (client.Polite.Count > 0)
            {
                var tip = client.Polite[0].Tip;

                rbRCA.IsChecked = tip == TipPolita.RCA;
                rbCASCO.IsChecked = tip == TipPolita.CASCO;
                rbLocuinta.IsChecked = tip == TipPolita.Locuinta;
                rbViata.IsChecked = tip == TipPolita.Viata;
            }
        }

        private void Cauta_Click(object sender, RoutedEventArgs e)
        {
            string nume = txtCauta.Text.Trim();

            if (string.IsNullOrEmpty(nume))
            {
                MessageBox.Show("Introdu un nume!");
                return;
            }

            var rezultate = admin.CautaDupaNume(nume);

            lstClienti.Items.Clear();

            if (rezultate.Count == 0)
            {
                lstClienti.Items.Add("Nu s-au gasit rezultate.");
                return;
            }

            foreach (var c in rezultate)
            {
                string tipPolita = c.Polite.Count > 0 ? c.Polite[0].Tip.ToString() : "Fara polita";
                lstClienti.Items.Add($"{c.Id}. {c.Nume} - {c.Telefon} | {tipPolita}");
            }
        }
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            txtCauta.Clear();
            AfiseazaClienti();
        }
        private void MenuAdauga_Click(object sender, RoutedEventArgs e)
        {
            modCurent = "Adaugare";
            txtRezultat.Text = "Mod: Adaugare";
        }

        private void MenuModifica_Click(object sender, RoutedEventArgs e)
        {
            modCurent = "Modificare";
            txtRezultat.Text = "Mod: Modificare";
        }

        private void MenuCauta_Click(object sender, RoutedEventArgs e)
        {
            txtRezultat.Text = "Introdu numele pentru cautare";
        }

        private void MenuLista_Click(object sender, RoutedEventArgs e)
        {
            AfiseazaClienti();
        }
        private void Executa_Click(object sender, RoutedEventArgs e)
        {
            if (modCurent == "Adaugare")
            {
                Button_Click(sender, e); // codul tău existent
            }
            else if (modCurent == "Modificare")
            {
                Modifica_Click(sender, e);
            }
            OptiuniPolita optiuni = OptiuniPolita.None;

            if (chkUrgenta.IsChecked == true)
                optiuni |= OptiuniPolita.Urgenta;

            if (chkSuport.IsChecked == true)
                optiuni |= OptiuniPolita.Suport24_7;

            if (chkAsistenta.IsChecked == true)
                optiuni |= OptiuniPolita.AsistentaRutiera;
        }
        private void MenuClienti_Click(object sender, RoutedEventArgs e)
        {
            mainMenu.Visibility = Visibility.Collapsed;

            panelHome.Visibility = Visibility.Collapsed;
            panelClienti.Visibility = Visibility.Visible;
            panelPolite.Visibility = Visibility.Collapsed;
        }

        private void MenuPolite_Click(object sender, RoutedEventArgs e)
        {
            mainMenu.Visibility = Visibility.Collapsed;

            panelHome.Visibility = Visibility.Collapsed;
            panelClienti.Visibility = Visibility.Collapsed;
            panelPolite.Visibility = Visibility.Visible;
        }

        private void MenuIesire_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
        private void AdaugaPolita_Click(object sender, RoutedEventArgs e)
        {

            Client client = cmbClienti.SelectedItem as Client;

            if (client == null)
            {
                MessageBox.Show("Selecteaza un client!");
                return;
            }

            string tipSelectat = "";

            if (rbRCA_P.IsChecked == true) tipSelectat = "RCA";
            else if (rbCASCO_P.IsChecked == true) tipSelectat = "CASCO";
            else if (rbLocuinta_P.IsChecked == true) tipSelectat = "Locuinta";
            else if (rbViata_P.IsChecked == true) tipSelectat = "Viata";

            if (string.IsNullOrEmpty(tipSelectat))
            {
                MessageBox.Show("Selecteaza tip polita!");
                return;
            }

            OptiuniPolita optiuni = OptiuniPolita.None;

            if (chkUrgenta_P.IsChecked == true)
                optiuni |= OptiuniPolita.Urgenta;

            if (chkSuport_P.IsChecked == true)
                optiuni |= OptiuniPolita.Suport24_7;

            if (chkAsistenta_P.IsChecked == true)
                optiuni |= OptiuniPolita.AsistentaRutiera;

            Polita p = new Polita
            {
                Tip = (TipPolita)Enum.Parse(typeof(TipPolita), tipSelectat),
                Optiuni = optiuni
            };

            if (client.Polite == null)
                client.Polite = new List<Polita>();

            client.Polite.Add(p);

            admin.ModificaClient(client);

            AfiseazaPolite(client);

            MessageBox.Show("Polita adaugata!");
        }
        void AfiseazaPolite(Client c)
        {
            lstPolite.Items.Clear();

            foreach (var p in c.Polite)
            {
                lstPolite.Items.Add($"{p.Tip} | {p.Optiuni}");
            }
        }
        private void cmbClienti_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Client client = cmbClienti.SelectedItem as Client;

            if (client == null)
                return;

            AfiseazaPolite(client);
        }
        void IncarcaClientiCombo()
        {
            cmbClienti.ItemsSource = null;
            cmbClienti.ItemsSource = admin.GetAll();
        }
        private void MenuHome_Click(object sender, RoutedEventArgs e)
        {
            mainMenu.Visibility = Visibility.Visible;

            panelHome.Visibility = Visibility.Visible;
            panelClienti.Visibility = Visibility.Collapsed;
            panelPolite.Visibility = Visibility.Collapsed;
        }

    }
}
