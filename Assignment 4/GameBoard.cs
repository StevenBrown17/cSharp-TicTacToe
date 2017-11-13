using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_4
{
    public class GameBoard
    {
        /// <summary>
        /// string array variable
        /// </summary>
        public string[,] board;
        /// <summary>
        /// these variable will be set true if there was a winning move, and the Form1 class can access these
        /// </summary>
        public bool v1 = false, v2 = false, v3 = false, h1 = false, h2 = false, h3 = false, d1 = false, d2 = false;
        /// <summary>
        /// variable keeps track of how many moves have been played
        /// </summary>
        public int count=0;

        /// <summary>
        /// empty constructor
        /// </summary>
        public GameBoard() {

        }


        /// <summary>
        /// checks all scenarios for a winning move
        /// </summary>
        /// <returns></returns>
        public String isWinningMove(){
            //top row scenarios
            if(board[0, 0] != "" && board[0,0] == board[0,1] && board[0,2] == "") {
                return "02"; }
            if(board[0, 2] != "" && board[0, 0] == board[0, 2] && board[0, 1] == "") {
                return "01"; }
            if (board[0, 1] != "" && board[0, 1] == board[0, 2] && board[0, 0] == "") {
                return "00"; }
            //middle row scenarios
            if (board[1, 0] != "" && board[1, 0] == board[1, 1] && board[1, 2] == "") {
                return "12"; }
            if (board[1, 2] != "" && board[1, 0] == board[1, 2] && board[1, 1] == "") {
                return "11"; }
            if (board[1, 1] != "" && board[1, 1] == board[1, 2] && board[1, 0] == "") {
                return "10"; }
            //bottom row scenarios
            if (board[2, 0] != "" && board[2, 0] == board[2, 1] && board[2, 2] == "") {
                return "22"; }
            if (board[2, 2] != "" && board[2, 0] == board[2, 2] && board[2, 1] == "") {
                return "21"; }
            if (board[2, 1] != "" && board[2, 1] == board[2, 2] && board[2, 0] == "") {
                return "20"; }

            //col1 scenarios
            if (board[0, 0] != "" && board[0, 0] == board[1, 0] && board[2, 0] == "") {
                return "20"; }
            if (board[2, 0] != "" && board[0, 0] == board[2, 0] && board[1, 0] == "") {
                return "10"; }
            if (board[1, 0] != "" && board[1, 0] == board[2, 0] && board[0, 0] == "") {
                return "00"; }
            //col2 scenarios
            if (board[0, 1] != "" && board[0, 1] == board[1, 1] && board[2, 1] == "") {
                return "21"; }
            if (board[2, 1] != "" && board[0, 1] == board[2, 1] && board[1, 1] == "") {
                return "11"; }
            if (board[1, 1] != "" && board[1, 1] == board[2, 1] && board[0, 1] == "") {
                return "01"; }
            //col3 scenarios
            if (board[0, 2] != "" && board[0, 2] == board[1, 2] && board[2, 2] == "") {
                return "22"; }
            if (board[2, 2] != "" && board[0, 2] == board[2, 2] && board[1, 2] == "") {
                return "12"; }
            if (board[1, 2] != "" && board[1, 2] == board[2, 2] && board[0, 2] == "") {
                return "02"; }

            //diag1 scenarios
            if (board[0, 0] != "" && board[0, 0] == board[1, 1] && board[2, 2] == "") {
                return "22"; }
            if (board[1, 1] != "" && board[0, 0] == board[2, 2] && board[1, 1] == "") {
                return "11"; }
            if (board[2, 2] != "" && board[2, 2] == board[1, 1] && board[0, 0] == "") {
                return "00"; }

            //diag2 scenarios
            if (board[0, 2] != "" && board[0, 2] == board[1, 1] && board[2, 0] == "") {
                return "20"; }
            if (board[2, 0] != "" && board[0, 2] == board[2, 0] && board[1, 1] == "") {
                return "11"; }
            if (board[1, 1] != "" && board[2, 0] == board[1, 1] && board[0, 2] == "") {
                return "02"; }

            return "";
        }

        /// <summary>
        /// checks scenarios, and determines what diag won, and what player won
        /// </summary>
        /// <returns></returns>
        public bool isDiagonalWin()
        {

            if (board[0, 0] == "X" && board[1, 1] == "X" && board[2, 2] == "X" || board[0, 0] == "O" && board[1, 2] == "O" && board[2, 2] == "O")
            {
                d1 = true;
                return true;
            }

            if (board[0, 2] == "X" && board[1, 1] == "X" && board[2, 0] == "X" || board[0, 2] == "O" && board[1, 1] == "O" && board[2, 0] == "O")
            {
                d2 = true;
                return true;
            }
            return false;
        }

        /// <summary>
        /// checks scenarios, and determines what col won, and what player won
        /// </summary>
        /// <returns></returns>
        public bool isVerticleWin()
        {
            if (board[0, 0] == "X" && board[1, 0] == "X" && board[2, 0] == "X" || board[0, 0] == "O" && board[1, 0] == "O" && board[2, 0] == "O")
            {
                v1 = true;
                return true;
            }

            if (board[0, 1] == "X" && board[1, 1] == "X" && board[2, 1] == "X" || board[0, 1] == "O" && board[1, 1] == "O" && board[2, 1] == "O")
            {
                v2 = true;
                return true;
            }

            if (board[0, 2] == "X" && board[1, 2] == "X" && board[2, 2]=="X" || board[0, 2] == "O" && board[1, 2] == "O" && board[2, 2] == "O")
            {
                v3=true;
                return true;
            }

            return false;
        }

        /// <summary>
        /// checks scenarios, and determines what row won, and what player won
        /// </summary>
        /// <returns></returns>
        public bool isHorizonalWin()
        {
            if (board[0, 0] == "X" && board[0, 1] == "X" && board[0, 2] =="X" || board[0, 0] == "O" && board[0, 1] == "O" && board[0, 2] == "O")
            {
                h1 = true;
                return true;
            }

            if (board[1, 0] == "X" && board[1, 1]=="X" && board[1, 2] =="X" || board[1, 0] == "O" && board[1, 1] == "O" && board[1, 2] == "O")
            {
                h2 = true;
                return true;
            }

            if (board[2, 0] == "X" && board[2, 1] == "X" && board[2, 2]== "X" || board[2, 0] == "O" && board[2, 1] == "O" && board[2, 2] == "O")
            {
                h3 = true;
                return true;
            }

            return false;
        }


        /// <summary>
        /// determines if there was a tie
        /// </summary>
        /// <returns></returns>
        public bool isTie()
        {
            if (count == 9 && isDiagonalWin() == false && isVerticleWin() == false && isHorizonalWin() == false)
                return true;
            else
                return false;
        }

        /// <summary>
        /// resets variables
        /// </summary>
        public void reset()
        {
            v1 = false; v2 = false; v3 = false; h1 = false; h2 = false; h3 = false; d1 = false; d2 = false;
        }

    }
}
