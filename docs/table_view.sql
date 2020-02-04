

--店铺
if exists (SELECT * FROM sysobjects WHERE name = 'TShop')
 drop table TShop
go
create table TShop
( 
  ID bigint identity(1,1) not null primary key,
  Name NVARCHAR(100) NOT NULL,				--名称
  FullName NVARCHAR(200) NOT NULL,			--全称
  Address NVARCHAR(500) NOT NULL,			--店铺地址
  SecurityCode NVARCHAR(200) NOT NULL,		--店铺安全码
  FailLoginTime INT NOT NULL,				--错误10次禁用
  Status INT NOT NULL,						--状态0：新建，10：可用，20：禁用，30：超过最大错误次数
  CreateTime DATETIME NOT NULL,
  LastLoginTime DATETIME NOT NULL,			--最后登录时间
)
go

--订单
if exists (SELECT * FROM sysobjects WHERE name = 'TOrder')
 drop table TOrder
go
create table TOrder
( 
  ID bigint identity(1,1) not null primary key,
  Code NVARCHAR(100) NOT NULL,				--订单号
  Name NVARCHAR(20) NOT NULL,				--姓名
  IDCardNumber NVARCHAR(50) NOT NULL,		--身份证
  ShopId BIGINT NOT NULL,					--店铺ID
  MaterialId BIGINT NOT NULL,				--物资ID
  Num INT NOT NULL,							--数量
  Status INT NOT NULL,						--状态，0预定，10发放
  IP NVARCHAR(20) NOT NULL,					--IP
  CreateTime DATETIME NOT NULL,
)
go											 
create index idxTOrder_Code ON TOrder(Code)	
create index idxTOrder_Name on TOrder(Name)				   
create index idxTOrder_IDCardNumber on TOrder(IDCardNumber)
create index idxTOrder_IDCardNumber_CreateTime on TOrder(IDCardNumber, CreateTime)
go	

--物料配置
if exists (SELECT * FROM sysobjects WHERE name = 'TMaterial')
 drop table TMaterial
go
create table TMaterial
( 
  ID bigint identity(1,1) not null primary key,
  Name NVARCHAR(100) NOT NULL,				--名称
  FullName NVARCHAR(200) NOT NULL,			--全称
  Price DECIMAL(10,2) NOT NULL,				--单价
  MaxOrderNum INT NOT NULL,					--每人每天限购数量
  IPMaxOrderNum INT NOT NULL,				--每个IP每天限购数量
  TotalCount INT NOT NULL,					--总数量
  ShopId BIGINT NOT NULL,					--店铺码
  CreateTime DATETIME NOT NULL,
)
go
								
						
--物资发放情况视图
if exists (select * from sysobjects where name = 'VMaterialRelease')
 drop view VMaterialRelease
 go
 --创建视图
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
--('20200204121212_9999_9999', '张三', '431128199309013333', 1, 1, 3, 0, '0.0.0.0', GETDATE()),
--('20200204121212_9999_9996', '张三1', '431128199309012333', 1, 1, 3, 0, '0.0.0.0', GETDATE()),
--('20200204121212_9999_8888', '李四', '431128199309014444', 1, 1, 3, 10, '0.0.0.0', GETDATE())
																			 