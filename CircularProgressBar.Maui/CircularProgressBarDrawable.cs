namespace CircularProgressBar.Maui;

public record class RingProgress(float InnerProgress, float OuterProgress, bool IsEnabled);

public readonly record struct RingProperties(
    float RingThickness,
    float RingSpacing,
    float StartAngle,
    float DisabledOpacity,
    Color TrackColor,
    Color ProgressColor
);

public class CircularProgressBarDrawable : IDrawable
{
    public event Action<float>? ContentDiameterChanged;

    public RingProperties RingProperties { get; set; } =
        new RingProperties
        {
            RingThickness = 8.0f,
            RingSpacing = 4.0f,
            StartAngle = 90.0f,
            DisabledOpacity = 0.38f,
            TrackColor = Colors.DarkSlateGrey,
            ProgressColor = Colors.DeepSkyBlue,
        };

    public RingProgress RingProgress { get; set; } = new RingProgress(0.0f, 0.0f, true);

    public float ContentDiameter { get; private set; } = 0.0f;

    private static float NormalizeAngle(float angle) => (float.IsFinite(angle)) ? angle % 360 : 90;

    private static float ClampProgress(float progress) =>
        (float.IsFinite(progress)) ? Math.Clamp(progress, 0.0f, 1.0f) : 0.0f;

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        var diameter = Math.Min(dirtyRect.Width, dirtyRect.Height);

        var ringGeometry = RingsGeometry.Create(
            diameter,
            RingProperties.RingThickness,
            RingProperties.RingSpacing
        );

        if (ringGeometry.Thickness <= 0 || ringGeometry.ContentDiameter <= 0)
        {
            SetContentDiameter(0.0f);
            return;
        }

        SetContentDiameter(ringGeometry.ContentDiameter);

        var center = new PointF(
            dirtyRect.Left + (dirtyRect.Width / 2),
            dirtyRect.Top + (dirtyRect.Height / 2)
        );
        float startAngle = NormalizeAngle(RingProperties.StartAngle);

        canvas.SaveState();

        canvas.Alpha = RingProgress.IsEnabled ? 1.0f : RingProperties.DisabledOpacity;
        canvas.StrokeSize = ringGeometry.Thickness;

        DrawRing(canvas, center, ringGeometry.InnerRadius, RingProperties.TrackColor);
        DrawProgressArc(
            canvas,
            center,
            ringGeometry.InnerRadius,
            RingProperties.ProgressColor,
            RingProgress.InnerProgress,
            startAngle
        );

        DrawRing(canvas, center, ringGeometry.OuterRadius, RingProperties.TrackColor);
        DrawProgressArc(
            canvas,
            center,
            ringGeometry.OuterRadius,
            RingProperties.ProgressColor,
            RingProgress.OuterProgress,
            startAngle
        );

        canvas.RestoreState();
    }

    private void SetContentDiameter(float contentDiameter)
    {
        if (ContentDiameter.Equals(contentDiameter))
        {
            return;
        }

        ContentDiameter = contentDiameter;
        ContentDiameterChanged?.Invoke(contentDiameter);
    }

    private static void DrawRing(ICanvas canvas, PointF center, float radius, Color trackColor)
    {
        canvas.SaveState();

        canvas.StrokeLineCap = LineCap.Butt;
        canvas.StrokeColor = trackColor;
        canvas.DrawCircle(center, radius);

        canvas.RestoreState();
    }

    private static void DrawProgressArc(
        ICanvas canvas,
        PointF center,
        float radius,
        Color progressColor,
        float progress,
        float startAngle
    )
    {
        canvas.SaveState();

        canvas.StrokeLineCap = LineCap.Round;
        canvas.StrokeColor = progressColor;

        var clampedProgress = ClampProgress(progress);

        switch (clampedProgress)
        {
            case <= 0:
                break;
            case >= 1.0f:
                canvas.DrawCircle(center, radius);
                break;
            default:
            {
                float endAngle = startAngle - (clampedProgress * 360.0f);
                float left = (center.X - radius);
                float top = (center.Y - radius);
                float diameter = (radius * 2.0f);

                canvas.DrawArc(left, top, diameter, diameter, startAngle, endAngle, true, false);
                break;
            }
        }

        canvas.RestoreState();
    }

    private readonly record struct RingsGeometry(
        float Thickness,
        float OuterRadius,
        float InnerRadius,
        float ContentDiameter
    )
    {
        public static RingsGeometry Create(
            float diameter,
            float requestedThickness,
            float requestedSpacing
        )
        {
            float availableRadius = diameter / 2;
            float thickness = Math.Min(SanitizeLength(requestedThickness), availableRadius / 2);
            float remainingRadius = Math.Max(0, availableRadius - (2 * thickness));
            float spacing = Math.Min(SanitizeLength(requestedSpacing), remainingRadius);
            float outerRadius = Math.Max(0, availableRadius - (thickness / 2));
            float innerRadius = Math.Max(0, outerRadius - thickness - spacing);
            float contentDiameter = Math.Max(
                0,
                (float)((diameter - (4 * thickness) - (2 * spacing)) / Math.Sqrt(2.0))
            );

            return new RingsGeometry(thickness, outerRadius, innerRadius, contentDiameter);
        }

        private static float SanitizeLength(float value)
        {
            return (float.IsFinite(value) && value > 0) ? value : 0;
        }
    }
}
