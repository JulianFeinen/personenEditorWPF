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
using System.Data.SqlClient;
using System.Data;

namespace PersonenEditorWPF
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<nsTools.Person> personen = new List<nsTools.Person>();

        public MainWindow()
        {
            InitializeComponent();
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }



        private void btnAbbruch_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void dgAuswahlliste_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
            try
            {
                if (dgAuswahlliste.SelectedItem == null) return;
                nsTools.Person auswahl = (nsTools.Person)dgAuswahlliste.SelectedItem;
                var hilfsFormular = new Einzelperson(auswahl);
                hilfsFormular.ShowDialog();
                hilfsFormular = null;
                refreshListe();
            }
            catch (Exception ex)
            {
                MessageBox.Show("oops. Fehler in Auswahlliste: " + ex.Message);
                
            }
        }

        private void btnSucheStart_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            refreshListe();
        }

        private void refreshListe()
        {
            try
            {
                var handle = new nsTools.datenbankTools();
                SqlConnection dbVerbindung = handle.OeffneDatenbank("julidatabank", "julian", "aidabella");
                personen = handle.holeAlleBearbeiterAusDB(dbVerbindung, "SELECT * FROM [julidatabank].[dbo].[BEARBEITER]", tbSuche.Text);
                dgAuswahlliste.DataContext = personen;
                dbVerbindung = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show("oops. Fehler in Auswahlliste: " + ex.Message);

            }
        }

        private void tbSuche_TextChanged(object sender, TextChangedEventArgs e)
        {
            e.Handled = true;
            try
            {
            if (tbSuche.Text.Length >= 3)
            {
                var handle = new nsTools.datenbankTools();
                SqlConnection dbVerbindung = handle.OeffneDatenbank("julidatabank", "julian", "aidabella");
                personen = handle.holeAlleBearbeiterAusDB(dbVerbindung, "SELECT * FROM [julidatabank].[dbo].[BEARBEITER]", tbSuche.Text);
                dgAuswahlliste.DataContext = personen;
                dbVerbindung = null;
             
            }

            }
            catch (Exception ex)
            {
                MessageBox.Show("oops. Fehler in Auswahlliste: " + ex.Message);
                
            }

        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            try
            {
            var myFormular = new winImport();
            myFormular.ShowDialog();
            var handle = new nsTools.datenbankTools();
            handle.importNachDatenbank(myFormular.Importpersonen);
            handle = null;

            MessageBox.Show("Die von Ihnen Ausgewählte Liste wurde erfolgreich auf einer Datenbank gespeichert.\nAhnzahl der importierten Objekte: " + myFormular.Importpersonen.Count);
            dgAuswahlliste.DataContext = myFormular.Importpersonen;

            }
            catch (Exception ex)
            {
                MessageBox.Show("oops. Fehler in Auswahlliste: " + ex.Message);
                
            }
        }


    }
}
