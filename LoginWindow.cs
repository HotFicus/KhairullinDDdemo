using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace KhairullinDDdemo
{
    public partial class LoginWindow : Form
    {
        BDconnect dataBase = new BDconnect();
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginWindow_Load(object sender, EventArgs e)
        {

        }

      

        private void button1_Click(object sender, EventArgs e)
        {
            var famalia = textBox1.Text;
            var imaotch = textBox2.Text;
            var password = textBox3.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string quyerystring = $"select familia, imaOtchestvo, Password from Users where familia ='{famalia}' and imaOtchestvo = '{imaotch}' and Password = '{password}'";
            SqlCommand command = new SqlCommand(quyerystring, dataBase.getConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count == 1)
            {
                adminWindow adminWindow = new adminWindow();
                this.Hide();
                adminWindow.ShowDialog();
                this.Show();
                Close();
            }
            else
            {
                MessageBox.Show("НЕ ПРАВИЛЬНО ВВЕДЕНЫ ДАННЫЕ!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            userWindow userWindow = new userWindow();
            this.Hide();
            userWindow.ShowDialog();
            this.Show();
            Close();
           
        }
    }
}
