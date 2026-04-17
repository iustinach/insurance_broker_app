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
        public MainWindow()
        {
            InitializeComponent();
            admin = StocareFactory.GetStocare();

            AfiseazaClienti(); 
        }
        const int MAX_LUNGIME = 30;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Valideaza(out string mesaj))
            {
                if (cmbTipPolita.SelectedItem == null)
                {
                    txtRezultat.Text = "Selecteaza tip polita!";
                    return;
                }

                var tipSelectat = (cmbTipPolita.SelectedItem as ComboBoxItem)?.Content.ToString();

                Polita p = new Polita
                {
                    Tip = (TipPolita)Enum.Parse(typeof(TipPolita), tipSelectat)
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
                    cmbTipPolita.SelectedIndex = -1;
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
        private void Sterge_Click(object sender, RoutedEventArgs e)
        {
            if (lstClienti.SelectedItem == null)
            {
                MessageBox.Show("Selecteaza un client!");
                return;
            }

            string selectat = lstClienti.SelectedItem.ToString();

            
            int id = int.Parse(selectat.Split('.')[0]);

            admin.StergeClient(id);

            AfiseazaClienti();

            MessageBox.Show("Client sters!");
        }
    }
}
