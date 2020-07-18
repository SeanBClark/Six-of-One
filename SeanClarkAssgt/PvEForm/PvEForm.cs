//=================================================//
// Sean Clark
// PvEForm
// Handles gameplay for human vs computer games
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
using static SeanClarkAssgt.PvPForm.PvPForm;
using System.Diagnostics.Eventing.Reader;

namespace SeanClarkAssgt.PvEForm
{
    public partial class PvEForm : Form
    {

        int playerScore = 0;
        int computerScore = 0;
        int selectedScore = 0;
        public PvEForm()
        {
            InitializeComponent();
            HideTextBox(txtBxDieChoice);
            HideButton(btnRoll);
            HideLabel(lblYouRolled);
            HideLabel(lblTempScore);
            HideRollPicBx(picBx1, picBx2, picBx3, picBx4, picBx5, picBx6);
        }

        // Function to exit current form
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();

            HomeForm homeForm = new HomeForm();

            homeForm.Show();
        }

        // Call logic methods to process game and display current scores
        // Also handles PC auto player
        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (RegexCheckerScore(txtbxWinScore) == false)
            {
                lblTitle.Text = "Please Desired Score";
                lblTitle.BackColor = Color.Red;
            }
            else
            {
                lblSetScore.Text = "Please Enter the Amount of Dice you would like to roll";
                selectedScore = int.Parse(txtbxWinScore.Text);
                HideButton(btnPlay);
                HideTextBox(txtbxWinScore);
                ShowTextBox(txtBxDieChoice);
                ShowButton(btnRoll);
                HideLabel(lblTitle);
            }
        }

        private async void btnRoll_Click(object sender, EventArgs e)
        {
            int userPick = int.Parse(txtBxDieChoice.Text);
            

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
                    HideLabel(lblTempScore);
                    resetPicBox(picBx1, picBx2, picBx3, picBx4, picBx5, picBx6);
                    ShowRollPicBx(picBx1, picBx2, picBx3, picBx4, picBx5, picBx6);

                    int[] rollResult = getRollResult(userPick, lblTitle);

                    Boolean isOne = false;
                    ShowLabel(lblYouRolled);
                    lblYouRolled.Text = "You rolled!";

                    RenderGraphics(rollResult);

                    // // Use Case Handlers for player
                    if (rollResult.Length == 1)
                    {
                        isOne = CaseOne(rollResult[0]);
                    }
                    else if (isOne == false)
                    {

                        if (CaseTwoThreeFour(rollResult) == 2)
                        {
                            playerScore = 0;
                        }
                        else if (CaseTwoThreeFour(rollResult) == 3)
                        {
                            HideButton(btnRoll);
                            ShowLabel(lblTitle);
                            txtBxDieChoice.Visible = false;
                            lblTitle.Text = "Computer Wins!";
                        }
                        else if (CaseTwoThreeFour(rollResult) == 4)
                        {
                            HideButton(btnRoll);
                            ShowLabel(lblTitle);
                            txtBxDieChoice.Visible = false;
                            lblTitle.Text = "You Win!";
                        }
                        else
                        {
                            playerScore += calScore(rollResult);
                            if (playerScore >= selectedScore)
                            {
                                HideButton(btnRoll);
                                txtBxDieChoice.Visible = false;
                                lblTitle.Text = "You Win! with a score of: " + playerScore;
                            }
                        }

                    }// END: else if (isOne == false)

                    lblPlayerScore.Text = "Your Current Score is: " + playerScore.ToString();

                    HideButton(btnRoll);
                    HideLabel(lblSetScore);
                    HideTextBox(txtBxDieChoice);
                    ShowLabel(lblTitle);
                    

                    await Task.Delay(4000);

                    lblTitle.Text = "Computer is now rolling!";
                    HideRollPicBx(picBx1, picBx2, picBx3, picBx4, picBx5, picBx6);
                    HideLabel(lblYouRolled);

                    await Task.Delay(4000);

                    HideLabel(lblTempScore);
                    HideButton(btnRoll);
                    HideLabel(lblSetScore);
                    HideLabel(lblTempScore);
                    txtBxDieChoice.Visible = false;

                    Random random = new Random();
                    int computerDieCount = random.Next(1, 6);

                    int[] computerRoll = getRollResult(computerDieCount, lblTitle);

                    await Task.Delay(5000);

                    resetPicBox(picBx1, picBx2, picBx3, picBx4, picBx5, picBx6);
                    ShowRollPicBx(picBx1, picBx2, picBx3, picBx4, picBx5, picBx6);
                    RenderGraphics(computerRoll);
                    ShowLabel(lblYouRolled);
                    lblYouRolled.Text = "Computer rolled!";

                    isOne = false;

                    // Use Case Handlers for computer
                    if (computerRoll.Length == 1)
                    {
                        isOne = CaseOne(computerRoll[0]);
                    }
                    else if (isOne == false)
                    {
                        if (CaseTwoThreeFour(computerRoll) == 2)
                        {
                            playerScore = 0;
                        }
                        else if (CaseTwoThreeFour(computerRoll) == 3)
                        {
                            HideButton(btnRoll);
                            ShowLabel(lblTitle);
                            txtBxDieChoice.Visible = false;
                            lblTitle.Text = "You Win!";
                        }
                        else if (CaseTwoThreeFour(computerRoll) >= 4)
                        {
                            HideButton(btnRoll);
                            ShowLabel(lblTitle);
                            txtBxDieChoice.Visible = false;
                            lblTitle.Text = "Computer Wins!";
                        }
                        else
                        {
                            computerScore += calScore(computerRoll);
                            if (computerScore >= selectedScore)
                            {
                                HideButton(btnRoll);
                                txtBxDieChoice.Visible = false;
                                lblTitle.Text = "Computer Wins! with a score of: " + computerScore;
                            }
                        }
                    }

                    lblComputerScore.Text = "Computer Score is: " + computerScore.ToString();

                    await Task.Delay(8000);

                    HideRollPicBx(picBx1, picBx2, picBx3, picBx4, picBx5, picBx6);

                    await Task.Delay(8000);

                    ShowRollPicBx(picBx1, picBx2, picBx3, picBx4, picBx5, picBx6);
                    ShowButton(btnRoll);
                    ShowLabel(lblTitle);
                    txtBxDieChoice.Visible = true;
                    lblTitle.Text = "You may roll again";

                }

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
                else if(array[i] == 2)
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
