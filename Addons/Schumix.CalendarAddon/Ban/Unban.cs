/*
 * This file is part of Schumix.
 * 
 * Copyright (C) 2010-2011 Megax <http://www.megaxx.info/>
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
using Schumix.Irc;
using Schumix.Framework;
using Schumix.Framework.Extensions;
using Schumix.Framework.Localization;

namespace Schumix.CalendarAddon
{
	public sealed class Unban
	{
		private readonly LocalizationManager sLManager = Singleton<LocalizationManager>.Instance;
		private readonly Utilities sUtilities = Singleton<Utilities>.Instance;
		private readonly Sender sSender = Singleton<Sender>.Instance;
		private Unban() {}

		public string UnbanName(string name, string channel)
		{
			var db = SchumixBase.DManager.QueryFirstRow("SELECT* FROM banned WHERE Name = '{0}' AND Channel = '{1}'", sUtilities.SqlEscape(name.ToLower()), channel.ToLower());
			if(db.IsNull())
				return sLManager.GetWarningText("UnbanList", channel);

			sSender.Unban(channel, name);
			SchumixBase.DManager.QueryFirstRow("DELETE FROM `banned` WHERE Name = '{0}' AND Channel = '{1}'", sUtilities.SqlEscape(name.ToLower()), channel.ToLower());
			return sLManager.GetWarningText("UnbanList1", channel);
		}
	}
}