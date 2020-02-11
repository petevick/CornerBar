using CornerBar.Forms.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CornerBar.Helpers
{

    public class NumericValidationBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            double result;
            bool isValid = Double.TryParse(args.NewTextValue, out result);
            if (sender is ECEntry)
            {
                ECEntry mvsender = sender as ECEntry;
                ((ECEntry)sender).TextColor = isValid ? mvsender.ValidTextColor : mvsender.InvalidTextColor;
            }
            else
            {
                ((Entry)sender).TextColor = isValid ? Color.Default : Color.Red;
            }
        }
    }

    public class RequiredField : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            bool isValid = false;
            if (args.NewTextValue != null)
            {
                isValid = args.NewTextValue.Length == 0 ? false : true;
            }

            ((Entry)sender).TextColor = isValid ? Color.Default : Color.Red;
        }
    }


    
public class PhoneValidatorBehavior : Behavior<Entry>
{
    const string phoneRegex = @"^([a-zA-Z0-9]+(?:[.-]?[a-zA-Z0-9]+)*@[a-zA-Z0-9]+(?:[.-]?[a-zA-Z0-9]+)*\.[a-zA-Z]{2,7})$";
    static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(PhoneValidatorBehavior), false);
    public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;
    public bool IsValid
    {
        get { return (bool)base.GetValue(IsValidProperty); }
        private set { base.SetValue(IsValidPropertyKey, value); }
    }
    protected override void OnAttachedTo(Entry bindable)
    {
        bindable.TextChanged += HandleTextChanged;
    }
    void HandleTextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                IsValid = false;
            }
            else
            {
                IsValid = (Regex.IsMatch(e.NewTextValue, phoneRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
            }

            ECEntry entry = sender as ECEntry;
            if (entry != null)
            {
                entry.TextColor = IsValid ? entry.ValidTextColor : entry.InvalidTextColor;
            }
            else {
                ((Entry)sender).TextColor = IsValid ? Color.FromHex("#F8931E") : Color.Red;
            }
        }
        catch
        {
            IsValid = false;
        }
    }
    protected override void OnDetachingFrom(Entry bindable)
    {
        bindable.TextChanged -= HandleTextChanged;
    }
}
public class EmailValidatorBehavior : Behavior<Entry>
    {
        const string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
        static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(EmailValidatorBehavior), false);
        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;
        public bool IsValid
        {
            get { return (bool)base.GetValue(IsValidProperty); }
            private set { base.SetValue(IsValidPropertyKey, value); }
        }
        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += HandleTextChanged;
        }
        void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(e.NewTextValue))
                {
                    IsValid = false;
                }
                else
                {
                    IsValid = (Regex.IsMatch(e.NewTextValue, emailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
                }

                ECEntry entry = sender as ECEntry;
                if (entry != null)
                {
                    entry.TextColor = IsValid ? entry.ValidTextColor : entry.InvalidTextColor;
                }
                else {
                    ((Entry)sender).TextColor = IsValid ? Color.FromHex("#F8931E") : Color.Red;
                }
            }
            catch
            {
                IsValid = false;
            }
        }
        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= HandleTextChanged;
        }
    }

    public class MaxLengthValidator : Behavior<Entry>
    {
        public static readonly BindableProperty MaxLengthProperty = BindableProperty.Create("MaxLength", typeof(int), typeof(MaxLengthValidator), 0);

        public int MaxLength
        {
            get { return (int)GetValue(MaxLengthProperty); }
            set { SetValue(MaxLengthProperty, value); }
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += bindable_TextChanged;
        }

        private void bindable_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (MaxLength != null && MaxLength.HasValue)
            if (e.NewTextValue.Length > 0 && e.NewTextValue.Length > MaxLength)
                ((Entry)sender).Text = e.NewTextValue.Substring(0, MaxLength);
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= bindable_TextChanged;

        }
    }

}

