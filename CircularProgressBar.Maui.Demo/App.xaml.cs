namespace CircularProgressBar.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = new Window(new AppShell());

#if WINDOWS
            window.Width = 1000;
            window.Height = 1100;
#endif

            return window;
        }
    }
}
