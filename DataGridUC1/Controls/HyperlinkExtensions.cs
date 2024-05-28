
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace DataGridUC1.Controls
{
    // https://stackoverflow.com/questions/10238694/example-using-hyperlink-in-wpf

    public static class HyperlinkExtensions
    {
        public static bool GetIsExternal(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsExternalProperty);
        }

        public static void SetIsExternal(DependencyObject obj, bool value)
        {
            obj.SetValue(IsExternalProperty, value);
        }
        public static readonly DependencyProperty IsExternalProperty =
            DependencyProperty.RegisterAttached("IsExternal", typeof(bool), 
                typeof(HyperlinkExtensions), 
                new UIPropertyMetadata(false, OnIsExternalChanged));

        private static void OnIsExternalChanged(object sender, 
            DependencyPropertyChangedEventArgs args)
        {
            var hyperlink = sender as Hyperlink;

            if ((bool)args.NewValue)
                hyperlink.RequestNavigate += Hyperlink_RequestNavigate;
            else
                hyperlink.RequestNavigate -= Hyperlink_RequestNavigate;
        }

        private static void Hyperlink_RequestNavigate(object sender, 
            System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            //Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            //e.Handled = true;

// https://www.codeproject.com/Questions/5380961/How-do-I-fix-net-8-process-start-url-issue
            Hyperlink link = (Hyperlink)e.OriginalSource;
            Process? process = Process.Start(new ProcessStartInfo(link.NavigateUri.AbsoluteUri)
            {
                UseShellExecute = true
            });

            process!.WaitForExit();
            e.Handled = true;
        }
    }
}
