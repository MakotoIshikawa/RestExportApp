using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestExportApp.Models {
	#region PrecipitationModel

	public class PrecipitationModel {
		public Resultinfo ResultInfo { get; set; }
		public Feature[] Feature { get; set; }
	}

	public class Resultinfo {
		public int Count { get; set; }
		public int Total { get; set; }
		public int Start { get; set; }
		public int Status { get; set; }
		public float Latency { get; set; }
		public string Description { get; set; }
		public string Copyright { get; set; }
	}

	#region Feature

	public class Feature {
		public string Id { get; set; }
		public string Name { get; set; }
		public Geometry Geometry { get; set; }
		public Property Property { get; set; }
	}

	public class Geometry {
		public string Type { get; set; }
		public string Coordinates { get; set; }
	}

	#region Property

	public class Property {
		public int WeatherAreaCode { get; set; }
		public WeatherList WeatherList { get; set; }
	}

	public class WeatherList {
		public Weather[] Weather { get; set; }
	}

	public class Weather {
		public string Type { get; set; }
		public string Date { get; set; }
		public float Rainfall { get; set; }
	}

	#endregion

	#endregion

	#endregion
}
