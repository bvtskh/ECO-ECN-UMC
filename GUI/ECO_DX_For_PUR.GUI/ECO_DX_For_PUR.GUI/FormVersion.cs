using ECO_DX_For_PUR.DATA.Bussiness.Interface;
using ECO_DX_For_PUR.DATA.Bussiness.SQLHelper;
using ECO_DX_For_PUR.DATA.Entities.ECN_ECO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECO_DX_For_PUR.GUI
{
    public partial class FormVersion : Form
    {
        ILOGIN _versonHelper = new LoginHelper();
        public FormVersion()
        {
            InitializeComponent();
        }

        private void FormVersion_Load(object sender, EventArgs e)
        {
            string currentVer = _versonHelper.GetCurrentVersion();
            txtcurrentVer.Text = currentVer;
            int nextver = int.Parse(currentVer.Split('.').Last())+1;
            txtNextVer.Text = currentVer.Substring(0, 6) + nextver;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (IsNextversion(txtcurrentVer, txtNextVer))
            {
                CopyFilesAndFoldersToMyFolder();
            }
            else
            {
                MessageBox.Show("Sai định dạng vesion!");
            }
        }
        private void CopyFilesAndFoldersToMyFolder()
        {
            if(System.Environment.MachineName != "UMC-C862")
            {
                MessageBox.Show("Liên hệ máy C862-DX để cập nhật!");
                return;
            }
            try
            {
                string sourceDirectory = @"C:\Users\U42107\Desktop\ThanhDX\Thanh_Project\ECO DX for PUR\ECO DX for PUR\GUI\ECO_DX_For_PUR.GUI\ECO_DX_For_PUR.GUI\bin\Debug"; // Change this to your project's Debug directory
                string savePath = @"\\172.28.10.12\DX Center\7.ECN_Management\StartUp\Version";
                string targetDirectory = Path.Combine(savePath, txtNextVer.Text);
                try
                {
                    // Check if the folder doesn't exist, then create it
                    if (!Directory.Exists(targetDirectory))
                    {
                        Directory.CreateDirectory(targetDirectory);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error creating folder: {ex.Message}");
                }             
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings["Version"].Value = txtNextVer.Text.Trim();
                config.Save(ConfigurationSaveMode.Modified);
                Versions version = new Versions();
                version.CREATE_DATE = DateTime.Now;
                version.VERSION1 = txtNextVer.Text.Trim();
                version.REMARK = txtRemark.Text;
                _versonHelper.SaveNewVersion(version);
                // Copy files and folders recursively
                CopyFilesRecursively(sourceDirectory, targetDirectory);
                MessageBox.Show("Update Success full!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error copying files and folders: " + ex.Message);
            }
        }

        private bool IsNextversion(TextBox txtcurrentVer, TextBox txtNextVer)
        {
            int currentver = Convert.ToInt32(txtcurrentVer.Text.Replace(".",""));
            int nextver = Convert.ToInt32(txtNextVer.Text.Replace(".", ""));
            if (currentver > nextver) return false;
            return true;
        }

        private void CopyFilesRecursively(string sourceDirectory, string targetDirectory)
        {
            // Copy files
            string[] files = Directory.GetFiles(sourceDirectory);
            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file);
                string destFile = Path.Combine(targetDirectory, fileName);
                File.Copy(file, destFile, true);
            }
        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
            FormChat f = new FormChat();
            f.Show();
        }
    }
}
