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
    public void score_board_is_fifteen_love_when_start_match_and_player1_has_scored()
    {
        _reader.Read().Returns(Player1Scored, EndGame);

        _gameScoreBoard.StartGame();

        _notifier.Received(1).Notify("Fifteen Love");
    }

    [Test]
    public void score_board_is_love_fifteen_when_start_match_and_player2_has_scored()
    {
        _reader.Read().Returns(Player2Scored, EndGame);

        _gameScoreBoard.StartGame();

        _notifier.Received(1).Notify("Love Fifteen");
    }

    [Test]
    public void score_board_is_fifteen_love_when_start_match_and_player1_has_scored_two_points_consecutives()
    {
        _reader.Read().Returns(Player1Scored, Player1Scored,EndGame);

        _gameScoreBoard.StartGame();

        _notifier.Received(1).Notify("Fifteen Love");
        _notifier.Received(1).Notify("Thirty Love");
    }
}