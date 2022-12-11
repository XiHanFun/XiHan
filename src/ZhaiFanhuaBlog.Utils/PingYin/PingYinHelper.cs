#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:PingYinHelper
// Guid:67d786c7-de53-41d0-aab6-399ca5b3d43d
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-09-07 上午 03:03:00
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.International.Converters.PinYinConverter;
using System.Text.RegularExpressions;

namespace ZhaiFanhuaBlog.Utils.PingYin;

/// <summary>
/// PingYinHelper
/// </summary>
public static class PingYinHelper
{
    /// <summary>
    /// 获取汉语拼音全拼（含多音字多次匹配）
    /// </summary>
    /// <param name="text">The string.</param>
    /// <returns></returns>
    public static List<string> GetTotalPingYin(this string text)
    {
        var result = new List<string>();
        foreach (var pinyins in GetTotalPingYinDictionary(text))
        {
            var items = pinyins.Value;
            if (!result.Any())
            {
                result = items;
            }
            else
            {
                // 全拼循环匹配
                var newTotalPingYinList = new List<string>();
                foreach (var totalPingYin in result)
                {
                    newTotalPingYinList.AddRange(items.Select(item => string.Concat(totalPingYin, " ", item)));
                }
                newTotalPingYinList = newTotalPingYinList.Distinct().ToList();
                result = newTotalPingYinList;
            }
        }
        return result;
    }

    /// <summary>
    /// 获取汉语拼音首字母（含多音字多次匹配）
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static List<string> GetFirstPingYin(this string text)
    {
        var result = new List<string>();
        foreach (var pinyins in GetTotalPingYinDictionary(text))
        {
            var items = pinyins.Value;
            if (!result.Any())
            {
                result = items.ConvertAll(p => p[..1]).Distinct().ToList();
            }
            else
            {
                // 首字母循环匹配
                var newFirstPingYinList = new List<string>();
                foreach (var firstPingYin in result)
                {
                    newFirstPingYinList.AddRange(items.Select(item => string.Concat(firstPingYin, " ", item.AsSpan(0, 1))));
                }
                newFirstPingYinList = newFirstPingYinList.Distinct().ToList();
                result = newFirstPingYinList;
            }
        }
        return result;
    }

    /// <summary>
    /// 获取所有汉字的拼音（含多音）字典
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    private static Dictionary<int, List<string>> GetTotalPingYinDictionary(string text)
    {
        var chs = text.ToCharArray();
        // 记录每个汉字的全拼
        var totalPingYinList = new Dictionary<int, List<string>>();
        for (int i = 0; i < chs.Length; i++)
        {
            // 每个字的所有拼音
            var pinyinList = new List<string>();
            // 是否是有效的汉字
            if (ChineseChar.IsValidChar(chs[i]))
            {
                ChineseChar cc = new(chs[i]);
                pinyinList = cc.Pinyins.Where(pinyin => !string.IsNullOrWhiteSpace(pinyin)).ToList();
            }
            else
            {
                pinyinList.Add(chs[i].ToString());
            }
            // 去除声调并转小写
            pinyinList = pinyinList.ConvertAll(pinyin => Regex.Replace(pinyin, @"\d", string.Empty).ToLower());
            // 去掉因声调（或轻声）不同而产生的重复，不去多音
            pinyinList = pinyinList.Where(pinyin => !string.IsNullOrWhiteSpace(pinyin)).Distinct().ToList();
            if (pinyinList.Any())
            {
                totalPingYinList[i] = pinyinList;
            }
        }
        return totalPingYinList;
    }
}