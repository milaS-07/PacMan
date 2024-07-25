namespace pekmen
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            tajmerZaOtvaranjeUsta = new System.Windows.Forms.Timer(components);
            tajmerZaPomeranjePekmena = new System.Windows.Forms.Timer(components);
            tajmerZaKretanjeDuha = new System.Windows.Forms.Timer(components);
            tajmerZaNoviPocetak = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // tajmerZaOtvaranjeUsta
            // 
            tajmerZaOtvaranjeUsta.Tick += tajmerZaOtvaranjeUsta_Tick;
            // 
            // tajmerZaPomeranjePekmena
            // 
            tajmerZaPomeranjePekmena.Tick += tajmerZaPomeranjePekmena_Tick;
            // 
            // tajmerZaKretanjeDuha
            // 
            tajmerZaKretanjeDuha.Tick += tajmerZaKretanjeDuha_Tick;
            // 
            // tajmerZaNoviPocetak
            // 
            tajmerZaNoviPocetak.Tick += tajmerZaNoviPocetak_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(664, 433);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            Name = "Form1";
            Text = "Pekmen";
            Load += Form1_Load;
            SizeChanged += Form1_SizeChanged;
            Paint += Form1_Paint;
            KeyDown += Form1_KeyDown;
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Timer tajmerZaOtvaranjeUsta;
        private System.Windows.Forms.Timer tajmerZaPomeranjePekmena;
        private System.Windows.Forms.Timer tajmerZaKretanjeDuha;
        private System.Windows.Forms.Timer tajmerZaNoviPocetak;
    }
}