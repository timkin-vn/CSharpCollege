\using WpfApp1.Models;

namespace WpfApp1.Services
{
    public class CalculatorService
    {
        public void PressDigit(CalculatorState state, string digit)
        {
            if (state.IsNewInput)
            {
                state.Display = digit;
                state.IsNewInput = false;
            }
            else
            {
                state.Display += digit;
            }
        }

        public void PressOperation(CalculatorState state, string op)
        {
            state.CurrentValue = double.Parse(state.Display);
            state.Operation = op;
            state.IsNewInput = true;
        }

        public void PressEqual(CalculatorState state)
        {
            double secondValue = double.Parse(state.Display);
            switch (state.Operation)
            {
                case "+": state.CurrentValue += secondValue; break;
                case "-": state.CurrentValue -= secondValue; break;
                case "*": state.CurrentValue *= secondValue; break;
                case "/": state.CurrentValue /= secondValue; break;
            }
            state.Display = state.CurrentValue.ToString();
            state.IsNewInput = true;
        }

        public void PressClear(CalculatorState state)
        {
            state.Display = "0";
            state.CurrentValue = 0;
            state.Operation = null;
            state.IsNewInput = true;
        }

        public void PressClearEntry(CalculatorState state)
        {
            state.Display = "0";
            state.IsNewInput = true;
        }

        public void PressBackspace(CalculatorState state)
        {
            if (state.Display.Length > 1)
                state.Display = state.Display.Substring(0, state.Display.Length - 1);
            else
                state.Display = "0";
        }

        public void PressSqrt(CalculatorState state) =>
            state.Display = Math.Sqrt(double.Parse(state.Display)).ToString();

        public void PressSquare(CalculatorState state) =>
            state.Display = Math.Pow(double.Parse(state.Display), 2).ToString();

        public void PressInverse(CalculatorState state) =>
            state.Display = (1 / double.Parse(state.Display)).ToString();

        public void PressNegate(CalculatorState state) =>
            state.Display = (-double.Parse(state.Display)).ToString();

        public void PressComma(CalculatorState state)
        {
            if (!state.Display.Contains(","))
                state.Display += ",";
        }
    }
}