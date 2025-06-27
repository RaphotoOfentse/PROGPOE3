using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CyberAwarenessBotGUI;
using CyberChatbotLib;

namespace CyberAwarenessBotGUI
{
    public partial class Form1 : Form
    {

        TaskManager taskManager;
        ChatbotLogic chatbotLogic;

        public Form1()
        {
            InitializeComponent();

            // Attach event handler for your new button
            //btnAddMultipleTasks.Click += btnAddMultipleTasks_Click;
            taskManager = new TaskManager(); // ✅ lowercase variable
            chatbotLogic = new ChatbotLogic("User", taskManager);
        }


        private void btnAddTask_Click(object sender, EventArgs e)
        {
            string title = TaskTitle.Text.Trim();
            string description = txtTaskDescripton.Text.Trim();
            DateTime reminderDate;

            if (string.IsNullOrEmpty(title))
            {
                MessageBox.Show("Please enter a task title.");
                return;
            }

            // Check if a timeframe was selected
            string timeframe = cmbTimeframe.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(timeframe) && timeframe != "(none)")
            {
                // Parse timeframe
                if (timeframe.Contains("1 day")) reminderDate = DateTime.Now.AddDays(1);
                else if (timeframe.Contains("3 days")) reminderDate = DateTime.Now.AddDays(3);
                else if (timeframe.Contains("7 days")) reminderDate = DateTime.Now.AddDays(7);
                else if (timeframe.Contains("14 days")) reminderDate = DateTime.Now.AddDays(14);
                else if (timeframe.Contains("30 days")) reminderDate = DateTime.Now.AddDays(30);
                else reminderDate = ReminderDate.Value; // fallback to chosen date
            }
            else
            {
                // Use the manually selected date
                reminderDate = ReminderDate.Value;
            }

            string formattedTask = $"[ ] {title} - {description} (Remind on {reminderDate.ToShortDateString()})";
            FirstTasks.Items.Add(formattedTask);

            // Also add the task to the UI list (ListBox or ListView)
            //FirstTasks.Items.Add($"{title} - {description} [Incomplete]");

            // Clear input fields
            TaskTitle.Clear();
            txtTaskDescripton.Clear();
            cmbTimeframe.SelectedIndex = 0; // Reset dropdown
            ReminderDate.Value = DateTime.Now;
        }
        
        private void btnMarkComplete_Click(object sender, EventArgs e)
        {
            if (FirstTasks.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a task to mark as completed.");
                return;
            }

            // Mark the task as completed visually (you can update the text)
            string selectedTask = FirstTasks.SelectedItem.ToString();

            // If it's already marked as completed
            if (selectedTask.Contains("[Completed]"))
            {
                MessageBox.Show("This task is already marked as completed.");
                return;
            }

            // Remove old [ ] or [Incomplete] tag and replace with [Completed]
            string updatedTask = selectedTask;

            if (selectedTask.Contains("[ ]"))
            {
                updatedTask = selectedTask.Replace("[ ]", "[Completed]");
            }
            else if (selectedTask.Contains("[Incomplete]"))
            {
                updatedTask = selectedTask.Replace("[Incomplete]", "[Completed]");
            }
            else
            {
                // If no tag, just add [Completed] to the end
                updatedTask += " [Completed]";
            }

            FirstTasks.Items[FirstTasks.SelectedIndex] = updatedTask;
        }

        // Delete Task Button
        private void btnDeleteTask_Click(object sender, EventArgs e)
        {
            if (FirstTasks.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a task to delete.");
                return;
            }

            // Remove from TaskManager as well if used
            // taskManager.RemoveTask(FirstTasks.SelectedIndex);

            FirstTasks.Items.RemoveAt(FirstTasks.SelectedIndex);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbTimeframe.SelectedIndex = 0;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // You can leave this empty or add code
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string userInput = txtUserInput.Text.Trim();
            //MessageBox.Show("Typed: " + userInput);  // debug

            if (string.IsNullOrWhiteSpace(userInput))
            {
                MessageBox.Show("Please enter some input.");
                return;
            }

            richTextBoxChat.AppendText("You: " + userInput + "\n");

            string response = chatbotLogic.ProcessInput(userInput);
            richTextBoxChat.AppendText("Bot: " + response + "\n");

            txtUserInput.Clear();
        }



    }

}