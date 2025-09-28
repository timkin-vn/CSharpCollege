﻿namespace Calculator
{
    partial class CalculatorForm
    {
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
            this.XToYButton = new System.Windows.Forms.Button();
            this.bracketFirst = new System.Windows.Forms.Button();
            this.bracketSecond = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // DisplayLabel
            // 
            this.DisplayLabel.BackColor = System.Drawing.Color.Black;
            this.DisplayLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DisplayLabel.ForeColor = System.Drawing.Color.LightGoldenrodYellow;
            this.DisplayLabel.Location = new System.Drawing.Point(24, 17);
            this.DisplayLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.DisplayLabel.Name = "DisplayLabel";
            this.DisplayLabel.Size = new System.Drawing.Size(800, 100);
            this.DisplayLabel.TabIndex = 0;
            this.DisplayLabel.Text = "0";
            this.DisplayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Digit7Button
            // 
            this.Digit7Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Digit7Button.Location = new System.Drawing.Point(24, 140);
            this.Digit7Button.Margin = new System.Windows.Forms.Padding(6);
            this.Digit7Button.Name = "Digit7Button";
            this.Digit7Button.Size = new System.Drawing.Size(150, 110);
            this.Digit7Button.TabIndex = 1;
            this.Digit7Button.Text = "7";
            this.Digit7Button.UseVisualStyleBackColor = true;
            this.Digit7Button.Click += new System.EventHandler(this.DigitButton_Click);
            // 
            // Digit8Button
            // 
            this.Digit8Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Digit8Button.Location = new System.Drawing.Point(188, 140);
            this.Digit8Button.Margin = new System.Windows.Forms.Padding(6);
            this.Digit8Button.Name = "Digit8Button";
            this.Digit8Button.Size = new System.Drawing.Size(150, 110);
            this.Digit8Button.TabIndex = 2;
            this.Digit8Button.Text = "8";
            this.Digit8Button.UseVisualStyleBackColor = true;
            this.Digit8Button.Click += new System.EventHandler(this.DigitButton_Click);
            // 
            // Digit9Button
            // 
            this.Digit9Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Digit9Button.Location = new System.Drawing.Point(350, 140);
            this.Digit9Button.Margin = new System.Windows.Forms.Padding(6);
            this.Digit9Button.Name = "Digit9Button";
            this.Digit9Button.Size = new System.Drawing.Size(150, 110);
            this.Digit9Button.TabIndex = 3;
            this.Digit9Button.Text = "9";
            this.Digit9Button.UseVisualStyleBackColor = true;
            this.Digit9Button.Click += new System.EventHandler(this.DigitButton_Click);
            // 
            // Digit4Button
            // 
            this.Digit4Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Digit4Button.Location = new System.Drawing.Point(24, 262);
            this.Digit4Button.Margin = new System.Windows.Forms.Padding(6);
            this.Digit4Button.Name = "Digit4Button";
            this.Digit4Button.Size = new System.Drawing.Size(150, 110);
            this.Digit4Button.TabIndex = 4;
            this.Digit4Button.Text = "4";
            this.Digit4Button.UseVisualStyleBackColor = true;
            this.Digit4Button.Click += new System.EventHandler(this.DigitButton_Click);
            // 
            // Digit5Button
            // 
            this.Digit5Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Digit5Button.Location = new System.Drawing.Point(188, 262);
            this.Digit5Button.Margin = new System.Windows.Forms.Padding(6);
            this.Digit5Button.Name = "Digit5Button";
            this.Digit5Button.Size = new System.Drawing.Size(150, 110);
            this.Digit5Button.TabIndex = 5;
            this.Digit5Button.Text = "5";
            this.Digit5Button.UseVisualStyleBackColor = true;
            this.Digit5Button.Click += new System.EventHandler(this.DigitButton_Click);
            // 
            // Digit6Button
            // 
            this.Digit6Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Digit6Button.Location = new System.Drawing.Point(350, 262);
            this.Digit6Button.Margin = new System.Windows.Forms.Padding(6);
            this.Digit6Button.Name = "Digit6Button";
            this.Digit6Button.Size = new System.Drawing.Size(150, 110);
            this.Digit6Button.TabIndex = 6;
            this.Digit6Button.Text = "6";
            this.Digit6Button.UseVisualStyleBackColor = true;
            this.Digit6Button.Click += new System.EventHandler(this.DigitButton_Click);
            // 
            // Digit1Button
            // 
            this.Digit1Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Digit1Button.Location = new System.Drawing.Point(24, 383);
            this.Digit1Button.Margin = new System.Windows.Forms.Padding(6);
            this.Digit1Button.Name = "Digit1Button";
            this.Digit1Button.Size = new System.Drawing.Size(150, 110);
            this.Digit1Button.TabIndex = 7;
            this.Digit1Button.Text = "1";
            this.Digit1Button.UseVisualStyleBackColor = true;
            this.Digit1Button.Click += new System.EventHandler(this.DigitButton_Click);
            // 
            // Digit2Button
            // 
            this.Digit2Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Digit2Button.Location = new System.Drawing.Point(188, 383);
            this.Digit2Button.Margin = new System.Windows.Forms.Padding(6);
            this.Digit2Button.Name = "Digit2Button";
            this.Digit2Button.Size = new System.Drawing.Size(150, 110);
            this.Digit2Button.TabIndex = 8;
            this.Digit2Button.Text = "2";
            this.Digit2Button.UseVisualStyleBackColor = true;
            this.Digit2Button.Click += new System.EventHandler(this.DigitButton_Click);
            // 
            // Digit3Button
            // 
            this.Digit3Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Digit3Button.Location = new System.Drawing.Point(350, 383);
            this.Digit3Button.Margin = new System.Windows.Forms.Padding(6);
            this.Digit3Button.Name = "Digit3Button";
            this.Digit3Button.Size = new System.Drawing.Size(150, 110);
            this.Digit3Button.TabIndex = 9;
            this.Digit3Button.Text = "3";
            this.Digit3Button.UseVisualStyleBackColor = true;
            this.Digit3Button.Click += new System.EventHandler(this.DigitButton_Click);
            // 
            // Digit0Button
            // 
            this.Digit0Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Digit0Button.Location = new System.Drawing.Point(24, 504);
            this.Digit0Button.Margin = new System.Windows.Forms.Padding(6);
            this.Digit0Button.Name = "Digit0Button";
            this.Digit0Button.Size = new System.Drawing.Size(150, 110);
            this.Digit0Button.TabIndex = 10;
            this.Digit0Button.Text = "0";
            this.Digit0Button.UseVisualStyleBackColor = true;
            this.Digit0Button.Click += new System.EventHandler(this.DigitButton_Click);
            // 
            // ClearButton
            // 
            this.ClearButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ClearButton.Location = new System.Drawing.Point(512, 140);
            this.ClearButton.Margin = new System.Windows.Forms.Padding(6);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(312, 110);
            this.ClearButton.TabIndex = 11;
            this.ClearButton.Text = "С";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // SubtractButton
            // 
            this.SubtractButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SubtractButton.Location = new System.Drawing.Point(674, 262);
            this.SubtractButton.Margin = new System.Windows.Forms.Padding(6);
            this.SubtractButton.Name = "SubtractButton";
            this.SubtractButton.Size = new System.Drawing.Size(150, 110);
            this.SubtractButton.TabIndex = 12;
            this.SubtractButton.Text = "-";
            this.SubtractButton.UseVisualStyleBackColor = true;
            this.SubtractButton.Click += new System.EventHandler(this.OperationButton_Click);
            // 
            // AddButton
            // 
            this.AddButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AddButton.Location = new System.Drawing.Point(512, 262);
            this.AddButton.Margin = new System.Windows.Forms.Padding(6);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(150, 110);
            this.AddButton.TabIndex = 13;
            this.AddButton.Text = "+";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.OperationButton_Click);
            // 
            // DivideButton
            // 
            this.DivideButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DivideButton.Location = new System.Drawing.Point(674, 383);
            this.DivideButton.Margin = new System.Windows.Forms.Padding(6);
            this.DivideButton.Name = "DivideButton";
            this.DivideButton.Size = new System.Drawing.Size(150, 110);
            this.DivideButton.TabIndex = 14;
            this.DivideButton.Text = "/";
            this.DivideButton.UseVisualStyleBackColor = true;
            this.DivideButton.Click += new System.EventHandler(this.OperationButton_Click);
            // 
            // MultiplyButton
            // 
            this.MultiplyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MultiplyButton.Location = new System.Drawing.Point(512, 383);
            this.MultiplyButton.Margin = new System.Windows.Forms.Padding(6);
            this.MultiplyButton.Name = "MultiplyButton";
            this.MultiplyButton.Size = new System.Drawing.Size(150, 110);
            this.MultiplyButton.TabIndex = 15;
            this.MultiplyButton.Text = "*";
            this.MultiplyButton.UseVisualStyleBackColor = true;
            this.MultiplyButton.Click += new System.EventHandler(this.OperationButton_Click);
            // 
            // EqualButton
            // 
            this.EqualButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.EqualButton.Location = new System.Drawing.Point(512, 504);
            this.EqualButton.Margin = new System.Windows.Forms.Padding(6);
            this.EqualButton.Name = "EqualButton";
            this.EqualButton.Size = new System.Drawing.Size(312, 110);
            this.EqualButton.TabIndex = 16;
            this.EqualButton.Text = "=";
            this.EqualButton.UseVisualStyleBackColor = true;
            this.EqualButton.Click += new System.EventHandler(this.OperationButton_Click);
            // 
            // XToYButton
            // 
            this.XToYButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.XToYButton.Location = new System.Drawing.Point(188, 504);
            this.XToYButton.Margin = new System.Windows.Forms.Padding(6);
            this.XToYButton.Name = "XToYButton";
            this.XToYButton.Size = new System.Drawing.Size(150, 110);
            this.XToYButton.TabIndex = 17;
            this.XToYButton.Text = "X->Y";
            this.XToYButton.UseVisualStyleBackColor = true;
            this.XToYButton.Visible = false;
            //this.XToYButton.Click += new System.EventHandler(this.XToYButton_Click);
            // 
            // bracketFirst
            // 
            this.bracketFirst.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bracketFirst.Location = new System.Drawing.Point(350, 504);
            this.bracketFirst.Margin = new System.Windows.Forms.Padding(6);
            this.bracketFirst.Name = "bracketFirst";
            this.bracketFirst.Size = new System.Drawing.Size(66, 110);
            this.bracketFirst.TabIndex = 18;
            this.bracketFirst.Text = "(";
            this.bracketFirst.UseVisualStyleBackColor = true;
            this.bracketFirst.Click += new System.EventHandler(this.bracketFirst_Click);
            // 
            // bracketSecond
            // 
            this.bracketSecond.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bracketSecond.Location = new System.Drawing.Point(434, 505);
            this.bracketSecond.Margin = new System.Windows.Forms.Padding(6);
            this.bracketSecond.Name = "bracketSecond";
            this.bracketSecond.Size = new System.Drawing.Size(66, 110);
            this.bracketSecond.TabIndex = 19;
            this.bracketSecond.Text = ")";
            this.bracketSecond.UseVisualStyleBackColor = true;
            this.bracketSecond.Click += new System.EventHandler(this.bracketSecond_Click);
            // 
            // CalculatorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(846, 637);
            this.Controls.Add(this.bracketSecond);
            this.Controls.Add(this.bracketFirst);
            this.Controls.Add(this.XToYButton);
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.Name = "CalculatorForm";
            this.Text = "Калькулятор";
            this.ResumeLayout(false);

        }

        #endregion

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
        private System.Windows.Forms.Button XToYButton;
        private System.Windows.Forms.Button bracketFirst;
        private System.Windows.Forms.Button bracketSecond;
    }
}

