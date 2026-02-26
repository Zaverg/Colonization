using UnityEngine;
using UnityEngine.InputSystem;

public class FlagSpawner : MonoBehaviour, IClickable
{
    [SerializeField] private Flag _flag;

    private ICollectorBase _base;

    public void OnClick()
    {
        Spawn();
    }

    private void Spawn()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 spawnPosition = new Vector3(worldPosition.x, 1, worldPosition.z);

        Flag flag = Instantiate(_flag, spawnPosition, Quaternion.identity);
        _flag.Installed += _base.SwitchToBildTask;
    }
}
