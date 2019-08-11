using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using UltimatePomodoro.Models;
using System.ComponentModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UltimatePomodoro
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Timer : Page, INotifyPropertyChanged
    {
        public TimeManager current;
        public Boolean isTimerPlay = false;

        public event PropertyChangedEventHandler PropertyChanged;

        public Timer()
        {
            this.InitializeComponent();
            current = TaskManager.currentTimer;
            
        }

        public void onPropertyChanged(string propertyName)
        {
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            if (new TimeSpan(current.span.Hours,current.span.Minutes,current.span.Seconds) == new TimeSpan(0, 0, 3))
            {
                current.pauseTimer();
                icon.Symbol = Symbol.Play;
            }
        }

        private void TimerTextBox_LayoutUpdated(object sender, object e)
        {
            
        }

        private void StartTimer_Click(object sender, RoutedEventArgs e)
        {
            
            if (!isTimerPlay)
            {
                current.startTimer();
                icon.Symbol = Symbol.Pause;
            }
            else
            {
                current.pauseTimer();
                icon.Symbol = Symbol.Play;
            }
            isTimerPlay = !isTimerPlay;
            
        }


        private void RestartTimer_Click(object sender, RoutedEventArgs e)
        {
            current.resetTimer();
            icon.Symbol = Symbol.Play;
            isTimerPlay = false;
        }
    }


 

}
