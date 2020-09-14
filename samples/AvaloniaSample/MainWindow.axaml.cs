using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Metadata;
using Avalonia.Platform;
using Avalonia.Rendering.SceneGraph;
using Avalonia.Skia;
using Avalonia.Visuals.Media.Imaging;
using SkiaSharp;

namespace AvaloniaSample
{
    public class PictureDrawOperation : ICustomDrawOperation
    {
        private readonly SKPicture _picture;

        public PictureDrawOperation(Rect bounds, SKPicture picture)
        {
            _picture = picture;
            Bounds = bounds;
        }

        public void Dispose()
        {
        }

        public Rect Bounds { get; }

        public bool HitTest(Point p) => false;

#pragma warning disable CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).
        public bool Equals(ICustomDrawOperation other) => false;
#pragma warning restore CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).

        public void Render(IDrawingContextImpl context)
        {
            var canvas = (context as ISkiaDrawingContextImpl)?.SkCanvas;
            if (canvas != null && _picture != null)
            {
                canvas.DrawPicture(_picture);
            }
        }
    }

    public class SvgImage : AvaloniaObject, IImage, IAffectsRender
    {
        public static readonly StyledProperty<SKPicture> SourceProperty =
            AvaloniaProperty.Register<SvgImage, SKPicture>(nameof(Source));

        public event EventHandler? Invalidated;

        [Content]
        public SKPicture Source
        {
            get => GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        public Size Size => Source != null ? new Size(Source.CullRect.Width, Source.CullRect.Height) : default;

        void IImage.Draw(DrawingContext context, Rect sourceRect, Rect destRect, BitmapInterpolationMode bitmapInterpolationMode)
        {
            var source = Source;
            if (source == null)
            {
                return;
            }
            var bounds = source.CullRect;
            var scale = Matrix.CreateScale(destRect.Width / sourceRect.Width, destRect.Height / sourceRect.Height);
            var translate = Matrix.CreateTranslation(-sourceRect.X + destRect.X - bounds.Top, -sourceRect.Y + destRect.Y - bounds.Left);
            using (context.PushClip(destRect))
            using (context.PushPreTransform(translate * scale))
            {
                context.Custom(new PictureDrawOperation(new Rect(0, 0, bounds.Width, bounds.Height), source));
            }
        }

        protected override void OnPropertyChanged<T>(AvaloniaPropertyChangedEventArgs<T> change)
        {
            base.OnPropertyChanged(change);
            if (change.Property == SourceProperty)
            {
                RaiseInvalidated(EventArgs.Empty);
            }
        }

        protected void RaiseInvalidated(EventArgs e) => Invalidated?.Invoke(this, e);
    }

    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
