#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:JsonHelper
// Guid:227522db-7512-4a80-972c-bbedb715da02
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-11-21 上午 01:06:17
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Text;
using System.Text.Json;

namespace ZhaiFanhuaBlog.Utils.Serialize;

/// <summary>
/// JsonHelper
/// </summary>
public class JsonHelper
{
    /// <summary>
    /// 文件路径
    /// </summary>
    private readonly string _JsonFileName = string.Empty;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="jsonFileName"></param>
    public JsonHelper(string jsonFileName)
    {
        _JsonFileName = jsonFileName;
    }

    /// <summary>
    /// 加载整个文件并反序列化为一个对象
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <returns></returns>
    public TEntity? Get<TEntity>()
    {
        if (!File.Exists(_JsonFileName))
        {
            return default;
        }
        string jsonStr = File.ReadAllText(_JsonFileName, Encoding.UTF8);
        var result = JsonSerializer.Deserialize<TEntity>(jsonStr, SerializeHelper.JsonSerializerOptionsInstance);
        return result;
    }

    /// <summary>
    /// 从Json文件中读取对象
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="keyLink">对象的键名链，例如 Order.User</param>
    /// <returns>类型为TEntity的对象</returns>
    public TEntity? Get<TEntity>(string keyLink)
    {
        if (!File.Exists(_JsonFileName))
        {
            return default;
        }
        using StreamReader streamReader = new(_JsonFileName);
        string jsonStr = streamReader.ReadToEnd();
        dynamic? obj = JsonSerializer.Deserialize<TEntity>(jsonStr, SerializeHelper.JsonSerializerOptionsInstance);
        obj ??= JsonDocument.Parse(JsonSerializer.Serialize(new object()));
        var keys = keyLink.Split('.');
        dynamic currentObject = obj;
        foreach (var key in keys)
        {
            currentObject = currentObject[key];
            if (currentObject == null)
            {
                return default;
            }
        }
        var result = JsonSerializer.Deserialize<TEntity>(currentObject.ToString(), SerializeHelper.JsonSerializerOptionsInstance);
        return result;
    }

    /// <summary>
    /// 设置对象值
    /// </summary>
    /// <typeparam name="REntity"></typeparam>
    /// <param name="keyLink">对象的键名链，例如 Order.User，当其上级不存在时将创建</param>
    /// <param name="value"></param>
    public void Set<TEntity, REntity>(string keyLink, REntity value)
    {
        dynamic? jsoObj;
        string jsonStr = File.ReadAllText(_JsonFileName, Encoding.UTF8);

        Type type = typeof(TEntity);
        jsoObj = JsonSerializer.Deserialize<TEntity>(jsonStr, SerializeHelper.JsonSerializerOptionsInstance);
        jsoObj ??= JsonDocument.Parse(JsonSerializer.Serialize(new object()));

        var keys = keyLink.Split(':');
        dynamic currentObject = jsoObj;
        for (int i = 0; i < keys.Length; i++)
        {
            var oldObject = currentObject;

            var sss = keys[i];

            currentObject = currentObject[keys[i]];
            var isValueType = value!.GetType().IsValueType;
            if (i == keys.Length - 1)
            {
                if (isValueType || value is string)
                {
                    oldObject[keys[i]] = JsonSerializer.Serialize(value);
                }
                else
                {
                    oldObject[keys[i]] = value;
                }
            }
            else
            {
                //如果不存在，新建
                if (currentObject == null)
                {
                    JsonDocument obj = JsonDocument.Parse(JsonSerializer.Serialize(new object()));
                    oldObject[keys[i]] = obj;
                    currentObject = oldObject[keys[i]];
                    continue;
                }
            }
        }
        Save<dynamic>(jsoObj);
    }

    /// <summary>
    /// 保存Json文件
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="jsoObj"></param>
    public void Save<TEntity>(TEntity jsoObj)
    {
        string jsonStr = JsonSerializer.Serialize(jsoObj, SerializeHelper.JsonSerializerOptionsInstance);
        File.WriteAllText(_JsonFileName, jsonStr, Encoding.UTF8);
    }
}