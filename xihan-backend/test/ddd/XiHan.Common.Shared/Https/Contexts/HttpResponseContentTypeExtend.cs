#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:HttpResponseContentTypeExtend
// Guid:ca5b5d9a-af52-403f-bc17-19fbd52bf7bf
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-08-10 上午 11:09:46
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Collections;
using XiHan.Common.Shared.Https.Enums;
using XiHan.Common.Utilities.Extensions;

namespace XiHan.Common.Shared.Https.Contexts;

/// <summary>
/// 文件的 MIME 类型扩展
/// 通常由两部分组成：主类型和子类型，用斜杠分隔。主类型表示数据的大类，子类型表示主类型下的具体细分。
/// </summary>
public static class HttpResponseContentTypeExtend
{
    /// <summary>
    /// 获取文件的 MIME 类型的值
    /// </summary>
    /// <param name="contentType"></param>
    /// <returns></returns>
    public static string GetValue(this HttpResponseContentTypeEnum contentType)
    {
        Hashtable hashtable = new()
        {
            // 纯文本
            { HttpResponseContentTypeEnum.TextPlain, "text/plain" },
            // HTML 文档
            { HttpResponseContentTypeEnum.TextHtml, "text/html" },
            // CSS 样式表
            { HttpResponseContentTypeEnum.TextCss, "text/css" },
            // JavaScript 脚本
            { HttpResponseContentTypeEnum.TextJavaScript, "text/javascript" },
            // XML 文档
            { HttpResponseContentTypeEnum.TextXml, "text/xml" },
            // JSON 数据
            { HttpResponseContentTypeEnum.ApplicationJson, "application/json" },
            // XML 数据
            { HttpResponseContentTypeEnum.ApplicationXml, "application/xml" },
            // PDF 文档
            { HttpResponseContentTypeEnum.ApplicationPdf, "application/pdf" },
            // Microsoft Word 文档(97-2003)
            { HttpResponseContentTypeEnum.ApplicationDoc, "application/msword" },
            // Microsoft Word 文档(2007)
            {HttpResponseContentTypeEnum.ApplicationDocx,"application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
            // Microsoft Excel 表格(97-2003)
            { HttpResponseContentTypeEnum.ApplicationXls, "application/vnd.ms-excel" },
            // Microsoft Excel 表格(2007)
            { HttpResponseContentTypeEnum.ApplicationXlsx, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" },
            // Microsoft PowerPoint 演示文稿(97-2003)
            { HttpResponseContentTypeEnum.ApplicationPpt, "application/vnd.ms-powerpoint" },
            // Microsoft PowerPoint 演示文稿(2007)
            {HttpResponseContentTypeEnum.ApplicationPptx, "application/vnd.openxmlformats-officedocument.presentationml.presentation"},
            // ZIP 压缩文件
            { HttpResponseContentTypeEnum.ApplicationZip, "application/zip" },
            // JPEG 图像
            { HttpResponseContentTypeEnum.ImageJpeg, "image/jpeg" },
            // PNG 图像
            { HttpResponseContentTypeEnum.ImagePng, "image/png" },
            // GIF 图像
            { HttpResponseContentTypeEnum.ImageGif, "image/gif" },
            // SVG 图像
            { HttpResponseContentTypeEnum.ImageSvg, "image/svg+xml" },
            // MP3 音频
            { HttpResponseContentTypeEnum.AudioMp3, "audio/mpeg" },
            // WAV 音频
            { HttpResponseContentTypeEnum.AudioWav, "audio/wav" },
            // MIDI 音频
            { HttpResponseContentTypeEnum.AudioMidi, "audio/midi" },
            // MP4 视频
            { HttpResponseContentTypeEnum.VideoMp4, "video/mp4" },
            // MPEG 视频
            { HttpResponseContentTypeEnum.VideoMpeg, "video/mpeg" },
            // QuickTime 视频
            { HttpResponseContentTypeEnum.VideoQuickTime, "video/quicktime" },
            // 二进制数据流
            { HttpResponseContentTypeEnum.ApplicationStream, "application/octet-stream" },
            // 可执行文件下载
            { HttpResponseContentTypeEnum.ApplicationExecutableFile, "application/x-msdownload" }
        };

        var value = hashtable[contentType].ParseToString();
        return value;
    }
}