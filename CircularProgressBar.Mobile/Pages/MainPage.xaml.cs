using System.Globalization;
using CircularProgressBar.Mobile.ViewModels;

namespace CircularProgressBar.Mobile;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;

        EntryX.TextChanged += OnCenterChanged;
        EntryY.TextChanged += OnCenterChanged;

        UpdateCenter();
    }

    private void OnCenterChanged(object? sender, TextChangedEventArgs e)
    {
        UpdateCenter();
    }

    private void UpdateCenter()
    {
        bool validX = double.TryParse(
            EntryX.Text,
            NumberStyles.Float,
            CultureInfo.CurrentCulture,
            out double x
        );

        bool validY = double.TryParse(
            EntryY.Text,
            NumberStyles.Float,
            CultureInfo.CurrentCulture,
            out double y
        );

        if (validX && validY)
        {
            CircularProgressBar.Center = new Point(x, y);
        }
    }
}
