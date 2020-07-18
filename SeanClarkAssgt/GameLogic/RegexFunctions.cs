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

namespace SeanClarkAssgt.GameLogic
{
    class RegexFunctions
    {

        public static Boolean RegexChecker(TextBox textBox)
        {
            if ((new Regex(@"^[1-6]+$")).IsMatch(textBox.Text))
                return true;
            else
                return false;
        }

        public static Boolean RegexCheckerScore(TextBox textBox)
        {
            if ((new Regex("^[0-9]+$")).IsMatch(textBox.Text))
                return true;
            else
                return false;
        }

    }
}
