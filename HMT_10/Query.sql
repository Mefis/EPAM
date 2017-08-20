/*1 ������ � ������ ������ Date, NULL ����������, ����������� ������.
����������� ������������ �������� � ����������� �������  �  ����������� �� ���������� �������������� �������� ���������� �������. 
�������� � ����������� ������� ������ ������������ �������.*/

/*1.1 ������� � ������� Orders ������, ������� ���� ���������� ����� 6 ��� 1998 ���� (������� ShippedDate) ������������ � ������� ���������� � ShipVia >= 2. 
������ �������� ���� ������ ���� ������ ��� ����� ������������ ����������, �������� ����������� ������ 
�Writing International Transact-SQL Statements� � Books Online ������ �Accessing and Changing Relational Data Overview�.
���� ����� ������������ �����  ���  ����  �������. ������ ������ ����������� ������ ������� OrderID, ShippedDate � ShipVia. 
��������  ������  ����  ��  ������  ������  � NULL-��  �  ������� ShippedDate.*/

select OrderID, ShippedDate, ShipVia
from dbo.Orders
where ShippedDate >= convert(datetime, '19980506', 101)
and ShipVia >= 2;

/*������  � NULL-��  �  ������� ShippedDate �� ������������� ������� ������.*/

/*1.2 �������� ������, ������� ������� ������ �������������� ������ �� ������� Orders.  
�  �����������  �������  �����������  ��� ������� ShippedDate ������ �������� NULL ������ �Not Shipped� � ������������ ��������� ������� CAS�.
������ ������ ����������� ������ ������� OrderID � ShippedDate.*/

select OrderID,
case when ShippedDate is null then 'Not shipped' end ShippedDate 
from dbo.Orders
where ShippedDate is null;

/*1.3 ������� � ������� Orders ������, ������� ���� ���������� ����� 6 ��� 1998 ���� (ShippedDate) �� ������� ��� ���� ��� ������� ��� �� ����������.
� ������� ������ ������������� ������ ������� OrderID (������������� � Order Number) � ShippedDate (������������� � Shipped Date).
� �����������  ������� ����������� ��� ������� ShippedDate ������ �������� NULL ������ �Not Shipped�, 
��� ��������� �������� ����������� ���� � ������� �� ���������.*/

select OrderID as 'Order Number',
case when ShippedDate is null then 'Not shipped' else convert(nvarchar, ShippedDate) end "Shipped Date"
from dbo.Orders
where ShippedDate > convert(datetime, '19980506', 101)
or ShippedDate is null;

/*2 ������������� ���������� IN, DISTINCT, ORDER BY, NOT*/

/*2.1 ������� �� ������� Customers ���� ����������, ����������� � USA � Canada. 
������ ������� � ������ ������� ��������� IN. ����������� ������� � ������ ������������ � ��������� ������ � ����������� �������. 
����������� ���������� ������� �� ����� ���������� � �� ����� ����������.*/

select ContactName, Country
from dbo.Customers
where Country in ('USA','Canada')
order by ContactName, Country;

/*2.2 ������� �� ������� Customers ���� ����������, �� ����������� � USA � Canada. 
������ ������� � ������� ��������� IN. ����������� ������� � ������ ������������ � ��������� ������ � ����������� �������. 
����������� ���������� ������� �� ����� ����������.*/

select ContactName, Country
from dbo.Customers
where Country not in ('USA','Canada')
order by ContactName;

/*2.3 ������� �� ������� Customers ��� ������, � ������� ��������� ���������. 
������ ������ ���� ��������� ������ ���� ��� � ������ ������������ �� ��������. 
�� ������������ ����������� GROUP BY. ����������� ������ ���� ������� � ����������� �������.*/

select distinct Country
from dbo.Customers
order by Country desc;

/*3 ������������� ��������� BETWEEN, DISTINCT*/

/*3.1 ������� ��� ������ (OrderID) �� ������� Order Details (������ �� ������ �����������), 
��� ����������� �������� � ����������� �� 3 �� 10 ������������ � ���  ������� Quantity � ������� Order Details.  
������������  �������� BETWEEN. ������  ������ ����������� ������ ������� OrderID.*/

select distinct OrderID
from dbo.[Order Details]
where Quantity between 3 and 10;

/*3.2 ������� ���� ���������� �� ������� Customers, � ������� �������� ������ ���������� �� ����� �� ��������� b � g. 
������������ �������� BETWEEN. ���������, ��� � ���������� ������� �������� Germany. 
������ ������ ����������� ������ ������� CustomerID � Country � ������������ �� Country.*/

