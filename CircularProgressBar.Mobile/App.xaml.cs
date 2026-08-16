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
            window.Width = 450;
            window.Height = 900;
#endif

            return window;
        }
    }
}
