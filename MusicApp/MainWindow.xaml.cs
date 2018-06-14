using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MusicApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Timer timer = null;

        public MainWindow()
        {
            InitializeComponent();

            timer = new Timer(250);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();

            this.Closing += MainWindow_Closing;
        }
    

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            timer?.Stop();
            
        }
        int pom = 0;
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
           
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                if (Player_Singleton.Player.Instance.getPlaying())
                {
                  
                        if (pom == 4)
                        {
                             
                             EnableChange = false;
                        
                            
                                timelineSlider.Value = Player_Singleton.Player.Instance.CurrentSongPosition.TotalSeconds;

                            
                        EnableChange = true;
                        pom = 0;
                        }
                    pom++;
                }
                
                BindingExpression binding = this.songPositionTextBlock.GetBindingExpression(TextBlock.TextProperty);
                binding.UpdateTarget();
            }));
        }
        Boolean EnableChange = false;
        
        private void timelineSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (EnableChange)
            {
                var slider = sender as Slider;
                Player_Singleton.Player.Instance.setSongTime((int)slider.Value);
            }
            EnableChange = true;
        }

        private void printToPdf(object sender,RoutedEventArgs e)
        {
            PrintDialog printdialog = new PrintDialog();
            if(printdialog.ShowDialog() == true)
            {
                printdialog.PrintVisual(TrackList, "Wydruk Playlisty");
            }
        }
    }
}
