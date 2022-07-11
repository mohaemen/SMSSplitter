using NUnit.Framework;
using SMSSplitter;

namespace SMSSplitterTests;

public class Tests
{
    [TestCase(1 , "○")]
    public void GivenNotAllowedCharacter_ThrowError(int expectedNumberOfParts, string message)
    {
        Assert.Throws<ArgumentException>(() => Splitter.Split(message));
    }

    [TestCase(1 , "Lorem ipsum dolor sit amet")]
    public void GivenLessThan160LongText_DoesNotSplit(int expectedNumberOfParts, string message)
    {
        string[] parts = Splitter.Split(message).ToArray();

        Assert.That(parts.Length, Is.EqualTo(expectedNumberOfParts));

        Assert.That(parts[0].Length, Is.EqualTo(26));
        Assert.That(parts[0], Is.EqualTo("Lorem ipsum dolor sit amet"));
    }

    [TestCase(1 , "Lorem ipsum dolor sit amet, consectetur adipiscing elit.Sed quis aliquet mauris, in tempor libero.Phasellus in vestibulum felis, sed facilisis libero.Vesttibulu")]
    public void Given160LongText_DoesNotSplit(int expectedNumberOfParts, string message)
    {
        string[] parts = Splitter.Split(message).ToArray();

        Assert.That(parts.Length, Is.EqualTo(expectedNumberOfParts));

        Assert.That(parts[0].Length, Is.EqualTo(160));
        Assert.That(parts[0], Is.EqualTo("Lorem ipsum dolor sit amet, consectetur adipiscing elit.Sed quis aliquet mauris, in tempor libero.Phasellus in vestibulum felis, sed facilisis libero.Vesttibulu"));
    }

    [TestCase(6 , "Lorem ipsum dolor sit amet, consectetur adipiscing elit.Sed quis aliquet mauris, in tempor libero.Phasellus in vestibulum felis, sed facilisis libero.Vestibulum in felis augue.Suspendisse odio risus, pretium quis dignissim ut, elementum sit amet nunc.Proin sed orci a leo malesuada luctus a at tortor.Duis in ipsum lectus.Mauris non urna ut sapien rutrum lobortis.Sed at turpis sit amet orci elementum gravida eu a dui.Morbi sapien lectus, viverra a eleifend eget, dignissim eu felis.Proin non ullamcorper dui.Cras congue facilisis justo, id feugiat erat ornare scelerisque.In hac habitasse platea dictumst.Pellentesque sed ante tortor.Mauris accumsan viverra purus, ac tincidunt justo consequat sed.Aenean condimentum nisi eu diam vestibulum efficitur.Ut felis eros, congue et malesuada vitae, blandit eu sem.")]
    public void GivenLongerThan160Text_SplitIntoCorrectParts(int expectedNumberOfParts, string message)
    {
        string[] parts = Splitter.Split(message).ToArray();

        Assert.That(parts.Length, Is.EqualTo(expectedNumberOfParts));

        Assert.That(parts[0].Length, Is.EqualTo(153));
        Assert.That(parts[0], Is.EqualTo("Lorem ipsum dolor sit amet, consectetur adipiscing elit.Sed quis aliquet mauris, in tempor libero.Phasellus in vestibulum felis, sed facilisis libero.Ves"));
        Assert.That(parts[1].Length, Is.EqualTo(153));
        Assert.That(parts[2].Length, Is.EqualTo(153));
        Assert.That(parts[3].Length, Is.EqualTo(153));
        Assert.That(parts[4].Length, Is.EqualTo(153));
        Assert.That(parts[5].Length, Is.EqualTo(45));
    }

    [TestCase(2 , "Lorem ipsum dolor sit amet, consectetur adipiscing elit.Sed quis aliquet mauris, in tempor€libero.Phasellus in vestibulum felis, sed facilisis libero.Vesttibulu")]
    public void GivenTextWithExtendedCharacter_SplitIntoCorrectParts(int expectedNumberOfParts, string message)
    {
        string[] parts = Splitter.Split(message).ToArray();

        Assert.That(parts.Length, Is.EqualTo(expectedNumberOfParts));

        Assert.That(parts[0].Length, Is.EqualTo(152));
        Assert.That(parts[0], Is.EqualTo("Lorem ipsum dolor sit amet, consectetur adipiscing elit.Sed quis aliquet mauris, in tempor€libero.Phasellus in vestibulum felis, sed facilisis libero.Ve"));
        Assert.That(parts[1].Length, Is.EqualTo(8));
        Assert.That(parts[1], Is.EqualTo("sttibulu"));
    }

    [TestCase(2 , "Lorem ipsum dolor sit amet, consectetur adipiscing elit.Sed quis aliquet mauris, in tempor libero.Phasellus in vestibulum felis, sed facilisis libero.Ve€tibulum in felis augue.")]
    public void GivenTextWithExtendedCharacterOnPos153_SplitIntoCorrectParts(int expectedNumberOfParts, string message)
    {
        string[] parts = Splitter.Split(message).ToArray();

        Assert.That(parts.Length, Is.EqualTo(expectedNumberOfParts));

        Assert.That(parts[0].Length, Is.EqualTo(152));
        Assert.That(parts[0], Is.EqualTo("Lorem ipsum dolor sit amet, consectetur adipiscing elit.Sed quis aliquet mauris, in tempor libero.Phasellus in vestibulum felis, sed facilisis libero.Ve"));
        Assert.That(parts[1].Length, Is.EqualTo(24));
        Assert.That(parts[1], Is.EqualTo("€tibulum in felis augue."));
    }

    [TestCase(3 , "}}}}}}}}}}}}}}}}}}}}}}}}}a}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}")]
    public void GivenTextWithExtendedCharacters_SplitIntoCorrectParts(int expectedNumberOfParts, string message)
    {
        string[] parts = Splitter.Split(message).ToArray();

        Assert.That(parts.Length, Is.EqualTo(expectedNumberOfParts));

        Assert.That(parts[0].Length, Is.EqualTo(77));
        Assert.That(parts[0], Is.EqualTo("}}}}}}}}}}}}}}}}}}}}}}}}}a}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}"));
        Assert.That(parts[1].Length, Is.EqualTo(76));
        Assert.That(parts[1], Is.EqualTo("}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}"));
        Assert.That(parts[2].Length, Is.EqualTo(61));
        Assert.That(parts[2], Is.EqualTo("}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}"));
    }

    [TestCase(3 , "}}}}}}}}}}}}}}}}}}}}}}}}}a}}}}}}}}}}}}}}}}}s}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}")]
    public void GivenTextWithExtendedCharacters_SplitIntoCorrectParts2(int expectedNumberOfParts, string message)
    {
        string[] parts = Splitter.Split(message).ToArray();

        Assert.That(parts.Length, Is.EqualTo(expectedNumberOfParts));

        Assert.That(parts[0].Length, Is.EqualTo(77));
        Assert.That(parts[0], Is.EqualTo("}}}}}}}}}}}}}}}}}}}}}}}}}a}}}}}}}}}}}}}}}}}s}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}"));
        Assert.That(parts[1].Length, Is.EqualTo(76));
        Assert.That(parts[1], Is.EqualTo("}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}"));
        Assert.That(parts[2].Length, Is.EqualTo(61));
        Assert.That(parts[2], Is.EqualTo("}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}"));
    }
}