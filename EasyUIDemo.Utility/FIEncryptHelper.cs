/* ----------------------------------------------------------------
 * Copyright (C) 2012 方正国际软件有限公司版权所有
 * 
 * 文件名：EncryptionUtility.cs
 * 文件功能描述：数据加密解密实现类。
 * 
 * 创建标识：方正 2012-8-14
 * 
 * 修改标识：
 * 修改描述：
 * ----------------------------------------------------------------*/

using System;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace EasyUIDemo.Utility
{
    /// <summary>
    ///     数据加密解密实现类
    /// </summary>
    public static class FIEncryption
    {
        #region 字段

        private static readonly string EncryptionKey = (typeof(BinaryReader) + "-w9" + typeof(NameTable) + "sdf3f" +
                                                        typeof(Random) + "jsow23j235ay2s" + typeof(FIEncryption) +
                                                        "a2skwp230a" + typeof(Queue) + "黶dadjm" +
                                                        typeof(NullReferenceException));

        #endregion

        #region 方法
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="algorithm">算法</param>
        /// <param name="valueToDeCrypt">解密值</param>
        /// <returns>结果</returns>
        private static string DeCrypt(SymmetricAlgorithm algorithm, string valueToDeCrypt)
        {
            var buffer = new byte[valueToDeCrypt.Length / 2];
            for (int i = 0; i < (valueToDeCrypt.Length / 2); i++)
            {
                int num2 = Convert.ToInt32(valueToDeCrypt.Substring(i * 2, 2), 0x10);
                buffer[i] = (byte)num2;
            }
            string encryptionKey = EncryptionKey;
            byte[] bytes = Encoding.ASCII.GetBytes(encryptionKey);
            algorithm.Key = (byte[])ArrayFunctions.ReDim(bytes, algorithm.Key.Length);
            algorithm.IV = (byte[])ArrayFunctions.ReDim(bytes, algorithm.IV.Length);
            StringBuilder builder;
            using (var stream = new MemoryStream())
            {
                using (var stream2 = new CryptoStream(stream, algorithm.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    stream2.Write(buffer, 0, buffer.Length);
                    stream2.FlushFinalBlock();
                }
                builder = new StringBuilder();
                for (int j = 0; j < stream.ToArray().Length; j++)
                {
                    builder.Append((char)stream.ToArray()[j]);
                }
            }
            return builder.ToString();
        }
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="algorithm">算法</param>
        /// <param name="valueToEnCrypt">加密值</param>
        /// <returns>结果</returns>
        private static string EnCrypt(SymmetricAlgorithm algorithm, string valueToEnCrypt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(valueToEnCrypt);
            string encryptionKey = EncryptionKey;
            byte[] originalArray = Encoding.ASCII.GetBytes(encryptionKey);
            algorithm.Key = (byte[])ArrayFunctions.ReDim(originalArray, algorithm.Key.Length);
            algorithm.IV = (byte[])ArrayFunctions.ReDim(originalArray, algorithm.IV.Length);
            StringBuilder builder;
            using (var stream = new MemoryStream())
            {
                using (var stream2 = new CryptoStream(stream, algorithm.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    stream2.Write(bytes, 0, bytes.Length);
                    stream2.FlushFinalBlock();
                }
                builder = new StringBuilder();
                for (int i = 0; i < stream.ToArray().Length; i++)
                {
                    byte num2 = stream.ToArray()[i];
                    builder.AppendFormat("{0:X2}", num2);
                }
            }
            return builder.ToString();
        }

        /// <summary>
        ///     DES解密
        /// </summary>
        /// <param name="encryptedText">解密文本</param>
        /// <returns>结果</returns>
        public static string DESDecryption(string encryptedText)
        {
            return DeCrypt(new DESCryptoServiceProvider(), encryptedText);
        }

        /// <summary>
        ///     DES加密
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string DESEncryption(string plainText)
        {
            return EnCrypt(new DESCryptoServiceProvider(), plainText);
        }

        /// <summary>
        ///     MD5加密
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string MD5Encryption(string plainText)
        {
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(Encoding.ASCII.GetBytes(plainText)));
        }

        /// <summary>
        ///     MD5加密
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string MD5(string plainText)
        {
            byte[] dataToHash = Encoding.ASCII.GetBytes(plainText);
            byte[] hashvalue = new MD5CryptoServiceProvider().ComputeHash(dataToHash);

            string md5 = string.Empty;
            for (int i = 0; i < 16; i++)
            {
                md5 += hashvalue[i].ToString("X").ToLower();
            }
            return md5;
        }

        /// <summary>
        ///     RSA解密
        /// </summary>
        /// <param name="encryptedText"></param>
        /// <returns></returns>
        public static string RSADecryption(string encryptedText)
        {
            return BitConverter.ToString(new RSACryptoServiceProvider().Decrypt(Encoding.ASCII.GetBytes(encryptedText),
                false));
        }

        /// <summary>
        ///     RSA加密
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string RSAEncryption(string plainText)
        {
            return
                BitConverter.ToString(new RSACryptoServiceProvider().Encrypt(Encoding.ASCII.GetBytes(plainText), false));
        }

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="plainText">原文本内容</param>
        /// <param name="key">加密Key值</param>
        /// <returns>加密文本内容</returns>
        public static string AESEncrypt(string plainText, string key)
        {
            var AES = new RijndaelManaged();
            var MD5 = new MD5CryptoServiceProvider();

            byte[] plainTextData = Encoding.Unicode.GetBytes(plainText);
            byte[] keyData = MD5.ComputeHash(Encoding.Unicode.GetBytes(key));
            byte[] IVData = MD5.ComputeHash(Encoding.Unicode.GetBytes("Alex Lee"));
            ICryptoTransform transform = AES.CreateEncryptor(keyData, IVData);
            byte[] outputData = transform.TransformFinalBlock(plainTextData, 0, plainTextData.Length);
            return Convert.ToBase64String(outputData);
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="cipherText">加密文本内容</param>
        /// <param name="key">加密Key值</param>
        /// <returns>原本内容</returns>
        public static string AESDecrypt(string cipherText, string key)
        {
            var AES = new RijndaelManaged();
            var MD5 = new MD5CryptoServiceProvider();

            byte[] cipherTextData = Convert.FromBase64String(cipherText);
            byte[] keyData = MD5.ComputeHash(Encoding.Unicode.GetBytes(key));
            byte[] IVData = MD5.ComputeHash(Encoding.Unicode.GetBytes("Alex Lee"));
            ICryptoTransform transform = AES.CreateDecryptor(keyData, IVData);
            byte[] outputData = transform.TransformFinalBlock(cipherTextData, 0, cipherTextData.Length);
            return Encoding.Unicode.GetString(outputData);

        }

        /// <summary>
        /// 账号加密
        /// </summary>
        /// <param name="account">账号</param>
        /// <returns>加密串</returns>
        public static string NormalEncrypt(string account)
        {
            Random rd = new Random();
            var param = rd.Next(0, 899999) + 100000;
            return "5erdy633242sfw" + account + "w456" + param + "21wt789e";
        }

        #endregion
    }

    public class ArrayFunctions
    {
        public static Array ReDim(Array originalArray, int newSize)
        {
            Array destinationArray = Array.CreateInstance(originalArray.GetType().GetElementType(), newSize);
            Array.Copy(originalArray, 0, destinationArray, 0, Math.Min(originalArray.Length, newSize));
            return destinationArray;
        }
    }
}