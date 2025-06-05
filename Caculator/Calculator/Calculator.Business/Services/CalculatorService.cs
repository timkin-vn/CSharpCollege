using Calculator.Business.Models;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Business.Services
{
    public class CalculatorService
    {
        public void InsertDigit(CalculatorState state, string digitString)
        {
            var digit = byte.Parse(digitString);
            if (state.NeedClear)
            {
                state.XRegister = 0;
            }

            state.XRegister = state.XRegister * 10 + digit;
            state.NeedClear = false;
        }

        public void Clear(CalculatorState state)
        {
            state.XRegister = 0;
        }

        public void PressOperation(CalculatorState state, string opCode)
        {
            MakeOperation(state, state.OpCodeRegister);
            MoveXToY(state);
            state.OpCodeRegister = opCode;
            state.NeedClear = true;
        }

        private void MoveXToY(CalculatorState state)
        {
            state.YRegister = state.XRegister;
        }
        public void DegretOperation(CalculatorState state)
        {

            state.XRegister = state.XRegister * state.XRegister;

            state.NeedClear = true;

        }   
        public void RowOperation(CalculatorState state)
        {
            const double precision = 0.000001;
            double row_X = state.XRegister / 2;
                  
            while (Math.Abs(row_X * row_X - state.XRegister) > precision)
            {
                row_X = (row_X + state.XRegister / row_X) / 2;
            }

            state.XRegister = row_X;

            state.NeedClear = true;

        }
        public void  ABSOperation(CalculatorState state) { 
            if (state.XRegister >= 0) 
            {
                state.XRegister = state.XRegister;
            } 
            else
            {
                
                    state.XRegister =-state.XRegister;
                
            }
            state.NeedClear = true;
        } 
        public void  Negativ_and_Positiv_Operation(CalculatorState state) { 
            if (state.XRegister >= 0 || state.XRegister <0 ) 
            {
                state.XRegister = -state.XRegister;
            } 
            
            state.NeedClear = true;
        }
        private void MakeOperation(CalculatorState state, string opCode)
        {
            switch (opCode)
            {
                case "+":
                    state.XRegister += state.YRegister;
                    break;
                case "-":
                    state.XRegister = state.YRegister - state.XRegister;
                    break;
                case "*":
                    state.XRegister *= state.YRegister;
                    break;
                case "/":
                    if (state.XRegister == 0)
                    {
                        state.XRegister = double.NaN;
                        break;
                    }
                    state.XRegister = state.YRegister / state.XRegister;
                    break;
                    /* case "X^Y":
                     *    const start_XRegister= state.YRegister 
                         if (state.YRegister == 0)
                         {
                             state.XRegister = 1;
                             break;
                         }
                         for (int i = 0; i <= state.XRegister; i++)
                         {
                             state.YRegister *= start_XRegister;
                         }
                        state.XRegister =   state.YRegister
                         break;*/


            }
        }
    }
}
