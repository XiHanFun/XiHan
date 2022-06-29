// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:ClassSummaryHelper
// Guid:078ecf5c-fc39-4f1a-94d1-34570d9adaf8
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-02 下午 09:11:24
// ----------------------------------------------------------------

namespace ZhaiFanhuaBlog.Utils.Summary;

/// <summary>
/// 类注释帮助类
/// </summary>
public static class ClassSummaryHelper
{
    public static string GetClassSummary(this object obj)
    {
        var type = obj.GetType();
        foreach (var item in type.GetFields())
        {
            //找到对应的字段
            if (item.GetValue(obj)!.Equals(obj))
            {
                // return null;// item.GetXmlDocsSummary();
            };
        }
        return string.Empty;
    }
}