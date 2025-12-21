using System.Collections;
using System.Collections.Generic;
using System.Text.Json;

namespace Services
{
    public class APIObjectResponseHandler
    {
        public async Task<T?> ConvertResponseObjectToModel<T>(HttpResponseMessage response) where T : class, new()
        {
            try
            {
                string responseContent = await response.Content.ReadAsStringAsync();

                // Deserialize into JsonElement first
                var jsonElement = JsonSerializer.Deserialize<JsonElement>(responseContent);

                // Check if the JSON contains the 'results' array
                if (jsonElement.TryGetProperty("results", out var resultsProperty) &&
                    resultsProperty.ValueKind == JsonValueKind.Array)
                {
                    if (typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(List<>))
                    {
                        // Get the item type of the collection
                        var itemType = typeof(T).GetGenericArguments()[0];

                        // Create a list dynamically
                        var list = Activator.CreateInstance(typeof(List<>).MakeGenericType(itemType)) as IList;

                        foreach (var element in resultsProperty.EnumerateArray()) // Enumerate through JSON array
                        {
                            var item = CreateInstanceAndSetProperties(element, itemType);
                            list?.Add(item);
                        }

                        return list as T;
                    }
                    else if (resultsProperty.GetArrayLength() > 0)
                    {
                        // Handle single object scenario: map the first object in the array
                        var firstResult = resultsProperty[0];
                        return CreateInstanceAndSetProperties(firstResult, typeof(T)) as T;
                    }
                }

                return default;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private object CreateInstanceAndSetProperties(JsonElement element, Type type)
        {
            var instance = Activator.CreateInstance(type);

            foreach (var property in type.GetProperties())
            {
                if (element.TryGetProperty(property.Name, out var jsonProperty) &&
                    jsonProperty.ValueKind != JsonValueKind.Null)
                {
                    object? value = null;

                    if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(IEnumerable))
                    {
                        // Handle collection properties
                        var elementType = property.PropertyType.GetElementType();

                        if (elementType != null && jsonProperty.ValueKind == JsonValueKind.Array)
                        {
                            var arrayValues = Array.CreateInstance(elementType, jsonProperty.GetArrayLength());

                            int index = 0;

                            foreach (var arrayElement in jsonProperty.EnumerateArray())
                            {
                                var arrayItem = JsonSerializer.Deserialize(arrayElement.GetRawText(), elementType);

                                if (arrayItem != null)
                                    arrayValues.SetValue(arrayItem, index++);
                            }

                            value = arrayValues;
                        }
                    }

                    // Deserialize the property value
                    // Had to be wrapped in try catch because some properties in the Json Object
                    // would change type if the property had a value or not causing it to break.
                    try
                    {
                        value = JsonSerializer.Deserialize(jsonProperty.GetRawText(), property.PropertyType);
                    }
                    catch (Exception ex)
                    {
                        value = null;
                    }

                    // Set the property only if it's valid
                    if (value != null && !(value is string strValue && string.IsNullOrWhiteSpace(strValue)))
                        property.SetValue(instance, value);
                }
            }

            return instance;
        }
    }
}
