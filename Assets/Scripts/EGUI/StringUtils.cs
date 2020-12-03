namespace Assets.Scripts.EGUI
{
    public class StringUtils
    {
        public static int GetLineCount(string text)
        {
            return text.Split('\n').Length;
        }
    }
}