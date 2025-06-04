using Calculator.Business.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator.Business.Services
{
    public class CalculatorService
    {
        public void PressDigit(CalculatorState state, string digitString)
        {
            if (!int.TryParse(digitString, out var digit))
            {
                return;
            }

            if (state.IsClearNeeded)
            {
                state.RegisterX = 0;
            }

            var currentValue = state.RegisterX.ToString(CultureInfo.InvariantCulture);
            if (currentValue.Contains("."))
            {
                state.RegisterX = double.Parse($"{currentValue}{digit}", CultureInfo.InvariantCulture);
            }
            else
            {
                state.RegisterX = state.RegisterX * 10 + digit;
            }
            state.IsClearNeeded = false;
            state.ErrorMessage = null;
        }

        public void Clear(CalculatorState state)
        {
            state.RegisterX = 0;
            state.RegisterY = 0;
            state.IsClearNeeded = true;
            state.OperationCode = null;
            state.ErrorMessage = null;
        }

        public void MoveXToY(CalculatorState state)
        {
            state.RegisterY = state.RegisterX;
        }

        public void CompleteOperation(CalculatorState state, string operationCode)
        {
            state.ErrorMessage = null;

            double originalValue = state.RegisterX;

            switch (operationCode)
            {
                case "+":
                    state.RegisterX = state.RegisterY + originalValue;
                    break;

                case "-":
                    state.RegisterX = state.RegisterY - originalValue;
                    break;

                case "*":
                    state.RegisterX = state.RegisterY * originalValue;
                    break;

                case "/":
                    if (originalValue != 0)
                    {
                        state.RegisterX = state.RegisterY / originalValue;
                    }
                    else
                    {
                        state.ErrorMessage = "На ноль делить нельзя!";
                        state.IsClearNeeded = true;
                        return;
                    }
                    break;
            }
            string expression = $"{state.RegisterY} {operationCode} {originalValue}";
            RecordHistory(state, expression, state.RegisterX);
        }

        public void PressOperation(CalculatorState state, string operationCode)
        {
            if (state.OperationCode != null)
            {
                CompleteOperation(state, state.OperationCode);
            }
            else
            {
                MoveXToY(state);
            }

            state.OperationCode = operationCode;
            state.IsClearNeeded = true;

        }
        public void PressEqual(CalculatorState state)
        {
            if (state.OperationCode != null)
            {
                CompleteOperation(state, state.OperationCode);
                state.OperationCode = null;
            }
            state.IsClearNeeded = true;
        }

        public void PressSin(CalculatorState state)
        {
            double originalValue = state.RegisterX;

            state.RegisterX = Math.Sin(originalValue * Math.PI / 180);
            state.IsClearNeeded = true;
            RecordHistory(state, $"sin({originalValue})", state.RegisterX);
        }
        public void PressCos(CalculatorState state)
        {
            double originalValue = state.RegisterX;

            state.RegisterX = Math.Cos(originalValue * Math.PI / 180);
            state.IsClearNeeded = true;
            RecordHistory(state, $"cos({originalValue})", state.RegisterX);
        }

        public void PressLog(CalculatorState state)
        {
            double originalValue = state.RegisterX;

            if (originalValue <= 0)
            {
                state.ErrorMessage = "Ошибка!";
                state.IsClearNeeded = true;
            }
            else
            {
                state.RegisterX = Math.Log(originalValue);
                state.IsClearNeeded = true;
                RecordHistory(state, $"log({originalValue})", state.RegisterX);
            }
        }

        public void PressTan(CalculatorState state)
        {
            double originalValue = state.RegisterX;

            if (Math.Cos(originalValue * Math.PI / 180) == 0)
            {
                state.ErrorMessage = "Ошибка!";
                state.IsClearNeeded = true;
            }
            else
            {
                state.RegisterX = Math.Tan(originalValue * Math.PI / 180);
                state.IsClearNeeded = true;

                RecordHistory(state, $"tg({originalValue})", state.RegisterX);
            }
        }

        public void PressPercent(CalculatorState state)
        {
            double originalValue = state.RegisterX;

            state.RegisterX /= 100;
            state.IsClearNeeded = true;
            RecordHistory(state, $"{originalValue}%", state.RegisterX);
        }

        public void PressUpdateSign(CalculatorState state)
        {
            double originalValue = state.RegisterX;

            state.RegisterX = -originalValue;
            state.IsClearNeeded = true;
            RecordHistory(state, $"+/-({originalValue})", state.RegisterX);
        }

        public void PressSqrt(CalculatorState state)
        {
            double originalValue = state.RegisterX;

            if (state.RegisterX < 0)
            {
                state.ErrorMessage = "Ошибка!";
                state.IsClearNeeded = true;
            }
            else
            {
                state.RegisterX = Math.Sqrt(originalValue);
                state.IsClearNeeded = true;

                RecordHistory(state, $"sqrt({originalValue})", state.RegisterX);
            }
        }

        public void PressDegree(CalculatorState state)
        {
            double originalValue = state.RegisterX;

            state.RegisterX = Math.Pow(originalValue, 2);
            state.IsClearNeeded = true;
            RecordHistory(state, $"({originalValue})^2", state.RegisterX);
        }

        public void RecordHistory(CalculatorState state, string expression, double result)
        {
            state.History.Add($"{expression} = {result}");
        }
    }
}
