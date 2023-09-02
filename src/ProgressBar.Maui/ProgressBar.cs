using SkiaSharp;
using SkiaSharp.Views.Maui;
using SkiaSharp.Views.Maui.Controls;

namespace ProgressBar.Maui;

// All the code in this file is included in all platforms.
public class ProgressBar : SKCanvasView
{
   // actual canvas instance to draw on
   private SKCanvas _canvas;

   // rectangle which will be used to draw the Progress Bar
   private SKRect _drawRect;

   // holds information about the dimensions, etc.
   private SKImageInfo _info;
   private float progress;
   private static object progressColor;

   public float Progress
   {
      get => (float)GetValue(ProgressProperty);
      set => SetValue(ProgressProperty, value);
   }
   public Color ProgressColor
   {
      get => (Color)GetValue(ProgressColorPorperty);
      set => SetValue(ProgressColorPorperty, value);
   }
   public Color BaseColor
   {
      get => (Color)GetValue(BaseColorProperty);
      set => SetValue(BaseColorProperty, value);
   }

   public static readonly BindableProperty ProgressProperty =
      BindableProperty.Create(nameof(Progress), typeof(float), typeof(ProgressBar), 0f, propertyChanged: OnBindablePropertyChanged);


   public static readonly BindableProperty ProgressColorPorperty =
      BindableProperty.Create(nameof(ProgressColor), typeof(Color), typeof(ProgressBar), Colors.CornflowerBlue, propertyChanged: OnBindablePropertyChanged);

   public static readonly BindableProperty BaseColorProperty =
      BindableProperty.Create(nameof(BaseColor), typeof(Color), typeof(ProgressBar), Colors.LightGray, propertyChanged: OnBindablePropertyChanged);


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
      using SKPath basePath = new ();

      basePath.AddRect(_drawRect);

      _canvas.DrawPath(basePath, new()
      {
         Style = SKPaintStyle.Fill,
         Color = BaseColor.ToSKColor(),
         IsAntialias = true,
      });
   }

   private void DrawProgress ()
   {
      using SKPath progressPath = new ();

      var progressRect = new SKRect(0, 0, _info.Width * Progress, _info.Height);

      progressPath.AddRect(progressRect);

      _canvas.DrawPath(progressPath, new()
      {
         Style = SKPaintStyle.Fill,
         Color = ProgressColor.ToSKColor(),
         IsAntialias = true,
      });
   }
   
   private static void OnBindablePropertyChanged (BindableObject bindable, object oldValue, object newValue) 
      => ((ProgressBar)bindable).InvalidateSurface();
}
