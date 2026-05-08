using UnityEngine;

public class BuildProcessMenuViwer : MonoBehaviour
{
    [SerializeField] private TimerViewer _buildTime;

    public TimerViewer BuildTime => _buildTime;
}