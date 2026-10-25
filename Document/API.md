# ClamGauge API reference

## 📖 ClamGaugeView

`ClamGaugeView` derives from `SKCanvasView` and draws the largest circle that fits the view.  
Angles are degrees clockwise from 12 o'clock.  
Extents are fractions of the radius inside the bezels, that is half of the shorter side of the view minus `BezelOuterThickness` and `BezelInnerThickness`.  
Thicknesses, widths, the pivot radius and font sizes are DIP.  
Invalid values throw `ArgumentOutOfRangeException` when assigned.  
Angles and `ValueOffsetY` must be finite, font sizes must be positive and finite, and the other sizes, extents, intervals and `AnimationDuration` must be finite and zero or more.  

### 🧩 Properties

#### Value and scale

| Name | Type | Default | Bindable | Description |
|---|---|---|:-:|---|
| `Value` | `double` | `0` | ✓ | Value the needle points to; outside the scale the needle stops at its ends |
| `MinValue`, `MaxValue` | `double` | `0`, `100` | ✓ | Scale; nothing is drawn unless `MaxValue` is greater than `MinValue` |
| `StartAngle` | `double` | `225` | ✓ | Angle of `MinValue` |
| `SweepAngle` | `double` | `270` | ✓ | Angle from `MinValue` to `MaxValue`; `360` makes a full circle |
| `Ranges` | `IList<GaugeRange>` | empty `ObservableCollection<GaugeRange>` | ✓ | Colored segments drawn in order instead of the arc fill; add items as XAML elements or replace the list from code, a style setter or a binding, and an `INotifyCollectionChanged` list redraws on change |

#### Face

| Name | Type | Default | Bindable | Description |
|---|---|---|:-:|---|
| `GaugeBackgroundStartColor`, `GaugeBackgroundEndColor` | `Color` | `Transparent`, `Transparent` | ✓ | Fill of the dial inside the bezels; a transparent end color fills it with the start color alone, otherwise it is a gradient from the top left to the bottom right |
| `BezelOuterThickness` | `double` | `4` | ✓ | Ring at the edge of the dial; `0` hides it |
| `BezelOuterStartColor`, `BezelOuterEndColor` | `Color` | `#E8EEF8`, `#6878A0` | ✓ | Gradient of the outer ring from the top left to the bottom right |
| `BezelInnerThickness` | `double` | `2` | ✓ | Ring inside the outer one; `0` hides it |
| `BezelInnerStartColor`, `BezelInnerEndColor` | `Color` | `#4A5A88`, `#C0D0E8` | ✓ | Gradient of the inner ring from the top left to the bottom right |

#### Arc

| Name | Type | Default | Bindable | Description |
|---|---|---|:-:|---|
| `ArcInnerExtent`, `ArcOuterExtent` | `double` | `0.77`, `0.87` | ✓ | Inner and outer edges of the arc |
| `ArcTrackColor` | `Color` | `#20000000` | ✓ | Groove under the whole sweep |
| `ArcStartColor`, `ArcEndColor` | `Color` | `DodgerBlue`, `DodgerBlue` | ✓ | Gradient of the arc over the whole sweep, drawn when `Ranges` is empty |

#### Ticks and labels

| Name | Type | Default | Bindable | Description |
|---|---|---|:-:|---|
| `MajorTickInterval` | `double` | `10` | ✓ | Value between major ticks; `0` hides them |
| `MajorTickInnerExtent`, `MajorTickOuterExtent` | `double` | `0.68`, `0.76` | ✓ | Ends of the major ticks |
| `MajorTickThickness` | `double` | `2` | ✓ | Line width of the major ticks |
| `MajorTickColor` | `Color` | `Black` | ✓ | Color of the major ticks |
| `MinorTickInterval` | `double` | `2` | ✓ | Value between minor ticks; `0` hides them |
| `MinorTickInnerExtent`, `MinorTickOuterExtent` | `double` | `0.72`, `0.76` | ✓ | Ends of the minor ticks |
| `MinorTickThickness` | `double` | `1` | ✓ | Line width of the minor ticks |
| `MinorTickColor` | `Color` | `DimGray` | ✓ | Color of the minor ticks |
| `LabelInterval` | `double` | `20` | ✓ | Value between labels; `0` hides them |
| `LabelExtent` | `double` | `0.57` | ✓ | Center of the labels |
| `LabelFormat` | `string` | `{0:N0}` | ✓ | Format of the label value in the current culture: composite when it contains `{` (`{0:N0}%`), otherwise a format string (`N0`) |
| `LabelConverter` | `IValueConverter?` | `null` | ✓ | Converts the label value (`double`) to its text; used instead of `LabelFormat` |
| `LabelFormatter` | `Func<double, string>?` | `null` | | Label text from code; used before `LabelConverter` and `LabelFormat` |
| `LabelFontSize` | `double` | `12` | ✓ | Font size of the labels |
| `LabelColor` | `Color` | `Black` | ✓ | Color of the labels |

