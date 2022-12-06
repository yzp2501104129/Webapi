using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Entity.Dto.Users
{
    public class UsersDto
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Sex { get; set; }

        public string Name { get; set; }

        public string MobilePhone { get; set; }

        public string DepartMentCode { get; set; }

        public DateTime Birthday { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime ModificationTime { get; set; }

        public bool IsDeleted { get; set; }

        public bool Enabled { get; set; }

        public string AuthenticationType { get; set; }
    }
}
