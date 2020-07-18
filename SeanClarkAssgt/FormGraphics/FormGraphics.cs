using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace SeanClarkAssgt.FormGraphics
{
    class FormGraphics
    {

        // Small Functions to Hide and Show items
        // Used to avoid repeated code
        static public void ShowButton(Button button) { button.Visible = true; }
        static public void HideButton(Button button) { button.Visible = false; }
        static public void ShowLabel(Label label) { label.Visible = true; }
        static public void HideLabel(Label label) { label.Visible = false; }
        static public void HideTextBox(TextBox textBox) { textBox.Visible = false; }
        static public void ShowTextBox(TextBox textBox) { textBox.Visible = true; }

        static public void HideRollPicBx(PictureBox b1, PictureBox b2, PictureBox b3, PictureBox b4, PictureBox b5, PictureBox b6)
        {
            b1.Visible = false;
            b2.Visible = false;
            b3.Visible = false;
            b4.Visible = false;
            b5.Visible = false;
            b6.Visible = false;
        }
        static public void ShowRollPicBx(PictureBox b1, PictureBox b2, PictureBox b3, PictureBox b4, PictureBox b5, PictureBox b6)
        {
            b1.Visible = true;
            b2.Visible = true;
            b3.Visible = true;
            b4.Visible = true;
            b5.Visible = true;
            b6.Visible = true;
        }

        static public void resetPicBox(PictureBox b1, PictureBox b2, PictureBox b3, PictureBox b4, PictureBox b5, PictureBox b6)
        {
            b1.Image = Image.FromFile("Blank.png");
            b2.Image = Image.FromFile("Blank.png");
            b3.Image = Image.FromFile("Blank.png");
            b4.Image = Image.FromFile("Blank.png");
            b5.Image = Image.FromFile("Blank.png");
            b6.Image = Image.FromFile("Blank.png");
        }

    }
}
