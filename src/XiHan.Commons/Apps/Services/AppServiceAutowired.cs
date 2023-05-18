#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:AppServiceAutowired
// Guid:9a4eb123-327c-4d54-9df2-0f76b5f772d5
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-04-21 下午 01:04:48
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Linq.Expressions;
using System.Reflection;

namespace XiHan.Commons.Apps.Services;

/// <summary>
/// 从容器通过属性或字段自动注入装配服务
/// </summary>
/// <remarks>
/// 参考地址：https://www.cnblogs.com/loogn/p/10566510.html
/// </remarks>
[AppService(IsInterfaceServiceType = false)]
public class AppServiceAutowired
{
    private readonly IServiceProvider _serviceProvider;
    private readonly Dictionary<Type, Action<object, IServiceProvider>> _autowiredActions = new();

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="serviceProvider"></param>
    public AppServiceAutowired(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// 装配属性和字段
    /// </summary>
    /// <param name="service"></param>
    public void Autowired(object service)
    {
        Autowired(service, _serviceProvider);
    }

    /// <summary>
    /// 装配属性和字段
    /// </summary>
    /// <param name="service"></param>
    /// <param name="serviceProvider"></param>
    public void Autowired(object service, IServiceProvider serviceProvider)
    {
        var serviceType = service.GetType();
        if (_autowiredActions.TryGetValue(serviceType, out Action<object, IServiceProvider>? act))
        {
            act(service, serviceProvider);
        }
        else
        {
            //参数
            var objParam = Expression.Parameter(typeof(object), "obj");
            var spParam = Expression.Parameter(typeof(IServiceProvider), "sp");
            var obj = Expression.Convert(objParam, serviceType);
            var getService = typeof(IServiceProvider).GetMethod("GetService");

            List<Expression> setList = new();
            if (getService != null)
            {
                // 字段赋值
                foreach (FieldInfo field in serviceType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
                {
                    var autowiredAttr = field.GetCustomAttribute<ServiceAutowiredAttribute>();
                    if (autowiredAttr != null)
                    {
                        var fieldExp = Expression.Field(obj, field);
                        var createService = Expression.Call(spParam, getService, Expression.Constant(field.FieldType));
                        var setExp = Expression.Assign(fieldExp, Expression.Convert(createService, field.FieldType));
                        setList.Add(setExp);
                    }
                }
                // 属性赋值
                foreach (PropertyInfo property in serviceType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
                {
                    var autowiredAttr = property.GetCustomAttribute<ServiceAutowiredAttribute>();
                    if (autowiredAttr != null)
                    {
                        var propExp = Expression.Property(obj, property);
                        var createService = Expression.Call(spParam, getService, Expression.Constant(property.PropertyType));
                        var setExp = Expression.Assign(propExp, Expression.Convert(createService, property.PropertyType));
                        setList.Add(setExp);
                    }
                }
            }
            var bodyExp = Expression.Block(setList);
            var setAction = Expression.Lambda<Action<object, IServiceProvider>>(bodyExp, objParam, spParam).Compile();
            _autowiredActions[serviceType] = setAction;
            setAction(service, serviceProvider);
        }
    }
}