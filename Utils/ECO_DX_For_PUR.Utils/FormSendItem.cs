using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECO_DX_For_PUR.Utils
{
    public partial class FormSendItem : Form
    {
        private readonly Timer timer = new Timer();
        private readonly int duration = 5000; // 5 seconds in milliseconds
        private  int startX;
        private  int startY;
        private  int endX;
        private  int endY;
        private DateTime startTime;
        public FormSendItem()
        {
            InitializeComponent();
            // Set up timer
            timer.Interval = 3; // Update every 20 milliseconds (adjust for smoothness)
            timer.Tick += Timer_Tick;
            SetUpLocation();
            // Start the movement
            startTime = DateTime.Now;
            timer.Start();
        }
        private void SetUpLocation()
        {
            startX = pic1.Location.X;
            startY = pic1.Location.Y;
            endX = pic2.Location.X;
            endY = pic2.Location.Y;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                // Calculate the elapsed time since the movement started
                TimeSpan elapsedTime = DateTime.Now - startTime;

                // Calculate the progress (a value between 0 and 1)
                double progress = (double)elapsedTime.TotalMilliseconds / duration;

                // Ensure progress is capped at 1 to prevent overshooting
                if (progress > 1)
                {
                    progress = 1;
                    startTime = DateTime.Now;
                }

                // Calculate the angle based on progress
                double angle = Math.PI * progress;

                // Calculate the x and y coordinates of the point along the arc
                int newX = (int)(startX + (endX - startX) * progress);
                int newY = (int)(startY - Math.Sin(angle) * 50); // Adjust the amplitude as needed

                // Update the PictureBox's position on the UI thread
                pictureBox1.Invoke((MethodInvoker)delegate {
                    pictureBox1.Location = new System.Drawing.Point(newX, newY);
                });               
            }
            catch (Exception)
            {
                return;
            }           
        }

        private void FormSendItem_Load(object sender, EventArgs e)
        {

        }

        private void FormSendItem_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Stop the timer and perform any necessary cleanup
            timer.Stop();
            
        }
    }
}
