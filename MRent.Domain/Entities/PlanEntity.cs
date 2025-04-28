using MRent.Domain.Base;

namespace MRent.Domain.Entities
{
    public class PlanEntity : BaseEntity
    {
        public int Days { get; set; }
        public double DailyValue { get; set; }
        public int ReturnFeePercent { get; set; }
        public int DailyExceededEndDateFee { get; set; }

        public virtual ICollection<RentEntity> Rents { get; set; } = new List<RentEntity>();
    }
}
