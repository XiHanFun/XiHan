// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:HttpHelper
// Guid:a0813c9d-590b-48e3-90f1-91d62780ea3d
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-09-07 上午 03:12:07
// ----------------------------------------------------------------

using System.Net.Http.Headers;

namespace ZhaiFanhuaBlog.Utils.Http;

/// <summary>
/// HttpHelper
/// </summary>
public class HttpHelper
{
    /// <summary>
    /// Get方法
    /// </summary>
    /// <param name="serviceAddress"></param>
    /// <returns></returns>
    public static async Task<string> GetAsync(string serviceAddress)
    {
        try
        {
            string result = string.Empty;
            Uri getUrl = new(serviceAddress);
            using var httpClient = new HttpClient();
            httpClient.Timeout = new TimeSpan(0, 0, 60);
            result = await httpClient.GetAsync(serviceAddress).Result.Content.ReadAsStringAsync();
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// Post方法
    /// </summary>
    /// <param name="serviceAddress"></param>
    /// <param name="requestJson"></param>
    /// <returns></returns>
    public static async Task<string> PostAsync(string serviceAddress, string requestJson)
    {
        try
        {
            string result = string.Empty;
            Uri postUrl = new(serviceAddress);

            using (HttpContent httpContent = new StringContent(requestJson))
            {
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                using var httpClient = new HttpClient();
                httpClient.Timeout = new TimeSpan(0, 0, 60);
                result = await httpClient.PostAsync(serviceAddress, httpContent).Result.Content.ReadAsStringAsync();
            }
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }
}