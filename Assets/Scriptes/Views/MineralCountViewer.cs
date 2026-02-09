public class MineralCountViewer : TextViewer
{
    public void UpdateView(int value)
    {
        Text.text = SubText + string.Format("{0:F0}", value);
    }
}