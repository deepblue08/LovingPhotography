using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhotoManagement.Models
{
    public class Client
    {
        public virtual int ClientId { get; set; }
        [DisplayName("Client's Name:")]
        public virtual string ClientName { get; set; }
        [DisplayName("Date:")]
        public virtual DateTime ClientDate { get; set; }
        [Required]
        [DisplayName("User Name:")]
        public virtual string UserName { get; set; }
        [Required]
        [DisplayName("Password:")]
        public virtual string Password { get; set; }
        public virtual ICollection<Photo> Photo { get; set; }
    }
}