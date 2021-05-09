namespace Journal
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.formPanel = new System.Windows.Forms.Panel();
            this.JournalGridView = new System.Windows.Forms.DataGridView();
            this.subjectsTreeView = new System.Windows.Forms.TreeView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.topPanel = new System.Windows.Forms.Panel();
            this.formPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.JournalGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // formPanel
            // 
            this.formPanel.Controls.Add(this.JournalGridView);
            this.formPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formPanel.Location = new System.Drawing.Point(0, 0);
            this.formPanel.Margin = new System.Windows.Forms.Padding(4);
            this.formPanel.Name = "formPanel";
            this.formPanel.Size = new System.Drawing.Size(784, 561);
            this.formPanel.TabIndex = 0;
            // 
            // JournalGridView
            // 
            this.JournalGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.JournalGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.JournalGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            this.JournalGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.JournalGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.JournalGridView.Location = new System.Drawing.Point(242, 79);
            this.JournalGridView.Name = "JournalGridView";
            this.JournalGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.JournalGridView.RowHeadersVisible = false;
            this.JournalGridView.Size = new System.Drawing.Size(530, 273);
            this.JournalGridView.TabIndex = 0;
            // 
            // subjectsTreeView
            // 
            this.subjectsTreeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.subjectsTreeView.Dock = System.Windows.Forms.DockStyle.Left;
            this.subjectsTreeView.Location = new System.Drawing.Point(0, 72);
            this.subjectsTreeView.Margin = new System.Windows.Forms.Padding(4);
            this.subjectsTreeView.Name = "subjectsTreeView";
            this.subjectsTreeView.Size = new System.Drawing.Size(234, 489);
            this.subjectsTreeView.TabIndex = 1;
            this.subjectsTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.subjectsTreeView_AfterSelect);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // topPanel
            // 
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(784, 72);
            this.topPanel.TabIndex = 2;
            this.topPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.topPanel_MouseDown);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.subjectsTreeView);
            this.Controls.Add(this.topPanel);
            this.Controls.Add(this.formPanel);
            this.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainForm";
            this.Text = "Journal";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainForm_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseDown);
            this.formPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.JournalGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel formPanel;
        private System.Windows.Forms.TreeView subjectsTreeView;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.DataGridView JournalGridView;
    }
}

