// File: CalculatorForm.cs
using Calculator.Business.Models;
using Calculator.Business.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class CalculatorForm : Form
    {
        private CalculatorState _state = new CalculatorState();
        private CalculatorService _service = new CalculatorService();

        public CalculatorForm()
        {
            InitializeComponent();
            UpdateDisplay(); // Обновляем дисплей при запуске
        }

        // Вспомогательный метод для обновления дисплея
        private void UpdateDisplay()
        {
            // Используем метод FormatNumberForDisplay из CalculatorService
            DisplayLabel.Text = _service.FormatNumberForDisplay(_state.XRegister);
        }

        private void DigitButton_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var digit = button.Text;
            _service.InsertDigit(_state, digit);
            UpdateDisplay();
        }

        private void CalculatorForm_Load(object sender, EventArgs e)
        {
            UpdateDisplay(); // Обновляем дисплей при загрузке формы
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            _service.Clear(_state);
            UpdateDisplay();
        }

        private void OperationButton_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var opCode = button.Text;
            // Преобразуем символы из UI в коды операций для сервиса
            if (opCode == "÷") opCode = "/";
            if (opCode == "×") opCode = "*";

            _service.PressOperation(_state, opCode);
            UpdateDisplay();
        }

        // --- НОВЫЙ/ВОЗВРАЩЕННЫЙ ОБРАБОТЧИК СОБЫТИЯ ДЛЯ КНОПКИ "=" ---
        private void EqualButton_Click(object sender, EventArgs e)
        {
            _service.PressOperation(_state, "="); // Передаем "=" в сервис
            UpdateDisplay(); // Обновляем дисплей
        }
        // --- КОНЕЦ НОВОГО/ВОЗВРАЩЕННОГО ОБРАБОТЧИКА ---


        private void DegreeButton_Click(object sender, EventArgs e) // x²
        {
            _service.DegretOperation(_state);
            UpdateDisplay();
        }

        private void RowButton_Click(object sender, EventArgs e) // √x
        {
            _service.RowOperation(_state);
            UpdateDisplay();
        }

        private void ABSButton_Click(object sender, EventArgs e) // |x|
        {
            _service.ABSOperation(_state);
            UpdateDisplay();
        }

        private void Negativ_PositivButton_Click_1(object sender, EventArgs e) // +/-
        {
            _service.Negativ_and_Positiv_Operation(_state);
            UpdateDisplay();
        }

        private void CubeButton_Click(object sender, EventArgs e) // x³
        {
            _service.CubeOperation(_state);
            UpdateDisplay();
        }

        private void PercentButton_Click(object sender, EventArgs e) // %
        {
            _service.PercentOperation(_state);
            UpdateDisplay();
        }
    }
}