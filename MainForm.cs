/// Philip Rönnmark 990513-4392 2021-12-12
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace A6
{
    public partial class MainForm : Form
    {
        private Task task = new Task();
        private bool selectedTask = false;
        public MainForm()
        {
            InitializeComponent();
            CheckButtonEnableOrNot();


            cboxPriority.DataSource = Enum.GetValues(typeof(PriorityType));
        }

        /// <summary>
        /// Refreshed Listbox, E.g when item is added
        /// </summary>
        private void refreshLbox()
        {
            CheckButtonEnableOrNot();
            int i = 0;
            lboxTasks.Items.Clear();
            foreach(Task t in TaskManager.GetTasks())
            {
                lboxTasks.Items.Add(t);
            }
        }

        /// <summary>
        /// Checks if buttons should be enabled or not
        /// </summary>
        private void CheckButtonEnableOrNot()
        {

            if (TaskManager.GetTasks().Count > 0 && selectedTask)
            {
                btnChange.Enabled = true;
                btnDelete.Enabled = true;

            } else
            {
                btnChange.Enabled = false;
                btnDelete.Enabled = false;
            }

        }

        /// <summary>
        /// If index changes enable or disable button, and fill fields for changing.
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lboxTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            

            Console.WriteLine(lboxTasks.SelectedIndex);
            if(lboxTasks.SelectedIndex > -1)
            {
                displayTaskForChange(TaskManager.GetTask(lboxTasks.SelectedIndex));
                selectedTask = true;
                
            }
            else
            {
                selectedTask = false;
                
            }
            CheckButtonEnableOrNot();


        }

        /// <summary>
        /// Adds task to taskManagers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {

            if (txtDesc.Text != "")
            {
                task.Date = dateTimePicker.Value;

                task.Description = txtDesc.Text;
                task.Priority = (PriorityType)cboxPriority.SelectedItem;
                TaskManager.AddTask(task);
                task = new Task();
                lboxTasks.SelectedIndex = -1; //So that form does not auto select the next list item
                refreshLbox();
                resetAddFields();
            } else
            {
                MessageBox.Show("Provide a valid description");
            }



        }

        /// <summary>
        /// Resets input fields to give better user experience
        /// </summary>
        private void resetAddFields()
        {
            dateTimePicker.Value = DateTime.Now; 
            txtDesc.Text = "";
            cboxPriority.SelectedItem = PriorityType.Very_important;
        }

        /// <summary>
        /// Deletes selected task in listbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {

                if (lboxTasks.SelectedIndex != -1)
                {
                DialogResult dialogResult = MessageBox.Show("This will delete the task, are you sure?", "Close", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    TaskManager.deleteTask(lboxTasks.SelectedIndex);
                    lboxTasks.SelectedIndex = -1; //So that form does not auto select the next list item
                    refreshLbox();
                }
                
                }

            resetAddFields();

        }

        /// <summary>
        /// Shows current time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {

            lblTimer.Text = DateTime.Now.ToString("hh:mm:ss");
        }

        /// <summary>
        /// Saves changes in input fields to selected task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChange_Click(object sender, EventArgs e)
        {
            task.Date = dateTimePicker.Value;
            task.Description = txtDesc.Text;
            task.Priority = (PriorityType)cboxPriority.SelectedItem;
            refreshLbox();
            resetAddFields();
            task = new Task();

        }

        /// <summary>
        /// Displays task properties in input fields
        /// </summary>
        /// <param name="t">Task to be displayed</param>
        private void displayTaskForChange(Task t )
        {
            task = t;
            dateTimePicker.Value = t.Date;
            txtDesc.Text = t.Description;
            cboxPriority.SelectedItem = t.Priority;
        }

        /// <summary>
        /// Clears and resets the form and taskmanager
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            this.InitializeComponent();
            cboxPriority.DataSource = Enum.GetValues(typeof(PriorityType));
            TaskManager.resetTasks();

        }

        /// <summary>
        /// Open files saved in bin folder.
        /// bin\Debug\reminders.txt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string line;
            var path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "reminders.txt");
            Console.WriteLine(path);
            // Read the file and display it line by line.
            System.IO.StreamReader file =
                new System.IO.StreamReader(path);
            while ((line = file.ReadLine()) != null)
            {
                string[] words = line.Split(',');
                task.Date = Convert.ToDateTime(words[0]);
                task.Description = words[1];
                PriorityType p;
                task.Priority = (PriorityType)Enum.Parse(typeof(PriorityType), words[2]);
                TaskManager.AddTask(task);
                task = new Task();
                refreshLbox();


            }

            file.Close();


        }
        /// <summary>
        /// Saves txt file to bin\Debug\reminders.txt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //path for me \bin\Debug\reminders.txt
            var path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "reminders.txt");
            using (TextWriter tw = new StreamWriter(path))
            {
                foreach (Task t in TaskManager.GetTasks())
                {
                    tw.WriteLine(string.Format("{0},{1},{2}", t.Date, t.Description, t.Priority));
                }
            }
        }
        /// <summary>
        /// Shows About Box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (AboutBox1 box = new AboutBox1())
            {
                box.ShowDialog(this);
            }
        }
        /// <summary>
        /// Closes program if user checks yes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Sure to exit program?", "Close", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                this.Dispose(true);
            }
        }
    }
}
