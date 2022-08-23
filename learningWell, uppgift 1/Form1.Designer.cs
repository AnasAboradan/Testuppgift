
using System.Drawing;
using System.Windows.Forms;

namespace learingwell
{
    partial class Chart
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        Panel asid_panel = new Panel();

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
            this.SuspendLayout();
            // 
            // Chart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "Chart";
            this.Text = "Chart";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.BackColor = Color.Black;
            this.Width = 1500;
            this.Height = 1000;


            // asid panel

           
            asid_panel.Location = new Point(1300,0);
            asid_panel.Width = 200;
            asid_panel.Height = 1000;
            asid_panel.BackColor = Color.RosyBrown;


            Label l = new Label();
            l.Location = new Point(0, 20);
            l.Width = 200;
            l.Height = 50;
            l.BackColor = Color.RosyBrown;
            l.ForeColor = Color.White;
            l.Font = new Font("Arial", 20);
            l.Text = "select a year";


            asid_panel.Controls.Add(l);
            this.Controls.Add(asid_panel);



        }

        #endregion
    }
}

