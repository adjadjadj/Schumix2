/*
 * This file is part of Schumix.
 * 
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

namespace Schumix.Config.CopyTo
{
	sealed class Copy
	{
		/// <summary>
		///     Több helyről átmásolja az új fájlokat.
		/// </summary>
		public Copy(string Dir, string Addons, string Configs)
		{
			string ndir = Dir;
			Dir = Dir + "/Run/Release";

			var dir = new DirectoryInfo(Dir + "/Addons");

			foreach(var file in dir.GetFiles())
			{
				if(file.Name.ToLower().Contains(".db3"))
					continue;

				if(File.Exists(Addons + "/" + file.Name))
					File.Delete(Addons + "/" + file.Name);

				File.Move(Dir + "/Addons/" + file.Name, Addons + "/" + file.Name);
			}

			dir = new DirectoryInfo(Dir);

			foreach(var file in dir.GetFiles())
			{
				if(file.Name.ToLower().Contains(".db3"))
					continue;

				if(File.Exists(file.Name))
					File.Delete(file.Name);

				File.Move(Dir + "/" + file.Name, file.Name);
			}

			if(Directory.Exists(ndir + "/Scripts"))
				Directory.Move(ndir + "/Scripts", "Scripts");
			
			if(Directory.Exists(Dir + "/locale"))
				Directory.Move(Dir + "/locale", "locale");

			dir = new DirectoryInfo(Configs);

			foreach(var fi in dir.GetFiles())
			{
				if(fi.Name.Substring(0, 1) == "_")
					continue;

				File.Move("Configs/" + fi.Name, Configs + "/_" + fi.Name);
			}
		}
	}
}