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
    public partial class EditForm : Form
    {
        Link currentLink;
        List<Tuple<TextBox, Button>> answerList;
        
        public EditForm()
        {
            InitializeComponent();
            
            
        }
        public void fillAnswerList()
        {

            List<String> answers = (currentLink as Question).getAnswers();
           answerList = new List<Tuple<TextBox, Button>>();

            for(int i=0; i<answers.Count; i++)
            {
                TextBox tmp = new TextBox();
                tmp.Location = new Point(16, 97 + 26 * i);
                tmp.Size = new Size(256, 20);
                tmp.Text = answers[i];
                Button tmp2 = new Button();
                tmp2.Location = new Point(298, 97 + 26 * i);
                tmp2.Size = new Size(71,19);
                tmp2.Text = "DELETE";
                tmp2.Click += new EventHandler(delAnswers);
                answerList.Add(new Tuple<TextBox, Button>(tmp, tmp2));
                this.Controls.Add(tmp);
                this.Controls.Add(tmp2);
            }
        }
        private void delAnswers(object sender, EventArgs e)
        {
            for (int i=0; i<answerList.Count; i++)
            {
                if (answerList[i].Item2 == (sender as Button))
                {
                    (currentLink as Question).removeAnswer(answerList[i].Item1.Text);
                    for (int j = 0; j < answerList.Count(); j++)
                    {

                        this.Controls.Remove(answerList[j].Item1);
                        this.Controls.Remove(answerList[j].Item2);
                    }
                }
            }
            fillAnswerList();
        }

        public void setCurrentLink(Link link)
        {
            currentLink = link;
            if (currentLink.isQuestion() == true)
            {
                radioButton2.Checked = true;
            }
            else
            {
                radioButton1.Checked = true;
            }
        }
        private void EditForm_Load(object sender, EventArgs e)
        {
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                //for (int i = 0; i < answerList.Count(); i++)
                //{

                //    this.Controls.Remove(answerList[i].Item1);
                //    this.Controls.Remove(answerList[i].Item2);
                //}
                //this.Controls.Remove(button1);
                richTextBox1.Text=(currentLink as Answer).getText();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                int i = answerList.Count();
                TextBox tmp = new TextBox();
                tmp.Location = new Point(16, 97 + 26 * i);
                tmp.Size = new Size(256, 20);

                Button tmp2 = new Button();
                tmp2.Location = new Point(298, 97 + 26 * i);
                tmp2.Size = new Size(71, 19);
                tmp2.Text = "DELETE";
                tmp2.Click += new EventHandler(delAnswers);

                answerList.Add(new Tuple<TextBox, Button>(tmp, tmp2));
                (currentLink as Question).addAnswer(richTextBox1.Text, new Link());
                this.Controls.Add(tmp);
                this.Controls.Add(tmp2);
            }
            if (radioButton1.Checked == true)
            {
                (currentLink as Answer).setText(richTextBox1.Text);
                Label save = new Label();
                save.Text = "Сохранено";
                save.Font = new Font("Arial", 18, FontStyle.Regular);
                save.Width = 200;
                save.Location = new Point(16, 120);
                this.Controls.Add(save);
            }


        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            currentLink.setText(richTextBox1.Text);
            if (currentLink.isQuestion() == true)
            {
                (currentLink as Question).setText(richTextBox1.Text);
                List<String> answers = (currentLink as Question).getAnswers();
                for (int i=0; i<answers.Count;i++)
                {
                    answers[i] = answerList[i].Item1.Text;
                }

            }
              
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                richTextBox1.Text = (currentLink as Question).getText();
                fillAnswerList();
                this.Controls.Add(button1);
            }
            
        }
    }
}
