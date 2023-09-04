using SkiaSharp;
using SkiaSharp.Views.Maui;
using SkiaSharp.Views.Maui.Controls;
using System.Diagnostics;

namespace ProgressBar.Maui;

// All the code in this file is included in all platforms.
public class ProgressBar : SKCanvasView
{
   public static readonly BindableProperty ProgressProperty = BindableProperty.Create(nameof(Progress), typeof(float), typeof(ProgressBar), 0.0f, propertyChanged: OnProgressPropertyChanged, coerceValue: ClampProgressValue);
   public static readonly BindableProperty BaseColorProperty = BindableProperty.Create(nameof(BaseColor), typeof(Color), typeof(ProgressBar), Colors.LightGray, propertyChanged: OnColorPropertyChanged);
   public static readonly BindableProperty ProgressColorProperty = BindableProperty.Create(nameof(ProgressColor), typeof(Color), typeof(ProgressBar), Colors.CornflowerBlue, propertyChanged: OnColorPropertyChanged);
   public static readonly BindableProperty GradientColorProperty = BindableProperty.Create(nameof(GradientColor), typeof(Color), typeof(ProgressBar), Colors.CornflowerBlue.WithLuminosity(0.5f), propertyChanged: OnColorPropertyChanged);
   public static readonly BindableProperty UseGradientProperty = BindableProperty.Create(nameof(UseGradient), typeof(bool), typeof(ProgressBar), false, propertyChanged: OnColorPropertyChanged);

   public float Progress
   {
      get => (float)GetValue(ProgressProperty);
      set => SetValue(ProgressProperty, value);
   }
   public Color BaseColor
   {
      get => (Color)GetValue(BaseColorProperty);
      set => SetValue(BaseColorProperty, value);
   }
   public Color ProgressColor
   {
      get => (Color)GetValue(ProgressColorProperty);
      set => SetValue(ProgressColorProperty, value);
   }
   public Color GradientColor
   {
      get => (Color)GetValue(GradientColorProperty);
      set => SetValue(GradientColorProperty, value);
   }
   public bool UseGradient
   {
      get => (bool)GetValue(UseGradientProperty);
      set => SetValue(UseGradientProperty, value);
   }


   // actual canvas instance to draw on
   private SKCanvas _canvas;

   // rectangle which will be used to draw the Progress Bar
   private SKRect _drawRect;

   // holds information about the dimensions, etc.
   private SKImageInfo _info;

   private const float _RedrawThreshold = 0.1f; // 1% change
   private float _displayedProgress = 0f;

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

      if (UseGradient)
      {
         _progressPaint.Shader = SKShader.CreateLinearGradient(
            new SKPoint(_drawRect.Left, _drawRect.MidY),
            new SKPoint(_drawRect.Right, _drawRect.MidY),
            new SKColor[] { ProgressColor.ToSKColor(), GradientColor.ToSKColor() },
            SKShaderTileMode.Clamp);
      }
      else
      {
         _progressPaint.Color = ProgressColor.ToSKColor();
      }

      _canvas.DrawPath(progressPath, _progressPaint);
   }

   private static void OnProgressPropertyChanged (BindableObject bindable, object oldValue, object newValue)
   {
      if ((float)oldValue == (float)newValue)
         return;

      if ((float)newValue - ((ProgressBar)bindable)._displayedProgress >= _RedrawThreshold || ((float)newValue >= 1.0f))
         ((ProgressBar)bindable).Update();
   }

   private static void OnColorPropertyChanged (BindableObject bindable, object oldValue, object newValue)
   {
      if (oldValue != newValue)
         ((ProgressBar)bindable).InvalidateSurface();
   }

   public void Update ()
   {
      if (_displayedProgress != Progress)
      {
         _displayedProgress = Progress;
         InvalidateSurface();
      }
   }


   private static object ClampProgressValue (BindableObject bindable, object value)
   {
      return (float)value switch
      {
         < 0 => 0.0f,
         > 1 => 1.0f,
         _ => (float)value,
      };
   }
}
