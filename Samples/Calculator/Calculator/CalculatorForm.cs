using Calculator.Business.Entites;
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
        private CalculatorService _service = new CalculatorService();

        private CalculatorState _state = new CalculatorState();

        public CalculatorForm()
        {
            InitializeComponent();
        }

        private void DigitButton_Click(object sender, EventArgs e)
        {
            var senderButton = (Button)sender;
            var digitText = senderButton.Text;

            _service.InsertDigit(_state, digitText);
            DisplayLabel.Text = _state.XRegister.ToString();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            _service.Clear(_state);
            DisplayLabel.Text = _state.XRegister.ToString();
        }

        private void OperationButton_Click(object sender, EventArgs e)
        {
            var senderButton = (Button)sender;
            var opCode = senderButton.Text;

            _service.InsertOperation(_state, opCode);
            DisplayLabel.Text = _state.XRegister.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Создаем и открываем окно
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.Show();

        }
    }
}


//окно 
public partial class SettingsForm : Form
{
    private ListView list;
    private TextBox settingsTextBox;

    public SettingsForm()
    {
        

        this.Text = "журнал";

        
        Button applyButton = new Button();
        applyButton.Text = "Применить";
        applyButton.Location = new System.Drawing.Point(10, 30); 
        applyButton.Click += ApplyButton_Click;
        Controls.Add(applyButton);

        
        Button closeButton = new Button();
        closeButton.Text = "Закрыть";
        closeButton.Location = new System.Drawing.Point(200, 220); 
        closeButton.Click += CloseButton_Click;
        Controls.Add(closeButton);

        
        Label instructionsLabel = new Label();
        instructionsLabel.Text = "Запишите сюда что бы не забыть - ";
        instructionsLabel.Location = new System.Drawing.Point(5, 10); // Позиция
        Controls.Add(instructionsLabel);

        
        list = new ListView();//в поле класса
        list.Location = new System.Drawing.Point(40, 100);
        Controls.Add(list);

       
        settingsTextBox = new TextBox();//в поле класса
        settingsTextBox.Location = new System.Drawing.Point(40, 65);
        settingsTextBox.Size = new System.Drawing.Size(200, 20);
        settingsTextBox.Text = "Введите значение - ";
        Controls.Add(settingsTextBox);
    }

    // Обработчик для кнопки 
    private void ApplyButton_Click(object sender, EventArgs e)
    {
        string inputText = settingsTextBox.Text; // Получаем 


        ListViewItem item = new ListViewItem(inputText);//создает новый элемент для списка 
        list.Items.Add(item);//добавляет только что созданный элемент


        if (list.Items.Count > 3)
        {
            list.Items.RemoveAt(0); 
        }

        
        settingsTextBox.Clear();
    }

   
    private void CloseButton_Click(object sender, EventArgs e)
    {
        this.Close();
    }
}



