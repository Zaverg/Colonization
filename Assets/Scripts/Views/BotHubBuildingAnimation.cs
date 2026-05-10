using UnityEngine;
using DG.Tweening;

public class BotHubBuildingAnimation
{
    public void StartAnimation(Material material, float duration)
    {
        float defoultAlpha = material.color.a;

        material.DOFade(1, duration);
    }
}