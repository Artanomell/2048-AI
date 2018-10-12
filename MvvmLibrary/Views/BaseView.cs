using System.Windows.Controls;

using MvvmLibrary.ViewModels;

namespace MvvmLibrary.Views
{
	public abstract class BaseView : UserControl
	{
		protected BaseVM viewModel;
		public BaseVM ViewModel
		{
			get => viewModel;
			set
			{
				viewModel = value;
				DataContext = viewModel;
			}
		}
	}
}
