using System.ComponentModel;
using System.Runtime.CompilerServices;
using CalculatorForm.Business.Services;
using CalculatorForm.Business.Models;

namespace CalculatorForm.Wpf.ViewModels;

public class ViewModel : INotifyPropertyChanged {
    private readonly CalculatorState _state = new CalculatorState();
    private readonly CalculatorService _service = new CalculatorService();
    private string _displayText = "0";
    private string _expressionText = "";
    private string _sineButtonText = "sin";

    public event PropertyChangedEventHandler PropertyChanged;

    public string DisplayText {
        get => _displayText;
        set {
            _displayText = value;
            OnPropertyChanged();
        }
    }

    public string ExpressionText {
        get => _expressionText;
        set {
            _expressionText = value;
            OnPropertyChanged();
        }
    }

    public string SineButtonText {
        get => _sineButtonText;
        set {
            _sineButtonText = value;
            OnPropertyChanged();
        }
    }

    public void PressDigit(string digit) {
        _service.PressDigit(_state, digit);
        UpdateDisplay();
    }

    public void Clear() {
        CalculatorService.Clear(_state);
        SineButtonText = "sin";
        UpdateDisplay();
    }

    public void PressOperation(string operation) {
        _service.PressOperation(_state, operation);
        UpdateDisplay();
    }

    public void PressEqual() {
        CalculatorService.PressEqual(_state);
        UpdateDisplay();
    }

    public void FunctionButton(string function) {
        switch (function) {
            case "√":
                CalculatorService.SquareRoot(_state);
                break;
            case "sin":
            case "cos":
            case "tan":
            case "cot":
            case "arcsin":
            case "arccos":
            case "arctan":
            case "arccot":
                CalculatorService.ComputeTrigFunction(_state);
                break;
            case "x²":
                CalculatorService.Square(_state);
                break;
            case "%":
                CalculatorService.Percent(_state);
                break;
            case "+/-":
                CalculatorService.ChangeSign(_state);
                break;
            case ".":
                CalculatorService.AddDecimal(_state);
                break;
            case "MS":
                CalculatorService.MemoryStore(_state);
                break;
            case "MR":
                CalculatorService.MemoryRecall(_state);
                break;
        }
        UpdateDisplay();
    }

    public void SetTrigFunction(string function) {
        _state.SelectedFunction = function;
        SineButtonText = function;
    }

    public void UpdateDisplay() {
        DisplayText = !string.IsNullOrEmpty(_state.CurrentInput) 
            ? _state.CurrentInput 
            : _state.RegisterX.ToString("F6").TrimEnd('0').TrimEnd('.');
        ExpressionText = _state.Expression;
    }

    private void OnPropertyChanged([CallerMemberName] string propertyName = null) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}