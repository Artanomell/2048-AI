using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLibrary.ViewNavigation.Regions
{
	public class RegionsCollection
	{
		Dictionary<Type, Type> ViewModelToViewMap { get; }

		public RegionsCollection(Dictionary<Type, Type> viewModelToViewMap)
		{
			ViewModelToViewMap = viewModelToViewMap;
		}

		public RegionWrapper GetRegion(string RegionId)
		{
			var regionWrapper = RegionManager.Instance.GetRegionWrapper(RegionId);
			regionWrapper.ViewModelToViewMap = ViewModelToViewMap;
			return regionWrapper;
		}

		public RegionWrapper this[string RegionId] => GetRegion(RegionId);
	}
}
