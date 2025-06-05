// ClockForm.Designer.cs
namespace AlarmClock
{
    partial class ClockForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.Panel analogClockPanel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.labelTime = new System.Windows.Forms.Label();
            this.analogClockPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();

            // labelTime
            this.labelTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.labelTime.Location = new System.Drawing.Point(12, 9);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(360, 50);
            this.labelTime.TabIndex = 0;
            this.labelTime.Text = "00:00:00";
            this.labelTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // analogClockPanel
            this.analogClockPanel.Location = new System.Drawing.Point(12, 70);
            this.analogClockPanel.Name = "analogClockPanel";
            this.analogClockPanel.Size = new System.Drawing.Size(360, 360);
            this.analogClockPanel.TabIndex = 1;
            this.analogClockPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.AnalogClockPanel_Paint);

            // ClockForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 450);
            this.Controls.Add(this.analogClockPanel);
            this.Controls.Add(this.labelTime);
            this.Name = "ClockForm";
            this.Text = "Clock";
            this.ResumeLayout(false);
        }
    }
}
