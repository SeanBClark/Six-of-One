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

namespace SeanClarkAssgt.DiceGraphics
{
    class DiceGraphics
    {
        // Methods to render each die face
        static public void DieFace1(PictureBox pictureBox)
        {
            pictureBox.Image = Image.FromFile("Face1.png");
        }
        static public void DieFace2(PictureBox pictureBox)
        {
            pictureBox.Image = Image.FromFile("Face2.png");
        }
        static public void DieFace3(PictureBox pictureBox)
        {
            pictureBox.Image = Image.FromFile("Face3.png");
        }
        static public void DieFace4(PictureBox pictureBox)
        {
            pictureBox.Image = Image.FromFile("Face4.png");
        }
        static public void DieFace5(PictureBox pictureBox)
        {
            pictureBox.Image = Image.FromFile("Face5.png");
        }
        static public void DieFace6(PictureBox pictureBox)
        {
            pictureBox.Image = Image.FromFile("Face6.png");
        }
    }
}
