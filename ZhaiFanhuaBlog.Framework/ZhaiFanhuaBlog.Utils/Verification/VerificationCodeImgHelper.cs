// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:VerificationCodeImgHelper
// Guid:63867975-2e09-4307-9726-7f2428109e2e
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-09-18 上午 02:35:02
// ----------------------------------------------------------------

using Microsoft.AspNetCore.Http;
using System.Drawing;
using System.Security.Cryptography;

namespace ZhaiFanhuaBlog.Utils.Verification;

/// <summary>
/// 验证码类型
/// </summary>
public enum VerificationCodeImgType
{
    /// <summary>
    /// 随机数字
    /// </summary>
    Number,

    /// <summary>
    /// 随机字母
    /// </summary>
    Letter,

    /// <summary>
    /// 随机字母或数字
    /// </summary>
    NumberOrLetter
}

/// <summary>
/// 验证码图片帮助类
/// </summary>
public class VerificationCodeImgHelper
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="httpContext"></param>
    /// <param name="type"></param>
    /// <param name="length"></param>
    public VerificationCodeImgHelper(HttpContext httpContext, VerificationCodeImgType type, int length = 4)
    {
        httpContext.Response.Headers.Add("pragma", "no-cache");
        this.text = type switch
        {
            VerificationCodeImgType.Number => VerificationCodeHelper.Number(length),
            VerificationCodeImgType.Letter => VerificationCodeHelper.Letter(length),
            VerificationCodeImgType.NumberOrLetter => VerificationCodeHelper.NumberOrLetter(length),
            _ => VerificationCodeHelper.NumberOrLetter(length),
        };
    }

    #region 属性

    /// <summary>
    /// 验证码的值
    /// </summary>
    public string Text
    {
        get { return this.text; }
    }

    /// <summary>
    /// 验证码图片
    /// </summary>
    public Bitmap Image
    {
        get { return this.image; }
    }

    #endregion 属性

    #region 私有

    // 初始化验证码文字
    private string text;

    // 初始化验证码图片
    private Bitmap? image;

    // 单个字体的宽度范围
    private readonly int letterWidth = 40;

    // 单个字体的高度范围
    private readonly int letterHeight = 40;

    // 随机种子
    private static readonly byte[] randb = new byte[4];

    private static RNGCryptoServiceProvider rand = new RNGCryptoServiceProvider();//实例化加密随机数生成器

    private Font[] fonts =
    {
        new Font(new FontFamily("Times New Roman"),10 +Next(1),FontStyle.Regular),
        new Font(new FontFamily("Georgia"), 10 + Next(1),FontStyle.Regular),
        new Font(new FontFamily("Arial"), 10 + Next(1),FontStyle.Regular),
        new Font(new FontFamily("Comic Sans MS"), 10 + Next(1),FontStyle.Regular)
    };

    /// <summary>
    /// 获得下一个随机数
    /// </summary>
    /// <param name="max">最大值</param>
    /// <returns></returns>
    private static int Next(int max)
    {
        rand.GetBytes(randb);
        int value = BitConverter.ToInt32(randb, 0);
        value %= (max + 1);
        if (value < 0) value = -value;
        return value;
    }

    /// <summary>
    /// 获得下一个随机数
    /// </summary>
    /// <param name="min">最小值</param>
    /// <param name="max">最大值</param>
    /// <returns></returns>
    private static int Next(int min, int max)
    {
        int value = Next(max - min) + min;
        return value;
    }

    #endregion 私有

    #region 方法

    /// <summary>
    /// 绘制验证码
    /// </summary>
    public void CreateImage()
    {
        Bitmap image = new(text.Length * letterWidth, letterHeight);
        Graphics graphics = Graphics.FromImage(image);
        //图像背景
        graphics.Clear(Color.White);
        //绘制干扰线
        for (int i = 0; i < 2; i++)
        {
            int x1 = Next(image.Width - 1);
            int x2 = Next(image.Width - 1);
            int y1 = Next(image.Height - 1);
            int y2 = Next(image.Height - 1);
            graphics.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
        }
        //绘制干扰点
        for (int i = 0; i < image.Width; i++)
        {
            int x = Next(image.Width - 1);
            int y = Next(image.Height - 1);
            image.SetPixel(x, y, Color.FromArgb(Next(0, 255), Next(0, 255), Next(0, 255)));
        }
        //绘制字符串
        int _x = -12, _y = 0;
        for (int int_index = 0; int_index < this.text.Length; int_index++)
        {
            _x += Next(12, 16);
            _y = Next(-2, 2);
            string str_char = this.text.Substring(int_index, 1);
            str_char = Next(1) == 1 ? str_char.ToLower() : str_char.ToUpper();
            Brush newBrush = new SolidBrush(GetRandomColor());
            Point thePos = new Point(_x, _y);
            graphics.DrawString(str_char, fonts[Next(fonts.Length - 1)], newBrush, thePos);
        }
        //绘制图像
        image = TwistImage(image, true, Next(1, 4), Next(0, 6));
        this.image = image;
    }

    //字体随机颜色
    public Color GetRandomColor()
    {
        Random RandomNum_First = new Random((int)DateTime.Now.Ticks);
        System.Threading.Thread.Sleep(RandomNum_First.Next(50));
        Random RandomNum_Sencond = new Random((int)DateTime.Now.Ticks);
        int int_Red = RandomNum_First.Next(180);
        int int_Green = RandomNum_Sencond.Next(180);
        int int_Blue = (int_Red + int_Green > 300) ? 0 : 400 - int_Red - int_Green;
        int_Blue = (int_Blue > 255) ? 255 : int_Blue;
        return Color.FromArgb(int_Red, int_Green, int_Blue);
    }

    //正弦曲线Wave扭曲图片(图片路径;如果扭曲则选择为True;波形的幅度倍数，越大扭曲的程度越高,一般为3;波形的起始相位,取值区间[0-2*PI))
    public Bitmap TwistImage(Bitmap srcBmp, bool bXDir, double dMultValue, double dPhase)
    {
        double PI = 6.283185307179586476925286766559;
        Bitmap destBmp = new Bitmap(srcBmp.Width, srcBmp.Height);
        Graphics graph = Graphics.FromImage(destBmp);
        graph.FillRectangle(new SolidBrush(Color.White), 0, 0, destBmp.Width, destBmp.Height);
        graph.Dispose();
        double dBaseAxisLen = bXDir ? (double)destBmp.Height : (double)destBmp.Width;
        for (int i = 0; i < destBmp.Width; i++)
        {
            for (int j = 0; j < destBmp.Height; j++)
            {
                double dx = 0;
                dx = bXDir ? (PI * (double)j) / dBaseAxisLen : (PI * (double)i) / dBaseAxisLen;
                dx += dPhase;
                double dy = Math.Sin(dx);
                int nOldX = 0, nOldY = 0;
                nOldX = bXDir ? i + (int)(dy * dMultValue) : i;
                nOldY = bXDir ? j : j + (int)(dy * dMultValue);

                Color color = srcBmp.GetPixel(i, j);
                if (nOldX >= 0 && nOldX < destBmp.Width
                 && nOldY >= 0 && nOldY < destBmp.Height)
                {
                    destBmp.SetPixel(nOldX, nOldY, color);
                }
            }
        }
        srcBmp.Dispose();
        return destBmp;
    }

    #endregion 方法
}