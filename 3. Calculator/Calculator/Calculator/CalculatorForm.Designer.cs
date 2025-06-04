namespace Calculator
{
    partial class CalculatorForm
    {
        private System.ComponentModel.IContainer components = null;

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
            this.DisplayLabel = new System.Windows.Forms.Label();
            this.Digit7Button = new System.Windows.Forms.Button();
            this.Digit8Button = new System.Windows.Forms.Button();
            this.Digit9Button = new System.Windows.Forms.Button();
            this.Digit4Button = new System.Windows.Forms.Button();
            this.Digit5Button = new System.Windows.Forms.Button();
            this.Digit6Button = new System.Windows.Forms.Button();
            this.Digit1Button = new System.Windows.Forms.Button();
            this.Digit2Button = new System.Windows.Forms.Button();
            this.Digit3Button = new System.Windows.Forms.Button();
            this.Digit0Button = new System.Windows.Forms.Button();
            this.ClearButton = new System.Windows.Forms.Button();
            this.SubtractButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.DivideButton = new System.Windows.Forms.Button();
            this.MultiplyButton = new System.Windows.Forms.Button();
            this.EqualButton = new System.Windows.Forms.Button();
            this.FloatButton = new System.Windows.Forms.Button();
            this.PercentButton = new System.Windows.Forms.Button();
            this.BackspaceButton = new System.Windows.Forms.Button();
            this.SquareRootButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // DisplayLabel
            // 
            this.DisplayLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.DisplayLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DisplayLabel.ForeColor = System.Drawing.Color.GreenYellow;
            this.DisplayLabel.Location = new System.Drawing.Point(12, 9);
            this.DisplayLabel.Name = "DisplayLabel";
            this.DisplayLabel.Size = new System.Drawing.Size(434, 52);
            this.DisplayLabel.TabIndex = 0;
            this.DisplayLabel.Text = "0";
            this.DisplayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Digit7Button
            // 
            this.Digit7Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.Digit7Button.Location = new System.Drawing.Point(12, 73);
            this.Digit7Button.Name = "Digit7Button";
            this.Digit7Button.Size = new System.Drawing.Size(75, 57);
            this.Digit7Button.TabIndex = 1;
            this.Digit7Button.Text = "7";
            this.Digit7Button.UseVisualStyleBackColor = true;
            this.Digit7Button.Click += new System.EventHandler(this.DigitButton_Click);
            // 
            // Digit8Button
            // 
            this.Digit8Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.Digit8Button.Location = new System.Drawing.Point(94, 73);
            this.Digit8Button.Name = "Digit8Button";
            this.Digit8Button.Size = new System.Drawing.Size(75, 57);
            this.Digit8Button.TabIndex = 2;
            this.Digit8Button.Text = "8";
            this.Digit8Button.UseVisualStyleBackColor = true;
            this.Digit8Button.Click += new System.EventHandler(this.DigitButton_Click);
            // 
            // Digit9Button
            // 
            this.Digit9Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.Digit9Button.Location = new System.Drawing.Point(175, 73);
            this.Digit9Button.Name = "Digit9Button";
            this.Digit9Button.Size = new System.Drawing.Size(75, 57);
            this.Digit9Button.TabIndex = 3;
            this.Digit9Button.Text = "9";
            this.Digit9Button.UseVisualStyleBackColor = true;
            this.Digit9Button.Click += new System.EventHandler(this.DigitButton_Click);
            // 
            // Digit4Button
            // 
            this.Digit4Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.Digit4Button.Location = new System.Drawing.Point(12, 136);
            this.Digit4Button.Name = "Digit4Button";
            this.Digit4Button.Size = new System.Drawing.Size(75, 57);
            this.Digit4Button.TabIndex = 4;
            this.Digit4Button.Text = "4";
            this.Digit4Button.UseVisualStyleBackColor = true;
            this.Digit4Button.Click += new System.EventHandler(this.DigitButton_Click);
            // 
            // Digit5Button
            // 
            this.Digit5Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.Digit5Button.Location = new System.Drawing.Point(94, 136);
            this.Digit5Button.Name = "Digit5Button";
            this.Digit5Button.Size = new System.Drawing.Size(75, 57);
            this.Digit5Button.TabIndex = 5;
            this.Digit5Button.Text = "5";
            this.Digit5Button.UseVisualStyleBackColor = true;
            this.Digit5Button.Click += new System.EventHandler(this.DigitButton_Click);
            // 
            // Digit6Button
            // 
            this.Digit6Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.Digit6Button.Location = new System.Drawing.Point(175, 136);
            this.Digit6Button.Name = "Digit6Button";
            this.Digit6Button.Size = new System.Drawing.Size(75, 57);
            this.Digit6Button.TabIndex = 6;
            this.Digit6Button.Text = "6";
            this.Digit6Button.UseVisualStyleBackColor = true;
            this.Digit6Button.Click += new System.EventHandler(this.DigitButton_Click);
            // 
            // Digit1Button
            // 
            this.Digit1Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.Digit1Button.Location = new System.Drawing.Point(12, 199);
            this.Digit1Button.Name = "Digit1Button";
            this.Digit1Button.Size = new System.Drawing.Size(75, 57);
            this.Digit1Button.TabIndex = 7;
            this.Digit1Button.Text = "1";
            this.Digit1Button.UseVisualStyleBackColor = true;
            this.Digit1Button.Click += new System.EventHandler(this.DigitButton_Click);
            // 
            // Digit2Button
            // 
            this.Digit2Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.Digit2Button.Location = new System.Drawing.Point(94, 199);
            this.Digit2Button.Name = "Digit2Button";
            this.Digit2Button.Size = new System.Drawing.Size(75, 57);
            this.Digit2Button.TabIndex = 8;
            this.Digit2Button.Text = "2";
            this.Digit2Button.UseVisualStyleBackColor = true;
            this.Digit2Button.Click += new System.EventHandler(this.DigitButton_Click);
            // 
            // Digit3Button
            // 
            this.Digit3Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.Digit3Button.Location = new System.Drawing.Point(175, 199);
            this.Digit3Button.Name = "Digit3Button";
            this.Digit3Button.Size = new System.Drawing.Size(75, 57);
            this.Digit3Button.TabIndex = 9;
            this.Digit3Button.Text = "3";
            this.Digit3Button.UseVisualStyleBackColor = true;
            this.Digit3Button.Click += new System.EventHandler(this.DigitButton_Click);
            // 
            // Digit0Button
            // 
            this.Digit0Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.Digit0Button.Location = new System.Drawing.Point(12, 262);
            this.Digit0Button.Name = "Digit0Button";
            this.Digit0Button.Size = new System.Drawing.Size(75, 57);
            this.Digit0Button.TabIndex = 10;
            this.Digit0Button.Text = "0";
            this.Digit0Button.UseVisualStyleBackColor = true;
            this.Digit0Button.Click += new System.EventHandler(this.DigitButton_Click);
            // 
            // ClearButton
            // 
            this.ClearButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.ClearButton.Location = new System.Drawing.Point(290, 73);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(75, 57);
            this.ClearButton.TabIndex = 11;
            this.ClearButton.Text = "С";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // SubtractButton
            // 
            this.SubtractButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.SubtractButton.Location = new System.Drawing.Point(371, 136);
            this.SubtractButton.Name = "SubtractButton";
            this.SubtractButton.Size = new System.Drawing.Size(75, 57);
            this.SubtractButton.TabIndex = 12;
            this.SubtractButton.Text = "-";
            this.SubtractButton.UseVisualStyleBackColor = true;
            this.SubtractButton.Click += new System.EventHandler(this.OperationButton_Click);
            // 
            // AddButton
            // 
            this.AddButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.AddButton.Location = new System.Drawing.Point(290, 136);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(75, 57);
            this.AddButton.TabIndex = 13;
            this.AddButton.Text = "+";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.OperationButton_Click);
            // 
            // DivideButton
            // 
            this.DivideButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.DivideButton.Location = new System.Drawing.Point(371, 199);
            this.DivideButton.Name = "DivideButton";
            this.DivideButton.Size = new System.Drawing.Size(75, 57);
            this.DivideButton.TabIndex = 14;
            this.DivideButton.Text = "/";
            this.DivideButton.UseVisualStyleBackColor = true;
            this.DivideButton.Click += new System.EventHandler(this.OperationButton_Click);
            // 
            // MultiplyButton
            // 
            this.MultiplyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.MultiplyButton.Location = new System.Drawing.Point(290, 199);
            this.MultiplyButton.Name = "MultiplyButton";
            this.MultiplyButton.Size = new System.Drawing.Size(75, 57);
            this.MultiplyButton.TabIndex = 15;
            this.MultiplyButton.Text = "*";
            this.MultiplyButton.UseVisualStyleBackColor = true;
            this.MultiplyButton.Click += new System.EventHandler(this.OperationButton_Click);
            // 
            // EqualButton
            // 
            this.EqualButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.EqualButton.Location = new System.Drawing.Point(290, 262);
            this.EqualButton.Name = "EqualButton";
            this.EqualButton.Size = new System.Drawing.Size(75, 57);
            this.EqualButton.TabIndex = 16;
            this.EqualButton.Text = "=";
            this.EqualButton.UseVisualStyleBackColor = true;
            this.EqualButton.Click += new System.EventHandler(this.EqualButton_Click);
            // 
            // FloatButton
            // 
            this.FloatButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.FloatButton.Location = new System.Drawing.Point(94, 262);
            this.FloatButton.Name = "FloatButton";
            this.FloatButton.Size = new System.Drawing.Size(75, 57);
            this.FloatButton.TabIndex = 18;
            this.FloatButton.Text = ",";
            this.FloatButton.UseVisualStyleBackColor = true;
            this.FloatButton.Click += new System.EventHandler(this.FloatButton_Click);
            // 
            // PercentButton
            // 
            this.PercentButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.PercentButton.Location = new System.Drawing.Point(175, 262);
            this.PercentButton.Name = "PercentButton";
            this.PercentButton.Size = new System.Drawing.Size(75, 57);
            this.PercentButton.TabIndex = 19;
            this.PercentButton.Text = "%";
            this.PercentButton.UseVisualStyleBackColor = true;
            this.PercentButton.Click += new System.EventHandler(this.PercentButton_Click);
            // 
            // BackspaceButton
            // 
            this.BackspaceButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.BackspaceButton.Location = new System.Drawing.Point(371, 73);
            this.BackspaceButton.Name = "BackspaceButton";
            this.BackspaceButton.Size = new System.Drawing.Size(75, 57);
            this.BackspaceButton.TabIndex = 20;
            this.BackspaceButton.Text = "←";
            this.BackspaceButton.UseVisualStyleBackColor = true;
            this.BackspaceButton.Click += new System.EventHandler(this.BackspaceButton_Click);
            // 
            // SquareRootButton
            // 
            this.SquareRootButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.SquareRootButton.Location = new System.Drawing.Point(371, 262);
            this.SquareRootButton.Name = "SquareRootButton";
            this.SquareRootButton.Size = new System.Drawing.Size(75, 57);
            this.SquareRootButton.TabIndex = 21;
            this.SquareRootButton.Text = "√";
            this.SquareRootButton.UseVisualStyleBackColor = true;
            this.SquareRootButton.Click += new System.EventHandler(this.SquareRootButton_Click);
            // 
            // CalculatorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(458, 331);
            this.Controls.Add(this.SquareRootButton);
            this.Controls.Add(this.BackspaceButton);
            this.Controls.Add(this.PercentButton);
            this.Controls.Add(this.FloatButton);
            this.Controls.Add(this.EqualButton);
            this.Controls.Add(this.MultiplyButton);
            this.Controls.Add(this.DivideButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.SubtractButton);
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.Digit0Button);
            this.Controls.Add(this.Digit3Button);
            this.Controls.Add(this.Digit2Button);
            this.Controls.Add(this.Digit1Button);
            this.Controls.Add(this.Digit6Button);
            this.Controls.Add(this.Digit5Button);
            this.Controls.Add(this.Digit4Button);
            this.Controls.Add(this.Digit9Button);
            this.Controls.Add(this.Digit8Button);
            this.Controls.Add(this.Digit7Button);
            this.Controls.Add(this.DisplayLabel);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "CalculatorForm";
            this.Text = "Калькулятор";
            this.Load += new System.EventHandler(this.CalculatorForm_Load);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Label DisplayLabel;
        private System.Windows.Forms.Button Digit7Button;
        private System.Windows.Forms.Button Digit8Button;
        private System.Windows.Forms.Button Digit9Button;
        private System.Windows.Forms.Button Digit4Button;
        private System.Windows.Forms.Button Digit5Button;
        private System.Windows.Forms.Button Digit6Button;
        private System.Windows.Forms.Button Digit1Button;
        private System.Windows.Forms.Button Digit2Button;
        private System.Windows.Forms.Button Digit3Button;
        private System.Windows.Forms.Button Digit0Button;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.Button SubtractButton;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Button DivideButton;
        private System.Windows.Forms.Button MultiplyButton;
        private System.Windows.Forms.Button EqualButton;
        private System.Windows.Forms.Button FloatButton;
        private System.Windows.Forms.Button PercentButton;
        private System.Windows.Forms.Button BackspaceButton;
        private System.Windows.Forms.Button SquareRootButton;
    }
}