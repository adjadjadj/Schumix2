/*
 * This file is part of Schumix.
 * 
 * Copyright (C) 2010-2011 Twl
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
using System.Threading;
using System.Data;
using System.Data.SQLite;
using System.Text.RegularExpressions;

namespace Schumix.Framework.Database
{
	public sealed class SQLite
	{
		private SQLiteConnection Connection;

		public SQLite(string file)
		{
			if(!Initialize(file))
			{
				Log.Error("SQLite", "Hiba tortent az adatbazishoz valo kapcsolodas soran!");
				Thread.Sleep(200);
				Environment.Exit(1);
			}
			else
				Log.Notice("SQLite", "SQLite rendszer elindult.");
		}

		~SQLite()
		{
			Log.Debug("SQLite", "~SQLite()");
			Connection.Close();
		}

		private bool Initialize(string file)
		{
			try
			{
				Connection = new SQLiteConnection("Data Source=" + file);
				Connection.Open();
				return true;
			}
			catch(SQLiteException s)
			{
				Log.Error("SQLite", "{0}", s.Message);
				return false;
			}
		}

		/// <summary>
		/// Executes the given query on the database.
		/// </summary>
		/// <param name="sql">The query</param>
		/// <returns>Result from the database.</returns>
		public DataTable Query(string sql)
		{
			try
			{
				var adapter = new SQLiteDataAdapter();
				var command = Connection.CreateCommand();
				SQLiteEscape(sql);
				command.CommandText = sql;
				adapter.SelectCommand = command;

				var table = new DataTable();
				adapter.Fill(table);

				command.Dispose();
				adapter.Dispose();

				return table;
			}
			catch(SQLiteException s)
			{
				Log.Error("SQLite", "Query hiba: {0}", s.Message);
				return null;
			}
		}

		/// <summary>
		/// Executes the given query on the database and returns the result's first row.
		/// </summary>
		/// <param name="query">Query to execute</param>
		/// <returns>The row</returns>
		public DataRow QueryFirstRow(string query)
		{
			var table = Query(query);
			return !table.Equals(null) && table.Rows.Count > 0 ? table.Rows[0] : null;
		}

		private string SQLiteEscape(string usString)
		{
			if(usString == null)
				return null;

			return Regex.Replace(usString, @"[\r\n\x00\x1a\\'""]", @"\$0");
		}
	}
}