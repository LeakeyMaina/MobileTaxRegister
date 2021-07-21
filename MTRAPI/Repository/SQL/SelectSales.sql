/****** Script for SelectTopNRows command from SSMS  ******/
SELECT [SaleID]
      ,[SaleDate]
      ,Format(SaleAmount, 'N') as [SaleAmount]
      ,Format(VATAmount, 'N') as [VATAmount]
      ,[ETRID]
  FROM [MTR].[dbo].[Sales]
  where [ETRID] = 'e7161445-ff93-491e-89de-2a687396e5b3'
  order by [SaleDate] desc
  

  
  SELECT 
	Format(sum(SaleAmount), 'N')as TotalSales,
	Format(sum(VatAmount),'N') as VATDue
  FROM [MTR].[dbo].[Sales] 
  where [ETRID] = 'e7161445-ff93-491e-89de-2a687396e5b3'