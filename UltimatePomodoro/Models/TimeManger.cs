using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace UltimatePomodoro.Models
{

    public class TimeManager
    {
        private  DispatcherTimer time;
        private  DateTimeOffset start, last;
        public  TimeSpan span;


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
        }
    }
}
