using UnityEngine;

public class Builder : IBuilder
{
    public void Build(Transform buildObject)
    {
        buildObject.gameObject.SetActive(true);
    }
}
