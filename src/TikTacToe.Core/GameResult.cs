namespace TikTacToe.Core;

public sealed record GameResult(bool IsFinished, Player Winner, bool IsDraw)
{
    public static GameResult InProgress { get; } = new(false, Player.None, false);

    public static GameResult Draw { get; } = new(true, Player.None, true);

    public static GameResult WonBy(Player player) => new(true, player, false);
}
