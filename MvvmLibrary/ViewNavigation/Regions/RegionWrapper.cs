using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MvvmLibrary.ViewModels;
using MvvmLibrary.Views;

namespace MvvmLibrary.ViewNavigation.Regions
{
	public class RegionWrapper
	{
		internal Dictionary<Type, Type> ViewModelToViewMap { get; set; }
		internal Region Region { get; set; }
		private readonly Stack<BaseView> views = new Stack<BaseView>();

		public RegionWrapper() { }

		internal void Initialization()
		{
			if (views.Count != 0)
			{
				Region.Content = views.Peek();
				Region.DataContext = views.Peek().ViewModel;
			}
		}
		

		public void NavigateTo(BaseVM viewModel, Func<BaseVM, bool> afterViewClosed = null)
		{
			if (afterViewClosed != null)
				viewModel.AfterViewClosed = afterViewClosed;

			if (ViewModelToViewMap.TryGetValue(viewModel.GetType(), out Type viewType))
			{
				var view = Activator.CreateInstance(viewType) as BaseView;
				view.ViewModel = viewModel;

				views.Push(view);

				if (Region != null)
				{
					Region.Content = view;
					Region.DataContext = viewModel;
				}
			}
		}

		public void CloseLastView(bool isCallbackCloseViewHandler = true)
		{
			var lastView = views.Pop();
			if (isCallbackCloseViewHandler)
			{
				if (!lastView.ViewModel.AfterViewClosed?.Invoke(lastView.ViewModel) ?? false)
				{
					views.Push(lastView);
					return;
				}
			}

			if (views.Count == 0)
			{
				CloseAllViews();
				return;
			}

			if (Region != null)
			{
				Region.Content = views.Peek();
				Region.DataContext = views.Peek().ViewModel;
			}
		}

		public void CloseAllViews()
		{
			if (Region != null)
			{
				Region.Content = null;
				Region.DataContext = null;
			}
		}

		public void UpdateRegion(BaseVM viewModel, Func<BaseVM, bool> afterViewClosed = null)
		{
			if (afterViewClosed != null)
				viewModel.AfterViewClosed = afterViewClosed;

			if (ViewModelToViewMap.TryGetValue(viewModel.GetType(), out Type viewType))
			{
				var view = Activator.CreateInstance(viewType) as BaseView;
				view.ViewModel = viewModel;

				views.Clear();
				views.Push(view);

				if (Region != null)
				{
					Region.Content = view;
					Region.DataContext = viewModel;
				}
			}
		}
	}
}