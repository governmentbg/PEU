using Microsoft.Data.SqlClient;
using System;
using System.IO;

namespace EAU.Utilities
{
    public class SqlVarBinaryStream : Stream
    {
        private readonly SqlDataReader _reader;
        private int _columnIndex = 0;
        private long _position = 0;

        #region Construcotrs

        public SqlVarBinaryStream(SqlDataReader reader)
        {
            _position = 0;
            _reader = reader;
            _reader.Read();
        }

        #endregion

        #region Overriden Methods

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanSeek
        {
            get { return false; }
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override long Length
        {
            get { throw new NotSupportedException(); }
        }

        public override long Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public override void Flush()
        {
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            switch (origin)
            {
                case SeekOrigin.Begin:
                    {
                        if ((offset < 0) && (offset > this.Length))
                            throw new ArgumentException("Invalid seek origin.");
                        _position = offset;
                        break;
                    }
                case SeekOrigin.End:
                    {
                        if ((offset > 0) && (offset < -this.Length))
                            throw new ArgumentException("Invalid seek origin.");
                        _position = this.Length - offset;
                        break;
                    }
                case SeekOrigin.Current:
                    {
                        if ((_position + offset > this.Length) || (_position + offset < 0))
                            throw new ArgumentException("Invalid seek origin.");
                        _position = _position + offset;
                        break;
                    }
                default:
                    {
                        throw new ArgumentOutOfRangeException("origin", origin, "Unknown SeekOrigin");
                    }
            }
            return _position;
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int _bytesRead = Convert.ToInt32(_reader.GetBytes(_columnIndex, _position, buffer, offset, count));
            _position += (long)_bytesRead;
            return (int)_bytesRead;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }

        protected override void Dispose(bool disposing)
        {
            if (_reader != null)
                _reader.Dispose();

            base.Dispose(disposing);
        }

        #endregion

        #region Helpers

        private static byte[] GetWriteBuffer(byte[] buffer, int count, int offset)
        {
            if (buffer.Length == count)
                return buffer;
            byte[] data = new byte[count];
            Buffer.BlockCopy(buffer, offset, data, 0, count);
            return data;
        }

        #endregion
    }
}
