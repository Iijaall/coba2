
namespace GrandHotel_31_08_21
{
    partial class FrmConfirmationExit
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
            this.btnOk = new System.Windows.Forms.Button();
            this.rbtnQuit = new System.Windows.Forms.RadioButton();
            this.rbtnRestart = new System.Windows.Forms.RadioButton();
            this.rbtnLogoff = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(16)))), ((int)(((byte)(152)))));
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOk.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.ForeColor = System.Drawing.SystemColors.Control;
            this.btnOk.Location = new System.Drawing.Point(157, 196);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 10;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // rbtnQuit
            // 
            this.rbtnQuit.AutoSize = true;
            this.rbtnQuit.Location = new System.Drawing.Point(17, 157);
            this.rbtnQuit.Name = "rbtnQuit";
            this.rbtnQuit.Size = new System.Drawing.Size(44, 17);
            this.rbtnQuit.TabIndex = 9;
            this.rbtnQuit.TabStop = true;
            this.rbtnQuit.Text = "Quit";
            this.rbtnQuit.UseVisualStyleBackColor = true;
            // 
            // rbtnRestart
            // 
            this.rbtnRestart.AutoSize = true;
            this.rbtnRestart.Location = new System.Drawing.Point(17, 101);
            this.rbtnRestart.Name = "rbtnRestart";
            this.rbtnRestart.Size = new System.Drawing.Size(59, 17);
            this.rbtnRestart.TabIndex = 8;
            this.rbtnRestart.TabStop = true;
            this.rbtnRestart.Text = "Restart";
            this.rbtnRestart.UseVisualStyleBackColor = true;
            // 
            // rbtnLogoff
            // 
            this.rbtnLogoff.AutoSize = true;
            this.rbtnLogoff.Location = new System.Drawing.Point(17, 34);
            this.rbtnLogoff.Name = "rbtnLogoff";
            this.rbtnLogoff.Size = new System.Drawing.Size(66, 17);
            this.rbtnLogoff.TabIndex = 7;
            this.rbtnLogoff.TabStop = true;
            this.rbtnLogoff.Text = "Log Off :";
            this.rbtnLogoff.UseVisualStyleBackColor = true;
            // 
            // FrmConfirmationExit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(267, 234);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.rbtnQuit);
            this.Controls.Add(this.rbtnRestart);
            this.Controls.Add(this.rbtnLogoff);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FrmConfirmationExit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmConfirmationExit";
            this.Load += new System.EventHandler(this.FrmConfirmationExit_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.RadioButton rbtnQuit;
        private System.Windows.Forms.RadioButton rbtnRestart;
        private System.Windows.Forms.RadioButton rbtnLogoff;
    }
}