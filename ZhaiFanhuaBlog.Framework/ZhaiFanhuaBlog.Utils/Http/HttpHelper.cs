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

using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

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
    /// <typeparam name="T"></typeparam>
    /// <param name="httpEnum"></param>
    /// <param name="url"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    public async Task<T?> GetAsync<T>(HttpEnum httpEnum, string url, Dictionary<string, string>? headers = null)
    {
        try
        {
            using var client = _IHttpClientFactory.CreateClient(httpEnum.ToString());
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    if (!client.DefaultRequestHeaders.Contains(header.Key))
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }
            }
            var response = await client.GetAsync(url);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(result);
            }
            else
            {
                throw new Exception($"Http Error StatusCode:{response.StatusCode}");
            }
        }
        catch (Exception)
        {
            throw;
        }
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
        try
        {
            using var client = _IHttpClientFactory.CreateClient(httpEnum.ToString());
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    if (!client.DefaultRequestHeaders.Contains(header.Key))
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }
            }
            var response = await client.GetAsync(url);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return await response.Content.ReadAsStringAsync(); ;
            }
            else
            {
                throw new Exception($"Http Error StatusCode:{response.StatusCode}");
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// Post请求
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="R"></typeparam>
    /// <param name="httpEnum"></param>
    /// <param name="url"></param>
    /// <param name="request"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    public async Task<T?> PostAsync<T, R>(HttpEnum httpEnum, string url, R request, Dictionary<string, string>? headers = null)
    {
        try
        {
            using var client = _IHttpClientFactory.CreateClient(httpEnum.ToString());
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    if (!client.DefaultRequestHeaders.Contains(header.Key))
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }
            }
            var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, stringContent);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(result);
            }
            else
            {
                throw new Exception($"Http Error StatusCode:{response.StatusCode}");
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// Post请求上传文件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="httpEnum"></param>
    /// <param name="url"></param>
    /// <param name="fileStream"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    public async Task<T?> PostAsync<T>(HttpEnum httpEnum, string url, FileStream fileStream, Dictionary<string, string>? headers = null)
    {
        try
        {
            using var client = _IHttpClientFactory.CreateClient(httpEnum.ToString());
            using var formDataContent = new MultipartFormDataContent();
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    if (!formDataContent.Headers.Contains(header.Key))
                    {
                        formDataContent.Headers.Add(header.Key, header.Value);
                    }
                }
            }
            formDataContent.Headers.ContentType = new MediaTypeHeaderValue("multipart/form-data");
            formDataContent.Add(new StreamContent(fileStream, (int)fileStream.Length), "file", fileStream.Name);
            var response = await client.PostAsync(url, formDataContent);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(result);
            }
            else
            {
                throw new Exception($"Http Error StatusCode:{response.StatusCode}");
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// Post请求
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="httpEnum"></param>
    /// <param name="url"></param>
    /// <param name="request"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    public async Task<T?> PostAsync<T>(HttpEnum httpEnum, string url, string request, Dictionary<string, string>? headers = null)
    {
        try
        {
            using var client = _IHttpClientFactory.CreateClient(httpEnum.ToString());
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    if (!client.DefaultRequestHeaders.Contains(header.Key))
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }
            }
            var stringContent = new StringContent(request, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, stringContent);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(result);
            }
            else
            {
                throw new Exception($"Http Error StatusCode:{response.StatusCode}");
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// Post请求
    /// </summary>
    /// <typeparam name="R"></typeparam>
    /// <param name="httpEnum"></param>
    /// <param name="url"></param>
    /// <param name="request"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    public async Task<string> PostAsync<R>(HttpEnum httpEnum, string url, R request, Dictionary<string, string>? headers = null)
    {
        try
        {
            using var client = _IHttpClientFactory.CreateClient(httpEnum.ToString());
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    if (!client.DefaultRequestHeaders.Contains(header.Key))
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }
            }
            var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, stringContent);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new Exception($"Http Error StatusCode:{response.StatusCode}");
            }
        }
        catch (Exception)
        {
            throw;
        }
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
        try
        {
            using var client = _IHttpClientFactory.CreateClient(httpEnum.ToString());
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    if (!client.DefaultRequestHeaders.Contains(header.Key))
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }
            }
            var stringContent = new StringContent(request, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, stringContent);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new Exception($"Http Error StatusCode:{response.StatusCode}");
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// Put请求
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="R"></typeparam>
    /// <param name="httpEnum"></param>
    /// <param name="url"></param>
    /// <param name="request"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    public async Task<T?> PutAsync<T, R>(HttpEnum httpEnum, string url, R request, Dictionary<string, string>? headers = null)
    {
        try
        {
            using var client = _IHttpClientFactory.CreateClient(httpEnum.ToString());
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    if (!client.DefaultRequestHeaders.Contains(header.Key))
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }
            }
            var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await client.PutAsync(url, stringContent);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(result);
            }
            else
            {
                throw new Exception($"Http Error StatusCode:{response.StatusCode}");
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// Put请求
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="httpEnum"></param>
    /// <param name="url"></param>
    /// <param name="request"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    public async Task<T?> PutAsync<T>(HttpEnum httpEnum, string url, string request, Dictionary<string, string>? headers = null)
    {
        try
        {
            using var client = _IHttpClientFactory.CreateClient(httpEnum.ToString());
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    if (!client.DefaultRequestHeaders.Contains(header.Key))
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }
            }
            var stringContent = new StringContent(request, Encoding.UTF8, "application/json");
            var response = await client.PutAsync(url, stringContent);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(result);
            }
            else
            {
                throw new Exception($"Http Error StatusCode:{response.StatusCode}");
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// Delete请求
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="httpEnum"></param>
    /// <param name="url"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    public async Task<T?> DeleteAsync<T>(HttpEnum httpEnum, string url, Dictionary<string, string>? headers = null)
    {
        try
        {
            using var client = _IHttpClientFactory.CreateClient(httpEnum.ToString());
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    if (!client.DefaultRequestHeaders.Contains(header.Key))
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }
            }
            var response = await client.DeleteAsync(url);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(result);
            }
            else
            {
                throw new Exception($"Http Error StatusCode:{response.StatusCode}");
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
}