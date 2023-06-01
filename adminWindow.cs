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
    enum RowState
    {
        Existed,
        New,
        Modified,
        ModifiedNew,
        Deleted
    }
    public partial class adminWindow : Form
    {
        BDconnect dataBase = new BDconnect();

        int selectedRow;
        public adminWindow()
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
        private void deleteRow()
        {
            int index = dataGridView1.CurrentCell.RowIndex;

            dataGridView1.Rows[index].Visible = false;

            if (dataGridView1.Rows[index].Cells[0].Value.ToString() == String.Empty)
            {
                dataGridView1.Rows[index].Cells[12].Value = RowState.Deleted;
                return;
            }
            dataGridView1.Rows[index].Cells[12].Value = RowState.Deleted;
            MessageBox.Show("Запись была успешно удалина!", "Выполнено", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        private void adminWindow_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshDataGrid(dataGridView1);
        }

        public void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Add Add = new Add();
            Add.Show();
            this.Show();
          
        }
        private void Updates()
        {
            dataBase.openConnection();

            for (int index = 0; index < dataGridView1.Rows.Count; index++)
            {
                var rowState = (RowState)dataGridView1.Rows[index].Cells[12].Value;

                if (rowState == RowState.Existed)
                    continue;


                if (rowState == RowState.Deleted)
                {
                    var id = Convert.ToString(dataGridView1.Rows[index].Cells[0].Value);
                    var deleteQuery = $"delete from tovary where id = '{id}'";
                    var comamnd = new SqlCommand(deleteQuery, dataBase.getConnection());

                    comamnd.ExecuteNonQuery();
                }
                if (rowState == RowState.Modified)
                {

                    var id = dataGridView1.Rows[index].Cells[0].Value.ToString();
                    var artikul = dataGridView1.Rows[index].Cells[1].Value.ToString();
                    var naimen = dataGridView1.Rows[index].Cells[2].Value.ToString();
                    var edinitsa = dataGridView1.Rows[index].Cells[3].Value.ToString();
                    var price = dataGridView1.Rows[index].Cells[4].Value.ToString();
                    var maxsell = dataGridView1.Rows[index].Cells[5].Value.ToString();
                    var proizv = dataGridView1.Rows[index].Cells[6].Value.ToString();
                    var post = dataGridView1.Rows[index].Cells[7].Value.ToString();
                    var kategory = dataGridView1.Rows[index].Cells[8].Value.ToString();
                    var sellact = dataGridView1.Rows[index].Cells[9].Value.ToString();
                    var sklad = dataGridView1.Rows[index].Cells[10].Value.ToString();
                    var opis = dataGridView1.Rows[index].Cells[11].Value.ToString();


                    var changeQuery = $"update tovary set artikul = '{artikul}', naimen = '{naimen}', edinitsa = '{edinitsa}', price = '{price}', maxsell = '{maxsell}', proizv = '{proizv}', post = '{post}', kategory = '{kategory}', sellact = '{sellact}', sklad = '{sklad}', opis = '{opis}' where id = '{id}'";

                    var command = new SqlCommand(changeQuery, dataBase.getConnection());
                    command.ExecuteNonQuery();
                }

            }
            dataBase.closeConnection();
        }
         private void Change()
        {

            var selectedRowIndex = dataGridView1.CurrentCell.RowIndex;

            var id = textBoxid.Text;
            var artikul = textBox1.Text;
            var naimen = textBox2.Text;
            var edinitsa = textBox3.Text;
            var price = textBox4.Text;
            var maxsell = textBox5.Text;
            var proizv = textBox6.Text;
            var post = textBox7.Text;
            var kategory = textBox8.Text;
            var sellact = textBox9.Text;
            var sklad = textBox10.Text;
            var opis = textBox11.Text;


            if (textBox2.Text!= "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "" && textBox7.Text != "" && textBox8.Text != "" && textBox9.Text != "" && textBox10.Text != "" && textBox11.Text != "")
            {

                dataGridView1.Rows[selectedRowIndex].SetValues(id, artikul, naimen, edinitsa, price, maxsell, proizv, post, kategory, sellact, sklad, opis);
                dataGridView1.Rows[selectedRowIndex].Cells[12].Value = RowState.Modified;
                MessageBox.Show("Перед сохранением, проверь изменения!", "ВНИМАНИЕ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Изменений не произошло!");
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textsearh_TextChanged(object sender, EventArgs e)
        {
            Search(dataGridView1);
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            deleteRow();
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];

                textBoxid.Text = row.Cells[0].Value.ToString();
                textBox1.Text = row.Cells[1].Value.ToString();
                textBox2.Text = row.Cells[2].Value.ToString();
                textBox3.Text = row.Cells[3].Value.ToString();
                textBox4.Text = row.Cells[4].Value.ToString();
                textBox5.Text = row.Cells[5].Value.ToString();
                textBox6.Text = row.Cells[6].Value.ToString();
                textBox7.Text = row.Cells[7].Value.ToString();
                textBox8.Text = row.Cells[8].Value.ToString();
                textBox9.Text = row.Cells[9].Value.ToString();
                textBox10.Text = row.Cells[10].Value.ToString();
                textBox11.Text = row.Cells[11].Value.ToString();

            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(dataGridView1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Updates();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Change();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            LoginWindow LoginWindow = new LoginWindow();
            this.Hide();
            LoginWindow.ShowDialog();
            this.Show();
            Close();
        }
    }
}
