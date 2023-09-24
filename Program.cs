using System;
using System.Reflection;

namespace CyberHW6_5
{
    class Program
    {

        public string PathInput()
        {
            string filePath;
            bool IsCorrect = false;
            do
            {
                Console.WriteLine("Enter path to (.dll) file: ");
                filePath = Console.ReadLine();
                if (filePath.EndsWith(".dll") || filePath.EndsWith(".exe")) IsCorrect = true;
            } while (!IsCorrect);
            return filePath;
        }
        
        public void UserMenuOutput(bool IsOnAttributes, bool IsOnTypes, bool IsOnMethods, bool IsOnFields, bool IsFileInit)
        {
            
            if(IsFileInit)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;
            }
            Console.WriteLine("1 - Load file");
            Console.ResetColor();
            if (IsOnAttributes)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.WriteLine("2 On/Off Attributes");
            Console.ResetColor();

            if (IsOnTypes)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.WriteLine("3 On/Off Types");
            Console.ResetColor();

            if (IsOnMethods)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.WriteLine("4 On/Off Methods");
            Console.ResetColor();

            if (IsOnFields)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.WriteLine("5 On/Off Fields");
            Console.ResetColor();
            if(IsFileInit)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.WriteLine("6 - Show reflection");
            Console.ResetColor();

            Console.WriteLine("0 - End");
        }
       
        public void ShowManager(Assembly assembly, bool IsOnAttributes, bool IsOnType, bool IsOnMethods, bool IsOnFields)
        {
            Type[] types = assembly.GetTypes();

            foreach (Type type in types)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(new string('#', 70));
                Console.ResetColor();
                if (IsOnType)
                {
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    ShowTypeInfo(type, IsOnAttributes);
                    Console.ResetColor();
                }
                    Console.WriteLine("{");
                if (IsOnFields)
                {
                    var fields = type.GetFields();

                    if (fields != null)
                    {
                        foreach (var field in fields)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            ShowFieldInfo(field, IsOnAttributes);
                            Console.ResetColor();
                            Console.WriteLine();
                        }
                    }
                    Console.WriteLine();
                }

