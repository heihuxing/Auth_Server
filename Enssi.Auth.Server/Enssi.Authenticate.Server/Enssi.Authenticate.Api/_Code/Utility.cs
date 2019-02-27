using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace Enssi
{
    public static class Utility
    {

        public static object GetValue(this object obj, string name)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            return obj.GetType().GetProperty(name)?.GetValue(obj, null);
        }

        public static T As<T>(this object value)
        {
            try
            {
                if (value is T)
                {
                    return (T)value;
                }
                return (T)value.AsType(typeof(T));
            }
            catch
            {
                return default(T);
            }
        }

        public static T As<T>(this object value, T defaultValue)
        {
            try
            {
                return (T)value.AsType(typeof(T));
            }
            catch
            {
                return defaultValue;
            }
        }

        public static object AsType(this object value, Type conversionType)
        {
            if ((value != null) && (conversionType == value.GetType()))
            {
                return value;
            }
            if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                {
                    return null;
                }
                NullableConverter converter = new NullableConverter(conversionType);
                conversionType = converter.UnderlyingType;
            }
            if (conversionType == typeof(Guid))
            {
                return Guid.Parse(value + "");
            }
            try
            {
                return Convert.ChangeType(value, conversionType);
            }
            catch
            {
                return null;
            }
        }

        public static Expression DeserializeSelector<T>(string selector)
        {
            if (string.IsNullOrEmpty(selector) || selector.StartsWith("Includes:"))
            {
                return (Expression<Func<T, T>>)(a => a);
            }

            selector = GZipDecompressString(selector);

            var serializer = new ExpressionSerialization.ExpressionSerializer(new ExpressionSerialization.TypeResolver(null, new[] { typeof(T) }));
            var xele = Newtonsoft.Json.JsonConvert.DeserializeXNode(selector);
            var result = serializer.Deserialize(xele.Elements().FirstOrDefault());

            return result;
        }

        public static string[] GetIncludes(string selector)
        {
            return (selector + "").StartsWith("Includes:") ? selector.Split(':')[1].Split(',').Where(a => !string.IsNullOrEmpty(a)).ToArray() : new string[0];
        }

        public static IQueryable<T> Includes<T>(this DbQuery<T> source, string[] includes) where T : class
        {
            foreach (var item in includes)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    source = source.Include(item);
                }
            }

            return source.AsQueryable();
        }
        public static IQueryable<object> SelectObject<T>(this IQueryable<T> source, Expression selector)
        {
            var types = selector.GetType().GetGenericArguments()[0].GetGenericArguments();

            var type = typeof(Queryable);
            var methodInfo = (System.Reflection.MethodInfo)type.GetMember("Select").Where(a => !a.ToString().Contains("Int32")).FirstOrDefault();
            methodInfo = methodInfo.MakeGenericMethod(types);
            var resultlist = (IQueryable<object>)methodInfo.Invoke(null, new object[] { source, selector });
            return resultlist;
        }

        public static IList<Expression<Func<T, bool>>> DeserializePredicate<T>(string predicateList)
        {
            if (string.IsNullOrEmpty(predicateList))
            {
                return new List<Expression<Func<T, bool>>>();
            }

            predicateList = GZipDecompressString(predicateList);

            var list = Newtonsoft.Json.JsonConvert.DeserializeObject<IList<string>>(predicateList);

            var expressionList = new List<Expression<Func<T, bool>>>();
            var serializer = new ExpressionSerialization.ExpressionSerializer(new ExpressionSerialization.TypeResolver(null, new[] { typeof(T) }));
            foreach (var item in list)
            {
                var xele = Newtonsoft.Json.JsonConvert.DeserializeXNode(item);
                expressionList.Add(serializer.Deserialize<Func<T, bool>>(xele.Elements().FirstOrDefault()));
            }

            return expressionList;
        }

        public static IDictionary<Expression<Func<T, object>>, string> DeserializeOrderBy<T>(string orderByList)
        {
            if (string.IsNullOrEmpty(orderByList))
            {
                return new Dictionary<Expression<Func<T, object>>, string>();
            }

            orderByList = GZipDecompressString(orderByList);

            var list = Newtonsoft.Json.JsonConvert.DeserializeObject<IDictionary<string, string>>(orderByList);

            var expressionList = new Dictionary<Expression<Func<T, object>>, string>();
            var serializer = new ExpressionSerialization.ExpressionSerializer(new ExpressionSerialization.TypeResolver(null, new[] { typeof(T) }));
            foreach (var item in list)
            {
                var xele = Newtonsoft.Json.JsonConvert.DeserializeXNode(item.Key);
                expressionList.Add(serializer.Deserialize<Func<T, object>>(xele.Elements().FirstOrDefault()), item.Value);
            }

            return expressionList;
        }

        public static IQueryable<T> OrderByList<T>(this IQueryable<T> list, IDictionary<Expression<Func<T, object>>, string> orderByList)
        {
            foreach (var item in orderByList)
            {
                if (item.Value == "asc")
                {
                    if (item.Equals(orderByList.First()))
                    {
                        list = list.OrderBy(item.Key);
                    }
                    else
                    {
                        list = ((IOrderedQueryable<T>)list).ThenBy(item.Key);
                    }
                }
                else if (item.Value == "desc")
                {
                    if (item.Equals(orderByList.First()))
                    {
                        list = list.OrderByDescending(item.Key);
                    }
                    else
                    {
                        list = ((IOrderedQueryable<T>)list).ThenByDescending(item.Key);
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="encryptStr">明文</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        internal static string AesEncrypt(string encryptStr, string key)
        {
            byte[] keyArray = Encoding.UTF8.GetBytes(key);
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(encryptStr);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="decryptStr">密文</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        internal static string AesDecrypt(string decryptStr, string key)
        {
            byte[] keyArray = Encoding.UTF8.GetBytes(key);
            byte[] toEncryptArray = Convert.FromBase64String(decryptStr);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Encoding.UTF8.GetString(resultArray);
        }

        /// <summary>
        /// 将传入字符串以GZip算法压缩后，返回Base64编码字符
        /// </summary>
        /// <param name="rawString">需要压缩的字符串</param>
        /// <returns>压缩后的Base64编码的字符串</returns>
        public static string GZipCompressString(string rawString)
        {
            if (string.IsNullOrEmpty(rawString) || rawString.Length == 0)
            {
                return "";
            }
            else
            {
                byte[] rawData = System.Text.Encoding.UTF8.GetBytes(rawString.ToString());
                byte[] zippedData = Compress(rawData);
                return (string)(Convert.ToBase64String(zippedData));
            }

        }

        /// <summary>
        /// GZip压缩
        /// </summary>
        /// <param name="rawData"></param>
        /// <returns></returns>
        public static byte[] Compress(byte[] rawData)
        {
            MemoryStream ms = new MemoryStream();
            System.IO.Compression.GZipStream compressedzipStream = new System.IO.Compression.GZipStream(ms, System.IO.Compression.CompressionMode.Compress, true);
            compressedzipStream.Write(rawData, 0, rawData.Length);
            compressedzipStream.Close();
            return ms.ToArray();
        }

        /// <summary>
        /// 将传入的二进制字符串资料以GZip算法解压缩
        /// </summary>
        /// <param name="zippedString">经GZip压缩后的二进制字符串</param>
        /// <returns>原始未压缩字符串</returns>
        public static string GZipDecompressString(string zippedString)
        {
            if (string.IsNullOrEmpty(zippedString) || zippedString.Length == 0)
            {
                return "";
            }
            else
            {
                byte[] zippedData = Convert.FromBase64String(zippedString.ToString());
                return (string)(System.Text.Encoding.UTF8.GetString(Decompress(zippedData)));
            }
        }

        /// <summary>
        /// ZIP解压
        /// </summary>
        /// <param name="zippedData"></param>
        /// <returns></returns>
        public static byte[] Decompress(byte[] zippedData)
        {
            MemoryStream ms = new MemoryStream(zippedData);
            System.IO.Compression.GZipStream compressedzipStream = new System.IO.Compression.GZipStream(ms, System.IO.Compression.CompressionMode.Decompress);
            MemoryStream outBuffer = new MemoryStream();
            byte[] block = new byte[1024];
            while (true)
            {
                int bytesRead = compressedzipStream.Read(block, 0, block.Length);
                if (bytesRead <= 0)
                    break;
                else
                    outBuffer.Write(block, 0, bytesRead);
            }
            compressedzipStream.Close();
            return outBuffer.ToArray();
        }

        /// <summary>
        /// 获得上传的文件
        /// </summary>
        /// <param name="name">Form名称</param>
        /// <returns></returns>
        public static System.Web.HttpPostedFile GetRequestFile(string name)
        {
            var current = System.Web.HttpContext.Current;
            var file = current.Request.Files[name];
            return file;
        }

        /// <summary>
        /// 保存上传的文件，文件夹不存在时会自动创建
        /// </summary>
        /// <param name="name">Form名称</param>
        /// <param name="dirPath">文件夹路径</param>
        /// <param name="fileName">文件名</param>
        public static void SaveRequestFile(string name, string dirPath, string fileName)
        {
            var current = System.Web.HttpContext.Current;
            var file = current.Request.Files[name];

            var serverDirPath = current.Server.MapPath(dirPath);
            if (!Directory.Exists(serverDirPath))
            {
                Directory.CreateDirectory(serverDirPath);
            }
            file.SaveAs(Path.Combine(serverDirPath, fileName));
        }
    }
}
