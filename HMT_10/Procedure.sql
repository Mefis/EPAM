/*13 Разработка функций и процедур*/

/*13.1 Написать процедуру, которая возвращает самый крупный заказ для каждого из продавцов за определенный год. 
В результатах не может быть несколько заказов одного продавца, должен быть только один и самый крупный. 
В результатах запроса должны быть выведены следующие колонки: 
колонка с именем и фамилией продавца (FirstName и LastName – пример: Nancy Davolio), номер заказа и его стоимость.
В запросе надо учитывать Discount при продаже товаров. Процедуре передается год, за который надо сделать отчет, и количество возвращаемых записей. 
Результаты запроса должны быть упорядочены по убыванию суммы заказа. 
Процедура должна быть реализована с использованием оператора SELECT и БЕЗ ИСПОЛЬЗОВАНИЯ КУРСОРОВ. 
Название функции соответственно GreatestOrders. Необходимо продемонстрировать использование этих процедур. 
Также  помимо  демонстрации вызовов процедур в скрипте Query.sql надо написать отдельный ДОПОЛНИТЕЛЬНЫЙ проверочный запрос 
для тестирования правильности  работы  процедуры  GreatestOrders.  
Проверочный запрос должен выводить в удобном для сравнения с результатами работы процедур виде для определенного продавца 
для всех его заказов за определенный указанный год в результатах следующие колонки: имя продавца, номер заказа, сумму заказа. 
Проверочный запрос не должен повторять запрос, написанный в процедуре, - он должен выполнять только то, что описано в требованиях по нему.
ВСЕ ЗАПРОСЫ ПО ВЫЗОВУ ПРОЦЕДУР ДОЛЖНЫ БЫТЬ НАПИСАНЫ В ФАЙЛЕ Query.sql – см. пояснение ниже в разделе «Требования к оформлению».*/

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

/*13.2 Написать процедуру, которая возвращает заказы в таблице Orders, согласно указанному сроку доставки в днях 
(разница между OrderDate и ShippedDate). В результатах должны быть возвращены заказы, 
срок которых превышает переданное значение или еще недоставленные заказы.  
Значению  по умолчанию для передаваемого срока 35 дней. Название процедуры ShippedOrdersDiff. 
Процедура должна высвечивать следующие колонки: OrderID, OrderDate, ShippedDate, 
ShippedDelay (разность в днях между ShippedDate и OrderDate), SpecifiedDelay (переданное в процедуру значение).  
Необходимо продемонстрировать использование этой процедуры.*/

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

/*13.3 Написать процедуру, которая высвечивает всех подчиненных заданного продавца, как непосредственных, так и подчиненных его подчиненных.   
В качестве входного параметра функции используется EmployeeID. Необходимо распечатать имена подчиненных и выровнять их в тексте 
(использовать оператор PRINT) согласно иерархии подчинения. Продавец, для которого надо найти подчиненных также должен быть высвечен. 
Название процедуры SubordinationInfo. В качестве алгоритма для решения этой задачи надо использовать пример, 
приведенный в Books Online и рекомендованный Microsoft для решения подобного типа задач. Продемонстрировать использование процедуры.*/

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

/*13.4 Написать функцию, которая определяет, есть ли у продавца подчиненные. Возвращает тип данных BIT. 
В качестве входного параметра функции используется EmployeeID. Название функции IsBoss. 
Продемонстрировать использование функции для всех продавцов из таблицы Employees.*/

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