namespace Score
{
    /// <summary>
    /// During the match this class keeps and manages the player's score.
    /// </summary>
    public class ScoreController : IScore
    {
        // Store the current points of player
        private int _totalScore;
    
        public void AddPoints()
        {
            _totalScore += 100;
        
        } 
        public void RewardPoints()
        {
            _totalScore += 1000;
        }
        public int CurrentPoints()
        {
            return _totalScore;
        }
    }
}