public class TimerViewer : TextViewer
{
    public void UpdateView(float value)
    {
        Text.text = SubText + string.Format("{0:F0}", value); ;
    }
}
