using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace WeatherWS {
	class RestWS {
		WebRequest request;
		public String body;
		public String response;

		public RestWS(String method, String endPoint, List<String> parameters) { 
		}
	}
}
