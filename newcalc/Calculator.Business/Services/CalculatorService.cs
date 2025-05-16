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
            if (!byte.TryParse(digitText, out var digit))
            {
                return;
            }

            if (state.NeedClearX)
            {
                state.RegisterX = 0;
            }

            state.RegisterX = state.RegisterX * 10 + digit;
            state.NeedClearX = false;
        }
        bool Radiance = false;

        public void Clear(CalculatorState state)
        {
            state.RegisterX = 0;
            state.Operation = string.Empty;
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

        public void PressSin(CalculatorState state)
        {
            double angle = Radiance ? state.RegisterX : state.RegisterX * Math.PI / 180;
            state.RegisterX = Math.Sin(angle);
            state.NeedClearX = true;

        }

        public void Back(CalculatorState state)
        {
            if (state.RegisterX == 0)
                return;


            if (state.RegisterX % 1 == 0)
            {
                state.RegisterX = (int)(state.RegisterX / 10);
            }

            else
            {
                string numStr = state.RegisterX.ToString();
                numStr = numStr.Substring(0, numStr.Length - 1);

                if (string.IsNullOrEmpty(numStr) || numStr == "-")
                    numStr = "0";

                if (double.TryParse(numStr, out double result))
                {
                    state.RegisterX = result;
                }
            }

            
        }


        public void Cos(CalculatorState state)
        {
            double angle = Radiance ? state.RegisterX : state.RegisterX * Math.PI / 180;
            state.RegisterX = Math.Cos(angle);
            state.NeedClearX = true;


        }

        public void Log(CalculatorState state)
        {
            
            
            double originalValue = state.RegisterX;
            state.RegisterX = Math.Log(originalValue);
                

            
        }

        public void Tan(CalculatorState state)
        {
            double angle = Radiance ? state.RegisterX : state.RegisterX * Math.PI / 180;
            state.RegisterX = Math.Tan(angle);
            state.NeedClearX = true;




        }

        public void Percent(CalculatorState state)
        {
            if (state.NeedClearX)
                return;

            switch (state.Operation)
            {
                case "+":
                case "-":
                    
                    state.RegisterX = state.RegisterY * (state.RegisterX / 100);
                    break;

                case "*":
                case "/":
                   
                    state.RegisterX /= 100;
                    break;

                default:
                    
                    state.RegisterX /= 100;
                    break;
            }

            state.NeedClearX = true;
        }
        public void Sqrt(CalculatorState state)
        {
            double originalValue = state.RegisterX;

            
                state.RegisterX = Math.Sqrt(originalValue);
                


            
        }

        public void Degree(CalculatorState state)
        {
            double originalValue = state.RegisterX;

            state.RegisterX = Math.Pow(originalValue, 2);
            

        }

        public void change()
        {
            if(Radiance == true)
            {
                Radiance = false;

            }
            else
            {
                Radiance = true;
            }
        }
    }


}


