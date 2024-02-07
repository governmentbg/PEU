using CNSys.Data;
using CNSys.IO;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.IO;

namespace EAU.Utilities
{
    /// <summary>
    /// Наследник на DeferredInitializedStream, подготвя 
    /// </summary>
    public class SqlDeferredInitializedStream : DeferredInitializedStream
    {
        #region Private Members

        private readonly Func<IDbContext, IDataReader> _readerGenerator;
        private Stream _innerStream;
        private IWrappedDataReader _reader;
        private IDbContext _dbContext;
        private readonly IDbContextProvider _dbContextProvider;

        #endregion

        #region Constructors

        public SqlDeferredInitializedStream(Func<IDbContext, IDataReader> readerGenerator, IDbContextProvider dbContextProvider)
        {
            _readerGenerator = readerGenerator;
            _dbContextProvider = dbContextProvider;
        }

        #endregion

        #region Overriden Methods

        public override bool CanWrite
        {
            get { return false; }
        }

        protected override Stream InnerStream
        {
            get { return _innerStream; }
        }

        protected override void InitializeInnerStream()
        {
            _dbContext = _dbContextProvider.CreateContext();
            _dbContext.InitContextAsync().GetAwaiter().GetResult();

            _reader = _readerGenerator(_dbContext) as IWrappedDataReader;
            var sqlReader = _reader.Reader as SqlDataReader;

            if (sqlReader.HasRows)
                _innerStream = new SqlVarBinaryStream(sqlReader);
            else
                _innerStream = new MemoryStream(Array.Empty<byte>(), false);

            base.InitializeInnerStream();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (_innerStream != null)
            {
                _innerStream.Dispose();
            }

            if (_reader != null && !_reader.IsClosed)
            {
                _reader.Close();
                _reader.Dispose();
            }

            /*винаги се вика Dispose, за да може, ако се вика няколко пъти на stream - а през вътрешни ExecutionContext - и то да се освободи през външния! */
            _dbContext?.Complete();
            _dbContext?.Dispose();
        }

        #endregion
    }    
}
