using System.Windows;

using MvvmLibrary.Enums;
using MvvmLibrary.Interfaces;

namespace MvvmLibrary.Windows
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	partial class MainWindow : Window
	{
		IViewNavigator ViewNavigator { get; }

		public MainWindow(IViewNavigator viewNavigator)
		{
			InitializeComponent();

			ViewNavigator = viewNavigator;
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (!ViewNavigator.ShowModalWindow("Вы точно хотите выйти из программы?", "Выход", ModalWindowButtons.OkCancelButton, "Выйти"))
			{
				e.Cancel = true;
			}
		}
	}
}