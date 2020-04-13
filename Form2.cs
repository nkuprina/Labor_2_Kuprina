using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Color = System.Drawing.Color;

namespace Labor_2_Kuprina_Duarte
{
    public partial class Colors : Form
    { 
        public string s
        {
            set { _ = Form1.currentPen.Color; }
        }
        public static Color historyColor;
        public static Color colorResult = Color.Black;
        public Colors(string data)
        {
            

            InitializeComponent();
            Scroll_Red.Tag = numeric_Red;
            Scroll_Green.Tag = numeric_Green;
            Scroll_Blue.Tag = numeric_Blue;
            numeric_Red.Tag = Scroll_Red;
            numeric_Green.Tag = Scroll_Green;
            numeric_Blue.Tag = Scroll_Blue;
            numeric_Red.Value = color.R;
            numeric_Green.Value = color.G;
            numeric_Blue.Value = color.B;

            this.data = data;
        }
        string data;




    private void Scroll_Red_ValueChanged(object sender, EventArgs e)
        {
            ScrollBar scrollBar = (ScrollBar)sender;
            NumericUpDown numericUpDown = (NumericUpDown)scrollBar.Tag;
            numericUpDown.Value = scrollBar.Value;
            UpdateColor();

        }

        private void numeric_Red_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numericUpDown = (NumericUpDown)sender;
            ScrollBar scrollBar = (ScrollBar)numericUpDown.Tag;
            scrollBar.Value = (int)numericUpDown.Value;

        }

        private void Scroll_Green_ValueChanged(object sender, EventArgs e)
        {
            ScrollBar scrollBar = (ScrollBar)sender;
            NumericUpDown numericUpDown = (NumericUpDown)scrollBar.Tag;
            numericUpDown.Value = scrollBar.Value;
            UpdateColor();
        }

        private void numeric_Green_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numericUpDown = (NumericUpDown)sender;
            ScrollBar scrollBar = (ScrollBar)numericUpDown.Tag;
            scrollBar.Value = (int)numericUpDown.Value;
        }

        private void Scroll_Blue_ValueChanged(object sender, EventArgs e)
        {
            ScrollBar scrollBar = (ScrollBar)sender;
            NumericUpDown numericUpDown = (NumericUpDown)scrollBar.Tag;
            numericUpDown.Value = scrollBar.Value;
            UpdateColor();
        }

        private void numeric_Blue_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numericUpDown = (NumericUpDown)sender;
            ScrollBar scrollBar = (ScrollBar)numericUpDown.Tag;
            scrollBar.Value = (int)numericUpDown.Value;
        }
        public void UpdateColor()
        {
            colorResult = Color.FromArgb(Scroll_Red.Value, Scroll_Green.Value, Scroll_Blue.Value);
            historyColor = Color.FromArgb(Scroll_Red.Value, Scroll_Green.Value, Scroll_Blue.Value);
            picResultColor.BackColor = colorResult;
            pictureBox1.BackColor = colorResult;
        }

        private class picResultColor
        {
            public static Color BackColor { get; internal set; }
        }

        private void buttonOther_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                Scroll_Red.Value = colorDialog.Color.R;
                Scroll_Green.Value = colorDialog.Color.G;
                Scroll_Blue.Value = colorDialog.Color.B;
                colorResult = colorDialog.Color;
                UpdateColor();
            }

        }
     
        public void Colors_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

    private void Scroll_Red_Scroll(object sender, ScrollEventArgs e)
    {

    }
}

}
    
    

