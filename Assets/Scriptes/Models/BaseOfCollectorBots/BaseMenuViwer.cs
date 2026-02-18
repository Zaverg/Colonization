using UnityEngine;

public class BaseMenuViwer : MonoBehaviour
{
    public void ChangeActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
