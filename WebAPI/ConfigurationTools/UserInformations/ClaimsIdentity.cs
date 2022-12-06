using WebApi.Entity.Models.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace WebAPI.Tools.UserInformations
{
    /// <summary>
    /// 身份证
    /// </summary>
    public class ClaimsIdentity : IIdentity
    {
        public ClaimsIdentity(UsersModel usersModel)
        {
            Name = usersModel.Name;
            UserName = usersModel.UserName;
            Sex = usersModel.Sex;
            MobilePhone = usersModel.MobilePhone;
            Birthday = usersModel.Birthday;
            CreateTime = usersModel.CreateTime;
            ModificationTime = usersModel.ModificationTime;
            IsDeleted = usersModel.IsDeleted;
            Enabled = usersModel.Enabled;
            DepartMentCode = usersModel.DepartMentCode;

            AuthenticationType = usersModel.AuthenticationType;
            IsAuthenticated = true;
        }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Sex { get; set; }
        public string MobilePhone { get; set; }
        public DateTime Birthday { get; set; }
        public string DepartMentCode { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime ModificationTime { get; set; }
        public bool IsDeleted { get; set; }
        public bool Enabled { get; set; }
        /// <summary>
        /// 身份类型
        /// </summary>
        public string AuthenticationType { get; set; }
        /// <summary>
        /// 是否通过校验
        /// </summary>
        public bool IsAuthenticated { get; set; }
    }
}