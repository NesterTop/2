using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        DataTable dt = new DataTable();
        SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-AVGELME\STP;Initial Catalog=root;Integrated Security=True");
        
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            connection.Open();

            bool loginInData = false;
            bool kvalRegistrPass = false;
            bool kvalSimvolPass = false;

            string login = textBox1.Text;
            string password = textBox2.Text;

            char[] registr = "QWERTYUIOPASDFGHJKLZXCVBNM".ToArray();
            char[] simvol = "@,!".ToArray();

            SqlCommand cmd = new SqlCommand($"select * from users where login = '{login}'", connection);
            dt.Load(cmd.ExecuteReader());

            if(dt.Rows.Count > 0)
            {
                loginInData = true;
            }

            foreach(char c in password)
            {
                foreach(char r in registr)
                {
                    if(c == r)
                    {
                        kvalRegistrPass = true;
                        break;
                    }
                }
                
                foreach (char s in simvol)
                {
                    if (c == s)
                    {
                        kvalSimvolPass = true;
                        break;
                    }
                }
            }

            if(password.Length == 5 && kvalRegistrPass && kvalSimvolPass && !loginInData)
            {
                SqlCommand icmd = new SqlCommand($"insert into users values('{login}', '{password}')", connection);
                icmd.ExecuteNonQuery();
                MessageBox.Show("Вы успешно зарегистрировались!");
            }

            else
            {
                MessageBox.Show("Пароль должен содержать буквы верхнего регистра и специальные символы, " +
                                "либо пользователь с таким логином уже существует");
            }

            connection.Close();
        }

    }
}
