using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace EAU.Utilities
{
    /// <summary>
    /// Wrap-ър клас на Dapper.SqlMapper.GridReader, който улеснява работата с него и изчитането на out параметри.
    /// </summary>
    public class CnsysGridReader : IDisposable
    {
        private readonly GridReader _inner = null;
        private readonly DynamicParameters _parameters = null;

        public CnsysGridReader(GridReader reader, DynamicParameters parameters = null)
        {
            _inner = reader;
            _parameters = parameters;
        }
        public Task<IEnumerable<T>> ReadAsync<T>() => _inner.ReadAsync<T>();

        public T ReadOutParameter<T>(string paramName)
        {
            if (string.IsNullOrEmpty(paramName)) throw new ArgumentNullException(nameof(paramName));

            if (_parameters == null) throw new InvalidOperationException();

            return _parameters.Get<T>(paramName);
        }

        public void Dispose() => _inner?.Dispose();
    }
}
