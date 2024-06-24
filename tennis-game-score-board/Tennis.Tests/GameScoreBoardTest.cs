using NSubstitute;
using NUnit.Framework;

namespace Tennis.Tests;

public class GameScoreBoardTest
{
    private const string Player1Scored = "$ score 1";
    private const string Player2Scored = "$ score 2";
    private const string EndGame = "Exit";
    private Notifier _notifier;
    private Reader _reader;
    private GameScoreBoard _gameScoreBoard;

    [SetUp]
    public void SetUp()
    {
        _reader = Substitute.For<Reader>();
        _notifier = Substitute.For<Notifier>();
        _gameScoreBoard = new GameScoreBoard(_reader, _notifier);
    }

    [Test]
    public void finish_game_when_player1_win_all_points()
    {
        _reader.Read().Returns(Player1Scored, Player1Scored, Player1Scored, Player1Scored);

        _gameScoreBoard.StartGame();

        _notifier.Received(1).Notify("Fifteen Love");
        _notifier.Received(1).Notify("Thirty Love");
        _notifier.Received(1).Notify("Forty Love");
        _notifier.Received(1).Notify("Player 1 has won!!\nIt was a nice game.\nBye now!");
    }

    [Test]
    public void finish_game_when_player2_win_all_points()
    {
        _reader.Read().Returns(Player2Scored, Player2Scored, Player2Scored, Player2Scored);

        _gameScoreBoard.StartGame();

        _notifier.Received(1).Notify("Love Fifteen");
        _notifier.Received(1).Notify("Love Thirty");
        _notifier.Received(1).Notify("Love Forty");
        _notifier.Received(1).Notify("Player 2 has won!!\nIt was a nice game.\nBye now!");
    }

    [Test]
    public void score_is_deuce_and_player1_win_with_one_advantage()
    {
        _reader.Read().Returns(Player2Scored, Player1Scored, Player2Scored, Player1Scored, Player1Scored, Player2Scored, Player1Scored, Player1Scored);

        _gameScoreBoard.StartGame();

        _notifier.Received(1).Notify("Love Fifteen");
        _notifier.Received(1).Notify("Fifteen Fifteen");
        _notifier.Received(1).Notify("Fifteen Thirty");
        _notifier.Received(1).Notify("Thirty Thirty");
        _notifier.Received(1).Notify("Forty Thirty");
        _notifier.Received(1).Notify("Deuce");
        _notifier.Received(1).Notify("Advantage Forty");
        _notifier.Received(1).Notify("Player 1 has won!!\nIt was a nice game.\nBye now!");
        _notifier.Received(8).Notify(Arg.Any<string>());
    }

    [Test]
    public void score_is_deuce_and_then_player2_win_with_two_advantages()
    {
        _reader.Read().Returns(Player2Scored, Player1Scored, Player2Scored, Player1Scored, Player1Scored, Player2Scored, Player2Scored, Player1Scored, Player2Scored, Player2Scored);

        _gameScoreBoard.StartGame();

        _notifier.Received(1).Notify("Love Fifteen");
        _notifier.Received(1).Notify("Fifteen Fifteen");
        _notifier.Received(1).Notify("Fifteen Thirty");
        _notifier.Received(1).Notify("Thirty Thirty");
        _notifier.Received(1).Notify("Forty Thirty");
        _notifier.Received(2).Notify("Deuce");
        _notifier.Received(2).Notify("Forty Advantage");
        _notifier.Received(1).Notify("Player 2 has won!!\nIt was a nice game.\nBye now!");
        _notifier.Received(10).Notify(Arg.Any<string>());
    }

}