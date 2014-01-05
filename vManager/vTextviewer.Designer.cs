namespace vMixManager
{
    partial class vMixTextviewer
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.rtb_viewer = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rtb_viewer
            // 
            this.rtb_viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_viewer.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtb_viewer.Location = new System.Drawing.Point(0, 0);
            this.rtb_viewer.Name = "rtb_viewer";
            this.rtb_viewer.Size = new System.Drawing.Size(499, 345);
            this.rtb_viewer.TabIndex = 0;
            this.rtb_viewer.Text = "";
            // 
            // vMixTextviewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 345);
            this.Controls.Add(this.rtb_viewer);
            this.Name = "vMixTextviewer";
            this.ShowIcon = false;
            this.Text = "vManager - Copy & Paste Window";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtb_viewer;
    }
}