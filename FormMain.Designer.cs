namespace AStarPuzzle
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.grbLeft = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tblSplitImages = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.picSelectedImage = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSolveAllAlgorithm = new System.Windows.Forms.Button();
            this.lblSolveStep = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pbSolveStep = new System.Windows.Forms.ProgressBar();
            this.Heuristic = new System.Windows.Forms.Label();
            this.cmbHeuristic = new System.Windows.Forms.ComboBox();
            this.btnRunSolver = new System.Windows.Forms.Button();
            this.btnCanSolve = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbSpeed = new System.Windows.Forms.TrackBar();
            this.btnRandom = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnChooseImage = new System.Windows.Forms.Button();
            this.cmbSize = new System.Windows.Forms.ComboBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.grbLeft.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSelectedImage)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbSpeed)).BeginInit();
            this.SuspendLayout();
            // 
            // splitMain
            // 
            this.splitMain.BackColor = System.Drawing.Color.White;
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(0, 0);
            this.splitMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.grbLeft);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.groupBox1);
            this.splitMain.Size = new System.Drawing.Size(1183, 658);
            this.splitMain.SplitterDistance = 753;
            this.splitMain.SplitterWidth = 6;
            this.splitMain.TabIndex = 0;
            // 
            // grbLeft
            // 
            this.grbLeft.Controls.Add(this.panel1);
            this.grbLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbLeft.Location = new System.Drawing.Point(0, 0);
            this.grbLeft.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grbLeft.Name = "grbLeft";
            this.grbLeft.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grbLeft.Size = new System.Drawing.Size(753, 658);
            this.grbLeft.TabIndex = 0;
            this.grbLeft.TabStop = false;
            this.grbLeft.Text = "Hình ảnh";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tblSplitImages);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(4, 27);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(745, 626);
            this.panel1.TabIndex = 0;
            // 
            // tblSplitImages
            // 
            this.tblSplitImages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tblSplitImages.ColumnCount = 1;
            this.tblSplitImages.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblSplitImages.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tblSplitImages.Location = new System.Drawing.Point(61, 14);
            this.tblSplitImages.Margin = new System.Windows.Forms.Padding(0);
            this.tblSplitImages.MinimumSize = new System.Drawing.Size(512, 511);
            this.tblSplitImages.Name = "tblSplitImages";
            this.tblSplitImages.RowCount = 1;
            this.tblSplitImages.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblSplitImages.Size = new System.Drawing.Size(600, 600);
            this.tblSplitImages.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(424, 658);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chức năng";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.picSelectedImage);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(4, 262);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(416, 391);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ảnh đã chọn";
            // 
            // picSelectedImage
            // 
            this.picSelectedImage.Location = new System.Drawing.Point(30, 26);
            this.picSelectedImage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.picSelectedImage.Name = "picSelectedImage";
            this.picSelectedImage.Size = new System.Drawing.Size(360, 360);
            this.picSelectedImage.TabIndex = 0;
            this.picSelectedImage.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnSolveAllAlgorithm);
            this.panel2.Controls.Add(this.lblSolveStep);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.pbSolveStep);
            this.panel2.Controls.Add(this.Heuristic);
            this.panel2.Controls.Add(this.cmbHeuristic);
            this.panel2.Controls.Add(this.btnRunSolver);
            this.panel2.Controls.Add(this.btnCanSolve);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.tbSpeed);
            this.panel2.Controls.Add(this.btnRandom);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.btnChooseImage);
            this.panel2.Controls.Add(this.cmbSize);
            this.panel2.Controls.Add(this.btnReset);
            this.panel2.Controls.Add(this.btnStop);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(4, 27);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(416, 626);
            this.panel2.TabIndex = 0;
            // 
            // btnSolveAllAlgorithm
            // 
            this.btnSolveAllAlgorithm.Location = new System.Drawing.Point(285, 51);
            this.btnSolveAllAlgorithm.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSolveAllAlgorithm.Name = "btnSolveAllAlgorithm";
            this.btnSolveAllAlgorithm.Size = new System.Drawing.Size(114, 39);
            this.btnSolveAllAlgorithm.TabIndex = 13;
            this.btnSolveAllAlgorithm.Text = "Giải hết";
            this.btnSolveAllAlgorithm.UseVisualStyleBackColor = true;
            this.btnSolveAllAlgorithm.Click += new System.EventHandler(this.btnSolveAllAlgorithm_Click);
            // 
            // lblSolveStep
            // 
            this.lblSolveStep.AutoSize = true;
            this.lblSolveStep.Location = new System.Drawing.Point(331, 146);
            this.lblSolveStep.Name = "lblSolveStep";
            this.lblSolveStep.Size = new System.Drawing.Size(34, 21);
            this.lblSolveStep.TabIndex = 11;
            this.lblSolveStep.Text = "0/0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 146);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 21);
            this.label3.TabIndex = 10;
            this.label3.Text = "Lời giải";
            // 
            // pbSolveStep
            // 
            this.pbSolveStep.Location = new System.Drawing.Point(107, 146);
            this.pbSolveStep.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pbSolveStep.Name = "pbSolveStep";
            this.pbSolveStep.Size = new System.Drawing.Size(193, 22);
            this.pbSolveStep.TabIndex = 9;
            // 
            // Heuristic
            // 
            this.Heuristic.AutoSize = true;
            this.Heuristic.Location = new System.Drawing.Point(13, 105);
            this.Heuristic.Name = "Heuristic";
            this.Heuristic.Size = new System.Drawing.Size(71, 21);
            this.Heuristic.TabIndex = 7;
            this.Heuristic.Text = "Heuristic";
            // 
            // cmbHeuristic
            // 
            this.cmbHeuristic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHeuristic.FormattingEnabled = true;
            this.cmbHeuristic.Location = new System.Drawing.Point(107, 103);
            this.cmbHeuristic.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbHeuristic.Name = "cmbHeuristic";
            this.cmbHeuristic.Size = new System.Drawing.Size(161, 29);
            this.cmbHeuristic.TabIndex = 6;
            // 
            // btnRunSolver
            // 
            this.btnRunSolver.Location = new System.Drawing.Point(285, 97);
            this.btnRunSolver.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRunSolver.Name = "btnRunSolver";
            this.btnRunSolver.Size = new System.Drawing.Size(114, 39);
            this.btnRunSolver.TabIndex = 5;
            this.btnRunSolver.Text = "Chạy";
            this.btnRunSolver.UseVisualStyleBackColor = true;
            this.btnRunSolver.Click += new System.EventHandler(this.btnRunSolver_Click);
            // 
            // btnCanSolve
            // 
            this.btnCanSolve.Location = new System.Drawing.Point(146, 51);
            this.btnCanSolve.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCanSolve.Name = "btnCanSolve";
            this.btnCanSolve.Padding = new System.Windows.Forms.Padding(3);
            this.btnCanSolve.Size = new System.Drawing.Size(114, 39);
            this.btnCanSolve.TabIndex = 4;
            this.btnCanSolve.Text = "Ước lượng";
            this.btnCanSolve.UseVisualStyleBackColor = true;
            this.btnCanSolve.Click += new System.EventHandler(this.btnCanSolve_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 186);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 21);
            this.label2.TabIndex = 3;
            this.label2.Text = "Tốc độ";
            // 
            // tbSpeed
            // 
            this.tbSpeed.Location = new System.Drawing.Point(107, 186);
            this.tbSpeed.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbSpeed.Minimum = 1;
            this.tbSpeed.Name = "tbSpeed";
            this.tbSpeed.Size = new System.Drawing.Size(175, 45);
            this.tbSpeed.TabIndex = 2;
            this.tbSpeed.Value = 5;
            // 
            // btnRandom
            // 
            this.btnRandom.Location = new System.Drawing.Point(11, 51);
            this.btnRandom.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRandom.Name = "btnRandom";
            this.btnRandom.Size = new System.Drawing.Size(114, 39);
            this.btnRandom.TabIndex = 3;
            this.btnRandom.Text = "Ngẫu nhiên";
            this.btnRandom.UseVisualStyleBackColor = true;
            this.btnRandom.Click += new System.EventHandler(this.btnRandom_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "Số ô";
            // 
            // btnChooseImage
            // 
            this.btnChooseImage.Location = new System.Drawing.Point(285, 6);
            this.btnChooseImage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnChooseImage.Name = "btnChooseImage";
            this.btnChooseImage.Size = new System.Drawing.Size(114, 39);
            this.btnChooseImage.TabIndex = 0;
            this.btnChooseImage.Text = "Chọn ảnh";
            this.btnChooseImage.UseVisualStyleBackColor = true;
            this.btnChooseImage.Click += new System.EventHandler(this.btnChooseImage_Click);
            // 
            // cmbSize
            // 
            this.cmbSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSize.FormattingEnabled = true;
            this.cmbSize.Items.AddRange(new object[] {
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.cmbSize.Location = new System.Drawing.Point(92, 11);
            this.cmbSize.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbSize.Name = "cmbSize";
            this.cmbSize.Size = new System.Drawing.Size(161, 29);
            this.cmbSize.TabIndex = 1;
            this.cmbSize.SelectedIndexChanged += new System.EventHandler(this.cmbSize_SelectedIndexChanged);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(285, 177);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(114, 39);
            this.btnReset.TabIndex = 14;
            this.btnReset.Text = "Xóa hết";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(288, 177);
            this.btnStop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(111, 39);
            this.btnStop.TabIndex = 5;
            this.btnStop.Text = "Dừng";
            this.btnStop.UseVisualStyleBackColor = true;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1183, 658);
            this.Controls.Add(this.splitMain);
            this.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormMain";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.grbLeft.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picSelectedImage)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbSpeed)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.GroupBox grbLeft;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tblSplitImages;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnChooseImage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbSize;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox picSelectedImage;
        private System.Windows.Forms.Button btnRandom;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar tbSpeed;
        private System.Windows.Forms.Button btnCanSolve;
        private System.Windows.Forms.Button btnRunSolver;
        private System.Windows.Forms.ComboBox cmbHeuristic;
        private System.Windows.Forms.Label Heuristic;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ProgressBar pbSolveStep;
        private System.Windows.Forms.Label lblSolveStep;
        private System.Windows.Forms.Button btnSolveAllAlgorithm;
        private System.Windows.Forms.Button btnReset;
    }
}