using System;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;


namespace h1
{
    public partial class Form1 : Form
    {
        string url;
        private delegate void MyDelegate();
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string keyword = textBox1.Text;
            url = "http://www.baidu.com/s?wd=" + keyword;
            Thread thread1 = new Thread(new ThreadStart(() => Find_text()));
            Thread thread2 = new Thread(new ThreadStart(() => Search()));
            CheckForIllegalCrossThreadCalls = false;
            try
            {   thread1.Start();
                label2.Text = "搜索中";
                thread2.Start();
                label3.Text = "搜索中";
            }
            catch (Exception err) { Console.WriteLine(err); }

        }
        //方法一（尽量获取正文部分）
        private void Find_text()
        {
            string SearchUrl = "http://www.baidu.com/baidu?wd=" + textBox1.Text;
            string source = GetMainContentHelper.getDataFromUrl(url);
            string contents = GetMainContentHelper.GetMainContent(source);

            string output;
            if (contents.Length > 200)
            {
                output = contents.Substring(0, 200);
            }
            else
            {
                output = contents.Substring(0, contents.Length);
            }
            Test.form1.label2.Text = "搜索结束";
            this.BeginInvoke(new MyDelegate(() => { Test.form1.textBox2.Text = output; Test.form1.label2.Text = "搜索结束"; }));
            Thread.Sleep(300);
        }
        //方法二
        private void Search()       
        {
            string text = "";
            //String url = "http://www.baidu.com/baidu?wd=" + textBox1.Text;
            WebClient webClient = new WebClient();
            byte[] recvdata = webClient.DownloadData(url);       
            string response = Encoding.UTF8.GetString(recvdata); 
            foreach (char i in response)
            {
                if (i> 127)//默认最高位为1的都是汉字
                {
                    text += i;
                };
                if (text.Length == 200)
                    break;
            }
            this.BeginInvoke(new MyDelegate(() => { Test.form1.textBox3.Text = text; Test.form1.label3.Text = "搜索结束"; }));
            Thread.Sleep(300);
        }
  
    }
}
