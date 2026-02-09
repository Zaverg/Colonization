public interface IMiner
{
    public bool HasMined { get; }
    public void SetDuration(float duration);
    public void StartMining();
}
