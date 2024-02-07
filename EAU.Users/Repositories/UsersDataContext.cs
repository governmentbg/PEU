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
    /// Клас, капсулиращ работата по извикването на процедурите от базата данни, свързани с потребители
    /// </summary>
    public class UsersDataContext : BaseDataContext
    {
        public UsersDataContext(IDbContext dbContext) : base(dbContext)
        {
        }

        #region Users 
                
        public async Task<(int?, long?, int?)> UserCreateAsync(
            string p_email, 
            byte? p_status,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("p_email", p_email);
            parameters.Add("p_status", p_status);
            parameters.Add("p_user_id", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
            parameters.Add("p_user_ver_id", dbType: System.Data.DbType.Int64, direction: System.Data.ParameterDirection.Output);
            parameters.Add("p_cin", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

            await _dbContext.SPExecuteAsync("[users].[p_users_create]", parameters, cancellationToken);

            var p_user_id = parameters.Get<int?>("p_user_id");
            var p_user_ver_id = parameters.Get<long?>("p_user_ver_id");
            var p_cin = parameters.Get<int?>("p_cin");

            return (p_user_id, p_user_ver_id, p_cin);
        }

        public async Task<long?> UserUpdateAsync(
            int p_user_id,
            string p_email,
            byte? p_status,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("p_user_id", p_user_id);
            parameters.Add("p_user_ver_id", dbType: System.Data.DbType.Int64, direction: System.Data.ParameterDirection.Output);
            parameters.Add("p_email", p_email);
            parameters.Add("p_status", p_status);
                        
            await _dbContext.SPExecuteAsync("[users].[p_users_update]", parameters, cancellationToken);

            var p_user_ver_id = parameters.Get<long?>("p_user_ver_id");

            return p_user_ver_id;
        }

        public Task UserDeleteAsync(int p_user_id, CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_user_id", p_user_id);

            return _dbContext.SPExecuteAsync("[users].[p_users_delete]", parameters, cancellationToken);
        }

        public Task UserDeleteDataAsync(int p_user_id, CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_user_id", p_user_id);

            return _dbContext.SPExecuteAsync("[users].[p_users_delete_data]", parameters, cancellationToken);
        }

        public async Task<(IEnumerable<User> data, int? count)> UsersSearchAsync(
            string p_users_ids,
            int? p_cin,
            string p_email,
            string p_username,
            string p_statuses,
            DateTime? p_date_from,
            DateTime? p_date_to,
            short? p_authentication_type,
            int? p_start_index,
            int? p_page_size,
            int? p_max_nor,
            bool? p_calculate_count,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("p_users_ids", p_users_ids);
            parameters.Add("p_cin", p_cin);
            parameters.Add("p_email", p_email);
            parameters.Add("p_username", p_username);
            parameters.Add("p_statuses", p_statuses);
            parameters.Add("p_date_from", p_date_from);
            parameters.Add("p_date_to", p_date_to);
            parameters.Add("p_authentication_type", p_authentication_type);
            parameters.Add("p_start_index", p_start_index);
            parameters.Add("p_page_size", p_page_size);
            parameters.Add("p_max_nor", p_max_nor);
            parameters.Add("p_calculate_count", p_calculate_count);
            parameters.Add("p_count", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

            var res = await _dbContext.SPQueryAsync<User>("[users].[p_users_search]", parameters, cancellationToken);
            
            int? count = parameters.Get<int?>("p_count");

            return (res, count);
        }

        #endregion

        #region User authentications

        public Task<IEnumerable<UserAuthentication>> UsersAuthenticationSearchAsync(
            string p_authentication_ids,
            int? p_user_id,
            short? p_authentication_type,
            string p_username,
            string p_certificate_hash,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("p_authentication_ids", p_authentication_ids);
            parameters.Add("p_user_id", p_user_id);
            parameters.Add("p_authentication_type", p_authentication_type);
            parameters.Add("p_username", p_username);
            parameters.Add("p_certificate_hash", p_certificate_hash);

            return _dbContext.SPQueryAsync<UserAuthentication>("[users].[p_user_authentications_search]", parameters, cancellationToken);
        }

        public async Task<int?> UsersAuthenticationCreateAsync(
            int p_user_id,
            short p_authentication_type,
            string p_password_hash,
            string p_username,
            int? p_certificate_id,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("p_user_id", p_user_id);
            parameters.Add("p_authentication_type", p_authentication_type);
            parameters.Add("p_password_hash", p_password_hash);
            parameters.Add("p_username", p_username);
            parameters.Add("p_certificate_id", p_certificate_id);
            parameters.Add("p_authentication_id", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

            await _dbContext.SPExecuteAsync("[users].[p_user_authentications_create]", parameters, cancellationToken);

            var p_authentication_id = parameters.Get<int?>("p_authentication_id");

            return p_authentication_id;
        }

        public Task UsersAuthenticationUpdateAsync(
            int p_authentication_id,
            int p_user_id,
            short p_authentication_type,
            string p_password_hash,
            string p_username,
            bool p_is_locked,
            DateTime? p_locked_until,
            int? p_certificate_id,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("p_authentication_id", p_authentication_id);
            parameters.Add("p_user_id", p_user_id);            
            parameters.Add("p_authentication_type", p_authentication_type);
            parameters.Add("p_password_hash", p_password_hash);
            parameters.Add("p_username", p_username);
            parameters.Add("p_is_locked", p_is_locked);
            parameters.Add("p_locked_until", p_locked_until);
            parameters.Add("p_certificate_id", p_certificate_id);

            return _dbContext.SPExecuteAsync("[users].[p_user_authentications_update]", parameters, cancellationToken);
        }

        public Task UsersAuthenticationDeleteAsync(
            int p_authentication_id,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_authentication_id", p_authentication_id);

            return _dbContext.SPExecuteAsync("[users].[p_user_authentications_delete]", parameters, cancellationToken);
        }

        #endregion

        #region Login sessions

        public Task UserLoginSessionCreateAsync(
            Guid p_login_session_id,
            Guid p_user_session_id,
            int p_user_id,
            DateTimeOffset p_login_date,
            DateTimeOffset? p_logout_date,
            byte[] p_ip_address,
            short p_authentication_type,
            int? p_certificate_id,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("p_login_session_id", p_login_session_id);
            parameters.Add("p_user_session_id", p_user_session_id);
            parameters.Add("p_user_id", p_user_id);
            parameters.Add("p_login_date", p_login_date);
            parameters.Add("p_logout_date", p_logout_date);
            parameters.Add("p_ip_address", p_ip_address);
            parameters.Add("p_authentication_type", p_authentication_type);
            parameters.Add("p_certificate_id", p_certificate_id);
            parameters.Add("p_id", dbType: System.Data.DbType.Int64, direction: System.Data.ParameterDirection.Output);

            return _dbContext.SPExecuteAsync("[users].[p_login_sessions_create]", parameters, cancellationToken);
        }

        public Task UserLoginSessionUpdateAsync(
            Guid p_login_session_id,
            Guid p_user_session_id,
            int p_user_id,
            DateTimeOffset p_login_date,
            DateTimeOffset? p_logout_date,
            byte[] p_ip_address,
            short p_authentication_type,
            int? p_certificate_id,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("p_login_session_id", p_login_session_id);
            parameters.Add("p_user_session_id", p_user_session_id);
            parameters.Add("p_user_id", p_user_id);
            parameters.Add("p_login_date", p_login_date);
            parameters.Add("p_logout_date", p_logout_date);
            parameters.Add("p_ip_address", p_ip_address);
            parameters.Add("p_authentication_type", p_authentication_type);
            parameters.Add("p_certificate_id", p_certificate_id);

            return _dbContext.SPExecuteAsync("[users].[p_login_sessions_update]", parameters, cancellationToken);
        }

        public async Task<(IEnumerable<UserLoginSession> data, int? count)> UsersLoginSessionSearchAsync(
            string p_login_session_ids,
            int? p_start_index,
            int? p_page_size,
            bool? p_calculate_count,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("p_login_session_ids", p_login_session_ids);
            parameters.Add("p_start_index", p_start_index);
            parameters.Add("p_page_size", p_page_size);
            parameters.Add("p_calculate_count", p_calculate_count);
            parameters.Add("p_count", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

            var res = await _dbContext.SPQueryAsync<UserLoginSession>("[users].[p_login_sessions_search]", parameters, cancellationToken);

            int? count = parameters.Get<int?>("p_count");

            return (res, count);
        }

        #endregion

        #region User Login Attempts 

        public async Task<int?> UserLoginAttemptCreateAsync(
            short? p_authentication_type,
            string p_login_name,
            int p_failed_login_attempts,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("p_authentication_type", p_authentication_type);
            parameters.Add("p_login_name", p_login_name);
            parameters.Add("p_failed_login_attempts", p_failed_login_attempts);
            parameters.Add("p_attempt_id", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

            await _dbContext.SPExecuteAsync("[users].p_user_failed_login_attempts_create", parameters, cancellationToken);

            int? p_attempt_id = parameters.Get<int?>("p_attempt_id");

            return p_attempt_id;
        }

        public Task UserLoginAttemptUpdateAsync(
            int p_attempt_id,
            short p_authentication_type,
            string p_login_name,
            int p_failed_login_attempts,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("p_attempt_id", p_attempt_id);
            parameters.Add("p_authentication_type", p_authentication_type);
            parameters.Add("p_login_name", p_login_name);
            parameters.Add("p_failed_login_attempts", p_failed_login_attempts);

            return _dbContext.SPExecuteAsync("[users].p_user_failed_login_attempts_update", parameters, cancellationToken);
        }

        public Task UserLoginAttemptDeleteAsync(
            int p_attempt_id,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("p_attempt_id", p_attempt_id);

            return _dbContext.SPExecuteAsync("[users].p_user_failed_login_attempts_delete", parameters, cancellationToken);
        }

        public Task<IEnumerable<UserLoginAttempt>> UsersLoginAttemptSearchAsync(
            string p_attempt_ids,
            string p_login_name,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("p_attempt_ids", p_attempt_ids);
            parameters.Add("p_login_name", p_login_name);

            return _dbContext.SPQueryAsync<UserLoginAttempt>("[users].p_user_failed_login_attempts_search", parameters, cancellationToken);
        }

        #endregion

        #region User Processes

        public async Task<int?> UserProcessesCreateAsync(
            int p_user_id,
            Guid p_process_guid,
            byte p_process_type,
            DateTime? p_invalid_after,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("p_user_id", p_user_id);
            parameters.Add("p_process_guid", p_process_guid);
            parameters.Add("p_process_type", p_process_type);
            parameters.Add("p_invalid_after", p_invalid_after);            
            parameters.Add("p_process_id", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

            await _dbContext.SPExecuteAsync("[users].[p_user_processes_create]", parameters, cancellationToken);

            int? p_attempt_id = parameters.Get<int?>("p_process_id");

            return p_attempt_id;
        }

        public Task UserProcessesUpdateAsync(
            int p_process_id,
            byte p_status,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("p_process_id", p_process_id);
            parameters.Add("p_status", p_status);

            return _dbContext.SPExecuteAsync("[users].[p_user_processes_update]", parameters, cancellationToken);
        }

        public async Task<(IEnumerable<UserProcess> data, int? count)> UserProcessesSearchAsync(
            string p_process_ids,
            string p_process_guids,
            string p_user_ids,
            int? p_process_type,
            int? p_start_index,
            int? p_page_size,
            bool? p_calculate_count,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("p_process_ids", p_process_ids);
            parameters.Add("p_process_guids", p_process_guids);
            parameters.Add("p_user_ids", p_user_ids);
            parameters.Add("p_process_type", p_process_type);
            parameters.Add("p_start_index", p_start_index);
            parameters.Add("p_page_size", p_page_size);
            parameters.Add("p_calculate_count", p_calculate_count);
            parameters.Add("p_count", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

            var data = await _dbContext.SPQueryAsync<UserProcess>("[users].[p_user_processes_search]", parameters, cancellationToken);

            int? p_count = parameters.Get<int?>("p_count");

            return (data, p_count);
        }

        #endregion

        public Task UserPermissionCreateAsync(
            int p_user_id,
            int p_permission_id,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("p_user_id", p_user_id);
            parameters.Add("p_permission_id", p_permission_id);

            return _dbContext.SPExecuteAsync("[users].[p_user_permissions_create]", parameters, cancellationToken);
        }

        public Task UserPermissionDeleteAsync(
            int p_user_id,
            int p_permission_id,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("p_user_id", p_user_id);
            parameters.Add("p_permission_id", p_permission_id);

            return _dbContext.SPExecuteAsync("[users].[p_user_permissions_delete]", parameters, cancellationToken);
        }

        public Task<IEnumerable<UserPermission>> UserPermissionsSearchAsync(
            string p_users_ids,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_users_ids", p_users_ids);

            return _dbContext.SPQueryAsync<UserPermission>("[users].[p_user_permissions_search]", parameters, cancellationToken);
        }
    }
}
