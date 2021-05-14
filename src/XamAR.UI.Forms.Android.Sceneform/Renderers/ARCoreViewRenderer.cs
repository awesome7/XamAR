using System;
using System.ComponentModel;
using Android.Content;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Google.AR.Sceneform.UX;
using XamAR.UI.Android.Sceneform.Views;
using XamAR.UI.Forms.Android.Sceneform.Renderers;
using XamAR.UI.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.FastRenderers;
using View = Android.Views.View;

[assembly: ExportRenderer(typeof(ARView), typeof(ArCoreViewRenderer))]

namespace XamAR.UI.Forms.Android.Sceneform.Renderers
{
    /// <summary>
    ///     Responsible for displaying platform specific View.
    /// </summary>
    /// <see cref="https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/custom-renderer/view" />
    public class ArCoreViewRenderer : FrameLayout, IVisualElementRenderer, IViewRenderer
    {
        private ArFragment _arFragment;
        private int? _defaultLabelFor;
        private bool _disposed;
        private ARView _element;
        private FragmentManager _fragmentManager;
        private VisualElementRenderer _visualElementRenderer;
        private VisualElementTracker _visualElementTracker;

        public ArCoreViewRenderer(Context context) : base(context)
        {
            _visualElementRenderer = new VisualElementRenderer(this);
        }

        private FragmentManager FragmentManager
        {
            get
            {
                _fragmentManager = (Context.GetActivity() as FragmentActivity)?.SupportFragmentManager;
                return _fragmentManager;
            }
        }

        private ARView Element
        {
            get => _element;
            set
            {
                if (_element == value)
                {
                    return;
                }

                ARView oldElement = _element;
                _element = value;
                OnElementChanged(new ElementChangedEventArgs<ARView>(oldElement, _element));
            }
        }

        void IViewRenderer.MeasureExactly()
        {
            MeasureExactly(this, Element, Context);
        }

        public event EventHandler<VisualElementChangedEventArgs> ElementChanged;
        public event EventHandler<PropertyChangedEventArgs> ElementPropertyChanged;

        VisualElement IVisualElementRenderer.Element
        {
            get { return Element; }
        }

        VisualElementTracker IVisualElementRenderer.Tracker
        {
            get { return _visualElementTracker; }
        }

        ViewGroup IVisualElementRenderer.ViewGroup
        {
            get { return null; }
        }

        View IVisualElementRenderer.View
        {
            get { return this; }
        }

        SizeRequest IVisualElementRenderer.GetDesiredSize(int widthConstraint, int heightConstraint)
        {
            Measure(widthConstraint, heightConstraint);

            SizeRequest result = new SizeRequest(
                new Size(MeasuredWidth, MeasuredHeight),
                new Size(Context.ToPixels(20), Context.ToPixels(20)));

            return result;
        }

        void IVisualElementRenderer.SetElement(VisualElement element)
        {
            if (!(element is ARView view))
            {
                throw new ArgumentException($"{nameof(element)} must be of type {nameof(ARView)}");
            }

            _visualElementTracker ??= new VisualElementTracker(this);

            Element = view;
        }

        void IVisualElementRenderer.SetLabelFor(int? id)
        {
            _defaultLabelFor ??= LabelFor;

            LabelFor = (int)(id ?? _defaultLabelFor);
        }

        void IVisualElementRenderer.UpdateLayout()
        {
            _visualElementTracker?.UpdateLayout();
        }

        private void OnElementChanged(ElementChangedEventArgs<ARView> e)
        {
            ArFragment newFragment = null;

            if (e.OldElement != null)
            {
                e.OldElement.PropertyChanged -= OnElementPropertyChanged;
                _arFragment.Dispose();
            }

            if (e.NewElement != null)
            {
                this.EnsureId();

                e.NewElement.PropertyChanged += OnElementPropertyChanged;

                ElevationHelper.SetElevation(this, e.NewElement);
                newFragment = new XamARFragment();
            }

            FragmentManager.BeginTransaction()
                .Replace(Id, _arFragment = newFragment, "a7_ar_fragment")
                .Commit();

            ElementChanged?.Invoke(this, new VisualElementChangedEventArgs(e.OldElement, e.NewElement));
        }

        private void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            ElementPropertyChanged?.Invoke(this, e);

            switch (e.PropertyName)
            {
                case "Width":
                    //await cameraFragment.RetrieveCameraDevice();
                    break;
            }
        }

        private static void MeasureExactly(View control, VisualElement element, Context context)
        {
            if (control == null || element == null)
            {
                return;
            }

            double width = element.Width;
            double height = element.Height;

            if (width <= 0 || height <= 0)
            {
                return;
            }

            int realWidth = (int)context.ToPixels(width);
            int realHeight = (int)context.ToPixels(height);

            int widthMeasureSpec = MeasureSpecFactory.MakeMeasureSpec(realWidth, MeasureSpecMode.Exactly);
            int heightMeasureSpec = MeasureSpecFactory.MakeMeasureSpec(realHeight, MeasureSpecMode.Exactly);

            control.Measure(widthMeasureSpec, heightMeasureSpec);
        }

        protected override void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            _arFragment.Dispose();
            _disposed = true;

            if (disposing)
            {
                SetOnClickListener(null);
                SetOnTouchListener(null);

                if (_visualElementTracker != null)
                {
                    _visualElementTracker.Dispose();
                    _visualElementTracker = null;
                }

                if (_visualElementRenderer != null)
                {
                    _visualElementRenderer.Dispose();
                    _visualElementRenderer = null;
                }

                if (Element != null)
                {
                    Element.PropertyChanged -= OnElementPropertyChanged;

                    if (Xamarin.Forms.Platform.Android.Platform.GetRenderer(Element) == this)
                    {
                        Xamarin.Forms.Platform.Android.Platform.SetRenderer(Element, null);
                    }
                }
            }

            base.Dispose(disposing);
        }


        private static class MeasureSpecFactory
        {
            public static int GetSize(int measureSpec)
            {
                const int modeMask = 0x3 << 30;
                return measureSpec & ~modeMask;
            }

            public static int MakeMeasureSpec(int size, MeasureSpecMode mode)
            {
                return size + (int)mode;
            }
        }
    }
}
