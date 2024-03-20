using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ruangou_4_2
{
    internal class Program
    {
        //使用事件机制，模拟实现一个闹钟功能。
        //闹钟可以有嘀嗒（Tick）事件和响铃（Alarm）两个事件。
        //在闹钟走时时或者响铃时，在控制台显示提示信息。

        static void Main(string[] args)
        {
            DelegateClockEvent clock = new DelegateClockEvent(0,10);

            clock.Tick += Clock_Tick;//注册
            clock.Alarm += Clock_Alarm;//注册

            for (int i = 0; i < 20; i++)
            {
                clock.TickHandler();
                Console.WriteLine($"当前时间: {clock.CurrentTime}");
                Thread.Sleep(1000);
            }
        }

        // 嘀嗒事件的处理函数
        static void Clock_Tick()
        {
            Console.Write("嘀嗒！ ");
        }
        static void Clock_Alarm()
        {
            Console.Write("响铃！ ");
        }
    }
    class DelegateClockEvent
    {
        public delegate void EventHandler();

        public event EventHandler Tick;

        public event EventHandler Alarm;

        private int time;

        public int CurrentTime { get; set; }

        public DelegateClockEvent(int CurrentTime,int time)
        {
            this.CurrentTime = CurrentTime;
            this.time = time;
        }

        public void TickHandler()
        {
            CurrentTime++;
            OnTick();
            if (CurrentTime == time)
            {
                OnAlarm();
            }

        }

        protected virtual void OnTick()
        {
            if (Tick != null)
            {
                Tick();
            }
        }

        protected virtual void OnAlarm()
        {
            if (Alarm != null)
            {
                Alarm();
            }
        }
    }

}
