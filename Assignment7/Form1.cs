using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace Assignment6
{
    public partial class Form1 : Form
    {
        private HockeyPlayers[] hockeyPlayera = new HockeyPlayers[30];
        HockeyPlayers hockeyPlayer;
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        int counter = 0;
        private void button1_Click(object sender, EventArgs e)
        {

            String playersName = textBox1.Text;
            int jerseyNo = int.Parse(textBox2.Text);
            int goalsScored = int.Parse(textBox3.Text);

            if (compareNames(playersName))
            {
                if (textBox1.Text.Length != 0 && textBox2.Text.Length != 0 && textBox3.Text.Length != 0)
                {
                    hockeyPlayer = new HockeyPlayers(playersName, jerseyNo, goalsScored);
                    if (hockeyPlayer != null)
                    {

                        hockeyPlayera[counter++] = hockeyPlayer;
                    }
                    else
                    {
                        MessageBox.Show("Put in something reasonable , you sucker");
                    }
                }

            }
            else
            {
                MessageBox.Show("Name already in , you sucker");
            }


            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            richTextBox1.Clear();


            for (int player = 0; player < counter; player++)
            {
                richTextBox1.Text += String.Format("{0,10}{1,25}{2,27}\n", hockeyPlayera[player].PlayersName, hockeyPlayera[player].JerseyNo, hockeyPlayera[player].GoalsScored);
            }


        }

        private bool compareNames(String p)
        {

            for (int player = 0; player < counter; player++)
            {
                if (hockeyPlayera[player].PlayersName.ToLower() == p.ToLower())
                {
                    return false;

                }

            }
            return true;

        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
          

        }

        private void button3_Click(object sender, EventArgs e)
        {
            String playersName = textBox1.Text;
            int jerseyNo = int.Parse(textBox2.Text);
            int goalsScored = int.Parse(textBox3.Text);

            for (int player = 0; player < counter; player++)
            {
                if (hockeyPlayera[player].PlayersName.ToLower() == playersName.ToLower())
                {
                   hockeyPlayera [player] = new HockeyPlayers(playersName, jerseyNo, goalsScored);

                }

            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < counter; i++)
            {
                hockeyPlayera[i] = null;

            }
            counter = 0;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            String oFile = openFileDialog.FileName;
            BinaryFormatter fomatter = new BinaryFormatter();
            FileStream inputFile = new FileStream(oFile, FileMode.Open, FileAccess.Read);

            while (inputFile.Position < oFile.Length)
            {
                hockeyPlayer = (HockeyPlayers)fomatter.Deserialize(inputFile);
                String playersName = hockeyPlayer.PlayersName;
                int jerseyNo = hockeyPlayer.JerseyNo;
                int goalsScored = hockeyPlayer.GoalsScored;


                hockeyPlayer = new HockeyPlayers(playersName, jerseyNo, goalsScored);
                hockeyPlayera[counter++] = hockeyPlayer;
            }
            inputFile.Close();
        }
        [Serializable]
        public class HockeyPlayers
        {
            public string PlayersName { get; }
            public int JerseyNo { get; }
            public int GoalsScored { get; set; }

            public HockeyPlayers(string playersName, int jerseyNo, int goalsScored)
            {
                PlayersName = playersName;
                JerseyNo = jerseyNo;
                GoalsScored = goalsScored;
            }
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            BinaryFormatter fomatter = new BinaryFormatter();

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileStream outputFile = new FileStream(saveFileDialog.FileName, FileMode.Create);

                for (int player = 0; player < counter; player++)
                {
                    hockeyPlayer = new HockeyPlayers(hockeyPlayera[player].PlayersName, hockeyPlayera[player].JerseyNo, hockeyPlayera[player].GoalsScored);
                    fomatter.Serialize(outputFile, hockeyPlayer);
                }
            }
        }

        private void richTextBox1_DoubleClick(object sender, EventArgs e)
        {
           
        }
    }
}