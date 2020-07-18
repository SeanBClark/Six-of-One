//=================================================//
// Sean Clark
// GameLogic
// Handles all Game Logic
//=================================================//

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeanClarkAssgt.GameLogic
{
    class GameLogic
    {
        // Calculates who current player is
        static public Boolean CurrentPlayer(int count)
        {
            Boolean isPlayerOne = true;

            if (count % 2 == 0)
                isPlayerOne = false;

            return isPlayerOne;
        }

        // Handles Case one
        // If player rolls a single dice and that dice is one
        static public Boolean CaseOne(int param)
        {
            Boolean isOne = false;
            if (param == 1)
                isOne = true;

            return isOne;
        }

        // Handles cases two, three and four
        static public int CaseTwoThreeFour(int[] arrayParam)
        {
            int count = 0;

            foreach (int item in arrayParam)
                if (item == 1)
                    count++;
            return count;
        }

        // Method that calulates the game scores for each player/computer
        static public int calScore(int[] arrayParam)
        {
            int result = 0;

            foreach (int item in arrayParam)
                result += item;

            foreach (var index in arrayParam.GroupBy(x => x))
            {
                if (index.Count() == 3)
                {
                    result = result * 2;
                    Console.WriteLine("3 Times Same Number");
                }
            }
            return result;
        }

        // Gets the dice rolls for each player/computer
        static public int[] getRollResult(int param, Label label)
        {
            int[] result = new int[param];
            foreach (int item in result)
            {
                result[item] = 0;
            }

            try
            {
                // Second check for no number
                // User should never be able to trigger this if
                if (param == 0)
                {
                    label.Text = "Please Enter A Number between 1 and 6!";
                    label.BackColor = Color.Red;
                }
                else
                {
                    Random random = new Random();
                    for (int i = 0; i < result.Length; i++)
                    {
                        result[i] = random.Next(1, 6);
                    }
                }
            }
            catch (Exception e) { Console.WriteLine("{0} Exception caught.", e); }

            return result;
        }
    }
}
