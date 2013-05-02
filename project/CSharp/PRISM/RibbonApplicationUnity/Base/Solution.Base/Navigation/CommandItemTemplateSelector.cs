using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using ${SolutionName}.Base.Enums;
using ${SolutionName}.Base.Navigation;

namespace ${SolutionName}.Base.Navigation
{
    public class CommandItemTemplateSelector: DataTemplateSelector
    {

        public override System.Windows.DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            if (item != null && item is CommandItem)
            {
                CommandItem c = (CommandItem) item;
                if (c != null)
                {
					FrameworkElement frameworkElement = (FrameworkElement) container;
					if (frameworkElement != null)
					{
						if (c.Type == CommandType.Navigation)
                		{
							return (c.IconSize.ToLower() == "large")
								? (DataTemplate)frameworkElement.FindResource("NavLargeTemplate") 
								: (DataTemplate)frameworkElement.FindResource("NavMiddleTemplate");
                		}
						else
						{
							return (c.IconSize.ToLower() == "large")
								? (DataTemplate)frameworkElement.FindResource("CmdLargeTemplate") 
								: (DataTemplate)frameworkElement.FindResource("CmdMiddleTemplate");
						}
                    }
                }
            }

            return null;
        }
    }
}
