namespace Conexao_SQL
{
    partial class Visualizar
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
            this.lstVisualizar = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // lstVisualizar
            // 
            this.lstVisualizar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstVisualizar.HideSelection = false;
            this.lstVisualizar.Location = new System.Drawing.Point(0, 0);
            this.lstVisualizar.Name = "lstVisualizar";
            this.lstVisualizar.Size = new System.Drawing.Size(800, 450);
            this.lstVisualizar.TabIndex = 0;
            this.lstVisualizar.UseCompatibleStateImageBehavior = false;
            // 
            // Visualizar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lstVisualizar);
            this.Name = "Visualizar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Visualizar categorias e produtos";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lstVisualizar;
    }
}