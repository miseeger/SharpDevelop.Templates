using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows;
using System.ComponentModel;
using System.Windows.Markup;
using System.Diagnostics;
using System.Collections;
using System.Collections.Specialized;

namespace ${SolutionName}.Base.Mvvm.Behavior
{

	public class CommandBehaviorCollection
	{

		private static readonly DependencyPropertyKey BehaviorsPropertyKey
			= DependencyProperty.RegisterAttachedReadOnly(
				"BehaviorsInternal", typeof(BehaviorBindingCollection), typeof(CommandBehaviorCollection),
				new FrameworkPropertyMetadata((BehaviorBindingCollection)null)
			);


		public static readonly DependencyProperty BehaviorsProperty
			= BehaviorsPropertyKey.DependencyProperty;


		public static BehaviorBindingCollection GetBehaviors(DependencyObject d)
		{
			if (d == null)
				throw new InvalidOperationException("The dependency object trying to attach to is set to null");

			BehaviorBindingCollection collection = d.GetValue(CommandBehaviorCollection.BehaviorsProperty) as BehaviorBindingCollection;
			if (collection == null)
			{
				collection = new BehaviorBindingCollection();
				collection.Owner = d;
				SetBehaviors(d, collection);
			}

			return collection;
		}


		private static void SetBehaviors(DependencyObject d, BehaviorBindingCollection value)
		{
			d.SetValue(BehaviorsPropertyKey, value);
			INotifyCollectionChanged collection = (INotifyCollectionChanged)value;
			collection.CollectionChanged += new NotifyCollectionChangedEventHandler(CollectionChanged);
		}


		static void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			BehaviorBindingCollection sourceCollection = (BehaviorBindingCollection)sender;

			switch (e.Action)
			{

				case NotifyCollectionChangedAction.Add:
					if (e.NewItems != null)
						foreach (BehaviorBinding item in e.NewItems)
							item.Owner = sourceCollection.Owner;
					break;

				case NotifyCollectionChangedAction.Remove:
					if (e.OldItems != null)
						foreach (BehaviorBinding item in e.OldItems)
							item.Behavior.Dispose();
					break;

				case NotifyCollectionChangedAction.Replace:
					if (e.NewItems != null)
						foreach (BehaviorBinding item in e.NewItems)
							item.Owner = sourceCollection.Owner;

					if (e.OldItems != null)
						foreach (BehaviorBinding item in e.OldItems)
							item.Behavior.Dispose();
					break;

				case NotifyCollectionChangedAction.Reset:
					if (e.OldItems != null)
						foreach (BehaviorBinding item in e.OldItems)
							item.Behavior.Dispose();
					break;

				case NotifyCollectionChangedAction.Move:

				default:
					break;
			}
		}

	}


	public class BehaviorBindingCollection : FreezableCollection<BehaviorBinding>
	{
		public DependencyObject Owner { get; set; }
	}

}