using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace znpad
{
    public partial class find : Form
    {
        public find()
        {
            InitializeComponent();
        }

        private void find_Load(object sender, EventArgs e)
        {

        }
             

        private void confirm_Click(object sender, EventArgs e)
        {
            Form1.findString = stringData.Text.ToString();
            this.Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
