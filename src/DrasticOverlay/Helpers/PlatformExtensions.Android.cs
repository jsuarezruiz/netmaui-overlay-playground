﻿using Microsoft.Maui.Platform;
using AView = Android.Views.View;

namespace DrasticOverlay
{
    internal static class PlatformExtensions
    {
        internal static AView? GetNative(this IElement view, bool returnWrappedIfPresent)
        {
            if (view.Handler is IPlatformViewHandler nativeHandler && nativeHandler.PlatformView != null)
                return nativeHandler.PlatformView;

            return (view.Handler?.PlatformView as AView);
        }

        public static NavigationRootManager GetNavigationRootManager(this IMauiContext mauiContext) =>
            mauiContext.Services.GetRequiredService<NavigationRootManager>();

        internal static List<T> GetChildView<T>(this Android.Views.ViewGroup view)
        {
            var childCount = view.ChildCount;
            var list = new List<T>();
            for (var i = 0; i < childCount; i++)
            {
                var child = view.GetChildAt(i);
                if (child is T tChild)
                    list.Add(tChild);
            }
            return list;
        }
    }
}
