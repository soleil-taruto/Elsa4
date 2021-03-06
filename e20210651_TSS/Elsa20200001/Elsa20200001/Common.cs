using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;

namespace Charlotte
{
	public static class Common
	{
		public static string WrapNullOrString(string value)
		{
			return value == null ? Consts.SERIALIZED_NULL : Consts.SERIALIZED_NOT_NULL_PREFIX + value;
		}

		public static string UnwrapNullOrString(string value)
		{
			return value == Consts.SERIALIZED_NULL ? null : value.Substring(Consts.SERIALIZED_NOT_NULL_PREFIX.Length);
		}

		public static string FirstNotEmpty(params string[] strs)
		{
			return strs.First(str => !string.IsNullOrEmpty(str));
		}

		public static IEnumerable<T> E_Insert<T>(IEnumerable<T> list, int index, IEnumerable<T> listForInsert)
		{
			return list.Take(index).Concat(listForInsert).Concat(list.Skip(index));
		}

		public static IEnumerable<T> E_Remove<T>(IEnumerable<T> list, int index, int count)
		{
			return list.Take(index).Concat(list.Skip(index + count));
		}

		public static T[] Insert<T>(T[] arr, int index, T[] arrForInsert)
		{
			if (
				arr == null ||
				arrForInsert == null ||
				index < 0 || arr.Length < index
				)
				throw new DDError();

			T[] dest = new T[arr.Length + arrForInsert.Length];

			Array.Copy(arr, 0, dest, 0, index);
			Array.Copy(arrForInsert, 0, dest, index, arrForInsert.Length);
			Array.Copy(arr, index, dest, index + arrForInsert.Length, arr.Length - index);

			return dest;
		}

		public static T[] Remove<T>(T[] arr, int index, int count)
		{
			if (
				arr == null ||
				index < 0 || arr.Length < index ||
				count < 0 || arr.Length - index < count
				)
				throw new DDError();

			T[] dest = new T[arr.Length - count];

			Array.Copy(arr, 0, dest, 0, index);
			Array.Copy(arr, index + count, dest, index, arr.Length - (index + count));

			return dest;
		}
	}
}
