#if UNITY_EDITOR && ODIN_INSPECTOR
using System;
using System.Collections.Generic;
using System.Linq;
using VMFramework.Core;
using Sirenix.OdinInspector;

public static class TypeNameListUtility
{
    public static IEnumerable<ValueDropdownItem> GetAllBaseTypesNameList(this IEnumerable<object> objects,
        bool includingInterfaces, bool includingGeneric)
    {
        var initialTypes = new HashSet<Type>();

        foreach (var o in objects)
        {
            initialTypes.Add(o.GetType());
        }

        var resultTypes = new HashSet<Type>();

        foreach (var type in initialTypes.LevelOrderTraverse(true, t => t.GetBaseTypes(includingInterfaces, false)))
        {
            if (type.IsGenericType && includingGeneric == false)
            {
                continue;
            }

            resultTypes.Add(type);
        }

        return resultTypes.Select(t => new ValueDropdownItem(t.FullName?.Replace(".", "/"), t));
    }

    public static IEnumerable<ValueDropdownItem> GetDerivedTypeNamesNameList(this Type baseType, bool includingSelf,
        bool includingInterfaces, bool includingGeneric, bool includingAbstract, bool includingSealed)
    {
        return GetDerivedTypesNameList(baseType, includingSelf, includingInterfaces, includingGeneric,
                includingAbstract, includingSealed)
            .Select(item => new ValueDropdownItem(item.Text, ((Type)item.Value).GetNiceName()));
    }

    public static IEnumerable<ValueDropdownItem> GetDerivedTypesNameList(this Type baseType, bool includingSelf,
        bool includingInterfaces, bool includingGeneric, bool includingAbstract, bool includingSealed)
    {
        var resultTypes = new HashSet<Type>();

        foreach (var type in baseType.GetDerivedClasses(includingSelf, false))
        {
            if (type.IsGenericType && includingGeneric == false)
            {
                continue;
            }

            if (type.IsInterface && includingInterfaces == false)
            {
                continue;
            }

            if (type.IsAbstract && includingAbstract == false)
            {
                continue;
            }

            if (type.IsSealed && includingSealed == false)
            {
                continue;
            }

            resultTypes.Add(type);
        }

        return resultTypes.Select(t => new ValueDropdownItem(t.FullName?.Replace(".", "/"), t));
    }
}

#endif