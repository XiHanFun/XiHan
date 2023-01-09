#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:HttpHelper
// Guid:a0813c9d-590b-48e3-90f1-91d62780ea3d
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-09-07 上午 03:12:07
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ZhaiFanhuaBlog.Utils.Http;

/// <summary>
/// HttpHelper
/// </summary>
public class HttpHelper : IHttpHelper
{
    private readonly IHttpClientFactory _IHttpClientFactory;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="httpClientFactory"></param>
    public HttpHelper(IHttpClientFactory httpClientFactory)
    {
        _IHttpClientFactory = httpClientFactory;
    }

    /// <summary>
    /// Get请求
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="httpEnum"></param>
    /// <param name="url"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    public async Task<TEntity?> GetAsync<TEntity>(HttpEnum httpEnum, string url, Dictionary<string, string>? headers = null)
    {
        using var client = _IHttpClientFactory.CreateClient(httpEnum.ToString());
        if (headers != null)
        {
            foreach (var header in headers.Where(header => !client.DefaultRequestHeaders.Contains(header.Key)))
            {
                client.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }
        var response = await client.GetAsync(url);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            var result = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TEntity>(result);
        }

        throw new Exception($"Http Error StatusCode:{response.StatusCode}");
    }

    /// <summary>
    /// Get请求
    /// </summary>
    /// <param name="httpEnum"></param>
    /// <param name="url"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    public async Task<string> GetAsync(HttpEnum httpEnum, string url, Dictionary<string, string>? headers = null)
    {
        using var client = _IHttpClientFactory.CreateClient(httpEnum.ToString());
        if (headers != null)
        {
            foreach (var header in headers.Where(header => !client.DefaultRequestHeaders.Contains(header.Key)))
            {
                client.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }
        var response = await client.GetAsync(url);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return await response.Content.ReadAsStringAsync();
        }

        throw new Exception($"Http Error StatusCode:{response.StatusCode}");
    }

    /// <summary>
    /// Post请求
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TREntity"></typeparam>
    /// <param name="httpEnum"></param>
    /// <param name="url"></param>
    /// <param name="request"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    public async Task<TEntity?> PostAsync<TEntity, TREntity>(HttpEnum httpEnum, string url, TREntity request, Dictionary<string, string>? headers = null)
    {
        using var client = _IHttpClientFactory.CreateClient(httpEnum.ToString());
        if (headers != null)
        {
            foreach (var header in headers.Where(header => !client.DefaultRequestHeaders.Contains(header.Key)))
            {
                client.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }
        var stringContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
        var response = await client.PostAsync(url, stringContent);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            var result = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TEntity>(result);
        }

        throw new Exception($"Http Error StatusCode:{response.StatusCode}");
    }

    /// <summary>
    /// Post请求上传文件
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="httpEnum"></param>
    /// <param name="url"></param>
    /// <param name="fileStream"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    public async Task<TEntity?> PostAsync<TEntity>(HttpEnum httpEnum, string url, FileStream fileStream, Dictionary<string, string>? headers = null)
    {
        using var client = _IHttpClientFactory.CreateClient(httpEnum.ToString());
        using var formDataContent = new MultipartFormDataContent();
        if (headers != null)
        {
            foreach (var header in headers.Where(header => !formDataContent.Headers.Contains(header.Key)))
            {
                formDataContent.Headers.Add(header.Key, header.Value);
            }
        }
        formDataContent.Headers.ContentType = new MediaTypeHeaderValue("multipart/form-data");
        formDataContent.Add(new StreamContent(fileStream, (int)fileStream.Length), "file", fileStream.Name);
        var response = await client.PostAsync(url, formDataContent);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            var result = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TEntity>(result);
        }

