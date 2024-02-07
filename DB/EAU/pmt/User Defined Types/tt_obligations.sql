CREATE TYPE [pmt].[tt_obligations] AS TABLE (
    [obligation_id]             BIGINT          NOT NULL,
    [status]                    TINYINT         NOT NULL,
    [amount]                    MONEY           NOT NULL,
    [discount_amount]           MONEY           NOT NULL,
    [bank_name]                 NVARCHAR (500)  NULL,
    [bic]                       NVARCHAR (8)    NOT NULL,
    [iban]                      NVARCHAR (22)   NOT NULL,
    [payment_reason]            NVARCHAR (2000) NOT NULL,
    [pep_cin]                   NVARCHAR (100)  NULL,
    [expiration_date]           DATETIME        NOT NULL,
    [applicant_id]              INT             NULL,
    [obliged_person_name]       NVARCHAR (150)  NULL,
    [obliged_person_ident]      NVARCHAR (100)  NULL,
    [obliged_person_ident_type] TINYINT         NULL,
    [obligation_date]           DATE            NOT NULL,
    [obligation_identifier]     NVARCHAR (300)  NOT NULL,
    [type]                      TINYINT         NOT NULL,
    [service_instance_id]       BIGINT          NULL,
    [service_id]                INT             NOT NULL,
    [additional_data]           NVARCHAR (MAX)  NULL);















