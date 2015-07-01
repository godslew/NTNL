using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Windows.Interactivity;

using Livet.Messaging;
using Livet.Behaviors.Messaging;
using NTNL.Helper;


namespace NTNL.Views
{
    //Tはこのアクションがアタッチできる型を表します。
    //この場合はこのアクションはFrameworkElementにアタッチできます。
    public class Behaviors
    {
        #region LazySource 添付プロパティ
        [AttachedPropertyBrowsableForType(typeof(Image))]
    public static Uri GetLazySource(Image element)
    {
        return (Uri)element.GetValue(LazySourceProperty);
    }
 
    [AttachedPropertyBrowsableForType(typeof(Image))]
    public static void SetLazySource(Image element, Uri value)
    {
        element.SetValue(LazySourceProperty, value);
    }
 
    public static readonly DependencyProperty LazySourceProperty =
        DependencyProperty.RegisterAttached("LazySource", typeof(Uri), typeof(Behaviors), new PropertyMetadata(null, LazySource_Changed));
 
    #endregion
 
    #region LazyImageSource 添付プロパティ
 
    [AttachedPropertyBrowsableForType(typeof(ImageBrush))]
    public static Uri GetLazyImageSource(ImageBrush element)
    {
        return (Uri)element.GetValue(LazyImageSourceProperty);
    }
 
    [AttachedPropertyBrowsableForType(typeof(ImageBrush))]
    public static void SetLazyImageSource(ImageBrush element, Uri value)
    {
        element.SetValue(LazyImageSourceProperty, value);
    }
 
    public static readonly DependencyProperty LazyImageSourceProperty =
        DependencyProperty.RegisterAttached("LazyImageSource", typeof(Uri), typeof(Behaviors), new PropertyMetadata(null, LazyImageSource_Changed));
 
    #endregion
 
    private static async void LazySource_Changed(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
        var element = sender as Image;
        if (element == null)
        {
            return;
        }
        var image = await helper.GetImage(e.NewValue as Uri);
        if (image != null)
        {
            element.Source = image;
        }
    }
 
    private static async void LazyImageSource_Changed(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
        var element = sender as ImageBrush;
        if (element == null)
        {
            return;
        }
        var image = await helper.GetImage(e.NewValue as Uri);
        if (image != null)
        {
            element.ImageSource = image;
        }
    }
    }
}
