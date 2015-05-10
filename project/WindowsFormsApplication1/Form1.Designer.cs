namespace WindowsFormsApplication1
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.fileButtons = new System.Windows.Forms.GroupBox();
            this.save = new System.Windows.Forms.Button();
            this.open = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.originalImage = new System.Windows.Forms.Button();
            this.edgeRelaxation = new System.Windows.Forms.Button();
            this.edgeDetection = new System.Windows.Forms.Button();
            this.thresholdLabel = new System.Windows.Forms.Label();
            this.threshold = new System.Windows.Forms.TextBox();
            this.matrixCombo = new System.Windows.Forms.ComboBox();
            this.fileButtons.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // fileButtons
            // 
            this.fileButtons.Controls.Add(this.save);
            this.fileButtons.Controls.Add(this.open);
            resources.ApplyResources(this.fileButtons, "fileButtons");
            this.fileButtons.Name = "fileButtons";
            this.fileButtons.TabStop = false;
            // 
            // save
            // 
            resources.ApplyResources(this.save, "save");
            this.save.Name = "save";
            this.save.UseVisualStyleBackColor = false;
            // 
            // open
            // 
            resources.ApplyResources(this.open, "open");
            this.open.Name = "open";
            this.open.UseVisualStyleBackColor = false;
            this.open.Click += new System.EventHandler(this.openFile);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.matrixCombo);
            this.groupBox1.Controls.Add(this.originalImage);
            this.groupBox1.Controls.Add(this.edgeRelaxation);
            this.groupBox1.Controls.Add(this.edgeDetection);
            this.groupBox1.Controls.Add(this.thresholdLabel);
            this.groupBox1.Controls.Add(this.threshold);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // originalImage
            // 
            resources.ApplyResources(this.originalImage, "originalImage");
            this.originalImage.Name = "originalImage";
            this.originalImage.UseVisualStyleBackColor = true;
            this.originalImage.Click += new System.EventHandler(this.drawOriginalImage);
            // 
            // edgeRelaxation
            // 
            resources.ApplyResources(this.edgeRelaxation, "edgeRelaxation");
            this.edgeRelaxation.Name = "edgeRelaxation";
            this.edgeRelaxation.UseVisualStyleBackColor = true;
            // 
            // edgeDetection
            // 
            resources.ApplyResources(this.edgeDetection, "edgeDetection");
            this.edgeDetection.Name = "edgeDetection";
            this.edgeDetection.UseVisualStyleBackColor = true;
            this.edgeDetection.Click += new System.EventHandler(this.edgesDetection);
            // 
            // thresholdLabel
            // 
            resources.ApplyResources(this.thresholdLabel, "thresholdLabel");
            this.thresholdLabel.Name = "thresholdLabel";
            // 
            // threshold
            // 
            resources.ApplyResources(this.threshold, "threshold");
            this.threshold.Name = "threshold";
            // 
            // matrixCombo
            // 
            this.matrixCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.matrixCombo, "matrixCombo");
            this.matrixCombo.FormattingEnabled = true;
            this.matrixCombo.Items.AddRange(new object[] {
            resources.GetString("matrixCombo.Items"),
            resources.GetString("matrixCombo.Items1"),
            resources.GetString("matrixCombo.Items2")});
            this.matrixCombo.Name = "matrixCombo";
            this.matrixCombo.SelectedIndexChanged += new System.EventHandler(this.matrixCombo_SelectedIndexChanged);
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.fileButtons);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.fileButtons.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox fileButtons;
        private System.Windows.Forms.Button save;
        private System.Windows.Forms.Button open;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox threshold;
        private System.Windows.Forms.Label thresholdLabel;
        private System.Windows.Forms.Button edgeRelaxation;
        private System.Windows.Forms.Button edgeDetection;
        private System.Windows.Forms.Button originalImage;
        private System.Windows.Forms.ComboBox matrixCombo;
    }
}

