using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SqlSugar;

namespace WebApi.Entity.Models.Login
{
    /// <summary>
    /// 当类名和表民不一样时指定表名
    /// </summary>
    [SugarTable("User")]
    public class UsersModel
    {
        /// <summary>
        /// 是否为主键，是否自增
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        /// <summary>
        /// 标识列id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string MobilePhone { get; set; }

        /// <summary>
        /// 部门编号
        /// </summary>
        public string DepartMentCode { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModificationTime { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 身份类型
        /// </summary>
        public string AuthenticationType { get; set; }
    }
}