CREATE TYPE [pmt].[tt_obligations_search_ids] AS TABLE (
    [obligation_date]       DATE           NOT NULL,
    [obligation_identifier] NVARCHAR (300) NOT NULL);

