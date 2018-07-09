using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;


  public  class XmlProcess
    {/// <summary>
        /// 加载xml文件解析成对应对象
        /// </summary>
        /// <typeparam name="T"> 解析对象类型</typeparam>
        /// <param name="path">加载的xml配置文件路径</param>
        /// <returns></returns>
        public static T Loadxml<T>(string path)
        {
            string xml = File.ReadAllText(path);
            return DeserializeObject<T>(xml);
        }
        /// <summary>
        /// 将对象序列化为xml字符串
        /// </summary>
        /// <param name="pObject"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private static string SerializeObject(object pObject, System.Type type)
        {
            XmlSerializer xs = new XmlSerializer(type);
            StringBuilder sb = new StringBuilder();
            XmlWriter xw = XmlWriter.Create(sb);
            XmlSerializerNamespaces xsn = new XmlSerializerNamespaces();
            xs.Serialize(xw, pObject, xsn, Encoding.UTF8.EncodingName);
            return sb.ToString();
        }
        /// <summary>
        /// 将xml字符串解析为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pXmlizedString"></param>
        /// <returns></returns>
        public static T DeserializeObject<T>(string pXmlizedString)
        {
            return (T)DeserializeObject(pXmlizedString, typeof(T));
        }
        /// <summary>
        /// 将xml字符串解析为对象
        /// </summary>
        /// <param name="pXmlizedString"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private static object DeserializeObject(string pXmlizedString, System.Type type)
        {
            XmlSerializer xs = new XmlSerializer(type);
            MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(pXmlizedString));
            return xs.Deserialize(memoryStream);
        }

        private static string UTF8ByteArrayToString(byte[] characters)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            string constructedString = encoding.GetString(characters);
            return (constructedString);
        }

        private static byte[] StringToUTF8ByteArray(string pXmlString)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] byteArray = encoding.GetBytes(pXmlString);
            return byteArray;
        } 
    }

