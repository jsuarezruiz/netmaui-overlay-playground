using Microsoft.Maui.Controls.Shapes;

namespace DrasticOverlay.Sample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestOverlayPage : ContentPage
    {
        readonly TestWindow window;
        
        public TestOverlayPage(TestWindow window)
        {
            this.window = window;
            InitializeComponent();
            UpdateBorderView();
        }

        private void UpdateBorderView()
        {
            var borderShape = new RoundRectangle
            {
                CornerRadius = new CornerRadius(2, 2, 2, 2)
            };

            BorderView.StrokeShape = borderShape;
            BorderView.Stroke = new SolidColorBrush(Colors.Black);
        }

        private async void OnPageOverlay(object sender, EventArgs e)
        {
            this.window.pageOverlay.RemovePage();
        }
    }
}
