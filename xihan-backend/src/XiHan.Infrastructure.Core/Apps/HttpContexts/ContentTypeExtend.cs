#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ContentTypeExtend
// Guid:ca5b5d9a-af52-403f-bc17-19fbd52bf7bf
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-08-10 上午 11:09:46
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Collections;
using XiHan.Common.Utilities.Extensions;

namespace XiHan.Infrastructure.Core.Apps.HttpContexts;

/// <summary>
/// 文件的 MIME 类型扩展
/// 通常由两部分组成：主类型和子类型，用斜杠分隔。主类型表示数据的大类，子类型表示主类型下的具体细分。
/// </summary>
public static class ContentTypeExtend
{
    /// <summary>
    /// 获取文件的 MIME 类型的值
    /// </summary>
    /// <param name="contentType"></param>
    /// <returns></returns>
    public static string GetValue(this ContentTypeEnum contentType)
    {
        Hashtable hashtable = new()
        {
            // 纯文本
            { ContentTypeEnum.TextPlain, "text/plain" },
            // HTML 文档
            { ContentTypeEnum.TextHtml, "text/html" },
            // CSS 样式表
            { ContentTypeEnum.TextCss, "text/css" },
            // JavaScript 脚本
            { ContentTypeEnum.TextJavaScript, "text/javascript" },
            // XML 文档
            { ContentTypeEnum.TextXml, "text/xml" },
            // JSON 数据
            { ContentTypeEnum.ApplicationJson, "application/json" },
            // XML 数据
            { ContentTypeEnum.ApplicationXml, "application/xml" },
            // PDF 文档
            { ContentTypeEnum.ApplicationPdf, "application/pdf" },
            // Microsoft Word 文档(97-2003)
            { ContentTypeEnum.ApplicationDoc, "application/msword" },
            // Microsoft Word 文档(2007)
            {ContentTypeEnum.ApplicationDocx,"application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
            // Microsoft Excel 表格(97-2003)
            { ContentTypeEnum.ApplicationXls, "application/vnd.ms-excel" },
            // Microsoft Excel 表格(2007)
            { ContentTypeEnum.ApplicationXlsx, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" },
            // Microsoft PowerPoint 演示文稿(97-2003)
            { ContentTypeEnum.ApplicationPpt, "application/vnd.ms-powerpoint" },
            // Microsoft PowerPoint 演示文稿(2007)
            {ContentTypeEnum.ApplicationPptx,"application/vnd.openxmlformats-officedocument.presentationml.presentation"},
            // ZIP 压缩文件
            { ContentTypeEnum.ApplicationZip, "application/zip" },
            // JPEG 图像
            { ContentTypeEnum.ImageJpeg, "image/jpeg" },
            // PNG 图像
            { ContentTypeEnum.ImagePng, "image/png" },
            // GIF 图像
            { ContentTypeEnum.ImageGif, "image/gif" },
            // SVG 图像
            { ContentTypeEnum.ImageSvg, "image/svg+xml" },
            // MP3 音频
            { ContentTypeEnum.AudioMp3, "audio/mpeg" },
            // WAV 音频
            { ContentTypeEnum.AudioWav, "audio/wav" },
            // MIDI 音频
            { ContentTypeEnum.AudioMidi, "audio/midi" },
            // MP4 视频
            { ContentTypeEnum.VideoMp4, "video/mp4" },
            // MPEG 视频
            { ContentTypeEnum.VideoMpeg, "video/mpeg" },
            // QuickTime 视频
            { ContentTypeEnum.VideoQuickTime, "video/quicktime" },
            // 二进制数据流
            { ContentTypeEnum.ApplicationStream, "application/octet-stream" },
            // 可执行文件下载
            { ContentTypeEnum.ApplicationExecutableFile, "application/x-msdownload" }
        };

        var value = hashtable[contentType].ParseToString();
        return value;
    }
}