Ticks and labels start at `MinValue` and step by their interval; a last step that passes `MaxValue` only by rounding is drawn at `MaxValue`.  
Labels with empty text are skipped.  

#### Center value

| Name | Type | Default | Bindable | Description |
|---|---|---|:-:|---|
| `ValueVisible` | `bool` | `true` | ✓ | Draws the value in the dial |
| `ValueFormat` | `string` | `{0:N1}` | ✓ | Format of the value in the current culture: composite when it contains `{` (`{0:N1} km/h`), otherwise a format string (`N1`) |
| `ValueFontSize` | `double` | `28` | ✓ | Font size of the value |
| `ValueColor` | `Color` | `Black` | ✓ | Color of the value |
| `Unit` | `string?` | `null` | ✓ | Text next to the value; `null` or empty draws the value alone |
| `UnitFontSize` | `double` | `12` | ✓ | Font size of the unit |
| `UnitColor` | `Color` | `DimGray` | ✓ | Color of the unit |
| `UnitPosition` | `GaugeUnitPosition` | `Bottom` | ✓ | Unit on the right of the value or below it; the pair is centered |
| `ValueOffsetY` | `double` | `0.3` | ✓ | Center of the value below the center of the dial as a fraction of the radius |

#### Font

| Name | Type | Default | Bindable | Description |
|---|---|---|:-:|---|
| `FontFamily` | `string` | `sans-serif` | ✓ | Primary font of the labels, the value and the unit; missing characters are resolved through `FallbackFonts` of SkiaExtension |

#### Needle

| Name | Type | Default | Bindable | Description |
|---|---|---|:-:|---|
| `NeedleEndExtent` | `double` | `0.75` | ✓ | Length of the needle from the center |
| `NeedleStartWidth`, `NeedleEndWidth` | `double` | `10`, `2` | ✓ | Width at the center and at the tip |
| `NeedleLeftColor`, `NeedleRightColor` | `Color` | `#CC2828`, `#EE3C3C` | ✓ | Halves on the left and the right of the needle axis |
| `PivotRadius` | `double` | `8` | ✓ | Round cap over the center |
| `PivotColor` | `Color` | `#CC2828` | ✓ | Color of the cap, with a lighter highlight toward the top left |
| `AnimationDuration` | `int` | `300` | ✓ | Milliseconds the needle takes to reach a new value; `0` moves it at once |
| `AnimationEasing` | `Easing` | `CubicOut` | ✓ | Easing of the needle animation |

The first value and values set while the view has no handler are shown at once.  
A new value during an animation starts a new animation from the current position.  
The center value shows the animated value as it is, while the needle stops at the ends of the scale.  

### 🛠 Methods

| Name | Returns | Description |
|---|---|---|
| `InvalidateSurface()` | `void` | Redraws the gauge |
| `Dispose()` | `void` | Stops the needle animation and releases the `Ranges` subscription and native resources |

### 📣 Events

`PaintSurface` of `SKCanvasView` is raised after the gauge is drawn, with the canvas in pixels.  

## 🎨 GaugeRange

A segment of the arc.  
It does not notify changes, so replace the item or the list to redraw.  

| Name | Type | Default | Description |
|---|---|---|---|
| `StartValue`, `EndValue` | `double` | `0` | Values of the segment, clamped to the scale; nothing is drawn unless the start is below the end |
| `StartColor` | `Color` | `Transparent` | Color at `StartValue` |
| `EndColor` | `Color?` | `null` | Color at `EndValue`; `null` paints the whole segment with `StartColor` |

## 🧭 GaugeUnitPosition

| Value | Description |
|---|---|
| `Right` | Unit after the value on the same line |
| `Bottom` | Unit centered below the value |

## 🔤 Fonts

Per character font fallback comes from [SkiaExtension](https://github.com/usausa/skia-extension).  
Configure it with `FallbackFonts` in the `SkiaExtension` namespace at startup, before the first view is shown.  
`Languages` defaults to the UI language of the device.  

```csharp
FallbackFonts.Languages = ["ja", "en"];
FallbackFonts.Typefaces = [SKTypeface.FromStream(await FileSystem.OpenAppPackageFileAsync("NotoSansJP-Regular.ttf"))];
```
