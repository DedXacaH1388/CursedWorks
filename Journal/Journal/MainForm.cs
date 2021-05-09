using Newtonsoft.Json;
using Npgsql;
using System;
using System.IO;
using System.Windows.Forms;
using Journal.Forms;
using Journal.Classes;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Drawing;
using FontAwesome.Sharp;
using Icon = System.Drawing.Icon;
using System.Linq;

namespace Journal
{
    public partial class MainForm : Form
    {
        private Form currentChildForm;
        ConnectionForm conForm = new ConnectionForm();
        LoginForm logForm = new LoginForm();

        
        List<int>[] subject_ids;
        List<string>[] subject_names;
        List<int> class_id = new List<int>();
        List<string> class_name = new List<string>();
        List<int> member_id = new List<int>();
        List<string> member_name = new List<string>();

        //used to open forms in panel
        private void OpenChildForm(Form childForm)
        {
            //if form is opened then close it
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }

            currentChildForm = childForm;

            //form styles
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            //assign form to panel
            formPanel.Controls.Add(childForm);
            formPanel.Tag = childForm;

            //show form
            childForm.BringToFront();
            childForm.Show();
        }

        private void test()
        {
            subjectsTreeView.Nodes.Clear();
            subjectsTreeView.Nodes.Add("gay");
        }

