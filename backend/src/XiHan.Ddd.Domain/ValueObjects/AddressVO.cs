#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:AddressVO
// Guid:69f7a5ad-4050-41eb-a080-40faaa15ee65
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/27 1:03:54
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Ddd.Domain.ValueObjects.Bases;

namespace XiHan.Ddd.Domain.ValueObjects;

/// <summary>
/// 地址
/// </summary>
public class AddressVO : ValueObject
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="country"></param>
    /// <param name="province"></param>
    /// <param name="city"></param>
    /// <param name="district"></param>
    /// <param name="street"></param>
    /// <param name="community"></param>
    /// <param name="houseNumber"></param>
    public AddressVO(string country, string province, string city, string district, string street, string community, string houseNumber)
    {
        Country = country;
        Province = province;
        City = city;
        District = district;
        Street = street;
        Community = community;
        HouseNumber = houseNumber;
    }

    /// <summary>
    /// 国家
    /// </summary>
    public string Country { get; private set; }

    /// <summary>
    /// 省/自治区/直辖市/特别行政区
    /// </summary>
    public string Province { get; private set; }

    /// <summary>
    /// 市/地区/自治州/盟
    /// </summary>
    public string City { get; private set; }

    /// <summary>
    /// 区/县/自治县/旗/自治旗/特区/林区
    /// </summary>
    public string District { get; private set; }

    /// <summary>
    /// 镇/乡/民族乡/苏木/民族苏木/县辖区/街道
    /// </summary>
    public string Street { get; private set; }

    /// <summary>
    /// 村/民族村/居民委员会/社区
    /// </summary>
    public string Community { get; private set; }

    /// <summary>
    /// 门牌号码
    /// </summary>
    public string HouseNumber { get; private set; }

    /// <summary>
    /// 获取属性值集合
    /// </summary>
    /// <returns></returns>
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Country;
        yield return Province;
        yield return City;
        yield return District;
        yield return Street;
        yield return Community;
        yield return HouseNumber;
    }
}