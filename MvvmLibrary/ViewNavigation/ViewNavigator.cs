using System;
using System.Windows;
using System.Collections.Generic;

using MvvmLibrary.Enums;
using MvvmLibrary.Views;
using MvvmLibrary.Windows;
using MvvmLibrary.Interfaces;
using MvvmLibrary.ViewModels;
using MvvmLibrary.ViewNavigation.Regions;

namespace MvvmLibrary.ViewNavigation
{
	/// <summary>
	/// Менеджер навигации между представлениями.
	/// </summary>
	public class ViewNavigator : IViewNavigator
	{
		/// <summary>
		/// Окно для отображения представлений.
		/// </summary>
		private readonly Window mainWindow;

		/// <summary>
		/// Стек представлений.
		/// </summary>
		private readonly Stack<BaseView> views = new Stack<BaseView>();

		/// <summary>
		/// Установка соответствия ViewModel к View.
		/// </summary>
		private readonly Dictionary<Type, Type> viewModelToViewMap = new Dictionary<Type, Type>()
		{
			[typeof(ModalMessageVM)] = typeof(ModalMessageView)
		};

		private Type tempVM;

		public ViewNavigator Bind<T>() where T : BaseVM
		{
			tempVM = typeof(T);
			return this;
		}

		public void To<T>() where T : BaseView
		{
			viewModelToViewMap.Add(tempVM, typeof(T));
			tempVM = null;
		}

		public RegionsCollection Regions { get; }

		public ViewNavigator()
		{
			mainWindow = new MainWindow(this);
			mainWindow.Show();

			Regions = new RegionsCollection(viewModelToViewMap);
		}

		/// <summary>
		/// Отображает в окне новое представление, соответствующее указанной <paramref name="viewModel"/>.
		/// </summary>
		/// <param name="viewModel">Указывает на представление, которое необходимо отобразить в окне.</param>
		public void NavigateTo(BaseVM viewModel, Func<BaseVM, bool> afterViewClosed = null)
		{
			if (afterViewClosed != null)
				viewModel.AfterViewClosed = afterViewClosed;

			if (viewModelToViewMap.TryGetValue(viewModel.GetType(), out Type viewType))
			{
				var view = Activator.CreateInstance(viewType) as BaseView;
				view.ViewModel = viewModel;

				views.Push(view);

				mainWindow.Content = view;
				mainWindow.DataContext = viewModel;
			}
		}

		/// <summary>
		/// Закрывает последнее представление и выполняет действие закрытия представления при необходимости.
		/// </summary>
		/// <param name="isCallbackCloseViewHandler">Флаг, указывающий нужно ли выполнять действие закрытия представления.</param>
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

			mainWindow.Content = views.Peek();
			mainWindow.DataContext = views.Peek().ViewModel;
		}

		/// <summary>
		/// Закрывает все представления и выходит из главного окна.
		/// </summary>
		public void CloseAllViews()
		{
			mainWindow.Close();
		}

		/// <summary>
		/// Отображает модальное диалоговое окно с указанным текстом.
		/// </summary>
		/// <param name="viewModel">Указывает на представление, которое необходимо отобразить в модальном окне.</param>
		public bool ShowModalWindow(string text, string caption = "", ModalWindowButtons buttonType = ModalWindowButtons.OnlyOkButton,
									string btnOkText = "Ок", string btnCancelText = "Отмена",
									Action<BaseVM> okResult = null, Action<BaseVM> cancelResult = null)
		{
			var viewModel = new ModalMessageVM(this, text);
			return ShowModalWindow(viewModel, caption, buttonType, btnOkText, btnCancelText, okResult, cancelResult);
		}

		/// <summary>
		/// Отображает модальное диалоговое окно с указанным представлением.
		/// </summary>
		/// <param name="viewModel">Указывает на представление, которое необходимо отобразить в модальном окне.</param>
		public bool ShowModalWindow(BaseVM viewModel, string caption = "", ModalWindowButtons buttonType = ModalWindowButtons.OnlyOkButton,
									string btnOkText = "Ок", string btnCancelText = "Отмена",
									Action<BaseVM> okResult = null, Action<BaseVM> cancelResult = null)
		{
			var result = false;

			if (viewModelToViewMap.TryGetValue(viewModel.GetType(), out Type viewType))
			{
				var view = Activator.CreateInstance(viewType) as BaseView;
				view.ViewModel = viewModel;

				var modalWindow = new ModalWindow(view) { Owner = mainWindow };
				modalWindow.DataContext = new ModalWindowVM(this, caption, buttonType, btnOkText, btnCancelText);

				result = modalWindow.ShowDialog() ?? false;

				if (result)
					okResult?.Invoke(viewModel);
				else
					cancelResult?.Invoke(viewModel);
			}

			return result;
		}
	}
}