        private bool LoadTree()
        {
            int i = 0;
            try
            {
                this.subjectsTreeView.Nodes.Clear();

                using (var conn = new NpgsqlConnection(Info.connStr.ConnectionString))
                {
                    var clases = new List<string>();
                    conn.Open();
                    using (var command = new NpgsqlCommand("SELECT * FROM get_classes(:_id);", conn))
                    {
                        command.Parameters.Clear();
                        if (Info._user == null) return false;
                        command.Parameters.AddWithValue("_id", Info._user.id);
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                class_id.Add(reader.GetInt32(0));
                                class_name.Add(reader.GetString(1));
                            }
                        }
                    }
                    subject_ids = new List<int>[class_id.Count];
                    for (int j = 0; j < subject_ids.Length; ++j)
                    {
                        subject_ids[j] = new List<int>();
                    }
                    subject_names = new List<string>[class_id.Count];
                    for (int j = 0; j < subject_names.Length; ++j)
                    {
                        subject_names[j] = new List<string>();
                    }
                    foreach (var _class in class_id)
                    {
                        using (var command = new NpgsqlCommand("SELECT * FROM get_subjects(:_id);", conn))
                        {
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("_id", _class);
                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    subject_ids[i].Add(reader.GetInt32(0));
                                    subject_names[i].Add(reader.GetString(1));
                                }
                            }
                        }
                        i++;
                    }
                    conn.Close();
                }

                foreach (var _class in class_name)
                    this.subjectsTreeView.Nodes.Add(_class);
                for (int j = 0; j < subject_names.Length; j++)
                {
                    foreach (var _subject in subject_names[j])
                    {
                        this.subjectsTreeView.Nodes[j].Nodes.Add(_subject);
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void LoadGrid()
        {

        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            timer1.Start();

            //form
            this.BackColor = Info.pal.text;
            this.ForeColor = Info.pal.text;

            //form panel
            formPanel.BackColor = Info.pal.background;
            formPanel.ForeColor = Info.pal.text;

            //top panel
            topPanel.BackColor = Info.pal.themeColor;
            topPanel.ForeColor = Info.pal.text; ;

            //tree
            subjectsTreeView.BackColor = Info.pal.backgroundAccent;
            subjectsTreeView.ForeColor = Info.pal.text;
            subjectsTreeView.LineColor = Info.pal.text;

            //grid
            JournalGridView.BackgroundColor = Info.pal.background;
            JournalGridView.ForeColor = Info.pal.text;
            JournalGridView.GridColor = Info.pal.background;
            JournalGridView.DefaultCellStyle.BackColor = Info.pal.background;
            JournalGridView.DefaultCellStyle.ForeColor = Info.pal.text;
            JournalGridView.DefaultCellStyle.SelectionBackColor = Info.pal.selected;
            JournalGridView.DefaultCellStyle.SelectionForeColor = Info.pal.selectedText;
            JournalGridView.AlternatingRowsDefaultCellStyle.BackColor = Info.pal.backgroundAccent;
            JournalGridView.AlternatingRowsDefaultCellStyle.ForeColor = Info.pal.text;
            JournalGridView.AlternatingRowsDefaultCellStyle.SelectionBackColor = Info.pal.selectedAccent;
            JournalGridView.AlternatingRowsDefaultCellStyle.SelectionForeColor = Info.pal.selectedTextAccent;
            JournalGridView.GridColor = Info.pal.selectedText;

            JournalGridView.ColumnHeadersDefaultCellStyle.BackColor = Info.pal.backgroundAccent;
            JournalGridView.ColumnHeadersDefaultCellStyle.ForeColor = Info.pal.text;
            JournalGridView.ColumnHeadersDefaultCellStyle.SelectionBackColor = Info.pal.selectedAccent;
            JournalGridView.ColumnHeadersDefaultCellStyle.SelectionForeColor = Info.pal.selectedTextAccent;
            JournalGridView.EnableHeadersVisualStyles = false;


        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            if (LoadTree())
            {
                timer1.Stop();
            }
        }

        private void subjectsTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            int nodeIndex = subjectsTreeView.SelectedNode.Index;
            if (subjectsTreeView.SelectedNode.Parent != null)
            {
                int parentIndex = subjectsTreeView.SelectedNode.Parent.Index; 
                var tmp1 = subject_ids[parentIndex];
                var tmp2 = subject_names[parentIndex];
                Info._subject = new Records(tmp1[nodeIndex], tmp2[nodeIndex]);
                Info._class = new Records(class_id[parentIndex], class_name[parentIndex]);

                member_id.Clear();
                member_name.Clear();

                using (var conn = new NpgsqlConnection(Info.connStr.ConnectionString))
                {
                    var clases = new List<string>();
                    conn.Open();
                    using (var command = new NpgsqlCommand("SELECT * FROM get_students(:_id);", conn))
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("_id", Info._subject.id);
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                member_id.Add(reader.GetInt32(0));
                                member_name.Add(reader.GetString(1));
                            }
                        }
                    }
                    conn.Close();
                }

                int i = 0;
                JournalGridView.Columns.Clear();
                JournalGridView.Columns.Add("Student", "Student");
                foreach (var _name in member_name)
                {
                    JournalGridView.Rows.Add();
                    JournalGridView.Rows[i].Cells[0].Value = _name;
                    i++;
                }
                JournalGridView.AutoResizeRowHeadersWidth(
                    DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);

                //MessageBox.Show(Convert.ToString(Info._subject.id) + ' ' + Info._subject.value, "info", MessageBoxButtons.OK);
                //MessageBox.Show(Convert.ToString(Info._class.id) + ' ' + Info._class.value, "info", MessageBoxButtons.OK);

                LoadGrid();
            }
            else
            {
                if (!subjectsTreeView.SelectedNode.IsExpanded)
                    subjectsTreeView.SelectedNode.Expand();
                else
                    subjectsTreeView.SelectedNode.Collapse();
            }

            subjectsTreeView.SelectedNode = null;
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void topPanel_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }


        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle, Info.pal.themeColor, ButtonBorderStyle.Solid);
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //private void JournalGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        //{
        //    if (e.RowIndex == -1 || e.RowIndex % 2 == 1)
        //    {
        //        using (Brush b = new SolidBrush(Info.pal.background))
        //            e.Graphics.FillRectangle(b, e.CellBounds);
        //    }
        //    else if (e.ColumnIndex == -1)
        //    {
        //        using (Brush b = new SolidBrush(Info.pal.backgroundAccent))
        //            e.Graphics.FillRectangle(b, e.CellBounds);
        //    }
        //        e.PaintContent(e.ClipBounds);
        //    if (e.RowIndex == -1)
        //        e.Graphics.DrawLine(new Pen(Info.pal.text), 0, e.CellBounds.Bottom - 1,
        //            e.CellBounds.Right, e.CellBounds.Bottom - 1);
        //    if (e.ColumnIndex == -1)
        //        e.Graphics.DrawLine(new Pen(Info.pal.text), e.CellBounds.Right - 1, 0,
        //            e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);
        //    e.Handled = true;

        //}
    }
}
