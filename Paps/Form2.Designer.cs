namespace Paps
{
    partial class Tables
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Tables));
            this.listView1 = new System.Windows.Forms.ListView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.but_add = new System.Windows.Forms.ToolStripButton();
            this.but_redact = new System.Windows.Forms.ToolStripButton();
            this.but_delete = new System.Windows.Forms.ToolStripButton();
            this.but_texbox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(0, 27);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(594, 341);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.but_add,
            this.but_redact,
            this.but_delete,
            this.but_texbox,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(594, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // but_add
            // 
            this.but_add.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.but_add.Image = ((System.Drawing.Image)(resources.GetObject("but_add.Image")));
            this.but_add.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.but_add.Name = "but_add";
            this.but_add.Size = new System.Drawing.Size(63, 22);
            this.but_add.Text = "Добавить";
            this.but_add.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // but_redact
            // 
            this.but_redact.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.but_redact.Image = ((System.Drawing.Image)(resources.GetObject("but_redact.Image")));
            this.but_redact.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.but_redact.Name = "but_redact";
            this.but_redact.Size = new System.Drawing.Size(91, 22);
            this.but_redact.Text = "Редактировать";
            this.but_redact.Click += new System.EventHandler(this.but_redact_Click);
            // 
            // but_delete
            // 
            this.but_delete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.but_delete.Image = ((System.Drawing.Image)(resources.GetObject("but_delete.Image")));
            this.but_delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.but_delete.Name = "but_delete";
            this.but_delete.Size = new System.Drawing.Size(55, 22);
            this.but_delete.Text = "Удалить";
            // 
            // but_texbox
            // 
            this.but_texbox.Name = "but_texbox";
            this.but_texbox.Size = new System.Drawing.Size(100, 25);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(46, 22);
            this.toolStripButton1.Text = "Поиск";
            this.toolStripButton1.Click += new System.EventHandler(this.but_texbox_TextChanged);
            // 
            // Tables
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 365);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.listView1);
            this.Name = "Tables";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Tables_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton but_add;
        private System.Windows.Forms.ToolStripButton but_redact;
        private System.Windows.Forms.ToolStripButton but_delete;
        private System.Windows.Forms.ToolStripTextBox but_texbox;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
    }
}