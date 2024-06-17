using NSubstitute;
using NSubstitute.Core;
using NUnit.Framework;

namespace Tennis.Tests;

public class GameScoreBoardTest
{
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
        _reader.Read().Returns("$ score 1");

        _gameScoreBoard.StartGame();

        _notifier.Received(1).Notify("Fifteen Love");
    }

    [Ignore("")]
    [Test]
    public void score_board_is_love_fifteen_when_start_match_and_player2_has_scored()
    {
        _reader.Read().Returns("$ score 2");

        _gameScoreBoard.StartGame();

        _notifier.Received(1).Notify("Love Fifteen");
    }
}