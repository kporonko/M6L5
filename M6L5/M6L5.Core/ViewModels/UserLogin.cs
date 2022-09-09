using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M6L5.Core.ViewModels
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Enter login")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Login must be a phone number or email address")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Enter Password")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Password must contain from 8 to 20 symbols")]
        public string Password { get; set; }
    }
}
