using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLibrary.ViewNavigation.Regions
{
	class RegionManager
	{
		public static RegionManager Instance { get; } = new RegionManager();
		private RegionManager() { }


		Dictionary<string, RegionWrapper> Regions { get; } = new Dictionary<string, RegionWrapper>();

		public void AddRegion(string regionId, Region region)
		{
			if (!Regions.ContainsKey(regionId))
			{
				Regions.Add(regionId, new RegionWrapper());
			}

			Regions[regionId].Region = region;
			Regions[regionId].Initialization();
		}
			

		public RegionWrapper GetRegionWrapper(string regionId)
		{
			if (!Regions.ContainsKey(regionId))
			{
				Regions.Add(regionId, new RegionWrapper());
			}

			return Regions[regionId];
		}			
	}
}