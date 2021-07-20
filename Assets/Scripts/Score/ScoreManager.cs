namespace Score
{
    public class ScoreManager : IScore
    {
        private int _totalScore;

        public void AddPoints() => _totalScore += 100;

        public void RewardPoints() => _totalScore += 1000;
        public void RemovePoints() => _totalScore = 0;

        public int CurrentPoints() => _totalScore;

    }
}