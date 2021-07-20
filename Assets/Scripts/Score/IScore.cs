namespace Score
{
    public interface IScore
    {
        public void AddPoints();
        public int CurrentPoints();
        public void RemovePoints();
        public void RewardPoints();

    }
}