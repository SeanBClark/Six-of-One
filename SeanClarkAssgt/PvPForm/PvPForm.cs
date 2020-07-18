//=================================================//
// Sean Clark
// PvPForm
// Handles gameplay and ui for player vs player games
//=================================================//

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
using static SeanClarkAssgt.DiceGraphics.DiceGraphics;
using static SeanClarkAssgt.GameLogic.GameLogic;
using static SeanClarkAssgt.GameLogic.RegexFunctions;
using static SeanClarkAssgt.FormGraphics.FormGraphics;
using System.Diagnostics.Eventing.Reader;

namespace SeanClarkAssgt.PvPForm
{
    public partial class PvPForm : Form
    {

        int currentPlayerCount = 0;
        string playerOne = "Player One";
        string playerTwo = "Player Two";
        int playerOneScore = 0;
        int playerTwoScore = 0;
        int currentPlayer = 1;
        int selectedScore = 0;

        public PvPForm()
        {
            InitializeComponent();
            HideButton(btnPlay);
            HideTextBox(txtbxWinScore);
            HideLabel(lblSetScore);
            HideLabel(lblTitle);
            txtBxDieChoice.Visible = false;
            HideButton(btnRoll);
            renderPnl.Visible = false;
            HideLabel(tempScorelbl);

        }

        // Exits current Form
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();

            HomeForm homeForm = new HomeForm();

            homeForm.Show();
        }

        // Play Button
        private void btnPvPPlay_Click(object sender, EventArgs e)
        {
            HideButton(btnPvPPlay);
            ShowButton(btnPlay);
            ShowLabel(lblTitle);
            ShowTextBox(txtbxWinScore);
            ShowLabel(lblSetScore);
        }

        // Simple Button to get the score the players what to aim for.
        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (RegexCheckerScore(txtbxWinScore) == false)
            {
                lblTitle.Text = "Please Desired Score";
            }
            else
            {
                selectedScore = int.Parse(txtbxWinScore.Text);
                HideButton(btnPlay);
                HideTextBox(txtbxWinScore);
                HideLabel(lblSetScore);
                lblTitle.Text = "First Player! Roll 1 - 6 Dice";
                txtBxDieChoice.Visible = true;
                ShowButton(btnRoll);
            }
        }        


        // Call logic methods to process game and display current scores
        private void btnRoll_Click(object sender, EventArgs e)
        {

            int userPick = int.Parse(txtBxDieChoice.Text);

            currentPlayerCount += 1;

            // Decides which player is currently playing
            if (CurrentPlayer(currentPlayerCount) == true)
                currentPlayer = 1;
            else if (CurrentPlayer(currentPlayerCount) == false)
                currentPlayer = 2;

            if (currentPlayer == 1)
                lblTitle.Text = "Next Roll is: " + playerTwo;
            else if (currentPlayer == 2)
                lblTitle.Text = "Next Roll is: " + playerOne;

            try
            {
                // Checks if the text box is empty or contains something other then 1-6
                if (txtBxDieChoice.Text == null || txtBxDieChoice.Text == "")
                {
                    lblTitle.Text = "Please Enter A Number between 1 and 6!";
                    lblTitle.BackColor = Color.Red;
                }
                else if (RegexChecker(txtBxDieChoice) == false)
                {
                    lblTitle.Text = "Please Enter A Number between 1 and 6!";
                    lblTitle.BackColor = Color.Red;
                }
                else
                {
                    int[] rollResult = getRollResult(userPick, lblTitle);
                    Boolean isOne = false;
                    resetPicBox(picBx1, picBx2, picBx3, picBx4, picBx5, picBx6);
                    RenderGraphics(rollResult);

                    // Use Case Handlers
                    if (rollResult.Length == 1)
                    {
                        isOne = CaseOne(rollResult[0]);
                    }
                    else if (isOne == false)
                    {
                        if (CaseTwoThreeFour(rollResult) == 2)
                        {
                            if (currentPlayer == 1)
                                playerOneScore = 0;
                            else if (currentPlayer == 2)
                                playerTwoScore = 0;
                        }
                        else if(CaseTwoThreeFour(rollResult) == 3)
                        {
                            HideButton(btnRoll);
                            txtBxDieChoice.Visible = false;

                            if (currentPlayer == 1)
                                lblTitle.Text = playerTwo + " Wins";
                            else if (currentPlayer == 2)
                                lblTitle.Text = playerOne + " Wins";

                        }
                        else if(CaseTwoThreeFour(rollResult) >= 4)
                        {
                            HideButton(btnRoll);
                            txtBxDieChoice.Visible = false;

                            if (currentPlayer == 1)
                                lblTitle.Text = playerOne + " Wins";
                            else if (currentPlayer == 2)
                                lblTitle.Text = playerTwo + " Wins";
                        }
                        else
                        {

                            if (currentPlayer == 1)
                            {
                                playerOneScore += calScore(rollResult);
                                if (playerOneScore >= selectedScore)
                                {
                                    HideButton(btnRoll);
                                    txtBxDieChoice.Visible = false;
                                    lblTitle.Text = playerOne + " Wins with a score of: " + playerOneScore;
                                }
                            }
                            else if (currentPlayer == 2)
                            {
                                playerTwoScore += calScore(rollResult);
                                if (playerTwoScore >= selectedScore)
                                {
                                    HideButton(btnRoll);
                                    txtBxDieChoice.Visible = false;
                                    lblTitle.Text = playerTwo + " Wins with a score of: " + playerTwoScore;
                                }
                            }
                        }
                    }
                }

                if (currentPlayer == 1)
                    lblPlayerOneScore.Text = playerOne + " score: " + playerOneScore.ToString();
                else if (currentPlayer == 2)
                    lblPlayerTwoScore.Text = playerTwo + " score: " + playerTwoScore.ToString();
            }
            catch { Console.WriteLine("{0} Exception caught.", e); }
        }

        // Method to Render Graphic Die for users to see
        public void RenderGraphics(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                String box = "picBx" + (i + 1);
                PictureBox pictureBox = (PictureBox)Controls.Find(box, false).FirstOrDefault();
                if (array[i] == 1)
                    DieFace1(pictureBox);
                else if (array[i] == 2)
                    DieFace2(pictureBox);
                else if (array[i] == 3)
                    DieFace3(pictureBox);
                else if (array[i] == 4)
                    DieFace4(pictureBox);
                else if (array[i] == 5)
                    DieFace5(pictureBox);
                else if (array[i] == 6)
                    DieFace6(pictureBox);
            }
        }
    }
}
