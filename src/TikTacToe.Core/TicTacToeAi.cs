namespace TikTacToe.Core;

public static class TicTacToeAi
{
    public static int FindBestMove(IReadOnlyList<Player> board, Player aiPlayer, Player humanPlayer)
    {
        var bestScore = int.MinValue;
        var bestMove = -1;

        for (var i = 0; i < board.Count; i++)
        {
            if (board[i] != Player.None)
            {
                continue;
            }

            var candidate = board.ToArray();
            candidate[i] = aiPlayer;
            var score = Minimax(candidate, aiPlayer, humanPlayer, isAiTurn: false);

            if (score > bestScore)
            {
                bestScore = score;
                bestMove = i;
            }
        }

        return bestMove;
    }

    private static int Minimax(Player[] board, Player aiPlayer, Player humanPlayer, bool isAiTurn)
    {
        var result = TicTacToeGame.Evaluate(board);
        if (result.IsFinished)
        {
            if (result.IsDraw)
            {
                return 0;
            }

            return result.Winner == aiPlayer ? 10 : -10;
        }

        var bestScore = isAiTurn ? int.MinValue : int.MaxValue;
        var player = isAiTurn ? aiPlayer : humanPlayer;

        for (var i = 0; i < board.Length; i++)
        {
            if (board[i] != Player.None)
            {
                continue;
            }

            board[i] = player;
            var score = Minimax(board, aiPlayer, humanPlayer, !isAiTurn);
            board[i] = Player.None;

            bestScore = isAiTurn
                ? Math.Max(bestScore, score)
                : Math.Min(bestScore, score);
        }

        return bestScore;
    }
}
