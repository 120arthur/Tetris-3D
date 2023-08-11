namespace Score
{
    public interface IScore
    {
        public void AddPoints();
        public int GetCurrentPoints();
        public void RemovePoints();
        public void RewardPoints();

    }
}