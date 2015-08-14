using StoryGeneratorEngine.EngineClasses;
using StoryGeneratorEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace StoryGeneratorEngine
{
    public partial class GameForm : Form
    {
        private Game game;
        private string lastMessage;
        public GameForm()
        {
            InitializeComponent();
            string[] dataIn = LoadFromFile.LoadFileToString("LastNames.txt");
            foreach(string s in dataIn)
            {
                OutputText.AppendText(s + "\n");
            }
          //  game = linkedgame;
        }
        public void ClearText()
        {
            OutputText.Text = "";
        }
        public void AddLine(string line)
        {
            OutputText.AppendText(line + "\n");
        }
        private void enterButton_Click(object sender, EventArgs e)
        {
            if (!inputBox.Text.Equals(""))
            {
               // OutputText.AppendText(inputBox.Text + "\n"); //test
                lastMessage = inputBox.Text;
               ((Game)Program.getGameClass()).ChangeInputMessage(inputBox.Text);

            }
            inputBox.Focus();
            inputBox.Clear();
            
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //capture up arrow key
            if (keyData == Keys.Up)
            {
                changeMessageToLast();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }


        private void changeMessageToLast()
        {
            inputBox.Text = lastMessage;
            inputBox.Focus();
            inputBox.SelectionStart = inputBox.Text.Length;
        }
        private void outputText_gotFocus(object sender, EventArgs e)
        {
            inputBox.Focus();
        }

        private void OutputText_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void GameForm_Load(object sender, EventArgs e)
        {

        
        }

        public void clearMap()
        {
            p00.BackColor = Color.White;
            p01.BackColor = Color.White;
            p02.BackColor = Color.White;
            p02.BackColor = Color.White;
            p03.BackColor = Color.White;
            p04.BackColor = Color.White;
            p05.BackColor = Color.White;
            p06.BackColor = Color.White;
            p07.BackColor = Color.White;
            p08.BackColor = Color.White;
            p10.BackColor = Color.White;
            p11.BackColor = Color.White;
            p12.BackColor = Color.White;
            p13.BackColor = Color.White;
            p14.BackColor = Color.White;
            p15.BackColor = Color.White;
            p16.BackColor = Color.White;
            p17.BackColor = Color.White;
            p18.BackColor = Color.White;
            p20.BackColor = Color.White;
            p21.BackColor = Color.White;
            p22.BackColor = Color.White;
            p23.BackColor = Color.White;
            p24.BackColor = Color.White;
            p25.BackColor = Color.White;
            p26.BackColor = Color.White;
            p27.BackColor = Color.White;
            p28.BackColor = Color.White;
            p30.BackColor = Color.White;
            p31.BackColor = Color.White;
            p32.BackColor = Color.White;
            p33.BackColor = Color.White;
            p34.BackColor = Color.White;
            p35.BackColor = Color.White;
            p36.BackColor = Color.White;
            p37.BackColor = Color.White;
            p38.BackColor = Color.White;
            p40.BackColor = Color.White;
            p41.BackColor = Color.White;
            p42.BackColor = Color.White;
            p43.BackColor = Color.White;
            p44.BackColor = Color.White;
            p45.BackColor = Color.White;
            p46.BackColor = Color.White;
            p47.BackColor = Color.White;
            p48.BackColor = Color.White;
            p50.BackColor = Color.White;
            p51.BackColor = Color.White;
            p52.BackColor = Color.White;
            p53.BackColor = Color.White;
            p54.BackColor = Color.White;
            p55.BackColor = Color.White;
            p56.BackColor = Color.White;
            p57.BackColor = Color.White;
            p58.BackColor = Color.White;
            p60.BackColor = Color.White;
            p61.BackColor = Color.White;
            p62.BackColor = Color.White;
            p63.BackColor = Color.White;
            p64.BackColor = Color.White;
            p65.BackColor = Color.White;
            p66.BackColor = Color.White;
            p67.BackColor = Color.White;
            p68.BackColor = Color.White;
            p70.BackColor = Color.White;
            p71.BackColor = Color.White;
            p72.BackColor = Color.White;
            p73.BackColor = Color.White;
            p74.BackColor = Color.White;
            p75.BackColor = Color.White;
            p76.BackColor = Color.White;
            p77.BackColor = Color.White;
            p78.BackColor = Color.White;
            p80.BackColor = Color.White;
            p81.BackColor = Color.White;
            p82.BackColor = Color.White;
            p83.BackColor = Color.White;
            p84.BackColor = Color.White;
            p85.BackColor = Color.White;
            p86.BackColor = Color.White;
            p87.BackColor = Color.White;
            p88.BackColor = Color.White;
           
        }
        public void updateMap(int diffX,int diffY,string col)
        {
            Color c = System.Drawing.ColorTranslator.FromHtml(col);
            //0
            if (diffX == -4)
            {
                if (diffY == -4) p00.BackColor = c;
                if (diffY == -3) p01.BackColor = c;
                if (diffY == -2) p02.BackColor = c;
                if (diffY == -1) p03.BackColor = c;
                if (diffY == 0) p04.BackColor = c;
                if (diffY == 1) p05.BackColor = c;
                if (diffY == 2) p06.BackColor = c;
                if (diffY == 3) p07.BackColor = c;
                if (diffY == 4) p08.BackColor = c;
            }
            //1
            if (diffX == -3)
            {
                if (diffY == -4) p10.BackColor = c;
                if (diffY == -3) p11.BackColor = c;
                if (diffY == -2) p12.BackColor = c;
                if (diffY == -1) p13.BackColor = c;
                if (diffY == 0) p14.BackColor = c;
                if (diffY == 1) p15.BackColor = c;
                if (diffY == 2) p16.BackColor = c;
                if (diffY == 3) p17.BackColor = c;
                if (diffY == 4) p18.BackColor = c;
            }
            //2
            if (diffX == -2)
            {
                if (diffY == -4) p20.BackColor = c;
                if (diffY == -3) p21.BackColor = c;
                if (diffY == -2) p22.BackColor = c;
                if (diffY == -1) p23.BackColor = c;
                if (diffY == 0) p24.BackColor = c;
                if (diffY == 1) p25.BackColor = c;
                if (diffY == 2) p26.BackColor = c;
                if (diffY == 3) p27.BackColor = c;
                if (diffY == 4) p28.BackColor = c;
            }
            //3
            if (diffX == -1)
            {
                if (diffY == -4) p30.BackColor = c;
                if (diffY == -3) p31.BackColor = c;
                if (diffY == -2) p32.BackColor = c;
                if (diffY == -1) p33.BackColor = c;
                if (diffY == 0) p34.BackColor = c;
                if (diffY == 1) p35.BackColor = c;
                if (diffY == 2) p36.BackColor = c;
                if (diffY == 3) p37.BackColor = c;
                if (diffY == 4) p38.BackColor = c;
            }
            //4
            if (diffX == 0)
            {
                if (diffY == -4) p40.BackColor = c;
                if (diffY == -3) p41.BackColor = c;
                if (diffY == -2) p42.BackColor = c;
                if (diffY == -1) p43.BackColor = c;
                if (diffY == 0) p44.BackColor = c;
                if (diffY == 1) p45.BackColor = c;
                if (diffY == 2) p46.BackColor = c;
                if (diffY == 3) p47.BackColor = c;
                if (diffY == 4) p48.BackColor = c;
            }
            //5
            if (diffX == 1)
            {
                if (diffY == -4) p50.BackColor = c;
                if (diffY == -3) p51.BackColor = c;
                if (diffY == -2) p52.BackColor = c;
                if (diffY == -1) p53.BackColor = c;
                if (diffY == 0) p54.BackColor = c;
                if (diffY == 1) p55.BackColor = c;
                if (diffY == 2) p56.BackColor = c;
                if (diffY == 3) p57.BackColor = c;
                if (diffY == 4) p58.BackColor = c;
            }
            //6
            if (diffX == 2)
            {
                if (diffY == -4) p60.BackColor = c;
                if (diffY == -3) p61.BackColor = c;
                if (diffY == -2) p62.BackColor = c;
                if (diffY == -1) p63.BackColor = c;
                if (diffY == 0) p64.BackColor = c;
                if (diffY == 1) p65.BackColor = c;
                if (diffY == 2) p66.BackColor = c;
                if (diffY == 3) p67.BackColor = c;
                if (diffY == 4) p68.BackColor = c;
            }
            //6
            if (diffX == 3)
            {
                if (diffY == -4) p70.BackColor = c;
                if (diffY == -3) p71.BackColor = c;
                if (diffY == -2) p72.BackColor = c;
                if (diffY == -1) p73.BackColor = c;
                if (diffY == 0) p74.BackColor = c;
                if (diffY == 1) p75.BackColor = c;
                if (diffY == 2) p76.BackColor = c;
                if (diffY == 3) p77.BackColor = c;
                if (diffY == 4) p78.BackColor = c;
            }
            //7
            if (diffX == 4)
            {
                if (diffY == -4) p80.BackColor = c;
                if (diffY == -3) p81.BackColor = c;
                if (diffY == -2) p82.BackColor = c;
                if (diffY == -1) p83.BackColor = c;
                if (diffY == 0) p84.BackColor = c;
                if (diffY == 1) p85.BackColor = c;
                if (diffY == 2) p86.BackColor = c;
                if (diffY == 3) p87.BackColor = c;
                if (diffY == 4) p88.BackColor = c;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

    }
  
}
