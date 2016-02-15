namespace WiDAC
{
    partial class WiDACForm
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
            this.outputDeviceComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.bitRateComboBox = new System.Windows.Forms.ComboBox();
            this.rateComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.inputDeviceComboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.videoBitrateComboBox = new System.Windows.Forms.ComboBox();
            this.frameRateComboBox = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.screenComboBox = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.captureCheckBox = new System.Windows.Forms.CheckBox();
            this.previewPanel = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Output device";
            // 
            // outputDeviceComboBox
            // 
            this.outputDeviceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.outputDeviceComboBox.FormattingEnabled = true;
            this.outputDeviceComboBox.Location = new System.Drawing.Point(98, 19);
            this.outputDeviceComboBox.Name = "outputDeviceComboBox";
            this.outputDeviceComboBox.Size = new System.Drawing.Size(239, 21);
            this.outputDeviceComboBox.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.bitRateComboBox);
            this.groupBox1.Controls.Add(this.rateComboBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 97);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(351, 79);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Audio Capture Settings";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(55, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Bitrate";
            // 
            // bitRateComboBox
            // 
            this.bitRateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.bitRateComboBox.FormattingEnabled = true;
            this.bitRateComboBox.Location = new System.Drawing.Point(98, 46);
            this.bitRateComboBox.Name = "bitRateComboBox";
            this.bitRateComboBox.Size = new System.Drawing.Size(239, 21);
            this.bitRateComboBox.TabIndex = 8;
            // 
            // rateComboBox
            // 
            this.rateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.rateComboBox.FormattingEnabled = true;
            this.rateComboBox.Location = new System.Drawing.Point(98, 19);
            this.rateComboBox.Name = "rateComboBox";
            this.rateComboBox.Size = new System.Drawing.Size(239, 21);
            this.rateComboBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Sample rate";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.inputDeviceComboBox);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.outputDeviceComboBox);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(351, 79);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Audio Capture Devices";
            // 
            // inputDeviceComboBox
            // 
            this.inputDeviceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.inputDeviceComboBox.FormattingEnabled = true;
            this.inputDeviceComboBox.Location = new System.Drawing.Point(98, 46);
            this.inputDeviceComboBox.Name = "inputDeviceComboBox";
            this.inputDeviceComboBox.Size = new System.Drawing.Size(239, 21);
            this.inputDeviceComboBox.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 49);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Input device";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.videoBitrateComboBox);
            this.groupBox3.Controls.Add(this.frameRateComboBox);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.screenComboBox);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Location = new System.Drawing.Point(12, 182);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(351, 107);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Video Capture Settings";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(55, 76);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Bitrate";
            // 
            // videoBitrateComboBox
            // 
            this.videoBitrateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.videoBitrateComboBox.FormattingEnabled = true;
            this.videoBitrateComboBox.Location = new System.Drawing.Point(98, 73);
            this.videoBitrateComboBox.Name = "videoBitrateComboBox";
            this.videoBitrateComboBox.Size = new System.Drawing.Size(239, 21);
            this.videoBitrateComboBox.TabIndex = 8;
            // 
            // frameRateComboBox
            // 
            this.frameRateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.frameRateComboBox.FormattingEnabled = true;
            this.frameRateComboBox.Location = new System.Drawing.Point(98, 46);
            this.frameRateComboBox.Name = "frameRateComboBox";
            this.frameRateComboBox.Size = new System.Drawing.Size(239, 21);
            this.frameRateComboBox.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(35, 49);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(57, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Frame rate";
            // 
            // screenComboBox
            // 
            this.screenComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.screenComboBox.FormattingEnabled = true;
            this.screenComboBox.Location = new System.Drawing.Point(98, 19);
            this.screenComboBox.Name = "screenComboBox";
            this.screenComboBox.Size = new System.Drawing.Size(239, 21);
            this.screenComboBox.TabIndex = 3;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(51, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 13);
            this.label10.TabIndex = 2;
            this.label10.Text = "Screen";
            // 
            // captureCheckBox
            // 
            this.captureCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.captureCheckBox.Location = new System.Drawing.Point(382, 31);
            this.captureCheckBox.Name = "captureCheckBox";
            this.captureCheckBox.Size = new System.Drawing.Size(140, 133);
            this.captureCheckBox.TabIndex = 11;
            this.captureCheckBox.Text = "Capture";
            this.captureCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.captureCheckBox.UseVisualStyleBackColor = true;
            // 
            // previewPanel
            // 
            this.previewPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.previewPanel.Location = new System.Drawing.Point(382, 189);
            this.previewPanel.Name = "previewPanel";
            this.previewPanel.Size = new System.Drawing.Size(140, 100);
            this.previewPanel.TabIndex = 12;
            // 
            // WiDACForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 301);
            this.Controls.Add(this.previewPanel);
            this.Controls.Add(this.captureCheckBox);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "WiDACForm";
            this.Text = "WiDAC";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.WiDACForm_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox outputDeviceComboBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox rateComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox bitRateComboBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox inputDeviceComboBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox videoBitrateComboBox;
        private System.Windows.Forms.ComboBox frameRateComboBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox screenComboBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox captureCheckBox;
        private System.Windows.Forms.Panel previewPanel;
    }
}

