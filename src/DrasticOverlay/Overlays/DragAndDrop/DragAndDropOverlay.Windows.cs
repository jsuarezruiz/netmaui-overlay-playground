﻿using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.Storage.Streams;

namespace DrasticOverlay.Overlays
{
    public partial class DragAndDropOverlay
    {
        Microsoft.UI.Xaml.Controls.Panel? panel;

        public override bool Initialize()
        {
            if (dragAndDropOverlayNativeElementsInitialized)
                return true;

            base.Initialize();

            var _nativeElement = Window.Content.GetNative(true);
            if (_nativeElement == null)
                return false;

            var handler = Window.Handler as Microsoft.Maui.Handlers.WindowHandler;
            if (handler?.PlatformView is not Microsoft.UI.Xaml.Window _window)
                return false;

            this.panel = _window.Content as Microsoft.UI.Xaml.Controls.Panel;
            if (panel == null)
                return false;

            panel.AllowDrop = true;
            panel.DragOver += Panel_DragOver;
            panel.Drop += Panel_Drop;
            panel.DragLeave += Panel_DragLeave;
            panel.DropCompleted += Panel_DropCompleted;
            return dragAndDropOverlayNativeElementsInitialized = true;
        }


        public override bool Deinitialize()
        {
            if (panel != null)
            {
                panel.AllowDrop = false;
                panel.DragOver -= Panel_DragOver;
                panel.Drop -= Panel_Drop;
                panel.DragLeave -= Panel_DragLeave;
                panel.DropCompleted -= Panel_DropCompleted;
            }

            return base.Deinitialize();
        }

        private void Panel_DropCompleted(Microsoft.UI.Xaml.UIElement sender, Microsoft.UI.Xaml.DropCompletedEventArgs args)
        {
            this.IsDragging = false;
        }

        private void Panel_DragLeave(object sender, Microsoft.UI.Xaml.DragEventArgs e)
        {
            this.IsDragging = false;
        }


        private async void Panel_Drop(object sender, Microsoft.UI.Xaml.DragEventArgs e)
        {
            // We're gonna cheat and only take the first item dragged in by the user.
            // In the real world, you would probably want to handle multiple drops and figure
            // Out what to do for your app.
            if (e.DataView.Contains(StandardDataFormats.StorageItems))
            {
                var items = await e.DataView.GetStorageItemsAsync();
                if (items.Any())
                {
                    var item = items.First() as StorageFile;
                    if (item != null)
                    {
                        // Take the random access stream and turn it into a byte array.
                        var bits = (await item.OpenAsync(FileAccessMode.Read));
                        var reader = new DataReader(bits.GetInputStreamAt(0));
                        var bytes = new byte[bits.Size];
                        await reader.LoadAsync((uint)bits.Size);
                        reader.ReadBytes(bytes);
                        this.Drop?.Invoke(this, new DragAndDropOverlayTappedEventArgs(item.Name, bytes));
                    }
                }
            }

            this.IsDragging = false;
        }

        private void Panel_DragOver(object sender, Microsoft.UI.Xaml.DragEventArgs e)
        {
            // For this, we're going to allow "copy"
            // As I want to drag an image into the panel.
            e.AcceptedOperation = Windows.ApplicationModel.DataTransfer.DataPackageOperation.Copy;
            this.IsDragging = true;
        }
    }
}
