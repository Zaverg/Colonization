using UnityEngine;

public class CounterViewer : TextViewer
{
    public void UpdateView(int value)
    {
        Text.text = text + string.Format("{0:F0}", value);
    }
}