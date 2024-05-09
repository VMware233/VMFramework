using System;
using System.Linq;
using System.Reflection;
using VMFramework.Configuration;
using VMFramework.Core;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;

[JsonObject(MemberSerialization.OptIn)]
public sealed class EnumSetter<T> : ObjectChooser<T> where T : Enum
{
    #region GUI

    protected override WeightedSelectItem AddWeightedSelectItemGUI()
    {
        if (ContainsAllEnumValues())
        {
            return null;
        }

        return new()
        {
            value = Enum.GetValues(typeof(T)).Cast<T>().Except(weightedSelectItems.Select(item => item.value)).Choose(),
            ratio = 1
        };
    }

    [Button("添加所有可能")]
    [ShowIf(@"@isRandomValue && randomType == ""Weighted Select"" && ContainsAllEnumValues() == false")]
    private void AddAllEnumValues()
    {
        if (typeof(T).IsEnum == false)
        {
            return;
        }

        foreach (var enumValue in Enum.GetValues(typeof(T)))
        {
            if (weightedSelectItems.Any(item => item.value.Equals(enumValue)))
            {
                continue;
            }

            weightedSelectItems.Add(new()
            {
                value = (T)enumValue,
                ratio = 1
            });
        }

        OnWeightedSelectItemsChanged();
    }

    private bool ContainsAllEnumValues()
    {
        return Enum.GetValues(typeof(T)).Cast<object>().
            All(enumValue => weightedSelectItems.Any(item => item.value.Equals(enumValue)));
    }

    protected override string ValueToString(T value)
    {
        var labelText = typeof(T).GetField(value.ToString())
            .GetCustomAttribute<LabelTextAttribute>();
        return labelText == null ? value.ToString() : labelText.Text;
    }

    protected override void OnInspectorInit()
    {
        base.OnInspectorInit();

        if (typeof(T).IsEnum == false)
        {
            Debug.LogWarning($"{nameof(T)}不是Enum类型不能放在EnumSetter里");
        }
    }

    #endregion

    public static implicit operator EnumSetter<T>(T enumValue)
    {
        return new()
        {
            isRandomValue = false,
            value = enumValue
        };
    }
}
