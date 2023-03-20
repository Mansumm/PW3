using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace WpfApp3
{
 
    
    public partial class MainWindow : Window
    {

        public static string[] files;
        public static int n = 0;
        public static bool a = true;
        public static bool b = true;
        static bool random_music = false;


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            

            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
          
            var result = dialog.ShowDialog();
            if (result == CommonFileDialogResult.Ok)
            {
                
                files = Directory.GetFiles(dialog.FileName, "*.mp3");
               
                foreach (string file in files)
                {
                    ListOfSongs.Items.Add(file);
                   
                }
                media.Source = new Uri(files[n]);
                media.Volume = Volume.Value = 0.1;
                BigSlider.Value = 0;
                media.Play();

                play.Visibility = Visibility.Hidden;
                Stop.Visibility = Visibility.Visible;
               
                Thread thread = new Thread(SecondThread);
                thread.Start();
            }
            else
            {
                MessageBox.Show("Вы не выбрали папку", "Ошибка");
            }
        }

       
        private void up_Click(object sender, RoutedEventArgs e)
        {
            media.Stop();
            media.Play();
        }

        private void down_Click(object sender, RoutedEventArgs e)
        {
            media.Pause();
        }

        private void random_Click(object sender, RoutedEventArgs e)
        {
            if (random_music)
            {
                random_music = false;
                random.ToolTip = "Включить рандом";
            }
            else
            {
                random_music = true;
                random.ToolTip = "Выключить рандом";
            }
        }
        private void StartANDStop(object sender, RoutedEventArgs e)
        {
            media.Play();

            a = false;


            play.Visibility = Visibility.Hidden;
            Stop.Visibility = Visibility.Visible;

        }
        private void StopANDStart(object sender, RoutedEventArgs e)
        {
            media.Pause();

            a = false;

            Stop.Visibility = Visibility.Hidden;
            play.Visibility = Visibility.Visible;

        }




        private void VolumeVALUE(object sender, RoutedEventArgs e)
        {
            media.Volume = Volume.Value;
        }
        private void MediaOpener(object sender, RoutedEventArgs e)
        {

            secondtime.Content = media.NaturalDuration.TimeSpan.ToString().Substring(0, 8);
            BigSlider.Maximum = media.NaturalDuration.TimeSpan.Ticks;
        }
        private void BigSliderNewVAlUE(object sender, RoutedEventArgs e)
        {

            media.Position = TimeSpan.FromSeconds(BigSlider.Value);

        }
        private void SecondThread()
        {

           
            while (a == true)
            {
                a = false;
               
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {



                    fisrttime.Content = media.Position.ToString().Substring(0, 8);

                    BigSlider.Value = media.Position.TotalSeconds;


                }));

                

            }
        }

        private void MediaEnd(object sender, RoutedEventArgs e)
        {
            if (n == 3)
            {
                a = false;
                BigSlider.Value = 0;
                n = 0;
                media.Source = new Uri(files[n]);
       

            }
            else
            {
                a = false;
                BigSlider.Value = 0;
                media.Source = new Uri(files[n + 1]);
                n++;
               
            }
        }
    }
}