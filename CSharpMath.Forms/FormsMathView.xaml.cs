﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace CSharpMath.Forms {
  using Rendering;
  [XamlCompilation(XamlCompilationOptions.Compile), ContentProperty(nameof(LaTeX))]
  public partial class FormsMathView : SKCanvasView {
    public FormsMathView() {
      InitializeComponent();
      painter = new SkiaSharp.SkiaMathPainter(CanvasSize);
      var pinch = new PinchGestureRecognizer();
      pinch.PinchUpdated += OnPinch;
      GestureRecognizers.Add(pinch);
    }

    protected SkiaSharp.SkiaMathPainter painter;
    protected override void OnPaintSurface(SKPaintSurfaceEventArgs e) {
      base.OnPaintSurface(e);
      e.Surface.Canvas.Clear();
      painter.Bounds = CanvasSize;
      painter.Draw(e.Surface.Canvas);
      ScrollX = painter.TranslationX;
      ScrollY = painter.TranslationY;
      var sz = painter.DrawingSize;
      if (sz.HasValue) LayoutBounds = new Rectangle(X, Y, sz.Value.Width, sz.Value.Height);
    }

    #region BindableProperties
    static FormsMathView() {
      var painter = new SkiaSharp.SkiaMathPainter(default);
      var thisType = typeof(FormsMathView);
      SkiaSharp.SkiaMathPainter p(BindableObject b) => ((FormsMathView)b).painter;
      SourceProperty = BindableProperty.Create(nameof(Source), typeof(MathSource), thisType, painter.Source, BindingMode.TwoWay, null, (b, o, n) => { p(b).Source = (MathSource)n; ((FormsMathView)b).ErrorMessage = p(b).ErrorMessage; });
      DisplayErrorInlineProperty = BindableProperty.Create(nameof(DisplayErrorInline), typeof(bool), thisType, painter.DisplayErrorInline, propertyChanged: (b, o, n) => p(b).DisplayErrorInline = (bool)n);
      FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(float), thisType, painter.FontSize, propertyChanged: (b, o, n) => p(b).FontSize = (float)n);
      ErrorFontSizeProperty = BindableProperty.Create(nameof(ErrorFontSize), typeof(float?), thisType, painter.ErrorFontSize, propertyChanged: (b, o, n) => p(b).ErrorFontSize = (float)n);
      TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), thisType, painter.TextColor.ToFormsColor(), propertyChanged: (b, o, n) => p(b).TextColor = ((Color)n).ToSKColor());
      ErrorColorProperty = BindableProperty.Create(nameof(ErrorColor), typeof(Color), thisType, painter.ErrorColor.ToFormsColor(), propertyChanged: (b, o, n) => p(b).ErrorColor = ((Color)n).ToSKColor());
      TextAlignmentProperty = BindableProperty.Create(nameof(TextAlignment), typeof(TextAlignment), thisType, painter.TextAlignment, propertyChanged: (b, o, n) => p(b).TextAlignment = (TextAlignment)n);
      ScrollXProperty = BindableProperty.Create(nameof(ScrollX), typeof(float), thisType, painter.TranslationX, BindingMode.TwoWay, propertyChanged: (b, o, n) => p(b).TranslationX = (float)n);
      ScrollYProperty = BindableProperty.Create(nameof(ScrollY), typeof(float), thisType, painter.TranslationY, BindingMode.TwoWay, propertyChanged: (b, o, n) => p(b).TranslationY = (float)n);
      MagnificationProperty = BindableProperty.Create(nameof(Magnification), typeof(float), thisType, painter.Magnification, propertyChanged: (b, o, n) => p(b).Magnification = (float)n);
      PaintStyleProperty = BindableProperty.Create(nameof(PaintStyle), typeof(PaintStyle), thisType, painter.PaintStyle, propertyChanged: (b, o, n) => p(b).PaintStyle = (PaintStyle)n);
      LineStyleProperty = BindableProperty.Create(nameof(LineStyle), typeof(Enumerations.LineStyle), thisType, painter.LineStyle, propertyChanged: (b, o, n) => p(b).LineStyle = (Enumerations.LineStyle)n);
      GlyphBoxColorProperty = BindableProperty.Create(nameof(GlyphBoxColor), typeof((Color glyph, Color textRun)?), thisType, painter.GlyphBoxColor.HasValue ? (painter.GlyphBoxColor.Value.glyph.ToNative(), painter.GlyphBoxColor.Value.textRun.ToNative()) : default((Color glyph, Color textRun)?), propertyChanged: (b, o, n) => p(b).GlyphBoxColor = n != null ? ((((Color glyph, Color textRun)?)n).Value.glyph.FromNative(), (((Color glyph, Color textRun)?)n).Value.textRun.FromNative()) : default((Structures.Color glyph, Structures.Color textRun)?));
      PaddingProperty = BindableProperty.Create(nameof(Padding), typeof(Thickness), thisType, new Thickness(painter.Padding.Left, painter.Padding.Top, painter.Padding.Right, painter.Padding.Bottom), propertyChanged: (b, o, n) => p(b).Padding = new Thickness(((Thickness)n).Left, ((Thickness)n).Top, ((Thickness)n).Right, ((Thickness)n).Bottom));
      StrokeCapProperty = BindableProperty.Create(nameof(StrokeCap), typeof(SKStrokeCap), thisType, painter.StrokeCap, propertyChanged: (b, o, n) => p(b).StrokeCap = (SKStrokeCap)n);
      EnableGesturesProperty = BindableProperty.Create(nameof(EnableGestures), typeof(bool), thisType, false);
      ErrorMessagePropertyKey = BindableProperty.CreateReadOnly(nameof(ErrorMessage), typeof(string), thisType, painter.ErrorMessage, BindingMode.OneWayToSource);
      ErrorMessageProperty = ErrorMessagePropertyKey.BindableProperty;
      GestureCountPropertyKey = BindableProperty.CreateReadOnly(nameof(GestureCount), typeof(int), thisType, 0, BindingMode.OneWayToSource);
      GestureCountProperty = GestureCountPropertyKey.BindableProperty;
      LayoutBoundsPropertyKey = BindableProperty.CreateReadOnly(nameof(LayoutBounds), typeof(Rectangle), thisType, new Rectangle(), BindingMode.OneWayToSource);
      LayoutBoundsProperty = LayoutBoundsPropertyKey.BindableProperty;
    }
    public static readonly BindableProperty DisplayErrorInlineProperty;
    public static readonly BindableProperty FontSizeProperty;
    public static readonly BindableProperty ErrorFontSizeProperty;
    public static readonly BindableProperty TextColorProperty;
    public static readonly BindableProperty ErrorColorProperty;
    public static readonly BindableProperty TextAlignmentProperty;
    public static readonly BindableProperty ScrollXProperty;
    public static readonly BindableProperty ScrollYProperty;
    public static readonly BindableProperty MagnificationProperty;
    public static readonly BindableProperty PaintStyleProperty;
    public static readonly BindableProperty LineStyleProperty;
    public static readonly BindableProperty GlyphBoxColorProperty;
    public static readonly BindableProperty PaddingProperty;
    public static readonly BindableProperty SourceProperty;
    public static readonly BindableProperty StrokeCapProperty;
    public static readonly BindableProperty EnableGesturesProperty;
    private static readonly BindablePropertyKey ErrorMessagePropertyKey;
    public static readonly BindableProperty ErrorMessageProperty;
    private static readonly BindablePropertyKey GestureCountPropertyKey;
    public static readonly BindableProperty GestureCountProperty;
    private static readonly BindablePropertyKey LayoutBoundsPropertyKey;
    public static readonly BindableProperty LayoutBoundsProperty;
    #endregion

    SKPoint _origin;
    protected override void OnTouch(SKTouchEventArgs e) {
      if (e.InContact) {
        switch (e.ActionType) {
          case SKTouchAction.Entered:
            break;
          case SKTouchAction.Pressed:
            _origin = e.Location;
            e.Handled = true;
            break;
          case SKTouchAction.Moved:
            var displacement = e.Location - _origin;
            painter.TranslationX += displacement.X;
            painter.TranslationY += displacement.Y;
            _origin = e.Location;
            InvalidateSurface();
            e.Handled = true;
            break;
          case SKTouchAction.Released:
            _origin = e.Location;
            e.Handled = true;
            break;
          case SKTouchAction.Cancelled:
            break;
          case SKTouchAction.Exited:
            break;
          default:
            break;
        }
      }
      base.OnTouch(e);
    }
    protected virtual void OnPinch(object sender, PinchGestureUpdatedEventArgs e) {
      if (EnableGestures && painter.LaTeX.IsNonEmpty()) {
        switch (e.Status) {
          case GestureStatus.Started:
            GestureCount++;
            break;
          case GestureStatus.Running:
            Magnification *= (float)e.Scale;
            InvalidateSurface();
            break;
          case GestureStatus.Completed:
          case GestureStatus.Canceled:
            GestureCount--;
            break;
          default:
            throw new NotImplementedException("A new GestureStatus is in Xamarin.Forms?");
        }
      }
    }

    public bool EnableGestures { get => (bool)GetValue(EnableGesturesProperty); set => SetValue(EnableGesturesProperty, value); }
    public int GestureCount { get => (int)GetValue(GestureCountProperty); private set => SetValue(GestureCountPropertyKey, value); }
    public MathSource Source { get => (MathSource)GetValue(SourceProperty); set => SetValue(SourceProperty, value); }
    public Interfaces.IMathList MathList { get => Source.MathList; set => Source = new MathSource(value); }
    public string LaTeX { get => Source.LaTeX; set => Source = new MathSource(value); }
    public bool DisplayErrorInline { get => (bool)GetValue(DisplayErrorInlineProperty); set => SetValue(DisplayErrorInlineProperty, value); }
    /// <summary>
    /// Unit of measure: points
    /// </summary>
    public float FontSize { get => (float)GetValue(FontSizeProperty); set => SetValue(FontSizeProperty, value); }
    /// <summary>
    /// Unit of measure: points;
    /// Defaults to <see cref="FontSize"/>.
    /// </summary>
    public float? ErrorFontSize { get => (float?)GetValue(ErrorFontSizeProperty); set => SetValue(ErrorFontSizeProperty, value); }
    public Color TextColor { get => (Color)GetValue(TextColorProperty); set => SetValue(TextColorProperty, value); }
    public Color ErrorColor { get => (Color)GetValue(ErrorColorProperty); set => SetValue(ErrorColorProperty, value); }
    public TextAlignment TextAlignment { get => (TextAlignment)GetValue(TextAlignmentProperty); set => SetValue(TextAlignmentProperty, value); }
    public float ScrollX { get => (float)GetValue(ScrollXProperty); set => SetValue(ScrollXProperty, value); }
    public float ScrollY { get => (float)GetValue(ScrollYProperty); set => SetValue(ScrollYProperty, value); }
    public float Magnification { get => (float)GetValue(MagnificationProperty); set => SetValue(MagnificationProperty, value); }
    public PaintStyle PaintStyle { get => (PaintStyle)GetValue(PaintStyleProperty); set => SetValue(PaintStyleProperty, value); }
    public Enumerations.LineStyle LineStyle { get => (Enumerations.LineStyle)GetValue(LineStyleProperty); set => SetValue(LineStyleProperty, value); }
    public (Color glyph, Color textRun)? GlyphBoxColor { get => ((Color glyph, Color textRun)?)GetValue(GlyphBoxColorProperty); set => SetValue(GlyphBoxColorProperty, value); }
    public Thickness Padding { get => (Thickness)GetValue(PaddingProperty); set => SetValue(PaddingProperty, value); }
    public string ErrorMessage { get => (string)GetValue(ErrorMessageProperty); private set => SetValue(ErrorMessagePropertyKey, value); }
    public SKStrokeCap StrokeCap { get => (SKStrokeCap)GetValue(StrokeCapProperty); set => SetValue(StrokeCapProperty, value); }

    public System.Drawing.SizeF? DrawingSize => painter.DrawingSize;
    public Rectangle LayoutBounds { get => (Rectangle)GetValue(LayoutBoundsProperty); private set => SetValue(LayoutBoundsPropertyKey, value); }
  }
}