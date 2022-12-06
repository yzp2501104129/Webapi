using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace WebAPI.Tools.Jwt
{
    /// <summary>
    /// 身份校验失败统一返回
    /// </summary>
    public class AuthenticationFaileResult : IHttpActionResult
    {
        /// <summary>
        /// 异常消息
        /// </summary>
        private string _Message { get; set; }
        /// <summary>
        /// 异常请求
        /// </summary>
        private HttpRequestMessage _HttpRequest { get; set; }
        /// <summary>
        /// 相应内容
        /// </summary>
        private HttpContent _HttpContent { get; set; }
        public AuthenticationFaileResult(HttpRequestMessage HttpRequest, string httpContent, string Message = null)
        {
            _Message = Message;
            _HttpRequest = HttpRequest;
            _HttpContent = new StringContent(JsonConvert.SerializeObject(new JwtRespone() { _Data = null, _IsSuccess = false, _Message = httpContent }));
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }
        public HttpResponseMessage Execute()
        {
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.Unauthorized,
                ReasonPhrase = this._Message,
                RequestMessage = this._HttpRequest,
                Content = this._HttpContent,
            };
        }
    }
}