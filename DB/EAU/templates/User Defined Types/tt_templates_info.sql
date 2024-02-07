CREATE TYPE [templates].[tt_templates_info] AS TABLE (
    [template_id] INT            NOT NULL,
    [name]        NVARCHAR (200) NOT NULL,
    [status]      SMALLINT       NOT NULL,
    [is_in_use]   BIT            NOT NULL);

