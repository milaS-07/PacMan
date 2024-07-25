namespace pekmen
{
    partial class Form2
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
            bIgrajOpet = new Button();
            bX = new Button();
            SuspendLayout();
            // 
            // bIgrajOpet
            // 
            bIgrajOpet.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point);
            bIgrajOpet.Location = new Point(192, 283);
            bIgrajOpet.Name = "bIgrajOpet";
            bIgrajOpet.Size = new Size(189, 53);
            bIgrajOpet.TabIndex = 0;
            bIgrajOpet.Text = "Igraj opet";
            bIgrajOpet.UseVisualStyleBackColor = true;
            bIgrajOpet.Click += bIgrajOpet_Click;
            // 
            // bX
            // 
            bX.Location = new Point(565, 12);
            bX.Name = "bX";
            bX.Size = new Size(23, 22);
            bX.TabIndex = 2;
            bX.Text = "x";
            bX.UseVisualStyleBackColor = true;
            bX.Click += bX_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(600, 398);
            Controls.Add(bX);
            Controls.Add(bIgrajOpet);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form2";
            Text = "Kraj igre";
            Load += Form2_Load;
            Paint += Form2_Paint;
            ResumeLayout(false);
        }

        #endregion

        private Button bIgrajOpet;
        private Button bX;
    }
}