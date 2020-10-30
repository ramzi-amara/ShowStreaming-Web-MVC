namespace System {
    using System.ComponentModel.DataAnnotations;
    public static class EnumHelper<T> {
        public static T GetValueFromName(string name) {
            var type = typeof(T);
            if (!type.IsEnum) {
                throw new InvalidOperationException();
            }
            foreach (var field in type.GetFields()) {
                if (Attribute.GetCustomAttribute(field, typeof(DisplayAttribute)) is DisplayAttribute attribute) {
                    if (attribute.Name == name) {
                        return (T)field.GetValue(null);
                    }
                }
                else {
                    if (field.Name == name) {
                        return (T)field.GetValue(null);
                    }
                }
            }
            throw new ArgumentOutOfRangeException("name");
        }
    }
}