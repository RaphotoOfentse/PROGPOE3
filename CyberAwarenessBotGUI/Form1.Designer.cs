namespace CyberAwarenessBotGUI
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
            this.label1 = new System.Windows.Forms.Label();
            this.TaskTitle = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTaskDescripton = new System.Windows.Forms.TextBox();
            this.ReminderDate = new System.Windows.Forms.DateTimePicker();
            this.lblReminderDate = new System.Windows.Forms.Label();
            this.btnAddTask = new System.Windows.Forms.Button();
            this.btnMarkComplete = new System.Windows.Forms.Button();
            this.btnDeleteTask = new System.Windows.Forms.Button();
            this.FirstTasks = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbTimeframe = new System.Windows.Forms.ComboBox();
            this.richTextBoxChat = new System.Windows.Forms.RichTextBox();
            this.txtUserInput = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Task Title:\r\n\r\n";
            // 
            // TaskTitle
            // 
            this.TaskTitle.Location = new System.Drawing.Point(219, 29);
            this.TaskTitle.Name = "TaskTitle";
            this.TaskTitle.Size = new System.Drawing.Size(274, 22);
            this.TaskTitle.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(54, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Description";
            // 
            // txtTaskDescripton
            // 
            this.txtTaskDescripton.Location = new System.Drawing.Point(219, 69);
            this.txtTaskDescripton.Multiline = true;
            this.txtTaskDescripton.Name = "txtTaskDescripton";
            this.txtTaskDescripton.Size = new System.Drawing.Size(274, 37);
            this.txtTaskDescripton.TabIndex = 3;
            this.txtTaskDescripton.Text = "txtTaskDescription";
            // 
            // ReminderDate
            // 
            this.ReminderDate.Location = new System.Drawing.Point(219, 141);
            this.ReminderDate.Name = "ReminderDate";
            this.ReminderDate.Size = new System.Drawing.Size(274, 22);
            this.ReminderDate.TabIndex = 4;
            // 
            // lblReminderDate
            // 
            this.lblReminderDate.AutoSize = true;
            this.lblReminderDate.Location = new System.Drawing.Point(54, 147);
            this.lblReminderDate.Name = "lblReminderDate";
            this.lblReminderDate.Size = new System.Drawing.Size(101, 16);
            this.lblReminderDate.TabIndex = 5;
            this.lblReminderDate.Text = "Reminder Date:";
            // 
            // btnAddTask
            // 
            this.btnAddTask.Location = new System.Drawing.Point(57, 354);
            this.btnAddTask.Name = "btnAddTask";
            this.btnAddTask.Size = new System.Drawing.Size(187, 52);
            this.btnAddTask.TabIndex = 6;
            this.btnAddTask.Text = "Add Task";
            this.btnAddTask.UseVisualStyleBackColor = true;
            this.btnAddTask.Click += new System.EventHandler(this.btnAddTask_Click);
            // 
            // btnMarkComplete
            // 
            this.btnMarkComplete.Location = new System.Drawing.Point(260, 354);
            this.btnMarkComplete.Name = "btnMarkComplete";
            this.btnMarkComplete.Size = new System.Drawing.Size(187, 52);
            this.btnMarkComplete.TabIndex = 7;
            this.btnMarkComplete.Text = "Mark as Completed";
            this.btnMarkComplete.UseVisualStyleBackColor = true;
            this.btnMarkComplete.Click += new System.EventHandler(this.btnMarkComplete_Click);
            // 
            // btnDeleteTask
            // 
            this.btnDeleteTask.Location = new System.Drawing.Point(465, 354);
            this.btnDeleteTask.Name = "btnDeleteTask";
            this.btnDeleteTask.Size = new System.Drawing.Size(187, 52);
            this.btnDeleteTask.TabIndex = 8;
            this.btnDeleteTask.Text = "Delete Task";
            this.btnDeleteTask.UseVisualStyleBackColor = true;
            this.btnDeleteTask.Click += new System.EventHandler(this.btnDeleteTask_Click);
            // 
            // FirstTasks
            // 
            this.FirstTasks.FormattingEnabled = true;
            this.FirstTasks.ItemHeight = 16;
            this.FirstTasks.Location = new System.Drawing.Point(57, 239);
            this.FirstTasks.Name = "FirstTasks";
            this.FirstTasks.Size = new System.Drawing.Size(757, 84);
            this.FirstTasks.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(54, 202);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "Or remind me in: ";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // cmbTimeframe
            // 
            this.cmbTimeframe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTimeframe.FormattingEnabled = true;
            this.cmbTimeframe.Items.AddRange(new object[] {
            "In 1 day",
            "In 3 days",
            "In 7 days",
            "In 14 days",
            "In 30 days"});
            this.cmbTimeframe.Location = new System.Drawing.Point(219, 202);
            this.cmbTimeframe.Name = "cmbTimeframe";
            this.cmbTimeframe.Size = new System.Drawing.Size(145, 24);
            this.cmbTimeframe.TabIndex = 11;
            // 
            // richTextBoxChat
            // 
            this.richTextBoxChat.Location = new System.Drawing.Point(57, 430);
            this.richTextBoxChat.Name = "richTextBoxChat";
            this.richTextBoxChat.ReadOnly = true;
            this.richTextBoxChat.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBoxChat.Size = new System.Drawing.Size(834, 178);
            this.richTextBoxChat.TabIndex = 12;
            this.richTextBoxChat.Text = "";
            // 
            // txtUserInput
            // 
            this.txtUserInput.Location = new System.Drawing.Point(57, 618);
            this.txtUserInput.Name = "txtUserInput";
            this.txtUserInput.Size = new System.Drawing.Size(650, 22);
            this.txtUserInput.TabIndex = 13;
            this.txtUserInput.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(738, 614);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(94, 26);
            this.btnSend.TabIndex = 14;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 725);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtUserInput);
            this.Controls.Add(this.richTextBoxChat);
            this.Controls.Add(this.cmbTimeframe);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.FirstTasks);
            this.Controls.Add(this.btnDeleteTask);
            this.Controls.Add(this.btnMarkComplete);
            this.Controls.Add(this.btnAddTask);
            this.Controls.Add(this.lblReminderDate);
            this.Controls.Add(this.ReminderDate);
            this.Controls.Add(this.txtTaskDescripton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TaskTitle);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "FirstTask";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TaskTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTaskDescripton;
        private System.Windows.Forms.DateTimePicker ReminderDate;
        private System.Windows.Forms.Label lblReminderDate;
        private System.Windows.Forms.Button btnAddTask;
        private System.Windows.Forms.Button btnMarkComplete;
        private System.Windows.Forms.Button btnDeleteTask;
        private System.Windows.Forms.ListBox FirstTasks;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbTimeframe;
        private System.Windows.Forms.RichTextBox richTextBoxChat;
        private System.Windows.Forms.TextBox txtUserInput;
        private System.Windows.Forms.Button btnSend;
       // private System.Windows.Forms.TextBox txtMultiTaskInput;
        //private System.Windows.Forms.Button btnAddMultipleTasks;

    }
}

