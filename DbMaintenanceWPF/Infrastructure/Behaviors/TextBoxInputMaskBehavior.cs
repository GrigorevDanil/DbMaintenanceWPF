using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

namespace DbMaintenanceWPF.Infrastructure.Behaviors
{
    public class TextBoxInputMaskBehavior : Behavior<TextBox>
    {
        #region DependencyProperties

        public static readonly DependencyProperty InputMaskProperty =
          DependencyProperty.Register("InputMask", typeof(string), typeof(TextBoxInputMaskBehavior), null);

        public string InputMask
        {
            get { return (string)GetValue(InputMaskProperty); }
            set { SetValue(InputMaskProperty, value); }
        }

        public static readonly DependencyProperty PromptCharProperty =
           DependencyProperty.Register("PromptChar", typeof(char), typeof(TextBoxInputMaskBehavior),
                                        new PropertyMetadata('_'));

        public char PromptChar
        {
            get { return (char)GetValue(PromptCharProperty); }
            set { SetValue(PromptCharProperty, value); }
        }

        #endregion

        public MaskedTextProvider Provider { get; private set; }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Loaded += AssociatedObjectLoaded;
            AssociatedObject.PreviewTextInput += AssociatedObjectPreviewTextInput;
            AssociatedObject.PreviewKeyDown += AssociatedObjectPreviewKeyDown;

            DataObject.AddPastingHandler(AssociatedObject, Pasting);
        }


        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.Loaded -= AssociatedObjectLoaded;
            AssociatedObject.PreviewTextInput -= AssociatedObjectPreviewTextInput;
            AssociatedObject.PreviewKeyDown -= AssociatedObjectPreviewKeyDown;

            DataObject.RemovePastingHandler(AssociatedObject, Pasting);
        }

        void AssociatedObjectLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Provider = new MaskedTextProvider(InputMask, CultureInfo.CurrentCulture);
            this.Provider.Set(AssociatedObject.Text);
            this.Provider.PromptChar = this.PromptChar;
            AssociatedObject.Text = this.Provider.ToDisplayString();

            //seems the only way that the text is formatted correct, when source is updated
            var textProp = DependencyPropertyDescriptor.FromProperty(TextBox.TextProperty, typeof(TextBox));
            if (textProp != null)
            {
                textProp.AddValueChanged(AssociatedObject, (s, args) => this.UpdateText());
            }
        }

        void AssociatedObjectPreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            this.TreatSelectedText();

            var position = this.GetNextCharacterPosition(AssociatedObject.SelectionStart);

            if (Keyboard.IsKeyToggled(Key.Insert))
            {
                if (this.Provider.Replace(e.Text, position))
                    position++;
            }
            else
            {
                if (this.Provider.InsertAt(e.Text, position))
                    position++;
            }

            position = this.GetNextCharacterPosition(position);

            this.RefreshText(position);

            e.Handled = true;
        }

        void AssociatedObjectPreviewKeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Space)//handle the space
            {
                this.TreatSelectedText();

                var position = this.GetNextCharacterPosition(AssociatedObject.SelectionStart);

                if (this.Provider.InsertAt(" ", position))
                    this.RefreshText(position);

                e.Handled = true;
            }

            if (e.Key == Key.Back)//handle the back space
            {
                if (this.TreatSelectedText())
                {
                    this.RefreshText(AssociatedObject.SelectionStart);
                }
                else
                {
                    if (AssociatedObject.SelectionStart != 0)
                    {
                        if (this.Provider.RemoveAt(AssociatedObject.SelectionStart - 1))
                            this.RefreshText(AssociatedObject.SelectionStart - 1);
                    }
                }

                e.Handled = true;
            }

            if (e.Key == Key.Delete)//handle the delete key
            {
                //treat selected text
                if (this.TreatSelectedText())
                {
                    this.RefreshText(AssociatedObject.SelectionStart);
                }
                else
                {

                    if (this.Provider.RemoveAt(AssociatedObject.SelectionStart))
                        this.RefreshText(AssociatedObject.SelectionStart);

                }

                e.Handled = true;
            }

        }

        /// <summary>
        /// Pasting prüft ob korrekte Daten reingepastet werden
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                var pastedText = (string)e.DataObject.GetData(typeof(string));

                this.TreatSelectedText();

                var position = GetNextCharacterPosition(AssociatedObject.SelectionStart);

                if (this.Provider.InsertAt(pastedText, position))
                {
                    this.RefreshText(position);
                }
            }

            e.CancelCommand();
        }

        private void UpdateText()
        {
            //check Provider.Text + TextBox.Text
            if (this.Provider.ToDisplayString().Equals(AssociatedObject.Text))
                return;

            //use provider to format
            var success = this.Provider.Set(AssociatedObject.Text);

            //ui and mvvm/codebehind should be in sync
            this.SetText(success ? this.Provider.ToDisplayString() : AssociatedObject.Text);
        }

        /// <summary>
        /// Falls eine Textauswahl vorliegt wird diese entsprechend behandelt.
        /// </summary>
        /// <returns>true Textauswahl behandelt wurde, ansonsten falls </returns>
        private bool TreatSelectedText()
        {
            if (AssociatedObject.SelectionLength > 0)
            {
                return this.Provider.RemoveAt(AssociatedObject.SelectionStart,
                                              AssociatedObject.SelectionStart + AssociatedObject.SelectionLength - 1);
            }
            return false;
        }

        private void RefreshText(int position)
        {
            SetText(this.Provider.ToDisplayString());
            AssociatedObject.SelectionStart = position;
        }

        private void SetText(string text)
        {
            AssociatedObject.Text = String.IsNullOrWhiteSpace(text) ? String.Empty : text;
        }

        private int GetNextCharacterPosition(int startPosition)
        {
            var position = this.Provider.FindEditPositionFrom(startPosition, true);

            if (position == -1)
                return startPosition;
            else
                return position;
        }
    }
}
