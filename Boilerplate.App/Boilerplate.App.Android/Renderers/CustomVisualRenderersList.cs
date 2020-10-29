using Boilerplate.App.CustomControls;
using Xamarin.Forms;
using Xamarin.Forms.Material.Android;

[assembly: ExportRenderer(typeof(Entry), typeof(MaterialEntryRenderer), new[] { typeof(CustomVisual) })]
[assembly: ExportRenderer(typeof(Editor), typeof(MaterialEditorRenderer), new[] { typeof(CustomVisual) })]
[assembly: ExportRenderer(typeof(Picker), typeof(MaterialPickerRenderer), new[] { typeof(CustomVisual) })]
[assembly: ExportRenderer(typeof(ActivityIndicator), typeof(MaterialActivityIndicatorRenderer), new[] { typeof(CustomVisual) })]
[assembly: ExportRenderer(typeof(Frame), typeof(MaterialFrameRenderer), new[] {typeof(CustomVisual)})]
[assembly: ExportRenderer(typeof(Slider), typeof(MaterialSliderRenderer), new[] { typeof(CustomVisual) })]
[assembly: ExportRenderer(typeof(ProgressBar), typeof(MaterialProgressBarRenderer), new[] { typeof(CustomVisual) })]
[assembly: ExportRenderer(typeof(Stepper), typeof(MaterialStepperRenderer), new[] { typeof(CustomVisual) })]
[assembly: ExportRenderer(typeof(DatePicker), typeof(MaterialDatePickerRenderer), new[] { typeof(CustomVisual) })]
[assembly: ExportRenderer(typeof(TimePicker), typeof(MaterialTimePickerRenderer), new[] { typeof(CustomVisual) })]