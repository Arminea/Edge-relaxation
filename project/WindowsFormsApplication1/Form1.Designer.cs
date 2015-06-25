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
            this.originalImage = new System.Windows.Forms.Button();
            this.open = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.matrixCombo = new System.Windows.Forms.ComboBox();
            this.edgeDetection = new System.Windows.Forms.Button();
            this.thresholdLabel = new System.Windows.Forms.Label();
            this.threshold = new System.Windows.Forms.TextBox();
            this.edgeRelaxation = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.loops = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.button3 = new System.Windows.Forms.Button();
            this.fileButtons.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // fileButtons
            // 
            this.fileButtons.Controls.Add(this.save);
            this.fileButtons.Controls.Add(this.originalImage);
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
            this.save.Click += new System.EventHandler(this.saveImage);
            // 
            // originalImage
            // 
            resources.ApplyResources(this.originalImage, "originalImage");
            this.originalImage.Name = "originalImage";
            this.originalImage.UseVisualStyleBackColor = true;
            this.originalImage.Click += new System.EventHandler(this.drawOriginalImage);
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
            this.groupBox1.Controls.Add(this.edgeDetection);
            this.groupBox1.Controls.Add(this.thresholdLabel);
            this.groupBox1.Controls.Add(this.threshold);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
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
            // edgeRelaxation
            // 
            resources.ApplyResources(this.edgeRelaxation, "edgeRelaxation");
            this.edgeRelaxation.Name = "edgeRelaxation";
            this.edgeRelaxation.UseVisualStyleBackColor = true;
            this.edgeRelaxation.Click += new System.EventHandler(this.MultipleEdgeRelaxationInImage);
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.SingleEdgeRelaxationInImage);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.loops);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.edgeRelaxation);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.StepBack);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // loops
            // 
            resources.ApplyResources(this.loops, "loops");
            this.loops.Name = "loops";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioButton2);
            this.groupBox3.Controls.Add(this.radioButton1);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // radioButton2
            // 
            resources.ApplyResources(this.radioButton2, "radioButton2");
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.TabStop = true;
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            resources.ApplyResources(this.radioButton1, "radioButton1");
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.TabStop = true;
            this.radioButton1.UseCompatibleTextRendering = true;
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // button3
            // 
            resources.ApplyResources(this.button3, "button3");
            this.button3.Name = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.StartEdgeRelaxation);
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.fileButtons);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.fileButtons.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
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
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox loops;
        private System.Windows.Forms.Button button3;
    }
}

