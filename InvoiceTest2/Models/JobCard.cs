using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InvoiceTest2.Models
{
    public class JobCard
    {
        [Key]
        public int JobID { get; set; }
        public string PartsUsed { get; set; }
        public string FeesIncluded { get; set; }
        public double TotalAmount { get; set; }
        public string PhoneNo { get; set; }

        [Required(ErrorMessage = "Please Enter Your Email")]
        [Display(Name = "Email Address:")]
        [EmailAddress]
        public string Email { get; set; }
    }
}