using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace PROG6212.PART2.Models
{
    public class UploadDocuments
    {
        [Display(Name = "Receipt")]
        public IFormFile Receipt { get; set; }

        [Display(Name = "Invoice")]
        public IFormFile Invoice { get; set; }

        [Display(Name = "Timesheet")]
        public IFormFile Timesheet { get; set; }

        [Display(Name = "Approval Letter")]
        public IFormFile ApprovalLetter { get; set; }

        [Display(Name = "Other")]
        public IFormFile Other { get; set; }
    }
}
