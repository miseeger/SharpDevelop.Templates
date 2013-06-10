using System.Windows;
using System.Windows.Controls;
using ${SolutionName}.Base.Data.Model.System;
using ${SolutionName}.Base.Data.Enums.System;

namespace ${SolutionName}.Base.Navigation
{
    public class CommandItemTemplateSelector: DataTemplateSelector
    {

        public override System.Windows.DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            if (item != null && item is CommandItem)
            {
                var c = (CommandItem) item;
                if (c != null)
                {
					var frameworkElement = (FrameworkElement) container;
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
