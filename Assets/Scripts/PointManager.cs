using System;

public static class PointManager
{
    public static int Score { get; private set; } = 0;
    public static Action<int> OnScoreUpdated;

    public static void AddPoints(int points)
    {
        Score += points;
        OnScoreUpdated?.Invoke(Score);
    }
    public static void SetScore(int points)
    {
        Score = points;
        OnScoreUpdated?.Invoke(Score);
    }
}
