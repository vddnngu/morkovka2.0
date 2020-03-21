using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MorkovkaAPI;

namespace ResultViever
{
    public partial class Form1 : Form
    {
        List<Button> buttons = new List<Button>();
        List<TestResult> results = new List<TestResult>();
        TestProcessing testProcessing;
        public Form1()
        {
            InitializeComponent();
            


        }
        
        public void panel(int count)
        {

            panel2.Width = count * (buttons[0].Width + 60);
            panel2.Height = buttons[0].Height + 80;
            hScrollBar1.Maximum = (panel2.Width - panel1.Width) / 2;

        }
        public void generateButton()
        {
            for (int i = 0; i < results[1].getListAnswers().Count(); i++)
            {
                Button but = new Button();
                but.Text = "Кнопка" + (i + 1);
                buttons.Add(but);
            }
        }
        public void locateButtons()
        {
            generateButton();
            panel(buttons.Count);
            for (int i = 0; i < buttons.Count; i++)
            {
                panel2.Controls.Add(buttons[i]);
                buttons[i].Location = new Point((buttons[i].Width + 60) * i, 35);
                buttons[i].Text = testProcessing.getAnswers()[results[1].getListAnswers()[i]];
                ToolTip toolTip1 = new ToolTip();
                toolTip1.SetToolTip(buttons[i], testProcessing.getCurLink().getText());

                PictureBox pb = new PictureBox();
                pb.Image = new Bitmap("C:/морковка/Pm5ZL.png");
                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                pb.Size = new Size(60, buttons[i].Height);
                pb.Location = new Point(buttons[i].Width * (i + 1) + 60 * i, 35);
                panel2.Controls.Add(pb);

                testProcessing.goNext(buttons[i].Text);
            }
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            panel2.Location = new Point(panel2.Location.X + 2 * (e.OldValue - e.NewValue), panel2.Location.Y);

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        private void chekText(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path;
            OpenFileDialog OPF = new OpenFileDialog();
            OPF.Filter = "Test files(*.res)|*.res";
            TestResultParser resultParser = new TestResultParser(OPF.FileName);
            resultParser.ParseHeader();
            resultParser.Parse();
            ResultCreator resultCreator = new ResultCreator();
            results = resultCreator.getTestResults(resultParser.resEntities);
            testProcessing= results[1].getTestProcessing();
            locateButtons();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
