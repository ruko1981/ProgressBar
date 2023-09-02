using SkiaSharp;
using SkiaSharp.Views.Maui;
using SkiaSharp.Views.Maui.Controls;

namespace ProgressBar.Maui;

// All the code in this file is included in all platforms.
public class ProgressBar : SKCanvasView
{
   public static readonly BindableProperty ProgressProperty =
   BindableProperty.Create(nameof(Progress), typeof(float), typeof(ProgressBar), 0f, propertyChanged: OnBindablePropertyChanged);

   public static readonly BindableProperty ProgressColorProperty =
      BindableProperty.Create(nameof(ProgressColor), typeof(Color), typeof(ProgressBar), Colors.CornflowerBlue, propertyChanged: OnBindablePropertyChanged);

   public static readonly BindableProperty BaseColorProperty =
      BindableProperty.Create(nameof(BaseColor), typeof(Color), typeof(ProgressBar), Colors.LightGray, propertyChanged: OnBindablePropertyChanged);

   public float Progress
   {
      get => (float)GetValue(ProgressProperty);
      set
      {
         if (Math.Abs(value - _previousProgress) >= _RedrawThreshold)
         {
            SetValue(ProgressProperty, value);
            _previousProgress = value;
         }
      }
   }
   public Color ProgressColor
   {
      get => (Color)GetValue(ProgressColorProperty);
      set => SetValue(ProgressColorProperty, value);
   }
   public Color BaseColor
   {
      get => (Color)GetValue(BaseColorProperty);
      set => SetValue(BaseColorProperty, value);
   }


   // actual canvas instance to draw on
   private SKCanvas _canvas;

   // rectangle which will be used to draw the Progress Bar
   private SKRect _drawRect;

   // holds information about the dimensions, etc.
   private SKImageInfo _info;

   private const float _RedrawThreshold = 0.5f; // 1% change
   private float _previousProgress = 0f;

   private SKPaint _basePaint = new()
   {
      Style = SKPaintStyle.Fill,
      IsAntialias = true,
   };

   private SKPaint _progressPaint = new()
   {
      Style = SKPaintStyle.Fill,
      IsAntialias = true,
   };



   protected override void OnPaintSurface (SKPaintSurfaceEventArgs e)
   {
      base.OnPaintSurface(e);

      _canvas = e.Surface.Canvas;
      _canvas.Clear();

      _info = e.Info;

      _drawRect = new SKRect(0, 0, _info.Width, _info.Height);

      DrawBase();
      DrawProgress();
   }

   private void DrawBase ()
   {
      using SKPath basePath = new();

      basePath.AddRect(_drawRect);

      _basePaint.Color = BaseColor.ToSKColor();

      _canvas.DrawPath(basePath, _basePaint);
   }

   private void DrawProgress ()
   {
      using SKPath progressPath = new();

      var progressRect = new SKRect(0, 0, _info.Width * Progress, _info.Height);

      progressPath.AddRect(progressRect);

      _progressPaint.Color = ProgressColor.ToSKColor();

      _canvas.DrawPath(progressPath, _progressPaint);
   }

   private static void OnBindablePropertyChanged (BindableObject bindable, object oldValue, object newValue)
   {
      if (oldValue != newValue)
         ((ProgressBar)bindable).InvalidateSurface();
   }
}
