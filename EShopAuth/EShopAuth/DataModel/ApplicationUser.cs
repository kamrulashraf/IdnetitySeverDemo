using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopAuth.DataModel
{
    public class ApplicationUser : IdentityUser
    {
        public string FristName{ get; set; }
        public string LastName { get; set; }
        public DateTime CreatedOn { get; set; }

    }
}
