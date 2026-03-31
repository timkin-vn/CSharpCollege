using Calculator.Business.Models;
using Calculator.Business.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculetor
{
    public partial class Calculetor : Form
    {
        private const string ImFolderName = "fons";

        private string[] ImFileName;

        private int indexer;

        private CalculatorModel _calculatorModel = new CalculatorModel();

        private CalculatorService _service = new CalculatorService();

        public Calculetor()
        {
            InitializeComponent();
        }
        private void Ricerd()
        {
            ImFileName = Directory.EnumerateFiles(ImFolderName).ToArray();
            indexer = 0;
            pictureBox1.Load(ImFileName[indexer]);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Ricerd();
        }

        private void DigitButton_Click(object sender, EventArgs e)
        {
            _service.PressDigit(_calculatorModel, ((Button)sender).Text);
            DisplayValue();
        }

        private void DisplayValue()
        {
            DisplayLabel.Text = _calculatorModel.RegisterX.ToString();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            _service.PressClear(_calculatorModel);
            DisplayValue();
        }

        private void MoveXToYButton_Click(object sender, EventArgs e)
        {
            _service.MoveXToY(_calculatorModel);
            DisplayValue();
        }

        private void OperationButton_Click(object sender, EventArgs e)
        {
            _service.PressOperation(_calculatorModel, ((Button)sender).Text);
            DisplayValue();
        }

        private void EqualButton_Click(object sender, EventArgs e)
        {
            _service.PressEqual(_calculatorModel);
            DisplayValue();
        }
        private void DecimalButton_Click(object sender, EventArgs e)
        {
            _service.PressDecimal(_calculatorModel);
            DisplayValue();
        }
        private void SquareButton_Click(object sender, EventArgs e)
        {
            _service.Square(_calculatorModel);
            DisplayValue();
        }
        private void SqrtButton_Click(object sender, EventArgs e)
        {
            try
            {
                _service.Sqrt(_calculatorModel);
                DisplayValue();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
