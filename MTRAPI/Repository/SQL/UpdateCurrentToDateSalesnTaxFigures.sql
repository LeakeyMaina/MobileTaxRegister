DROP TRIGGER [UpdateSalesnTaxFigures]
GO

CREATE TRIGGER [UpdateSalesnTaxFigures]
ON [MTR].[dbo].[Sales]
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

	DECLARE @ETRID AS NVARCHAR

	DECLARE @CurrentMonthSalesAmount AS FLOAT
	DECLARE @CurrentMonthVATAmount AS FLOAT
	DECLARE @CurrentYearSalesAmount AS FLOAT
	DECLARE @CurrentYearVATAmount AS FLOAT

	select @ETRID = ETRID from inserted

	SELECT
		@CurrentMonthSalesAmount=Format(sum(SaleAmount), 'N') ,
		@CurrentMonthVATAmount=Format(sum(VATAmount), 'N') 
	from [MTR].[dbo].[Sales]
	WHERE ETRid = @ETRID
	and MONTH(SaleDate) = MONTH(GETDATE()) 
	AND YEAR(SaleDate) = YEAR(GETDATE())

	SELECT
		@CurrentYearSalesAmount=Format(sum(SaleAmount), 'N'),
		@CurrentYearVATAmount=Format(sum(VATAmount), 'N') 
	from [MTR].[dbo].[Sales]
	WHERE ETRid = @ETRID
	AND YEAR(SaleDate) = YEAR(GETDATE())


	
	UPDATE [MTR].[dbo].ETRs
	SET 
		[CurrentMonthSalesAmount] += @CurrentMonthSalesAmount,
		[CurrentMonthVATAmount] += @CurrentMonthVATAmount,
		[CurrentYearSalesAmount] += @CurrentYearSalesAmount,
		[CurrentYearVATAmount] += @CurrentYearVATAmount
	WHERE
		[ETRID]= @ETRID

END
