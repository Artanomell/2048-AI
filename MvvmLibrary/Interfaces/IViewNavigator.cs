using System;

using MvvmLibrary.Enums;
using MvvmLibrary.ViewModels;
using MvvmLibrary.ViewNavigation.Regions;

namespace MvvmLibrary.Interfaces
{
	/// <summary>
	/// Менеджер навигации между представлениями.
	/// </summary>
	public interface IViewNavigator
	{
		/// <summary>
		/// Отображает в окне новое представление, соответствующее указанной <paramref name="viewModel"/>.
		/// </summary>
		/// <param name="viewModel">Указывает на представление, которое необходимо отобразить в окне.</param>
		void NavigateTo(BaseVM viewModel, Func<BaseVM, bool> afterViewClosed = null);

		/// <summary>
		/// Закрывает последнее представление и выполняет действие закрытия представления при необходимости.
		/// </summary>
		/// <param name="isCallbackCloseViewHandler">Флаг, указывающий нужно ли выполнять действие закрытия представления.</param>
		void CloseLastView(bool isCallbackCloseViewHandler = true);

		/// <summary>
		/// Закрывает все представления и выходит из главного окна.
		/// </summary>
		void CloseAllViews();

		/// <summary>
		/// Отображает модальное диалоговое окно с указанным текстом.
		/// </summary>
		/// <param name="viewModel">Указывает на представление, которое необходимо отобразить в модальном окне.</param>
		bool ShowModalWindow(string text, string caption = "", ModalWindowButtons buttonType = ModalWindowButtons.OnlyOkButton,
									string btnOkText = "Ок", string btnCancelText = "Отмена",
									Action<BaseVM> okResult = null, Action<BaseVM> cancelResult = null);

		/// <summary>
		/// Отображает модальное диалоговое окно с указанным представлением.
		/// </summary>
		/// <param name="viewModel">Указывает на представление, которое необходимо отобразить в модальном окне.</param>
		bool ShowModalWindow(BaseVM viewModel, string caption = "", ModalWindowButtons buttonType = ModalWindowButtons.OnlyOkButton,
									string btnOkText = "Ок", string btnCancelText = "Отмена",
									Action<BaseVM> okResult = null, Action<BaseVM> cancelResult = null);

		RegionsCollection Regions { get; }
	}
}