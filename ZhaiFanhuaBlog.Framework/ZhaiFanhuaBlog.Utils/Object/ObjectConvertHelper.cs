namespace ZhaiFanhuaBlog.Utils.Object;

/// <summary>
/// 对象转换帮助类
/// </summary>
public static class ObjectConvertHelper
{
    /// <summary>
    /// 对象转数字
    /// </summary>
    /// <param name="thisValue"></param>
    /// <returns></returns>
    public static int ObjToInt(this object thisValue)
    {
        int reval = 0;
        if (thisValue == null) return 0;
        if (thisValue != DBNull.Value && int.TryParse(thisValue.ToString(), out reval))
        {
            return reval;
        }
        return reval;
    }

    /// <summary>
    /// 对象转数字
    /// </summary>
    /// <param name="thisValue"></param>
    /// <param name="errorValue"></param>
    /// <returns></returns>
    public static int ObjToInt(this object thisValue, int errorValue)
    {
        int reval = 0;
        if (thisValue != null && thisValue != DBNull.Value && int.TryParse(thisValue.ToString(), out reval))
        {
            return reval;
        }
        return errorValue;
    }

    /// <summary>
    /// 对象转金额
    /// </summary>
    /// <param name="thisValue"></param>
    /// <returns></returns>
    public static double ObjToMoney(this object thisValue)
    {
        double reval = 0;
        if (thisValue != null && thisValue != DBNull.Value && double.TryParse(thisValue.ToString(), out reval))
        {
            return reval;
        }
        return 0;
    }

    /// <summary>
    /// 对象转金额
    /// </summary>
    /// <param name="thisValue"></param>
    /// <param name="errorValue"></param>
    /// <returns></returns>
    public static double ObjToMoney(this object thisValue, double errorValue)
    {
        double reval = 0;
        if (thisValue != null && thisValue != DBNull.Value && double.TryParse(thisValue.ToString(), out reval))
        {
            return reval;
        }
        return errorValue;
    }

    /// <summary>
    /// 对象转字符串
    /// </summary>
    /// <param name="thisValue"></param>
    /// <returns></returns>
    public static string ObjToString(this object thisValue)
    {
        if (thisValue != null)
            return thisValue.ToString()!.Trim();
        return string.Empty;
    }

    /// <summary>
    /// 对象转字符串
    /// </summary>
    /// <param name="thisValue"></param>
    /// <param name="errorValue"></param>
    /// <returns></returns>
    public static string ObjToString(this object thisValue, string errorValue)
    {
        if (thisValue != null)
            return thisValue.ToString()!.Trim();
        return errorValue;
    }

    /// <summary>
    /// 判断是否为空
    /// </summary>
    /// <param name="thisValue"></param>
    /// <returns></returns>
    public static bool IsNotEmptyOrNull(this object thisValue)
    {
        return ObjToString(thisValue) != "" && ObjToString(thisValue) != "undefined" && ObjToString(thisValue) != "null";
    }

    /// <summary>
    /// 对象转浮点数
    /// </summary>
    /// <param name="thisValue"></param>
    /// <returns></returns>
    public static decimal ObjToDecimal(this object thisValue)
    {
        decimal reval = 0;
        if (thisValue != null && thisValue != DBNull.Value && decimal.TryParse(thisValue.ToString(), out reval))
        {
            return reval;
        }
        return 0;
    }

    /// <summary>
    /// 对象转浮点数
    /// </summary>
    /// <param name="thisValue"></param>
    /// <param name="errorValue"></param>
    /// <returns></returns>
    public static decimal ObjToDecimal(this object thisValue, decimal errorValue)
    {
        decimal reval = 0;
        if (thisValue != null && thisValue != DBNull.Value && decimal.TryParse(thisValue.ToString(), out reval))
        {
            return reval;
        }
        return errorValue;
    }

    /// <summary>
    /// 对象转日期
    /// </summary>
    /// <param name="thisValue"></param>
    /// <returns></returns>
    public static DateTime ObjToDate(this object thisValue)
    {
        DateTime reval = DateTime.MinValue;
        if (thisValue != null && thisValue != DBNull.Value && DateTime.TryParse(thisValue.ToString(), out reval))
        {
            reval = Convert.ToDateTime(thisValue);
        }
        return reval;
    }

    /// <summary>
    /// 对象转日期
    /// </summary>
    /// <param name="thisValue"></param>
    /// <param name="errorValue"></param>
    /// <returns></returns>
    public static DateTime ObjToDate(this object thisValue, DateTime errorValue)
    {
        DateTime reval = DateTime.MinValue;
        if (thisValue != null && thisValue != DBNull.Value && DateTime.TryParse(thisValue.ToString(), out reval))
        {
            return reval;
        }
        return errorValue;
    }

    /// <summary>
    /// 对象转布尔值
    /// </summary>
    /// <param name="thisValue"></param>
    /// <returns></returns>
    public static bool ObjToBool(this object thisValue)
    {
        bool reval = false;
        if (thisValue != null && thisValue != DBNull.Value && bool.TryParse(thisValue.ToString(), out reval))
        {
            return reval;
        }
        return reval;
    }

    /// <summary>
    /// 获取当前时间的时间戳
    /// </summary>
    /// <param name="thisValue"></param>
    /// <returns></returns>
    public static string DateToTimeStamp(this DateTime thisValue)
    {
        TimeSpan ts = thisValue - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return Convert.ToInt64(ts.TotalSeconds).ToString();
    }
}