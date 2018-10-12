using MvvmLibrary.Interfaces;

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MvvmLibrary.ViewModels
{
	/// <summary>
	/// Базовый класс для создания модели представления.
	/// </summary>
	public abstract class BaseVM : INotifyPropertyChanged
	{
		protected IViewNavigator ViewNavigator { get; }

		private string title;

		/// <summary>
		/// Заголовок окна.
		/// </summary>
		public string Title
		{
			get => title;
			set { title = value; OnPropertyChanged(); }
		}

		public BaseVM(IViewNavigator viewNavigator)
		{
			ViewNavigator = viewNavigator;
		}

		/// <summary>
		/// Представляет действие, выполняющееся после закрытия представления.
		/// Возвращает true, если представление может быть закрыто. В противном случае - false.
		/// </summary>
		public Func<BaseVM, bool> AfterViewClosed { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
