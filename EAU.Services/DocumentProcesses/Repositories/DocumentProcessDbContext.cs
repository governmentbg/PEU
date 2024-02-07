using CNSys.Data;
using Dapper;
using EAU.Services.DocumentProcesses.Models;
using EAU.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Services.DocumentProcesses.Repositories
{
    internal class DocumentProcessDbContext : BaseDataContext
    {
        public DocumentProcessDbContext(IDbContext dbContext) : base(dbContext)
        {
        }

        #region Document processes

        public async Task<long?> DocumentProcessCreateAsync(
                                int? p_applicant_id,
                                int? p_document_type_id,
                                long? p_service_id,
                                short? p_status,
                                short? p_mode,
                                short? p_type,
                                AdditionalData p_additional_data,
                                Guid? p_signing_guid,
                                string p_error_message,
                                string p_case_file_uri,
                                string p_not_acknowledged_message_uri,
                                string p_request_id,
                                CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@p_document_process_id", dbType: DbType.Int64, direction: ParameterDirection.Output);
            parameters.Add("@p_applicant_id", p_applicant_id);
            parameters.Add("@p_document_type_id", p_document_type_id);
            parameters.Add("@p_service_id", p_service_id);
            parameters.Add("@p_status", p_status);
            parameters.Add("@p_mode", p_mode);
            parameters.Add("@p_type", p_type);
            parameters.Add("@p_additional_data", p_additional_data);
            parameters.Add("@p_signing_guid", p_signing_guid);
            parameters.Add("@p_error_message", p_error_message);
            parameters.Add("@p_case_file_uri", p_case_file_uri);
            parameters.Add("@p_not_acknowledged_message_uri", p_not_acknowledged_message_uri);
            parameters.Add("@p_request_id", p_request_id);

            await _dbContext.SPExecuteAsync("[dbo].[p_document_processes_create]", parameters, cancellationToken);

            long? id = parameters.Get<long?>("@p_document_process_id");

            return id;
        }

        public Task<CnsysGridReader> DocumentProcessesSearchAsync(long? p_document_process_id,
                                           int? p_applicant_cin,
                                           int? p_service_id,
                                           string p_request_id,
                                           Guid? p_signing_guid,
                                           short? p_type,
                                           bool? p_with_lock,
                                           bool? p_load_all_content,
                                           bool? p_load_attached_documents,
                                           bool? p_load_json_content,
                                           bool? p_load_xml_content,
                                           int? p_start_index,
                                           int? p_page_size,
                                           bool? p_calculate_count,
                                           CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@p_document_process_id", p_document_process_id);
            parameters.Add("@p_applicant_cin", p_applicant_cin);
            parameters.Add("@p_service_id", p_service_id);
            parameters.Add("@p_request_id", p_request_id);
            parameters.Add("@p_signing_guid", p_signing_guid);
            parameters.Add("@p_type", p_type);
            parameters.Add("@p_with_lock", p_with_lock);
            parameters.Add("@p_load_all_content", p_load_all_content);
            parameters.Add("@p_load_attached_documents", p_load_attached_documents);
            parameters.Add("@p_load_json_content", p_load_json_content);
            parameters.Add("@p_load_xml_content", p_load_xml_content);
            parameters.Add("@p_start_index", p_start_index);
            parameters.Add("@p_page_size", p_page_size);
            parameters.Add("@p_calculate_count", p_calculate_count);
            parameters.Add("@p_count", dbType: DbType.Int32, direction: System.Data.ParameterDirection.Output);

            return _dbContext.SPQueryMultipleAsync("[dbo].[p_document_processes_search]", parameters, cancellationToken);
        }

        public async Task DocumentProcessUpdateAsync(
                                long? p_document_process_id,
                                short? p_status,
                                AdditionalData p_additional_data,
                                Guid? p_signing_guid,
                                string p_error_message,
                                string p_case_file_uri,
                                string p_not_acknowledged_message_uri,
                                string p_request_id,
                                CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@p_document_process_id", p_document_process_id);
            parameters.Add("@p_status", p_status);
            parameters.Add("@p_additional_data", p_additional_data);
            parameters.Add("@p_signing_guid", p_signing_guid);
            parameters.Add("@p_error_message", p_error_message);
            parameters.Add("@p_case_file_uri", p_case_file_uri);
            parameters.Add("@p_not_acknowledged_message_uri", p_not_acknowledged_message_uri);
            parameters.Add("@p_request_id", p_request_id);

            await _dbContext.SPExecuteAsync("[dbo].[p_document_processes_update]", parameters, cancellationToken);
        }


        public async Task DocumentProcessDeleteAsync(long? p_document_process_id, CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@p_document_process_id", p_document_process_id);

            await _dbContext.SPExecuteAsync("[dbo].[p_document_processes_delete]", parameters, cancellationToken);
        }

        #endregion

        #region Attached Documents

        public async Task<long?> AttachedDocumentCreateAsync(
                                Guid? p_attached_document_guid,
                                long? p_document_process_id,
                                long? p_document_process_content_id,
                                int? p_document_type_id,
                                string p_description,
                                string p_mime_type,
                                string p_file_name,
                                string p_html_template_content,
                                Guid? p_signing_guid,
                                CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@p_attached_document_id", dbType: DbType.Int64, direction: ParameterDirection.Output);
            parameters.Add("@p_attached_document_guid", p_attached_document_guid);
            parameters.Add("@p_document_process_id", p_document_process_id);
            parameters.Add("@p_document_process_content_id", p_document_process_content_id);
            parameters.Add("@p_document_type_id", p_document_type_id);
            parameters.Add("@p_description", p_description);
            parameters.Add("@p_mime_type", p_mime_type);
            parameters.Add("@p_file_name", p_file_name);           
            parameters.Add("@p_html_template_content", p_html_template_content);
            parameters.Add("@p_signing_guid", p_signing_guid);

            await _dbContext.SPExecuteAsync("[dbo].[p_attached_documents_create]", parameters, cancellationToken);

            long? id = parameters.Get<long?>("@p_attached_document_id");

            return id;
        }

        public Task<CnsysGridReader> AttachedDocumentsSearchAsync(long? p_attached_document_id,
                                           long? p_document_process_id,
                                           bool? p_load_content,
                                           Guid? p_signing_guid,
                                           int? p_start_index,
                                           int? p_page_size,
                                           bool? p_calculate_count,
                                           CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@p_attached_document_id", p_attached_document_id);
            parameters.Add("@p_document_process_id", p_document_process_id);
            parameters.Add("@p_load_content", p_load_content);
            parameters.Add("@p_signing_guid", p_signing_guid);
            parameters.Add("@p_start_index", p_start_index);
            parameters.Add("@p_page_size", p_page_size);
            parameters.Add("@p_calculate_count", p_calculate_count);
            parameters.Add("@p_count", dbType: DbType.Int32, direction: System.Data.ParameterDirection.Output);

            return _dbContext.SPQueryMultipleAsync("[dbo].[p_attached_documents_search]", parameters, cancellationToken);
        }

        public async Task AttachedDocumentUpdateAsync(
                                long? p_attached_document_id,
                                string p_description,
                                string p_mime_type,
                                string p_file_name,
                                long? p_document_process_content_id,
                                string p_html_template_content,
                                Guid? p_signing_guid,
                                CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@p_attached_document_id", p_attached_document_id);
            parameters.Add("@p_description", p_description);
            parameters.Add("@p_mime_type", p_mime_type);
            parameters.Add("@p_file_name", p_file_name);
            parameters.Add("@p_document_process_content_id", p_document_process_content_id);
            parameters.Add("@p_html_template_content", p_html_template_content);
            parameters.Add("@p_signing_guid", p_signing_guid);

            await _dbContext.SPExecuteAsync("[dbo].[p_attached_documents_update]", parameters, cancellationToken);
        }

        public async Task AttachedDocumentDeleteAsync(long? p_attached_document_id, CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@p_attached_document_id", p_attached_document_id);

            await _dbContext.SPExecuteAsync("[dbo].[p_attached_documents_delete]", parameters, cancellationToken);
        }

        #endregion

        #region Document processes contents

        public async Task<long?> DocumentProcessContentCreateAsync(
                                long? p_document_process_id,
                                short? p_type,
                                string p_text_content,
                                CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@p_document_process_content_id", dbType: DbType.Int64, direction: ParameterDirection.Output);
            parameters.Add("@p_document_process_id", p_document_process_id);
            parameters.Add("@p_type", p_type);
            parameters.Add("@p_text_content", p_text_content);

            await _dbContext.SPExecuteAsync("[dbo].[p_document_process_contents_create]", parameters, cancellationToken);

            long? id = parameters.Get<long?>("@p_document_process_content_id");

            return id;
        }

        public async Task<IDataReader> DocumentProcessContentReadAsync(long? p_document_process_content_id, CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@p_document_process_content_id", p_document_process_content_id);

            IDataReader reader = await _dbContext.SPExecuteReaderAsync("[dbo].[p_document_process_contents_read]", parameters, cancellationToken);

            return reader;
        }

        public async Task<(IEnumerable<DocumentProcessContent> docContents, int? count)> DocumentProcessContentSearchAsync(List<long> p_document_process_ids,
                                          short? p_type,
                                          int? p_start_index,
                                          int? p_page_size,
                                          bool? p_calculate_count,
                                          CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(long));

            if (p_document_process_ids != null && p_document_process_ids.Count > 0)
            {
                p_document_process_ids.ForEach(el => dt.Rows.Add(el));
            }

            parameters.Add("@p_document_process_ids", dt.AsTableValuedParameter("[dbo].[tt_bigintegers]"));
            parameters.Add("@p_type", p_type);
            parameters.Add("@p_start_index", p_start_index);
            parameters.Add("@p_page_size", p_page_size);
            parameters.Add("@p_calculate_count", p_calculate_count);
            parameters.Add("@p_count", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var result = await _dbContext.SPQueryAsync<DocumentProcessContent>("[dbo].[p_document_process_contents_search]", parameters, cancellationToken);

            var p_count = parameters.Get<int?>("@p_count");

            return (result, p_count);
        }

        public async Task DocumentProcessContentUpdateAsync(
                               long? p_document_process_content_id,
                               string p_text_content,
                               CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@p_document_process_content_id", p_document_process_content_id);
            parameters.Add("@p_text_content", p_text_content);

            await _dbContext.SPExecuteAsync("[dbo].[p_document_process_contents_update]", parameters, cancellationToken);
        }

        public Task DocumentProcessContentUploadAsync(long? p_document_process_content_id,
                                                    byte[] p_content,
                                                    int? p_length,
                                                    CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@p_document_process_content_id", p_document_process_content_id);
            parameters.Add("@p_content", p_content, DbType.Binary, ParameterDirection.Input, p_length);
            //parameters.Add("@p_offset", p_offset);
            //parameters.Add("@p_length", p_length);

            return _dbContext.SPExecuteAsync("[dbo].[p_document_process_contents_upload]", parameters, cancellationToken);
        }

        public async Task DocumentProcessContentDeleteAsync(long? p_document_process_content_id, CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@p_document_process_content_id", p_document_process_content_id);

            await _dbContext.SPExecuteAsync("[dbo].[p_document_process_contents_delete]", parameters, cancellationToken);
        }

        #endregion

        public static Stream CreateDocumentProcessContentStream(long p_document_process_content_id, IDbContextProvider dbContextProvider)
        {
            return new SqlDeferredInitializedStream((context) =>
            {
                using (DocumentProcessDbContext dataContext = new DocumentProcessDbContext(context))
                {
                    return dataContext.DocumentProcessContentReadAsync(p_document_process_content_id, CancellationToken.None).GetAwaiter().GetResult();
                }
            }, dbContextProvider);
        }
    }
}