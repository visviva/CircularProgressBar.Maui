using CommunityToolkit.Mvvm.ComponentModel;

namespace CircularProgressBar.Mobile.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    public partial double RingThickness { get; set; } = 30.0;

    [ObservableProperty]
    public partial double RingSpacing { get; set; } = 20.0;

    [ObservableProperty]
    public partial double StartAngle { get; set; } = 90.0;

    [ObservableProperty]
    public partial double DisabledOpacity { get; set; } = 0.20;

    [ObservableProperty]
    public partial double InnerProgress { get; set; } = 0.75;

    [ObservableProperty]
    public partial double OuterProgress { get; set; } = 0.35;

    [ObservableProperty]
    public partial bool IsProgressEnabled { get; set; } = true;

    [ObservableProperty]
    public partial Color TrackColor { get; set; } = Colors.DarkSlateGrey;

    [ObservableProperty]
    public partial Color ProgressColor { get; set; } = Colors.DeepSkyBlue;

    [ObservableProperty]
    public partial string CenterText { get; set; } = "Inner Content";

    [ObservableProperty]
    public partial float ContentDiameter { get; set; }
}
