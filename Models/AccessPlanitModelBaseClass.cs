using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
	public class AccessPlanitModelBaseClass
	{
		public static Dictionary<string, object?> GetUpdatedProperties<T>(T original, T updated)
		{
			var delta = new Dictionary<string, object?>();
			var properties = typeof(T).GetProperties();

			foreach (var property in properties)
			{
				var originalValue = property.GetValue(original);
				var updatedValue = property.GetValue(updated);

				if (!Equals(originalValue, updatedValue))
				{
					delta[property.Name] = updatedValue;
				}
			}

			return delta;
		}

		public void UpdateProperties(Dictionary<string, object> propertiesToUpdate)
		{
			var properties = this.GetType().GetProperties();

			foreach (var property in propertiesToUpdate)
			{
				var propertyInfo = properties.FirstOrDefault(p => p.Name == property.Key);

				if (propertyInfo != null && propertyInfo.CanWrite)
				{
					propertyInfo.SetValue(this, property.Value);
				}
			}
		}
	}
}
