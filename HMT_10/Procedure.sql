/*13 ���������� ������� � ��������*/

/*13.1 �������� ���������, ������� ���������� ����� ������� ����� ��� ������� �� ��������� �� ������������ ���. 
� ����������� �� ����� ���� ��������� ������� ������ ��������, ������ ���� ������ ���� � ����� �������. 
� ����������� ������� ������ ���� �������� ��������� �������: 
������� � ������ � �������� �������� (FirstName � LastName � ������: Nancy Davolio), ����� ������ � ��� ���������.
� ������� ���� ��������� Discount ��� ������� �������. ��������� ���������� ���, �� ������� ���� ������� �����, � ���������� ������������ �������. 
���������� ������� ������ ���� ����������� �� �������� ����� ������. 
��������� ������ ���� ����������� � �������������� ��������� SELECT � ��� ������������� ��������. 
�������� ������� �������������� GreatestOrders. ���������� ������������������ ������������� ���� ��������. 
�����  ������  ������������ ������� �������� � ������� Query.sql ���� �������� ��������� �������������� ����������� ������ 
��� ������������ ������������  ������  ���������  GreatestOrders.  
����������� ������ ������ �������� � ������� ��� ��������� � ������������ ������ �������� ���� ��� ������������� �������� 
��� ���� ��� ������� �� ������������ ��������� ��� � ����������� ��������� �������: ��� ��������, ����� ������, ����� ������. 
����������� ������ �� ������ ��������� ������, ���������� � ���������, - �� ������ ��������� ������ ��, ��� ������� � ����������� �� ����.
��� ������� �� ������ �������� ������ ���� �������� � ����� Query.sql � ��. ��������� ���� � ������� ����������� � �����������.*/

if exists (select type_desc, type
		   from sys.procedures with(nolock)
           where NAME = 'GreatestOrders'
           and type = 'P')
	drop procedure dbo.GreatestOrders
go

create proc GreatestOrders @Year int   
as    
with MainTable as (select FirstName + ' ' + LastName as Seller, o.OrderID, od.UnitPrice - od.UnitPrice * od.Discount as Price
				   from dbo.Employees as e
				   join dbo.Orders as o
					   on o.EmployeeID = e.EmployeeID
				   join dbo.[Order Details] as od
				   on od.OrderID = o.OrderID
				   where year(o.OrderDate) = @Year)
select mt.Seller, max(mt.OrderID) as OrderID, max(mt.Price) as "Max Price"
from MainTable as mt
inner join (select Seller, max(Price) as MaxPrice
		    from MainTable as mt
			group by Seller) as st
	on mt.Seller = st.Seller
	and mt.Price = st.MaxPrice
group by mt.Seller
order by max(mt.Price) desc;
go

/*13.2 �������� ���������, ������� ���������� ������ � ������� Orders, �������� ���������� ����� �������� � ���� 
(������� ����� OrderDate � ShippedDate). � ����������� ������ ���� ���������� ������, 
���� ������� ��������� ���������� �������� ��� ��� �������������� ������.  
��������  �� ��������� ��� ������������� ����� 35 ����. �������� ��������� ShippedOrdersDiff. 
��������� ������ ����������� ��������� �������: OrderID, OrderDate, ShippedDate, 
ShippedDelay (�������� � ���� ����� ShippedDate � OrderDate), SpecifiedDelay (���������� � ��������� ��������).  
���������� ������������������ ������������� ���� ���������.*/

if exists (select type_desc, type
		   from sys.procedures with(nolock)
           where NAME = 'ShippedOrdersDiff'
           and type = 'P')
	drop procedure dbo.ShippedOrdersDiff
go

create proc ShippedOrdersDiff @SpecifiedDelay int = 35
as
select * 
from (select OrderID, OrderDate, 
	  case when ShippedDate is null then 'Not delivered' else convert(nvarchar, ShippedDate, 120) end "ShippedDate", 
	  case when ShippedDate is null then datediff(day, OrderDate, getdate()) else datediff(day, OrderDate, ShippedDate) end "ShippedDelay",
	  SpecifiedDelay = @SpecifiedDelay
	  from dbo.Orders) as m
where ShippedDelay > @SpecifiedDelay;
go

/*13.3 �������� ���������, ������� ����������� ���� ����������� ��������� ��������, ��� ����������������, ��� � ����������� ��� �����������.   
� �������� �������� ��������� ������� ������������ EmployeeID. ���������� ����������� ����� ����������� � ��������� �� � ������ 
(������������ �������� PRINT) �������� �������� ����������. ��������, ��� �������� ���� ����� ����������� ����� ������ ���� ��������. 
�������� ��������� SubordinationInfo. � �������� ��������� ��� ������� ���� ������ ���� ������������ ������, 
����������� � Books Online � ��������������� Microsoft ��� ������� ��������� ���� �����. ������������������ ������������� ���������.*/

if exists (select type_desc, type
		   from sys.procedures with(nolock)
           where NAME = 'SubordinationInfo'
           and type = 'P')
	drop procedure dbo.SubordinationInfo
go

create proc SubordinationInfo @EmployeeID int
as
declare @List nvarchar(255);
set @List = '';
with items as (
    select EmployeeID, LastName, 0 as Level, cast(EmployeeID as nvarchar(255)) as Path
    from Employees 
    where EmployeeID = @EmployeeID

    union all

    select i.EmployeeID, i.LastName, Level + 1, cast(Path + '.' + cast(i.EmployeeID as nvarchar(255)) as nvarchar(255))
    from Employees i
    inner join items itms on itms.EmployeeID = i.ReportsTo
)
select @List = @List + replicate(' ', 4 * items.Level) + items.LastName + char(13) + char(10) from items order by Path
print @List
go

/*13.4 �������� �������, ������� ����������, ���� �� � �������� �����������. ���������� ��� ������ BIT. 
� �������� �������� ��������� ������� ������������ EmployeeID. �������� ������� IsBoss. 
������������������ ������������� ������� ��� ���� ��������� �� ������� Employees.*/

if exists (select type_desc, type
		   from sys.procedures with(nolock)
           where NAME = 'IsBoss'
           and type = 'P')
	drop procedure dbo.IsBoss
go

create proc IsBoss @EmployeeID int, @IsBossValue bit output
as
select @IsBossValue = case when count(*) > 0 then 1 else 0 end
from dbo.Employees
where ReportsTo = @EmployeeID
return
go