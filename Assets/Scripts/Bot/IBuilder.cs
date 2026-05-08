public interface IBuilder
{
    public float BuildSpeedСoefficient {  get; }
    public void StartBuild(BuildProcess buildObject, IBot stateMachine);
}