select CustomerID, Country
from dbo.Customers
where left(Country, 1) between 'b' and 'g'
order by Country;

/*3.3 ������� ���� ���������� �� ������� Customers, 
� ������� �������� ������ ���������� �� ����� �� ��������� b � g, �� ��������� �������� BETWEEN. 
� ������� ����� �Execution Plan� ���������� ����� ������ ���������������� 
3.2 ��� 3.3 � ��� ����� ���� ������ � ������ ���������� ���������� Execution Plan - a ��� ���� ���� ��������, ���������� ���������� 
Execution Plan ���� ������ � ������ � ���� ����������� � �� �� ����������� ���� ����� �� ������ � �� ������ ��������� ���� ��������� ���������. 
������ ������ ����������� ������ ������� CustomerID � Country � ������������ �� Country.*/

select CustomerID, Country
from dbo.Customers
where 'b' <= left(Country, 1) and left(Country, 1) <= 'g'
order by Country;

/*� ������� ����� �Execution Plan� ��������� ��������� ���� �������� - �� ���� ���������� ��� ������� ���������� ���������� ����������.*/

/*4 ������������� ��������� LIKE*/

/*4.1 � ������� Products ����� ��� �������� (������� ProductName), ��� ����������� ��������� 'chocolade'. 
��������, ��� � ��������� 'chocolade' ����� ���� �������� ���� ����� 'c' � �������� - ����� ��� ��������, ������� ������������� ����� �������. 
���������: ���������� ������� ������ ����������� 2 ������.*/

select *
from dbo.Products
where ProductName like 'cho_olade';

/*5 ������������� ���������� ������� (SUM, COUNT)*/

/*5.1 ����� ����� ����� ���� ������� �� ������� Order Details � ������ ���������� ����������� ������� � ������ �� ���. 
��������� ��������� �� ����� � ��������� � ����� 1 ��� ���� ������ money. 
������ (������� Discount) ���������� ������� �� ��������� ��� ������� ������.  
��� ����������� �������������� ���� �� ��������� ������� ���� ������� ������ �� ��������� � ������� UnitPrice ����. 
����������� ������� ������ ���� ���� ������ � ����� �������� � ��������� ������� 'Totals'.*/

select convert(nvarchar, cast(round(sum(Quantity * (UnitPrice - UnitPrice*Discount)), 2) as money), 1) as 'Totals'
from dbo.[Order Details];

/*5.2 �� ������� Orders ����� ���������� �������, ������� ��� �� ���� ���������� (�.�. � ������� ShippedDate ��� �������� ���� ��������).  
������������ ��� ���� ������� ������ �������� COUNT. �� ������������ ����������� WHERE � GROUP.*/

select count(*) - count(ShippedDate)
from dbo.Orders

/*5.3 �� ������� Orders ����� ���������� ��������� ����������� (CustomerID), ��������� ������. 
������������ ������� COUNT � �� ������������ ����������� WHERE � GROUP.*/

select count(distinct CustomerID)
from dbo.Orders

/*6 ����� ���������� ������, ��������������, ������������� ���������� ������� � ����������� GROUP BY � HAVING*/

/*6.1 �� ������� Orders ����� ���������� ������� � ������������ �� �����.  
� ����������� ������� ���� ����������� ��� ������� c ���������� Year � Total. 
�������� ����������� ������, ������� ��������� ���������� ���� �������.*/

select year(OrderDate) as 'Year', count(OrderDate) as 'Total'
from dbo.Orders
group by year(OrderDate);

/*����������� ������, ������� ��������� ���������� ���� �������.*/
select count(OrderDate)
from dbo.Orders

/*6.2 �� ������� Orders ����� ���������� �������, c�������� ������ ���������. 
����� ��� ���������� �������� � ��� ����� ������ � ������� Orders, ��� � ������� EmployeeID ������ �������� ��� ������� ��������.  
� ����������� ������� ���� ����������� ������� � ������ �������� (������ ������������� ��� ���������� ������������� LastName & FirstName. 
��� ������ LastName & FirstName ������ ���� �������� ��������� �������� � ������� ��������� �������. 
����� �������� ������ ������ ������������ ����������� �� EmployeeID.) � ��������� ������� �Seller� � ������� c ����������� ������� ����������� � ��������� 'Amount'.  
����������  �������  ������  ����  �����������  �� �������� ���������� �������. */

