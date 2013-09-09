using System.Drawing;

namespace SoccerBase.BackgroundWorks
{
    public class BGReport
    {
        public string Text;
        public Brush Color;

        public BGReport(string text)
        {
            Text = text;
            Color = Brushes.Black;
        }

        public BGReport(string text, Brush color = null)
        {
            Text = text;
            Color = color ?? Brushes.Black;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}