using PiaNO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PlotFactory
{
    public partial class PiaForm : Form
    {
        PlotterConfiguration PdfConfig = null;
        string PcfPath;

        public PiaForm(string pcfPath = null)
        {
            InitializeComponent();
            textBox2.KeyPress += TextBox2_KeyPress;//绑定组合键 ctrl+a  
            PcfPath = pcfPath;
        }

        //拖文件到图标就自动打开
        private void PiaForm_Load(object sender, EventArgs e)
        {
            if (File.Exists(PcfPath))
            {
                OpendPcf(PcfPath);
            }
        }

        // var cad2019pc3 = @"C:\Users\54076\AppData\Roaming\Autodesk\AutoCAD 2019\R23.0\chs\Plotters"; 
        // var cad2008pc3 = @"C:\Users\54076\AppData\Roaming\Autodesk\AutoCAD 2008\R17.1\chs\Plotters";

        //选择文件
        private void Button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";//防止第二次点,先清空
            textBox2.Text = "";//防止第二次点,先清空

            string dir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);//桌面路径
            OpenFileDialog ofd = new OpenFileDialog
            {
                AutoUpgradeEnabled = false,//OpenFileDialog会卡顿,用这个就好了
                Filter = "所有文件(*.*)|*.*|AutoCAD打印机(*.pc3)|*.pc3|AutoCAD打印机纸张(*.pmp)|*.pmp|AutoCAD打印样式(*.ctb)|*.ctb",
                // Multiselect = true,//允许同时选择多个文件

                InitialDirectory = dir,
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                foreach (string pcfPath in ofd.FileNames)//单选的
                {
                    PcfPath = pcfPath;
                    OpendPcf(pcfPath);
                }
            }
        }

        /// <summary>
        /// 打开路径的文件,写到txtbox2上
        /// </summary>
        /// <param name="pcfPath"></param>
        private void OpendPcf(string pcfPath)
        {
            textBox1.Text = pcfPath;
            PdfConfig = new PlotterConfiguration(pcfPath)
            {
                TruetypeAsText = true
            };
            textBox2.Text = PdfConfig.InnerData.Replace("\n", Environment.NewLine);
        }

        //取消
        private void Button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        //保存文本
        private void Button1_Click(object sender, EventArgs e)
        {
            PdfConfig.Clear();
            PdfConfig.InnerData = textBox2.Text.Replace(Environment.NewLine, "\n");

            //获取无后缀的路径
            var path = Path.GetDirectoryName(textBox1.Text);
            var str = Path.GetFileNameWithoutExtension(textBox1.Text) + ".txt";
            PdfConfig.Saves(path + "\\" + str, false); //生成
        }
        
        //保存到原文件 重新压缩到里面
        private void Button4_Click(object sender, EventArgs e)
        {
            PdfConfig.Clear();
            PdfConfig.InnerData = textBox2.Text.Replace(Environment.NewLine, "\n");
            PdfConfig.Saves(textBox1.Text); //生成
        }
        
        //移动下面的三个按钮
        private void Button2_SizeChanged(object sender, EventArgs e)
        {
            var x = BottomPanel1.Location.X / 2;
            var y = BottomPanel1.Location.Y / 2;
            BottomPanel1.Location = new Point(x, y);
        }


        private void TextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case '\u0001': //ctrl+a
                    textBox2.SelectAll();
                    break;
                default:
                    break;
            }
        }

    }
}
