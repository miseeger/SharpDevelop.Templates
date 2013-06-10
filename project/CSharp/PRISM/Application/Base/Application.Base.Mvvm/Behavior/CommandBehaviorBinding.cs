using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Reflection;
using System.Windows;

namespace ${SolutionName}.Base.Mvvm.Behavior
{


	public class CommandBehaviorBinding : IDisposable
	{

		public DependencyObject Owner { get; private set; }
		public string EventName { get; private set; }
		public EventInfo Event { get; private set; }
		public Delegate EventHandler { get; private set; }

		IExecutionStrategy strategy;
		public object CommandParameter { get; set; }


		ICommand command;
		public ICommand Command
		{
			get { return command; }
			set
			{
				command = value;
				strategy = new CommandExecutionStrategy { Behavior = this };
			}
		}


		Action<object> action;
		public Action<object> Action
		{
			get { return action; }
			set
			{
				action = value;
				strategy = new ActionExecutionStrategy { Behavior = this };
			}
		}


		public void BindEvent(DependencyObject owner, string eventName)
		{
			EventName = eventName;
			Owner = owner;
			Event = Owner.GetType().GetEvent(EventName, BindingFlags.Public | BindingFlags.Instance);

			if (Event == null)
				throw new InvalidOperationException(String.Format("Could not resolve event name {0}", EventName));

			EventHandler = EventHandlerGenerator.CreateDelegate(
				Event.EventHandlerType, typeof(CommandBehaviorBinding).GetMethod("Execute", BindingFlags.Public | BindingFlags.Instance), this);

			Event.AddEventHandler(Owner, EventHandler);
		}


		public void Execute()
		{
			if (strategy != null)
				strategy.Execute(CommandParameter);
		}


		bool disposed = false;

		public void Dispose()
		{
			if (!disposed)
			{
				Event.RemoveEventHandler(Owner, EventHandler);
				disposed = true;
			}
		}

	}

}
