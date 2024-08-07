﻿using System;
using System.Diagnostics;

namespace VMFramework.OdinExtensions
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    [Conditional("UNITY_EDITOR")]
    public sealed class HelperAttribute : Attribute
    {
        public readonly string URL;

        public HelperAttribute(string URL)
        {
            this.URL = URL;
        }
    }
}
