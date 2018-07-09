using UnityEngine;
using System.Collections;
using System.Text;
using System;

/// <summary>
/// 
/// </summary>
public class Base64Code : MonoBehaviour {

    /// <summary>
    /// 编码
    /// </summary>
    /// <param name="encodeType"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    public static string EncodeBase64(Encoding encodeType, string code)
    {
        string encode = "";
        byte[] bytes = encodeType.GetBytes(code);
        try
        {
            encode = Convert.ToBase64String(bytes);
        }
        catch
        {
            encode = code;
        }
        return encode;
    }
    /// <summary>
    /// 解码
    /// </summary>
    /// <param name="encodeType"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    public static string DecodeBase64(Encoding encodeType, string code)
    {
        string decode = "";
        byte[] bytes = Convert.FromBase64String(code);
        try
        {
            decode = encodeType.GetString(bytes);
        }
        catch
        {
            decode = code;
        }
        return decode;
    }
}
