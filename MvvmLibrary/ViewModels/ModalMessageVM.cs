using MvvmLibrary.Interfaces;

namespace MvvmLibrary.ViewModels
{
	class ModalMessageVM : BaseVM
	{
		private string message;
		public string Message
		{
			get => message;
			set { message = value; OnPropertyChanged(); }
		}

		public ModalMessageVM(IViewNavigator viewNavigator, string message) : base(viewNavigator)
		{
			Message = message;
		}
	}
}