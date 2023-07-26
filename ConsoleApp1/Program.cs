// See https://aka.ms/new-console-template for more information

Board board = new Board(); // Draw
board.Click(0, 1); // showX: (0, 1)
board.Click(0, 1); // don't do anything
board.Click(1, 1); // showO: (1, 1)
board.Click(0, 0); // showX: (0, 0)
board.Click(0, 2); // showO: (1, 2)
board.Click(2, 0); // showX: (2, 0)
board.Click(1, 0); // showO: (1, 0)
board.Click(1, 2); // showX: (1, 2)
board.Click(2, 1); // showO: (2, 1)
board.Click(2, 2); // showX: (2, 2) draw

board = new Board();
board.Click(1, 1); // showX: (1, 1)
board.Click(2, 1); // showO: (2, 1)
board.Click(1, 0); // showX: (1, 0)
board.Click(0, 1); // showO: (0, 1)
board.Click(1, 2); // showX: (1, 2) winX

public class Board
{
    private char[,] board;
    private int size;
    private int moves;

    const char X_SYMBOL = 'X';
    const char O_SYMBOL = 'O';

    public Board()
    {
        size = 3;
        board = new char[size, size];
        moves = 0;
    }

    public void Click(int x, int y)
    {
        if (x < 0 || x >= size || y < 0 || y >= size)
        {
            Console.WriteLine("Invalid pos");
            return;
        }

        if (board[x, y] != '\0')
        {
            Console.WriteLine("Is already filled up");
            return;
        }

        moves++;
        if (moves % 2 == 0)
        {
            ShowX(x, y);
            board[x, y] = X_SYMBOL;
        }
        else
        {
            ShowO(x, y);
            board[x, y] = O_SYMBOL;
        }

        ShowBoard();

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

    private bool CheckWin(char symbol)
    {
        for (int i = 0; i < size; i++)
        {
            if (board[i, 0] == symbol && board[i, 1] == symbol && board[i, 2] == symbol)
                return true;

            if (board[0, i] == symbol && board[1, i] == symbol && board[2, i] == symbol)
                return true;
        }

        if (board[0, 0] == symbol && board[1, 1] == symbol && board[2, 2] == symbol)
            return true;

        if (board[0, 2] == symbol && board[1, 1] == symbol && board[2, 0] == symbol)
            return true;

        return false;
    }

    private void ShowBoard()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                char symbol = board[i, j];
                Console.Write((symbol == '\0') ? "- " : $"{symbol} ");
            }
            Console.WriteLine();
        }
    }

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

