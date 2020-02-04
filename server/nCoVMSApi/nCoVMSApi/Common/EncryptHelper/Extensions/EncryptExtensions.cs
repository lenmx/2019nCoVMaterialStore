using System;
using System.Collections.Generic;
using System.Text;

namespace nCoVMSApi.Common.EncryptHelper.Extensions
{
    public static class EncryptExtensions
    {
        /// <summary>
        /// String MD5 extension
        /// </summary>
        /// <param name="srcString"></param>
        /// <returns></returns>
        public static string MD5(this string srcString)
        {
            Check.Argument.IsNotEmpty(srcString, nameof(srcString));
            return EncryptHelper.Md5(srcString);
        }

        /// <summary>
        /// String SHA1 extensions
        /// </summary>
        /// <param name="srcString"></param>
        /// <returns></returns>
        public static string SHA1(this string srcString)
        {
            Check.Argument.IsNotEmpty(srcString, nameof(srcString));
            return EncryptHelper.Sha1(srcString);
        }

        /// <summary>
        /// String SHA256 extensions
        /// </summary>
        /// <param name="srcString"></param>
        /// <returns></returns>
        public static string SHA256(this string srcString)
        {
            Check.Argument.IsNotEmpty(srcString, nameof(srcString));
            return EncryptHelper.Sha256(srcString);
        }

        /// <summary>
        /// String SHA384 extensions
        /// </summary>
        /// <param name="srcString"></param>
        /// <returns></returns>
        public static string SHA384(this string srcString)
        {
            Check.Argument.IsNotEmpty(srcString, nameof(srcString));
            return EncryptHelper.Sha384(srcString);
        }

        /// <summary>
        /// String SHA512 extensions
        /// </summary>
        /// <param name="srcString"></param>
        /// <returns></returns>
        public static string SHA512(this string srcString)
        {
            Check.Argument.IsNotEmpty(srcString, nameof(srcString));
            return EncryptHelper.Sha512(srcString);
        }

        /// <summary>
        /// String HMACMD5 extensions
        /// </summary>
        /// <param name="srcString"></param>
        /// <returns></returns>
        public static string HMACMD5(this string srcString, string key)
        {
            Check.Argument.IsNotEmpty(srcString, nameof(srcString));
            return EncryptHelper.HMACMD5(srcString, key);
        }

        /// <summary>
        /// String HMACSHA1 extensions
        /// </summary>
        /// <param name="srcString"></param>
        /// <returns></returns>
        public static string HMACSHA1(this string srcString, string key)
        {
            Check.Argument.IsNotEmpty(srcString, nameof(srcString));
            return EncryptHelper.HMACSHA1(srcString, key);
        }

        /// <summary>
        /// String HMACSHA1 extensions
        /// </summary>
        /// <param name="srcString"></param>
        /// <returns></returns>
        public static string HMACSHA256(this string srcString, string key)
        {
            Check.Argument.IsNotEmpty(srcString, nameof(srcString));
            return EncryptHelper.HMACSHA256(srcString, key);
        }

        /// <summary>
        /// String HMACSHA384 extensions
        /// </summary>
        /// <param name="srcString"></param>
        /// <returns></returns>
        public static string HMACSHA384(this string srcString, string key)
        {
            Check.Argument.IsNotEmpty(srcString, nameof(srcString));
            return EncryptHelper.HMACSHA384(srcString, key);
        }

        /// <summary>
        /// String HMACSHA512 extensions
        /// </summary>
        /// <param name="srcString"></param>
        /// <returns></returns>
        public static string HMACSHA512(this string srcString, string key)
        {
            Check.Argument.IsNotEmpty(srcString, nameof(srcString));
            return EncryptHelper.HMACSHA512(srcString, key);
        }
    }
}