                if (IsOnMethods)
                {
                    var methods = type.GetMethods();
                    if (methods != null)
                    {
                        foreach (var method in methods)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            ShowMethodInfo(method, IsOnAttributes);
                            Console.ResetColor();
                            Console.WriteLine();
                        }
                    }
                }
                Console.WriteLine("}");
            }
        }
        
        public void ShowTypeInfo(Type type, bool IsOnAttributes)
        {
            string coutType = "";

            if (IsOnAttributes) coutType += AttributeInfo(type);

            if (type.IsNotPublic)
            {
                coutType += "NotPublic ";
            }
            if (type.IsPublic)
            {
                coutType += "public ";
            }

            if (type.IsSealed)
            {
                coutType += "sealed ";
            }
            if (type.IsAbstract)
            {
                coutType += "abstract ";
            }
            if (type.IsGenericType)
            {
                coutType += "generic ";
            }

            if (type.IsClass)
            {
                coutType += "class ";
            }
            else if (type.IsEnum)
            {
                coutType += "enum ";
            }
            else if (type.IsInterface)
            {
                coutType += "interface ";
            }
            else if (type.IsAutoClass)
            {
                coutType += "autoClass ";
            }
            else if (type.IsValueType)
            {
                coutType += "struct ";
            }

            coutType += type.Name;
            
            Console.WriteLine(coutType);
        }
        public void ShowMethodInfo(MethodInfo method, bool IsOnAttributes)
        {
            string coutMethod = "";
            if (IsOnAttributes) coutMethod +=  AttributeInfo(method);

            if (method.IsPrivate)
            {
                coutMethod += "private ";
            }
            if (method.IsPublic)
            {
                coutMethod += "public ";
            }

            if (method.IsStatic)
            {
                coutMethod += "static ";
            }
            if (method.IsAbstract)
            {
                coutMethod += "abstract ";
            }
            if (method.IsVirtual)
            {
                coutMethod += "virtual ";
            }
            if (method.IsGenericMethod)
            {
                coutMethod += "generic ";
            }
            coutMethod += method.ReturnType.Name + " " + method.Name + "(";
            var parameters = method.GetParameters();
            foreach (var param in parameters)
            {
                coutMethod += param.ParameterType.Name + " " + param.Name + ",";
            }
            coutMethod += ")";

            Console.WriteLine(coutMethod);
        }
        public void ShowFieldInfo(FieldInfo field, bool IsOnAttributes)
        {
            string coutField = "";
            if(IsOnAttributes)coutField += AttributeInfo(field);

            if (field.IsPrivate)
            {
                coutField += "private ";
            }
            if (field.IsPublic)
            {
                coutField += "public ";
            }

            if (field.IsStatic)
            {
                coutField += "static ";
            }
            if (field.IsLiteral)
            {
                coutField += "const ";
            }
            else if (field.IsInitOnly)
            {
                coutField += "readonly ";
            }
            coutField += field.FieldType.Name + " " + field.Name + ";";

            Console.WriteLine(coutField);
        }
       
        public string? AttributeInfo(Type type)
        {
            string cout = null;
            var attributes = type.GetCustomAttributes();
           
            foreach (var attribute in attributes)
            {
                cout += "[" + attribute + "]\n";
            }
            return cout;
        }
        public string? AttributeInfo(MethodInfo method)
        {
            string cout = null;
            var attributes = method.GetCustomAttributes();

            foreach (var attribute in attributes)
            {
                cout += "[" + attribute + "]\n";
            }
            return cout;
        }
        public string? AttributeInfo(FieldInfo field)
        {
            string cout = null;
            var attributes = field.GetCustomAttributes();

            foreach (var attribute in attributes)
            {
                cout += "[" + attribute + "]\n";
            }
            return cout;
        }

        public void Main(string[] args)
        {
            Console.CursorVisible = false;
            Assembly assembly = null;

            bool IsOnAttributes = true;
            bool IsOnTypes = true;
            bool IsOnMethods = true;
            bool IsOnFields = true;
            bool IsFileInit = false;
            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine(new string('-', 70));
                UserMenuOutput(IsOnAttributes, IsOnTypes, IsOnMethods, IsOnFields, IsFileInit);
                Console.WriteLine(new string('-', 70));
                key = Console.ReadKey(true);
                Console.Clear();

                switch (key.Key)
                {
                    case ConsoleKey.D0:
                        {
                        }break;
                    case ConsoleKey.D1:
                        {
                            assembly = Assembly.LoadFile(@PathInput());
                            IsFileInit = true;
                        }
                        break;
                    case ConsoleKey.D2:
                        {
                            if (IsOnAttributes) IsOnAttributes = false;
                            else IsOnAttributes = true;
                        }
                        break;
                    case ConsoleKey.D3:
                        {
                            if (IsOnTypes) IsOnTypes = false;
                            else IsOnTypes = true;
                        }
                        break;
                    case ConsoleKey.D4:
                        {
                            if (IsOnMethods) IsOnMethods = false;
                            else IsOnMethods = true;
                        }
                        break;
                    case ConsoleKey.D5:
                        {
                            if (IsOnFields) IsOnFields = false;
                            else IsOnFields = true;
                        }
                        break;
                    case ConsoleKey.D6:
                        {
                            if(IsFileInit)
                            {
                                ShowManager(assembly, IsOnAttributes, IsOnTypes, IsOnMethods, IsOnFields);
                            }
                            else 
                            {
                                Console.WriteLine("!!!     Empty file path");
                            }
                        }
                        break;
                    default:
                        {
                        }break;
                }
            } while (key.KeyChar != '0');
        }
    }
}
