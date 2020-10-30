namespace D72TP1P3.Models.XML {
    using System;
    [AttributeUsage(AttributeTargets.Property)]
    public class MatchParentAttribute : Attribute {
        public readonly string ParentPropertyName;
        public MatchParentAttribute(string parentPropertyName) {
            this.ParentPropertyName = parentPropertyName;
        }
    }
    public static class ObjectExtensionMethods {
        public static void CopyPropertiesFrom(this object self, object parent) {
            System.Reflection.PropertyInfo[] fromProperties = parent.GetType().GetProperties();
            System.Reflection.PropertyInfo[] toProperties = self.GetType().GetProperties();
            foreach (System.Reflection.PropertyInfo fromProperty in fromProperties) {
                foreach (System.Reflection.PropertyInfo toProperty in toProperties) {
                    if (fromProperty.Name.ToUpper() == toProperty.Name.ToUpper() && fromProperty.PropertyType == toProperty.PropertyType) {
                        toProperty.SetValue(self, fromProperty.GetValue(parent));
                        break;
                    }
                }
            }
        }
        public static void MatchPropertiesFrom(this object self, object parent) {
            System.Reflection.PropertyInfo[] childProperties = self.GetType().GetProperties();
            foreach (System.Reflection.PropertyInfo childProperty in childProperties) {
                object[] attributesForProperty = childProperty.GetCustomAttributes(typeof(MatchParentAttribute), true);
                bool isOfTypeMatchParentAttribute = false;
                MatchParentAttribute currentAttribute = null;
                foreach (object attribute in attributesForProperty) {
                    if (attribute.GetType() == typeof(MatchParentAttribute)) {
                        isOfTypeMatchParentAttribute = true;
                        currentAttribute = (MatchParentAttribute)attribute;
                        break;
                    }
                }
                if (isOfTypeMatchParentAttribute) {
                    System.Reflection.PropertyInfo[] parentProperties = parent.GetType().GetProperties();
                    object parentPropertyValue = null;
                    foreach (System.Reflection.PropertyInfo parentProperty in parentProperties) {
                        if (parentProperty.Name == currentAttribute.ParentPropertyName) {
                            if (parentProperty.PropertyType == childProperty.PropertyType) {
                                parentPropertyValue = parentProperty.GetValue(parent);
                            }
                        }
                    }
                    childProperty.SetValue(self, parentPropertyValue);
                }
            }
        }
    }
}
