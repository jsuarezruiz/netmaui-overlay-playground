﻿namespace DrasticOverlay.Overlays
{
    public partial class PageOverlay : WindowOverlay, IVisualTreeElement
    {
        bool pageOverlayNativeElementsInitialized;
        internal bool pageSet;
        internal Page? page;
        public PageOverlay(IWindow window)
            : base(window)
        {
        }

        public IReadOnlyList<IVisualTreeElement> GetVisualChildren()
        {
            if (pageSet && this.page != null && this.page is IVisualTreeElement element)
                return element.GetVisualChildren();

            return new List<IVisualTreeElement>();
        }

        public IVisualTreeElement? GetVisualParent()
        {
            return this.Window as IVisualTreeElement;
        }
    }
}
