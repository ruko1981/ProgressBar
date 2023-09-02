using SkiaSharp.Views.Maui.Handlers;

namespace ProgressBar.Maui;
public static class Registration
{
   public static MauiAppBuilder UseProgressBar(this MauiAppBuilder builder)
   {
      builder.ConfigureMauiHandlers(h =>
      {
         h.AddHandler<ProgressBar, SKCanvasViewHandler>();
      });

      return builder;
   }
}
