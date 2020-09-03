using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
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
using Microsoft.Win32;
using System.Media;



namespace HabbitationApp
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private int tmpans;
        private int wl;
        private String[,] words;
        private int k;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {


            RegistryKey runRegKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (runRegKey.GetValue("HabbitationApp.exe") == null)
            {
                runRegKey.SetValue("HabbitationApp.exe", Environment.CurrentDirectory + "\\" + AppDomain.CurrentDomain.FriendlyName);
            }

            string path = "../../Assets/sw.txt";
            string textValue = System.IO.File.ReadAllText(path);

            string[] tmp = textValue.Split('/');
            wl = tmp.Length;
            words = new string[wl, 2];
            for (int i = 0; i < tmp.Length; i++)
            {
                words[i, 0] = tmp[i].Split(':')[0];
                words[i, 1] = tmp[i].Split(':')[1];
            }
            k = 0;
            func();

        }

        private void func()
        {
            Random r = new Random();
            if (k < 9)
            {
                int[] words_idx = new int[4];
                for (int i = 0; i < 4; i++)
                {
                    words_idx[i] = r.Next(0, wl - i - 1);
                    for (int j = 0; j < i; j++)
                    {
                        if (words_idx[j] <= words_idx[i])
                        {
                            words_idx[i] += 1;
                        }
                    }
                }

                tmpans = r.Next(0, 4);
                Console.WriteLine(tmpans);
                answer.Text = words[words_idx[tmpans], 0];
                word1.Content = words[words_idx[0], 1];
                word2.Content = words[words_idx[1], 1];
                word3.Content = words[words_idx[2], 1];
                word4.Content = words[words_idx[3], 1];
            }
            else
            {
                Environment.Exit(0);
            }
        }

        private void sound(int ansnum)
        {
            Console.WriteLine(ansnum);
            if (tmpans == ansnum) { k++; SystemSounds.Beep.Play(); Console.WriteLine("O"); }
            else { k--; SystemSounds.Hand.Play(); Console.WriteLine("X"); }
        }

        private void word1_Click(object sender, RoutedEventArgs e)
        {
            sound(0);
            func();
        }

        private void word2_Click(object sender, RoutedEventArgs e)
        {
            sound(1);
            func();
        }

        private void word3_Click(object sender, RoutedEventArgs e)
        {
            sound(2);
            func();
        }

        private void word4_Click(object sender, RoutedEventArgs e)
        {
            sound(3);
            func();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            //MessageBox.Show("안돼");
            /*
            if (MessageBox.Show("정말로 창을 닫을까요?", "창 종료 확인", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
            */
        }

        private void Window_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            this.Activate();
        }
    }
}