#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ContentTypeEnum
// Guid:8ad901d7-755a-4564-8861-a462b6ebe260
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/29 4:46:54
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Infrastructure.Core.Apps.HttpContexts;

/// <summary>
/// 文件的 MIME 类型
/// </summary>
public enum ContentTypeEnum
{
    #region 文本类型

    /// <summary>
    /// 纯文本
    /// </summary>
    TextPlain,

    /// <summary>
    /// HTML 文档
    /// </summary>
    TextHtml,

    /// <summary>
    /// CSS 样式表
    /// </summary>
    TextCss,

    /// <summary>
    /// JavaScript 脚本
    /// </summary>
    TextJavaScript,

    /// <summary>
    /// XML 文档
    /// </summary>
    TextXml,

    #endregion

    #region 应用程序类型

    /// <summary>
    /// JSON 数据
    /// </summary>
    ApplicationJson,

    /// <summary>
    /// XML 数据
    /// </summary>
    ApplicationXml,

    /// <summary>
    /// PDF 文档
    /// </summary>
    ApplicationPdf,

    /// <summary>
    /// Microsoft Word 文档(97-2003)
    /// </summary>
    ApplicationDoc,

    /// <summary>
    /// Microsoft Word 文档(2007)
    /// </summary>
    ApplicationDocx,

    /// <summary>
    /// Microsoft Excel 表格(97-2003)
    /// </summary>
    ApplicationXls,

    /// <summary>
    /// Microsoft Excel 表格(2007)
    /// </summary>
    ApplicationXlsx,

    /// <summary>
    /// Microsoft PowerPoint 演示文稿(97-2003)
    /// </summary>
    ApplicationPpt,

    /// <summary>
    /// Microsoft PowerPoint 演示文稿(2007)
    /// </summary>
    ApplicationPptx,

    /// <summary>
    /// ZIP 压缩文件
    /// </summary>
    ApplicationZip,

    #endregion

    #region 图像类型

    /// <summary>
    /// JPEG 图像
    /// </summary>
    ImageJpeg,

    /// <summary>
    /// PNG 图像
    /// </summary>
    ImagePng,

    /// <summary>
    /// GIF 图像
    /// </summary>
    ImageGif,

    /// <summary>
    /// SVG 图像
    /// </summary>
    ImageSvg,

    #endregion

    #region 音频类型

    /// <summary>
    /// MP3 音频
    /// </summary>
    AudioMp3,

    /// <summary>
    /// WAV 音频
    /// </summary>
    AudioWav,

    /// <summary>
    /// MIDI 音频
    /// </summary>
    AudioMidi,

    #endregion

    #region 视频类型

    /// <summary>
    /// MP4 视频
    /// </summary>
    VideoMp4,

    /// <summary>
    /// MPEG 视频
    /// </summary>
    VideoMpeg,

    /// <summary>
    /// QuickTime 视频
    /// </summary>
    VideoQuickTime,

    #endregion

    #region 应用程序下载类型

    /// <summary>
    /// 二进制数据流
    /// </summary>
    ApplicationStream,

    /// <summary>
    /// 可执行文件下载
    /// </summary>
    ApplicationExecutableFile,

    #endregion
}