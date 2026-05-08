using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(RectTransform), typeof(CanvasGroup))]
public class MenuAnimation : MonoBehaviour
{
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();

        _rectTransform.localScale = Vector3.zero;
        _canvasGroup.alpha = 0f;

        gameObject.SetActive(false);
    }

    public void OpenMenu(float duration)
    {
        _rectTransform.DOKill();
        _canvasGroup.DOKill();
        gameObject.SetActive(true);

        _rectTransform.DOScale(1f, duration).SetEase(Ease.OutBack);
        _canvasGroup.DOFade(1f, duration).SetEase(Ease.OutQuad);
    }

    public void CloseMenu(float duration)
    {
        _rectTransform.DOKill();
        _canvasGroup.DOKill();

        _rectTransform.DOScale(0f, duration).SetEase(Ease.OutQuad);
        _canvasGroup.DOFade(0f, duration).SetEase(Ease.OutQuad).OnComplete(() => gameObject.SetActive(false));
    }
}
