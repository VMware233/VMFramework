using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using VMFramework.Core;
using VMFramework.Core.FSM;

namespace VMFramework.Procedure
{
    public partial class ProcedureManager
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasProcedure(string procedureID)
        {
            return procedures.ContainsKey(procedureID);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasCurrentProcedure(string procedureID)
        {
            return _fsm.HasCurrentState(procedureID);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IProcedure GetProcedure(string procedureID)
        {
            return procedures.GetValueOrDefault(procedureID);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IProcedure GetProcedureStrictly(string procedureID)
        {
            if (procedures.TryGetValue(procedureID, out var procedure) == false)
            {
                throw new ArgumentException($"Procedure with ID:{procedureID} does not exist.");
            }

            return procedure;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetProcedure(string procedureID, out IProcedure procedure)
        {
            return procedures.TryGetValue(procedureID, out procedure);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetProcedureWithWarning(string procedureID, out IProcedure procedure)
        {
            if (procedures.TryGetValue(procedureID, out procedure) == false)
            {
                Debugger.LogWarning($"Procedure with ID:{procedureID} does not exist.");
                return false;
            }
            
            return true;
        }
    }
}