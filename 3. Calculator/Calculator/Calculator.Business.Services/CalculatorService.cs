using Calculator.Business.Models;
using System;

namespace Calculator.Business.Services
{
    public class CalculatorService
    {
        public void RoundDown(CalculatorState state)
        {
            state.RegisterX = Math.Floor(state.RegisterX * 10) / 10;
        }

        public void RoundUp(CalculatorState state)
        {
            state.RegisterX = Math.Ceiling(state.RegisterX * 10) / 10;
        }
    }
}

