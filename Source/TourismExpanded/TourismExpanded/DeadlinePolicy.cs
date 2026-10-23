using System;
using System.Reflection;
using Contracts;

namespace TourismExpanded
{
    internal static class DeadlinePolicy
    {
        private static readonly FieldInfo DeadlineTypeField = RequireField("deadlineType");
        private static readonly FieldInfo DeadlineDateField = RequireField("dateDeadline");

        private static FieldInfo RequireField(string name)
        {
            return typeof(Contract).GetField(name, BindingFlags.Instance | BindingFlags.NonPublic)
                ?? throw new MissingFieldException(typeof(Contract).FullName, name);
        }

        internal static bool Apply(Contract contract, bool deadlinesEnabled)
        {
            if (deadlinesEnabled) return false;
            bool changed = (Contract.DeadlineType)DeadlineTypeField.GetValue(contract) != Contract.DeadlineType.None
                || contract.TimeDeadline != 0 || (double)DeadlineDateField.GetValue(contract) != 0;
            DeadlineTypeField.SetValue(contract, Contract.DeadlineType.None);
            contract.TimeDeadline = 0;
            DeadlineDateField.SetValue(contract, 0d);
            return changed;
        }
    }
}
