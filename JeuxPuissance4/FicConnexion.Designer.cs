
namespace JeuxPuissance4
{
    partial class EcranConnexion
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnConnecter = new System.Windows.Forms.Button();
            this.btnEcouter = new System.Windows.Forms.Button();
            this.tbServeur = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(2, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "Nom Machine adverse";
            // 
            // btnConnecter
            // 
            this.btnConnecter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConnecter.ForeColor = System.Drawing.Color.Blue;
            this.btnConnecter.Location = new System.Drawing.Point(180, 53);
            this.btnConnecter.Name = "btnConnecter";
            this.btnConnecter.Size = new System.Drawing.Size(80, 32);
            this.btnConnecter.TabIndex = 7;
            this.btnConnecter.Text = "Rejoindre";
            this.btnConnecter.UseVisualStyleBackColor = true;
            this.btnConnecter.Click += new System.EventHandler(this.btnConnecter_Click);
            // 
            // btnEcouter
            // 
            this.btnEcouter.BackColor = System.Drawing.Color.Black;
            this.btnEcouter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEcouter.ForeColor = System.Drawing.Color.Blue;
            this.btnEcouter.Location = new System.Drawing.Point(6, 53);
            this.btnEcouter.Name = "btnEcouter";
            this.btnEcouter.Size = new System.Drawing.Size(80, 32);
            this.btnEcouter.TabIndex = 6;
            this.btnEcouter.Text = "Inviter";
            this.btnEcouter.UseVisualStyleBackColor = false;
            this.btnEcouter.Click += new System.EventHandler(this.btnEcouter_Click);
            // 
            // tbServeur
            // 
            this.tbServeur.Location = new System.Drawing.Point(6, 25);
            this.tbServeur.Name = "tbServeur";
            this.tbServeur.Size = new System.Drawing.Size(254, 22);
            this.tbServeur.TabIndex = 5;
            this.tbServeur.Text = "DESKTOP-U9ONRQ2";
            // 
            // EcranConnexion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(265, 94);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnConnecter);
            this.Controls.Add(this.btnEcouter);
            this.Controls.Add(this.tbServeur);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EcranConnexion";
            this.Text = "Connexion";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnConnecter;
        private System.Windows.Forms.Button btnEcouter;
        private System.Windows.Forms.TextBox tbServeur;
    }
}