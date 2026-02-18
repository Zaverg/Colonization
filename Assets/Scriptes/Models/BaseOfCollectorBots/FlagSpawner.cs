using UnityEngine;
using UnityEngine.InputSystem;

public class FlagSpawner : MonoBehaviour, IClickeble
{
    [SerializeField] private Flag _flag;

    public void OnClick()
    {
        Spawn();
    }

    private void Spawn()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 spawnPosition = new Vector3(worldPosition.x, 1, worldPosition.z);

        Instantiate(_flag, spawnPosition, Quaternion.identity);
    }
}
