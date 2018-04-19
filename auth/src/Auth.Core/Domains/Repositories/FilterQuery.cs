using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Auth.Core.Domains.Repositories
{
	public class FilterQuery
	{
		private Dictionary<string, string> _dictionary;

		public virtual string Filter { get; set; }

		public string GetFilter(string filter)
		{
			string value = string.Empty;
			ToDictionary()?.TryGetValue(filter, out value);
			return value;
		}

		private IDictionary<string, string> ToDictionary()
		{
			if (_dictionary == null && !string.IsNullOrWhiteSpace(Filter))
			{
				_dictionary = new Dictionary<string, string>();

				foreach (var kvs in Filter.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries))
				{
					var kv = kvs.Split(new[] { "::" }, StringSplitOptions.RemoveEmptyEntries);
					var key = kv.First().ToLower();
					var value = kv.Length > 1 ? kv[1] : null;
					if (_dictionary.ContainsKey(key))
					{
						_dictionary[key] = value;
					}
					else
					{
						_dictionary.Add(key, value);
					}
				}
			}
			return _dictionary;
		}
	}
}
