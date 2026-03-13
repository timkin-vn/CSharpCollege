namespace AlarmClock.Forms
{
    partial class MathChallengeForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelQuestion;
        private System.Windows.Forms.TextBox textBoxAnswer;
        private System.Windows.Forms.Button buttonSubmit;
        private System.Windows.Forms.Label labelTimer;
        private System.Windows.Forms.Label labelHint;

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
            this.label1 = new System.Windows.Forms.Label();
            this.labelQuestion = new System.Windows.Forms.Label();
            this.textBoxAnswer = new System.Windows.Forms.TextBox();
            this.buttonSubmit = new System.Windows.Forms.Button();
            this.labelTimer = new System.Windows.Forms.Label();
            this.labelHint = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // label1
            this.label1.Text = "Чтобы отключить будильник, решите пример:";
            this.label1.Location = new System.Drawing.Point(50, 30);
            this.label1.Size = new System.Drawing.Size(300, 30);
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));

            // labelQuestion
            this.labelQuestion.Text = "5 + 3 = ?";
            this.labelQuestion.Location = new System.Drawing.Point(50, 70);
            this.labelQuestion.Size = new System.Drawing.Size(300, 50);
            this.labelQuestion.Font = new System.Drawing.Font("Microsoft Sans Serif", 24, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelQuestion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // textBoxAnswer
            this.textBoxAnswer.Location = new System.Drawing.Point(120, 130);
            this.textBoxAnswer.Size = new System.Drawing.Size(150, 30);
            this.textBoxAnswer.Font = new System.Drawing.Font("Microsoft Sans Serif", 16, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxAnswer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxAnswer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxAnswer_KeyPress);

            // buttonSubmit
            this.buttonSubmit.Text = "Проверить";
            this.buttonSubmit.Location = new System.Drawing.Point(140, 170);
            this.buttonSubmit.Size = new System.Drawing.Size(100, 30);
            this.buttonSubmit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSubmit.Click += new System.EventHandler(this.ButtonSubmit_Click);

            // labelTimer
            this.labelTimer.Text = "Осталось времени: 30 сек";
            this.labelTimer.Location = new System.Drawing.Point(50, 210);
            this.labelTimer.Size = new System.Drawing.Size(300, 20);
            this.labelTimer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // labelHint
            this.labelHint.Text = "Подсказка: введите число и нажмите Enter";
            this.labelHint.Location = new System.Drawing.Point(50, 235);
            this.labelHint.Size = new System.Drawing.Size(300, 20);
            this.labelHint.ForeColor = System.Drawing.Color.Gray;

            // MathChallengeForm
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Controls.Add(this.labelHint);
            this.Controls.Add(this.labelTimer);
            this.Controls.Add(this.buttonSubmit);
            this.Controls.Add(this.textBoxAnswer);
            this.Controls.Add(this.labelQuestion);
            this.Controls.Add(this.label1);
            this.Text = "Математический тест";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}