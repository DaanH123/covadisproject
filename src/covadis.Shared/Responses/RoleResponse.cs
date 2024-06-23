using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace covadis.Shared.Responses
{
    public class RoleResponse : BaseResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
