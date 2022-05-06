using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Data.Entity;


namespace i1
{
    public partial class Form1 : Form
    {
        int r=1;
        public Form1()
        {
            InitializeComponent();
            using (var db = new WordListContext())
            {
                var word1 = new WordList {  Eng = "apple", Chi = "苹果" };
                var word2 = new WordList { Eng = "banana", Chi = "香蕉" };
                var word3 = new WordList { Eng = "abandon", Chi = "抛弃" };
                var word4 = new WordList { Eng = "happy", Chi = "快乐的" };

                db.wordlist.Add(word1);
                db.wordlist.Add(word2);
                db.wordlist.Add(word3);
                db.wordlist.Add(word4);

                db.SaveChanges();
            }
        }
        /*
        private static void InsertWord(string e,string c)
        {
            using (MySqlConnection connection = GetConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand
                ("INSERT INTO WordList(Eng,Chi) VALUES(@Eng,@Chi)", connection))
                {
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@Eng",e);
                    cmd.Parameters.AddWithValue("@Chi",c);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static MySqlConnection GetConnection()
        {
            string conString = "data source=localhost;initial catalog=WordList;user id=root;password=123456";// 数据库连接字符串
            MySqlConnection connection = new MySqlConnection(conString);
            connection.Open();
            return connection;
        }
        */
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            using (var context = new WordListContext()) {
                var cur_word = context.wordlist.SingleOrDefault(w => w.WordListId== r);
                if (cur_word != null)
                {
                    label3.Text = cur_word.Eng;
                    if (textBox1.Text == cur_word.Eng)
                    {
                        label3.Text = "正确!";
                    }
                    else label3.Text = "错误";
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var context = new WordListContext())
            {
                var cur_word = context.wordlist.SingleOrDefault(w => w.WordListId == r);
                textBox2.Text = cur_word.Chi;
            }
                label3.Text = "";
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (r < 4)
            {
                r++;
                using (var context = new WordListContext())
                {
                    var cur_word = context.wordlist.SingleOrDefault(w => w.WordListId == r);
                    textBox2.Text = cur_word.Chi;
                }
                label3.Text = "";
            }
            else MessageBox.Show("结束学习");
        }
    }

    public class WordList
    {
        public int WordListId { get; set; }
        public string Eng { set; get; }
        public string Chi { set; get; }
    }
    public class WordListContext : DbContext
    {
        public WordListContext() : base("WordList") {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<WordListContext>());
        }
        public DbSet<WordList> wordlist { set; get; }
    }
}
