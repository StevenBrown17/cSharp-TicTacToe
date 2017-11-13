using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment_4
{
    public partial class Form1 : Form
    {

        /// <summary>
        /// gameBoard object used throughout the application
        /// </summary>
        GameBoard gameBoard;
        /// <summary>
        /// isGameStarted checks to see if the player has clicked the start button. when the player changes to VS Computer, variable is reset
        /// </summary>
        bool isGameStarted;
        /// <summary>
        /// keeps track of whose turn it is
        /// </summary>
        int whoseTurn=0;//0 = player1 1 = player2
        /// <summary>
        /// variable to see if the computer mode is on
        /// </summary>
        bool compOn = false;
        //variables to keep track of wins, and ties
        int p1Count, p2Count, tieCount;


        /// <summary>
        /// upon runtime, InitializeComponent(), set label, create new instance of gameBoard, 
        /// reset all variables, and ensure 2 player mode is enabled.
        /// </summary>
        public Form1()
        {
            
            InitializeComponent();

            lblNotice.BackColor = Color.Transparent;

            gameBoard = new GameBoard();
            resetGame();
            rdoPlayer.Checked = true;

        }

        /// <summary>
        /// creates a new string array for the gameBoard object, clears all label text and their backColor, 
        /// sets player 1 to play next, and resets number of moves played
        /// 
        /// </summary>
        public void resetGame(){
            gameBoard.board = new String[3, 3] { { "", "", "" }, { "", "", "" }, { "", "", "" } };
            lbl00i.Text = "";
            lbl01i.Text = "";
            lbl02i.Text = "";
            lbl10i.Text = "";
            lbl11i.Text = "";
            lbl12i.Text = "";
            lbl20i.Text = "";
            lbl21i.Text = "";
            lbl22i.Text = "";

            lbl00i.BackColor = Color.White;
            lbl01i.BackColor = Color.White;
            lbl02i.BackColor = Color.White;
            lbl10i.BackColor = Color.White;
            lbl11i.BackColor = Color.White;
            lbl12i.BackColor = Color.White;
            lbl20i.BackColor = Color.White;
            lbl21i.BackColor = Color.White;
            lbl22i.BackColor = Color.White;
            whoseTurn = 0;
            gameBoard.count = 0;

            lblNotice.Visible = true;

        }

        /// <summary>
        /// pass in whoseTurn, and this method passes back what character will be placed in the variable
        /// </summary>
        /// <param name="whoseTurn"></param>
        /// <returns></returns>
        public string value(int whoseTurn){
            if(whoseTurn == 0) { return "X"; }
            else { return "O"; }
        }
        
        /// <summary>
        /// switches the player
        /// </summary>
        public void changePlayer(){
            if (whoseTurn == 0) { whoseTurn = 1; }
            else whoseTurn = 0;
        }

        /// <summary>
        /// This method contains the click event for the labels. First captures what label is clicked, then checks to see if the
        /// label has been clicked yet, then the logic to see if the click was a winning move or not.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spaceClick(object sender, EventArgs e){

            if (isGameStarted == true){//if game started continue
                String turn = value(whoseTurn);
                Label myLabel = (Label)sender;
                if (myLabel.Text == ""){
                    myLabel.Text = turn;
                    if (turn == "X") { myLabel.BackColor = Color.Aqua; }
                    else myLabel.BackColor = Color.DarkSeaGreen;
                    var temp = myLabel.Tag.ToString();
                    int i = int.Parse(temp[0]+"");
                    int k = int.Parse(temp[1]+"");
                    gameBoard.board[i,k] = turn;
                    i = 0;k = 0;

                    gameBoard.count++;

                    if (gameBoard.isDiagonalWin()) {
                        lblStatus.Text = "Player " + (whoseTurn + 1)+" WINS!"; setWinningLabels();
                        if (whoseTurn == 0) { p1Count++; lblP1Wins.Text = p1Count + ""; }
                        else { p2Count++; lblP2Wins.Text = p2Count + ""; }
                        isGameStarted = false; }
                    if (gameBoard.isVerticleWin()) {
                        lblStatus.Text = "Player " + (whoseTurn + 1) + " WINS!"; setWinningLabels();
                        if (whoseTurn == 0) { p1Count++; lblP1Wins.Text = p1Count + ""; }
                        else { p2Count++; lblP2Wins.Text = p2Count + ""; }
                        isGameStarted = false; }
                    if (gameBoard.isHorizonalWin()) {
                        lblStatus.Text = "Player " + (whoseTurn + 1) + " WINS!"; setWinningLabels();
                        if (whoseTurn == 0) { p1Count++; lblP1Wins.Text = p1Count + ""; }
                        else { p2Count++; lblP2Wins.Text = p2Count + ""; }
                        isGameStarted = false; }
                    if (gameBoard.isTie()) {
                        lblStatus.Text = "Its a TIE!"; tieCount++; lblTies.Text = tieCount + ""; isGameStarted = false; }

                    
                    changePlayer();
                }

                if (compOn == true && isGameStarted == true)
                {
                    runCompTurn();
                }

                printBoard();

            }

        }//end spaceClick();


                
        /// <summary>
        /// upon button click, remove notice label, call reset(), and reset gameBoard variables.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            isGameStarted = true;
            resetGame();
            lblNotice.Visible = false;
            gameBoard.reset();
            lblStatus.Text = "";
        }

        /// <summary>
        /// this method contains the logic for the radio buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void opponentSelect(object sender, EventArgs e){
            if (rdoPlayer.Checked) { compOn = false; }
            else compOn = true;

            p1Count = 0;
            p2Count = 0;
            lblP1Wins.Text = "0";
            lblP2Wins.Text = "0";
            lblTies.Text = "0";
            lblStatus.Text = "";

            resetGame();
        }

        /// <summary>
        /// runCompTurn() contains all logic for the computer to make a move
        /// </summary>
        public void runCompTurn(){
            int i=0, k=0;
            String turn = value(whoseTurn);
            var temp = gameBoard.isWinningMove(); //check to see if there is a winning move
            if (temp != "") {//if there is a winning move, put set indexes
                i = int.Parse(temp[0] + "");
                k = int.Parse(temp[1] + "");
            }

            if(gameBoard.board[i,k] == "") {//if the label is empty, place the move
                gameBoard.board[i, k] = turn;
                setLabel(i, k);
                i = 0; k = 0;
            }
            else{
                for(int ii=0; ii<3; ii++){//if there is no winning move, place move in next empty spot.
                    for(int j=0; j < 3; j++){
                        if (gameBoard.board[ii, j] == ""){
                            setLabel(ii, j);
                            gameBoard.board[ii, j] = turn;
                            j = 3;//exit conditions
                            ii = 3;
                        }//if
                    }//in for
                }//out for
            }//else


            //check winning conditions.
            gameBoard.count++;
            if (gameBoard.isDiagonalWin()) { lblStatus.Text = "COMPUTER WINS!"; p2Count++; lblP2Wins.Text = p2Count + ""; isGameStarted = false; setWinningLabels(); }
            if (gameBoard.isVerticleWin()) { lblStatus.Text = "COMPUTER WINS!"; p2Count++; lblP2Wins.Text = p2Count + ""; isGameStarted = false; setWinningLabels(); }
            if (gameBoard.isHorizonalWin()) { lblStatus.Text = "COMPUTER WINS!"; p2Count++; lblP2Wins.Text = p2Count + ""; isGameStarted = false; setWinningLabels(); }
            if (gameBoard.isTie()) { lblStatus.Text = "Its a TIE!"; tieCount++; lblTies.Text = tieCount + ""; isGameStarted = false; }

            changePlayer();
            
        }

        /// <summary>
        /// set the labels in the indexed passed in
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        public void setLabel(int i, int j)
        {
            if (i == 0 && j == 0)
            {
                lbl00i.Text = "O";
                lbl00i.BackColor = Color.DarkSeaGreen;
            }
            else if (i == 0 && j == 1)
            {
                lbl01i.Text = "O";
                lbl01i.BackColor = Color.DarkSeaGreen;
            }
            else if (i == 0 && j == 2)
            {
                lbl02i.Text = "O";
                lbl02i.BackColor = Color.DarkSeaGreen;
            }
            else if (i == 1 && j == 0)
            {
                lbl10i.Text = "O";
                lbl10i.BackColor = Color.DarkSeaGreen;
            }
            else if (i == 1 && j == 1)
            {
                lbl11i.Text = "O";
                lbl11i.BackColor = Color.DarkSeaGreen;
            }
            else if (i == 1 && j == 2)
            {
                lbl12i.Text = "O";
                lbl12i.BackColor = Color.DarkSeaGreen;
            }
            else if (i == 2 && j == 0)
            {
                lbl20i.Text = "O";
                lbl20i.BackColor = Color.DarkSeaGreen;
            }
            else if (i == 2 && j == 1)
            {
                lbl21i.Text = "O";
                lbl21i.BackColor = Color.DarkSeaGreen;
            }
            else if (i == 2 && j == 2)
            {
                lbl22i.Text = "O";
                lbl22i.BackColor = Color.DarkSeaGreen;
            }
        }

        /// <summary>
        /// for debugging purposes, prints the current board to the console
        /// </summary>
        public void printBoard(){

            Console.WriteLine(gameBoard.count+"\n");
            for (int i = 0; i < 3; i++){
                Console.Write("|\t");
                for (int j = 0; j < 3; j++){
                    System.Console.Write(gameBoard.board[i, j]+"\t");       
                }
                System.Console.WriteLine("|");
            }
            

        }//end printBoard()

        /// <summary>
        /// checks the winning move, highlights the winning moves
        /// </summary>
        public void setWinningLabels(){
            if (gameBoard.d1) { lbl00i.BackColor = Color.Gold; lbl11i.BackColor = Color.Gold; lbl22i.BackColor = Color.Gold; }
            if (gameBoard.d2) { lbl02i.BackColor = Color.Gold; lbl11i.BackColor = Color.Gold; lbl20i.BackColor = Color.Gold; }
            if (gameBoard.v1) { lbl00i.BackColor = Color.Gold; lbl10i.BackColor = Color.Gold; lbl20i.BackColor = Color.Gold; }
            if (gameBoard.v2) { lbl01i.BackColor = Color.Gold; lbl11i.BackColor = Color.Gold; lbl21i.BackColor = Color.Gold; }
            if (gameBoard.v3) { lbl02i.BackColor = Color.Gold; lbl12i.BackColor = Color.Gold; lbl22i.BackColor = Color.Gold; }
            if (gameBoard.h1) { lbl00i.BackColor = Color.Gold; lbl01i.BackColor = Color.Gold; lbl02i.BackColor = Color.Gold; }
            if (gameBoard.h2) { lbl10i.BackColor = Color.Gold; lbl11i.BackColor = Color.Gold; lbl12i.BackColor = Color.Gold; }
            if (gameBoard.h3) { lbl20i.BackColor = Color.Gold; lbl21i.BackColor = Color.Gold; lbl22i.BackColor = Color.Gold; }

        }


    }//end class
}//end namespace
