using EAU.Nomenclatures.Models;

namespace EAU.Documents.Models
{
    public class DeadlineVM
    {
        public ExecutionPeriodType? ExecutionPeriodType
        {
            get;
            set;
        }

        public int? PeriodValue 
        { 
            get; 
            set; 
        }

        public override string ToString()
        {
            if (PeriodValue.HasValue && ExecutionPeriodType.HasValue)
                return FormatTermTypeTerm(PeriodValue.Value, ExecutionPeriodType.Value);
            else
                return string.Empty;
        }

        public string OriginalDeadline
        {
            get;
            set;
        }

        private static string FormatTermTypeTerm(int ExecutionPeriod, ExecutionPeriodType ExecutionPeriodType)
        {
            switch (ExecutionPeriodType)
            {
                case ExecutionPeriodType.Days:
                    if (ExecutionPeriod == 1)
                        return string.Format("{0} {1}", ExecutionPeriod, "ден ТОДО");
                    else
                        return string.Format("{0} {1}", ExecutionPeriod, "дни тодо");
                case ExecutionPeriodType.Hours:
                    if (ExecutionPeriod == 1)
                        return string.Format("{0} {1}", ExecutionPeriod, "час тодо");
                    else
                        return string.Format("{0} {1}", ExecutionPeriod, "часа тодо");
                default:
                    return "";
            }
        }
    }
}
