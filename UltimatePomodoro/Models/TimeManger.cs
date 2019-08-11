using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace UltimatePomodoro.Models
{

    public class TimeManager : INotifyPropertyChanged
    {
        private  DispatcherTimer time = new DispatcherTimer();
        private Stopwatch watch = new Stopwatch();
        public  TimeSpan span;
        public string timeString = "00:00";
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public TimeManager()
        {
            time.Tick += Time_Tick;
            time.Interval = new TimeSpan(0, 0, 1);
            
        }
        public void startTimer()
        {
            
            watch.Start();
            time.Start();
        }
        
        public void pauseTimer()
        {
            time.Stop();
            watch.Stop();
        }

        public void resetTimer()
        {
            
            if (watch.IsRunning)
            {
                watch.Stop();
            }
            watch.Reset();
            timeString = "00:00";
            this.onPropertyChanged("timeString");
        }

        private void Time_Tick(object sender, object e)
        {
            span = watch.Elapsed;
            timeString = String.Format("{0}:{1}", span.Minutes.ToString().PadLeft(2,'0'), span.Seconds.ToString().PadLeft(2, '0'));
            this.onPropertyChanged("timeString");
            if (new TimeSpan(span.Hours,span.Minutes,span.Seconds) == new TimeSpan(0, 25, 0))
            {
                pauseTimer();
                
            }
        }

        public void onPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
