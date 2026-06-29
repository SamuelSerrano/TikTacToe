using TikTacToe.Core;

var tests = new (string Name, Action Run)[]
{
    ("Detecta ganador horizontal", DetectsHorizontalWinner),
    ("Detecta empate", DetectsDraw),
    ("Bloquea casillas ocupadas", BlocksOccupiedCells),
    ("La IA bloquea una victoria inmediata", AiBlocksImmediateWin),
    ("La IA toma una victoria inmediata", AiTakesImmediateWin)
};

foreach (var test in tests)
{
    test.Run();
    Console.WriteLine($"OK - {test.Name}");
}

static void DetectsHorizontalWinner()
{
    var game = new TicTacToeGame();

    game.Play(0);
    game.Play(3);
    game.Play(1);
    game.Play(4);
    game.Play(2);

    Assert(game.Result.IsFinished, "La partida debio terminar.");
    Assert(game.Result.Winner == Player.Smile, "Smile debio ganar.");
}

static void DetectsDraw()
{
    var game = new TicTacToeGame();

    foreach (var move in new[] { 0, 1, 2, 4, 3, 5, 7, 6, 8 })
    {
        game.Play(move);
    }

    Assert(game.Result.IsDraw, "La partida debio terminar en empate.");
}

static void BlocksOccupiedCells()
{
    var game = new TicTacToeGame();

    Assert(game.Play(0), "La primera jugada debio ser valida.");
    Assert(!game.Play(0), "No debe permitir jugar en una casilla ocupada.");
    Assert(game.CurrentPlayer == Player.Heart, "El turno no debe cambiar si la jugada falla.");
}

static void AiBlocksImmediateWin()
{
    var board = new[]
    {
        Player.Smile, Player.Smile, Player.None,
        Player.None, Player.Heart, Player.None,
        Player.None, Player.None, Player.None
    };

    var move = TicTacToeAi.FindBestMove(board, Player.Heart, Player.Smile);

    Assert(move == 2, "La IA debio bloquear en la casilla 2.");
}

static void AiTakesImmediateWin()
{
    var board = new[]
    {
        Player.Heart, Player.Heart, Player.None,
        Player.Smile, Player.Smile, Player.None,
        Player.None, Player.None, Player.None
    };

    var move = TicTacToeAi.FindBestMove(board, Player.Heart, Player.Smile);

    Assert(move == 2, "La IA debio ganar en la casilla 2.");
}

static void Assert(bool condition, string message)
{
    if (!condition)
    {
        throw new InvalidOperationException(message);
    }
}