select 
Seller = (select LastName + ' ' + FirstName from dbo.Employees as e where o.EmployeeID = e.EmployeeID), 
count(*) as 'Amount'
from dbo.Orders as o
group by EmployeeID
order by count(*) desc;

/*6.3 �� ������� Orders ����� ���������� �������, c�������� ������ ��������� � ��� ������� ����������. 
���������� ���������� ��� ������ ��� ������� ��������� � 1998 ����. 
� ����������� ������� ���� ����������� ������� � ������ �������� (�������� ������� �Seller�), 
������� � ������ ���������� (��������  ������� �Customer�) � ������� c ����������� ������� ����������� � ��������� 'Amount'.
� ������� ���������� ������������ ����������� �������� ����� T-SQL ��� ������ � ���������� GROUP 
(���� �� �������� ������� �������� ������ �ALL� � ����������� �������). 
����������� ������ ���� ������� �� ID �������� � ����������. 
���������� ������� ������ ���� ����������� �� ��������, ���������� � �� �������� ���������� ������. 
�  �����������  ������  ����  �������  ����������  �� ��������. 
�.�. � ������������� ������ ������ �������������� ������������� � ���������� � �������� �������� ��� ������� ���������� ��������� �������:
Seller	Customer	Amount
ALL		ALL			<����� ����� ������>
<���>	ALL			<����� ������ ��� ������� ��������>
ALL		<���>		<����� ������ ��� ������� ����������>
<���>	<���>		<����� ������ ������� �������� ��� ������� ����������>*/

select 
case when grouping(EmployeeID) = 1 then 'All' else
	(select LastName + ' ' + FirstName from dbo.Employees as e where o.EmployeeID = e.EmployeeID) end Seller,
case when grouping(CustomerID) = 1 then 'All' else
	(select ContactName from dbo.Customers as c where o.CustomerID = c.CustomerID) end Customer,
count(*) as 'Amount'
from dbo.Orders as o
where year(o.OrderDate) = 1998
group by cube (EmployeeID, CustomerID)
order by EmployeeID, CustomerID, 'Amount' desc;

/*6.4 ����� ����������� � ���������, ������� ����� � ����� ������. 
���� � ������ ����� ������ ���� ��� ��������� ��������� ��� ������ ���� ��� ��������� �����������, 
�� ���������� � ����� ���������� � ��������� �� ������ �������� � �������������� �����. �� ������������ ����������� JOIN. 
� ����������� ������� ���������� ������� ��������� ��������� ��� ����������� �������: �Person�, �Type� 
(����� ���� �������� ������ �Customer� ��� �Seller� � ��������� �� ���� ������), �City�. ������������� ���������� ������� �� ������� �City� � �� �Person�.*/

select * 
from (select Person = LastName + ' ' + FirstName, "Type" = 'Seller', e.City
	  from dbo.Employees as e, dbo.Customers as c
	  where e.City = c.City
	  union
	  select Person = ContactName, "Type" = 'Customer', c.City
	  from dbo.Customers as c, dbo.Employees as e
	  where c.City = e.City) as UnitedTable
order by City, Person;

/*6.5 ����� ���� �����������, ������� ����� � ����� ������. � ������� ������������ ���������� ������� Customers c ����� - ��������������. 
��������� ������� CustomerID � City. ������ �� ������ ����������� ����������� ������. 
��� �������� �������� ������, ������� ����������� ������, ������� ����������� ����� ������ ���� � ������� Customers. 
��� �������� ��������� ������������ �������.*/

select c1.CustomerID , c2.CustomerID, c1.City
from dbo.Customers as c1 
join dbo.Customers as c2 
	on c1.CustomerID > c2.CustomerID AND c1.City = c2.City;

/*����������� ������, ������������� ������, ������� ����������� ����� ������ ���� � ������� Customers.*/
select City, count(City) as 'Amount'
from dbo.Customers
group by City
having count(City) > 1;

/*6.6 �� ������� Employees ����� ��� ������� �������� ��� ������������, �.�. ���� �� ������ �������. 
��������� ������� � ������� 'UserName' (LastName) � 'Boss'. � �������� ������ ���� ��������� ����� �� ������� LastName. 
��������� �� ��� �������� � ���� �������?*/

select e1.LastName as UserName, e2.LastName as Boss
from dbo.Employees as e1 
join dbo.Employees as e2 
	on e1.ReportsTo = e2.EmployeeID;

/*� ������� �� ���� ��������� ��������, ������� �� ����� �����������.*/

