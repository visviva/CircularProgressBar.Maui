namespace CircularProgressBar.Mobile.Controls;

public partial class CircularProgressBarView : ContentView
{
    public static readonly BindableProperty CenterTextProperty = BindableProperty.Create(
        nameof(CenterText),
        typeof(string),
        typeof(CircularProgressBarView),
        string.Empty
    );
    public string CenterText
    {
        get => (string)GetValue(CenterTextProperty);
        set => SetValue(CenterTextProperty, value);
    }

    public static readonly BindableProperty NumberOfCirclesProperty = BindableProperty.Create(
        nameof(NumberOfCircles),
        typeof(int),
        typeof(CircularProgressBarView),
        100,
        propertyChanged: OnPropertyOfDrawableChanged
    );

    public static readonly BindableProperty InitialRadiusProperty = BindableProperty.Create(
        nameof(InitialRadius),
        typeof(double),
        typeof(CircularProgressBarView),
        5.0,
        propertyChanged: OnPropertyOfDrawableChanged
    );

    public static readonly BindableProperty RadiusIncrementProperty = BindableProperty.Create(
        nameof(RadiusIncrement),
        typeof(double),
        typeof(CircularProgressBarView),
        5.0,
        propertyChanged: OnPropertyOfDrawableChanged
    );

    public static readonly BindableProperty CenterProperty = BindableProperty.Create(
        nameof(Center),
        typeof(Point),
        typeof(CircularProgressBarView),
        new Point(10.0, 10.0),
        propertyChanged: OnPropertyOfDrawableChanged
    );

    public Point Center
    {
        get => (Point)GetValue(CenterProperty);
        set => SetValue(CenterProperty, value);
    }

    public int NumberOfCircles
    {
        get => (int)GetValue(NumberOfCirclesProperty);
        set => SetValue(NumberOfCirclesProperty, value);
    }

    public double InitialRadius
    {
        get => (double)GetValue(InitialRadiusProperty);
        set => SetValue(InitialRadiusProperty, value);
    }

    public double RadiusIncrement
    {
        get => (double)GetValue(RadiusIncrementProperty);
        set => SetValue(RadiusIncrementProperty, value);
    }

    private readonly CircularProgressBarDrawable _circularProgressBarDrawable = new();

    public CircularProgressBarView()
    {
        InitializeComponent();
        GraphicsView.Drawable = _circularProgressBarDrawable;
        UpdateDrawable();
    }

    private static void OnPropertyOfDrawableChanged(
        BindableObject bindable,
        object oldValue,
        object newValue
    )
    {
        // Pattern matching verifies the runtime type and avoids an invalid cast.
        if (bindable is CircularProgressBarView view)
        {
            view.UpdateDrawable();
        }
    }

    private void UpdateDrawable()
    {
        _circularProgressBarDrawable.NumberOfCircles = NumberOfCircles;
        _circularProgressBarDrawable.RadiusIncrement = RadiusIncrement;
        _circularProgressBarDrawable.InitialRadius = InitialRadius;
        _circularProgressBarDrawable.Center = Center;
        GraphicsView?.Invalidate();
    }
}
