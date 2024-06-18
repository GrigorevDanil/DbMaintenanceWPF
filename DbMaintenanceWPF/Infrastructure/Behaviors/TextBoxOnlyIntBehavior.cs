using Microsoft.Xaml.Behaviors;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace DbMaintenanceWPF.Infrastructure.Behaviors
{
    public class TextBoxOnlyIntBehavior : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.PreviewTextInput += PreviewTextInput;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.PreviewTextInput -= PreviewTextInput;
        }

        private void PreviewTextInput(object sender, TextCompositionEventArgs e) => e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
    }
}
