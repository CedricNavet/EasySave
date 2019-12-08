using EasySave;
using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Media.Imaging;

namespace EasyConsole
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            InitializeComponent();
            if (!File.Exists(@"..\SaveState\CrytoLink.txt"))
            {
                File.Create(@"..\SaveState\CrytoLink.txt").Close();
            }
            else
            {
                CryptoSoft.Text = Tools.ReadData(@"..\SaveState\CrytoLink.txt");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(CryptoSoft.Text))
            {
                MessageBoxResult messageBox = MessageBox.Show("The link need to be a file", Properties.Resources.error, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MainMenu mainMenu = new MainMenu(FileExtension.Text, business_software.Text, CryptoSoft.Text);
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
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            start.Visibility = Visibility.Visible;
        }

        private void spainish_Click(object sender, RoutedEventArgs e)
        {
            spainish.IsEnabled = true;
            IMGenglish.Source = new BitmapImage(new Uri("Ressources/BWenglish.png", UriKind.Relative));
            IMGspanish.Source = new BitmapImage(new Uri("Ressources/spainish.png", UriKind.Relative));
            IMGfrench.Source = new BitmapImage(new Uri("Ressources/BWfrench.png", UriKind.Relative));
            IMGgerman.Source = new BitmapImage(new Uri("Ressources/BWgerman.png", UriKind.Relative));
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-ES");
            start.Visibility = Visibility.Visible;
        }

        private void french_Click(object sender, RoutedEventArgs e)
        {
            french.IsEnabled = true;
            IMGenglish.Source = new BitmapImage(new Uri("Ressources/BWenglish.png", UriKind.Relative));
            IMGspanish.Source = new BitmapImage(new Uri("Ressources/BWspainish.png", UriKind.Relative));
            IMGfrench.Source = new BitmapImage(new Uri("Ressources/french.png", UriKind.Relative));
            IMGgerman.Source = new BitmapImage(new Uri("Ressources/BWgerman.png", UriKind.Relative));
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("fr-FR");
            start.Visibility = Visibility.Visible;
        }

        private void german_Click(object sender, RoutedEventArgs e)
        {
            german.IsEnabled = true;
            IMGenglish.Source = new BitmapImage(new Uri("Ressources/BWenglish.png", UriKind.Relative));
            IMGspanish.Source = new BitmapImage(new Uri("Ressources/BWspainish.png", UriKind.Relative));
            IMGfrench.Source = new BitmapImage(new Uri("Ressources/BWfrench.png", UriKind.Relative));
            IMGgerman.Source = new BitmapImage(new Uri("Ressources/german.png", UriKind.Relative));
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("de-DE");
            start.Visibility = Visibility.Visible;
        }
    }
}