using UnityEngine;

public class Builder : MonoBehaviour, IBuilder
{
    [SerializeField] private float _buildSpeedСoefficient = 1f;

    public float BuildSpeedСoefficient => _buildSpeedСoefficient;

    public void StartBuild(BuildProcess buildObject, IBot bot)
    {
        buildObject.gameObject.SetActive(true);
        buildObject.StartBuild(bot);
    }
}
