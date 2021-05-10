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

namespace Warehouse.Forms
{
    public partial class FormClients : Form
    {
        public FormClients()
        {
            InitializeComponent();
            loadData();
            comboBoxSearch.Items.Add("Название");
            comboBoxSearch.Items.Add("Категория");
            comboBoxSearch.SelectedIndex = 0;
            comboBoxCategory.Items.Clear();
            for (int i = 0; i < Inpatient.dataCategories.RowCount - 1; i++)
            {
                comboBoxCategory.Items.Add(Inpatient.dataCategories[1, i].Value.ToString());
                categoryIDs[i] = Inpatient.dataCategories[0, i].Value.ToString();

            }
        }
        private string[] categoryIDs = new string[Inpatient.dataCategories.RowCount - 1];
        private string id = "";
        private int intRow = 0;

        private void loadData()
        {
            loadData("SELECT * FROM Clients ORDER BY Номер::int");
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

                dgv1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dgv1.Columns[0].Width = 55;
                dgv1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgv1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgv1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            catch
            {
                return;
            }
        }

        private string loadData2(string query)
        {

            Classes.Database.sql = query;

            string strKeyword = string.Format("%{0}%", "");

            Classes.Database.cmd = new NpgsqlCommand(Classes.Database.sql, Classes.Database.con);
            Classes.Database.cmd.Parameters.Clear();
            Classes.Database.cmd.Parameters.AddWithValue("keyword", strKeyword);

            DataTable dt = Classes.Database.PerformDatabase(Classes.Database.cmd);

            string yes = dt.Rows[0].ItemArray[0].ToString();

            return yes;
        }
        private void addParameters(string str)
        {
            Classes.Database.cmd.Parameters.Clear();
            Classes.Database.sql = "SELECT * FROM view_categories ORDER BY номер)";
            try
            {
                Classes.Database.cmd.Parameters.AddWithValue("product_name", textName.Text.Trim());
                Classes.Database.cmd.Parameters.AddWithValue("category_id", categoryIDs[comboBoxCategory.SelectedIndex]);
                Classes.Database.cmd.Parameters.AddWithValue("units_in_stock", Convert.ToInt32(textUnits.Text.Trim()));
            }
            catch { }
            if (str == "Update" || str == "Delete" && !string.IsNullOrEmpty(this.id))
            {
                Classes.Database.cmd.Parameters.AddWithValue("product_id", this.id);
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
            if (string.IsNullOrEmpty(textName.Text.Trim()) || string.IsNullOrEmpty(textUnits.Text.Trim()) || comboBoxSearch.SelectedIndex == -1)
            {
                MessageBox.Show("Заполните все поля", "Вставить",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Classes.Database.sql = "SELECT cre_products(@product_name, @category_id::smallint, @units_in_stock::int)";

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
            textUnits.Text = Convert.ToString(dgv1.CurrentRow.Cells[3].Value);
            string tmp = loadData2($"SELECT get_category_id({dgv1.CurrentRow.Cells[0].Value})");
            for (int i = 0; i < comboBoxCategory.Items.Count; i++)
            {
                if (categoryIDs[i] == tmp)
                {
                    comboBoxCategory.SelectedIndex = i;
                    return;
                }
            }
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

            if (string.IsNullOrEmpty(textName.Text.Trim()) || string.IsNullOrEmpty(textUnits.Text.Trim()) || comboBoxSearch.SelectedIndex == -1)
            {
                MessageBox.Show("Заполните все поля", "Изменить",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Classes.Database.sql = "UPDATE products SET product_name = @product_name, category_id = @category_id::smallint, units_in_stock = @units_in_stock WHERE product_id = @product_id::smallint";

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

                Classes.Database.sql = "SELECT del_products(@product_id::smallint)";

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
                loadData($"SELECT * FROM sea_products('{textSearch.Text.Trim()}', {comboBoxSearch.SelectedIndex})");
            }
        }
    }
}
