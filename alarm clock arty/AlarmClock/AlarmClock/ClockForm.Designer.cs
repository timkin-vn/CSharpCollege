namespace AlarmClock
{
    partial class ClockForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.NumericUpDown numericUpDownHour;
        private System.Windows.Forms.NumericUpDown numericUpDownMinute;
        private System.Windows.Forms.NumericUpDown numericUpDownSecond;
        private System.Windows.Forms.Button setTimerButton;
        private System.Windows.Forms.Label timerLabelDown;
        private System.Windows.Forms.Button startPauseButtonDown;
        private System.Windows.Forms.Panel analogClockPanel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.numericUpDownHour = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownMinute = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownSecond = new System.Windows.Forms.NumericUpDown();
            this.setTimerButton = new System.Windows.Forms.Button();
            this.timerLabelDown = new System.Windows.Forms.Label();
            this.startPauseButtonDown = new System.Windows.Forms.Button();
            this.analogClockPanel = new System.Windows.Forms.Panel();

            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHour)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinute)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSecond)).BeginInit();

            this.SuspendLayout();

            // numericUpDownHour
            this.numericUpDownHour.Location = new System.Drawing.Point(20, 20);
            this.numericUpDownHour.Maximum = 23;
            this.numericUpDownHour.Name = "numericUpDownHour";
            this.numericUpDownHour.Size = new System.Drawing.Size(50, 20);

            // numericUpDownMinute
            this.numericUpDownMinute.Location = new System.Drawing.Point(80, 20);
            this.numericUpDownMinute.Maximum = 59;
            this.numericUpDownMinute.Name = "numericUpDownMinute";
            this.numericUpDownMinute.Size = new System.Drawing.Size(50, 20);

            // numericUpDownSecond
            this.numericUpDownSecond.Location = new System.Drawing.Point(140, 20);
            this.numericUpDownSecond.Maximum = 59;
            this.numericUpDownSecond.Name = "numericUpDownSecond";
            this.numericUpDownSecond.Size = new System.Drawing.Size(50, 20);

            // setTimerButton
            this.setTimerButton.Location = new System.Drawing.Point(200, 20);
            this.setTimerButton.Name = "setTimerButton";
            this.setTimerButton.Size = new System.Drawing.Size(100, 23);
            this.setTimerButton.Text = "Set Timer";
            this.setTimerButton.UseVisualStyleBackColor = true;
            this.setTimerButton.Click += new System.EventHandler(this.SetTimerButton_Click);

            // timerLabelDown
            this.timerLabelDown.Font = new System.Drawing.Font("Consolas", 24F);
            this.timerLabelDown.Location = new System.Drawing.Point(20, 60);
            this.timerLabelDown.Name = "timerLabelDown";
            this.timerLabelDown.Size = new System.Drawing.Size(280, 40);
            this.timerLabelDown.Text = "00:00:00";

            // startPauseButtonDown
            this.startPauseButtonDown.Location = new System.Drawing.Point(20, 110);
            this.startPauseButtonDown.Name = "startPauseButtonDown";
            this.startPauseButtonDown.Size = new System.Drawing.Size(150, 30);
            this.startPauseButtonDown.Text = "Start / Pause";
            this.startPauseButtonDown.UseVisualStyleBackColor = true;
            this.startPauseButtonDown.Click += new System.EventHandler(this.StartPauseButtonDown_Click);

            // analogClockPanel
            this.analogClockPanel.Location = new System.Drawing.Point(320, 20);
            this.analogClockPanel.Name = "analogClockPanel";
            this.analogClockPanel.Size = new System.Drawing.Size(200, 200);
            this.analogClockPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.AnalogClockPanel_Paint);
            this.analogClockPanel.BackColor = System.Drawing.Color.White;

            // ClockForm
            this.ClientSize = new System.Drawing.Size(540, 250);
            this.Controls.Add(this.numericUpDownHour);
            this.Controls.Add(this.numericUpDownMinute);
            this.Controls.Add(this.numericUpDownSecond);
            this.Controls.Add(this.setTimerButton);
            this.Controls.Add(this.timerLabelDown);
            this.Controls.Add(this.startPauseButtonDown);
            this.Controls.Add(this.analogClockPanel);
            this.Name = "ClockForm";
            this.Text = "Alarm Clock - Timer";

            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHour)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinute)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSecond)).EndInit();

            this.ResumeLayout(false);
        }
    }
}
