using System;

namespace mineSweeper
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press 5 Start to a new game! ");
            string num = Console.ReadLine();
            if (num == "5")
            {
                Console.WriteLine("Enter the Horizontal Limit: ");
                string Hl = Console.ReadLine();
                Console.WriteLine("Enter the Vertical Limit: ");
                string Vl = Console.ReadLine();
                GameBoard gameBoard = new GameBoard(Convert.ToInt32(Vl), Convert.ToInt32(Hl));
                gameBoard.PrintBoard();
                gameBoard.NextMove();
            }

        }
    }
}
