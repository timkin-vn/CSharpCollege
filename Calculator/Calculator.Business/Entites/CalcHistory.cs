using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Business.Entites
{
    public class CalcHistory
    {

        public CalcHistory() { }

        public CalcHistory(int first, int last, double result, char symbol) {

            this.first = first;
            this.last = last;
            this.result = result;
            this.symbol = symbol;

        }

        public CalcHistory(CalcHistory a)
        {
            this.first = a.first;
            this.last = a.last;
            this.result = a.result;
            this.symbol = a.symbol;
        }

        public double first { get; set; }
        public double last { get; set; }
        public double result { get; set; }
        public char symbol { get; set; }


        public String toString()
        {

            StringBuilder sb = new StringBuilder();

            sb.Append(last.ToString());
            sb.Append(" " + symbol + " ");
            sb.Append(first.ToString());
            sb.Append(" = ");
            sb.Append(result.ToString());

            return sb.ToString();

        }

        public void Copy(CalcHistory a){

            this.first = a.first;
            this.last = a.last;
            this.result = a.result;
            this.symbol = a.symbol;


        }


        public void Clear()
        {
            this.first = 0;
            this.last = 0;
            this.result = 0;
            this.symbol = '0';
        }


    }
}
