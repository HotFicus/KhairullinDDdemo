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
    public partial class Add : Form
    {
        BDconnect dataBase = new BDconnect();
        public Add()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataBase.openConnection();
            var art = textBox1.Text;
            var naim = textBox2.Text;
            var edi = textBox3.Text;
            var price = textBox4.Text;
            var maxsell = textBox5.Text;
            var proizv = textBox6.Text;
            var post = textBox7.Text;
            var kat = textBox8.Text;
            var sela = textBox9.Text;
            var sklad = textBox10.Text;
            var opis = textBox11.Text;

            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "" && textBox7.Text != "" && textBox8.Text != "" && textBox9.Text != "" && textBox10.Text != "" && textBox11.Text != "")
            {
                var addQuery = $"insert into tovary(artikul, naimen, edinitsa, price, maxsell, proizv, post, kategory, sellact, sklad, opis) values ('{art}', '{naim}', '{edi}', '{price}', '{maxsell}', '{proizv}', '{post}', '{kat}', '{sela}', '{sklad}', '{opis}')";
                SqlCommand command = new SqlCommand(addQuery, dataBase.getConnection());
                command.ExecuteNonQuery();

                MessageBox.Show("Запись успешно добавлена!", "Добавлен", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            { MessageBox.Show("ПРОВЕРТЕ ЗАПОЛНЕНИЕ ПОЛЕЙ", "ОШИБКА", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            dataBase.closeConnection();

        }
    }
}
