using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clock
{
    class clock
    {
        DateTime alarmTime = DateTime.Now;
        //委托
        public delegate void AlarmHandler(object sender, DateTime args);

        //public delegate void TickHanlder(object sender, DateTime args);
        //事件声明
        public event AlarmHandler OnAlarm;
        public event AlarmHandler OnTick;
        //public event TickHanlder OnTick;

        public clock()
        {
            //构造函数内将OnAlarm和Alarm关联起来，调用OnAlarm即触发Alarm
            OnAlarm += Alarm;
            OnTick += Tick;

        }
        public void Alarm(object sender, DateTime time)
        {
            Console.WriteLine("您设定的时间" + time + "到啦！");
        }

        public void Tick(object sender, DateTime time)
        {
            Console.WriteLine("现在是" + time);
        }

        public void Start()
        {
            while (true)
            {
                DateTime now = DateTime.Now;
                
                OnTick(this, now);
                //现在事件是设定时间时
                if (now.ToString() == alarmTime.ToString())
                {
                    OnAlarm(this, alarmTime);
                }
                //延迟1000ms
                System.Threading.Thread.Sleep(1000);
            }
        }

        public void SetAlarmTime(DateTime atime)
        {
            Console.WriteLine(atime);
            alarmTime = atime;
        }

    }
}
