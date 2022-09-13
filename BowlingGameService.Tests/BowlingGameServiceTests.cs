using System;
using System.Net.NetworkInformation;
using FluentAssertions;
namespace BowlingGameService.Tests;

public class Tests
{
    private BowlingScoreCalculator _bowlingScoreCalculator;

    [SetUp]
    public void Setup()
    {
        _bowlingScoreCalculator = new BowlingScoreCalculator();
    }

    [TestCase(new int[] { 4 }, 4)] //signle roll
    [TestCase(new int[] { 4, 3 }, 7)] //no spare nor strike in first frame
    [TestCase(new int[] { 4, 6, 5, 3 }, 23)] //spare in first frame
    [TestCase(new int[] { 4, 6, 5, 5, 3, 2 }, 33)] //first 2 consective spares
    [TestCase(new int[] { 4, 6, 5, 5, 3, 7, 9, 0 }, 56)] //spare in first 3 frames but not the 4th frame
    [TestCase(new int[] { 4, 6, 5, 3, 3, 7, 9, 0 }, 51)] //spare in 1st & 3rd frames
    [TestCase(new int[] { 4, 6, 5, 3, 3, 7, 9, 0, 3, 7, 2, 7, 3, 4, 6, 2, 3, 7, 9, 1, 5 }, 121)] //spare in 3rd, 5th & 10th frames
    [TestCase(new int[] { 10, 6, 3 }, 28)] //strike in first frame
    [TestCase(new int[] { 10, 10, 8, 0 }, 54)] //strike in 1st & 2nd frames but not the 3td frame
    [TestCase(new int[] { 10, 8, 1, 10, 0, 7 }, 52)] //strike in 1st & 3rd frames
    [TestCase(new int[] { 10, 4, 3, 8, 0, 10, 3, 4, 9, 0, 10, 5, 2, 7, 2, 1, 4 }, 103)] //played 10 frames, strike in 1st, 4th & 7th frames
    [TestCase(new int[] { 10, 4, 3, 8, 0, 10, 3, 4, 9, 0, 10, 5, 2, 7, 2, 10, 3, 5 }, 116)] //played 10 frames, strike in 1st, 4th & 10th frames
    [TestCase(new int[] { 10, 4, 6, 8, 0, 10, 3, 4, 9, 1, 10, 5, 2, 7, 2, 10, 3, 5 }, 141)] //played 10 frames, strike in 1st, 4th & 10th frames, spare in 2nd & 6th frames
    [TestCase(new int[] { 10, 4, 6, 8, 0, 10, 3, 4, 9, 1, 10, 5, 2, 7, 2, 10, 3, 7 }, 143)] //played 10 frames, strike in 1st, 4th & 10th frames, spare in 2nd, 6th & bonus frames
    [TestCase(new int[] { 10, 4, 6, 8, 0, 10, 3, 4, 9, 1, 10, 5, 2, 7, 2, 10, 10, 10 }, 153)] //played 10 frames, spare in 2nd, 6th & bonus frames, strike in 1st, 4th, 10th & bonus frames
    [TestCase(new int[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 }, 300)] //all strike 
    [TestCase(new int[] { 9, 0, 9, 0, 9, 0, 9, 0, 9, 0, 9, 0, 9, 0, 9, 0, 9, 0, 9, 0 }, 90)] //all 9 and 0
    [TestCase(new int[] { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 }, 150)] //all 5 and then spare, with bouns roll 5
    public void Given_Number_Of_Pin_Knocks_Down_Return_Score(int[] numPinKnockedDown, int expectedScore)
    {
        _bowlingScoreCalculator.CalculateScore(numPinKnockedDown).Should().Be(expectedScore);        
    }
}