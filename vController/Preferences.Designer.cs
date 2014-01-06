namespace vMixControler
{
    partial class vMixPreferences
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        private void InitializeComponent()
        {
            this.ud_vMixPort = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.bn_testport = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.ud_preload = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ud_linger = new System.Windows.Forms.NumericUpDown();
            this.cb_autoload = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ud_vMixPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ud_preload)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ud_linger)).BeginInit();
            this.SuspendLayout();
            // 
            // ud_vMixPort
            // 
            this.ud_vMixPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ud_vMixPort.Location = new System.Drawing.Point(157, 5);
            this.ud_vMixPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.ud_vMixPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ud_vMixPort.Name = "ud_vMixPort";
            this.ud_vMixPort.Size = new System.Drawing.Size(62, 21);
            this.ud_vMixPort.TabIndex = 0;
            this.ud_vMixPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ud_vMixPort.Value = new decimal(new int[] {
            8088,
            0,
            0,
            0});
            this.ud_vMixPort.ValueChanged += new System.EventHandler(this.ud_vMixPort_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Port of vMix\' Web-Interface:";
            // 
            // bn_testport
            // 
            this.bn_testport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bn_testport.Location = new System.Drawing.Point(225, 5);
            this.bn_testport.Name = "bn_testport";
            this.bn_testport.Size = new System.Drawing.Size(47, 21);
            this.bn_testport.TabIndex = 2;
            this.bn_testport.Text = "Test";
            this.bn_testport.UseVisualStyleBackColor = true;
            this.bn_testport.Click += new System.EventHandler(this.bn_testport_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(143, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Load Input before Transition:";
            // 
            // ud_preload
            // 
            this.ud_preload.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ud_preload.Location = new System.Drawing.Point(173, 35);
            this.ud_preload.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.ud_preload.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ud_preload.Name = "ud_preload";
            this.ud_preload.Size = new System.Drawing.Size(46, 21);
            this.ud_preload.TabIndex = 3;
            this.ud_preload.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ud_preload.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.ud_preload.ValueChanged += new System.EventHandler(this.ud_preload_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(225, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Seconds";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(225, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Seconds";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(150, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Remove Input after Transition:";
            // 
            // ud_linger
            // 
            this.ud_linger.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ud_linger.Location = new System.Drawing.Point(173, 57);
            this.ud_linger.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.ud_linger.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ud_linger.Name = "ud_linger";
            this.ud_linger.Size = new System.Drawing.Size(46, 21);
            this.ud_linger.TabIndex = 6;
            this.ud_linger.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ud_linger.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.ud_linger.ValueChanged += new System.EventHandler(this.ud_linger_ValueChanged);
            // 
            // cb_autoload
            // 
            this.cb_autoload.AutoSize = true;
            this.cb_autoload.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cb_autoload.Location = new System.Drawing.Point(11, 84);
            this.cb_autoload.Name = "cb_autoload";
            this.cb_autoload.Size = new System.Drawing.Size(175, 17);
            this.cb_autoload.TabIndex = 9;
            this.cb_autoload.Text = "Auto-Load Schedule at Startup:";
            this.cb_autoload.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cb_autoload.UseVisualStyleBackColor = true;
            this.cb_autoload.CheckedChanged += new System.EventHandler(this.cb_autoload_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(192, 85);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "(if vMix is online)";
            // 
            // vMixPreferences
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 106);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cb_autoload);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ud_linger);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ud_preload);
            this.Controls.Add(this.bn_testport);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ud_vMixPort);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "vMixPreferences";
            this.ShowIcon = false;
            this.Text = "Settings";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.vMixPreferences_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.ud_vMixPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ud_preload)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ud_linger)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown ud_vMixPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bn_testport;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown ud_preload;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown ud_linger;
        private System.Windows.Forms.CheckBox cb_autoload;
        private System.Windows.Forms.Label label6;
    }
}