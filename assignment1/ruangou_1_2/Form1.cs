using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ruangou_1_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //textBox1 comboBox1 textBox2 button1 textBox3
            double num1 = Int32.Parse(textBox1.Text);
            double num2 = Int32.Parse(textBox2.Text);
            string op = comboBox1.Text;
            double res = 0.00;
            switch (op)
            {

                case "+":
                    res = num1 + num2;
                    break;
                case "-":
                    res = num1 - num2;
                    break;
                case "*":
                    res = num1 * num2;
                    break;
                case "/":
                    res = num1 / num2;
                    break;
            }
            textBox3.Text = String.Format("{0:F2}", res);
        }
    }
}
