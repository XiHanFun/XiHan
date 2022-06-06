// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IBaseEntity
// Guid:c54f6677-db24-4a58-91a8-97bf61cfdd27
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-05 上午 12:42:39
// ----------------------------------------------------------------

namespace ZhaiFanhuaBlog.Models.Bases;

/// <summary>
/// IBaseEntity
/// </summary>
public interface IBaseEntity<TKey>
{
    TKey BaseId { get; set; }
}