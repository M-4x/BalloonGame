using System;

[Serializable]
public class Score
{
    public float score;
    public Score(float score)
    {
        this.score = float.Parse(score.ToString("F2"));
    }
}
