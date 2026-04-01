using Calculator.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

    namespace Calculator.Business.Services
    {
        public class CalculatorService
        {
            public void PressDigit(CalculatorModel model, string digitString)
            {
                if (!model.IsLastDigitPressed)
                {
                    
                    model.RegisterX = double.Parse(digitString);
                    model.IsLastDigitPressed = true;
                }
                else
                {
                    
                    string current = model.RegisterX.ToString();
                    if (double.TryParse(current + digitString, out double res))
                    {
                        model.RegisterX = res;
                    }
                }
            }

            public void PressOperation(CalculatorModel model, string op)
            {
                
                if (op == "x²")
                {
                    model.RegisterX = Math.Pow(model.RegisterX, 2);
                    model.IsLastDigitPressed = false;
                    return;
                }
                if (op == "√")
                {
                    model.RegisterX = Math.Sqrt(model.RegisterX);
                    model.IsLastDigitPressed = false;
                    return;
                }

                
                if (!string.IsNullOrEmpty(model.OperationCode) && model.RegisterY != null)
                {
                    CompleteOperation(model);
                }

                model.RegisterY = model.RegisterX;
                model.OperationCode = op;
                model.IsLastDigitPressed = false;
            }

            public void PressEqual(CalculatorModel model)
            {
                CompleteOperation(model);
                model.OperationCode = null;
                model.IsLastDigitPressed = false;
            }

            private void CompleteOperation(CalculatorModel model)
            {
                
                if (model.RegisterY == null || string.IsNullOrEmpty(model.OperationCode)) return;

                double x = model.RegisterX;
                double y = model.RegisterY.Value;

                switch (model.OperationCode)
                {
                    case "+": model.RegisterX = y + x; break;
                    case "-": model.RegisterX = y - x; break;
                    case "*": model.RegisterX = y * x; break;
                    case "/": model.RegisterX = x != 0 ? y / x : 0; break;
                    case "x^y": model.RegisterX = Math.Pow(y, x); break;
                    
                }

                model.RegisterY = null; 
            }

            public void PressClear(CalculatorModel model)
            {
                model.RegisterX = 0;
                model.RegisterY = null;
                model.OperationCode = null;
                model.IsLastDigitPressed = false;
            }

            public void PressComma(CalculatorModel model)
            {
                
                model.IsLastDigitPressed = true;
            }
        }
    }


