using Calculator.Business.Entites;
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
    public partial class history : Form
    {

        public static List<CalcHistory> temp_history = new List<CalcHistory>();



        public history()
        {
            InitializeComponent();
        }

        public void Sort()
        {
            for(int i = 1; i < 7; i++)
            {
                temp_history[i - 1] = temp_history[i];
                
            }
            temp_history.RemoveAt(6);
        }
        




    }
}
