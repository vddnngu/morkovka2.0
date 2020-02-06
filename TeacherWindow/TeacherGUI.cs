using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MorkovkaAPI
{
    public class TeacherGUI
    {
        TeacherForm form;
        TestProcessing game;
        public TeacherGUI(TeacherForm f, TestProcessing _game)
        {
            form = f;
            game = _game;
        }
        public void start()
        {
            displayLink();
        }
        public void goNext(string answer)
        {
            bool nextType = game.goNext(answer);
            displayLink();
        }
        void displayLink()
        {
            
            form.setMainText(game.getCurLinkText());
            createButtons();
            createBackButton();

        }

        private void createBackButton()
        {
            if (game.canGoBack())
            {
                Button newbtn = new Button();
                newbtn.Name = "Back";
                newbtn.Text = "Назад";
                newbtn.UseVisualStyleBackColor = true;
                newbtn.Click += new EventHandler(newBackBtn_Click);
                form.addBackButton(newbtn);
            }
        }

        public Link getCurrentLink()
        {
            return game.getCurLink();
        }
        Button createButton(string name, string text)
        {
            Button newbtn = new Button();
            newbtn.Name = name;
            newbtn.Text = text;
            newbtn.UseVisualStyleBackColor = true;
            newbtn.Click += new EventHandler(newAnswerBtn_Click);
            return newbtn;
        }
        void newAnswerBtn_Click(object sender, EventArgs e)
        {
            this.goNext((sender as Button).Text);
        }
        void newBackBtn_Click(object sender, EventArgs e)
        {
            this.goBack();
        }
        void createButtons()
        {
            if (game.curLinkIsQuestion())
            {
                var links = game.getLinks();
                var answers = game.getAnswers();
                List<Button> buts = new List<Button>();
                for (int i = 0; i < answers.Count; i++)
                {
                    buts.Add(createButton("ansBut_" + i, answers[i]));
                }
                form.addAnswerButtons(buts);
            }
            else
            {
                form.removeButtons();
            }
        }

        public Context getContext()
        {
            return game.getContext();
        }
        public void goBack()
        {
            game.goBack();
            displayLink();
        }
        public void renewCurLink(Link newCurrent)
        {
            game.renewCurrent(newCurrent);
            displayLink();
        }

        public Link getActualMainLink()
        {
            return game.getMainLink();
        }
    }
}
