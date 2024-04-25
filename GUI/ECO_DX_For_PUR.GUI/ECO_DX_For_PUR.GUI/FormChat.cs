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

namespace ECO_DX_For_PUR.GUI
{
    public partial class FormChat : Form
    {
        private FileSystemWatcher watcher;
        private string filePath = @"\\172.28.10.12\DX Center\7.ECN_Management\Doc\logtxt.txt"; // Specify the path to your text file
        public FormChat()
        {
            InitializeComponent();
            InitializeFileSystemWatcher();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string content = txtContent.Text;
            // Append the text to the selected file
            using (StreamWriter writer = File.AppendText(filePath))
            {
                if(!string.IsNullOrEmpty(content))
                writer.WriteLine($"{System.Environment.MachineName}: {content}");
            }

        }
        private void InitializeFileSystemWatcher()
        {
            watcher = new FileSystemWatcher();
            watcher.Path = Path.GetDirectoryName(filePath);
            watcher.Filter = Path.GetFileName(filePath);
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.Changed += OnFileChanged;

            // Begin watching the file
            watcher.EnableRaisingEvents = true;
        }

        private void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            // Read the updated contents of the file
            string fileContents = File.ReadAllText(filePath);

            // Update UI with the new contents
            UpdateTextBox(fileContents);
        }

        private void UpdateTextBox(string newText)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<string>(UpdateTextBox), newText);
                return;
            }

            txtInbox.Text = newText;
        }

        private void FormChat_Load(object sender, EventArgs e)
        {

        }
    }
}
