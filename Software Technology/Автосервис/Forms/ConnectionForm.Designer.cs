
namespace carService
{
    partial class connectionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.hostBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.portBox = new System.Windows.Forms.TextBox();
            this.dbComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // hostBox
            // 
            this.hostBox.Location = new System.Drawing.Point(107, 26);
            this.hostBox.Name = "hostBox";
            this.hostBox.Size = new System.Drawing.Size(100, 20);
            this.hostBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Host";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Database";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(107, 156);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(105, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Check connection";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Port";
            // 
            // portBox
            // 
            this.portBox.Location = new System.Drawing.Point(107, 52);
            this.portBox.Name = "portBox";
            this.portBox.Size = new System.Drawing.Size(100, 20);
            this.portBox.TabIndex = 9;
            // 
            // dbComboBox
            // 
            this.dbComboBox.FormattingEnabled = true;
            this.dbComboBox.Items.AddRange(new object[] {
            "car_service",
            "hospital"});
            this.dbComboBox.Location = new System.Drawing.Point(107, 80);
            this.dbComboBox.Name = "dbComboBox";
            this.dbComboBox.Size = new System.Drawing.Size(100, 21);
            this.dbComboBox.TabIndex = 11;
            // 
            // connectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(216, 184);
            this.Controls.Add(this.dbComboBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.portBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.hostBox);
            this.Name = "connectionForm";
            this.Text = "Connection Form";
            this.Load += new System.EventHandler(this.Form4_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.TextBox hostBox;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox portBox;
        public System.Windows.Forms.ComboBox dbComboBox;
    }
}