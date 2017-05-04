using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace DualSplit
{
    public partial class Main : Form
    {
        Label[,] splitTimes;
        Label[,] diffTimes;
        Label[] splitTexts;

        ControlCenter cc = new ControlCenter();

        Stopwatch clockA = new Stopwatch();
        Stopwatch clockB = new Stopwatch();

        TimeSpan adjustmentA = new TimeSpan();
        TimeSpan adjustmentB = new TimeSpan();

        TimeSpan[,] splitSpan;

        int splits = 8;

        int splitsDoneA = 0;
        int splitsDoneB = 0;

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            //this.AutoScaleMode = AutoScaleMode.None;
            splitTexts = new Label[splits];
            splitTimes = new Label[2, splits];
            diffTimes = new Label[2, splits];
            splitSpan = new TimeSpan[2, splits];

            lblTimerB.Left = (lblPlayerB.Left + lblPlayerB.Width - lblTimerB.Width);

            for (int i = 0; i < 2; i++)
                for (int j = 0; j < splits; j++)
                {
                    splitSpan[i, j] = new TimeSpan();

                    splitTimes[i, j] = new Label();
                    diffTimes[i, j] = new Label();

                    splitTimes[i, j].Left = (i == 0 ? 600 : 1100);
                    splitTimes[i, j].Top = 25 + (j * 35);
                    splitTimes[i, j].ForeColor = Color.White;
                    splitTimes[i, j].Visible = true;
                    splitTimes[i, j].Text = "";
                    splitTimes[i, j].AutoSize = true;
                    splitTimes[i, j].Font = new Font("Dragon Quest", 18);

                    Controls.Add(splitTimes[i, j]);

                    diffTimes[i, j].Left = (i == 0 ? 400 : 1300);
                    diffTimes[i, j].Top = 25 + (j * 35);
                    diffTimes[i, j].ForeColor = Color.White;
                    diffTimes[i, j].Visible = true;
                    diffTimes[i, j].Text = "";
                    diffTimes[i, j].AutoSize = true;
                    diffTimes[i, j].Font = new Font("Dragon Quest", 18);
                    Controls.Add(diffTimes[i, j]);
                }

            for (int i = 0; i < splits; i++)
            {
                splitTexts[i] = new Label();
                splitTexts[i].Text = (i == 0 ? "Mtn" : i == 1 ? "Harp" : i == 2 ? "Golem" : i == 3 ? "Dragons" : i == 4 ? "Armor" : i == 5 ? "Char" : i == 6 ? "DL" : "The End");
                splitTexts[i].Left = 860;
                splitTexts[i].Top = 25 + (i * 35);
                splitTexts[i].AutoSize = true;
                splitTexts[i].Font = new Font("Dragon Quest", 18);
                splitTexts[i].ForeColor = Color.White;
                Controls.Add(splitTexts[i]);
            }

            //this.Height = 70 + (splits * 30) + 60;

            cc.setupForm(this);
            cc.Show();
            cc.Focus();
        }

        public void startClocks()
        {
            clockA.Start();
            clockB.Start();
            timer1.Enabled = true;
            timer2.Enabled = true;
        }

        public void adjustTime(bool stopWatchA, bool tenths, bool subtract)
        {
            if (subtract)
                if (stopWatchA)
                    adjustmentA = adjustmentA.Subtract(new TimeSpan(0, 0, 0, (!tenths ? 1 : 0), (tenths ? 100 : 0)));
                else
                    adjustmentB = adjustmentB.Subtract(new TimeSpan(0, 0, 0, (!tenths ? 1 : 0), (tenths ? 100 : 0)));
            else
                if (stopWatchA)
                    adjustmentA = adjustmentA.Add(new TimeSpan(0, 0, 0, (!tenths ? 1 : 0), (tenths ? 100 : 0)));
                else
                    adjustmentB = adjustmentB.Add(new TimeSpan(0, 0, 0, (!tenths ? 1 : 0), (tenths ? 100 : 0)));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan tsA = clockA.Elapsed + adjustmentA;
            lblTimerA.Text = Math.Floor(tsA.TotalHours) + ":" + Math.Floor((double)tsA.Minutes).ToString("00") + ":" + Math.Floor((double)tsA.Seconds).ToString("00") + "." + tsA.Milliseconds / 100;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            TimeSpan tsB = clockB.Elapsed + adjustmentB;
            lblTimerB.Text = Math.Floor(tsB.TotalHours) + ":" + Math.Floor((double)tsB.Minutes).ToString("00") + ":" + Math.Floor((double)tsB.Seconds).ToString("00") + "." + tsB.Milliseconds / 100;
        }

        public void split(bool firstPlayer)
        {
            if ((firstPlayer && splitsDoneA == splits) || (!firstPlayer && splitsDoneB == splits)) return;
            int split = (firstPlayer ? splitsDoneA : splitsDoneB);

            TimeSpan ts = (firstPlayer ? clockA.Elapsed + adjustmentA : clockB.Elapsed + adjustmentB);

            splitSpan[firstPlayer ? 0 : 1, split] = ts;

            splitTimes[firstPlayer ? 0 : 1, split].Text = Math.Floor(ts.TotalHours) + ":" + Math.Floor((double)ts.Minutes).ToString("00") + ":" + Math.Floor((double)ts.Seconds).ToString("00");

            calcDiffs(firstPlayer ? splitsDoneA : splitsDoneB);

            if (firstPlayer) splitsDoneA++; else splitsDoneB++;
            if (splitsDoneA == splits) timer1.Enabled = false;
            if (splitsDoneB == splits) timer2.Enabled = false;
        }

        public void calcDiffs(int split)
        {
            if (splitSpan[0, split].TotalSeconds != 0 && splitSpan[1, split].TotalSeconds != 0)
            {
                TimeSpan diff = splitSpan[0, split].Subtract(splitSpan[1, split]);
                if (diff.TotalSeconds < 0)
                {
                    diff = splitSpan[1, split].Subtract(splitSpan[0, split]);
                    if (diff.TotalSeconds < 60)
                    {
                        diffTimes[0, split].Text = "-" + Math.Floor((double)diff.Seconds).ToString("00") + "." + diff.Milliseconds / 100;
                        diffTimes[0, split].ForeColor = Color.LawnGreen;
                        diffTimes[1, split].Text = "+" + Math.Floor((double)diff.Seconds).ToString("00") + "." + diff.Milliseconds / 100;
                        diffTimes[1, split].ForeColor = Color.LightCoral;
                    }
                    else
                    {
                        diffTimes[0, split].Text = "-" + Math.Floor(diff.TotalMinutes).ToString("00") + ":" + Math.Floor((double)diff.Seconds).ToString("00");
                        diffTimes[0, split].ForeColor = Color.LawnGreen;
                        diffTimes[1, split].Text = "+" + Math.Floor(diff.TotalMinutes).ToString("00") + ":" + Math.Floor((double)diff.Seconds).ToString("00");
                        diffTimes[1, split].ForeColor = Color.LightCoral;
                    }
                }
                else
                {
                    if (diff.TotalSeconds < 60)
                    {
                        diffTimes[0, split].Text = "+" + Math.Floor((double)diff.Seconds).ToString("00") + "." + diff.Milliseconds / 100;
                        diffTimes[0, split].ForeColor = Color.LightCoral;
                        diffTimes[1, split].Text = "-" + Math.Floor((double)diff.Seconds).ToString("00") + "." + diff.Milliseconds / 100;
                        diffTimes[1, split].ForeColor = Color.LawnGreen;
                    }
                    else
                    {
                        diffTimes[0, split].Text = "+" + Math.Floor(diff.TotalMinutes) + ":" + Math.Floor((double)diff.Seconds).ToString("00");
                        diffTimes[0, split].ForeColor = Color.LightCoral;
                        diffTimes[1, split].Text = "-" + Math.Floor(diff.TotalMinutes) + ":" + Math.Floor((double)diff.Seconds).ToString("00");
                        diffTimes[1, split].ForeColor = Color.LawnGreen;
                    }
                }
            }
            else
            {
                diffTimes[0, split].Text = "";
                diffTimes[1, split].Text = "";
            }
        }

        public void reverseSplit(bool firstPlayer)
        {
            if ((firstPlayer && splitsDoneA == 0) || (!firstPlayer && splitsDoneB == 0)) return;

            int player = (firstPlayer ? 0 : 1);
            if (firstPlayer) splitsDoneA--; else splitsDoneB--;
            int split = (firstPlayer ? splitsDoneA : splitsDoneB);

            splitSpan[player, split] = new TimeSpan();

            diffTimes[0, split].Text = "";
            diffTimes[1, split].Text = "";

            splitTimes[player, split].Text = "";

            if (firstPlayer) timer1.Enabled = true; else timer2.Enabled = true;
        }

        public void adjustSplit(bool firstPlayer, bool plus)
        {
            int player = (firstPlayer ? 0 : 1);
            int split = (firstPlayer ? splitsDoneA - 1 : splitsDoneB - 1);

            splitSpan[player, split] = splitSpan[player, split].Add(new TimeSpan(0, 0, (plus ? 1 : -1)));
            TimeSpan tsA = splitSpan[player, split];
            splitTimes[player, split].Text = Math.Floor(tsA.TotalHours) + ":" + Math.Floor((double)tsA.Minutes).ToString("00") + ":" + Math.Floor((double)tsA.Seconds).ToString("00");

            calcDiffs(split);
        }

        public void resetClocks()
        {
            timer1.Enabled = false;
            timer2.Enabled = false;

            adjustmentA = new TimeSpan();
            adjustmentB = new TimeSpan();
            lblTimerA.Text = lblTimerB.Text = "0:00:00.0";

            for (int i = 0; i < 2; i++)
                for (int j = 0; j < splits; j++)
                {
                    splitSpan[i, j] = new TimeSpan();
                    splitTimes[i, j].Text = "";
                    diffTimes[i, j].Text = "";
                }

            clockA.Reset();
            clockB.Reset();

            splitsDoneA = 0;
            splitsDoneB = 0;
        }
    }
}
