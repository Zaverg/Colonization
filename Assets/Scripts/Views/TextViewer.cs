using UnityEngine;
using TMPro;

public abstract class TextViewer : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI Text;

    public string SubText { get; private set; }

    private void Awake()
    {
        SubText = Text.text;
    }
}