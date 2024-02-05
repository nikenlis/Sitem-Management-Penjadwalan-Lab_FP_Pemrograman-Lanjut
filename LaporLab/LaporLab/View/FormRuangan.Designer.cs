namespace LaporLab.View
{
    partial class FormRuangan
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listView1 = new System.Windows.Forms.ListView();
            this.lvmRuangan = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1013, 683);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // lvmRuangan
            // 
            this.lvmRuangan.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvmRuangan.ForeColor = System.Drawing.Color.Black;
            this.lvmRuangan.HideSelection = false;
            this.lvmRuangan.Location = new System.Drawing.Point(34, 26);
            this.lvmRuangan.Name = "lvmRuangan";
            this.lvmRuangan.Size = new System.Drawing.Size(943, 638);
            this.lvmRuangan.TabIndex = 1;
            this.lvmRuangan.UseCompatibleStateImageBehavior = false;
            // 
            // FormRuangan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvmRuangan);
            this.Controls.Add(this.listView1);
            this.Name = "FormRuangan";
            this.Size = new System.Drawing.Size(1013, 684);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ListView lvmRuangan;
    }
}
