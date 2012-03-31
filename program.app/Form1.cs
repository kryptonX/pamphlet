using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using launcher;
using System.IO;



namespace znpad
{
    public partial class Form1 : Form
    {
        private string NULL_STRING = string.Empty.ToString();
        private const string literal = @"ZnPad";
        private const string filter = @"Plain Text files (*.txt)|*.txt|Log files (*.log)|*.log|Encoding free text files(*.text)|*.text|All files (*.*)|*.*";
        private const string line = "Line: ";
        public string[] filearray;
        private const string linecount = "Lines: ";
        public static string findString = string.Empty;
        public short charcount = 0;
        private enum stage_t { begin, confirm, options, install, installed, };
        public Form1()
        {
            InitializeComponent();
            this.columnLabel.Renderer = new ToolStripProfessionalRenderer(new CustomProfessionalColors());
            this.helpToolStripButton.Click += (aboutToolStripMenuItem_Click);
            this.pasteToolStripButton.Click += (pasteToolStripMenuItem_Click);
            this.cutToolStripButton.Click += (cutToolStripMenuItem_Click);
            this.copyToolStripButton.Click += (copyToolStripMenuItem_Click);
            this.openToolStripButton.Click += (openToolStripMenuItem_Click);
            this.newTabBtn.Click += (newTabToolStripMenuItem_Click);
            this.contextCut.Click += (cutToolStripMenuItem_Click);
            this.contextCopy.Click += (copyToolStripMenuItem_Click);
            this.contextPaste.Click += (pasteToolStripMenuItem_Click);
            this.contextUndo.Click += (undoToolStripMenuItem_Click);
            this.contextRedo.Click += (clearUndoToolStripMenuItem_Click);
            this.contextSelectAll.Click += (selectAllToolStripMenuItem_Click);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new Form1()).Show();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = filter.ToString();
            if (op.ShowDialog() == DialogResult.OK)
            {
                if (((TextBox)tabControl.SelectedTab.Controls[0]).Text.Length > 0)
                {
                    TabPage tb = new TabPage();
                    TextBox tx = new TextBox();
                    tx.Font = MaintextBox.Font;
                    tx.ScrollBars = ScrollBars.Both;
                    tx.TextChanged += (MaintextBox_TextChanged_1);
                    tx.Dock = DockStyle.Fill;
                    tx.Multiline = true;
                    tb.Text = "untitled";
                    tb.Controls.Add(tx);
                    tabControl.Controls.Add(tb);
                    tb.BringToFront();
                    tb.Select();
                    tb.PerformLayout();
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(op.FileName))
                    {
                        tx.Text = (sr.ReadToEnd());
                        sr.Dispose();                       
                    }
                    
                }
                else
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(op.FileName))
                    {
                        ((TextBox)tabControl.SelectedTab.Controls[0]).Text = (sr.ReadToEnd());
                        tabControl.SelectedTab.Text = op.FileName;
                        sr.Dispose();                        
                    }
                }
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sv = new SaveFileDialog();
            sv.Filter = filter.ToString();
            if (sv.ShowDialog() == DialogResult.OK) // save file
            {
                tabControl.SelectedTab.Text = sv.FileName.ToString();
                if (noneToolStripMenuItem.Checked == true)
                {
                    using (System.IO.StreamWriter sw = new System.IO.StreamWriter(tabControl.SelectedTab.Text))
                    {
                        sw.Write(((TextBox)tabControl.SelectedTab.Controls[0]).Text.ToString());
                        sw.Dispose();
                    }
                }
                else if (aSCIIToolStripMenuItem.Checked == true)
                {
                    TextWriter myTextWriter = new StreamWriter(tabControl.SelectedTab.Text, false, Encoding.ASCII);
                    myTextWriter.Write(((TextBox)tabControl.SelectedTab.Controls[0]).Text.ToString());
                    myTextWriter.Dispose();
                }
                else if (urtf7strip.Checked == true)
                {
                    TextWriter myTextWriter = new StreamWriter(tabControl.SelectedTab.Text, false, Encoding.UTF7);
                    myTextWriter.Write(((TextBox)tabControl.SelectedTab.Controls[0]).Text.ToString());
                    myTextWriter.Dispose();
                }
                else if (uTF7ToolStripMenuItem.Checked == true)
                {
                    TextWriter myTextWriter = new StreamWriter(tabControl.SelectedTab.Text, false, Encoding.UTF8);
                    myTextWriter.Write(((TextBox)tabControl.SelectedTab.Controls[0]).Text.ToString());
                    myTextWriter.Dispose();
                }
                this.saveToolStripButton.Enabled = true;
                this.saveToolStripMenuItem.Enabled = true;
            }
            if (tabControl.SelectedTab.Text.IndexOf('*') != -1)
                    this.tabControl.SelectedTab.Text.Replace('*', '\0');
        }

        private void MaintextBox_TextChanged(object sender, EventArgs e)
        {
           
        }
        private void MaintextBox_Wordwrap(object sender, EventArgs e)
        {
                            
            
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {           
            if (File.Exists(tabControl.SelectedTab.Text))
            {
                if (noneToolStripMenuItem.Checked == true)
                {
                    using (System.IO.StreamWriter sw = new System.IO.StreamWriter(tabControl.SelectedTab.Text))
                    {
                        sw.Write(((TextBox)tabControl.SelectedTab.Controls[0]).Text.ToString());
                        sw.Dispose();
                    }
                }
                else if (aSCIIToolStripMenuItem.Checked == true)
                {
                    TextWriter myTextWriter = new StreamWriter(tabControl.SelectedTab.Text, false, Encoding.ASCII);
                    myTextWriter.Write(((TextBox)tabControl.SelectedTab.Controls[0]).Text.ToString());
                    myTextWriter.Dispose();
                }
                else if (urtf7strip.Checked == true)
                {
                    TextWriter myTextWriter = new StreamWriter(tabControl.SelectedTab.Text, false, Encoding.UTF7);
                    myTextWriter.Write(((TextBox)tabControl.SelectedTab.Controls[0]).Text.ToString());
                    myTextWriter.Dispose();
                }
                else if (uTF7ToolStripMenuItem.Checked == true)
                {
                    TextWriter myTextWriter = new StreamWriter(tabControl.SelectedTab.Text, false, Encoding.UTF8);
                    myTextWriter.Write(((TextBox)tabControl.SelectedTab.Controls[0]).Text.ToString());
                    myTextWriter.Dispose();
                }
            }
            else
            {
                SaveFileDialog sv = new SaveFileDialog();
                sv.Filter = filter.ToString();
                if (sv.ShowDialog() == DialogResult.OK) // save file
                {
                    tabControl.SelectedTab.Text = sv.FileName;
                    if (noneToolStripMenuItem.Checked == true)
                    {
                        using (System.IO.StreamWriter sw = new System.IO.StreamWriter(tabControl.SelectedTab.Text))
                        {
                            sw.Write(((TextBox)tabControl.SelectedTab.Controls[0]).Text.ToString());
                            sw.Dispose();
                        }
                    }
                    else if (aSCIIToolStripMenuItem.Checked == true) 
                    {
                        TextWriter myTextWriter = new StreamWriter(tabControl.SelectedTab.Text, false, Encoding.ASCII);
                        myTextWriter.Write(((TextBox)tabControl.SelectedTab.Controls[0]).Text.ToString());
                        myTextWriter.Dispose();
                    }

                    else if (urtf7strip.Checked == true)
                    {
                        TextWriter myTextWriter = new StreamWriter(tabControl.SelectedTab.Text, false, Encoding.UTF7);
                        myTextWriter.Write(((TextBox)tabControl.SelectedTab.Controls[0]).Text.ToString());
                        myTextWriter.Dispose();
                    }
                    else if (uTF7ToolStripMenuItem.Checked == true)
                    {
                        TextWriter myTextWriter = new StreamWriter(tabControl.SelectedTab.Text, false, Encoding.UTF8);
                        myTextWriter.Write(((TextBox)tabControl.SelectedTab.Controls[0]).Text.ToString());
                        myTextWriter.Dispose();
                    }
                }

            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((TextBox)tabControl.SelectedTab.Controls[0]).Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((TextBox)tabControl.SelectedTab.Controls[0]).ClearUndo();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new icons()).ShowDialog(this);
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var font = new FontDialog();
            if (font.ShowDialog() == DialogResult.OK)
            {
                ((TextBox)tabControl.SelectedTab.Controls[0]).Font = font.Font;
            }
        }

        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (wordWrapToolStripMenuItem.Checked == true)
            {
                ((TextBox)tabControl.SelectedTab.Controls[0]).WordWrap = true;
                statusStrip1.Visible = false;
            }
            else if (wordWrapToolStripMenuItem.Checked == false)
            {
                ((TextBox)tabControl.SelectedTab.Controls[0]).WordWrap = false;
                statusStrip1.Visible = true;                
            }
        }

        private void clearUndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((TextBox)tabControl.SelectedTab.Controls[0]).ClearUndo();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((TextBox)tabControl.SelectedTab.Controls[0]).Copy();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((TextBox)tabControl.SelectedTab.Controls[0]).Cut();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((TextBox)tabControl.SelectedTab.Controls[0]).Paste();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((TextBox)tabControl.SelectedTab.Controls[0]).SelectAll();
        }

        private void timeDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string date = DateTime.Now.ToString();
            ((TextBox)tabControl.SelectedTab.Controls[0]).Text += date;
        }

        private void statusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (statusBarToolStripMenuItem.Checked == false)
            {
                statusStrip1.Visible = false;
            }
            else if (statusBarToolStripMenuItem.Checked == true)
            {
                statusStrip1.Visible = true;
            }
        }

        private void aSCIIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            noneToolStripMenuItem.Checked = false;
            uTF7ToolStripMenuItem.Checked = false;
            urtf7strip.Checked = false;
        }

        private void noneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aSCIIToolStripMenuItem.Checked = false;
            uTF7ToolStripMenuItem.Checked = false;
            urtf7strip.Checked = false;
        }

        private void urtf7strip_Click(object sender, EventArgs e)
        {
            noneToolStripMenuItem.Checked = false;
            uTF7ToolStripMenuItem.Checked = false;
            aSCIIToolStripMenuItem.Checked = false;
        }

        private void uTF7ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aSCIIToolStripMenuItem.Checked = false;
            noneToolStripMenuItem.Checked = false;
            urtf7strip.Checked = false;
        }

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            (new Form1()).Show();
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var findForm = (new find());
            findForm.ShowDialog(this);
            if (findString.Length > 0 && findString != " ")
            {
                long index = ((TextBox)tabControl.SelectedTab.Controls[0]).Text.IndexOf(findString);                
                ((TextBox)tabControl.SelectedTab.Controls[0]).SelectionStart = ((int)index);
                ((TextBox)tabControl.SelectedTab.Controls[0]).Select((int)index, findString.Length);
            }
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var findForm = (new find());
            findForm.ShowDialog(this);
            if (findString.Length > 0 && findString != " ")
            {
                long index = ((TextBox)tabControl.SelectedTab.Controls[0]).Text.IndexOf(findString);
                ((TextBox)tabControl.SelectedTab.Controls[0]).SelectionStart = ((int)index);
                ((TextBox)tabControl.SelectedTab.Controls[0]).Select((int)index, findString.Length);
            }
        }

        private void findNextOfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var findForm = (new findLast());
            findForm.ShowDialog(this);
            if (findString.Length > 0 && findString != " ")
            {
                long index = ((TextBox)tabControl.SelectedTab.Controls[0]).Text.LastIndexOf(findString);
                ((TextBox)tabControl.SelectedTab.Controls[0]).SelectionStart = ((int)index);
                ((TextBox)tabControl.SelectedTab.Controls[0]).Select((int)index, findString.Length);
            }
        }

        private void toolStripMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (toolMenuToggle.Checked == true)
                toolStrip1.Visible = true;
            else if (toolMenuToggle.Checked == false)
                toolStrip1.Visible = false;

        }

        private void goToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void newTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabPage tb = new TabPage();
            TextBox tx = new TextBox();
            tx.Font = MaintextBox.Font;
            tx.ScrollBars = ScrollBars.Both;
            tx.TextChanged += (MaintextBox_TextChanged_1);
            tx.Dock = DockStyle.Fill;
            tx.Multiline = true;
            tb.Text = "untitled";
            tb.Controls.Add(tx);
            tabControl.Controls.Add(tb);
            tb.BringToFront();
            tb.Select();
            tb.PerformLayout();
        }

        private void cutToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void MaintextBox_TextChanged_1(object sender, EventArgs e)
        {
            lineLabel.Text = (line.ToString() + ((TextBox)tabControl.SelectedTab.Controls[0]).GetLineFromCharIndex(((TextBox)tabControl.SelectedTab.Controls[0]).SelectionStart));
            int col, column;
            col = (((TextBox)tabControl.SelectedTab.Controls[0]).GetLineFromCharIndex(((TextBox)tabControl.SelectedTab.Controls[0]).SelectionStart));
            column = ((TextBox)tabControl.SelectedTab.Controls[0]).GetFirstCharIndexFromLine(col);
            columnLabel.Text = ("Column: " + (((TextBox)tabControl.SelectedTab.Controls[0]).SelectionStart - column).ToString());
            lineCount.Text = (linecount.ToString() + ((TextBox)tabControl.SelectedTab.Controls[0]).Lines.Count());
            if (((TextBox)tabControl.SelectedTab.Controls[0]).Text.Length <= 80)
                charcount++;         

            else
            {
                charcount = 0;
                if (File.Exists(tabControl.SelectedTab.Text))
                {
                    if (noneToolStripMenuItem.Checked == true)
                    {
                        using (System.IO.StreamWriter sw = new System.IO.StreamWriter(tabControl.SelectedTab.Text))
                        {
                            sw.Write(((TextBox)tabControl.SelectedTab.Controls[0]).Text.ToString());
                            sw.Dispose();
                        }
                    }
                    else if (aSCIIToolStripMenuItem.Checked == true)
                    {
                        TextWriter myTextWriter = new StreamWriter(tabControl.SelectedTab.Text, false, Encoding.ASCII);
                        myTextWriter.Write(((TextBox)tabControl.SelectedTab.Controls[0]).Text.ToString());
                        myTextWriter.Dispose();
                    }
                    else if (urtf7strip.Checked == true)
                    {
                        TextWriter myTextWriter = new StreamWriter(tabControl.SelectedTab.Text, false, Encoding.UTF7);
                        myTextWriter.Write(((TextBox)tabControl.SelectedTab.Controls[0]).Text.ToString());
                        myTextWriter.Dispose();
                    }
                    else if (uTF7ToolStripMenuItem.Checked == true)
                    {
                        TextWriter myTextWriter = new StreamWriter(tabControl.SelectedTab.Text, false, Encoding.UTF8);
                        myTextWriter.Write(((TextBox)tabControl.SelectedTab.Controls[0]).Text.ToString());
                        myTextWriter.Dispose();
                    }
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void lineCount_Click(object sender, EventArgs e)
        {
            if (lineCount.BorderStyle == Border3DStyle.RaisedInner)
                lineCount.BorderStyle = Border3DStyle.SunkenInner;
            else if (lineCount.BorderStyle == Border3DStyle.SunkenInner)
                lineCount.BorderStyle = Border3DStyle.RaisedInner;
        }

        private void lineLabel_Click(object sender, EventArgs e)
        {
            if (lineLabel.BorderStyle == Border3DStyle.RaisedInner)
                lineLabel.BorderStyle = Border3DStyle.SunkenInner;
            else if (lineLabel.BorderStyle == Border3DStyle.SunkenInner)
                lineLabel.BorderStyle = Border3DStyle.RaisedInner;
        }

        private void rmTab_Click(object sender, EventArgs e)
        {
            if (this.tabControl.TabCount >= 2)   // ok
                this.tabControl.Controls.Remove(this.tabControl.SelectedTab);
        }

        private void MaintextBox_KeyDown(object sender, KeyEventArgs e)
        {
            lineCount.Text = (linecount.ToString() + ((TextBox)tabControl.SelectedTab.Controls[0]).Lines.Count());
        }
    }

    class CustomProfessionalColors : ProfessionalColorTable
    {
        public override Color ToolStripGradientBegin
        { get { return Color.Black; } }

        public override Color ToolStripGradientMiddle
        { get { return Color.Black; } }

        public override Color ToolStripGradientEnd
        { get { return Color.WhiteSmoke; } }

        public override Color MenuStripGradientBegin
        { get { return Color.Gainsboro; } }

        public override Color MenuStripGradientEnd
        { get { return Color.Gray; } }
    }

}
