using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace calculator2
{
    public partial class Form1 : Form
    {
        string num1, num2,op;
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            num2=textBox2.Text;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            num1 = textBox1.Text;
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            comboBox1.Text = "";
            textBox3.Text = "";
            
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            op = comboBox1.Text.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        { 
            int n;
            int m;
            bool judge1 = Int32.TryParse(num1, out n);
            bool judge2 = Int32.TryParse(num2, out m);
            if (judge1 && judge2)
            {
               
                if (op == "+") textBox3.Text= (m + n).ToString();
                else if (op == "-") textBox3.Text = (m - n).ToString();
                else if (op == "*") textBox3.Text = (m * n).ToString();
                else if (op == "/") textBox3.Text = (m / n).ToString();
                else
                {
                    textBox3.Text = "请输入'+''-''*''/'中的运算符";
                    
                }
            }
            else
            {
                textBox3.Text = "请正确输入数字";
                
            }

            
               
        }

    }
}
