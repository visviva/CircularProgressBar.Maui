namespace CircularProgressBar.Mobile.Controls
{
    public class CircularProgressBarDrawable : IDrawable
    {
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.SaveState();

            canvas.FillColor = Colors.LightSlateGrey;
            canvas.FillRectangle(dirtyRect);

            canvas.RestoreState();

            double radius = 5.0;
            for (int i = 0; i < 200; i++)
            {
                canvas.DrawCircle(new Point(10.0, 10.0), radius);
                radius += 5.0;
            }
        }
    }
}
