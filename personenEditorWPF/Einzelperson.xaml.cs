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
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PersonenEditorWPF
{
    /// <summary>
    /// Interaktionslogik für Einzelperson.xaml
    /// </summary>
    public partial class Einzelperson : Window
    {
        nsTools.Person auswahl = new nsTools.Person();


        private bool ladevorgangabgeschloseen = false;

        public Einzelperson(nsTools.Person _auswahl)
        {

            InitializeComponent();
            //auswahl = _auswahl;
            auswahl = _auswahl.Kopie();

        }

        private void Einzelperson_Loaded(object sender, RoutedEventArgs e)

        {
            tbNachname.Text = auswahl.Nachname;
            tbUsername.Text = auswahl.Username;
            tbVorname.Text = auswahl.Vorname;
            lbBearbeiterID.Content = "Bearbeiter ID\t " + auswahl.BearbeiterID;
            ladevorgangabgeschloseen = true;
        }

        private void btnSpeichern_Click(object sender, RoutedEventArgs e)
        {
            string oldNachname = auswahl.Nachname;
            string oldUsername = auswahl.Username;
            string oldVorname = auswahl.Vorname;

            string newNachname = tbNachname.Text;
            string newUsername = tbUsername.Text;
            string newVorname = tbVorname.Text;

            var hendel = new nsTools.datenbankTools();
            SqlConnection dbVerbindung = hendel.OeffneDatenbank("julidatabank", "julian", "aidabella");
            string sqlResetscript = "UPDATE BEARBEITER\n" +
            "SET USERNAME = '" + newUsername + "', NACHNAME = '" + newNachname + "', VORNAME = '" + newVorname + "'\n" +
            "WHERE USERNAME = '" + oldUsername + "' AND NACHNAME = '" + oldNachname + "' AND VORNAME = '" + oldVorname + "';";

            SqlCommand com2 = new SqlCommand();
            com2.CommandText = sqlResetscript;
            com2.Connection = dbVerbindung;
            SqlDataAdapter da2 = new SqlDataAdapter(com2);
            var kobjssss2 = com2.ExecuteScalar();
            com2.Dispose();
            da2.Dispose();

            Close();
        }

        private void btnAbbruch_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void tbVorname_TextChanged(object sender, TextChangedEventArgs e)
        {
            e.Handled = true;

            if (ladevorgangabgeschloseen) btnSpeichern.IsEnabled = true;

        }

        private void tbNachname_TextChanged(object sender, TextChangedEventArgs e)
        {
            e.Handled = true;

            if (ladevorgangabgeschloseen) btnSpeichern.IsEnabled = true;
        }

        private void tbUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            e.Handled = true;

            if (ladevorgangabgeschloseen) btnSpeichern.IsEnabled = true;
        }

        private void btnLoeschen_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            DialogResult loeschenErgebnis = System.Windows.Forms.MessageBox.Show("Wollen Sie den Angestellten wirklich aus der Gesamten Liste löschen?", "Wirklich löschen?", (MessageBoxButtons)MessageBoxButton.YesNo);
            if(loeschenErgebnis == System.Windows.Forms.DialogResult.Yes)
            {
                var hendel = new nsTools.datenbankTools();
                SqlConnection dbVerbindung = hendel.OeffneDatenbank("julidatabank", "julian", "aidabella");

                string sqlDelete = "DELETE FROM BEARBEITER WHERE BEARBEITERID='" + auswahl.BearbeiterID +"';";
                SqlCommand com2 = new SqlCommand();
                com2.CommandText = sqlDelete;
                com2.Connection = dbVerbindung;
                SqlDataAdapter da2 = new SqlDataAdapter(com2);
                var kobjssss2 = com2.ExecuteScalar();
                com2.Dispose();
                da2.Dispose();

                Close();
            }
        }
    }
}
