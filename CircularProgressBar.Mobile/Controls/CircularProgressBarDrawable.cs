namespace CircularProgressBar.Mobile.Controls
{
    public class CircularProgressBarDrawable : IDrawable
    {
        public int NumberOfCircles { get; set; } = 200;
        public double InitialRadius { get; set; } = 5.0;
        public double RadiusIncrement { get; set; } = 5.0;

        public Point Center { get; set; } = new Point(10.0, 10.0);

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.SaveState();

            canvas.FillColor = Colors.LightSlateGrey;
            canvas.FillRectangle(dirtyRect);

            canvas.RestoreState();

            double radius = InitialRadius;
            for (int i = 0; i < NumberOfCircles; i++)
            {
                canvas.DrawCircle(Center, radius);
                radius += RadiusIncrement;
            }
        }
    }
}
