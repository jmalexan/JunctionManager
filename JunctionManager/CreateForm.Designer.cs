namespace JunctionManager {
    partial class CreateForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateForm));
            this.createButton = new System.Windows.Forms.Button();
            this.junctionLabel = new System.Windows.Forms.Label();
            this.junctionTextBox = new System.Windows.Forms.TextBox();
            this.junctionBrowseButton = new System.Windows.Forms.Button();
            this.targetBrowseButton = new System.Windows.Forms.Button();
            this.targetTextBox = new System.Windows.Forms.TextBox();
            this.targetLabel = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // createButton
            // 
            this.createButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.createButton.Location = new System.Drawing.Point(395, 91);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(75, 25);
            this.createButton.TabIndex = 0;
            this.createButton.Text = "Create";
            this.createButton.UseVisualStyleBackColor = true;
            this.createButton.Click += new System.EventHandler(this.createButton_Click);
            // 
            // junctionLabel
            // 
            this.junctionLabel.AutoSize = true;
            this.junctionLabel.Location = new System.Drawing.Point(13, 13);
            this.junctionLabel.Name = "junctionLabel";
            this.junctionLabel.Size = new System.Drawing.Size(65, 17);
            this.junctionLabel.TabIndex = 1;
            this.junctionLabel.Text = "Junction:";
            // 
            // junctionTextBox
            // 
            this.junctionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.junctionTextBox.Location = new System.Drawing.Point(84, 10);
            this.junctionTextBox.Name = "junctionTextBox";
            this.junctionTextBox.Size = new System.Drawing.Size(296, 22);
            this.junctionTextBox.TabIndex = 2;
            // 
            // junctionBrowseButton
            // 
            this.junctionBrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.junctionBrowseButton.Location = new System.Drawing.Point(395, 10);
            this.junctionBrowseButton.Name = "junctionBrowseButton";
            this.junctionBrowseButton.Size = new System.Drawing.Size(75, 23);
            this.junctionBrowseButton.TabIndex = 3;
            this.junctionBrowseButton.Text = "Browse";
            this.junctionBrowseButton.UseVisualStyleBackColor = true;
            this.junctionBrowseButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // targetBrowseButton
            // 
            this.targetBrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.targetBrowseButton.Location = new System.Drawing.Point(395, 38);
            this.targetBrowseButton.Name = "targetBrowseButton";
            this.targetBrowseButton.Size = new System.Drawing.Size(75, 23);
            this.targetBrowseButton.TabIndex = 6;
            this.targetBrowseButton.Text = "Browse";
            this.targetBrowseButton.UseVisualStyleBackColor = true;
            this.targetBrowseButton.Click += new System.EventHandler(this.targetBrowseButton_Click);
            // 
            // targetTextBox
            // 
            this.targetTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.targetTextBox.Location = new System.Drawing.Point(84, 38);
            this.targetTextBox.Name = "targetTextBox";
            this.targetTextBox.Size = new System.Drawing.Size(296, 22);
            this.targetTextBox.TabIndex = 5;
            // 
            // targetLabel
            // 
            this.targetLabel.AutoSize = true;
            this.targetLabel.Location = new System.Drawing.Point(13, 41);
            this.targetLabel.Name = "targetLabel";
            this.targetLabel.Size = new System.Drawing.Size(54, 17);
            this.targetLabel.TabIndex = 4;
            this.targetLabel.Text = "Target:";
            // 
            // CreateForm
            // 
            this.AcceptButton = this.createButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 128);
            this.Controls.Add(this.targetBrowseButton);
            this.Controls.Add(this.targetTextBox);
            this.Controls.Add(this.targetLabel);
            this.Controls.Add(this.junctionBrowseButton);
            this.Controls.Add(this.junctionTextBox);
            this.Controls.Add(this.junctionLabel);
            this.Controls.Add(this.createButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CreateForm";
            this.Text = "Create a Junction";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button createButton;
        private System.Windows.Forms.Label junctionLabel;
        private System.Windows.Forms.TextBox junctionTextBox;
        private System.Windows.Forms.Button junctionBrowseButton;
        private System.Windows.Forms.Button targetBrowseButton;
        private System.Windows.Forms.TextBox targetTextBox;
        private System.Windows.Forms.Label targetLabel;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}