namespace BowlingGameService;

public class BowlingScoreCalculator
{
    public int CalculateScore(int[] numPinKnockedDown)
    {
        var score = 0;
        var numRoll = 0;
        var normalRolls = 20;
        const int numPins = 10;

        for (int i = 0; i<numPinKnockedDown.Count(); i++) 
        {
            numRoll++;

            if (i < normalRolls) //if not bouns roll
                score += numPinKnockedDown[i];

            if ((numRoll%2 == 0) && (numPinKnockedDown[i] + numPinKnockedDown[i-1] == numPins) && (i < normalRolls)) //spare within 10 frames
            {
                score += numPinKnockedDown[i + 1];
            }

            if ((numRoll%2 == 1) && (numPinKnockedDown[i] == numPins) && (i < normalRolls)) //strike before the last roll
            {
                score = score + numPinKnockedDown[i+1] + numPinKnockedDown[i+2];
                numRoll++;
                normalRolls--;
            }
        }

        return score;
    }
}
