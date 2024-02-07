using EAU.Utilities;
using System;

namespace EAU.Common.Models
{
    /// <summary>
    /// Клас за работа с параметри.
    /// </summary>
    public class AppParameter
    {
        /// <summary>
        /// Идентиификатор на параметър.
        /// </summary>
        [DapperColumn("app_param_id")]
        public int? AppParamID { get; set; }

        /// <summary>
        /// Уникален идентификатор на функционалност.
        /// </summary>
        [DapperColumn("functionality_id")]
        public Functionalities? FunctionalityID { get; set; }

        /// <summary>
        /// Код на параметър.
        /// </summary>
        [DapperColumn("code")]
        public string Code { get; set; }

        /// <summary>
        /// Описание на параметър.
        /// </summary>
        [DapperColumn("description")]
        public string Description { get; set; }

        /// <summary>
        /// Флаг, указващ дали параметъра е системен или не.
        /// </summary>
        [DapperColumn("is_system")]
        public bool? IsSystem { get; set; }

        /// <summary>
        /// Тип на параметъра.
        /// </summary>
        [DapperColumn("param_type")]
        public AppParameterTypes? ParameterType { get; set; }

        /// <summary>
        /// Стойност са тип на параметър дата, час и минути.
        /// </summary>
        [DapperColumn("value_datetime")]
        public DateTime? ValueDateTime { get; set; }

        /// <summary>
        /// Стойност са тип на параметър интервал от време представен като дата.
        /// </summary>
        [DapperColumn("value_interval")]
        public DateTime? ValueIntervalFromStartDate { get; set; }

        /// <summary>
        /// Стойност са тип на параметър интервал от време.
        /// </summary>
        public TimeSpan? ValueInterval
        {
            get { return ValueIntervalFromStartDate - new DateTime(1900, 01, 01, 00, 00, 00, 00); }
            set { ValueIntervalFromStartDate = new DateTime(1900, 01, 01, 00, 00, 00, 00) + value; }
        }

        /// <summary>
        /// Стойност са тип на параметър стринг.
        /// </summary>
        [DapperColumn("value_string")]
        public string ValueString { get; set; }

        /// <summary>
        /// Стойност са тип на параметър цяло число.
        /// </summary>
        [DapperColumn("value_int")]
        public int? ValueInt { get; set; }

        /// <summary>
        /// Стойност са тип параметър час и минути.
        /// </summary>
        [DapperColumn("value_hour")]
        public TimeSpan? ValueHour { get; set; }
    }

    /// <summary>
    /// Типове на параметри.
    /// 1 = Дата, час и минути.; 2 = Период от време.; 3 = Стринг.; 4 = Цяло число.; 5 = Час и минута.;
    /// </summary>
    public enum AppParameterTypes
    {
        /// <summary>
        /// Дата, час и минути.
        /// </summary>
        DateTime = 1,

        /// <summary>
        /// Период от време.
        /// </summary>
        Interval = 2,

        /// <summary>
        /// Стринг.
        /// </summary>
        String = 3,

        /// <summary>
        /// Цяло число.
        /// </summary>
        Integer = 4,

        /// <summary>
        /// Час и минута.
        /// </summary>
        HourAndMinute = 5
    }
}
