using CNSys.Data;
using Dapper;
using EAU.Users.Models;
using EAU.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Users.Repositories
{
    /// <summary>
    /// Клас капсулиращ работата по извикване на процедурите в базата данни свързани със сертификатите на потребителите.
    /// </summary>
    public class CertificateDataContext : BaseDataContext
    {
        public CertificateDataContext(IDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<int?> CertificatesCreateAsync(
            string p_serial_number, string p_issuer, string p_subject, DateTimeOffset? p_not_after, DateTimeOffset? p_not_before, string p_thumbprint, byte[] p_content,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("p_serial_number", p_serial_number);
            parameters.Add("p_issuer", p_issuer);
            parameters.Add("p_subject", p_subject);
            parameters.Add("p_not_after", p_not_after);
            parameters.Add("p_not_before", p_not_before);
            parameters.Add("p_thumbprint", p_thumbprint);
            parameters.Add("p_content", p_content);
            parameters.Add("p_certificate_id", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

            await _dbContext.SPExecuteAsync("[users].[p_certificates_create]", parameters, cancellationToken);

            var p_certificate_id = parameters.Get<int?>("p_certificate_id");

            return p_certificate_id;
        }

        public Task<IEnumerable<Certificate>> CertificatesSearchAsync(
            string p_certificate_ids,
            string p_cert_hash,
            string p_cert_sernum,
            bool p_load_content,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("p_certificate_ids", p_certificate_ids);
            parameters.Add("p_cert_hash", p_cert_hash);
            parameters.Add("p_cert_sernum", p_cert_sernum);
            parameters.Add("p_load_content", p_load_content);

            return _dbContext.SPQueryAsync<Certificate>("[users].[p_certificates_search]", parameters, cancellationToken);
        }
    }
}