/*7 ������������� Inner JOIN*/

/*7.1 ���������� ���������, ������� ����������� ������ 'Western' (������� Region). 
���������� ������� ������ ����������� ��� ����: 'LastName' �������� � �������� ������������� ���������� 
('TerritoryDescription' �� ������� Territories). ������ ������ ������������ JOIN � ����������� FROM. 
��� ����������� ������ ����� ��������� Employees � Territories ���� ������������ ����������� ��������� ��� ���� Northwind.*/

select e.LastName, t.TerritoryDescription
from dbo.Employees as e
join dbo.EmployeeTerritories as et
	on et.EmployeeID = e.EmployeeID
join dbo.Territories as t
	on t.TerritoryID = et.TerritoryID
join dbo.Region as r
	on r.RegionID = t.RegionID
where r.RegionDescription = 'Western';

/*8 ������������� Outer JOIN*/

/*8.1 ��������� � ����������� ������� ����� ���� ���������� �� ������� Customers � ��������� ���������� �� ������� �� ������� Orders. 
������� �� ��������, ��� � ��������� ���������� ��� �������, �� ��� ����� ������ ���� �������� � ����������� �������.  
����������� ���������� ������� �� ����������� ���������� �������.*/

select c.ContactName, count(o.CustomerID) as 'Orders'
from dbo.Customers as c 
left outer join dbo.Orders as o
	on c.CustomerID = o.CustomerID
group by c.ContactName;

/*9 ������������� �����������*/

/*9.1 ��������� ���� ����������� ������� CompanyName � ������� Suppliers, � ������� ��� ���� �� ������ �������� �� ������ 
(UnitsInStock � ������� Products ����� 0). ������������ ��������� SELECT ��� ����� ������� � �������������� ��������� IN.  
����� �� ������������ ������ ��������� IN �������� '=' ?*/

select s.CompanyName
from dbo.Suppliers as s
where s.SupplierID in
	(select p.SupplierID
	 from dbo.Products as p
	 where p.UnitsInStock = 0);

/*10 ��������������� ������*/

/*10.1 ��������� ���� ���������, ������� ����� ����� 150 �������. 
������������ ��������� ��������������� SELECT.*/

select LastName + ' ' + FirstName as 'Sellers with over 150 orders.'
from dbo.Employees as e
where 150 < (select count(EmployeeID)
			 from dbo.Orders as o
			 where o.EmployeeID = e.EmployeeID);

/*11 ������������� EXISTS*/

/*11.1 ��������� ���� ���������� (������� Customers), ������� �� ����� �� ������ ������ (��������� �� ������� Orders).
������������ ��������������� SELECT � �������� EXISTS.*/

select ContactName as "Don`t have orders"
from dbo.Customers as c
where not exists (select o.CustomerID 
				  from dbo.Orders as o 
				  where o.CustomerID = c.CustomerID);

/*12 ������������� ��������� �������*/

/*12.1 ��� ������������ ����������� ��������� Employees ��������� �� ������� Employees ������ ������ ��� ���� ��������, 
� ������� ���������� ������� Employees (������� LastName ) �� ���� �������. ���������� ������ ������ ���� ������������ �� �����������.*/

select distinct left(LastName, 1) as Letters
from Employees
order by Letters;

/*13 ���������� ������� � ��������*/

/*13.1*/
exec dbo.GreatestOrders 1996;

/*����������� ������, ������������ ������ ��������� �������� �� �������� ���.*/
select FirstName + ' ' + LastName as Seller, o.OrderID, od.UnitPrice - od.UnitPrice * od.Discount as Price
from dbo.Employees as e
join dbo.Orders as o
	on o.EmployeeID = e.EmployeeID
join dbo.[Order Details] as od
	on od.OrderID = o.OrderID
where year(o.OrderDate) = 1996
and FirstName + ' ' + LastName like '%Margaret%'
order by Price desc

/*13.2*/

/*������ ��������� � ���������� ��-���������.*/
exec dbo.ShippedOrdersDiff;

/*������ ��������� � �������� ����������.*/
exec dbo.ShippedOrdersDiff 20;

/*13.3*/

exec SubordinationInfo 2;

/*13.4*/
declare @EmployeeID int;
set @EmployeeID = 1;
declare @IsBossValue bit;
exec dbo.IsBoss @EmployeeID, @IsBossValue output;
print 'Employee with ID ' + convert(nvarchar, @EmployeeID) + case when @IsBossValue = 1 then ' is' else ' isn`t' end + ' a boss.';