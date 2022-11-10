using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZhaiFanhuaBlog.Api.Controllers.Bases;
using ZhaiFanhuaBlog.Extensions.Common.Swagger;
using ZhaiFanhuaBlog.Services.Utils.MessagePush;
using ZhaiFanhuaBlog.Utils.MessagePush.DingTalk;
using ZhaiFanhuaBlog.ViewModels.Bases.Results;

namespace ZhaiFanhuaBlog.Api.Controllers.Utils.MessagePush;

/// <summary>
/// 钉钉消息推送
/// <code>包含：文本/链接/文档/任务卡片/卡片菜单</code>
/// </summary>
[AllowAnonymous]
[ApiExplorerSettings(GroupName = SwaggerGroup.Common)]
public class DingTalkController : BaseApiController
{
    private readonly IDingTalkMessagePush _IDingTalkMessagePush;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="iDingTalkMessagePush"></param>
    public DingTalkController(IDingTalkMessagePush iDingTalkMessagePush)
    {
        _IDingTalkMessagePush = iDingTalkMessagePush;
    }

    /// <summary>
    /// 文本
    /// </summary>
    /// <returns></returns>
    [HttpPost("Text")]
    public async Task<BaseResultDto> DingTalkToText()
    {
        string keyWord = "消息提醒";
        Text text = new()
        {
            Content = keyWord + "看万山红遍，层林尽染；漫江碧透，百舸争流。"
        };
        List<string> atMobiles = new() { "1302873****" };
        bool isAtAll = false;
        return await _IDingTalkMessagePush.DingTalkToText(text, atMobiles, isAtAll);
    }

    /// <summary>
    /// 链接
    /// </summary>
    /// <returns></returns>
    [HttpPost("Link")]
    public async Task<BaseResultDto> DingTalkToLink()
    {
        string keyWord = "消息提醒";
        Link link = new()
        {
            Title = keyWord + "时代在召唤",
            Text = "这个即将发布的新版本，创始人陈航（花名“无招”）称它为“红树林”。而在此之前，每当面临重大升级，产品经理们都会取一个应景的代号，这一次，为什么是“红树林”？",
            PicUrl = "https://img.alicdn.com/tfs/TB1NwmBEL9TBuNjy1zbXXXpepXa-2400-1218.png",
            MessageUrl = "https://www.dingtalk.com/"
        };

        return await _IDingTalkMessagePush.DingTalkToLink(link);
    }

    /// <summary>
    /// 文档
    /// </summary>
    /// <returns></returns>
    [HttpPost("Markdown")]
    public async Task<BaseResultDto> DingTalkToMarkdown()
    {
        string keyWord = "消息提醒";
        Markdown markdown = new()
        {
            Title = keyWord + "长沙天气",
            Text = "#### 长沙天气 \n" +
                    "> 8度，西北风3级，空气优16，相对湿度100%\n\n" +
                    "> ![screenshot](https://gw.alipayobjects.com/zos/skylark-tools/public/files/84111bbeba74743d2771ed4f062d1f25.png)\n" +
                    "> ###### 15点03分发布 [天气](https://www.seniverse.com/) \n",
        };
        List<string> atMobiles = new() { "1302873****" };
        bool isAtAll = false;
        return await _IDingTalkMessagePush.DingTalkToMarkdown(markdown, atMobiles, isAtAll);
    }

    /// <summary>
    /// 任务卡片(整体跳转)
    /// </summary>
    /// <returns></returns>
    [HttpPost("WholeActionCard")]
    public async Task<BaseResultDto> DingTalkToWholeActionCard()
    {
        string keyWord = "消息提醒";
        ActionCard actionCard = new()
        {
            Title = keyWord + "乔布斯 20 年前想打造一间苹果咖啡厅，而它正是 Apple Store 的前身",
            Text = "![screenshot](https://gw.alipayobjects.com/zos/skylark-tools/public/files/84111bbeba74743d2771ed4f062d1f25.png) " +
                    "### 乔布斯 20 年前想打造的苹果咖啡厅 " +
                    "Apple Store 的设计正从原来满满的科技感走向生活化，而其生活化的走向其实可以追溯到 20 年前苹果一个建立咖啡馆的计划",
            SingleTitle = "阅读全文",
            SingleUrl = "https://www.dingtalk.com/"
        };
        return await _IDingTalkMessagePush.DingTalkToActionCard(actionCard);
    }

    /// <summary>
    /// 任务卡片(独立跳转)
    /// </summary>
    /// <returns></returns>
    [HttpPost("PartActionCard")]
    public async Task<BaseResultDto> DingTalkToPartActionCard()
    {
        string keyWord = "消息提醒";
        ActionCard actionCard = new()
        {
            Title = keyWord + "乔布斯 20 年前想打造一间苹果咖啡厅，而它正是 Apple Store 的前身",
            Text = "![screenshot](https://gw.alipayobjects.com/zos/skylark-tools/public/files/84111bbeba74743d2771ed4f062d1f25.png) " +
                    "### 乔布斯 20 年前想打造的苹果咖啡厅 " +
                    "Apple Store 的设计正从原来满满的科技感走向生活化，而其生活化的走向其实可以追溯到 20 年前苹果一个建立咖啡馆的计划",
            BtnOrientation = "1",
            Btns = new List<BtnInfo>()
                {
                    new BtnInfo(){
                        Title = keyWord +"不错",
                        ActionUrl = "https://www.dingtalk.com/"
                    },
                    new BtnInfo(){
                        Title = keyWord +"不感兴趣",
                        ActionUrl = "https://www.dingtalk.com/"
                    }
                }
        };
        return await _IDingTalkMessagePush.DingTalkToActionCard(actionCard);
    }

    /// <summary>
    /// 卡片菜单
    /// </summary>
    /// <returns></returns>
    [HttpPost("FeedCard")]
    public async Task<BaseResultDto> DingTalkToFeedCard()
    {
        string keyWord = "消息提醒";
        FeedCard feedCard = new()
        {
            Links = new List<FeedCardLink>()
                {
                    new FeedCardLink(){
                        Title = keyWord + "时代的火车向前开",
                        MessageUrl="https://www.dingtalk.com/",
                        PicUrl="https://img.alicdn.com/tfs/TB1NwmBEL9TBuNjy1zbXXXpepXa-2400-1218.png"
                    },
                    new FeedCardLink(){
                        Title = keyWord + "时代在召唤",
                        MessageUrl="https://www.dingtalk.com/",
                        PicUrl="https://img.alicdn.com/tfs/TB1NwmBEL9TBuNjy1zbXXXpepXa-2400-1218.png"
                    }
                }
        };
        return await _IDingTalkMessagePush.DingTalkToFeedCard(feedCard);
    }
}