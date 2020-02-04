

--����
if exists (SELECT * FROM sysobjects WHERE name = 'TShop')
 drop table TShop
go
create table TShop
( 
  ID bigint identity(1,1) not null primary key,
  Name NVARCHAR(100) NOT NULL,				--����
  FullName NVARCHAR(200) NOT NULL,			--ȫ��
  Address NVARCHAR(500) NOT NULL,			--���̵�ַ
  SecurityCode NVARCHAR(200) NOT NULL,		--���̰�ȫ��
  FailLoginTime INT NOT NULL,				--����10�ν���
  Status INT NOT NULL,						--״̬0���½���10�����ã�20�����ã�30���������������
  CreateTime DATETIME NOT NULL,
  LastLoginTime DATETIME NOT NULL,			--����¼ʱ��
)
go

--����
if exists (SELECT * FROM sysobjects WHERE name = 'TOrder')
 drop table TOrder
go
create table TOrder
( 
  ID bigint identity(1,1) not null primary key,
  Code NVARCHAR(100) NOT NULL,				--������
  Name NVARCHAR(20) NOT NULL,				--����
  IDCardNumber NVARCHAR(50) NOT NULL,		--���֤
  ShopId BIGINT NOT NULL,					--����ID
  MaterialId BIGINT NOT NULL,				--����ID
  Num INT NOT NULL,							--����
  Status INT NOT NULL,						--״̬��0Ԥ����10����
  IP NVARCHAR(20) NOT NULL,					--IP
  CreateTime DATETIME NOT NULL,
)
go											 
create index idxTOrder_Code ON TOrder(Code)	
create index idxTOrder_Name on TOrder(Name)				   
create index idxTOrder_IDCardNumber on TOrder(IDCardNumber)
create index idxTOrder_IDCardNumber_CreateTime on TOrder(IDCardNumber, CreateTime)
go	

--��������
if exists (SELECT * FROM sysobjects WHERE name = 'TMaterial')
 drop table TMaterial
go
create table TMaterial
( 
  ID bigint identity(1,1) not null primary key,
  Name NVARCHAR(100) NOT NULL,				--����
  FullName NVARCHAR(200) NOT NULL,			--ȫ��
  Price DECIMAL(10,2) NOT NULL,				--����
  MaxOrderNum INT NOT NULL,					--ÿ��ÿ���޹�����
  IPMaxOrderNum INT NOT NULL,				--ÿ��IPÿ���޹�����
  TotalCount INT NOT NULL,					--������
  ShopId BIGINT NOT NULL,					--������
  CreateTime DATETIME NOT NULL,
)
go
								
						
--���ʷ��������ͼ
if exists (select * from sysobjects where name = 'VMaterialRelease')
 drop view VMaterialRelease
 go
 --������ͼ
create view VMaterialRelease AS

SELECT 
	m.*
	, ISNULL(o.PreCount, 0)	PreCount
	, ISNULL(o.ReleaseCount, 0)	ReleaseCount
FROM dbo.TMaterial m
LEFT JOIN (
	SELECT o.MaterialId, SUM(o.PreCount) PreCount, SUM(o.ReleaseCount)	ReleaseCount
	FROM (
		SELECT 
			o.MaterialId,
			PreCount = CASE WHEN o.Status = 0 THEN o.Num ELSE 0 END,
			ReleaseCount = CASE WHEN o.Status = 10 THEN o.Num ELSE 0 END
		FROM dbo.TOrder o
	) o GROUP BY o.MaterialId
) o ON m.ID = o.MaterialId

GO






SELECT * FROM dbo.TOrder
TRUNCATE TABLE 	  TOrder
--INSERT INTO dbo.TOrder VALUES  
--('20200204121212_9999_9999', '����', '431128199309013333', 1, 1, 3, 0, '0.0.0.0', GETDATE()),
--('20200204121212_9999_9996', '����1', '431128199309012333', 1, 1, 3, 0, '0.0.0.0', GETDATE()),
--('20200204121212_9999_8888', '����', '431128199309014444', 1, 1, 3, 10, '0.0.0.0', GETDATE())
																			 