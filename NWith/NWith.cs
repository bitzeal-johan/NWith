using System;
using System.Reflection;

namespace NWith
{
    public static class NWith
    {
        private static MemberWiseCloneDelegate _memberwiseClone;

        private delegate object MemberWiseCloneDelegate(object instance);

        public static T With<T>(this T self, string fieldName, object newValue)
        {
            if (self == null)
                return default(T);

            if (_memberwiseClone == null)
            {
                _memberwiseClone = (MemberWiseCloneDelegate)Delegate.CreateDelegate(typeof(MemberWiseCloneDelegate),
                    typeof(object).GetMethod("MemberwiseClone", BindingFlags.Instance | BindingFlags.NonPublic) ?? throw new InvalidOperationException("Could not bind to MemberwiseClone"));
            }

            var clone = (T)_memberwiseClone(self);

            var field = typeof(T).GetField
                (fieldName, BindingFlags.Instance | BindingFlags.Public);
            field?.SetValue(clone, newValue);

            return clone;
        }
    }
}
