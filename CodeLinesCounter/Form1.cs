using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CodeLinesCounter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }     

        /// <summary>
        /// if dragdrop then start a thread to count lines
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_DragDrop(object sender, DragEventArgs e)
        {
            dragPath = ((System.Array)(e.Data.GetData(DataFormats.FileDrop))).GetValue(0).ToString();
            CountThread countThread = new CountThread(dragPath, strQueue);
            countThread.start();
        }

        /// <summary>
        /// choose the effect of dragenter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
      
        /// <summary>
        /// if double click the window, show About msg.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            new MessageDancer("Author").showInformation("Powered by iHeadquarters.");
        }

        private String dragPath;
        private Queue<FileSystemInfo> strQueue = new Queue<FileSystemInfo>();
    }
}
