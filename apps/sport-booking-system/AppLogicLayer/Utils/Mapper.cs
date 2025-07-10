using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppLogicLayer
{
    public interface IMapper<TSource, TDestination>
    {
        TDestination? MapFrom(TSource source);
        void Map(TSource source, TDestination destination);
        IEnumerable<TDestination> MapToEnumerable(IEnumerable<TSource> sources);
    }

    public class Mapper<TSource, TDestination> : IMapper<TSource, TDestination>
    {
        public TDestination? MapFrom(TSource source)
        {
            var destination = Activator.CreateInstance(typeof(TDestination));
            InvokeGenericMapMethod(source, destination);

            if (destination == null)
                return default;

            return (TDestination)destination;
        }

        public void Map(TSource source, TDestination destination)
        {
            if (source == null || destination == null)
                throw new ArgumentNullException("Source or Destination cannot be null");

            PropertyInfo[] sourceProps = source.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            PropertyInfo[] destinationProps = destination.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var sProp in sourceProps)
            {
                // Check if destination properties contain the source property
                var dProp = destinationProps.FirstOrDefault(p => p.Name == sProp.Name && p.CanWrite);
                if (dProp == null)
                    continue;

                var sValue = sProp.GetValue(source, null);

                // Skip the property if it's value is null or empty string
                if (sValue == null || (sValue is string && string.IsNullOrWhiteSpace((string)sValue))) 
                    continue;

                // Check if simple type like pri
                if (IsSimpleType(dProp.PropertyType))
                {
                    if (dProp.PropertyType.IsAssignableFrom(sProp.PropertyType) || dProp.PropertyType == sProp.PropertyType)
                        dProp.SetValue(destination, sValue);
                }
                else if (typeof(IEnumerable).IsAssignableFrom(dProp.PropertyType) && typeof(IEnumerable).IsAssignableFrom(sProp.PropertyType)
                    && dProp.PropertyType != typeof(string) && sProp.PropertyType != typeof(string))
                {
                    var sourceCollection = sValue as IEnumerable;
                    if (sourceCollection == null) continue;

                    var sItemType = sProp.PropertyType.GetGenericArguments().First();
                    var dItemType = dProp.PropertyType.GetGenericArguments().First();

                    Type dListType = typeof(List<>).MakeGenericType(dItemType);

                    var dList = (IList?)Activator.CreateInstance(dListType);

                    if (dList == null) continue;

                    foreach (var sItem in sourceCollection)
                    {
                        var dItem = Activator.CreateInstance(dItemType);
                        InvokeGenericMapMethod(sItem, dItem);
                        dList.Add(dItem);
                    }

                    dProp.SetValue(destination, dList);
                }
                else
                {
                    var dValue = Activator.CreateInstance(dProp.PropertyType);
                    InvokeGenericMapMethod(sValue, dValue);
                    dProp.SetValue(destination, dValue);
                }
            }
        }

        public IEnumerable<TDestination> MapToEnumerable(IEnumerable<TSource> sources)
        {
            var sItemType = sources.GetType().GetGenericArguments().First();
            Type dListType = typeof(IEnumerable<>).MakeGenericType(typeof(TDestination));

            var dList = Activator.CreateInstance(dListType);

            if (dList == null)
                return Enumerable.Empty<TDestination>();

            return (IEnumerable<TDestination>)dList;
        }

        private void InvokeGenericMapMethod(object? source, object? destination)
        {
            if (source == null || destination == null)
                throw new ArgumentNullException("Source or Destination cannot be null");

            Type sourceType = source.GetType();
            Type destinationType = destination.GetType();

            // Create an instance of Mapper class
            Type genericMapperType = typeof(Mapper<,>);
            Type constructedMapperType = genericMapperType.MakeGenericType(sourceType, destinationType);
            object? mapperInstance = Activator.CreateInstance(constructedMapperType);

            // Get Map method
            MethodInfo? mapMethod = constructedMapperType.GetMethod("Map", [sourceType, destinationType]);

            if (mapperInstance == null || mapMethod == null || destination == null)
                return;

            var result = mapMethod.Invoke(mapperInstance, new object[] { source, destination });
        }

        private bool IsSimpleType(Type type)
        {
            return type.IsPrimitive
                || type.IsEnum
                || type.Equals(typeof(string))
                || type.Equals(typeof(decimal))
                || type.Equals(typeof(DateTime))
                || type.Equals(typeof(Guid));
        }
    }

    public static class MapperHelper
    { 
        public static void Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            var mapper = new Mapper<TSource, TDestination>();
            mapper.Map(source, destination);
        }

        public static TDestination? Map<TSource, TDestination>(TSource source)
        {
            var mapper = new Mapper<TSource, TDestination>();
            return mapper.MapFrom(source);
        }

        public static IEnumerable<TDestination> Map<TSource, TDestination>(IEnumerable<TSource> sources)
        {
            var mapper = new Mapper<TSource, TDestination>();
            return mapper.MapToEnumerable(sources);
        }
    }
}
