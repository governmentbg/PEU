using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EAU.Utilities
{
    public class DateTimeOffsetTypeHandler : SqlMapper.TypeHandler<DateTime>
    {
        //Handles how data is deserialized into object

        public override DateTime Parse(object value)
        {
            if (value is DateTime)
            {
                return (DateTime)value;
            }

            if (value is DateTimeOffset)
            {
                return ((DateTimeOffset)value).LocalDateTime;
            }

            DateTimeOffset dateOffsVal;
            DateTimeOffset.TryParse(value.ToString(), out dateOffsVal);

            return dateOffsVal.LocalDateTime;
        }

        //Handles how data is saved into the database
        public override void SetValue(IDbDataParameter parameter, DateTime value)
        {
            parameter.Value = new DateTimeOffset(value);
        }
    }
}
