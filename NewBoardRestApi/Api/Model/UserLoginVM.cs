using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBoardRestApi.Api.Model
{
    public class UserLoginVM
    {
        public string Email { get; set; } = "";

        public string Password { get; set; } = "";
    }
}
