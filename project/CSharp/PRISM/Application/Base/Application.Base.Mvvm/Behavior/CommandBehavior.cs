using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Markup;
using System.Windows;
using System.Windows.Input;

namespace ${SolutionName}.Base.Mvvm.Behavior
{

	public class CommandBehavior
	{

		private static readonly DependencyProperty BehaviorProperty =
			DependencyProperty.RegisterAttached(
				"Behavior", typeof(CommandBehaviorBinding), typeof(CommandBehavior),
				new FrameworkPropertyMetadata((CommandBehaviorBinding)null)
			);


		private static CommandBehaviorBinding GetBehavior(DependencyObject d)
		{
			return (CommandBehaviorBinding)d.GetValue(BehaviorProperty);
		}


		private static void SetBehavior(DependencyObject d, CommandBehaviorBinding value)
		{
			d.SetValue(BehaviorProperty, value);
		}


		public static readonly DependencyProperty CommandProperty =
			DependencyProperty.RegisterAttached(
				"Command", typeof(ICommand), typeof(CommandBehavior),
				new FrameworkPropertyMetadata((ICommand)null,
				new PropertyChangedCallback(OnCommandChanged)));


		public static ICommand GetCommand(DependencyObject d)
		{
			return (ICommand)d.GetValue(CommandProperty);
		}


		public static void SetCommand(DependencyObject d, ICommand value)
		{
			d.SetValue(CommandProperty, value);
		}


		private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			CommandBehaviorBinding binding = FetchOrCreateBinding(d);
			binding.Command = (ICommand)e.NewValue;
		}


		public static readonly DependencyProperty ActionProperty =
			DependencyProperty.RegisterAttached(
				"Action", typeof(Action<object>), typeof(CommandBehavior),
				new FrameworkPropertyMetadata((Action<object>)null,
				new PropertyChangedCallback(OnActionChanged))
			);


		public static Action<object> GetAction(DependencyObject d)
		{
			return (Action<object>)d.GetValue(ActionProperty);
		}


		public static void SetAction(DependencyObject d, Action<object> value)
		{
			d.SetValue(ActionProperty, value);
		}


		private static void OnActionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			CommandBehaviorBinding binding = FetchOrCreateBinding(d);
			binding.Action = (Action<object>)e.NewValue;
		}


		public static readonly DependencyProperty CommandParameterProperty =
			DependencyProperty.RegisterAttached(
				"CommandParameter", typeof(object), typeof(CommandBehavior),
				new FrameworkPropertyMetadata((object)null,
				new PropertyChangedCallback(OnCommandParameterChanged))
			);


		public static object GetCommandParameter(DependencyObject d)
		{
			return (object)d.GetValue(CommandParameterProperty);
		}


		public static void SetCommandParameter(DependencyObject d, object value)
		{
			d.SetValue(CommandParameterProperty, value);
		}


		private static void OnCommandParameterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			CommandBehaviorBinding binding = FetchOrCreateBinding(d);
			binding.CommandParameter = e.NewValue;
		}


		public static readonly DependencyProperty EventProperty =
			DependencyProperty.RegisterAttached(
				"Event", typeof(string), typeof(CommandBehavior),
				new FrameworkPropertyMetadata((string)String.Empty,
				new PropertyChangedCallback(OnEventChanged))
			);


		public static string GetEvent(DependencyObject d)
		{
			return (string)d.GetValue(EventProperty);
		}


		public static void SetEvent(DependencyObject d, string value)
		{
			d.SetValue(EventProperty, value);
		}


		private static void OnEventChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			CommandBehaviorBinding binding = FetchOrCreateBinding(d);

			if (binding.Event != null && binding.Owner != null)
				binding.Dispose();

			binding.BindEvent(d, e.NewValue.ToString());
		}


		private static CommandBehaviorBinding FetchOrCreateBinding(DependencyObject d)
		{
			CommandBehaviorBinding binding = CommandBehavior.GetBehavior(d);
			if (binding == null)
			{
				binding = new CommandBehaviorBinding();
				CommandBehavior.SetBehavior(d, binding);
			}

			return binding;
		}

	}

}
