using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.IO;

namespace Chatbot
{
    public partial class Form1 : Form
    {

        string botLast = "";

        public Form1()
        {
            InitializeComponent();
            botLast = stateStart();
            textBox2.Text += "Бот: " + botLast + "\r\n" + "\r\n";
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            //Console.WriteLine("Aha");
            sendMsg();
        }


        public string stateStart()
        {
            StreamReader str = new StreamReader("state.txt", Encoding.UTF8);
            string textOut = "";

            while (!str.EndOfStream)
            {
                string st = str.ReadLine();
                if (st.StartsWith("start"))
                {
                    textOut = st;
                    break;
                }
            }
            str.Close();

            textOut = textOut.Substring(textOut.IndexOf(";")+1);

            return textOut;
        }



        public string stateFind(string textIn)
        {
            StreamReader str = new StreamReader("state.txt", Encoding.UTF8);
            string textOut = "";

            while (!str.EndOfStream)
            {
                string st = str.ReadLine();
                if (st.StartsWith(textIn, StringComparison.OrdinalIgnoreCase))
                {
                    textOut = st;
                    break;
                }
            }
            str.Close();

            textOut = textOut.Substring(textOut.IndexOf(";") + 1);

            return textOut;
        }


        public void stateNew(string textInBot, string textInMan)
        {
            StreamWriter str = new StreamWriter("state.txt", true);

            str.WriteLine(textInBot + ";" + textInMan);
            
            str.Close();
        }


        public void sendMsg()
        {
            string man = "";
            string bot = "";

            man = textBox1.Text;
            textBox1.Text = "";

            bot = stateFind(man); //Поиск стейта

            if (bot == "") //Если стейт не найден
            {
                bot = stateStart(); //Использует стейт start                
                stateNew(botLast, man); //Cоздает новый стейт из последней фразы бота и новой фразы
            }

            botLast = bot; //Запоминаем последний ответ бота

            textBox2.Text += "Вы: " + man + "\r\n";
            textBox2.Text += "Бот: " + bot + "\r\n" + "\r\n";
            textBox2.SelectionStart = textBox2.Text.Length;
            textBox2.ScrollToCaret();
        }
        

        
}
}
