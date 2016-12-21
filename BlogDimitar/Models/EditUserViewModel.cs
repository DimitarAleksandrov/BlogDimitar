using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace BlogDimitar.Models
{
    public class EditUserViewModel
    {
        public ApplicationUser User { get; set; }

        [DisplayName("Парола")]
        public string Password { get; set; }

        [DisplayName("Потвърди паролата")]
        [Compare("Password", ErrorMessage = "Паролата не съвпада.")]
        public string ConfirmPassword { get; set; }

        public IList<Role> Roles { get; set; }
    }
}