using System;

namespace QRGenerator.Models
{
    internal class EmailAttribute : Attribute
    {
        public string ErrorMessage { get; set; }
    }
}