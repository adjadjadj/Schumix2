/*
 * This file is part of Schumix.
 * 
 * Copyright (C) 2010-2012 Twl
 * Copyright (C) 2010-2013 Megax <http://megax.yeahunter.hu/>
 * Copyright (C) 2013 Schumix Team <http://schumix.eu/>
 * 
 * Schumix is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * Schumix is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with Schumix.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Schumix.Framework;
using Schumix.Framework.Logger;
using Schumix.Framework.Config;
using Schumix.Framework.Extensions;
using Schumix.Framework.Localization;

namespace Schumix.ExtraAddon.Commands
{
	/// <summary>
	///   A class which provides useful methods for working with the world-wide web.
	/// </summary>
	static class WebHelper
	{
		private static readonly LocalizationConsole sLConsole = Singleton<LocalizationConsole>.Instance;
		private static readonly Utilities sUtilities = Singleton<Utilities>.Instance;

		/// <summary>
		///   Gets the title of the specified webpage.
		/// </summary>
		/// <param name = "url">
		///   The webpage's URL.
		/// </param>
		/// <returns>
		///   The webpage's title.
		/// </returns>
		public static string GetWebTitle(Uri url)
		{
			try
			{
				var getTitleRegex = new Regex(@"<title>(?<ttl>.*\s*.+\s*.*)\s*</title>", RegexOptions.IgnoreCase);
				string data = sUtilities.DownloadString(url, 3500, getTitleRegex, 20000);
				var match = getTitleRegex.Match(data);

				if(match.Success)
				{
					string ttl = match.Groups["ttl"].ToString();
					return ttl.TrimMessage();
				}

				return string.Empty;
			}
			catch(Exception e)
			{
				Log.Debug("WebHelper", sLConsole.GetString("Exception thrown while fetching web title: {0}"), e.Message);
				return string.Empty;
			}
		}
	}
}