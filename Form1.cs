using DocumentFormat.OpenXml.Office2013.PowerPoint.Roaming;
using DocumentFormat.OpenXml.Spreadsheet;
using EO.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Color = System.Drawing.Color;

namespace Labor_2_Kuprina_Duarte

{

    public partial class Form1 : Form
    {

        public static Graphics g;
        public Color color = Colors.colorResult; //Создаем переменную типа Color присваиваем ей цвет.
        public Color color1 = Colors.historyColor;//Сохранение текущего цвета перед использованием ластика
        public static List<Image> History; //Список для истории
        static bool drawing;
        int historyCounter; //Счетчик истории
        GraphicsPath currentPath;
        Point oldLocation;
        public static Pen currentPen;
        Bitmap bm = new Bitmap(653, 380);
        Bitmap bm2 = new Bitmap(653, 380);
        Stack<Bitmap> bmstack = new Stack<Bitmap>();
        Stack<Bitmap> bm2stack = new Stack<Bitmap>();
        public Form1()
        {
            InitializeComponent();
            currentPen = new Pen(System.Drawing.Color.Black);//Инициализация пера с цветом
            currentPen.Width = trackBarPen.Value; //Инициализация толщины пера
            currentPen.StartCap = currentPen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            drawing = false;//Переменная, ответственная за рисование          
            g = pictureBox1.CreateGraphics();
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            History = new List<Image>(); //Инициализация списка для истори          
        }

