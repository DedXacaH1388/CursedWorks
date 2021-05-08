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
    public partial class FormShippers : Form
    {
        public FormShippers()
        {
            InitializeComponent();
            loadData();
            comboBoxSearch.Items.Add("Компания");
            comboBoxSearch.Items.Add("Представитель");
            comboBoxSearch.Items.Add("Адрес");
            comboBoxSearch.Items.Add("Город");
            comboBoxSearch.Items.Add("Телефон");
            comboBoxSearch.Items.Add("Факс");
            comboBoxSearch.SelectedIndex = 0;
        }
        private string id = "";
        private int intRow = 0;

        private void loadData()
        {
            loadData("SELECT * FROM view_shippers ORDER BY Номер::int");
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
                dgv1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgv1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgv1.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            catch
            {
                return;
            }
        }
        private void addParameters(string str)
        {
            Classes.Database.cmd.Parameters.Clear();
            Classes.Database.sql = "SELECT * FROM view_categories ORDER BY номер)";
            try
            {
                Classes.Database.cmd.Parameters.AddWithValue("company_name", textName.Text.Trim());
                Classes.Database.cmd.Parameters.AddWithValue("contact_name", textContact.Text.Trim());
                Classes.Database.cmd.Parameters.AddWithValue("address", textAddress.Text.Trim());
                Classes.Database.cmd.Parameters.AddWithValue("city", textCity.Text.Trim());
                Classes.Database.cmd.Parameters.AddWithValue("phone", textPhone.Text.Trim());
                Classes.Database.cmd.Parameters.AddWithValue("fax", textFax.Text.Trim());
            }
            catch { }
            if (str == "Update" || str == "Delete" && !string.IsNullOrEmpty(this.id))
            {
                Classes.Database.cmd.Parameters.AddWithValue("shipper_id", this.id);
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
            if (string.IsNullOrEmpty(textName.Text.Trim()) || string.IsNullOrEmpty(textContact.Text.Trim()) || string.IsNullOrEmpty(textAddress.Text.Trim()) || string.IsNullOrEmpty(textCity.Text.Trim()) || string.IsNullOrEmpty(textPhone.Text.Trim()) || string.IsNullOrEmpty(textFax.Text.Trim()))
            {
                MessageBox.Show("Заполните все поля", "Вставить",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Classes.Database.sql = "SELECT cre_shippers(@company_name, @contact_name, @address, @city, @phone, @fax)";

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
            textContact.Text = Convert.ToString(dgv1.CurrentRow.Cells[2].Value);
            textAddress.Text = Convert.ToString(dgv1.CurrentRow.Cells[3].Value);
            textCity.Text = Convert.ToString(dgv1.CurrentRow.Cells[4].Value);
            textPhone.Text = Convert.ToString(dgv1.CurrentRow.Cells[5].Value);
            textFax.Text = Convert.ToString(dgv1.CurrentRow.Cells[6].Value);
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

            if (string.IsNullOrEmpty(textName.Text.Trim()) || string.IsNullOrEmpty(textContact.Text.Trim()) || string.IsNullOrEmpty(textAddress.Text.Trim()) || string.IsNullOrEmpty(textCity.Text.Trim()) || string.IsNullOrEmpty(textPhone.Text.Trim()) || string.IsNullOrEmpty(textFax.Text.Trim()))
            {
                MessageBox.Show("Заполните все поля", "Изменить",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Classes.Database.sql = "UPDATE shippers SET company_name = @company_name, contact_name = @contact_name, address = @address, city = @city, phone = @phone, fax = @fax WHERE shipper_id = @shipper_id::smallint";

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

                Classes.Database.sql = "SELECT del_shippers(@shipper_id::smallint)";

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
                loadData($"SELECT * FROM sea_shippers('{textSearch.Text.Trim()}', {comboBoxSearch.SelectedIndex})");
            }
        }
    }
}
