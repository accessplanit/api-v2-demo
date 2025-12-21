using Constants;

namespace Services.Models
{
    public class Filter
    {
        public FilterExpressions FilterExpression { get; set; }

        public string PropertyName { get; set; }

        public string FilterValue { get; set; }

        public LogicalOperator? LogicalOperator { get; set; }

        public Filter(string propertyName, FilterExpressions filterExpression, string filterValue) 
        { 
            FilterExpression = filterExpression;
            PropertyName = propertyName;
            FilterValue = filterValue;
        }
    }
}
