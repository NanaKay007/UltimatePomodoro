using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace UltimatePomodoro.Models
{

    public class TimeManager : INotifyPropertyChanged
    {
        private  DispatcherTimer time;
        private  DateTimeOffset start, last;
        public  TimeSpan span;
        public string timeString = "0:0";
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public void startTimer()
        {
            time = new DispatcherTimer();
            time.Tick += Time_Tick;

            time.Interval = new TimeSpan(0, 0, 1);
            start = DateTimeOffset.Now;
            last = start;
            time.Start();
            
        }

        private void Time_Tick(object sender, object e)
        {
            last = DateTimeOffset.Now;
            span = last - start;
            timeString = String.Format("{0}:{1}", span.Minutes, span.Seconds);
            this.onPropertyChanged("timeString");
        }

        public void onPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
