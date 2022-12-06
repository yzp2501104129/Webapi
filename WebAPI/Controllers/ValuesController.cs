using AutoMapper;
using WebApi.Entity.Dto.Users;
using WebApi.Entity.Models.Login;
using WebApi.IService.IUserService;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebApi.UtilityApi.CustomizeException;
using WebAPI.ConfigurationTools.UserInformations.RoleAttributes;
using WebAPI.ConfigurationTools.Log4;
using WebAPI.Tools;
using WebAPI.Tools.Jwt;
using WebAPI.Tools.UserInformations;
using ClaimsIdentity = WebAPI.Tools.UserInformations.ClaimsIdentity;
using WebApi.UtilityApi.EncryptionDecryption;
using Newtonsoft.Json;

namespace WebAPI.Controllers
{
    public class ValuesController : ApiController
    {
        private readonly ILog _log;
        private readonly IUserService _users;
        private readonly IUserInformations _UserInformations;
        private readonly IMapper _mapper;
        public ValuesController(ILog log, IUserService users, IUserInformations userInformations, IMapper mapper)
        {
            _log = log;
            _users = users;
            _UserInformations = userInformations;
            _mapper = mapper;
        }

        // GET api/values
        [HttpGet]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Get()
        {
            var UsersModel = await _users.GetListAsync();
            List<UsersDto> usersDto = _mapper.Map<List<UsersDto>>(UsersModel);

            //throw new LoginFailException("1213123123");

            //Dictionary<string, object> result = new Dictionary<string, object>();
            //result.Add("UserName", "Cni00230"); 
            //result.Add("Sex", "男");
            //result.Add("Name", "张三");
            //result.Add("MobilePhone", "123456");
            //result.Add("DepartMentCode", "001");
            //result.Add("AuthenticationType", "admin");
            //result.Add("Birthday", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
            //result.Add("CreateTime", DateTime.Now.AddDays(1).ToString("yyyy-MM-dd hh:mm:ss"));

            //JwtRespone jwtRespone = JwtTools.JwtEncode(result);

            //if (jwtRespone._IsSuccess)
            //{
            //    return Ok(jwtRespone._Data);
            //}


            //if (_log.IsDebugEnabled)
            //{
            //    _log.Debug("111");
            //}
            //Log4Helper.Error("123", "123", "123", "123", "123", "123");

            return Json(usersDto);
        }

        // GET api/values/5
        [AllowAnonymous]
        [RoleAuthorize(Roles = ("111111"))]
        public string Get(string id)
        {
            throw new ServiceLogicException("1213123123");
            var sd = ((ClaimsIdentity)User.Identity);
            var users = _UserInformations.Infomations.UserName;
            return users;
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
