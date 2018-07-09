using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;
using System.Collections;

/// <summary>
/// 文件操作工具类
/// </summary>
	public class FileUtils
	{
        /// <summary>
        /// 创建文件 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="savePath"></param>
        public static void CreateFile(byte[] data, string savePath)
        {
            DeleteFile(savePath);
            checkDir(savePath);
            File.WriteAllBytes(savePath, data);
        }

        /// <summary>
        /// 创建文件 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="savePath"></param>
        public static IEnumerable CreateFileYield(byte[] data, string savePath)
        {
            DeleteFile(savePath);
            checkDir(savePath);
            File.WriteAllBytes(savePath, data);
            yield return null;
        }

        private static void checkDir(string savePath)
        {

            string path = savePath.Substring(0, savePath.LastIndexOf('/'));
            if (Directory.Exists(path)) return;
            Directory.CreateDirectory(path);
        }

        /// <summary>
        /// 创建文件 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="savePath"></param>
        public static void CreateFile(string data, string savePath)
        {
            DeleteFile(savePath);
            checkDir(savePath);
            File.WriteAllText(savePath, data);
        }
        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="savePath"></param>
        /// <returns></returns>
        public static byte[] ReadAllBytes(string savePath)
        {
            return File.ReadAllBytes(savePath);
        }

        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="savePath"></param>
        /// <returns></returns>
        public static string ReadAllText(string savePath)
        {
            return File.ReadAllText(savePath);
        }

        public static bool Ex(string file)
        {
            return File.Exists(file);
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="path"></param>
        public static void DeleteFile(string path)
        {
            if (File.Exists(path))
                File.Delete(path);
        }

        /// <summary>
        /// 创建本地ass
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public AssetBundle GetAsset(string path) {
            return AssetBundle.LoadFromFile(path);
        }

    public AssetBundle GetAssetForByte(string path) {
        return AssetBundle.LoadFromMemory(File.ReadAllBytes(path));
    }
	}

