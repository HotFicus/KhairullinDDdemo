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
    public partial class userWindow : Form
    {
        BDconnect dataBase = new BDconnect();
        public userWindow()
        {
            InitializeComponent();
        }
        private void CreateColumns()
        {
            dataGridView1.Columns.Add("id", "id");
            dataGridView1.Columns.Add("artikul", "Артикул");
            dataGridView1.Columns.Add("naimen", "Наименование");
            dataGridView1.Columns.Add("edinitsa", "Единица измерения");
            dataGridView1.Columns.Add("price", "Стоимость");
            dataGridView1.Columns.Add("maxsell", "Размер максимальной скидки");
            dataGridView1.Columns.Add("proizv", "Производитель");
            dataGridView1.Columns.Add("post", "Поставщик");
            dataGridView1.Columns.Add("kategory", "Категория товара");
            dataGridView1.Columns.Add("sellact", "Действующая скидка");
            dataGridView1.Columns.Add("sklad", "Кол-во на складе");
            dataGridView1.Columns.Add("opis", "Описание");
            dataGridView1.Columns.Add("IsNew", String.Empty);
            dataGridView1.Columns[12].Visible = false;
            ;
        }
        private void ReadSingleRow(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetInt32(0),
                         record.GetString(1),
                         record.GetString(2),
                         record.GetString(3),
                         record.GetInt32(4),
                         record.GetInt32(5),
                         record.GetString(6),
                         record.GetString(7),
                         record.GetString(8),
                         record.GetInt32(9),
                         record.GetInt32(10),
                         record.GetString(11),
                         RowState.ModifiedNew);
        }
        private void RefreshDataGrid(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string queryString = $"select * from tovary";

            SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());

            dataBase.openConnection();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(dgw, reader);
            }
            reader.Close();
        }
        private void Search(DataGridView dgw)
        {
            dgw.Rows.Clear();
            string searchString = $"select * from tovary where concat(id, artikul, naimen, edinitsa, price, maxsell, proizv, post, kategory, sellact, sklad, opis) like '%" + textsearh.Text + "%'";

            SqlCommand com = new SqlCommand(searchString, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader read = com.ExecuteReader();

            while (read.Read())
            {
                ReadSingleRow(dgw, read);
            }
            read.Close();
        }
        private void userWindow_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshDataGrid(dataGridView1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoginWindow LoginWindow = new LoginWindow();
            this.Hide();
            LoginWindow.ShowDialog();
            this.Show();
            Close();
        }

        private void textsearh_TextChanged(object sender, EventArgs e)
        {
            Search(dataGridView1);
        }
    }
}
