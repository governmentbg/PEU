CREATE TYPE [dbo].[tt_document_processes] AS TABLE (
    [document_process_id]          BIGINT             NULL,
    [applicant_id]                 INT                NULL,
    [document_type_id]             INT                NULL,
    [service_id]                   INT                NULL,
    [status]                       SMALLINT           NULL,
    [mode]                         SMALLINT           NULL,
    [type]                         SMALLINT           NULL,
    [additional_data]              NVARCHAR (MAX)     NULL,
    [signing_guid]                 UNIQUEIDENTIFIER   NULL,
    [error_message]                NVARCHAR (250)     NULL,
    [case_file_uri]                NVARCHAR (100)     NULL,
    [not_acknowledged_message_uri] NVARCHAR (500)     NULL,
    [request_id]                   NVARCHAR (50)      NULL,
    [created_on]                   DATETIMEOFFSET (3) NULL);





