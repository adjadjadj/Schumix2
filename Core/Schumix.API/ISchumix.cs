﻿/*
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

namespace Schumix.API
{
	/// <summary>
	/// Another attempt to implement a plugin interface.
	/// </summary>
	public interface ISchumixBase
	{
		/// <summary>
		/// Creates the addon.
		/// </summary>
		/// <param name="conn">IRC connection.</param>
		/// <param name="channels">Channel list.</param>
		void Setup();
		/// <summary>
		/// Destroys the addon, releasing all resources.
		/// </summary>
		void Destroy();

		/// <summary>
		/// Name of the addon
		/// </summary>
		string Name { get; }
		/// <summary>
		/// Author of the addon.
		/// </summary>
		string Author { get; }
		/// <summary>
		/// Website where the addon is available.
		/// </summary>
		string Website { get; }
	}
}