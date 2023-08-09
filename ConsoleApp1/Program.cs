// See https://aka.ms/new-console-template for more information

using System.Drawing;

int SIZE = 10;
int WIN_LENGTH= 5;

Board board = new Board(SIZE, WIN_LENGTH);

board.Click(1, 0);
board.Click(0, 0);
board.Click(1, 1);
board.Click(0, 1);
board.Click(1, 2);
board.Click(0, 2);
board.Click(1, 3);
board.Click(0, 3);
board.Click(2, 0);
board.Click(0, 4);
// O WON

Board board2 = new Board(SIZE, WIN_LENGTH);

board2.Click(0, 0);
board2.Click(1, 0);
board2.Click(0, 1);
board2.Click(1, 1);
board2.Click(0, 2);
board2.Click(1, 2);
board2.Click(0, 3);
board2.Click(1, 3);
board2.Click(0, 4);
// X WON

public class Board
{
    private char[] board;
    private int size;
    private int moves;

    const char X_SYMBOL = 'X';
    const char O_SYMBOL = 'O';

    Dictionary<int, List<int[]>> winningCombinations = new Dictionary<int, List<int[]>>();

    void GenerateWinningCombinations(int size, int winLength)
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j <= size - winLength; j++)
            {
                List<int> rowCombination = new List<int>();
                List<int> colCombination = new List<int>();
                for (int k = 0; k < winLength; k++)
                {
                    rowCombination.Add(i * size + j + k);
                    colCombination.Add((j + k) * size + i);
                }
                winningCombinations.Add(winningCombinations.Count + 1, new List<int[]> { rowCombination.ToArray(), colCombination.ToArray() });

                if (i <= size - winLength)
                {
                    List<int> diagonalCombination = new List<int>();
                    List<int> antiDiagonalCombination = new List<int>();
                    for (int k = 0; k < winLength; k++)
                    {
                        diagonalCombination.Add((i + k) * size + j + k);
                        antiDiagonalCombination.Add((i + k) * size + (j + winLength - k - 1));
                    }
                    winningCombinations.Add(winningCombinations.Count + 1, new List<int[]> { diagonalCombination.ToArray(), antiDiagonalCombination.ToArray() });
                }
            }
        }
    }

    public Board(int size, int winLength)
    {
        this.size = size;
        board = new char[size * size];
        moves = 0;
        GenerateWinningCombinations(size, winLength);
    }

    public void Click(int x, int y)
    {
        if (x < 0 || x >= size || y < 0 || y >= size)
        {
            Console.WriteLine("Invalid pos");
            return;
        }

        if (board[x*size + y] != '\0')
        {
            Console.WriteLine("Is already filled up");
            return;
        }

        moves++;
        if (moves % 2 == 0)
        {
            ShowO(x, y);
            board[x * size + y] = O_SYMBOL;
        }
        else
        {
            ShowX(x, y);
            board[x * size + y] = X_SYMBOL;
        }

        //ShowBoard();

        if (CheckWin(X_SYMBOL))
        {
            WinX();
        }
        else if (CheckWin(O_SYMBOL))
        {
            WinO();
        }
        else if (moves == size * size)
        {
            Draw();
        }
    }

    bool CheckWin(char playerSymbol)
    {
        foreach (var combinationList in winningCombinations.Values)
        {
            foreach (var combination in combinationList)
            {
                if (Array.TrueForAll(combination, index => board[index] == playerSymbol))
                {
                    return true;
                }
            }
        }
        return false;
    }

    //private void ShowBoard()
    //{
    //    for (int i = 0; i < size; i++)
    //    {
    //        for (int j = 0; j < size; j++)
    //        {
    //            char symbol = board[i, j];
    //            Console.Write((symbol == '\0') ? "- " : $"{symbol} ");
    //        }
    //        Console.WriteLine();
    //    }
    //}

    private void ShowX(int x, int y)
    {
        Console.WriteLine($"X at position ({x}, {y})");
    }
    private void ShowO(int x, int y)
    {
        Console.WriteLine($"O at position ({x}, {y})");
    }
    private void WinX()
    {
        Console.WriteLine("X Won\n----------------------------");
    }
    private void WinO()
    {
        Console.WriteLine("O Won\n----------------------------");
    }
    private void Draw()
    {
        Console.WriteLine("Draw\n----------------------------");
    }
}
