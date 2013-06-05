using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.ComponentModel.DataAnnotations;


namespace PhotoManagement.Validations
{
    public class ImageValidationAttribute:RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            var file = value as HttpPostedFileBase;
            if (file == null)
            {
                return false;
            }
            if (file.ContentLength > 10 * 1024 * 1024)
            {
                return false;
            }
            try
            {
                using (var img = Image.FromStream(file.InputStream))
                {
                    if (img.RawFormat.Equals(ImageFormat.Png))
                    {
                        return true;
                    }
                    else if (img.RawFormat.Equals(ImageFormat.Jpeg))
                    {
                        return true;
                    }
                }
            }
            catch { }
            return false;
        }
    }
}