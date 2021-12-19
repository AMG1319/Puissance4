namespace JeuxPuissance4
{
    partial class EcranAcceuil
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.B1 = new System.Windows.Forms.RadioButton();
            this.V1 = new System.Windows.Forms.RadioButton();
            this.J1 = new System.Windows.Forms.RadioButton();
            this.R1 = new System.Windows.Forms.RadioButton();
            this.pbPionBleu1 = new System.Windows.Forms.PictureBox();
            this.pbPionVert1 = new System.Windows.Forms.PictureBox();
            this.pbPionJaune1 = new System.Windows.Forms.PictureBox();
            this.pbPionRouge1 = new System.Windows.Forms.PictureBox();
            this.tbNomJ1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnValider = new System.Windows.Forms.Button();
            this.btnAnnuler = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPionBleu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPionVert1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPionJaune1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPionRouge1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.tbNomJ1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(9, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(355, 215);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Joueur 1";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.groupBox3.Controls.Add(this.B1);
            this.groupBox3.Controls.Add(this.V1);
            this.groupBox3.Controls.Add(this.J1);
            this.groupBox3.Controls.Add(this.R1);
            this.groupBox3.Controls.Add(this.pbPionBleu1);
            this.groupBox3.Controls.Add(this.pbPionVert1);
            this.groupBox3.Controls.Add(this.pbPionJaune1);
            this.groupBox3.Controls.Add(this.pbPionRouge1);
            this.groupBox3.ForeColor = System.Drawing.Color.Blue;
            this.groupBox3.Location = new System.Drawing.Point(9, 100);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(340, 100);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Couleur des pions";
            // 
            // B1
            // 
            this.B1.AutoSize = true;
            this.B1.BackColor = System.Drawing.Color.Black;
            this.B1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.B1.Location = new System.Drawing.Point(298, 77);
            this.B1.Name = "B1";
            this.B1.Size = new System.Drawing.Size(16, 15);
            this.B1.TabIndex = 7;
            this.B1.UseVisualStyleBackColor = false;
            this.B1.CheckedChanged += new System.EventHandler(this.B1_CheckedChanged);
            // 
            // V1
            // 
            this.V1.AutoSize = true;
            this.V1.BackColor = System.Drawing.Color.Black;
            this.V1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.V1.Location = new System.Drawing.Point(206, 77);
            this.V1.Name = "V1";
            this.V1.Size = new System.Drawing.Size(16, 15);
            this.V1.TabIndex = 6;
            this.V1.UseVisualStyleBackColor = false;
            this.V1.CheckedChanged += new System.EventHandler(this.V1_CheckedChanged);
            // 
            // J1
            // 
            this.J1.AutoSize = true;
            this.J1.BackColor = System.Drawing.Color.Black;
            this.J1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.J1.Location = new System.Drawing.Point(114, 77);
            this.J1.Name = "J1";
            this.J1.Size = new System.Drawing.Size(16, 15);
            this.J1.TabIndex = 5;
            this.J1.UseVisualStyleBackColor = false;
            this.J1.CheckedChanged += new System.EventHandler(this.J1_CheckedChanged);
            // 
            // R1
            // 
            this.R1.AutoSize = true;
            this.R1.BackColor = System.Drawing.Color.Black;
            this.R1.Checked = true;
            this.R1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.R1.Location = new System.Drawing.Point(22, 77);
            this.R1.Name = "R1";
            this.R1.Size = new System.Drawing.Size(16, 15);
            this.R1.TabIndex = 4;
            this.R1.TabStop = true;
            this.R1.UseVisualStyleBackColor = false;
            this.R1.CheckedChanged += new System.EventHandler(this.R1_CheckedChanged);
            // 
            // pbPionBleu1
            // 
            this.pbPionBleu1.Location = new System.Drawing.Point(282, 21);
            this.pbPionBleu1.Name = "pbPionBleu1";
            this.pbPionBleu1.Size = new System.Drawing.Size(50, 50);
            this.pbPionBleu1.TabIndex = 3;
            this.pbPionBleu1.TabStop = false;
            // 
            // pbPionVert1
            // 
            this.pbPionVert1.Location = new System.Drawing.Point(190, 21);
            this.pbPionVert1.Name = "pbPionVert1";
            this.pbPionVert1.Size = new System.Drawing.Size(50, 50);
            this.pbPionVert1.TabIndex = 2;
            this.pbPionVert1.TabStop = false;
            // 
            // pbPionJaune1
            // 
            this.pbPionJaune1.Location = new System.Drawing.Point(98, 21);
            this.pbPionJaune1.Name = "pbPionJaune1";
            this.pbPionJaune1.Size = new System.Drawing.Size(50, 50);
            this.pbPionJaune1.TabIndex = 1;
            this.pbPionJaune1.TabStop = false;
            // 
            // pbPionRouge1
            // 
            this.pbPionRouge1.Location = new System.Drawing.Point(6, 21);
            this.pbPionRouge1.Name = "pbPionRouge1";
            this.pbPionRouge1.Size = new System.Drawing.Size(50, 50);
            this.pbPionRouge1.TabIndex = 0;
            this.pbPionRouge1.TabStop = false;
            // 
            // tbNomJ1
            // 
            this.tbNomJ1.BackColor = System.Drawing.Color.Black;
            this.tbNomJ1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbNomJ1.ForeColor = System.Drawing.Color.Blue;
            this.tbNomJ1.Location = new System.Drawing.Point(86, 48);
            this.tbNomJ1.Name = "tbNomJ1";
            this.tbNomJ1.Size = new System.Drawing.Size(255, 22);
            this.tbNomJ1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pseudo :";
            // 
            // btnValider
            // 
            this.btnValider.BackColor = System.Drawing.Color.Black;
            this.btnValider.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnValider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnValider.ForeColor = System.Drawing.Color.Blue;
            this.btnValider.Location = new System.Drawing.Point(183, 232);
            this.btnValider.Name = "btnValider";
            this.btnValider.Size = new System.Drawing.Size(75, 28);
            this.btnValider.TabIndex = 2;
            this.btnValider.Text = "Valider";
            this.btnValider.UseVisualStyleBackColor = false;
            this.btnValider.Click += new System.EventHandler(this.btnValider_Click);
            // 
            // btnAnnuler
            // 
            this.btnAnnuler.BackColor = System.Drawing.Color.Black;
            this.btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnAnnuler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnnuler.ForeColor = System.Drawing.Color.Blue;
            this.btnAnnuler.Location = new System.Drawing.Point(289, 232);
            this.btnAnnuler.Name = "btnAnnuler";
            this.btnAnnuler.Size = new System.Drawing.Size(75, 28);
            this.btnAnnuler.TabIndex = 3;
            this.btnAnnuler.Text = "Annuler";
            this.btnAnnuler.UseVisualStyleBackColor = false;
            this.btnAnnuler.Click += new System.EventHandler(this.btnAnnuler_Click);
            // 
            // EcranAcceuil
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(388, 294);
            this.ControlBox = false;
            this.Controls.Add(this.btnAnnuler);
            this.Controls.Add(this.btnValider);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EcranAcceuil";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Acceuil Puissance 4";
            this.Load += new System.EventHandler(this.EcranAcceuil_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPionBleu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPionVert1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPionJaune1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPionRouge1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton B1;
        private System.Windows.Forms.RadioButton V1;
        private System.Windows.Forms.RadioButton J1;
        private System.Windows.Forms.RadioButton R1;
        private System.Windows.Forms.PictureBox pbPionBleu1;
        private System.Windows.Forms.PictureBox pbPionVert1;
        private System.Windows.Forms.PictureBox pbPionJaune1;
        private System.Windows.Forms.PictureBox pbPionRouge1;
        private System.Windows.Forms.TextBox tbNomJ1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnValider;
        private System.Windows.Forms.Button btnAnnuler;
    }
}

