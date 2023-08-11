namespace Score
{
    public class ScoreManager : IScore
    {
        private int m_totalScore;

        public void AddPoints() => m_totalScore += 100;

        public void RewardPoints() => m_totalScore += 1000;
        public void RemovePoints() => m_totalScore = 0;

        public int GetCurrentPoints() => m_totalScore;

    }
}