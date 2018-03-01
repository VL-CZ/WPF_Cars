using System;

[Serializable()]
public class HighScore
{
    public int Time { get; set; }
    public string Initials { get; set; }
}

public List<HighScore> _highScores = new List<HighScore>();