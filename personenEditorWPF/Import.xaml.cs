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

namespace PersonenEditorWPF
{
    /// <summary>
    /// Interaktionslogik für Import.xaml
    /// </summary>
    public partial class winImport : Window
    {
        public List<nsTools.Person> Importpersonen = new List<nsTools.Person>();
        public winImport()
        {
            InitializeComponent();
        }


        private void Import_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnImportieren_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            var hilfsObjekt = new nsTools.createPersonalCollectionAusSCV();
            Importpersonen = hilfsObjekt.holeAlleBearbeiter(tbImport.Text, "");
            
            hilfsObjekt = null;
            Close();
        }
    }
}
