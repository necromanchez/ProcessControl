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
using SPC_DesktopV2.InspectionItem;

namespace SPC_DesktopV2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SPI_SPCSDB_ProdEntities db = new SPI_SPCSDB_ProdEntities();
        public MainWindow()
        {
            InitializeComponent();
            invalid.Visibility = Visibility.Hidden;
        }
        
        private void StartInspection_Click(object sender, RoutedEventArgs e)
        {
            string password = Password.Password.ToString();
            mUser user = (from c in db.mUsers where c.UserID == UserName.Text && c.Password == password select c).FirstOrDefault();
            if (user != null)
            {
                Home InspectItem = new Home(user);
                InspectItem.Show();
                this.Close();
                invalid.Visibility = Visibility.Hidden;
            }
            else
            {
                invalid.Visibility = Visibility.Visible;
            }

        }
    }
}
