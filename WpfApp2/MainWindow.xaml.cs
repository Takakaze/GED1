using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        List<data> list = new List<data>();
        List<data> pd = new List<data>();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string Mispath = "C:\\Users\\kevin\\Desktop\\kt\\misspell.txt";
            string Dictpath = "C:\\Users\\kevin\\Desktop\\kt\\dict.txt";
            string Corrpath = "C:\\Users\\kevin\\Desktop\\kt\\correct.txt";

            Data(Mispath);
            Data(Dictpath);
            Data(Corrpath);

            data Misspell = list[0];
            data Dictionary = list[1];
            data Correct = list[2];
            GED ged = new GED();

            string[] temp = new string[Dictionary.strs.Length];
            double[] distances = new double[Dictionary.strs.Length];
            int num = 0;
            int[] No = new int[Dictionary.strs.Length+1];

            for (int i = 0; i < Misspell.strs.Length; i++)
            {
                temp = new string[Dictionary.strs.Length];
                for (int j = 0; j < Dictionary.strs.Length; j++)
                {
                    distances[j] = ged.getStringDistance(Misspell.strs[i], Dictionary.strs[j]);
                }
                getMin(distances,ref No);
                for (int j = 0; j < No.Length && No[j] != -1; j++)
                {
                    temp[j] = Dictionary.strs[No[j]];
                }
                pd.Add(new data { strs = temp });
            }

            num = 0;

            for (int i = 0; i < Correct.strs.Length; i++)
            {
                if (Correct.strs[i] != pd[i].strs[0])
                {
                    num++;
                }
            }
            double Accuracy = 1 - num / Correct.strs.Length;

            num = 0;
            int count = 0;

            for (int i = 0; i < pd.Count; i++)
            {
                for (int j = 0; j < pd[i].strs.Length; j++)
                {
                    if (Correct.strs[i] != pd[i].strs[j])
                    {
                        num++;
                    }
                    count++;
                }
            }
            double Precision = 1 - num / count;

            num = 0;
            for (int i = 0; i < pd.Count; i++)
            {
                for (int j = 0; j < pd[i].strs.Length; j++)
                {
                    if (Correct.strs[i] == pd[i].strs[j])
                    {
                        num++;
                    }
                }
            }
            double Recall = 1 - num / Correct.strs.Length;

            Result.Text += ("Accuracy:" + Accuracy + "\n");
            Result.Text += ("Precision:" + Precision + "\n");
            Result.Text += ("Recall:" + Recall + "\n");
        }

        private static int[] getMin(double[] dist,ref int[] num)
        {
            double min = dist[0];
            int no = 0;
            for (int i = 0; i < dist.Length; i++,no++)
            {
                if (dist[i] < min)
                {
                    min = dist[i];
                    no = 0;
                    num[no] = i;
                }
                else if (dist[i] == min)
                {
                    num[no] = i;
                }
            }
            num[no] = -1;
            return num;
        }

        void Data(string path)
        {
            FileStream filename = new FileStream(path, FileMode.Open);
            StreamReader RD = new StreamReader(filename, Encoding.Default);
            string dataline = RD.ReadToEnd();
            dataline = dataline.Replace("\r", "");
            dataline = dataline.Substring(0, dataline.Length - 1);
            string[] DL = dataline.Split('\n');
            list.Add(new data { strs = DL });
            RD.Close();
        }
    }
}