        throw new Exception($"Http Error StatusCode:{response.StatusCode}");
    }

    /// <summary>
    /// Post请求
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="httpEnum"></param>
    /// <param name="url"></param>
    /// <param name="request"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    public async Task<TEntity?> PostAsync<TEntity>(HttpEnum httpEnum, string url, string request, Dictionary<string, string>? headers = null)
    {
        using var client = _IHttpClientFactory.CreateClient(httpEnum.ToString());
        if (headers != null)
        {
            foreach (var header in headers.Where(header => !client.DefaultRequestHeaders.Contains(header.Key)))
            {
                client.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }
        var stringContent = new StringContent(request, Encoding.UTF8, "application/json");
        var response = await client.PostAsync(url, stringContent);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            var result = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TEntity>(result);
        }

        throw new Exception($"Http Error StatusCode:{response.StatusCode}");
    }

    /// <summary>
    /// Post请求
    /// </summary>
    /// <typeparam name="TREntity"></typeparam>
    /// <param name="httpEnum"></param>
    /// <param name="url"></param>
    /// <param name="request"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    public async Task<string> PostAsync<TREntity>(HttpEnum httpEnum, string url, TREntity request, Dictionary<string, string>? headers = null)
    {
        using var client = _IHttpClientFactory.CreateClient(httpEnum.ToString());
        if (headers != null)
        {
            foreach (var header in headers.Where(header => !client.DefaultRequestHeaders.Contains(header.Key)))
            {
                client.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }
        var stringContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
        var response = await client.PostAsync(url, stringContent);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return await response.Content.ReadAsStringAsync();
        }

        throw new Exception($"Http Error StatusCode:{response.StatusCode}");
    }

    /// <summary>
    /// Post请求
    /// </summary>
    /// <param name="httpEnum"></param>
    /// <param name="url"></param>
    /// <param name="request"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    public async Task<string> PostAsync(HttpEnum httpEnum, string url, string request, Dictionary<string, string>? headers = null)
    {
        using var client = _IHttpClientFactory.CreateClient(httpEnum.ToString());
        if (headers != null)
        {
            foreach (var header in headers.Where(header => !client.DefaultRequestHeaders.Contains(header.Key)))
            {
                client.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }
        var stringContent = new StringContent(request, Encoding.UTF8, "application/json");
        var response = await client.PostAsync(url, stringContent);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return await response.Content.ReadAsStringAsync();
        }

        throw new Exception($"Http Error StatusCode:{response.StatusCode}");
    }

    /// <summary>
    /// Put请求
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TREntity"></typeparam>
    /// <param name="httpEnum"></param>
    /// <param name="url"></param>
    /// <param name="request"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    public async Task<TEntity?> PutAsync<TEntity, TREntity>(HttpEnum httpEnum, string url, TREntity request, Dictionary<string, string>? headers = null)
    {
        using var client = _IHttpClientFactory.CreateClient(httpEnum.ToString());
        if (headers != null)
        {
            foreach (var header in headers.Where(header => !client.DefaultRequestHeaders.Contains(header.Key)))
            {
                client.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }
        var stringContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
        var response = await client.PutAsync(url, stringContent);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            var result = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TEntity>(result);
        }

        throw new Exception($"Http Error StatusCode:{response.StatusCode}");
    }

    /// <summary>
    /// Put请求
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="httpEnum"></param>
    /// <param name="url"></param>
    /// <param name="request"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    public async Task<TEntity?> PutAsync<TEntity>(HttpEnum httpEnum, string url, string request, Dictionary<string, string>? headers = null)
    {
        using var client = _IHttpClientFactory.CreateClient(httpEnum.ToString());
        if (headers != null)
        {
            foreach (var header in headers.Where(header => !client.DefaultRequestHeaders.Contains(header.Key)))
            {
                client.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }
        var stringContent = new StringContent(request, Encoding.UTF8, "application/json");
        var response = await client.PutAsync(url, stringContent);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            var result = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TEntity>(result);
        }

        throw new Exception($"Http Error StatusCode:{response.StatusCode}");
    }

    /// <summary>
    /// Delete请求
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="httpEnum"></param>
    /// <param name="url"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    public async Task<TEntity?> DeleteAsync<TEntity>(HttpEnum httpEnum, string url, Dictionary<string, string>? headers = null)
    {
        using var client = _IHttpClientFactory.CreateClient(httpEnum.ToString());
        if (headers != null)
        {
            foreach (var header in headers.Where(header => !client.DefaultRequestHeaders.Contains(header.Key)))
            {
                client.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }
        var response = await client.DeleteAsync(url);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            var result = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TEntity>(result);
        }

        throw new Exception($"Http Error StatusCode:{response.StatusCode}");
    }
}