﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab6.ViewModels.Authentication
{
	public class LoginResponse
	{
		public string Token { get; set; }
		public DateTime Expiration { get; set; }
	}
}
