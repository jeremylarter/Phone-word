using System;
using Xamarin.Forms;

namespace Phoneword
{
    public class MainPage : ContentPage
    {
        private Entry PhonewordEntry { get; }
        private Button TranslateButton { get; }
        private Button CallButton { get; }
        private string TranslatedNumber { get; set; }

        public MainPage()
        {
            Padding = Device.OnPlatform<double>(40, 20, 20);

            PhonewordEntry = new Entry()
            {
                Placeholder = "Enter a phoneword",
                Text = "1-855-XAMARIN"
            };
            TranslateButton = new Button
            {
                Text = "Translate"
            };
            TranslateButton.Clicked += OnTranslate;

            CallButton = new Button
            {
                Text = "Call",
                IsEnabled = false
            };
            CallButton.Clicked += OnCall;

            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Spacing = 15,
                Children =
                {
                    new Label
                    {
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment = TextAlignment.Start,
                        Text = "Enter a phoneword:"
                    },
                    PhonewordEntry,
                    TranslateButton,
                    CallButton
                }
            };
        }

        private async void OnCall(object sender, EventArgs e)
        {
            if (await DisplayAlert("Dial a number", "Would you like to call " + TranslatedNumber + "?", "Yes", "No"))
            {
                var dialer = DependencyService.Get<IDialer>();
                if (dialer != null)
                    dialer.Dial(TranslatedNumber);
            }
        }

        private void OnTranslate(object sender, EventArgs e)
        {
            var enteredNumber = PhonewordEntry.Text;
            TranslatedNumber = Core.PhonewordTranslator.ToNumber(enteredNumber);
            if (string.IsNullOrEmpty(TranslatedNumber))
            {
                CallButton.IsEnabled = false;
                CallButton.Text = "Call";
            }
            else
            {
                CallButton.IsEnabled = true;
                CallButton.Text = "Call " + TranslatedNumber;
            }
        }
    }
}