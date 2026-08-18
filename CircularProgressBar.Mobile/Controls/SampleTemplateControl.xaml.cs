namespace CircularProgressBar.Mobile.Controls;

public partial class SampleTemplateControl : ContentView
{
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title),
        typeof(string),
        typeof(TemplatedControlPage),
        "Empty Title"
    );

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public SampleTemplateControl()
    {
        InitializeComponent();

        var theControlTemplate = Resources["CardTemplate"];
        ControlTemplate = theControlTemplate as ControlTemplate;
    }
}
