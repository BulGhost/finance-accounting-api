using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using FinanceAccounting.Logic.Interfaces;

namespace FinanceAccounting.Logic.Mappings
{
    public class AssemblyMappingProfile : Profile
    {
        public AssemblyMappingProfile(Assembly assembly)
        {
            ApplyMappingFromAssembly(assembly);
        }

        private void ApplyMappingFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(type => type.GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapWith<>)))
                .ToList();

            foreach (Type type in types)
            {
                object instance = Activator.CreateInstance(type);
                MethodInfo methodInfo = type.GetMethod(nameof(IMapWith<object>.Mapping));
                methodInfo?.Invoke(instance, new object[] {this});
            }
        }
    }
}
