namespace Calculator
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
            this.Digit3Button = new System.Windows.Forms.Button();
            this.Digit0Button = new System.Windows.Forms.Button();
            this.Digit6Button = new System.Windows.Forms.Button();
            this.Digit2Button = new System.Windows.Forms.Button();
            this.Digit1Button = new System.Windows.Forms.Button();
            this.Digit5Button = new System.Windows.Forms.Button();
            this.Digit4Button = new System.Windows.Forms.Button();
            this.Digit9Button = new System.Windows.Forms.Button();
            this.Digit8Button = new System.Windows.Forms.Button();
            this.SubtractButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.MultiplyButton = new System.Windows.Forms.Button();
            this.DivideButton = new System.Windows.Forms.Button();
            this.ClearButton = new System.Windows.Forms.Button();
            this.EqualButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.InverseButton = new System.Windows.Forms.Button();
            this.PercentButton = new System.Windows.Forms.Button();
            this.ClearEntryButton = new System.Windows.Forms.Button();
            this.BackspaceButton = new System.Windows.Forms.Button();
            this.SignChangeButton = new System.Windows.Forms.Button();
            this.DecimalButton = new System.Windows.Forms.Button();
            this.SqrtButton = new System.Windows.Forms.Button();
            this.SquareButton = new System.Windows.Forms.Button();
            this.OperationLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DisplayLabel
            // 
            this.DisplayLabel.BackColor = System.Drawing.Color.Transparent;
            this.DisplayLabel.Font = new System.Drawing.Font("Verdana", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DisplayLabel.ForeColor = System.Drawing.Color.Black;
            this.DisplayLabel.Location = new System.Drawing.Point(2, 73);
            this.DisplayLabel.Name = "DisplayLabel";
            this.DisplayLabel.Size = new System.Drawing.Size(317, 62);
            this.DisplayLabel.TabIndex = 0;
            this.DisplayLabel.Text = "0";
            this.DisplayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Digit7Button
            // 
            this.Digit7Button.Font = new System.Drawing.Font("Verdana", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Digit7Button.Location = new System.Drawing.Point(3, 109);
            this.Digit7Button.Name = "Digit7Button";
            this.Digit7Button.Size = new System.Drawing.Size(75, 47);
            this.Digit7Button.TabIndex = 1;
            this.Digit7Button.Text = "7";
            this.Digit7Button.UseVisualStyleBackColor = true;
            this.Digit7Button.Click += new System.EventHandler(this.DigitButton_Click);
            // 
            // Digit3Button
            // 
            this.Digit3Button.Font = new System.Drawing.Font("Verdana", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Digit3Button.Location = new System.Drawing.Point(165, 215);
            this.Digit3Button.Name = "Digit3Button";
            this.Digit3Button.Size = new System.Drawing.Size(75, 47);
            this.Digit3Button.TabIndex = 2;
            this.Digit3Button.Text = "3";
            this.Digit3Button.UseVisualStyleBackColor = true;
            this.Digit3Button.Click += new System.EventHandler(this.DigitButton_Click);
            // 
            // Digit0Button
            // 
            this.Digit0Button.Font = new System.Drawing.Font("Verdana", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Digit0Button.Location = new System.Drawing.Point(84, 268);
            this.Digit0Button.Name = "Digit0Button";
            this.Digit0Button.Size = new System.Drawing.Size(75, 50);
            this.Digit0Button.TabIndex = 3;
            this.Digit0Button.Text = "0";
            this.Digit0Button.UseVisualStyleBackColor = true;
            this.Digit0Button.Click += new System.EventHandler(this.DigitButton_Click);
            // 
            // Digit6Button
            // 
            this.Digit6Button.Font = new System.Drawing.Font("Verdana", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Digit6Button.Location = new System.Drawing.Point(165, 162);
            this.Digit6Button.Name = "Digit6Button";
            this.Digit6Button.Size = new System.Drawing.Size(75, 47);
            this.Digit6Button.TabIndex = 4;
            this.Digit6Button.Text = "6";
            this.Digit6Button.UseVisualStyleBackColor = true;
            this.Digit6Button.Click += new System.EventHandler(this.DigitButton_Click);
            // 
            // Digit2Button
            // 
            this.Digit2Button.Font = new System.Drawing.Font("Verdana", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Digit2Button.Location = new System.Drawing.Point(84, 215);
            this.Digit2Button.Name = "Digit2Button";
            this.Digit2Button.Size = new System.Drawing.Size(75, 47);
            this.Digit2Button.TabIndex = 5;
            this.Digit2Button.Text = "2";
            this.Digit2Button.UseVisualStyleBackColor = true;
            this.Digit2Button.Click += new System.EventHandler(this.DigitButton_Click);
            // 
            // Digit1Button
            // 
            this.Digit1Button.Font = new System.Drawing.Font("Verdana", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Digit1Button.Location = new System.Drawing.Point(3, 215);
            this.Digit1Button.Name = "Digit1Button";
            this.Digit1Button.Size = new System.Drawing.Size(75, 47);
            this.Digit1Button.TabIndex = 6;
            this.Digit1Button.Text = "1";
            this.Digit1Button.UseVisualStyleBackColor = true;
            this.Digit1Button.Click += new System.EventHandler(this.DigitButton_Click);
            // 
            // Digit5Button
            // 
            this.Digit5Button.Font = new System.Drawing.Font("Verdana", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Digit5Button.Location = new System.Drawing.Point(84, 162);
            this.Digit5Button.Name = "Digit5Button";
            this.Digit5Button.Size = new System.Drawing.Size(74, 47);
            this.Digit5Button.TabIndex = 7;
            this.Digit5Button.Text = "5";
            this.Digit5Button.UseVisualStyleBackColor = true;
            this.Digit5Button.Click += new System.EventHandler(this.DigitButton_Click);
            // 
            // Digit4Button
            // 
            this.Digit4Button.Font = new System.Drawing.Font("Verdana", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Digit4Button.Location = new System.Drawing.Point(3, 162);
            this.Digit4Button.Name = "Digit4Button";
            this.Digit4Button.Size = new System.Drawing.Size(75, 47);
            this.Digit4Button.TabIndex = 8;
            this.Digit4Button.Text = "4";
            this.Digit4Button.UseVisualStyleBackColor = true;
            this.Digit4Button.Click += new System.EventHandler(this.DigitButton_Click);
            // 
            // Digit9Button
            // 
            this.Digit9Button.Font = new System.Drawing.Font("Verdana", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Digit9Button.Location = new System.Drawing.Point(165, 109);
            this.Digit9Button.Name = "Digit9Button";
            this.Digit9Button.Size = new System.Drawing.Size(75, 47);
            this.Digit9Button.TabIndex = 9;
            this.Digit9Button.Text = "9";
            this.Digit9Button.UseVisualStyleBackColor = true;
            this.Digit9Button.Click += new System.EventHandler(this.DigitButton_Click);
            // 
            // Digit8Button
            // 
            this.Digit8Button.Font = new System.Drawing.Font("Verdana", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Digit8Button.Location = new System.Drawing.Point(84, 109);
            this.Digit8Button.Name = "Digit8Button";
            this.Digit8Button.Size = new System.Drawing.Size(75, 47);
            this.Digit8Button.TabIndex = 10;
            this.Digit8Button.Text = "8";
            this.Digit8Button.UseVisualStyleBackColor = true;
            this.Digit8Button.Click += new System.EventHandler(this.DigitButton_Click);
            // 
            // SubtractButton
            // 
            this.SubtractButton.Font = new System.Drawing.Font("Verdana", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SubtractButton.Location = new System.Drawing.Point(246, 162);
            this.SubtractButton.Name = "SubtractButton";
            this.SubtractButton.Size = new System.Drawing.Size(78, 47);
            this.SubtractButton.TabIndex = 11;
            this.SubtractButton.Text = "-";
            this.SubtractButton.UseVisualStyleBackColor = true;
            this.SubtractButton.Click += new System.EventHandler(this.SubtractButton_Click);
            // 
            // AddButton
            // 
            this.AddButton.Font = new System.Drawing.Font("Verdana", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AddButton.Location = new System.Drawing.Point(246, 215);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(78, 47);
            this.AddButton.TabIndex = 12;
            this.AddButton.Text = "+";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // MultiplyButton
            // 
            this.MultiplyButton.Font = new System.Drawing.Font("Verdana", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MultiplyButton.Location = new System.Drawing.Point(246, 109);
            this.MultiplyButton.Name = "MultiplyButton";
            this.MultiplyButton.Size = new System.Drawing.Size(77, 47);
            this.MultiplyButton.TabIndex = 13;
            this.MultiplyButton.Text = "X";
            this.MultiplyButton.UseVisualStyleBackColor = true;
            this.MultiplyButton.Click += new System.EventHandler(this.MultiplyButton_Click);
            // 
            // DivideButton
            // 
            this.DivideButton.Font = new System.Drawing.Font("Verdana", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DivideButton.Location = new System.Drawing.Point(246, 56);
            this.DivideButton.Name = "DivideButton";
            this.DivideButton.Size = new System.Drawing.Size(78, 47);
            this.DivideButton.TabIndex = 14;
            this.DivideButton.Text = "÷";
            this.DivideButton.UseVisualStyleBackColor = true;
            this.DivideButton.Click += new System.EventHandler(this.DivideButton_Click);
            // 
            // ClearButton
            // 
            this.ClearButton.Font = new System.Drawing.Font("Verdana", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ClearButton.Location = new System.Drawing.Point(165, 3);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(75, 47);
            this.ClearButton.TabIndex = 15;
            this.ClearButton.Text = "C";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // EqualButton
            // 
            this.EqualButton.BackColor = System.Drawing.SystemColors.Highlight;
            this.EqualButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.EqualButton.Font = new System.Drawing.Font("Verdana", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.EqualButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.EqualButton.Location = new System.Drawing.Point(246, 268);
            this.EqualButton.Name = "EqualButton";
            this.EqualButton.Size = new System.Drawing.Size(78, 50);
            this.EqualButton.TabIndex = 16;
            this.EqualButton.Text = "=";
            this.EqualButton.UseVisualStyleBackColor = false;
            this.EqualButton.Click += new System.EventHandler(this.OperationButton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.InverseButton, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.PercentButton, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.Digit0Button, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.EqualButton, 3, 5);
            this.tableLayoutPanel1.Controls.Add(this.Digit2Button, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.MultiplyButton, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.DivideButton, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.Digit3Button, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.SubtractButton, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.AddButton, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.Digit1Button, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.Digit4Button, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.Digit5Button, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.Digit6Button, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.Digit9Button, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.Digit8Button, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.Digit7Button, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.ClearButton, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.ClearEntryButton, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.BackspaceButton, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.SignChangeButton, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.DecimalButton, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.SqrtButton, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.SquareButton, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 176);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(327, 321);
            this.tableLayoutPanel1.TabIndex = 17;
            // 
            // InverseButton
            // 
            this.InverseButton.Font = new System.Drawing.Font("Verdana", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.InverseButton.Location = new System.Drawing.Point(3, 56);
            this.InverseButton.Name = "InverseButton";
            this.InverseButton.Size = new System.Drawing.Size(75, 47);
            this.InverseButton.TabIndex = 24;
            this.InverseButton.Text = "1/x";
            this.InverseButton.UseVisualStyleBackColor = true;
            this.InverseButton.Click += new System.EventHandler(this.SpecialOperationButton_Click);
            // 
            // PercentButton
            // 
            this.PercentButton.Font = new System.Drawing.Font("Verdana", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PercentButton.Location = new System.Drawing.Point(3, 3);
            this.PercentButton.Name = "PercentButton";
            this.PercentButton.Size = new System.Drawing.Size(75, 47);
            this.PercentButton.TabIndex = 19;
            this.PercentButton.Text = "%";
            this.PercentButton.UseVisualStyleBackColor = true;
            this.PercentButton.Click += new System.EventHandler(this.SpecialOperationButton_Click);
            // 
            // ClearEntryButton
            // 
            this.ClearEntryButton.Font = new System.Drawing.Font("Verdana", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ClearEntryButton.Location = new System.Drawing.Point(84, 3);
            this.ClearEntryButton.Name = "ClearEntryButton";
            this.ClearEntryButton.Size = new System.Drawing.Size(75, 47);
            this.ClearEntryButton.TabIndex = 17;
            this.ClearEntryButton.Text = "CE";
            this.ClearEntryButton.UseVisualStyleBackColor = true;
            this.ClearEntryButton.Click += new System.EventHandler(this.ClearEntryButton_Click);
            // 
            // BackspaceButton
            // 
            this.BackspaceButton.Font = new System.Drawing.Font("Verdana", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BackspaceButton.Location = new System.Drawing.Point(246, 3);
            this.BackspaceButton.Name = "BackspaceButton";
            this.BackspaceButton.Size = new System.Drawing.Size(78, 47);
            this.BackspaceButton.TabIndex = 18;
            this.BackspaceButton.Text = "⌫";
            this.BackspaceButton.UseVisualStyleBackColor = true;
            this.BackspaceButton.Click += new System.EventHandler(this.BackspaceButton_Click);
            // 
            // SignChangeButton
            // 
            this.SignChangeButton.Font = new System.Drawing.Font("Verdana", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SignChangeButton.Location = new System.Drawing.Point(3, 268);
            this.SignChangeButton.Name = "SignChangeButton";
            this.SignChangeButton.Size = new System.Drawing.Size(75, 50);
            this.SignChangeButton.TabIndex = 20;
            this.SignChangeButton.Text = "±";
            this.SignChangeButton.UseVisualStyleBackColor = true;
            this.SignChangeButton.Click += new System.EventHandler(this.SpecialOperationButton_Click);
            // 
            // DecimalButton
            // 
            this.DecimalButton.Font = new System.Drawing.Font("Verdana", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DecimalButton.Location = new System.Drawing.Point(165, 268);
            this.DecimalButton.Name = "DecimalButton";
            this.DecimalButton.Size = new System.Drawing.Size(75, 50);
            this.DecimalButton.TabIndex = 21;
            this.DecimalButton.Text = ",";
            this.DecimalButton.UseVisualStyleBackColor = true;
            this.DecimalButton.Click += new System.EventHandler(this.DecimalButton_Click);
            // 
            // SqrtButton
            // 
            this.SqrtButton.Font = new System.Drawing.Font("Verdana", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SqrtButton.Location = new System.Drawing.Point(165, 56);
            this.SqrtButton.Name = "SqrtButton";
            this.SqrtButton.Size = new System.Drawing.Size(75, 47);
            this.SqrtButton.TabIndex = 22;
            this.SqrtButton.Text = "√";
            this.SqrtButton.UseVisualStyleBackColor = true;
            this.SqrtButton.Click += new System.EventHandler(this.SpecialOperationButton_Click);
            // 
            // SquareButton
            // 
            this.SquareButton.Font = new System.Drawing.Font("Verdana", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SquareButton.Location = new System.Drawing.Point(84, 56);
            this.SquareButton.Name = "SquareButton";
            this.SquareButton.Size = new System.Drawing.Size(75, 47);
            this.SquareButton.TabIndex = 23;
            this.SquareButton.Text = "x²";
            this.SquareButton.UseVisualStyleBackColor = true;
            this.SquareButton.Click += new System.EventHandler(this.SpecialOperationButton_Click);
            // 
            // OperationLabel
            // 
            this.OperationLabel.BackColor = System.Drawing.Color.Transparent;
            this.OperationLabel.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.OperationLabel.ForeColor = System.Drawing.Color.DimGray;
            this.OperationLabel.Location = new System.Drawing.Point(2, 37);
            this.OperationLabel.Name = "OperationLabel";
            this.OperationLabel.Size = new System.Drawing.Size(311, 36);
            this.OperationLabel.TabIndex = 18;
            this.OperationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CalculatorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 497);
            this.Controls.Add(this.OperationLabel);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.DisplayLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "CalculatorForm";
            this.Text = "Калькулятор";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label DisplayLabel;
        private System.Windows.Forms.Button Digit7Button;
        private System.Windows.Forms.Button Digit3Button;
        private System.Windows.Forms.Button Digit0Button;
        private System.Windows.Forms.Button Digit6Button;
        private System.Windows.Forms.Button Digit2Button;
        private System.Windows.Forms.Button Digit1Button;
        private System.Windows.Forms.Button Digit5Button;
        private System.Windows.Forms.Button Digit4Button;
        private System.Windows.Forms.Button Digit9Button;
        private System.Windows.Forms.Button Digit8Button;
        private System.Windows.Forms.Button SubtractButton;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Button MultiplyButton;
        private System.Windows.Forms.Button DivideButton;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.Button EqualButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button PercentButton;
        private System.Windows.Forms.Button ClearEntryButton;
        private System.Windows.Forms.Button BackspaceButton;
        private System.Windows.Forms.Button InverseButton;
        private System.Windows.Forms.Button SignChangeButton;
        private System.Windows.Forms.Button DecimalButton;
        private System.Windows.Forms.Button SqrtButton;
        private System.Windows.Forms.Button SquareButton;
        private System.Windows.Forms.Label OperationLabel;
    }
}

