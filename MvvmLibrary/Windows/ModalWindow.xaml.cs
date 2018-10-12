using MvvmLibrary.Views;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MvvmLibrary.Windows
{
	/// <summary>
	/// Логика взаимодействия для ModalWindow.xaml
	/// </summary>
	partial class ModalWindow : Window
	{
		Point moveStart;

		public ModalWindow(BaseView view)
		{
			InitializeComponent();

			ContentView.Content = view;
		}

		private void Window_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed &&
				!(e.OriginalSource is TextBox))
			{
				var deltaPos = e.GetPosition(this) - moveStart;
				Left += deltaPos.X;
				Top += deltaPos.Y;
			}
		}

		private void Window_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left)
			{
				moveStart = e.GetPosition(this);
			}
		}

		private void btnClose_Click(object sender, MouseButtonEventArgs e)
		{
			Close();
		}

		private void btnOk_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
		}
	}
}