using System.ComponentModel.DataAnnotations;

namespace XiHan.Services.Syses.Configs.Dtos;

public class SysConfigCDto
{
    /// <summary>
    /// 分组编码
    ///</summary>
    [Required]
    public string GroupCode { get; set; } = string.Empty;

    /// <summary>
    /// 配置编码
    ///</summary>
    [Required]
    public string ConfigCode { get; set; } = string.Empty;

    /// <summary>
    /// 配置名称
    /// </summary>
    [Required]
    public string ConfigName { get; set; } = string.Empty;

    /// <summary>
    /// 配置项值
    /// </summary>
    [Required]
    public string ConfigValue { get; set; } = string.Empty;

    /// <summary>
    /// 是否系统内置
    /// </summary>
    [Required]
    public bool IsOfficial { get; set; } = false;

    /// <summary>
    /// 字典描述
    /// </summary>
    public string? Description { get; set; }
}