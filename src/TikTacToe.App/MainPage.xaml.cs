using TikTacToe.Core;

namespace TikTacToe.App;

public partial class MainPage : ContentPage
{
    private readonly TicTacToeGame _game = new(GameMode.LocalTwoPlayers);
    private readonly Button[] _cells = new Button[9];

    public MainPage()
    {
        InitializeComponent();
        BuildBoard();
        Refresh();
    }

    private void BuildBoard()
    {
        for (var i = 0; i < _cells.Length; i++)
        {
            var button = new Button
            {
                FontSize = 44,
                CornerRadius = 8,
                BackgroundColor = (Color)Application.Current!.Resources["CellBackground"],
                BorderColor = (Color)Application.Current.Resources["CellStroke"],
                BorderWidth = 2,
                TextColor = (Color)Application.Current.Resources["Ink"]
            };

            var cellIndex = i;
            button.Clicked += (_, _) => OnCellClicked(cellIndex);
            _cells[i] = button;
            BoardGrid.Add(button, i % 3, i / 3);
        }
    }

    private void OnCellClicked(int cellIndex)
    {
        if (!_game.Play(cellIndex))
        {
            return;
        }

        if (_game.Mode == GameMode.VersusAi)
        {
            _game.PlayAiTurn();
        }

        Refresh();
    }

    private void OnLocalModeClicked(object sender, EventArgs e)
    {
        _game.Reset(GameMode.LocalTwoPlayers);
        Refresh();
    }

    private void OnAiModeClicked(object sender, EventArgs e)
    {
        _game.Reset(GameMode.VersusAi);
        Refresh();
    }

    private void OnResetClicked(object sender, EventArgs e)
    {
        _game.Reset(_game.Mode);
        Refresh();
    }

    private void Refresh()
    {
        for (var i = 0; i < _cells.Length; i++)
        {
            _cells[i].Text = SymbolFor(_game.Board[i]);
            _cells[i].IsEnabled = !_game.Result.IsFinished && _game.Board[i] == Player.None;
        }

        LocalModeButton.BackgroundColor = _game.Mode == GameMode.LocalTwoPlayers ? Colors.Teal : Colors.LightGray;
        AiModeButton.BackgroundColor = _game.Mode == GameMode.VersusAi ? Colors.Teal : Colors.LightGray;
        LocalModeButton.TextColor = _game.Mode == GameMode.LocalTwoPlayers ? Colors.White : Colors.Black;
        AiModeButton.TextColor = _game.Mode == GameMode.VersusAi ? Colors.White : Colors.Black;

        StatusLabel.Text = BuildStatusText();
    }

    private string BuildStatusText()
    {
        if (_game.Result.IsDraw)
        {
            return "Empate";
        }

        if (_game.Result.IsFinished)
        {
            return $"Gana {SymbolFor(_game.Result.Winner)}";
        }

        if (_game.Mode == GameMode.VersusAi)
        {
            return _game.CurrentPlayer == _game.HumanPlayer
                ? $"Tu turno {SymbolFor(_game.HumanPlayer)}"
                : $"Turno de la IA {SymbolFor(_game.AiPlayer)}";
        }

        return $"Turno {SymbolFor(_game.CurrentPlayer)}";
    }

    private static string SymbolFor(Player player) =>
        player switch
        {
            Player.Smile => "🙂",
            Player.Heart => "😍",
            _ => string.Empty
        };
}
