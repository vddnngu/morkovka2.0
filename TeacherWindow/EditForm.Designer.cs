namespace TeacherWindow
{
    partial class EditForm
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
            this.answerRadioButton = new System.Windows.Forms.RadioButton();
            this.questRadioButton = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.changeQuestTextBox = new System.Windows.Forms.RichTextBox();
            this.addAnswerButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // answerRadioButton
            // 
            this.answerRadioButton.AutoSize = true;
            this.answerRadioButton.Location = new System.Drawing.Point(3, 26);
            this.answerRadioButton.Name = "answerRadioButton";
            this.answerRadioButton.Size = new System.Drawing.Size(60, 17);
            this.answerRadioButton.TabIndex = 0;
            this.answerRadioButton.TabStop = true;
            this.answerRadioButton.Text = "Answer";
            this.answerRadioButton.UseVisualStyleBackColor = true;
            this.answerRadioButton.CheckedChanged += new System.EventHandler(this.answerRadioButton_CheckedChanged);
            // 
            // questRadioButton
            // 
            this.questRadioButton.AutoSize = true;
            this.questRadioButton.Location = new System.Drawing.Point(3, 4);
            this.questRadioButton.Name = "questRadioButton";
            this.questRadioButton.Size = new System.Drawing.Size(67, 17);
            this.questRadioButton.TabIndex = 1;
            this.questRadioButton.Text = "Question";
            this.questRadioButton.UseVisualStyleBackColor = true;
            this.questRadioButton.CheckedChanged += new System.EventHandler(this.questRadioButton_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.questRadioButton);
            this.panel1.Controls.Add(this.answerRadioButton);
            this.panel1.Location = new System.Drawing.Point(16, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(71, 49);
            this.panel1.TabIndex = 2;
            // 
            // changeQuestTextBox
            // 
            this.changeQuestTextBox.Location = new System.Drawing.Point(102, 15);
            this.changeQuestTextBox.Name = "changeQuestTextBox";
            this.changeQuestTextBox.Size = new System.Drawing.Size(268, 47);
            this.changeQuestTextBox.TabIndex = 4;
            this.changeQuestTextBox.Text = "";
            this.changeQuestTextBox.TextChanged += new System.EventHandler(this.changeQuestTextBox_TextChanged);
            // 
            // addAnswerButton
            // 
            this.addAnswerButton.Location = new System.Drawing.Point(385, 15);
            this.addAnswerButton.Name = "addAnswerButton";
            this.addAnswerButton.Size = new System.Drawing.Size(79, 47);
            this.addAnswerButton.TabIndex = 5;
            this.addAnswerButton.Text = "Add Answer";
            this.addAnswerButton.UseVisualStyleBackColor = true;
            this.addAnswerButton.Click += new System.EventHandler(this.addAnswerButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(324, 339);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 6;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(405, 339);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // EditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(486, 374);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.addAnswerButton);
            this.Controls.Add(this.changeQuestTextBox);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "EditForm";
            this.Text = "EditForm";
            this.Load += new System.EventHandler(this.EditForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton answerRadioButton;
        private System.Windows.Forms.RadioButton questRadioButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox changeQuestTextBox;
        private System.Windows.Forms.Button addAnswerButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
    }
}