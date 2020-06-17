namespace ConsoleApplication1.Helper
{
    public class CustomHelper
    {
        public bool IsNumeric(string text)
        {
            double test;
            return double.TryParse(text, out test);
        }
    }
}