namespace TikTacToe.Core;

public sealed class TicTacToeGame
{
    private static readonly int[][] WinningLines =
    [
        [0, 1, 2],
        [3, 4, 5],
        [6, 7, 8],
        [0, 3, 6],
        [1, 4, 7],
        [2, 5, 8],
        [0, 4, 8],
        [2, 4, 6]
    ];

    private readonly Player[] _board = new Player[9];

    public GameMode Mode { get; private set; }

    public Player CurrentPlayer { get; private set; } = Player.Smile;

    public Player HumanPlayer { get; } = Player.Smile;

    public Player AiPlayer { get; } = Player.Heart;

    public GameResult Result { get; private set; } = GameResult.InProgress;

    public IReadOnlyList<Player> Board => _board;

    public TicTacToeGame(GameMode mode = GameMode.LocalTwoPlayers)
    {
        Mode = mode;
    }

    public bool Play(int cellIndex)
    {
        if (cellIndex < 0 || cellIndex >= _board.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(cellIndex));
        }

        if (Result.IsFinished || _board[cellIndex] != Player.None)
        {
            return false;
        }

        _board[cellIndex] = CurrentPlayer;
        Result = Evaluate(_board);

        if (!Result.IsFinished)
        {
            CurrentPlayer = NextPlayer(CurrentPlayer);
        }

        return true;
    }

    public bool PlayAiTurn()
    {
        if (Mode != GameMode.VersusAi || CurrentPlayer != AiPlayer || Result.IsFinished)
        {
            return false;
        }

        var move = TicTacToeAi.FindBestMove(_board, AiPlayer, HumanPlayer);
        return move >= 0 && Play(move);
    }

    public void Reset(GameMode mode)
    {
        Array.Fill(_board, Player.None);
        Mode = mode;
        CurrentPlayer = Player.Smile;
        Result = GameResult.InProgress;
    }

    public static GameResult Evaluate(IReadOnlyList<Player> board)
    {
        foreach (var line in WinningLines)
        {
            var first = board[line[0]];
            if (first != Player.None && first == board[line[1]] && first == board[line[2]])
            {
                return GameResult.WonBy(first);
            }
        }

        for (var i = 0; i < board.Count; i++)
        {
            if (board[i] == Player.None)
            {
                return GameResult.InProgress;
            }
        }

        return GameResult.Draw;
    }

    public static Player NextPlayer(Player player) =>
        player == Player.Smile ? Player.Heart : Player.Smile;
}
