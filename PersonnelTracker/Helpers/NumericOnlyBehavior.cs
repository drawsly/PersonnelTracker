using Wpf.Ui.Controls;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors;

namespace PersonnelTracker.Helpers
{
    public class NumericOnlyBehavior : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PreviewTextInput += OnPreviewTextInput;
            DataObject.AddPastingHandler(AssociatedObject, OnPaste);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.PreviewTextInput -= OnPreviewTextInput;
            DataObject.RemovePastingHandler(AssociatedObject, OnPaste);
        }

        private void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextNumeric(e.Text);
        }

        private void OnPaste(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(DataFormats.Text))
            {
                var text = e.DataObject.GetData(DataFormats.Text) as string;
                if (!IsTextNumeric(text))
                    e.CancelCommand();
            }
            else
            {
                e.CancelCommand();
            }
        }

        private bool IsTextNumeric(string? text)
        {
            return int.TryParse(text, out _);
        }
    }
}
