using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CircularProgressBar.Mobile.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        public partial int Count { get; set; } = 0;

        [ObservableProperty]
        public partial string CounterText { get; set; } = "Click me";

        [RelayCommand]
        async Task IncrementCount()
        {
            await Task.Delay(100);
            Count++;

            if (Count == 1)
                CounterText = $"Clicked {Count} time";
            else
                CounterText = $"Clicked {Count} times";
        }
    }
}
