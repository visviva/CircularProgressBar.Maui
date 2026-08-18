namespace CircularProgressBar.Mobile.Controls;

public partial class CircularProgressBarView : ContentView
{
    public static readonly BindableProperty CenterContentProperty = BindableProperty.Create(
        nameof(CenterContent),
        typeof(View),
        typeof(CircularProgressBarView)
    );

    public static readonly BindableProperty RingThicknessProperty = BindableProperty.Create(
        nameof(RingThickness),
        typeof(float),
        typeof(CircularProgressBarView),
        8.0f,
        propertyChanged: OnDrawablePropertyChanged
    );

    public static readonly BindableProperty RingSpacingProperty = BindableProperty.Create(
        nameof(RingSpacing),
        typeof(float),
        typeof(CircularProgressBarView),
        4.0f,
        propertyChanged: OnDrawablePropertyChanged
    );

    public static readonly BindableProperty StartAngleProperty = BindableProperty.Create(
        nameof(StartAngle),
        typeof(float),
        typeof(CircularProgressBarView),
        90.0f,
        propertyChanged: OnDrawablePropertyChanged
    );

    public static readonly BindableProperty DisabledOpacityProperty = BindableProperty.Create(
        nameof(DisabledOpacity),
        typeof(float),
        typeof(CircularProgressBarView),
        0.38f,
        propertyChanged: OnDrawablePropertyChanged
    );

    public static readonly BindableProperty TrackColorProperty = BindableProperty.Create(
        nameof(TrackColor),
        typeof(Color),
        typeof(CircularProgressBarView),
        Colors.DarkSlateGrey,
        propertyChanged: OnDrawablePropertyChanged
    );

    public static readonly BindableProperty ProgressColorProperty = BindableProperty.Create(
        nameof(ProgressColor),
        typeof(Color),
        typeof(CircularProgressBarView),
        Colors.DeepSkyBlue,
        propertyChanged: OnDrawablePropertyChanged
    );

    public static readonly BindableProperty InnerProgressProperty = BindableProperty.Create(
        nameof(InnerProgress),
        typeof(float),
        typeof(CircularProgressBarView),
        0.0f,
        propertyChanged: OnDrawablePropertyChanged
    );

    public static readonly BindableProperty OuterProgressProperty = BindableProperty.Create(
        nameof(OuterProgress),
        typeof(float),
        typeof(CircularProgressBarView),
        0.0f,
        propertyChanged: OnDrawablePropertyChanged
    );

    private static readonly BindablePropertyKey ContentDiameterPropertyKey =
        BindableProperty.CreateReadOnly(
            nameof(ContentDiameter),
            typeof(float),
            typeof(CircularProgressBarView),
            0.0f
        );

    public static readonly BindableProperty ContentDiameterProperty =
        ContentDiameterPropertyKey.BindableProperty;

    private static readonly BindablePropertyKey CenterContentMaxPropertyKey =
        BindableProperty.CreateReadOnly(
            nameof(CenterContentMax),
            typeof(double),
            typeof(CircularProgressBarView),
            0.0
        );

    public static readonly BindableProperty CenterContentMaxProperty =
        CenterContentMaxPropertyKey.BindableProperty;

    private readonly CircularProgressBarDrawable _drawable = new();
    private GraphicsView? _graphicsView;

    public CircularProgressBarView()
    {
        _drawable.ContentDiameterChanged += OnContentDiameterChanged;
        PropertyChanged += OnViewPropertyChanged;

        InitializeComponent();
        ControlTemplate = Resources["CircularProgressBarTemplate"] as ControlTemplate;
    }

    public View? CenterContent
    {
        get => (View?)GetValue(CenterContentProperty);
        set => SetValue(CenterContentProperty, value);
    }

    public float RingThickness
    {
        get => (float)GetValue(RingThicknessProperty);
        set => SetValue(RingThicknessProperty, value);
    }

    public float RingSpacing
    {
        get => (float)GetValue(RingSpacingProperty);
        set => SetValue(RingSpacingProperty, value);
    }

    public float StartAngle
    {
        get => (float)GetValue(StartAngleProperty);
        set => SetValue(StartAngleProperty, value);
    }

    public float DisabledOpacity
    {
        get => (float)GetValue(DisabledOpacityProperty);
        set => SetValue(DisabledOpacityProperty, value);
    }

    public Color TrackColor
    {
        get => (Color)GetValue(TrackColorProperty);
        set => SetValue(TrackColorProperty, value);
    }

    public Color ProgressColor
    {
        get => (Color)GetValue(ProgressColorProperty);
        set => SetValue(ProgressColorProperty, value);
    }

    public float InnerProgress
    {
        get => (float)GetValue(InnerProgressProperty);
        set => SetValue(InnerProgressProperty, value);
    }

    public float OuterProgress
    {
        get => (float)GetValue(OuterProgressProperty);
        set => SetValue(OuterProgressProperty, value);
    }

    public float ContentDiameter => (float)GetValue(ContentDiameterProperty);

    public double CenterContentMax => (double)GetValue(CenterContentMaxProperty);

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        _graphicsView = GetTemplateChild("GraphicsView") as GraphicsView;
        if (_graphicsView is not null)
        {
            _graphicsView.Drawable = _drawable;
        }

        UpdateDrawable();
    }

    private static void OnDrawablePropertyChanged(
        BindableObject bindable,
        object oldValue,
        object newValue
    )
    {
        if (bindable is CircularProgressBarView view)
        {
            view.UpdateDrawable();
        }
    }

    private void UpdateDrawable()
    {
        _drawable.RingProperties = new RingProperties(
            RingThickness,
            RingSpacing,
            StartAngle,
            DisabledOpacity,
            TrackColor,
            ProgressColor
        );
        _drawable.RingProgress = new RingProgress(
            InnerProgress,
            OuterProgress,
            IsEnabled
        );

        _graphicsView?.Invalidate();
    }

    private void OnViewPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IsEnabled))
        {
            UpdateDrawable();
        }
    }

    private void OnContentDiameterChanged(float contentDiameter)
    {
        Dispatcher.Dispatch(() =>
        {
            SetValue(ContentDiameterPropertyKey, contentDiameter);
            SetValue(CenterContentMaxPropertyKey, (double)contentDiameter);
        });
    }
}
