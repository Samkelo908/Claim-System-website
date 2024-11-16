using System;
using Microsoft.AspNetCore.Http;

namespace PROG6212.PART2.Models
{
    public class ClaimModel
    {
        public string ClaimID { get; set; }

        public string MonthlyRate { get; set; }
        public string Training { get; set; }
        public string MiscellaneousExpenses { get; set; }
        public DateTime DateRangeStart { get; set; }
        public DateTime EndDate { get; set; } 
        public string PaymentMethod { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public string FileName { get; set; }
        public string UploaderName { get; set; }
        public DateTime UploadDate { get; set; }

        public IFormFile SupportingDocuments { get; set; }
    }
}
