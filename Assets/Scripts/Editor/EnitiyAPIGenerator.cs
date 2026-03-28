using System.IO;
using System.Text;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using System.Linq;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Editor
{
    public class EnitiyAPIGenerator
    {
        private const string AssemblyName = "Assembly-CSharp";

        private static string OutputPath
            => Path.Combine(Application.dataPath, "Scripts/Runtime/Gameplay/EntitiesCore/Generated/EntityAPI.cs");

        [MenuItem("Tools/GenerateEntityAPI")]
        private static void Generate()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"namespace {typeof(Entity).Namespace}");
            sb.AppendLine("{");

            sb.AppendLine($"\tpublic partial class {typeof(Entity).Name}");
            sb.AppendLine("\t{");

            Assembly assembly = Assembly.Load(AssemblyName);

            IEnumerable<Type> componentTypes = GetComponentTypesFrom(assembly);

            foreach (Type componentType in componentTypes)
            {
                string typeName = componentType.Name;
                string fullName = componentType.FullName;

                string componentName = RemoveSuffixIfExists(typeName, "Component");
                string modified = componentName + "Comp";

                sb.AppendLine($"\t\tpublic {fullName} {modified}");
                sb.AppendLine($"\t\t\t=> GetComponent<{fullName}>();");

                if (HasSingleField(componentType, out var field) && field.Name == "Value")
                {
                    sb.AppendLine($"\t\tpublic {GetValidTypeName(field.FieldType)} {componentName}");
                    sb.AppendLine($"\t\t\t=> {modified}.{field.Name};");

                    sb.AppendLine($"\t\tpublic bool TryGet{componentName}(out " +
                        $"{GetValidTypeName(field.FieldType)} {GetVariableNameFrom(field.Name)})");
                    sb.AppendLine("\t\t{");
                    sb.AppendLine($"\t\t\tbool result = TryGetComponent(out {fullName} component);");
                    sb.AppendLine("\t\t\tif (result)");
                    sb.AppendLine($"\t\t\t\t{GetVariableNameFrom(field.Name)} = component.{field.Name};");
                    sb.AppendLine("\t\t\telse");
                    sb.AppendLine($"\t\t\t\t{GetVariableNameFrom(field.Name)} = default({GetValidTypeName(field.FieldType)});");
                    sb.AppendLine("\t\t\treturn result;");
                    sb.AppendLine("\t\t}");

                    if (HasEmptyConstructor(field.FieldType))
                    {
                        string initializer = "{ " + field.Name + " = new " + GetValidTypeName(field.FieldType) + "() }";

                        sb.AppendLine($"\t\tpublic {typeof(Entity).FullName} Add{componentName}()");
                        sb.AppendLine($"\t\t\t=> AddComponent(new {fullName}() {initializer});");
                    }
                }

                string componentParams = GetParameters(componentType);

                sb.AppendLine($"\t\tpublic {typeof(Entity).FullName} Add{componentName}({componentParams})");
                sb.AppendLine($"\t\t\t=> AddComponent(new {fullName}() {GetInitializer(componentType)});");
            }

            sb.AppendLine("\t}");

            sb.AppendLine("}");

            File.WriteAllText(OutputPath, sb.ToString());

            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
        }

        private static bool HasSingleField(Type type, out FieldInfo field)
        {
            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);

            if (fields.Length != 1)
            {
                field = null;
                return false;
            }

            field = fields[0];
            return true;
        }

        private static bool HasEmptyConstructor(Type type)
        {
            return
                type.GetConstructor(Type.EmptyTypes) != null
                && type.IsSubclassOf(typeof(UnityEngine.Object)) == false;
        }

        private static string RemoveSuffixIfExists(string str, string suffix)
        {
            if (str.EndsWith(suffix))
                return str.Substring(0, str.Length - suffix.Length);
            else
                return str;
        }

        private static IEnumerable<Type> GetComponentTypesFrom(Assembly assembly)
        {
            return assembly
                .GetTypes()
                .Where(t => t.IsInterface == false
                    && t.IsAbstract == false
                    && typeof(IEntityComponent).IsAssignableFrom(t));
        }

        private static string GetParameters(Type type)
        {
            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);

            if (fields.Any() == false)
                return "";

            IEnumerable<string> parameters = fields
                .Select(field => $"{GetValidTypeName(field.FieldType)} {GetVariableNameFrom(field.Name)}");

            return string.Join(",", parameters);
        }

        private static string GetInitializer(Type type)
        {
            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);

            if (fields.Any() == false)
                return "";

            IEnumerable<string> initializers = fields
                .Select(field => $"{field.Name} = {GetVariableNameFrom(field.Name)}");

            return "{" + string.Join(",", initializers) + "}";
        }

        private static string GetVariableNameFrom(string name)
            => char.ToLowerInvariant(name[0]) + name.Substring(1);

        private static string GetValidTypeName(Type type)
        {
            if (type.IsGenericType)
            {
                StringBuilder sb = new StringBuilder();

                string fullTypeName = type.FullName;
                var backtickIndex = fullTypeName.IndexOf('`');

                if (backtickIndex >= 0)
                    fullTypeName = fullTypeName.Substring(0, backtickIndex);

                sb.Append(fullTypeName);
                sb.Append("<");

                Type[] genericArgs = type.GetGenericArguments();

                for (int i = 0; i < genericArgs.Length; i++)
                {
                    if (i > 0)
                        sb.Append(", ");

                    sb.Append(GetValidTypeName(genericArgs[i]));
                }

                sb.Append(">");
                return sb.ToString();
            }
            else
            {
                return type.FullName;
            }
        }
    }
}
