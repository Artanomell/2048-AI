using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MvvmLibrary.ViewNavigation.Regions
{
	/// <summary>
	/// Логика взаимодействия для Region.xaml
	/// </summary>
	public partial class Region : UserControl
	{
		public static readonly DependencyProperty IdProperty = DependencyProperty.Register(
			"Id",
			typeof(string),
			typeof(Region),
			new FrameworkPropertyMetadata(OnIdChanged));

		public Region()
		{
			InitializeComponent();
		}
		
		public string Id
		{
			get { return (string)GetValue(IdProperty); }
			set { SetValue(IdProperty, value); }
		}

		private static void OnIdChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			RegionManager.Instance.AddRegion((string)e.NewValue, (Region)d);
		}

		#region Прикрепляемое свойство

		// То, что закомментировано - это для прикрепляемого свойства зависимости. Может пригодится, но пока, вроде, не нужно.
		// Нужно будет подумать над кастом в методе OnIdChanged, ведь уже параметр d будет не только Region, 
		// а любым ContentControl. Может тогда и кастовать к ему!?

		//public static readonly DependencyProperty IdProperty = DependencyProperty.RegisterAttached(
		//	"Id",
		//	typeof(string),
		//	typeof(Region),
		//	new FrameworkPropertyMetadata()
		//	{
		//		PropertyChangedCallback = new PropertyChangedCallback(OnIdChanged)
		//	});

		//public static string GetId(ContentControl element)
		//{
		//	if (element == null)
		//	{
		//		throw new ArgumentNullException();
		//	}
		//	return (string)element.GetValue(Region.IdProperty);
		//}

		//public static void SetId(ContentControl element, string value)
		//{
		//	if (element == null)
		//	{
		//		throw new ArgumentNullException();
		//	}
		//	element.SetValue(Region.IdProperty, value);
		//}

		#endregion
	}
}
