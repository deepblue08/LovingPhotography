using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using PhotoManagement.Validations;

namespace PhotoManagement.Models
{
    public class Photo
    {
        public virtual int PhotoId { get; set; }
        public virtual int ClientId { get; set; }
        public virtual string PhotoDescription { get; set; }
        [ImageValidation(ErrorMessage="Please select a PNG/JPEG image smaller than 10 MB")]
        [NotMapped]
        public HttpPostedFileBase File { get; set; }
    }
}