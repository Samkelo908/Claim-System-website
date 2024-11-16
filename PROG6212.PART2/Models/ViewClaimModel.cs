namespace PROG6212.PART2.Models
{
    public class ViewClaimModel
    {
        public string ClaimID { get; set; } 
        public string MonthlyRate { get; set; }
        public string MiscellaneousExpenses { get; set; }
        public DateTime DateRangeStart { get; set; }
        public DateTime EndDate { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime SubmittedAt { get; set; }
        public string Status { get; set; }
        public string SelectedStatus { get; set; }

        public decimal TotalAmount { get; set; }
    }

}
