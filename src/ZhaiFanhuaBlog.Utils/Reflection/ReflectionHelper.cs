#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:ReflectionHelper
// Guid:40401fb6-75d6-4ae8-83c2-11452fe99ee1
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-02-20 下午 03:08:22
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Reflection;

namespace ZhaiFanhuaBlog.Utils.Reflection;

/// <summary>
/// 反射辅助类
/// </summary>
public static class ReflectionHelper
{
    /// <summary>
    /// 获取所有符合条件的程序集。
    /// </summary>
    /// <param name="Prefix"></param>
    /// <param name="Suffix"></param>
    /// <returns></returns>
    public static List<Assembly> GetAssemblies(string Prefix = "ZhaiFanhuaBlog", string Suffix = "dll")
    {
        List<Assembly> result = new();
        string str = AppDomain.CurrentDomain.BaseDirectory;
        DirectoryInfo root = new(str);
        var files = root.GetFiles().ToList();
        var ddlstrs = files.Where(e => e.Name.ToLower().Contains($"{Prefix}.".ToLower()) &&
                                       e.Name.ToLower().Contains($".{Suffix}".ToLower()))
                            .Select(e => e.FullName).ToList();
        ddlstrs.ForEach(e => { result.Add(Assembly.LoadFrom(e)); });
        return result;
    }

    /// <summary>
    /// 获取所有的Type
    /// </summary>
    /// <returns></returns>
    public static List<Type> GetTypes()
    {
        List<Type> types = new();
        List<Assembly> assemblies = GetAssemblies();
        assemblies.ForEach(assembly =>
        {
            types = types.Union(assembly.GetTypes().ToList()).ToList();
        });
        return types;
    }

    /// <summary>
    /// 过滤有xx属性的
    /// </summary>
    /// <typeparam name="TAttribute"></typeparam>
    /// <returns></returns>
    public static List<Type> GetTypes<TAttribute>() where TAttribute : Attribute
    {
        List<Type> types = new();
        List<Assembly> assemblies = GetAssemblies();
        assemblies.ForEach(assembly =>
        {
            types = types.Union(assembly.GetTypes()
                                        .Where(e => !e.CustomAttributes.Any(g => g.AttributeType == typeof(TAttribute)))
                                        .ToList())
                         .ToList();
        });
        return types;
    }

    /// <summary>
    /// 获取XX接口/类的子类（非抽象类）[第一种实现]
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TAttribute"></typeparam>
    /// <returns></returns>
    public static List<Type> GetSubClass<T, TAttribute>() where T : class where TAttribute : Attribute
        => GetTypes<TAttribute>().Where(t => typeof(T).IsAssignableFrom(t)).Where(t => !t.IsAbstract && t.IsClass).ToList();

    /// <summary>
    /// 获取XX接口/类的子类（非抽象类）[第二种实现]
    /// </summary>
    /// <typeparam name="TAttribute"></typeparam>
    /// <param name="T"></param>
    /// <returns></returns>
    public static List<Type> GetSubClass<TAttribute>(Type T) where TAttribute : Attribute
        => GetTypes<TAttribute>().Where(t => T.IsAssignableFrom(t)).Where(t => !t.IsAbstract && t.IsClass).ToList();

    /// <summary>
    /// 获取T的非抽象子类（无视属性）
    /// </summary>
    /// <param name="T"></param>
    /// <returns></returns>
    public static List<Type> GetSubClass(Type T)
        => GetTypes().Where(t => T.IsAssignableFrom(t)).Where(t => !t.IsAbstract && t.IsClass).ToList();

    /// <summary>
    /// 对象转换成字典
    /// </summary>
    /// <typeparam name="TAttribute"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static List<Dictionary<string, dynamic>> FiltrationProp<TAttribute>(object obj) where TAttribute : Attribute
    {
        List<Dictionary<string, dynamic>> result = new List<Dictionary<string, dynamic>>();
        var Objitems = (obj as IEnumerable<dynamic>).Select(e => (e as object).GetType().GetProperties().ToList()).ToList(); // .ToDictionary().ToList();

        (obj as IEnumerable<dynamic>).ToList().ForEach(e =>
        {
            var item = (e as object).GetType().GetProperties()
            .Where(prop => !prop.HasAttribute<TAttribute>() || (prop.HasAttribute<TAttribute>() &&
                !(Attribute.GetCustomAttribute(prop, typeof(TAttribute)) as TAttribute)
                .GetPropertyValue<TAttribute, bool>("IsIgnore"))).ToDictionary(prop => prop.Name, prop => prop.GetValue(e, null));

            result.Add(item);
        });
        return result;
    }

    #region 获取表所有的列

    /// <summary>
    /// 获取表的列
    /// </summary>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public static List<string> GetTableColums(string tableName)
    {
        List<string> result = new();
        GetAssemblies().ForEach(assembly =>
        {
            var _calss = assembly.GetTypes().FirstOrDefault(a => a.Name == tableName);
            if (_calss != null)
            {
                result = _calss.GetProperties().Where(a => a.Name.Contains("Target")).Select(a => a.Name.FirstToLower()).ToList();
            }
        });
        return result;
    }

    #endregion 获取表所有的列

    #region 运行Method

    /// <summary>
    ///
    /// </summary>
    /// <param name="needRunJobInfo"></param>
    /// <returns></returns>
    public static bool RunMethod(JobData needRunJobInfo)
    {
        try
        {
            var DeclaringTypeName = needRunJobInfo.DeclaringTypeName.Split(".").ToList();
            DeclaringTypeName.RemoveAt(DeclaringTypeName.Count - 1);
            // 加载程序集
            var ass = Assembly.Load(string.Join(".", DeclaringTypeName));

            object instance = ass.CreateInstance($"{needRunJobInfo.DeclaringTypeName}");
            Type type = instance.GetType();
            // 获取函数信息
            MethodInfo methodinfo = type.GetMethod(needRunJobInfo.MethodName);
            /// 运行函数（无参）
            methodinfo.Invoke(instance, null);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    #endregion 运行Method

    #region 获取DropDownList数据By Enums

    public static List<DropDownDataDto> GetDropDownDataList(string emnuName)
    {
        List<DropDownDataDto> result = new();
        GetAssemblies().ForEach(assembly =>
        {
            var _calss = assembly.GetTypes().FirstOrDefault(a => a.Name == emnuName);
            if (_calss != null)
            {
                _calss.GetFields(BindingFlags.Static | BindingFlags.Public).ToList().ForEach(enumItem =>
                {
                    result.Add(new DropDownDataDto()
                    {
                        Value = (int)enumItem.GetRawConstantValue(),
                        Label = enumItem.Name,
                    });
                });
            }
        });
        return result;
    }

    #endregion 获取DropDownList数据By Enums
}