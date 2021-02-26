using System;
using Xamarin.Forms;

namespace SimpleDrumSequencer.Controls
{

    public class ToggleButton : Button
    {
        public ToggleButton()
        {
            Clicked += (sender, args) => IsToggled ^= true;
        }


        public static BindableProperty PositionStepProperty =
         BindableProperty.Create("PositionStep", typeof(int), typeof(ToggleButton), 0);

        public int PositionStep
        {
            set { SetValue(PositionStepProperty, value); }
            get { return (int)GetValue(PositionStepProperty); }
        }

        public bool IsHit = false;

        public event EventHandler<ToggledEventArgs> Toggled;

        public static BindableProperty IsToggledProperty =
            BindableProperty.Create("IsToggled", typeof(bool), typeof(ToggleButton), false,
                                    propertyChanged: OnIsToggledChanged);

        public bool IsToggled
        {
            set { SetValue(IsToggledProperty, value); }
            get { return (bool)GetValue(IsToggledProperty); }
        }

        public static BindableProperty PositionSequencerProperty =
           BindableProperty.Create("PositionSequencer", typeof(int), typeof(ToggleButton), 0,
                                   propertyChanged: OnPositionSequencerChanged);

        public int PositionSequencer
        {
            set { SetValue(PositionSequencerProperty, value); }
            get { return (int)GetValue(PositionSequencerProperty); }
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            VisualStateManager.GoToState(this, "ToggledOff_NotHit");
        }

        static void OnIsToggledChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ToggleButton toggleButton = (ToggleButton)bindable;
            bool isToggled = (bool)newValue;

            // Fire event
            toggleButton.Toggled?.Invoke(toggleButton, new ToggledEventArgs(isToggled));

            SetVisualState(toggleButton);
        }

        static void OnPositionSequencerChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ToggleButton toggleButton = (ToggleButton)bindable;
            bool isHit = (int)newValue == toggleButton.PositionStep;

            toggleButton.IsHit = isHit;

            SetVisualState(toggleButton);
        }

        private static void SetVisualState(ToggleButton toggleButton)
        {
            if (toggleButton.IsHit == false && toggleButton.IsToggled == false) 
                VisualStateManager.GoToState(toggleButton, "ToggledOff_NotHit");
            else if (toggleButton.IsHit == true && toggleButton.IsToggled == false)
                VisualStateManager.GoToState(toggleButton, "ToggledOff_GotHit");
            else if (toggleButton.IsHit == false && toggleButton.IsToggled == true)
                VisualStateManager.GoToState(toggleButton, "ToggledOn_NotHit");
            else if (toggleButton.IsHit == true && toggleButton.IsToggled == true)
                VisualStateManager.GoToState(toggleButton, "ToggledOn_GotHit");
        }
    }
}