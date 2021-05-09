using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Warehouse.Forms;
using FontAwesome.Sharp;

namespace Warehouse
{
    public partial class Inpatient : Form
    {
        private IconButton currentBtn;
        private Panel leftBorderBtn;
        private Form currentChildForm;
        public static DataGridView dataCategories;
        public Inpatient()
        {
            InitializeComponent();
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 60);
            panelMenu.Controls.Add(leftBorderBtn);
        }

        private void Inpatient_Load(object sender, EventArgs e)
        {

        }

        private static Color[] palette = new Color[]
        {
            Color.FromArgb(255, 159, 243),
            Color.FromArgb(254, 202, 87),
            Color.FromArgb(255, 107, 107),
            Color.FromArgb(72, 219, 251),
            Color.FromArgb(29, 209, 161),
            Color.FromArgb(0, 210, 211),
            Color.FromArgb(84, 160, 255),
            Color.FromArgb(95, 39, 205)
        };

        int paletteColors = palette.Length;
        int prevColor = -1;

        private void ActivateButton(object senderBtn)
        {
            int currentColor = new Random().Next(paletteColors);
            while (prevColor == currentColor)
            {
                currentColor = new Random().Next(paletteColors);
            }
            Color color = palette[currentColor];
            prevColor = currentColor;
            if (senderBtn != null)
            {
                DisableButton();
                //Button
                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = Color.FromArgb(34, 47, 62);
                currentBtn.ForeColor = color;
                currentBtn.IconColor = color;
                //Left border button
                leftBorderBtn.BackColor = color;
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();
            }
        }
        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.FromArgb(34, 47, 62);
                currentBtn.ForeColor = Color.FromArgb(200, 214, 229);
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = Color.Gainsboro;
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }

        private void btnShippers_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            OpenChildForm(new FormShippers());
        }

        private void btnClients_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            OpenChildForm(new FormClients());
            OpenChildForm(new FormCategories());
        }

        private void btnCategories_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            OpenChildForm(new FormCategories());
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            DisableButton();
            leftBorderBtn.Visible = false;
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void OpenChildForm(Form childForm)
        {
            //open only form
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }
            currentChildForm = childForm;
            //End
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelForm.Controls.Add(childForm);
            panelForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            lblTitle.Text = currentBtn.Text;
        }

        private void Inpatient_SizeChanged(object sender, EventArgs e)
        {
            if (currentBtn != null)
            {
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
            }
        }

        private void btnFloors_Click(object sender, EventArgs e)
        {

        }
    }
}
