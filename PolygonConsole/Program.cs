using System;
using System.Dynamic;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;

namespace PolygonConsole
{
    //[Serializable]
    internal class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            //EFTest.Test();

            const string asm_name = "ReflectionTestLib.dll";

            var asm_file = new FileInfo(asm_name);

            Assembly asm = Assembly.LoadFile(asm_file.FullName);

            Type[] exported_types = asm.ExportedTypes.ToArray();
            Type[] defined_types = asm.DefinedTypes.ToArray();

            Type first_type = asm.GetType("ReflectionTestLib.FirstClass");
            Type program_type = typeof(Program);
            Type type_of_type = first_type.GetType();

            ConstructorInfo first_type_ctor_info = first_type.GetConstructors().First();
            FieldInfo[] first_type_fields = first_type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            PropertyInfo[] first_type_properties = first_type.GetProperties();
            MethodInfo[] first_type_methods = first_type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            EventInfo[] first_type_events = first_type.GetEvents();

            var data_field = first_type.GetField("_Data", BindingFlags.Instance | BindingFlags.NonPublic);
            var private_method = first_type.GetMethod("PrivatePrint", BindingFlags.Instance | BindingFlags.NonPublic);

            var obj1 = first_type_ctor_info.Invoke(new object[] { "Hello World" });
            var obj = Activator.CreateInstance(first_type, "Hello world!!111");

            data_field.SetValue(obj, "qwe");

            var str_value = (string)data_field.GetValue(obj1);

            private_method.Invoke(obj, new object[0]);

            //var action_delegate = (Action)Delegate.CreateDelegate(typeof(Action), private_method, false);
            //action_delegate();

            Expression<Func<string, int>> test_expr = str => str.Length;

            var test_expr_function = test_expr.Compile();
            var result = test_expr_function("Hello World!");

            var tree_parameter = Expression.Parameter(typeof(string), "str");
            Expression<Func<string, int>> invoke_tree = Expression.Lambda<Func<string, int>>(
                Expression.Property(tree_parameter, "Length"), tree_parameter);

            var test_expr_function2 = invoke_tree.Compile();
            var result2 = test_expr_function2("Hello World!");

            //System.Reflection.Emit.AssemblyBuilder

            //var another_domain = AppDomain.CreateDomain("qwe");
            //another_domain.IsFinalizingForUnload()

            //dynamic dynamic_var = "Hello world";
            //var str_len = (int)dynamic_var.Length;
            //dynamic_var = 42;
            //var int_result = (int) (dynamic_var + 3);
            //dynamic_var = true;
            //if(!((bool)dynamic_var)) Console.WriteLine("123");
            //DynamicObject @do;

            GC.Collect();

            //System.Runtime.Serialization.Formatters.Binary.BinaryFormatter

            var dis_obj = new DisposableObj();
            var clone_obj = (DisposableObj)dis_obj.Clone();
            var clonr_obj2 = dis_obj.CloneObject<DisposableObj>();

            dis_obj.Dispose();



            int? ref_int_var = 5;
            Nullable<int> ref_int_var2 = 5;

            int int_var = (int) ref_int_var;
            int_var = ref_int_var.Value;

            using (var d_obj = new DisposableObj())
            {

            }

            Console.ReadLine();
        }

        private static int GetLength(string str)
        {
            return str.Length;
        }
    }

    internal class DisposableObj : IDisposable, ICloneable
    {
        public DisposableObj() { }

        ~DisposableObj() { Dispose(false); }

        public object Clone()
        {
            return new DisposableObj();
        }

        protected virtual void Dispose(bool Disposing)
        {
            if (Disposing)
            {
                // выполняем очистку памяти нашего объекта
            }
        }

        private bool _Disposed;
        public void Dispose()
        {
            if (!_Disposed)
            {
                _Disposed = true;
                Dispose(true);
                GC.SuppressFinalize(this);
            }
        }
    }

    internal class DisposableObj2 : DisposableObj
    {
        protected override void Dispose(bool Disposing)
        {
            base.Dispose(Disposing);
            if (Disposing)
            {
                // Очищаем дочерний класс
            }
        }
    }

    public static class CloneableExtensions
    {
        public static T CloneObject<T>(this ICloneable obj)
        {
            return (T) obj.Clone();
        }
    }
}
