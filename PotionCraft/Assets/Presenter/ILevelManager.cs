public interface ILevelManager
{
    public LevelData LoadLevel(int levelIndex);
    public void CompleteLevel(int levelIndex);
    public void LoadProgress();
    public void SaveProgress();
}