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

    struct EditAnswer
    {
        public String text;
        public Link link;
        public Tuple<TextBox, Button> GUIElem;
        public EditAnswer (String _text, Link _link, Tuple<TextBox, Button> _GUIElem)
        {
            text = _text;
            link = _link;
            GUIElem = _GUIElem;
        }
    }
    public partial class EditForm : Form
    {
        bool needToRenewAnswers = false;
        bool myTypeIsQuest;
        Link currentLink;
        Context context;
        List<EditAnswer> answerList;
        TeacherGUI mainGUI;
        
        public EditForm(Context _context, TeacherGUI _mainGUI)
        {
            InitializeComponent();
            context = _context;
            mainGUI = _mainGUI;
            answerList = new List<EditAnswer>();
        }
        public void fillAnswerList()
        {
            if (!currentLink.isQuestion()) return;
            answerList = new List<EditAnswer>();
            List<String> answers = (currentLink as Question).getAnswers();

            for (int i = 0; i < answers.Count; i++)
            {
                TextBox tmp = new TextBox();
                tmp.Location = new Point(16, 97 + 26 * i);
                tmp.Size = new Size(256, 20);
                tmp.Text = answers[i];
                Button tmp2 = new Button();
                tmp2.Location = new Point(298, 97 + 26 * i);
                tmp2.Size = new Size(71, 19);
                tmp2.Text = "DELETE";
                tmp2.Click += new EventHandler(delAnswers);
                answerList.Add(new EditAnswer(answers[i], (currentLink as Question).getNext(answers[i]), new Tuple<TextBox, Button>(tmp, tmp2)));
            }
            displayAnswersList();
        }

        private void correctAnswerList()
        {
            List<EditAnswer> tmpList = new List<EditAnswer>(answerList);
            answerList = new List<EditAnswer>();
            

            for (int i = 0; i < tmpList.Count; i++)
            {
                TextBox tmp = new TextBox();
                tmp.Location = new Point(16, 97 + 26 * i);
                tmp.Size = new Size(256, 20);
                tmp.Text = tmpList[i].text;
                Button tmp2 = new Button();
                tmp2.Location = new Point(298, 97 + 26 * i);
                tmp2.Size = new Size(71, 19);
                tmp2.Text = "DELETE";
                tmp2.Click += new EventHandler(delAnswers);
                answerList.Add(new EditAnswer(tmpList[i].text, tmpList[i].link, new Tuple<TextBox, Button>(tmp, tmp2)));
            }
        }

        private void displayAnswersList()
        {
            if (answerList.Count != 0)
            {
                for (int i = 0; i < answerList.Count; i++)
                {
                    Controls.Remove(answerList[i].GUIElem.Item1);
                    Controls.Remove(answerList[i].GUIElem.Item2);
                }
            }
            correctAnswerList();
            for (int i = 0; i < answerList.Count; i++)
            {
                this.Controls.Add(answerList[i].GUIElem.Item1);
                this.Controls.Add(answerList[i].GUIElem.Item2);
            }
        }


        private void delAnswers(object sender, EventArgs e)
        {
            needToRenewAnswers = true;
            if (MessageBox.Show("Вы уверенны? Если вы удалите ответ, вернуть его не получиться. Только создать заново! ",
                "Удаление ответа", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            for (int i=0; i<answerList.Count; i++)
            {
                if (answerList[i].GUIElem.Item2 == (sender as Button))
                {
                    Controls.Remove(answerList[i].GUIElem.Item1);
                    Controls.Remove(answerList[i].GUIElem.Item2);
                    answerList.RemoveAt(i);
                }
            }
            displayAnswersList();
        }

        public void setCurrentLink(Link link)
        {
            currentLink = link;
            changeQuestTextBox.Text = currentLink.getText();
            if (myTypeIsQuest = currentLink.isQuestion() == true)
            {
                questRadioButton.Checked = true;
            }
            else
            {
                answerRadioButton.Checked = true;
            }
            fillAnswerList();
        }
        private void EditForm_Load(object sender, EventArgs e)
        {
        }

        private void answerRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (answerRadioButton.Checked == true)
            {
                myTypeIsQuest = false;
                
                if(answerList!=null)
                    for (int i = 0; i < answerList.Count(); i++)
                    {
                        this.Controls.Remove(answerList[i].GUIElem.Item1);
                        this.Controls.Remove(answerList[i].GUIElem.Item2);
                    }
                this.Controls.Remove(addAnswerButton);
            }
        }

        private void addAnswerButton_Click(object sender, EventArgs e)
        {
            if (answerList.Count == 4) {
                MessageBox.Show("Кол-во ответов на один вопрос не может превышать 4. Для увеличения кол-ва ответов купите PRO версию.");
                return;
            }
            needToRenewAnswers = true;
            int i = answerList.Count();
            TextBox tmp = new TextBox();
            tmp.Location = new Point(16, 97 + 26 * i);
            tmp.Size = new Size(256, 20);
            tmp.Text = "Ответ " + (i+1);

            Button tmp2 = new Button();
            tmp2.Location = new Point(298, 97 + 26 * i);
            tmp2.Size = new Size(71, 19);
            tmp2.Text = "DELETE";
            tmp2.Click += new EventHandler(delAnswers);

            answerList.Add(new EditAnswer("Ответ "+(i+1), new Answer("Текст\n"), new Tuple<TextBox, Button>(tmp, tmp2)));
            this.Controls.Add(tmp);
            this.Controls.Add(tmp2);

            //if (radioButton1.Checked == true)
            //{
            //    (currentLink as Answer).setText(richTextBox1.Text);
            //    Label save = new Label();
            //    save.Text = "Сохранено";
            //    save.Font = new Font("Arial", 18, FontStyle.Regular);
            //    save.Width = 200;
            //    save.Location = new Point(16, 120);
            //    this.Controls.Add(save);
            //}


        }

        private void changeQuestTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверенны?",
                "Сохранение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            string text = changeQuestTextBox.Text;
            if (!myTypeIsQuest)
            {
                if (currentLink.isQuestion())
                {
                    if (context.prevLink != null)
                        (context.prevLink as Question).removeAnswer(context.answerText);
                    currentLink = new Answer(changeQuestTextBox.Text);
                    if (context.prevLink != null)
                        (context.prevLink as Question).addAnswer(context.answerText, currentLink);
                    mainGUI.renewCurLink(currentLink);
                }
            }
            else
            {
                if (!currentLink.isQuestion())
                {
                    if (context.prevLink != null)
                        (context.prevLink as Question).removeAnswer(context.answerText);
                    currentLink = new Question(changeQuestTextBox.Text);
                    if (context.prevLink != null)
                        (context.prevLink as Question).addAnswer(context.answerText, currentLink);
                    for (int i = 0; i < answerList.Count; i++)
                    {
                        (currentLink as Question).addAnswer(answerList[i].text, answerList[i].link);
                    }
                }
            }


            if (text[text.Length-1] != '\n') text += '\n';
                currentLink.setText(text);
            if (currentLink.isQuestion() == true && needToRenewAnswers)
            {
                (currentLink as Question).getAnswers().Clear();
                (currentLink as Question).getLinks().Clear();
                (currentLink as Question).clearMap();
                for (int i = 0; i < answerList.Count; i++)
                {
                    (currentLink as Question).addAnswer(answerList[i].GUIElem.Item1.Text, answerList[i].link);
                }

            }
            mainGUI.renewCurLink(currentLink);
            Close();
        }

        private void questRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (questRadioButton.Checked == true)
            {
                myTypeIsQuest = true;
                this.Controls.Add(addAnswerButton);
                displayAnswersList();
            }
            
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверенны? Ваши изменения будут потеряны!",
                "Отмена", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            Close();
        }
    }
}
