using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyTomatoClock
{
    public partial class Form1 : Form
    {
        enum ClockStatus {
        rest, work
        }

        Timer t;
        Label l;
        Button btn,btnStop,btnMode;
        ClockStatus cs = ClockStatus.work;
        bool ticking = false;
        int timer = 25 * 60;
        int workingMaxTime = 25*60;
        int resetMaxTime =5*60;

        public Form1()
        {
            InitializeComponent();
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 170);
            this.Icon = Icon.ExtractAssociatedIcon(System.Reflection.Assembly.GetExecutingAssembly().Location);
            this.Text = "TomatoClock By Forsakensilver";


            t = new Timer();
            t.Interval = 1000;
            t.Tick += new EventHandler(this.t_Tick);
            t.Start();

         l = new Label();
            l.Location = new Point(10,10);
            l.Size = new Size(400,120);
            l.Font = new Font("Arial", 24, FontStyle.Bold);
            this.Controls.Add(l);

            btnStop = new Button();
            btnStop.Text = "Stop/Continue";
            btnStop.Click += BtnStop_Click;
            btnStop.Location = new Point(10, 140);
            this.Controls.Add(btnStop);


            btn = new Button();
            btn.Text = "Reset";
            btn.Location = new Point(310,140);
            btn.Click += Btn_Click;
            this.Controls.Add(btn);

            btnMode = new Button();
            btnMode.Text = "Mode";
            btnMode.Location = new Point(210, 140);
            btnMode.Click += Btn_Mode;
            this.Controls.Add(btnMode);

        }

        private void Btn_Mode(object sender, EventArgs e)
        {
            if (cs == ClockStatus.work)
            {
                cs = ClockStatus.rest;
                timer = resetMaxTime;
                ticking = false;
                //  SystemSounds.Beep.Play();
               // showTipMessage("MyTomatoClock", "Working phase ended!");
            }
            else
            {
                cs = ClockStatus.work;
                timer = workingMaxTime;
                ticking = false;
                // SystemSounds.Beep.Play();
              //  showTipMessage("MyTomatoClock", "Resting phase ended!");
            }
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            ticking = true;
            timer = workingMaxTime;
            cs = ClockStatus.work;
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            //SystemSounds.Beep.Play();
            ticking = !ticking;



        
        }

        public void showTipMessage(string title, string content) 
        {
            NotifyIcon notifyIcon = new NotifyIcon();

            notifyIcon.Icon = Icon.ExtractAssociatedIcon(System.Reflection.Assembly.GetExecutingAssembly().Location); ;
            notifyIcon.Text = string.Format("MyTomatoClock");
            notifyIcon.Visible = true;
            notifyIcon.ShowBalloonTip(5000, title, content, ToolTipIcon.Info);
            notifyIcon.Visible = false;
            notifyIcon.Dispose();

        }

        private void t_Tick(object sender, EventArgs e)
        {
            string ans = "";
        
            if (cs == ClockStatus.work) { ans += "Current phase: working\n"; }
            if (cs == ClockStatus.rest) { ans += "Current phase: resting\n"; }
            ans += Math.Floor(timer / 60f) + ":";
            if (timer % 60 < 10) { ans += "0" + timer % 60; } else {
                ans += timer % 60;
            }
            if (!ticking) { ans += "\n[Stopped]"; }


            l.Text = ans;
            if (!ticking) {  return; }

            timer--;
            if (timer < 0) 
            {
                if (cs == ClockStatus.work)
                {
                    cs = ClockStatus.rest;
                    timer = resetMaxTime;
                    ticking = false;
                    //  SystemSounds.Beep.Play();
                    showTipMessage("MyTomatoClock","Working phase ended!");
                }
                else {
                    cs = ClockStatus.work;
                    timer = workingMaxTime;
                    ticking = false;
                    // SystemSounds.Beep.Play();
                    showTipMessage("MyTomatoClock", "Resting phase ended!");
                }
            
            }
        }
    }
}
