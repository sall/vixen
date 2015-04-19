﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using VixenModules.App.WebServer.Filter;

namespace VixenModules.App.WebServer.Controllers
{
	[ArgumentExceptionFilter]
	public class BaseController: ApiController
	{

		protected void CreateResponseMessage(HttpStatusCode code, string content, string reason)
		{
			var resp = new HttpResponseMessage(code)
				{
					Content = new StringContent(content),
					ReasonPhrase = reason
				};
				throw new HttpResponseException(resp);
		}
	}
}
