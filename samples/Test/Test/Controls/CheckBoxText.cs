using Xamarin.Forms;

namespace Test.Controls
{
    public class CheckBoxText : ContentView
    {
        public static readonly BindableProperty IsCheckedProperty
            = BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(CheckBoxText), true);

        public static readonly BindableProperty TextProperty
            = BindableProperty.Create(nameof(Text), typeof(string), typeof(CheckBoxText), "");

        public bool IsChecked
        {
            get => (bool)GetValue(IsCheckedProperty);
            set => SetValue(IsCheckedProperty, value);
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
    }
}
