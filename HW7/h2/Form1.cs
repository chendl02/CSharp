using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;


namespace g2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool is_legal=true;
            string message = "";
            string ID = textBox1.Text;
            /*
                 [1-9]\d{5}                 前六位地区，非0打头

                 (18|19|20)\d{2}      出身年份，覆盖范围为 1800-2099 年

                 ((0[1-9])|(10|11|12))      月份，01-12月

                (([0-2][1-9])|10|20|30|31) 日期，01-31天

                  \d{3}[0-9Xx]：              顺序码三位 + 一位校验码

                 */
             string pattern_v2 = @"^[1 - 9]\d{5} (18 | 19 |20)\d{2} ((0[1 - 9]) | 10 | 11 | 12)(([0 - 2][1 - 9]) | 10 | 20 | 30 | 31)\d{3}[0 - 9Xx]$";
            if (Regex.IsMatch(ID, pattern_v2))
            {
                string year = Convert.ToString(ID[6]) + Convert.ToString(ID[7]) + Convert.ToString(ID[8]) + Convert.ToString(ID[9]);
                string month = Convert.ToString(ID[10]) + Convert.ToString(ID[11]);
                string day = Convert.ToString(ID[12]) + Convert.ToString(ID[13]);
                if (month == "04" || month == "06" || month == "09" || month == "11")
                {
                    if (day == "31") { is_legal = false; message = "出生日期不规范"; }
                }
                else if (month == "02")
                {
                    if (int.Parse(year) % 4 == 0 && int.Parse(day) > 29) { is_legal = false; message = "出生日期不规范"; }
                    else if (int.Parse(day) > 28) { is_legal = false; message = "出生日期不规范"; }
                }
            }
            else { is_legal = false; message = "不符合第二代身份证规范"; }

            if (is_legal == true) MessageBox.Show("身份证号码符合规定，验证通过");
            else MessageBox.Show(message);

        }
    }
}
