using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace AlarmClock.Forms
{
    partial class TimerForm
    {

        static int mins = 0; int secs = 0; int milis = 0;
        static StringBuilder stringBuilder = new StringBuilder();


        private void Tick_timer(object sender, EventArgs e)
        {

            if (time_go)
            {
                milis += 10;
            }


            if (milis >= 100)
            {
                secs++;
                milis = 0;
            }

            if (secs >= 60)
            {
                mins++;
                secs = 0;
            }

            stringBuilder = new StringBuilder();
            if (mins < 10)
            {
                stringBuilder.Append("0");
            }
            stringBuilder.Append(mins);
            stringBuilder.Append(":");
            if (secs < 10)
            {
                stringBuilder.Append("0");
            }
            stringBuilder.Append(secs);
            stringBuilder.Append(":");
            stringBuilder.Append(milis / 10);

            DisplayLabel.Text = stringBuilder.ToString();


        }

        private static bool time_go = false;

        private void StartTimer(object sender, EventArgs e)
        {

            if (time_go == false)
            {
                StartButton.Text = "Стоп";
                IntervalButton.Text = "Интервал";
                time_go = true;
            }
            else
            {
                StartButton.Text = "Старт";
                IntervalButton.Text = "Сброс";
                time_go = false;
            }

        }

        static List<String> time_list = new List<String>();

        static List<String> time_list_difference = new List<String>();

        private String time_difference_fun(String s1, String s2)
        {

            String str1 = s1;
            String str2 = s2;

            int a1 = 0, a2 = 0, a3 = 0;
            int b1 = 0, b2 = 0, b3 = 0;

            String[] arr1 = str1.Split(':');
            String[] arr2 = str2.Split(':');

            a1 = Convert.ToInt32(arr1[0]);
            a2 = Convert.ToInt32(arr1[1]);
            a3 = Convert.ToInt32(arr1[2]);

            b1 = Convert.ToInt32(arr2[0]);
            b2 = Convert.ToInt32(arr2[1]);
            b3 = Convert.ToInt32(arr2[2]);

            b1 -= a1;
            b2 -= a2;
            b3 -= a3;

            if (b3 < 0)
            {
                b2--;
                b3 += 10;
            }
            if (b2 < 0)
            {
                b1--;
                b2 += 10;
            }

            

            StringBuilder sb = new StringBuilder();
            sb.Append("+");
            if(b1 < 10)
            {
                sb.Append("0");
            }
            sb.Append(b1);
            sb.Append(":");
            if (b2 < 10)
            {
                sb.Append("0");
            }
            sb.Append(b2);
            sb.Append(":");
            sb.Append(b3);

            return sb.ToString();


        }

        private void IntervalFunction(object sender, EventArgs e)
        {

            if (time_go == false)
            {

                mins = 0; secs = 0; milis = 0;

                time_list.Clear();
                time_list_difference.Clear();

                DisplayLabel_list1.Text = "";
                DisplayLabel_list2.Text = "";
                DisplayLabel_list3.Text = "";
                DisplayLabel_list4.Text = "";
                DisplayLabel_list5.Text = "";
                DisplayLabel_list6.Text = "";

                DisplayLabel_list1_timeplus.Text = "";
                DisplayLabel_list2_timeplus.Text = "";
                DisplayLabel_list3_timeplus.Text = "";
                DisplayLabel_list4_timeplus.Text = "";
                DisplayLabel_list5_timeplus.Text = "";

                DisplayLabel_list1_timeplus.BackColor = Color.BurlyWood;
                DisplayLabel_list2_timeplus.BackColor = Color.BurlyWood;
                DisplayLabel_list3_timeplus.BackColor = Color.BurlyWood;
                DisplayLabel_list4_timeplus.BackColor = Color.BurlyWood;
                DisplayLabel_list5_timeplus.BackColor = Color.BurlyWood;


            }
            else
            {
                time_list.Add(stringBuilder.ToString());

                if (DisplayLabel_list1.Text == "")
                {
                    DisplayLabel_list1.Text = time_list.ElementAt(0);
                }
                else if (DisplayLabel_list2.Text == "")
                {

                    DisplayLabel_list1.Text = time_list.ElementAt(1);
                    DisplayLabel_list2.Text = time_list.ElementAt(0);



                    time_list_difference.Add(time_difference_fun(time_list[0], time_list[1]));
                    DisplayLabel_list1_timeplus.Text = time_list_difference[0];
                    DisplayLabel_list1_timeplus.BackColor = Color.Bisque;
                }
                else if (DisplayLabel_list3.Text == "")
                {
                    DisplayLabel_list1.Text = time_list.ElementAt(2);
                    DisplayLabel_list2.Text = time_list.ElementAt(1);
                    DisplayLabel_list3.Text = time_list.ElementAt(0);


                    time_list_difference.Add(time_difference_fun(time_list[1], time_list[2]));
                    DisplayLabel_list2_timeplus.Text = time_list_difference[0];
                    DisplayLabel_list1_timeplus.Text = time_list_difference[1];
                    DisplayLabel_list2_timeplus.BackColor = Color.Bisque;

                }
                else if (DisplayLabel_list4.Text == "")
                {
                    DisplayLabel_list1.Text = time_list.ElementAt(3);
                    DisplayLabel_list2.Text = time_list.ElementAt(2);
                    DisplayLabel_list3.Text = time_list.ElementAt(1);
                    DisplayLabel_list4.Text = time_list.ElementAt(0);


                    time_list_difference.Add(time_difference_fun(time_list[2], time_list[3]));
                    DisplayLabel_list3_timeplus.Text = time_list_difference[0];
                    DisplayLabel_list2_timeplus.Text = time_list_difference[1];
                    DisplayLabel_list1_timeplus.Text = time_list_difference[2];
                    DisplayLabel_list3_timeplus.BackColor = Color.Bisque;
                }
                else if (DisplayLabel_list5.Text == "")
                {
                    DisplayLabel_list1.Text = time_list.ElementAt(4);
                    DisplayLabel_list2.Text = time_list.ElementAt(3);
                    DisplayLabel_list3.Text = time_list.ElementAt(2);
                    DisplayLabel_list4.Text = time_list.ElementAt(1);
                    DisplayLabel_list5.Text = time_list.ElementAt(0);


                    time_list_difference.Add(time_difference_fun(time_list[3], time_list[4]));
                    DisplayLabel_list4_timeplus.Text = time_list_difference[0];
                    DisplayLabel_list3_timeplus.Text = time_list_difference[1];
                    DisplayLabel_list2_timeplus.Text = time_list_difference[2];
                    DisplayLabel_list1_timeplus.Text = time_list_difference[3];
                    DisplayLabel_list4_timeplus.BackColor = Color.Bisque;
                }
                else if (DisplayLabel_list6.Text == "")
                {
                    DisplayLabel_list1.Text = time_list.ElementAt(5);
                    DisplayLabel_list2.Text = time_list.ElementAt(4);
                    DisplayLabel_list3.Text = time_list.ElementAt(3);
                    DisplayLabel_list4.Text = time_list.ElementAt(2);
                    DisplayLabel_list5.Text = time_list.ElementAt(1);
                    DisplayLabel_list6.Text = time_list.ElementAt(0);


                    time_list_difference.Add(time_difference_fun(time_list[4], time_list[5]));
                    DisplayLabel_list5_timeplus.Text = time_list_difference[0];
                    DisplayLabel_list4_timeplus.Text = time_list_difference[1];
                    DisplayLabel_list3_timeplus.Text = time_list_difference[2];
                    DisplayLabel_list2_timeplus.Text = time_list_difference[3];
                    DisplayLabel_list1_timeplus.Text = time_list_difference[4];
                    DisplayLabel_list5_timeplus.BackColor = Color.Bisque;
                }
                else
                {

                    for (int i = 0; i < time_list_difference.Count - 1; i++)
                    {
                        time_list_difference[i] = time_list_difference[i + 1];
                    }

                    for (int i = 0; i < time_list.Count - 1; i++)
                    {
                        time_list[i] = time_list[i + 1];
                    }

                    time_list[5] = stringBuilder.ToString();

                    time_list_difference[4] = time_difference_fun(time_list[4], time_list[5]);

                    DisplayLabel_list1.Text = time_list.ElementAt(5);
                    DisplayLabel_list2.Text = time_list.ElementAt(4);
                    DisplayLabel_list3.Text = time_list.ElementAt(3);
                    DisplayLabel_list4.Text = time_list.ElementAt(2);
                    DisplayLabel_list5.Text = time_list.ElementAt(1);
                    DisplayLabel_list6.Text = time_list.ElementAt(0);

                    DisplayLabel_list5_timeplus.Text = time_list_difference[0];
                    DisplayLabel_list4_timeplus.Text = time_list_difference[1];
                    DisplayLabel_list3_timeplus.Text = time_list_difference[2];
                    DisplayLabel_list2_timeplus.Text = time_list_difference[3];
                    DisplayLabel_list1_timeplus.Text = time_list_difference[4];

                }

            }

        }

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }



        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.DisplayLabel = new System.Windows.Forms.Label();

            this.DisplayLabel_list1 = new System.Windows.Forms.Label();
            this.DisplayLabel_list2 = new System.Windows.Forms.Label();
            this.DisplayLabel_list3 = new System.Windows.Forms.Label();
            this.DisplayLabel_list4 = new System.Windows.Forms.Label();
            this.DisplayLabel_list5 = new System.Windows.Forms.Label();
            this.DisplayLabel_list6 = new System.Windows.Forms.Label();




            this.DisplayLabel_list1_timeplus = new System.Windows.Forms.Label();
            this.DisplayLabel_list2_timeplus = new System.Windows.Forms.Label();
            this.DisplayLabel_list3_timeplus = new System.Windows.Forms.Label();
            this.DisplayLabel_list4_timeplus = new System.Windows.Forms.Label();
            this.DisplayLabel_list5_timeplus = new System.Windows.Forms.Label();



            this.StartButton = new System.Windows.Forms.Button();
            this.IntervalButton = new System.Windows.Forms.Button();
            this.AlarmTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // DisplayLabel
            // 
            this.DisplayLabel.BackColor = System.Drawing.Color.Black;
            this.DisplayLabel.Font = new System.Drawing.Font("Tahoma", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DisplayLabel.ForeColor = System.Drawing.Color.LimeGreen;
            this.DisplayLabel.Location = new System.Drawing.Point(75, 13);
            this.DisplayLabel.Name = "DisplayLabel";
            this.DisplayLabel.Size = new System.Drawing.Size(228, 83);
            this.DisplayLabel.TabIndex = 0;
            this.DisplayLabel.Text = "00:00:00";
            this.DisplayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            //displaylabel_list


            this.DisplayLabel_list1.BackColor = System.Drawing.Color.Bisque;
            this.DisplayLabel_list1.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DisplayLabel_list1.ForeColor = System.Drawing.Color.Chocolate;
            this.DisplayLabel_list1.Location = new System.Drawing.Point(40, 103);
            this.DisplayLabel_list1.Name = "DisplayLabel_list1";
            this.DisplayLabel_list1.Size = new System.Drawing.Size(200, 60);
            this.DisplayLabel_list1.TabIndex = 1;
            this.DisplayLabel_list1.Text = "";
            this.DisplayLabel_list1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.DisplayLabel_list2.BackColor = System.Drawing.Color.Bisque;
            this.DisplayLabel_list2.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DisplayLabel_list2.ForeColor = System.Drawing.Color.Chocolate;
            this.DisplayLabel_list2.Location = new System.Drawing.Point(40, 163);
            this.DisplayLabel_list2.Name = "DisplayLabel_list2";
            this.DisplayLabel_list2.Size = new System.Drawing.Size(200, 60);
            this.DisplayLabel_list2.TabIndex = 2;
            this.DisplayLabel_list2.Text = "";
            this.DisplayLabel_list2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.DisplayLabel_list3.BackColor = System.Drawing.Color.Bisque;
            this.DisplayLabel_list3.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DisplayLabel_list3.ForeColor = System.Drawing.Color.Chocolate;
            this.DisplayLabel_list3.Location = new System.Drawing.Point(40, 223);
            this.DisplayLabel_list3.Name = "DisplayLabel_list3";
            this.DisplayLabel_list3.Size = new System.Drawing.Size(200, 60);
            this.DisplayLabel_list3.TabIndex = 3;
            this.DisplayLabel_list3.Text = "";
            this.DisplayLabel_list3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.DisplayLabel_list4.BackColor = System.Drawing.Color.Bisque;
            this.DisplayLabel_list4.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DisplayLabel_list4.ForeColor = System.Drawing.Color.Chocolate;
            this.DisplayLabel_list4.Location = new System.Drawing.Point(40, 283);
            this.DisplayLabel_list4.Name = "DisplayLabel_list4";
            this.DisplayLabel_list4.Size = new System.Drawing.Size(200, 60);
            this.DisplayLabel_list4.TabIndex = 4;
            this.DisplayLabel_list4.Text = "";
            this.DisplayLabel_list4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.DisplayLabel_list5.BackColor = System.Drawing.Color.Bisque;
            this.DisplayLabel_list5.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DisplayLabel_list5.ForeColor = System.Drawing.Color.Chocolate;
            this.DisplayLabel_list5.Location = new System.Drawing.Point(40, 343);
            this.DisplayLabel_list5.Name = "DisplayLabel_list5";
            this.DisplayLabel_list5.Size = new System.Drawing.Size(200, 60);
            this.DisplayLabel_list5.TabIndex = 5;
            this.DisplayLabel_list5.Text = "";
            this.DisplayLabel_list5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.DisplayLabel_list6.BackColor = System.Drawing.Color.Bisque;
            this.DisplayLabel_list6.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DisplayLabel_list6.ForeColor = System.Drawing.Color.Chocolate;
            this.DisplayLabel_list6.Location = new System.Drawing.Point(40, 403);
            this.DisplayLabel_list6.Name = "DisplayLabel_list6";
            this.DisplayLabel_list6.Size = new System.Drawing.Size(200, 60);
            this.DisplayLabel_list6.TabIndex = 6;
            this.DisplayLabel_list6.Text = "";
            this.DisplayLabel_list6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //

            //
            //display time_list plus


            this.DisplayLabel_list1_timeplus.BackColor = System.Drawing.Color.BurlyWood;
            this.DisplayLabel_list1_timeplus.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DisplayLabel_list1_timeplus.ForeColor = System.Drawing.Color.Chocolate;
            this.DisplayLabel_list1_timeplus.Location = new System.Drawing.Point(260, 114);
            this.DisplayLabel_list1_timeplus.Name = "DisplayLabel_list7";
            this.DisplayLabel_list1_timeplus.Size = new System.Drawing.Size(110, 40);
            this.DisplayLabel_list1_timeplus.TabIndex = 7;
            this.DisplayLabel_list1_timeplus.Text = "";
            this.DisplayLabel_list1_timeplus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.DisplayLabel_list2_timeplus.BackColor = System.Drawing.Color.BurlyWood;
            this.DisplayLabel_list2_timeplus.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DisplayLabel_list2_timeplus.ForeColor = System.Drawing.Color.Chocolate;
            this.DisplayLabel_list2_timeplus.Location = new System.Drawing.Point(260, 174);
            this.DisplayLabel_list2_timeplus.Name = "DisplayLabel_list7";
            this.DisplayLabel_list2_timeplus.Size = new System.Drawing.Size(110, 40);
            this.DisplayLabel_list2_timeplus.TabIndex = 7;
            this.DisplayLabel_list2_timeplus.Text = "";
            this.DisplayLabel_list2_timeplus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.DisplayLabel_list3_timeplus.BackColor = System.Drawing.Color.BurlyWood;
            this.DisplayLabel_list3_timeplus.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DisplayLabel_list3_timeplus.ForeColor = System.Drawing.Color.Chocolate;
            this.DisplayLabel_list3_timeplus.Location = new System.Drawing.Point(260, 234);
            this.DisplayLabel_list3_timeplus.Name = "DisplayLabel_list7";
            this.DisplayLabel_list3_timeplus.Size = new System.Drawing.Size(110, 40);
            this.DisplayLabel_list3_timeplus.TabIndex = 7;
            this.DisplayLabel_list3_timeplus.Text = "";
            this.DisplayLabel_list3_timeplus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.DisplayLabel_list4_timeplus.BackColor = System.Drawing.Color.BurlyWood;
            this.DisplayLabel_list4_timeplus.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DisplayLabel_list4_timeplus.ForeColor = System.Drawing.Color.Chocolate;
            this.DisplayLabel_list4_timeplus.Location = new System.Drawing.Point(260, 294);
            this.DisplayLabel_list4_timeplus.Name = "DisplayLabel_list7";
            this.DisplayLabel_list4_timeplus.Size = new System.Drawing.Size(110, 40);
            this.DisplayLabel_list4_timeplus.TabIndex = 7;
            this.DisplayLabel_list4_timeplus.Text = "";
            this.DisplayLabel_list4_timeplus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.DisplayLabel_list5_timeplus.BackColor = System.Drawing.Color.BurlyWood;
            this.DisplayLabel_list5_timeplus.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DisplayLabel_list5_timeplus.ForeColor = System.Drawing.Color.Chocolate;
            this.DisplayLabel_list5_timeplus.Location = new System.Drawing.Point(260, 354);
            this.DisplayLabel_list5_timeplus.Name = "DisplayLabel_list7";
            this.DisplayLabel_list5_timeplus.Size = new System.Drawing.Size(110, 40);
            this.DisplayLabel_list5_timeplus.TabIndex = 7;
            this.DisplayLabel_list5_timeplus.Text = "";
            this.DisplayLabel_list5_timeplus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            //



            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(205, 500);
            this.StartButton.Name = "AboutButton";
            this.StartButton.Size = new System.Drawing.Size(150, 60);
            this.StartButton.TabIndex = 1;
            this.StartButton.Text = "Старт";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartTimer);


            //
            // IntervalButton button
            //
            this.IntervalButton.Location = new System.Drawing.Point(30, 500);
            this.IntervalButton.Name = "TimerButton";
            this.IntervalButton.Size = new System.Drawing.Size(150, 60);
            this.IntervalButton.TabIndex = 2;
            this.IntervalButton.Text = "Сброс";
            this.IntervalButton.UseVisualStyleBackColor = true;
            this.IntervalButton.Click += new System.EventHandler(this.IntervalFunction);

            // 
            // AlarmTimer
            // 
            this.AlarmTimer.Enabled = true;
            this.AlarmTimer.Interval = 100;
            this.AlarmTimer.Tick += new System.EventHandler(this.Tick_timer);
            // 
            // ClockForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 600);
            this.BackColor = System.Drawing.Color.BurlyWood;

            this.Controls.Add(this.DisplayLabel_list1_timeplus);
            this.Controls.Add(this.DisplayLabel_list2_timeplus);
            this.Controls.Add(this.DisplayLabel_list3_timeplus);
            this.Controls.Add(this.DisplayLabel_list4_timeplus);
            this.Controls.Add(this.DisplayLabel_list5_timeplus);


            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.IntervalButton);
            this.Controls.Add(this.DisplayLabel);
            this.Controls.Add(this.DisplayLabel_list1);
            this.Controls.Add(this.DisplayLabel_list2);
            this.Controls.Add(this.DisplayLabel_list3);
            this.Controls.Add(this.DisplayLabel_list4);
            this.Controls.Add(this.DisplayLabel_list5);
            this.Controls.Add(this.DisplayLabel_list6);
            this.Controls.Add(this.StartButton);



            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ClockForm";
            this.Text = "Секундомер";
            this.ResumeLayout(false);



        }

        #endregion

        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Button IntervalButton;
        private System.Windows.Forms.Label DisplayLabel;

        private System.Windows.Forms.Timer AlarmTimer;

        private System.Windows.Forms.Label DisplayLabel_list1;
        private System.Windows.Forms.Label DisplayLabel_list2;
        private System.Windows.Forms.Label DisplayLabel_list3;
        private System.Windows.Forms.Label DisplayLabel_list4;
        private System.Windows.Forms.Label DisplayLabel_list5;
        private System.Windows.Forms.Label DisplayLabel_list6;


        private System.Windows.Forms.Label DisplayLabel_list1_timeplus;
        private System.Windows.Forms.Label DisplayLabel_list2_timeplus;
        private System.Windows.Forms.Label DisplayLabel_list3_timeplus;
        private System.Windows.Forms.Label DisplayLabel_list4_timeplus;
        private System.Windows.Forms.Label DisplayLabel_list5_timeplus;



    }
}