using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Models.SystemModels
{
	public class NetworkInfoModel
	{
		public string IpAddress { get; set; } = string.Empty;
		public string Location { get; set; } = string.Empty;
		public bool IsWarpOn { get; set; } = false;
		public bool IsGatewayOn { get; set; } = false;
	}
}
