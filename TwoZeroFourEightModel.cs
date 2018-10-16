using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace twozerofoureight
{
    class TwoZeroFourEightModel : Model
    {
        protected int boardSize; // default is 4
        protected int[,] board;
        protected Random rand;
        protected int score = 0;
        protected bool Over;
        protected string g = "2048 GAME";



        public TwoZeroFourEightModel() : this(4)
        {

            // default board size is 4 
        }

        public string StatusText()
        {
            return g;
        }
        public string GetScore()
        {
            return (score).ToString();
        }
        public int[,] GetBoard()
        {

            return board;
        }





        public TwoZeroFourEightModel(int size)
        {
            boardSize = size;
            board = new int[boardSize, boardSize];
            var range = Enumerable.Range(0, boardSize);
            foreach (int i in range)
            {
                foreach (int j in range)
                {
                    board[i, j] = 0;
                }
            }
            rand = new Random();
            board = Random(board);
            NotifyAll();
        }

        private int[,] Random(int[,] input)
        {


            while (true)
            {

                int x = rand.Next(boardSize);
                int y = rand.Next(boardSize);

                if (board[x, y] == 0)
                {

                    board[x, y] = 2;
                    break;
                }
                else if (board[x, y] != 0)
                {
                    //check all block
                    int checkfull = 0;
                    int[,] check;
                    check = GetBoard();
                    var range = Enumerable.Range(0, boardSize);
                    foreach (int i in range)
                    {
                        foreach (int j in range)
                        {
                            if (check[i, j] != 0)
                            {

                                checkfull++;
                            }

                        }
                    }

                    //if all block full
                    if (checkfull == 16)
                    {
                        //check game over
                        var checkRANGE = Enumerable.Range(0, boardSize);
                        check = GetBoard();
                        foreach (int i in checkRANGE)
                        {
                            foreach (int j in checkRANGE)
                            {
                                try
                                {
                                    if (check[i, j] == check[i, j - 1] || check[i, j] == check[i, j + 1] || check[i, j] == check[i - 1, j] || check[i, j] == check[i + 1, j])
                                    {
                                        Over = false;
                                    }
                                    else Over = true;
                                }
                                catch (IndexOutOfRangeException)
                                {
                                    //do nothing
                                }


                            }
                        }
                        //check if over print "GAME OVER"
                        if (Over == true) g = "GAME OVER";

                        break;



                    }

                }
            }
            return input;

        }

        public void PerformDown()
        {
            int[] buffer;
            int pos;
            int[] rangeX = Enumerable.Range(0, boardSize).ToArray();
            int[] rangeY = Enumerable.Range(0, boardSize).ToArray();
            Array.Reverse(rangeY);
            foreach (int i in rangeX)
            {
                pos = 0;
                buffer = new int[4];
                foreach (int k in rangeX)
                {
                    buffer[k] = 0;
                }
                //shift left
                foreach (int j in rangeY)
                {
                    if (board[j, i] != 0)
                    {
                        buffer[pos] = board[j, i];
                        pos++;
                    }
                }
                // check duplicate
                foreach (int j in rangeX)
                {
                    if (j > 0 && buffer[j] != 0 && buffer[j] == buffer[j - 1])
                    {
                        buffer[j - 1] *= 2;
                        score += buffer[j - 1];
                        buffer[j] = 0;

                    }
                }
                // shift left again
                pos = 3;
                foreach (int j in rangeX)
                {
                    if (buffer[j] != 0)
                    {
                        board[pos, i] = buffer[j];
                        pos--;
                    }
                }
                // copy back
                for (int k = pos; k != -1; k--)
                {
                    board[k, i] = 0;
                }
            }
            board = Random(board);

            NotifyAll();
        }

        public void PerformUp()
        {
            int[] buffer;
            int pos;

            int[] range = Enumerable.Range(0, boardSize).ToArray();
            foreach (int i in range)
            {
                pos = 0;
                buffer = new int[4];
                foreach (int k in range)
                {
                    buffer[k] = 0;
                }
                //shift left
                foreach (int j in range)
                {
                    if (board[j, i] != 0)
                    {
                        buffer[pos] = board[j, i];
                        pos++;
                    }
                }
                // check duplicate
                foreach (int j in range)
                {
                    if (j > 0 && buffer[j] != 0 && buffer[j] == buffer[j - 1])
                    {
                        buffer[j - 1] *= 2;
                        score += buffer[j - 1];

                        buffer[j] = 0;
                    }

                }
                // shift left again
                pos = 0;
                foreach (int j in range)
                {
                    if (buffer[j] != 0)
                    {
                        board[pos, i] = buffer[j];
                        pos++;
                    }
                }
                // copy back
                for (int k = pos; k != boardSize; k++)
                {
                    board[k, i] = 0;
                }
            }
            board = Random(board);
            NotifyAll();
        }

        public void PerformRight()
        {
            int[] buffer;
            int pos;

            int[] rangeX = Enumerable.Range(0, boardSize).ToArray();
            int[] rangeY = Enumerable.Range(0, boardSize).ToArray();
            Array.Reverse(rangeX);
            foreach (int i in rangeY)
            {
                pos = 0;
                buffer = new int[4];
                foreach (int k in rangeY)
                {
                    buffer[k] = 0;
                }
                //shift left
                foreach (int j in rangeX)
                {
                    if (board[i, j] != 0)
                    {
                        buffer[pos] = board[i, j];
                        pos++;
                    }
                }
                // check duplicate
                foreach (int j in rangeY)
                {
                    if (j > 0 && buffer[j] != 0 && buffer[j] == buffer[j - 1])
                    {
                        buffer[j - 1] *= 2;
                        score += buffer[j - 1];

                        buffer[j] = 0;
                    }
                }
                // shift left again
                pos = 3;
                foreach (int j in rangeY)
                {
                    if (buffer[j] != 0)
                    {
                        board[i, pos] = buffer[j];
                        pos--;
                    }
                }
                // copy back
                for (int k = pos; k != -1; k--)
                {
                    board[i, k] = 0;
                }
            }
            board = Random(board);
            NotifyAll();
        }

        public void PerformLeft()
        {
            int[] buffer;
            int pos;
            int[] range = Enumerable.Range(0, boardSize).ToArray();
            foreach (int i in range)
            {
                pos = 0;
                buffer = new int[boardSize];
                foreach (int k in range)
                {
                    buffer[k] = 0;
                }
                //shift left
                foreach (int j in range)
                {
                    if (board[i, j] != 0)
                    {
                        buffer[pos] = board[i, j];
                        pos++;
                    }
                }
                // check duplicate
                foreach (int j in range)
                {
                    if (j > 0 && buffer[j] != 0 && buffer[j] == buffer[j - 1])
                    {
                        buffer[j - 1] *= 2;

                        score += buffer[j - 1];

                        buffer[j] = 0;
                    }
                }
                // shift left again
                pos = 0;
                foreach (int j in range)
                {
                    if (buffer[j] != 0)
                    {
                        board[i, pos] = buffer[j];
                        pos++;
                    }
                }
                for (int k = pos; k != boardSize; k++)
                {
                    board[i, k] = 0;
                }
            }
            board = Random(board);
            NotifyAll();
        }








    }//class
}

