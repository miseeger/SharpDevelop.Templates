using System;
using System.Collections.Specialized;
using System.Windows;

using Microsoft.Practices.Prism.Regions;
using Fluent;

namespace ${SolutionName}.Base.Navigation
{

	public class RibbonBackstageRegionAdapter : RegionAdapterBase<BackstageTabControl>
    {

		public RibbonBackstageRegionAdapter(IRegionBehaviorFactory behaviorFactory)
			: base(behaviorFactory)
        {
        }

  		
		protected override void Adapt(IRegion region, BackstageTabControl regionTarget)
        {
 			if (region == null) throw new ArgumentNullException("Region");
            if (regionTarget == null) throw new ArgumentNullException("RegionTarget");

            region.ActiveViews.CollectionChanged += (s, args) =>
            {
                switch (args.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        {
                            foreach (Object view in args.NewItems)
                            {
                                AddViewToRegion(view, regionTarget);
                            }
                            break;
                        }
                    case NotifyCollectionChangedAction.Remove:
                        {
                            foreach (Object view in args.OldItems)
                            {
                                RemoveViewFromRegion(view, regionTarget);
                            }
                            break;
                        }
                    default:
                        {
                            // Do nothing.
                            break;
                        }
                }
            };
  		}


  		protected override IRegion CreateRegion()
        {
            return new AllActiveRegion();
        }
  		
  		
        static void AddViewToRegion(Object view, BackstageTabControl backstageTabControl)
        {
            if (backstageTabControl != null)
                backstageTabControl.Items.Add(view);
        }

        static void RemoveViewFromRegion(Object view, BackstageTabControl backstageTabControl)
        {
            if (backstageTabControl != null)
                backstageTabControl.Items.Remove(view);
        }

	}

}