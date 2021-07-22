using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit.Sdk;

namespace SmartCharging.UnitTests.Helpers
{
    public class ObjectComparer<T> : IEqualityComparer<T>
    {
        public bool Equals(T expected, T actual)
        {
            return CompareObjectsInternal(expected?.GetType(), expected, actual);
        }

        private static bool CompareObjectsInternal(Type type, object expected, object actual)
        {
            var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in props.Where(x => !x.GetIndexParameters().Any()))
            {
                var expectedValue = prop.GetValue(expected, null);
                var actualValue = prop.GetValue(actual, null);

                var valueType = expectedValue?.GetType();

                if (valueType != null && Type.GetTypeCode(valueType) != TypeCode.String && valueType.IsClass)
                {
                    return CompareObjectsInternal(valueType, expectedValue, actualValue);
                }

                if (ReferenceEquals(expectedValue, null) != ReferenceEquals(actualValue, null)
                    || expectedValue != null && !expectedValue.Equals(actualValue))
                {
                    throw new EqualException($"A value of \"{expectedValue}\" for property \"{prop.Name}\"",
                        $"A value of \"{actualValue}\" for property \"{prop.Name}\"");
                }
            }

            return true;
        }

        public int GetHashCode(T parameterValue)
        {
            return Tuple.Create(parameterValue).GetHashCode();
        }
    }
}
