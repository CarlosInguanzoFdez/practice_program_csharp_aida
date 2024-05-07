using System.Collections.Generic;
using MarsRover;
using NUnit.Framework;

namespace MarsRover.Tests;

[TestFixture]
[TestOf(typeof(CommandExtractor))]
public class CommandExtractorTest
{

    [Test]
    public void extract_commands_with_lengt_one()
    {
        var commandExtractor = new CommandExtractor(1);

        var result = commandExtractor.Extract("abc");

        var expected = new List<string> { "a", "b", "c" };
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void extract_commands_with_lengt_two()
    {
        var commandExtractor = new CommandExtractor(2);

        var result = commandExtractor.Extract("abc");

        var expected = new List<string> { "ab", "c" };
        Assert.That(result, Is.EqualTo(expected));
    }
}