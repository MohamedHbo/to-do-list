using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace TO_DO_LIST
{
    
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
            sort();
            LoadSavedData(); // Load saved data when the form is loaded
            this.FormClosing += Home_FormClosing; // Attach the FormClosing event handler
                                                  // this.FormClosing += Home_FormClosing;
        }

        private void Home_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveCheckedListBoxData(); // Save data when the form is closing
        }

        private void SaveCheckedListBoxData()
        {
            // Save data from checkedListBox1
            List<TodoItem> items1 = checkedListBox1.Items.Cast<TodoItem>().ToList();
            // Save data from checkedListBox2
            List<TodoItem> items2 = checkedListBox2.Items.Cast<TodoItem>().ToList();
            // Save data from checkedListBox3
            List<TodoItem> items3 = checkedListBox3.Items.Cast<TodoItem>().ToList();

            // Serialize the lists of items to a file
            SerializeToBinaryFile("checkedListBox1.dat", items1);
            SerializeToBinaryFile("checkedListBox2.dat", items2);
            SerializeToBinaryFile("checkedListBox3.dat", items3);
        }

        private void LoadSavedData()
        {
            // Deserialize data and load it back into checked list boxes

            // Deserialize data from "checkedListBox1.dat" file and store it in items1 list
            List<TodoItem> items1 = DeserializeFromBinaryFile<List<TodoItem>>("checkedListBox1.dat");
            List<TodoItem> items2 = DeserializeFromBinaryFile<List<TodoItem>>("checkedListBox2.dat");
            List<TodoItem> items3 = DeserializeFromBinaryFile<List<TodoItem>>("checkedListBox3.dat");

            if (items1 != null)
            {
                foreach (var item in items1)
                {
                    checkedListBox1.Items.Add(item);
                }
            }

            if (items2 != null)
            {
                foreach (var item in items2)
                {
                    checkedListBox2.Items.Add(item);
                }
            }

            if (items3 != null)
            {
                foreach (var item in items3)
                {
                    checkedListBox3.Items.Add(item);
                }
            }
        }

        private void SerializeToBinaryFile<T>(string filePath, T data)
        {
            using (Stream stream = File.Open(filePath, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, data);
            }
        }

        private T DeserializeFromBinaryFile<T>(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return default(T); // Return default value if the file doesn't exist
            }

            try
            {
                using (Stream stream = File.Open(filePath, FileMode.Open))
                {
                    if (stream.Length > 0) // Check if the stream is not empty
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        return (T)formatter.Deserialize(stream);
                    }
                    else
                    {
                        return default(T); // Return default value if the stream is empty
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deserializing from file: {ex.Message}");
                return default(T); // Return default value in case of any exception
            }
        }



        private void sort()
        {
            // Sort checkedListBox1
            checkedListBox1.Sorted = false;
            List<TodoItem> items1 = checkedListBox1.Items.Cast<TodoItem>().ToList();
            items1.Sort(new Datacomparer()); // Sort items using Datacomparer comparer
            checkedListBox1.Items.Clear(); // Clear items in checkedListBox1
            checkedListBox1.Items.AddRange(items1.ToArray()); // Add sorted items back to checkedListBox1
            checkedListBox1.Refresh(); // Refresh the list to reflect the changes

            // Sort checkedListBox2
            checkedListBox2.Sorted = false;
            List<TodoItem> items2 = checkedListBox2.Items.Cast<TodoItem>().ToList();
            items2.Sort(new Datacomparer()); // Sort items using Datacomparer comparer
            checkedListBox2.Items.Clear(); // Clear items in checkedListBox2
            checkedListBox2.Items.AddRange(items2.ToArray()); // Add sorted items back to checkedListBox2
            checkedListBox2.Refresh(); // Refresh the list to reflect the changes

            // Sort checkedListBox3
            checkedListBox3.Sorted = false;
            List<TodoItem> items3 = checkedListBox3.Items.Cast<TodoItem>().ToList();
            items3.Sort(new Datacomparer()); // Sort items using Datacomparer comparer
            checkedListBox3.Items.Clear(); // Clear items in checkedListBox3
            checkedListBox3.Items.AddRange(items3.ToArray()); // Add sorted items back to checkedListBox3
            checkedListBox3.Refresh(); // Refresh the list to reflect the changes

            // Sort checkedListBox4
            checkedListBox4.Sorted = false;
            List<TodoItem> items4 = checkedListBox4.Items.Cast<TodoItem>().ToList();
            items4.Sort(new Datacomparer()); // Sort items using Datacomparer comparer
            checkedListBox4.Items.Clear(); // Clear items in checkedListBox4
            checkedListBox4.Items.AddRange(items4.ToArray()); // Add sorted items back to checkedListBox4
            checkedListBox4.Refresh(); // Refresh the list to reflect the changes
        }


        private void button5_Click(object sender, EventArgs e)
        {
            TodoItem selectedItem = (TodoItem)checkedListBox1.SelectedItem;

            if (checkedListBox1.SelectedItem != null)
            {
              //  TodoItem selectedItem = (TodoItem)checkedListBox1.SelectedItem;

                checkedListBox1.Items.Remove(selectedItem);
                checkedListBox2.Items.Add(selectedItem);
            }
            if (selectedItem.Date.Date == DateTime.Today)
            {
                checkedListBox3.Items.Remove(selectedItem);
                checkedListBox1.Items.Remove(selectedItem);
                checkedListBox2.Items.Add(selectedItem);

            }
            else
            {
                MessageBox.Show("Please select an item to move.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
           sort(); // Sort the lists after removal
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.SelectedItem != null)
            {
                TodoItem selectedItem = (TodoItem)checkedListBox1.SelectedItem;

                checkedListBox1.Items.Remove(selectedItem);

                // Remove the corresponding item from checkedListBox3 if it exists
                if (checkedListBox3.Items.Contains(selectedItem))
                {
                    checkedListBox3.Items.Remove(selectedItem);
                }
            }
            else
            {
                MessageBox.Show("Please select an item to delete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        
        private void button7_Click(object sender, EventArgs e)
        {

            // Create a list to hold the indices of items to remove from checkedListBox3
            List<int> indicesToRemove = new List<int>();

            // Loop through checkedListBox3 items to find checked items and their indices
            for (int i = checkedListBox3.Items.Count - 1; i >= 0; i--)
            {
                if (checkedListBox3.GetItemChecked(i))
                {
                    indicesToRemove.Add(i); // Add the index to the removal list
                }
            }

            // Remove items from checkedListBox3 using the indices in reverse order
            foreach (int index in indicesToRemove)
            {
                if (index >= 0 && index < checkedListBox3.Items.Count)
                {
                    TodoItem itemInCheckedListBox3 = (TodoItem)checkedListBox3.Items[index];

                    // Check if the index is valid for checkedListBox1 before removing
                    if (index < checkedListBox1.Items.Count)
                    {
                        TodoItem itemInCheckedListBox1 = (TodoItem)checkedListBox1.Items[index];

                        // Remove corresponding item from checkedListBox1
                        checkedListBox1.Items.Remove(itemInCheckedListBox1);
                    }

                    // Remove item from checkedListBox3
                    checkedListBox3.Items.RemoveAt(index);
                }
            }
            sort();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (checkedListBox3.SelectedItem != null)
            {
                TodoItem selectedItem = (TodoItem)checkedListBox3.SelectedItem;

                checkedListBox3.Items.Remove(selectedItem);
                checkedListBox2.Items.Add(selectedItem);

                // Check if the date is today and remove from checkedListBox1 if needed
                if (selectedItem.Date.Date == DateTime.Today)
                {
                    checkedListBox1.Items.Remove(selectedItem);
                }
            }
            else
            {
                MessageBox.Show("Please select an item to move.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            sort(); // Sort the lists after removal

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("the developers of this project is: " + "\n" + "     Mohamed Mostafa Ahmed Eltaieb" + "\n" + "     Mohamed Khaled Mohamed" + "\n" + "     Mohamed Elsayed Youssif" + "\n" + "     Youssif Mohamed Farrag");
        }

       

        private void button2_Click_1(object sender, EventArgs e)
        {
            checkedListBox4.Items.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {

           
            foreach(var item in checkedListBox1.Items) 
            {

                TodoItem todoItem = (TodoItem)item;
                if(todoItem.Date.Date == monthCalendar2.SelectionStart)
                {
                    checkedListBox4.Items.Add(todoItem);

                }
            
            
            
            }

            sort();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            TodoItem Item1 = new TodoItem()
            {
                Name = textBox1.Text,
                Notes = textBox2.Text,
                Time = dateTimePicker1.Value,
                Date = monthCalendar1.SelectionStart

            };
            if (checkedListBox4.SelectedItem != null)
            {
               Item1 = (TodoItem)checkedListBox4.SelectedItem;
                checkedListBox1.Items.Remove(Item1);

            }

            if (Item1.Date.Date == DateTime.Today)
            {
                checkedListBox1.Items.Remove(Item1);
                checkedListBox3.Items.Remove(Item1);


            }
            checkedListBox4.Items.Remove(Item1);

            sort();


        }

       /* private void Home_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveCheckedListBoxData();
        }
        private void SaveCheckedListBoxData()
        {
            // Save data from checkedListBox1
            List<TodoItem> items1 = checkedListBox1.Items.Cast<TodoItem>().ToList();
            // Save data from checkedListBox2
            List<TodoItem> items2 = checkedListBox2.Items.Cast<TodoItem>().ToList();
            // Save data from checkedListBox3
            List<TodoItem> items3 = checkedListBox3.Items.Cast<TodoItem>().ToList();

            // Here you can save these lists of items to a file, database, or any other storage mechanism
            // For example, you can use Entity Framework to save them to your database

            using (var context = new db())
            {
                context.Todoitems.AddRange(items1);
                context.Todoitems.AddRange(items2);
                context.Todoitems.AddRange(items3);
                context.SaveChanges();
            }
        }*/

        private void button8_Click_1(object sender, EventArgs e)
        {
            TodoItem newItem = new TodoItem
            {
                Name = textBox1.Text,
                Notes = textBox2.Text,
                Time = dateTimePicker1.Value,
                Date = monthCalendar1.SelectionStart

            };
            bool taskExists = checkedListBox1.Items.Cast<TodoItem>()
                        .Any(item => item.Date.Date == newItem.Date.Date && item.Time.TimeOfDay == newItem.Time.TimeOfDay);

            if (taskExists)
            {
                MessageBox.Show("A task with the same date and time already exists. Please edit the time.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (newItem.Date.Date == DateTime.Today)
                {
                    checkedListBox1.Items.Add(newItem);
                    checkedListBox3.Items.Add(newItem);

                }
                else
                {
                    checkedListBox1.Items.Add(newItem);
                }
            }
           
            sort();

        }

        private void Home_Load(object sender, EventArgs e)
        {

        }
    }
    }

