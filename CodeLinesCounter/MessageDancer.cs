using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CodeLinesCounter
{
    class MessageDancer
    {
        private String name = "";
        /// <summary>
        /// create a new class with name of sourcename
        /// </summary>
        /// <param name="sourceName">Name String</param>
        public MessageDancer(String sourceName)
        {
            this.name = sourceName;
        }

        public void showException(Exception e)
        {
            MessageBox.Show(this.name+"\n"+e.Source + ":" + e.Message + "\n" + e.StackTrace, "Message Dancer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public void showInformation(String information)
        {
            MessageBox.Show(information, "Message Dancer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); 
        }
    }
}
