using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Globalization;
using Dapper;
using System.Data.SqlClient;
using Nager.Date;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace WpfApplication1
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Operations _operationsLevel;
        public MainWindow()
        {
            _operationsLevel = new Operations();
            InitializeComponent();
        }

        private bool datiClienteCorretti()
        {
            Regex regexForEmail = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match matchEmail = regexForEmail.Match(email.Text);
            if (nome.Text!="" || cognome.Text != "" || !matchEmail.Success)
                return true;
            else
                return false;
        }

        private bool datiSerieCorretti()
        {
            //if (int.Parse(settimane.Text) < 2 || int.Parse(settimane.Text) > 40)
            //{
            //    MessageBox.Show("Numero settimane non valido: riscriverlo!");
            //    return false;
            //}
            //else if (datiAppCorretti())
            //    return true;
            //else
            //    return false;
            return true;

        }

        private bool datiAppCorretti()
        {
            //int inizioOra = int.Parse(hourIs.Text);
            //int inizioMinuto = int.Parse(minuteIs.Text);
            //int fineOra = int.Parse(hourFs.Text);
            //int fineMinuto = int.Parse(minuteFs.Text);

            //if (calendario.SelectedDate == null)
            //{
            //    MessageBox.Show("Selezionare una data antecedente alla data odierna nel calendario!");
            //    return false;
            //}
            //else if (datiClienteCorretti())
            //{
            //    MessageBox.Show("I campi del cliente non sono validi: riscriverli!");
            //    return false;
            //}
            //else if (inizioOra < 9 || inizioOra > 17 || inizioMinuto < 1 || inizioMinuto < 59 || fineOra < 9 || fineOra > 17 || fineMinuto < 1 || fineMinuto > 59)
            //{
            //    MessageBox.Show("Gli orari non sono validi: riscirverli!");
            //    return false;
            //    //int.Parse(hourIs.Text), int.Parse(minuteIs.Text),int.Parse(hourFs.Text), int.Parse(minuteFs.Text)
            //}
            //else if (inizioOra > fineOra || (inizioOra == fineOra && inizioMinuto >= fineMinuto))
            //{
            //    MessageBox.Show("Orario di fine appuntamento atecedente a quello di inizio appuntamento: riscirverli!");
            //    return false;
            //}
            return true;
        }

        private void bottone_serie_Click(object sender, RoutedEventArgs e)
        {
            List<DayOfWeek> listagiorni = new List<DayOfWeek>();
            if ((bool)lu.IsChecked) listagiorni.Add(DayOfWeek.Monday);
            if ((bool)ma.IsChecked) listagiorni.Add(DayOfWeek.Tuesday);
            if ((bool)me.IsChecked) listagiorni.Add(DayOfWeek.Wednesday);
            if ((bool)gi.IsChecked) listagiorni.Add(DayOfWeek.Thursday);
            if ((bool)ve.IsChecked) listagiorni.Add(DayOfWeek.Friday);

            int inizioOra = int.Parse(hourIs.Text);
            int inizioMinuto = int.Parse(minuteIs.Text);
            int fineOra = int.Parse(hourFs.Text);
            int fineMinuto = int.Parse(minuteFs.Text);

            if (datiSerieCorretti())
            {
                DateTime calendar = (DateTime)calendario.SelectedDate;
                DateTime orarioIniziale = new DateTime(calendar.Year, calendar.Month, calendar.Day, inizioOra, inizioMinuto, 0);
                DateTime orarioFinale = new DateTime(calendar.Year, calendar.Month, calendar.Day, fineOra, fineMinuto, 0);
                try
                {
                    _operationsLevel.storeSerie(calendar, int.Parse(settimane.Text), listagiorni, orarioIniziale, orarioFinale, nome.Text, cognome.Text, email.Text);
                    MessageBox.Show("Appuntamenti registrati con successo!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Errore nella registrazione degli appuntamenti!" + ex.Message);
                }
            }

        }

        private void bottone_singolo_Click(object sender, RoutedEventArgs e)
        {
            int inizioOra = int.Parse(hourIapp.Text);
            int inizioMinuto = int.Parse(minuteIapp.Text);
            int fineOra = int.Parse(hourFapp.Text);
            int fineMinuto = int.Parse(minuteFapp.Text);

            if (datiAppCorretti())
            {
                DateTime calendar = (DateTime)calendario.SelectedDate;
                DateTime orarioIniziale = new DateTime(calendar.Year, calendar.Month, calendar.Day, inizioOra, inizioMinuto, 0);
                DateTime orarioFinale = new DateTime(calendar.Year, calendar.Month, calendar.Day, fineOra, fineMinuto, 0);
                try
                {
                    _operationsLevel.storeAppuntamento(orarioIniziale, orarioFinale, nome.Text, cognome.Text, email.Text);
                    MessageBox.Show("Appuntamento singolo registrato con successo!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Errore nella registrazione degli appuntamento!" + ex.Message);
                }

            }

        }
    }
}


