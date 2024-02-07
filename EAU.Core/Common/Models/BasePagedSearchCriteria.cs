using CNSys.Data;

namespace EAU.Common.Models
{
    /// <summary>
    /// Базови критерии за търсене.
    /// </summary>
    public class BasePagedSearchCriteria
    {
        /// <summary>
        /// Страница.
        /// </summary>
        public int? Page { get; set; }

        /// <summary>
        /// Размер на страницата.
        /// </summary>
        public int? PageSize { get; set; }

        /// <summary>
        /// Брой.
        /// </summary>
        public int? Count { get; set; }

        /// <summary>
        /// Флаг указващ дали да се използва "Брой максимално допустими записи от върнат резултат" от конфигурационните параметри.
        /// </summary>
        public bool LoadMaxNumberOfRecords { get; set; }

        private int? _maxNumberOfRecords;

        /// <summary>
        /// Брой максимално допустими записи от върнат резултат.
        /// </summary>
        public int? MaxNumberOfRecords
        {
            get
            {
                if (LoadMaxNumberOfRecords && _maxNumberOfRecords == null)
                {
                    return int.MaxValue;
                    //todo da se napravi APP_PARAMETERS TABLE
                    //var appParams = NomenclaturesCache.GetAppParameters();
                    //_maxNumberOfRecords = appParams.Single(x => x.ParameterName == AppParameters.MAX_SEARCH_COUNT.ToString()).NumberValue;
                }
                return _maxNumberOfRecords;
            }
            set
            {
                _maxNumberOfRecords = value;
            }

        }

        public PagedDataState ExtractState()
        {
            PagedDataState state = null;

            if (Page.HasValue && PageSize.HasValue)
                state = new PagedDataState(Page.Value, PageSize.Value, Count);
            else
                state = PagedDataState.CreateMaxPagedDataState();

            return state;
        }
    }
}
