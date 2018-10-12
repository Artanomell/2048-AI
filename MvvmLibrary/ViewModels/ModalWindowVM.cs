using MvvmLibrary.Enums;
using MvvmLibrary.Interfaces;

namespace MvvmLibrary.ViewModels
{
	class ModalWindowVM : BaseVM
	{
		private bool btnCancelVisible;
		public bool BtnCancelVisible
		{
			get => btnCancelVisible;
			set { btnCancelVisible = value; OnPropertyChanged(); }
		}

		private string btnOkText;
		public string BtnOkText
		{
			get => btnOkText;
			set { btnOkText = value; OnPropertyChanged(); }
		}

		private string btnCancelText;
		public string BtnCancelText
		{
			get => btnCancelText;
			set { btnCancelText = value; OnPropertyChanged(); }
		}

		public ModalWindowVM(IViewNavigator viewNavigator, string caption, ModalWindowButtons buttonType, string btnOkText, string btnCancelText) : base(viewNavigator)
		{
			Title = caption;
			BtnCancelVisible = buttonType == ModalWindowButtons.OkCancelButton;
			BtnOkText = btnOkText;
			BtnCancelText = btnCancelText;
		}
	}
}