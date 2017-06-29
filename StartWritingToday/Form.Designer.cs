namespace PiszemySlowa
{
    partial class MainForm
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
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.txtBox = new System.Windows.Forms.RichTextBox();
            this.btnSprawdz = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.Location = new System.Drawing.Point(0, 190);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(1151, 456);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // txtBox
            // 
            this.txtBox.CausesValidation = false;
            this.txtBox.DetectUrls = false;
            this.txtBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtBox.Font = new System.Drawing.Font("Arial", 60F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtBox.Location = new System.Drawing.Point(0, 0);
            this.txtBox.MaxLength = 40;
            this.txtBox.Multiline = false;
            this.txtBox.Name = "txtBox";
            this.txtBox.Size = new System.Drawing.Size(1151, 139);
            this.txtBox.TabIndex = 1;
            this.txtBox.Text = "";
            this.txtBox.WordWrap = false;
            this.txtBox.TextChanged += new System.EventHandler(this.TxtBox_TextChanged);
            // 
            // btnSprawdz
            // 
            this.btnSprawdz.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSprawdz.Location = new System.Drawing.Point(0, 139);
            this.btnSprawdz.Name = "btnSprawdz";
            this.btnSprawdz.Size = new System.Drawing.Size(1151, 51);
            this.btnSprawdz.TabIndex = 2;
            this.btnSprawdz.Text = "v";
            this.btnSprawdz.UseVisualStyleBackColor = true;
            this.btnSprawdz.Click += new System.EventHandler(this.BtnSprawdz_Click);
            // 
            // MainForm
            // 
            this.AcceptButton = this.btnSprawdz;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1151, 646);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.btnSprawdz);
            this.Controls.Add(this.txtBox);
            this.MinimumSize = new System.Drawing.Size(1167, 685);
            this.Name = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.RichTextBox txtBox;
        private System.Windows.Forms.Button btnSprawdz;
    }
}