        private void exitAltXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutF1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Программа'Графический редактор'." + System.Environment.NewLine + "Автор: Natalja Kuprina Duarte." + System.Environment.NewLine + "      Программа 'Графический редактор' предназначена для создания новых и корректировки существующих изображений, их загрузки и сохранения.Программа позволяет рисовать спомощью мыши, задавать цвет, толщину и стиль линии." + System.Environment.NewLine + "     При нажатии левой кнопки мыши и еёперемещении отображается кривая движения указателя мыши. При нажатии правой кнопки мыши появляется стирательная резинка." + System.Environment.NewLine + "     Элементы программы: MenuStrip, ToolStrip, Panel, ColorDialog, OpenFileDialog,SaveFileDialog, PictureBox, ImageList, TrackBar, ComboBox.");
        }
        private void newCltrNToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (picDrawingSurface.Image != null)
            {

                var result = MessageBox.Show("Сохранить текущее изображение перед созданием нового рисунка ? ", "Предупреждение", MessageBoxButtons.YesNoCancel);
                switch (result)
                {
                    case DialogResult.No: g.Clear(this.pictureBox1.BackColor);
                        g.Clear(Color.White);
                        picDrawingSurface.Image = null;
                        pictureBox1.Image = null;
                        // bm2stack.Clear();
                        // bmstack.Clear();
                        pictureBox1.Refresh();
                        break;
                    case DialogResult.Yes: saveF2ToolStripMenuItem_Click(sender, e); break;
                    case DialogResult.Cancel: return;
                   
                }
                
            }
            else
            {

                pictureBox1.Refresh();
                g.Clear(this.pictureBox1.BackColor);
                picDrawingSurface.Image = null;
                pictureBox1.Image = null;
                bmstack.Clear();
                bm2stack.Clear();
                Bitmap pic = new Bitmap(650, 330);
                picDrawingSurface.Image = pic;
                pictureBox1.Image = null;
               // pictureBox1.BackColor = Color.White;
            }
        }
        public void saveF2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
             Size ScreenSize = Screen.PrimaryScreen.Bounds.Size;
             Bitmap image = new Bitmap(pictureBox1.Width-15, pictureBox1.Height-15 ); 
             using (Graphics g = Graphics.FromImage(image))
             {
                pictureBox1.BackgroundImageLayout = ImageLayout.Zoom;
                g.CopyFromScreen(180,110, 0,0, ScreenSize);//pictureBox1.Left, pictureBox1.Top
            }
            SaveFileDialog SaveDlg = new SaveFileDialog();
            SaveDlg.Filter = "JPEG Image|*.jpg|Bitmap Image|*.bmp|GIF Image|*.gif|PNGImage | *.png";
            SaveDlg.Title = "Save an Image File";
            SaveDlg.FilterIndex = 4; //По умолчанию будет выбрано последнее расширение*.png 
            SaveDlg.ShowDialog();

            if (SaveDlg.FileName != "") //Если введено не пустое имя
            {
                System.IO.FileStream fs = (System.IO.FileStream)SaveDlg.OpenFile();
                switch (SaveDlg.FilterIndex)
                {
                    case 1:

                        image.Save(fs, ImageFormat.Jpeg);
                        break;
                    case 2:
                        image.Save(fs, ImageFormat.Bmp);
                        break;
                    case 3:
                        image.Save(fs, ImageFormat.Gif);
                        break;
                    case 4:
                        pictureBox1.Image.Save(fs, ImageFormat.Png);
                        break;
                }
                fs.Close();
            }
        }
        private void openF3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog OP = new OpenFileDialog();
            OP.Filter = "JPEG Image|*.jpg|Bitmap Image|*.bmp|GIF Image|*.gif|PNG Image | *.png";
            OP.Title = "Open an Image File";
            OP.FilterIndex = 1; //По умолчанию будет выбрано первое расширение *.jpg И, когда пользователь укажет нужный путь к картинке, ее нужно будет загрузить в PictureBox:
            if (OP.ShowDialog() != DialogResult.Cancel)
            {
                pictureBox1.Load(OP.FileName);
                picDrawingSurface.AutoSize = true;
            }
            else OP.Reset();

        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            newCltrNToolStripMenuItem_Click(sender, e);
        }
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            saveF2ToolStripMenuItem_Click(sender, e);
        }
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            openF3ToolStripMenuItem_Click(sender, e);
        }

        private void picDrawingSurface_MouseDown(object sender, MouseEventArgs e)
        {
            if (picDrawingSurface.Image == null)
            {
                MessageBox.Show("Сначала создайте новый файл!");
                return;
            }
            if (e.Button == MouseButtons.Left)
            {

                if (Colors.colorResult == Color.White)//  && Colors.historyColor.IsSystemColor)
                    Colors.colorResult = Colors.historyColor;
              

                drawing = true;
                oldLocation = e.Location;
                currentPath = new GraphicsPath();
                picDrawingSurface.Cursor = Cursors.Cross;

                var copy = bm.Clone(new Rectangle(0, 0, bm.Width, bm.Height), bm.PixelFormat);
                bmstack.Push(copy);        

            }
            if (e.Button == MouseButtons.Right)
            {
                drawing = true;                
                currentPen = new Pen(Colors.colorResult, trackBar1.Value);
                Colors.colorResult = Color.White;
                oldLocation = e.Location;
                currentPath = new GraphicsPath();
            }
        }

        private void picDrawingSurface_MouseUp(object sender, MouseEventArgs e)
        {

            drawing = false;
            try
            {
                
                pictureBox1.Image = bm2;
                var g = Graphics.FromImage(bm);
                currentPen = new Pen(Colors.colorResult, trackBar1.Value);
                if (dashDotDotStyleMenu.Checked == true)
                {
                    dashDotDotToolStripMenuItem1_Click(sender, e);
                }
                if (dotStyleMenu.Checked == true)
                {
                    dotToolStripMenuItem1_Click(sender, e);
                }
                if (solidStyleMenu.Checked == true)
                {
                    solidToolStripMenuItem1_Click(sender, e);
                }
                //currentPath.AddLine(oldLocation, e.Location);
                g.DrawPath(currentPen, currentPath);
               // g.Save();
                g.Dispose();
                currentPath.Dispose();
                pictureBox1.Invalidate();
            }
            catch(ArgumentException) 
            {         
            };

        }

        public void picDrawingSurface_MouseMove(object sender, MouseEventArgs e)
        {
            label1.Text = e.X.ToString() + ", " + e.Y.ToString();
            if (drawing)
            {
                
                bm2 = new Bitmap(bm);
         
                currentPen = new Pen(Colors.colorResult, trackBar1.Value);

                if (dashDotDotStyleMenu.Checked == true)
                {
                    dashDotDotToolStripMenuItem1_Click(sender, e);
                }
                if (dotStyleMenu.Checked == true)
                {
                    dotToolStripMenuItem1_Click(sender, e);
                }
                if (solidStyleMenu.Checked == true)
                {
                    solidToolStripMenuItem1_Click(sender, e);
                }
                var g = Graphics.FromImage(bm2);
                currentPath.AddLine(oldLocation, e.Location);
                g.DrawPath(currentPen, currentPath);
                oldLocation = e.Location;
                g.Dispose();
                pictureBox1.Invalidate();
            }
        }
        public void trackBar1_Scroll(object sender, EventArgs e)
        {
            currentPen.Width = trackBarPen.Value;
        }
        private void picDrawingSurface_Click(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            currentPen.Width = trackBarPen.Value;
        }

        public void solidToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            {
                currentPen.DashStyle = DashStyle.Solid;
                solidStyleMenu.Checked = true;
                dotStyleMenu.Checked = false;
                dashDotDotStyleMenu.Checked = false;
            }
        }
        public void dotToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            currentPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            solidStyleMenu.Checked = false;
            dotStyleMenu.Checked = true;
            dashDotDotStyleMenu.Checked = false;
        }


        internal class picDrawingSurface
        {
            internal static Cursor Cursor;
            public static Bitmap Image { get; internal set; }
            public static bool AutoSize { get; internal set; }

        }

        internal class dashDotDotStyleMenu
        {
            internal static bool Checked;
        }

        internal class dotStyleMenu
        {
            internal static bool Checked;
        }
        internal class solidStyleMenu
        {
            public static bool Checked;
        }
        private void dashDotDotToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            currentPen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot;
            solidStyleMenu.Checked = false;
            dotStyleMenu.Checked = false;
            dashDotDotStyleMenu.Checked = true;
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Colors f = new Colors(this.pictureBox1.Text);
            f.ShowDialog();
        }

        public void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var copy2 = bm.Clone(new Rectangle(0, 0, bm.Width, bm.Height), bm.PixelFormat);
             bm2stack.Push(copy2);
            if (bmstack.Count > 0 && bmstack.Count != 0)
            { 
              bm = bmstack.Pop();
              pictureBox1.Image = bm;

       
   
               //  var copy2 = bm.Clone(new Rectangle(0, 0, bm.Width, bm.Height), bm.PixelFormat);
               //  bm2stack.Push(copy2);
            }
            if (bmstack.Count == 0)
              MessageBox.Show("История пуста");
            }

            private void rendoCltrShiftZToolStripMenuItem_Click(object sender, EventArgs e)
            {
            //Stack<Bitmap> bm2stack = new Stack<Bitmap>();
           // bmstack.Push(bm2stack.Pop());
            if (bm2stack.Count > 0 && bm2stack.Count != 0)
            {
                bm = bm2stack.Pop();
                pictureBox1.Image = bm;
                // bm2stack.();
                //RestoreTopbm2stackItem();   
                //RestoreTopUndoItem();
               
               // RestoreItem();
            }
                else MessageBox.Show("История пуста");
            }
       

        

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            colorToolStripMenuItem_Click(sender, e);
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}


