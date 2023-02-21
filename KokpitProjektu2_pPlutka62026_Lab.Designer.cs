namespace Projekt2_Plutka62026_Laboratorium
{
    partial class KokpitProjektu2_pPlutka62026_Lab
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SzeregLaboratoryjny = new System.Windows.Forms.Button();
            this.SzeregIndywidualny = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1004, 439);
            this.label1.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 34);
            this.label1.TabIndex = 0;
            this.label1.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(304, 74);
            this.label2.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(409, 32);
            this.label2.TabIndex = 1;
            this.label2.Text = "Analizator szeregów potęgowych\r\n";
            // 
            // SzeregLaboratoryjny
            // 
            this.SzeregLaboratoryjny.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.SzeregLaboratoryjny.Location = new System.Drawing.Point(40, 158);
            this.SzeregLaboratoryjny.Name = "SzeregLaboratoryjny";
            this.SzeregLaboratoryjny.Size = new System.Drawing.Size(442, 98);
            this.SzeregLaboratoryjny.TabIndex = 2;
            this.SzeregLaboratoryjny.Text = "Laboratorium Nr 2 \r\n(Analizator laboratoryjnego szeregu potęgowego)";
            this.SzeregLaboratoryjny.UseVisualStyleBackColor = true;
            this.SzeregLaboratoryjny.Click += new System.EventHandler(this.SzeregLaboratoryjny_Click);
            // 
            // SzeregIndywidualny
            // 
            this.SzeregIndywidualny.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.SzeregIndywidualny.Location = new System.Drawing.Point(536, 158);
            this.SzeregIndywidualny.Name = "SzeregIndywidualny";
            this.SzeregIndywidualny.Size = new System.Drawing.Size(433, 98);
            this.SzeregIndywidualny.TabIndex = 3;
            this.SzeregIndywidualny.Text = "Projekt Nr 2 \r\n(Analizator indywidualnego szeregu potęgowego)";
            this.SzeregIndywidualny.UseVisualStyleBackColor = true;
            this.SzeregIndywidualny.Click += new System.EventHandler(this.SzeregIndywidualny_Click);
            // 
            // KokpitProjektu2_pPlutka62026_Lab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(20F, 34F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1036, 604);
            this.Controls.Add(this.SzeregIndywidualny);
            this.Controls.Add(this.SzeregLaboratoryjny);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Georgia", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Margin = new System.Windows.Forms.Padding(9, 8, 9, 8);
            this.Name = "KokpitProjektu2_pPlutka62026_Lab";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.KokpitProjektu2_pPlutka62026_Lab_FormClosing);
            this.Load += new System.EventHandler(this.KokpitProjektu2_pPlutka62026_Lab_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button SzeregLaboratoryjny;
        private System.Windows.Forms.Button SzeregIndywidualny;
    }
}

