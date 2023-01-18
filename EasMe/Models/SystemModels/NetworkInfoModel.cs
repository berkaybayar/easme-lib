using EasMe.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Models.SystemModels
{
	public class NetworkInfoModel
	{
		private string? _IpAddress;

		public string? IpAddress
        {
			get { return _IpAddress; }
			set 
			{
				_IpAddress = value.IsNullOrEmpty() ? null : value;
            }
		}

		public string? Location { get; set; } 
		public bool IsWarpOn { get; set; } = false;
		public bool IsGatewayOn { get; set; } = false;
	}
}
