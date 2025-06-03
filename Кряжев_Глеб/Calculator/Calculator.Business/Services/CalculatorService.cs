using Calculator.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Business.Services
{
    public class CalculatorService
    {
        public void PressDigit(CalculatorState state, string digitText)
        {
            if (state.NeedClearX)
            {
                state.InputBuffer = "0";
                state.NeedClearX = false;
            }

            if (state.InputBuffer == "0")
                state.InputBuffer = digitText;
            else
                state.InputBuffer += digitText;

            if (double.TryParse(state.InputBuffer.Replace(',', '.'), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var value))
                state.RegisterX = value;
        }

        public void PressDecimal(CalculatorState state)
        {
            if (state.NeedClearX)
            {
                state.InputBuffer = "0";
                state.NeedClearX = false;
            }

            if (!state.InputBuffer.Contains(","))
                state.InputBuffer += ",";
        }

        public void Clear(CalculatorState state)
        {
            state.RegisterX = 0;
            state.Operation = string.Empty;
            state.InputBuffer = "0";
        }

        public void MoveXToY(CalculatorState state)
        {
            state.RegisterY = state.RegisterX;
        }

        public void CompleteOperation(CalculatorState state, string operation)
        {
            switch (operation)
            {
                case "+":
                    state.RegisterX = state.RegisterY + state.RegisterX;
                    break;

                case "-":
                    state.RegisterX = state.RegisterY - state.RegisterX;
                    break;

                case "*":
                    state.RegisterX = state.RegisterY * state.RegisterX;
                    break;

                case "/":
                    state.RegisterX = state.RegisterY / state.RegisterX;
                    break;
            }
            state.InputBuffer = state.RegisterX.ToString();
        }

        public void PressOperation(CalculatorState state, string operation)
        {
            CompleteOperation(state, state.Operation);
            MoveXToY(state);
            state.Operation = operation;
            state.NeedClearX = true;
        }

        public void PressEqual(CalculatorState state)
        {
            CompleteOperation(state, state.Operation);
            state.NeedClearX = true;
        }

        public void Sqrt(CalculatorState state)
        {
            if (state.RegisterX >= 0)
                state.RegisterX = Math.Sqrt(state.RegisterX);
            else
                state.RegisterX = double.NaN;
        }
    }
}
