using UnityEngine;

public class Builder : MonoBehaviour
{
    [SerializeField] private float _buildSpeedСoefficient = 1f;

    public float BuildSpeedСoefficient => _buildSpeedСoefficient;

    public void StartBuild(BuildProcess buildObject, IBot bot)
    {
        buildObject.StartBuild(bot);
    }
}
