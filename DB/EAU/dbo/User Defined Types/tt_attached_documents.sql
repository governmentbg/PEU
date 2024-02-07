CREATE TYPE [dbo].[tt_attached_documents] AS TABLE (
    [attached_document_id]        BIGINT             NOT NULL,
    [attached_document_guid]      UNIQUEIDENTIFIER   NOT NULL,
    [document_process_id]         BIGINT             NOT NULL,
    [document_process_content_id] BIGINT             NULL,
    [document_type_id]            INT                NULL,
    [description]                 NVARCHAR (1000)    NULL,
    [mime_type]                   NVARCHAR (100)     NULL,
    [file_name]                   NVARCHAR (200)     NULL,
    [html_template_content]       NVARCHAR (MAX)     NULL,
    [signing_guid]                UNIQUEIDENTIFIER   NULL,
    [created_by]                  INT                NOT NULL,
    [created_on]                  DATETIMEOFFSET (3) NOT NULL,
    [updated_by]                  INT                NOT NULL,
    [updated_on]                  DATETIMEOFFSET (3) NOT NULL);





