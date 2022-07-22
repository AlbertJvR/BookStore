using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Persistence;

public static class EfCoreExtensions
{
    public static void ApplyAllConfigurationsFromCurrentAssembly(this ModelBuilder modelBuilder, Assembly assembly, string configNamespace = "")
    {
        var applyGenericMethods = typeof(ModelBuilder).GetMethods( BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);
        var applyGenericApplyConfigurationMethods = applyGenericMethods.Where(m => m.IsGenericMethod && m.Name.Equals("ApplyConfiguration", StringComparison.OrdinalIgnoreCase));
        var applyGenericMethod = applyGenericApplyConfigurationMethods.FirstOrDefault(m => Enumerable.FirstOrDefault<ParameterInfo>(m.GetParameters())?.ParameterType.Name== "IEntityTypeConfiguration`1");

        var applicableTypes = assembly
            .GetTypes()
            .Where(c => c.IsClass && !c.IsAbstract && !c.ContainsGenericParameters);

        if (!string.IsNullOrEmpty(configNamespace))
        {
            applicableTypes = applicableTypes.Where(c => c.Namespace == configNamespace);
        }

        foreach (var type in applicableTypes)
        {
            foreach (var iface in type.GetInterfaces())
            {
                if (iface.IsConstructedGenericType && iface.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))
                {
                    var applyConcreteMethod = applyGenericMethod!.MakeGenericMethod(iface.GenericTypeArguments[0]);
                    applyConcreteMethod.Invoke(modelBuilder, new object[] { Activator.CreateInstance(type)! });
                    break;
                }
            }
        }
    }
}