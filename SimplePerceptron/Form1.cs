using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SimplePerceptron
{
    public partial class Form1 : Form
    {
        public int[,] input = new int[30, 30];
        Network NeuroNet;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            NeuroNet = new Network(30, 30, input);
            loadFile.Title = "Укажите файл весов";
            loadFile.ShowDialog();
            string s = loadFile.FileName;
            StreamReader str = File.OpenText(s);
            string line;
            string[] s1;
            int k = 0;
            while ((line = str.ReadLine()) != null)
            {

                s1 = line.Split(' ');
                for (int i = 0; i < s1.Length; i++)
                {
                    resultListBox.Items.Add("");
                    if (k < 30)
                    {
                        NeuroNet.weight[i, k] = Convert.ToInt32(s1[i]);
                        resultListBox.Items[k] += Convert.ToString(NeuroNet.weight[i, k]);
                    }

                }
                k++;

            }
            str.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            resultListBox.Items.Clear();
            button1.Enabled = true;
            loadFile.Title = "Укажите файл анализируемого изображения";
            loadFile.ShowDialog();
            pictureBox1.Image = Image.FromFile(loadFile.FileName);
            Bitmap im = pictureBox1.Image as Bitmap;
            for (var i = 0; i <= 30; i++)
                resultListBox.Items.Add(" ");
            for (var x = 0; x <= 29; x++)
            {
                for (var y = 0; y <= 29; y++)
                {
                    // listBox1.Items.Add(Convert.ToString(im.GetPixel(x, y).R));
                    int n = (im.GetPixel(x, y).R);
                    if (n >= 250)
                        n = 0;
                    else
                        n = 1;
                    resultListBox.Items[y] = resultListBox.Items[y] + "  " + Convert.ToString(n);
                    input[x, y] = n;
                    //if (n == 0) input[x, y] = 1;
                }
            }
            recognize();
        }
        public void recognize()
        {
            NeuroNet.mul_w();
            NeuroNet.Sum();
            if (NeuroNet.Res())
                resultListBox.Items.Add(" - True, Sum = " + Convert.ToString(NeuroNet.sum));
            else
                resultListBox.Items.Add(" - False, Sum = " + Convert.ToString(NeuroNet.sum));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;

            if (NeuroNet.Res() == false)
                NeuroNet.incW(input);
            else NeuroNet.decW(input);

            //Запись
            string s = "";
            string[] s1 = new string[30];
            System.IO.File.Delete("w.txt");
            FileStream FS = new FileStream("w.txt", FileMode.OpenOrCreate);
            StreamWriter SW = new StreamWriter(FS);
            
            for (int y = 0; y <= 29; y++)
            {
                for (int x = 0; x <= 29; x++)
                {
                    s += Convert.ToString(NeuroNet.weight[x, y]);                    
                }
                s1[y] = s;
                SW.WriteLine(s);
                s = "";
            }
            SW.Close();



        }
    }
}
