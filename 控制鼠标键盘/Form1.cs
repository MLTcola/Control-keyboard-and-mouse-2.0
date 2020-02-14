using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
        //static void Main(string[] args)
        //{

        //    System.Threading.Thread.Sleep(6 * 1000);

        //    MouseFlag.MouseLeftClickEvent(10, 1000, 0);

        //}
namespace 控制鼠标键盘
{ 
    public partial class Form1 : Form
    {
        public Int32 timerFlag = 1;
        public bool mouseState = false;
        public string xPosition, yPosition;
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "40";
            textBox2.Text = "6";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
            try
            {
                timer1.Interval = 1000 / Convert.ToInt16(textBox1.Text);
            }
            catch (Exception)
            {
                MessageBox.Show(Convert.ToString(1000 / Convert.ToInt16(textBox1.Text)));               
            }
            
           
            timerFlag = 1;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            MouseFlag.MouseLeftClickEvent(Convert.ToInt16(xPosition), Convert.ToInt16(yPosition), 0);

            if (timerFlag >= Convert.ToInt16(textBox2.Text)*Convert.ToInt16(textBox1.Text))
            {
                timer1.Stop();
                timerFlag = 1;
            }
            timerFlag++;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timerFlag = 1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mouseState = true;
            Bitmap map = new Bitmap(this.Width,this.Height);
            map = GetScreenCapture();

            this.FormBorderStyle = FormBorderStyle.None;     //设置窗体为无边框样式
            this.WindowState = FormWindowState.Maximized;    //最大化窗体

            this.pictureBox1.Dock = DockStyle.Fill;
            this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            this.pictureBox1.Image = map;
        }

        private Bitmap GetScreenCapture()             //获取屏幕截屏
        {
            Rectangle tScreenRect = new Rectangle(0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Bitmap tSrcBmp = new Bitmap(tScreenRect.Width, tScreenRect.Height); // 用于屏幕原始图片保存
            Graphics gp = Graphics.FromImage(tSrcBmp);
            gp.CopyFromScreen(0, 0, 0, 0, tScreenRect.Size);
            gp.DrawImage(tSrcBmp, 0, 0, tScreenRect, GraphicsUnit.Pixel);
            return tSrcBmp;
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (mouseState == true)
            {
                Point p = Control.MousePosition;
                string X = p.X.ToString();
                string Y = p.Y.ToString();
                mouseState = false;
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Normal;
                this.pictureBox1.Dock = DockStyle.Left;
                pictureBox1.Size = new Size(1, 1);

                pictureBox1.Refresh();
                xPosition = X;
                yPosition = Y;
                label1.Text = p.ToString();

            }
        }
    }

    public class MouseFlag
    {
        enum MouseEventFlag : uint
        {
            Move = 0x0001,
            LeftDown = 0x0002,
            LeftUp = 0x0004,
            RightDown = 0x0008,
            RightUp = 0x0010,
            MiddleDown = 0x0020,
            MiddleUp = 0x0040,
            XDown = 0x0080,
            XUp = 0x0100,
            Wheel = 0x0800,
            VirtualDesk = 0x4000,
            Absolute = 0x8000
        }
        [DllImport("user32.dll")]
        static extern void mouse_event(MouseEventFlag flags, int dx, int dy, uint data, UIntPtr extraInfo);
        [DllImport("user32.dll")]
        public static extern int SetCursorPos(int x, int y);
        public static void MouseLeftClickEvent(int dx, int dy, uint data)
        {
            SetCursorPos(dx, dy);
            //System.Threading.Thread.Sleep(2 * 1000);
            mouse_event(MouseEventFlag.LeftDown, dx, dy, data, UIntPtr.Zero);
            mouse_event(MouseEventFlag.LeftUp, dx, dy, data, UIntPtr.Zero);
        }
        public static void MouseRightClickEvent(int dx, int dy, uint data)
        {
            SetCursorPos(dx, dy);
            //System.Threading.Thread.Sleep(2 * 1000);
            mouse_event(MouseEventFlag.RightDown, dx, dy, data, UIntPtr.Zero);
            mouse_event(MouseEventFlag.RightUp, dx, dy, data, UIntPtr.Zero);
        }
    }
}
