using System;
using System.Collections.Generic;
using Controls.Core.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Maui.Controls.Handlers;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Hosting;

namespace Microsoft.Maui.Controls.Hosting
{
	public static class AppHostBuilderExtensions
	{
		static readonly Dictionary<Type, Type> DefaultMauiControlHandlers = new Dictionary<Type, Type>
		{
			{ typeof(NavigationPage), typeof(NavigationPageHandler) },
#if WINDOWS
			{ typeof(Shell), typeof(ShellHandler) },
#endif
			{ typeof(ActivityIndicator), typeof(ActivityIndicatorHandler) },
			{ typeof(Button), typeof(ButtonHandler) },
			{ typeof(CheckBox), typeof(CheckBoxHandler) },
			{ typeof(DatePicker), typeof(DatePickerHandler) },
			{ typeof(Editor), typeof(EditorHandler) },
			{ typeof(Entry), typeof(EntryHandler) },
			{ typeof(GraphicsView), typeof(GraphicsViewHandler) },
			{ typeof(Image), typeof(ImageHandler) },
			{ typeof(Label), typeof(Microsoft.Maui.Controls.Core.Handlers.LabelHandler) },
			{ typeof(Layout), typeof(LayoutHandler) },
			{ typeof(Layout2.Layout), typeof(LayoutHandler) },
			{ typeof(Picker), typeof(PickerHandler) },
			{ typeof(ProgressBar), typeof(ProgressBarHandler) },
			{ typeof(SearchBar), typeof(SearchBarHandler) },
			{ typeof(Slider), typeof(SliderHandler) },
			{ typeof(Stepper), typeof(StepperHandler) },
			{ typeof(Switch), typeof(SwitchHandler) },
			{ typeof(TimePicker), typeof(TimePickerHandler) },
			{ typeof(Page), typeof(PageHandler) }
		};

		public static IMauiHandlersCollection AddMauiControlsHandlers(this IMauiHandlersCollection handlersCollection)
			=> handlersCollection.AddHandlers(DefaultMauiControlHandlers);
	}
}
