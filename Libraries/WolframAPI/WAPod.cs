﻿/*
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
using System.Linq;
using System.Xml.Serialization;
using System.Diagnostics.Contracts;
using WolframAPI.Collections;
//using Schumix.Framework;
using Schumix.Framework.Extensions;

namespace WolframAPI
{
	/// <summary>
	///   The pod element in the response.
	/// </summary>
	[Serializable, CLSCompliant(true)]
	public sealed class WAPod : IEquatable<string>, IEquatable<WAPod>
	{
		//private readonly Utilities sUtilities = Singleton<Utilities>.Instance;

		/// <summary>
		///   Gets or sets the pod's title.
		/// </summary>
		[XmlAttribute("title")]
		public string Title { get; set; }

		/// <summary>
		///   No information.
		///   Seen: Identity
		/// </summary>
		[XmlAttribute("scanner")]
		public string Scanner { get; set; }

		/// <summary>
		///   Gets or sets the identifier for the pod.
		/// </summary>
		[XmlAttribute("id")]
		public string Id { get; set; }

		/// <summary>
		///   Gets or sets the pod position.
		/// </summary>
		[XmlAttribute("position")]
		public int Position { get; set; }

		/// <summary>
		///   Gets or sets a value indicating whether there was an error in this pod or not.
		/// </summary>
		[XmlAttribute("error")]
		public bool Error { get; set; }

		/// <summary>
		///   Gets or sets the number of sub pods.
		/// </summary>
		[XmlAttribute("numsubpods")]
		public int NumSubPods { get; set; }

		/// <summary>
		///   Gets or sets the list of sub pods.
		/// </summary>
		[XmlElement("subpod")]
		public UniqueList<WASubPod> SubPods { get; set; }

		/// <summary>
		/// Gets the <see cref="WolframAPI.WASubPod"/> with the specified name.
		/// </summary>
		/// <value></value>
		public WASubPod this[string name]
		{
			get
			{
				//if(!sPlatform.IsLinux)
				//	Contract.Requires(!SubPods.IsNull());

				return (from sp in SubPods where sp.Title.ToLower().Equals(name) select sp).FirstOrDefault();
			}
		}

		#region Interface Implementations

		#region Implementation of IEquatable<string>

		/// <summary>
		///   Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <returns>
		///   true if the current object is equal to the <paramref name = "other" /> parameter; otherwise, false.
		/// </returns>
		/// <param name = "other">An object to compare with this object.</param>
		public bool Equals(string other)
		{
			return other.Equals(Title);
		}

		#endregion

		#region Implementation of IEquatable<WAPod>

		/// <summary>
		///   Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <returns>
		///   true if the current object is equal to the <paramref name = "other" /> parameter; otherwise, false.
		/// </returns>
		/// <param name = "other">An object to compare with this object.</param>
		public bool Equals(WAPod other)
		{
			if(ReferenceEquals(null, other))
				return false;

			if(ReferenceEquals(this, other))
				return true;

			return Equals(other.Title, Title) && Equals(other.Scanner, Scanner) && Equals(other.Id, Id) &&
				   other.Position == Position && other.Error.Equals(Error) && other.NumSubPods == NumSubPods &&
				   Equals(other.SubPods, SubPods);
		}

		#endregion

		/// <summary>
		///   Determines whether the specified <see cref = "T:System.Object" /> is equal to the current <see cref = "T:System.Object" />.
		/// </summary>
		/// <returns>
		///   true if the specified <see cref = "T:System.Object" /> is equal to the current <see cref = "T:System.Object" />; otherwise, false.
		/// </returns>
		/// <param name = "obj">The <see cref = "T:System.Object" /> to compare with the current <see cref = "T:System.Object" />. </param>
		/// <filterpriority>2</filterpriority>
		public override bool Equals(object obj)
		{
			if(ReferenceEquals(null, obj))
				return false;

			if(ReferenceEquals(this, obj))
				return true;

			if(obj.GetType() != typeof(WAPod))
				return false;

			return Equals((WAPod)obj);
		}

		/// <summary>
		///   Serves as a hash function for a particular type.
		/// </summary>
		/// <returns>
		///   A hash code for the current <see cref = "T:System.Object" />.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override int GetHashCode()
		{
			unchecked
			{
				int result = (!Title.IsNull() ? Title.GetHashCode() : 0);
				result = (result*397) ^ (!Scanner.IsNull() ? Scanner.GetHashCode() : 0);
				result = (result*397) ^ (!Id.IsNull() ? Id.GetHashCode() : 0);
				result = (result*397) ^ Position;
				result = (result*397) ^ Error.GetHashCode();
				result = (result*397) ^ NumSubPods;
				result = (result*397) ^ (!SubPods.IsNull() ? SubPods.GetHashCode() : 0);
				return result;
			}
		}

		/// <summary>
		///   Implements the operator ==.
		/// </summary>
		/// <param name = "left">The left.</param>
		/// <param name = "right">The right.</param>
		/// <returns>The result of the operator.</returns>
		public static bool operator ==(WAPod left, WAPod right)
		{
			return Equals(left, right);
		}

		/// <summary>
		///   Implements the operator !=.
		/// </summary>
		/// <param name = "left">The left.</param>
		/// <param name = "right">The right.</param>
		/// <returns>The result of the operator.</returns>
		public static bool operator !=(WAPod left, WAPod right)
		{
			return !Equals(left, right);
		}

		#endregion
	}
}