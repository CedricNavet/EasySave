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

namespace EasyConsole
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
            this.Close();
        }

        private void english_Click(object sender, RoutedEventArgs e)
        {
            english.IsEnabled = true;
            IMGenglish.Source = new BitmapImage(new Uri("Ressources/english.png", UriKind.Relative));
            IMGspanish.Source = new BitmapImage(new Uri("Ressources/BWspainish.png", UriKind.Relative));
            IMGfrench.Source = new BitmapImage(new Uri("Ressources/BWfrench.png", UriKind.Relative));
            IMGgerman.Source = new BitmapImage(new Uri("Ressources/BWgerman.png", UriKind.Relative));
            start.Visibility = Visibility.Visible;
        }

        private void spainish_Click(object sender, RoutedEventArgs e)
        {
            spainish.IsEnabled = true;
            IMGenglish.Source = new BitmapImage(new Uri("Ressources/BWenglish.png", UriKind.Relative));
            IMGspanish.Source = new BitmapImage(new Uri("Ressources/spainish.png", UriKind.Relative));
            IMGfrench.Source = new BitmapImage(new Uri("Ressources/BWfrench.png", UriKind.Relative));
            IMGgerman.Source = new BitmapImage(new Uri("Ressources/BWgerman.png", UriKind.Relative));
            start.Visibility = Visibility.Visible;
        }

        private void french_Click(object sender, RoutedEventArgs e)
        {
            french.IsEnabled = true;
            IMGenglish.Source = new BitmapImage(new Uri("Ressources/BWenglish.png", UriKind.Relative));
            IMGspanish.Source = new BitmapImage(new Uri("Ressources/BWspainish.png", UriKind.Relative));
            IMGfrench.Source = new BitmapImage(new Uri("Ressources/french.png", UriKind.Relative));
            IMGgerman.Source = new BitmapImage(new Uri("Ressources/BWgerman.png", UriKind.Relative));
            start.Visibility = Visibility.Visible;
        }
    }
}
