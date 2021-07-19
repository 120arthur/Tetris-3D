
public interface IScore
{
    public void AddPoints();
    public int CurrentPoints();
 /// <summary>
 /// When the player matches the right words, this method is called.
 /// </summary>
    public void RewardPoints();

}