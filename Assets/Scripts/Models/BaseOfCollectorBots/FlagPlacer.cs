using UnityEngine;

public class FlagPlacer : MonoBehaviour
{
    [SerializeField] private Flag _activeFlag;

    public void TryInstalFlag(Transform surface)
    {
        if (_activeFlag != null && surface.TryGetComponent<Map>(out _))
        {
            _activeFlag.Instal();
            _activeFlag = null;
        }
        else if (_activeFlag != null)
        {
            _activeFlag.Deactivate();
            _activeFlag = null;
        }
    }

    public void SetFlag(Flag flag)
    {
        _activeFlag = flag;
    }
}