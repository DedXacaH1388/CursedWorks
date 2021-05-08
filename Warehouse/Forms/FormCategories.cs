using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace Warehouse
{
    public partial class FormCategories : Form
    {
        public FormCategories()
        {
            InitializeComponent();
            loadData();
        }

        

        private string id = "";
        private int intRow = 0;
        private void loadData()
        {
            loadData("SELECT* FROM view_categories ORDER BY Номер::int");
        }
        
        private void loadData(string query)
        {

            Classes.Database.sql = query;

            string strKeyword = string.Format("%{0}%", "");

            Classes.Database.cmd = new NpgsqlCommand(Classes.Database.sql, Classes.Database.con);
            Classes.Database.cmd.Parameters.Clear();
            Classes.Database.cmd.Parameters.AddWithValue("keyword", strKeyword);

            DataTable dt = Classes.Database.PerformDatabase(Classes.Database.cmd);

            if (dt.Rows.Count > 0)
            {
                intRow = Convert.ToInt32(dt.Rows.Count.ToString());
            }
            else
            {
                intRow = 0;
            }
            try
            {
                DataGridView dgv1 = dataGridView1;
                dgv1.MultiSelect = false;
                dgv1.AutoGenerateColumns = true;
                dgv1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                dgv1.DataSource = dt;

                Warehouse.dataCategories = dgv1;

                dgv1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dgv1.Columns[0].Width = 55;
                dgv1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            catch
            {
                return;
            }
        }
        private void addParameters(string str)
        {
            Classes.Database.cmd.Parameters.Clear();
            Classes.Database.sql = "SELECT * FROM view_categories ORDER BY Номер)";
            Classes.Database.cmd.Parameters.AddWithValue("category_name", textName.Text.Trim());

            if (str == "Update" || str == "Delete" && !string.IsNullOrEmpty(this.id))
            {
                Classes.Database.cmd.Parameters.AddWithValue("category_id", this.id);
            }
        }
        private void execute(string mySQL, string param)
        {
            Classes.Database.cmd = new NpgsqlCommand(mySQL, Classes.Database.con);
            addParameters(param);
            Classes.Database.PerformDatabase(Classes.Database.cmd);
        }
        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textName.Text.Trim()))
            {
                MessageBox.Show("Поле пустое", "Введите название категории",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Classes.Database.sql = "SELECT cre_categories(@category_name)";

            execute(Classes.Database.sql, "Insert");

            MessageBox.Show("Успех!", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            loadData();


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv1 = dataGridView1;

            id = Convert.ToString(dgv1.CurrentRow.Cells[0].Value);

            textName.Text = Convert.ToString(dgv1.CurrentRow.Cells[1].Value);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                return;
            }

            if (string.IsNullOrEmpty(id))
            {
                MessageBox.Show("Выберите строку для изменения", "Изменить",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (string.IsNullOrEmpty(textName.Text.Trim()))
            {
                MessageBox.Show("Обнаружена пустая строка", "Изменить",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Classes.Database.sql = "UPDATE categories SET category_name = @category_name WHERE category_id = @category_id::smallint";

            execute(Classes.Database.sql, "Update");

            MessageBox.Show("Успех!", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            loadData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                return;
            }

            if (string.IsNullOrEmpty(this.id))
            {
                MessageBox.Show("Выберите запить", "Удалить",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (MessageBox.Show("Удалить выбранную запись?", "Удалить",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {

                Classes.Database.sql = "SELECT del_categories(@category_id::smallint)";

                execute(Classes.Database.sql, "Update");

                MessageBox.Show("Успех!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                loadData();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textSearch.Text.Trim()))
            {
                loadData();
            }
            else
            {
                loadData($"SELECT * FROM sea_categories('{textSearch.Text.Trim()}')");
            }
        }
    }
}
