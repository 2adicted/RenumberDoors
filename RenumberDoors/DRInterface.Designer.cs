namespace RenumberDoors
{
    partial class DRInterface
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
            this.selectBtn = new System.Windows.Forms.Button();
            this.levelDrop = new System.Windows.Forms.ComboBox();
            this.doorTypeDropDefault = new System.Windows.Forms.ComboBox();
            this.levelLbl = new System.Windows.Forms.Label();
            this.doorTypeLbl = new System.Windows.Forms.Label();
            this.prefixTextBox = new System.Windows.Forms.TextBox();
            this.suffixTextBox = new System.Windows.Forms.TextBox();
            this.doorNameLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // selectBtn
            // 
            this.selectBtn.Location = new System.Drawing.Point(33, 233);
            this.selectBtn.Name = "selectBtn";
            this.selectBtn.Size = new System.Drawing.Size(75, 23);
            this.selectBtn.TabIndex = 0;
            this.selectBtn.Text = "Select ";
            this.selectBtn.UseVisualStyleBackColor = true;
            this.selectBtn.Click += new System.EventHandler(this.selectBtn_Click);
            // 
            // levelDrop
            // 
            this.levelDrop.FormattingEnabled = true;
            this.levelDrop.Location = new System.Drawing.Point(33, 45);
            this.levelDrop.Name = "levelDrop";
            this.levelDrop.Size = new System.Drawing.Size(177, 21);
            this.levelDrop.TabIndex = 1;
            // 
            // doorTypeDropDefault
            // 
            this.doorTypeDropDefault.FormattingEnabled = true;
            this.doorTypeDropDefault.Items.AddRange(new object[] {
            "Any"});
            this.doorTypeDropDefault.Location = new System.Drawing.Point(33, 96);
            this.doorTypeDropDefault.Name = "doorTypeDropDefault";
            this.doorTypeDropDefault.Size = new System.Drawing.Size(177, 21);
            this.doorTypeDropDefault.TabIndex = 2;
            // 
            // levelLbl
            // 
            this.levelLbl.AutoSize = true;
            this.levelLbl.Location = new System.Drawing.Point(33, 27);
            this.levelLbl.Name = "levelLbl";
            this.levelLbl.Size = new System.Drawing.Size(33, 13);
            this.levelLbl.TabIndex = 3;
            this.levelLbl.Text = "Level";
            // 
            // doorTypeLbl
            // 
            this.doorTypeLbl.AutoSize = true;
            this.doorTypeLbl.Location = new System.Drawing.Point(33, 78);
            this.doorTypeLbl.Name = "doorTypeLbl";
            this.doorTypeLbl.Size = new System.Drawing.Size(57, 13);
            this.doorTypeLbl.TabIndex = 4;
            this.doorTypeLbl.Text = "Door Type";
            // 
            // prefixTextBox
            // 
            this.prefixTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.prefixTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.prefixTextBox.Location = new System.Drawing.Point(107, 154);
            this.prefixTextBox.Name = "prefixTextBox";
            this.prefixTextBox.Size = new System.Drawing.Size(103, 13);
            this.prefixTextBox.TabIndex = 5;
            this.prefixTextBox.TextChanged += new System.EventHandler(this.prefixTextBox_TextChanged);
            // 
            // suffixTextBox
            // 
            this.suffixTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.suffixTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.suffixTextBox.Location = new System.Drawing.Point(107, 173);
            this.suffixTextBox.Name = "suffixTextBox";
            this.suffixTextBox.Size = new System.Drawing.Size(103, 13);
            this.suffixTextBox.TabIndex = 6;
            this.suffixTextBox.TextChanged += new System.EventHandler(this.suffixTextBox_TextChanged);
            // 
            // doorNameLabel
            // 
            this.doorNameLabel.AutoSize = true;
            this.doorNameLabel.Location = new System.Drawing.Point(104, 199);
            this.doorNameLabel.Name = "doorNameLabel";
            this.doorNameLabel.Size = new System.Drawing.Size(19, 13);
            this.doorNameLabel.TabIndex = 7;
            this.doorNameLabel.Text = "01";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 154);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Prefix";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 173);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Suffix";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 199);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Door Name";
            // 
            // DRInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(256, 286);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.doorNameLabel);
            this.Controls.Add(this.suffixTextBox);
            this.Controls.Add(this.prefixTextBox);
            this.Controls.Add(this.doorTypeLbl);
            this.Controls.Add(this.levelLbl);
            this.Controls.Add(this.doorTypeDropDefault);
            this.Controls.Add(this.levelDrop);
            this.Controls.Add(this.selectBtn);
            this.Name = "DRInterface";
            this.Text = "DRInterface";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button selectBtn;
        private System.Windows.Forms.ComboBox levelDrop;
        private System.Windows.Forms.ComboBox doorTypeDropDefault;
        private System.Windows.Forms.Label levelLbl;
        private System.Windows.Forms.Label doorTypeLbl;
        private System.Windows.Forms.TextBox prefixTextBox;
        private System.Windows.Forms.TextBox suffixTextBox;
        private System.Windows.Forms.Label doorNameLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}