using JWT;
using JWT.Algorithms;
using JWT.Exceptions;
using JWT.Serializers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Web;
using WebApi.UtilityApi.Tools.Redis;
using WebApi.Entity.Models.Login;
using WebAPI.Tools.Jwt;

namespace WebAPI.Tools
{
    public class JwtTools
    {
        /// <summary>
        /// Jwt秘钥
        /// </summary>
        private static readonly string JwtSecretkey = ConfigurationManager.AppSettings["JwtSecretkey"];
        /// <summary>
        /// Token过期时间【小时】
        /// </summary>
        private static readonly int TokenExpirationTime = Convert.ToInt32(ConfigurationManager.AppSettings["TokenExpirationTime"]);

        /// <summary>
        /// Jwt加密产生token秘文       7.2.1版本稳定
        /// </summary>
        /// <param name="payload">加密数据key-value</param>
        /// <param name="secret">加密的密钥</param>
        /// <returns></returns>
        public static JwtRespone JwtEncode(Dictionary<string, object> payload, string secret = null)
        {
            if (payload == null)
            {
                return new JwtRespone() { _IsSuccess = false, _Data = null, _Message = "加密参数为空" };
            }
            if (string.IsNullOrEmpty(secret))
            {
                secret = JwtSecretkey;
            }
            string Token = string.Empty;
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            Token = encoder.Encode(payload, secret);

            string RedisKey = payload.Keys.FirstOrDefault(X => X.Contains("UserName"));
            if (string.IsNullOrEmpty(RedisKey))
            {
                return new JwtRespone() { _IsSuccess = false, _Data = null, _Message = "请传入用户名" };
            }
            RedisTools.Set<string>(RedisKey, Token, DateTime.Now.AddHours(TokenExpirationTime));

            return new JwtRespone() { _IsSuccess = true, _Data = Token };
        }

        /// <summary>
        /// 解密的token字符串  7.2.1版本稳定
        /// </summary>
        /// <param name="Token">需要解密的Token</param>
        /// <param name="secret">密钥</param>
        /// <returns></returns>
        public static JwtRespone JwtDecode(string Token, string secret = null)
        {
            try
            {
                if (string.IsNullOrEmpty(Token))
                {
                    return new JwtRespone() { _IsSuccess = false, _Data = null, _Message = "校验的Token值为空" };
                }
                if (string.IsNullOrEmpty(secret))
                {
                    secret = JwtSecretkey;
                }

                IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithm);

                string JSon = decoder.Decode(Token, secret, true);
                return new JwtRespone() { _IsSuccess = true, _Data = JSon };
            }
            catch (SignatureVerificationException Failex)
            {
                return new JwtRespone() { _IsSuccess = false, _Data = null, _Message = string.Format("签名验证失败；异常{0}", Failex) };
            }
            catch (Exception ex)
            {
                return new JwtRespone() { _IsSuccess = false, _Data = null, _Message = string.Format("未知的错误；异常{0}", ex) };
            }
        }

        /// <summary>
        /// 校验token
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="usersModel">当前用户名作为redis的key</param>
        /// <returns></returns>
        public static JwtRespone CheckJwtDecode(string Token, out UsersModel usersModel)
        {
            if (string.IsNullOrEmpty(Token))
            {
                usersModel = null;
                return new JwtRespone() { _IsSuccess = false, _Data = null, _Message = "Token值为空" };
            }
            JwtRespone jwtRespone = JwtDecode(Token, JwtSecretkey);
            usersModel = JsonConvert.DeserializeObject<UsersModel>(jwtRespone._Data);
            if (jwtRespone._IsSuccess)
            {
                if (string.IsNullOrEmpty(RedisTools.GetValueString(usersModel.UserName)))
                {
                    return new JwtRespone() { _IsSuccess = false, _Data = null, _Message = "Token值已经过期请重新登录" };
                }
                return new JwtRespone() { _IsSuccess = true, _Data = jwtRespone._Data };
            }
            else
            {
                return jwtRespone;
            }
        }
        /// <summary>
        /// 刷新Token  
        /// </summary>
        /// <param name="Token">过期的Token</param>
        /// <returns></returns>
        public static string RefreshToken(string Token)
        {
            var Json = Base64UrlDecode(Token);
            Dictionary<string, object> keyValuePairs = JsonConvert.DeserializeObject<Dictionary<string, object>>(Json);
            JwtRespone jwtRespone = JwtEncode(keyValuePairs);
            return jwtRespone._IsSuccess == true ? jwtRespone._Data : "";
        }

        /// <summary>
        /// Base64解码
        /// </summary>
        /// <param name="base64UrlStr">Jwt第二段密文</param>
        /// <returns></returns>
        public static string Base64UrlDecode(string base64UrlStr)
        {
            base64UrlStr = base64UrlStr.Replace('-', '+').Replace('_', '/');
            switch (base64UrlStr.Length % 4)
            {
                case 2:
                    base64UrlStr += "==";
                    break;
                case 3:
                    base64UrlStr += "=";
                    break;
            }
            var bytes = Convert.FromBase64String(base64UrlStr);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}