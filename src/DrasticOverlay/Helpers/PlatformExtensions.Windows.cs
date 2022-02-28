using System;
using Microsoft.UI.Xaml;

namespace DrasticOverlay
{
	internal static class PlatformExtensions
	{
		internal static FrameworkElement? GetNative(this IElement view, bool returnWrappedIfPresent)
		{
			if (view.Handler is IPlatformViewHandler nativeHandler && nativeHandler.PlatformView != null)
				return nativeHandler.PlatformView;

			return (view.Handler?.PlatformView as FrameworkElement);
		}
	}
}
