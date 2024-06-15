using NSubstitute;
using NSubstitute.Core;
using NUnit.Framework;

namespace Tennis.Tests;

public class GameScoreBoardTest
{
    private Notifier _notifier;
    private Reader _reader;
    private GameScoreBoard _gameScoreBoard;

    [Test]
    public void score_board_is_fifteen_love_when_start_match_and_player1_has_scored()
    {
        _reader = Substitute.For<Reader>();
        _notifier = Substitute.For<Notifier>();
        _gameScoreBoard = new GameScoreBoard(_reader, _notifier);
        _reader.Read().Returns("$ score 1");

        _gameScoreBoard.StartGame();

        _notifier.Received(1).Notify("Fifteen Love");
    }
}