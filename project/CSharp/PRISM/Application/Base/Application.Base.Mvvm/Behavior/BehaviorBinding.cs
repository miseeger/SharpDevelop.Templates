using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace ${SolutionName}.Base.Mvvm.Behavior
{

	public class BehaviorBinding : Freezable
	{

		CommandBehaviorBinding behavior;
		internal CommandBehaviorBinding Behavior
		{
			get
			{
				if (behavior == null)
					behavior = new CommandBehaviorBinding();
				return behavior;
			}
		}


		DependencyObject owner;
		public DependencyObject Owner
		{
			get { return owner; }
			set
			{
				owner = value;
				ResetEventBinding();
			}
		}


		public static readonly DependencyProperty CommandProperty =
			DependencyProperty.Register(
				"Command", typeof(ICommand), typeof(BehaviorBinding),
				new FrameworkPropertyMetadata((ICommand)null,
				new PropertyChangedCallback(OnCommandChanged))
			);


		public ICommand Command
		{
			get { return (ICommand)GetValue(CommandProperty); }
			set { SetValue(CommandProperty, value); }
		}


		private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((BehaviorBinding)d).OnCommandChanged(e);
		}


		protected virtual void OnCommandChanged(DependencyPropertyChangedEventArgs e)
		{
			Behavior.Command = Command;
		}



		public static readonly DependencyProperty ActionProperty =
			DependencyProperty.Register(
				"Action", typeof(Action<object>), typeof(BehaviorBinding),
				new FrameworkPropertyMetadata((Action<object>)null,
				new PropertyChangedCallback(OnActionChanged))
			);


		public Action<object> Action
		{
			get { return (Action<object>)GetValue(ActionProperty); }
			set { SetValue(ActionProperty, value); }
		}


		private static void OnActionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((BehaviorBinding)d).OnActionChanged(e);
		}


		protected virtual void OnActionChanged(DependencyPropertyChangedEventArgs e)
		{
			Behavior.Action = Action;
		}


		public static readonly DependencyProperty CommandParameterProperty =
			DependencyProperty.Register(
				"CommandParameter", typeof(object), typeof(BehaviorBinding),
				new FrameworkPropertyMetadata((object)null,
				new PropertyChangedCallback(OnCommandParameterChanged)));


		public object CommandParameter
		{
			get { return (object)GetValue(CommandParameterProperty); }
			set { SetValue(CommandParameterProperty, value); }
		}


		private static void OnCommandParameterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((BehaviorBinding)d).OnCommandParameterChanged(e);
		}


		protected virtual void OnCommandParameterChanged(DependencyPropertyChangedEventArgs e)
		{
			Behavior.CommandParameter = CommandParameter;
		}


		public static readonly DependencyProperty EventProperty =
			DependencyProperty.Register(
				"Event", typeof(string), typeof(BehaviorBinding),
				new FrameworkPropertyMetadata((string)null,
				new PropertyChangedCallback(OnEventChanged)));


		public string Event
		{
			get { return (string)GetValue(EventProperty); }
			set { SetValue(EventProperty, value); }
		}


		private static void OnEventChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((BehaviorBinding)d).OnEventChanged(e);
		}


		protected virtual void OnEventChanged(DependencyPropertyChangedEventArgs e)
		{
			ResetEventBinding();
		}


		static void OwnerReset(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((BehaviorBinding)d).ResetEventBinding();
		}


		private void ResetEventBinding()
		{
			if (Owner != null)
			{
				if (Behavior.Event != null && Behavior.Owner != null)
					Behavior.Dispose();
				Behavior.BindEvent(Owner, Event);
			}
		}


		protected override Freezable CreateInstanceCore()
		{
			throw new NotImplementedException();
		}

	}

}
