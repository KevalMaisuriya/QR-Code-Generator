using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QRGenerator.Models
{
    public class QRCodeModel
    {
        [Display(Name = "QR Code Text")]
        public string QRCodeText { get; set; }

        [Display(Name = "QRCode Image")]
        public string QRCodeImagePath { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number Required!")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [Email(ErrorMessage = "The email address is not valid")]
        public string Email { get; set; }

        public string Message { get; set; }

        public string Name { get; set; }
    }
}