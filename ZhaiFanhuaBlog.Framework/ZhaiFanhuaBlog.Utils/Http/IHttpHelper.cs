// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IHttpHelper
// Guid:354152ba-2a3d-4b13-94f7-66c54d9911c2
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-10-08 下午 10:27:12
// ----------------------------------------------------------------

namespace ZhaiFanhuaBlog.Utils.Http;

/// <summary>
/// IHttpHelper
/// </summary>
public interface IHttpHelper
{
    /// <summary>
    /// Get请求
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="httpEnum"></param>
    /// <param name="url"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    Task<T?> GetAsync<T>(HttpEnum httpEnum, string url, Dictionary<string, string>? headers = null);

    /// <summary>
    /// Get请求
    /// </summary>
    /// <param name="httpEnum"></param>
    /// <param name="url"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    Task<string> GetAsync(HttpEnum httpEnum, string url, Dictionary<string, string>? headers = null);

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
    Task<T?> PostAsync<T, R>(HttpEnum httpEnum, string url, R request, Dictionary<string, string>? headers = null);

    /// <summary>
    /// Post请求上传文件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="httpEnum"></param>
    /// <param name="url"></param>
    /// <param name="fileStream"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    Task<T?> PostAsync<T>(HttpEnum httpEnum, string url, FileStream fileStream, Dictionary<string, string>? headers = null);

    /// <summary>
    /// Post请求
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="httpEnum"></param>
    /// <param name="url"></param>
    /// <param name="request"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    Task<T?> PostAsync<T>(HttpEnum httpEnum, string url, string request, Dictionary<string, string>? headers = null);

    /// <summary>
    /// Post请求
    /// </summary>
    /// <param name="httpEnum"></param>
    /// <param name="url"></param>
    /// <param name="request"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    Task<string> PostAsync(HttpEnum httpEnum, string url, string request, Dictionary<string, string>? headers = null);

    /// <summary>
    /// Post请求
    /// </summary>
    /// <typeparam name="R"></typeparam>
    /// <param name="httpEnum"></param>
    /// <param name="url"></param>
    /// <param name="request"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    Task<string> PostAsync<R>(HttpEnum httpEnum, string url, R request, Dictionary<string, string>? headers = null);

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
    Task<T?> PutAsync<T, R>(HttpEnum httpEnum, string url, R request, Dictionary<string, string>? headers = null);

    /// <summary>
    /// Put请求
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="httpEnum"></param>
    /// <param name="url"></param>
    /// <param name="request"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    Task<T?> PutAsync<T>(HttpEnum httpEnum, string url, string request, Dictionary<string, string>? headers = null);

    /// <summary>
    /// Delete请求
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="httpEnum"></param>
    /// <param name="url"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    Task<T?> DeleteAsync<T>(HttpEnum httpEnum, string url, Dictionary<string, string>? headers = null);
}