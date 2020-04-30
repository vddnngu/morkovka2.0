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

namespace TeacherWindow
{
    public partial class ResultViever : Form
    {
        List<Button> buttons = new List<Button>();
        TestResult results = new TestResult();
        TestProcessing testProcessing;
        public ResultViever(TestProcessing _testProcessing)
        {
            InitializeComponent();
            testProcessing = _testProcessing;
        }
        
        public void panel(int count)
        {

            panel2.Width = count * (buttons[0].Width + 60);
            panel2.Height = buttons[0].Height + 80;
            hScrollBar1.Maximum = (panel2.Width - panel1.Width) / 2;

        }
        public void generateButton()
        {
            for (int i = 0; i < results.GetAttempts()[0].getListAnswers().Count(); i++)
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
                buttons[i].Text = testProcessing.getAnswers()[results.GetAttempts()[0].getListAnswers()[i]];
                ToolTip toolTip1 = new ToolTip();
                toolTip1.SetToolTip(buttons[i], testProcessing.getCurLink().getText());
                if (i != buttons.Count-1)
                {
                    PictureBox pb = new PictureBox();
                    pb.Image = new Bitmap("C:/морковка/Pm5ZL.png");
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    pb.Size = new Size(60, buttons[i].Height);
                    pb.Location = new Point(buttons[i].Width * (i + 1) + 60 * i, 35);
                    panel2.Controls.Add(pb);
                }

                testProcessing.goNext(buttons[i].Text);
            }
            Label label = new Label();
            label.Text = testProcessing.getCurLinkText();
            label.Font = new Font("Arial", 20, FontStyle.Regular);
            label.AutoSize = true;
            label.Location = new Point(panel1.Width/2-label.Width/2, panel1.Height-100);
            panel1.Controls.Add(label);
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
            if (OPF.ShowDialog() == DialogResult.OK)
            {
                
                TestResultParser resultParser = new TestResultParser(OPF.FileName);
                resultParser.ParseHeader();
                resultParser.Parse();
                ResultCreator resultCreator = new ResultCreator();
                results = testProcessing.getTestResult();        
                locateButtons();
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
