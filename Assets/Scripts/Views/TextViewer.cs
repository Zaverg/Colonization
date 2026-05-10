using UnityEngine;
using TMPro;

public abstract class TextViewer : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI Text;

    public string text { get; private set; }

    private void Awake()
    {
        text = Text.text;
    }
}