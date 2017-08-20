/*1 Работа с типами данных Date, NULL значениями, трехзначная логика.
Возвращение определенных значений в результатах запроса  в  зависимости от полученных первоначальных значений результата запроса. 
Высветка в результатах запроса только определенных колонок.*/

/*1.1 Выбрать в таблице Orders заказы, которые были доставлены после 6 мая 1998 года (колонка ShippedDate) включительно и которые доставлены с ShipVia >= 2. 
Формат указания даты должен быть верным при любых региональных настройках, согласно требованиям статьи 
“Writing International Transact-SQL Statements” в Books Online раздел “Accessing and Changing Relational Data Overview”.
Этот метод использовать далее  для  всех  заданий. Запрос должен высвечивать только колонки OrderID, ShippedDate и ShipVia. 
Пояснить  почему  сюда  не  попали  заказы  с NULL-ом  в  колонке ShippedDate.*/

select OrderID, ShippedDate, ShipVia
from dbo.Orders
where ShippedDate >= convert(datetime, '19980506', 101)
and ShipVia >= 2;

/*Заказы  с NULL-ом  в  колонке ShippedDate не удовлетворяют условию поиска.*/

/*1.2 Написать запрос, который выводит только недоставленные заказы из таблицы Orders.  
В  результатах  запроса  высвечивать  для колонки ShippedDate вместо значений NULL строку ‘Not Shipped’ – использовать системную функцию CASЕ.
Запрос должен высвечивать только колонки OrderID и ShippedDate.*/

select OrderID,
case when ShippedDate is null then 'Not shipped' end ShippedDate 
from dbo.Orders
where ShippedDate is null;

/*1.3 Выбрать в таблице Orders заказы, которые были доставлены после 6 мая 1998 года (ShippedDate) не включая эту дату или которые еще не доставлены.
В запросе должны высвечиваться только колонки OrderID (переименовать в Order Number) и ShippedDate (переименовать в Shipped Date).
В результатах  запроса высвечивать для колонки ShippedDate вместо значений NULL строку ‘Not Shipped’, 
для остальных значений высвечивать дату в формате по умолчанию.*/

select OrderID as 'Order Number',
case when ShippedDate is null then 'Not shipped' else convert(nvarchar, ShippedDate) end "Shipped Date"
from dbo.Orders
where ShippedDate > convert(datetime, '19980506', 101)
or ShippedDate is null;

/*2 Использование операторов IN, DISTINCT, ORDER BY, NOT*/

/*2.1 Выбрать из таблицы Customers всех заказчиков, проживающих в USA и Canada. 
Запрос сделать с только помощью оператора IN. Высвечивать колонки с именем пользователя и названием страны в результатах запроса. 
Упорядочить результаты запроса по имени заказчиков и по месту проживания.*/

select ContactName, Country
from dbo.Customers
where Country in ('USA','Canada')
order by ContactName, Country;

/*2.2 Выбрать из таблицы Customers всех заказчиков, не проживающих в USA и Canada. 
Запрос сделать с помощью оператора IN. Высвечивать колонки с именем пользователя и названием страны в результатах запроса. 
Упорядочить результаты запроса по имени заказчиков.*/

select ContactName, Country
from dbo.Customers
where Country not in ('USA','Canada')
order by ContactName;

/*2.3 Выбрать из таблицы Customers все страны, в которых проживают заказчики. 
Страна должна быть упомянута только один раз и список отсортирован по убыванию. 
Не использовать предложение GROUP BY. Высвечивать только одну колонку в результатах запроса.*/

select distinct Country
from dbo.Customers
order by Country desc;

/*3 Использование оператора BETWEEN, DISTINCT*/

/*3.1 Выбрать все заказы (OrderID) из таблицы Order Details (заказы не должны повторяться), 
где встречаются продукты с количеством от 3 до 10 включительно – это  колонка Quantity в таблице Order Details.  
Использовать  оператор BETWEEN. Запрос  должен высвечивать только колонку OrderID.*/

select distinct OrderID
from dbo.[Order Details]
where Quantity between 3 and 10;

/*3.2 Выбрать всех заказчиков из таблицы Customers, у которых название страны начинается на буквы из диапазона b и g. 
Использовать оператор BETWEEN. Проверить, что в результаты запроса попадает Germany. 
Запрос должен высвечивать только колонки CustomerID и Country и отсортирован по Country.*/

select CustomerID, Country
from dbo.Customers
where left(Country, 1) between 'b' and 'g'
order by Country;

/*3.3 Выбрать всех заказчиков из таблицы Customers, 
у которых название страны начинается на буквы из диапазона b и g, не используя оператор BETWEEN. 
С помощью опции “Execution Plan” определить какой запрос предпочтительнее 
3.2 или 3.3 – для этого надо ввести в скрипт выполнение текстового Execution Plan - a для двух этих запросов, результаты выполнения 
Execution Plan надо ввести в скрипт в виде комментария и по их результатам дать ответ на вопрос – по какому параметру было проведено сравнение. 
Запрос должен высвечивать только колонки CustomerID и Country и отсортирован по Country.*/

select CustomerID, Country
from dbo.Customers
where 'b' <= left(Country, 1) and left(Country, 1) <= 'g'
order by Country;

/*С помощью опции “Execution Plan” проведено сравнение двух запросов - по всем параметрам оба запроса показывают одинаковые результаты.*/

/*4 Использование оператора LIKE*/

/*4.1 В таблице Products найти все продукты (колонка ProductName), где встречается подстрока 'chocolade'. 
Известно, что в подстроке 'chocolade' может быть изменена одна буква 'c' в середине - найти все продукты, которые удовлетворяют этому условию. 
Подсказка: результаты запроса должны высвечивать 2 строки.*/

select *
from dbo.Products
where ProductName like 'cho_olade';

/*5 Использование агрегатных функций (SUM, COUNT)*/

/*5.1 Найти общую сумму всех заказов из таблицы Order Details с учетом количества закупленных товаров и скидок по ним. 
Результат округлить до сотых и высветить в стиле 1 для типа данных money. 
Скидка (колонка Discount) составляет процент из стоимости для данного товара.  
Для определения действительной цены на проданный продукт надо вычесть скидку из указанной в колонке UnitPrice цены. 
Результатом запроса должна быть одна запись с одной колонкой с названием колонки 'Totals'.*/

select convert(nvarchar, cast(round(sum(Quantity * (UnitPrice - UnitPrice*Discount)), 2) as money), 1) as 'Totals'
from dbo.[Order Details];

/*5.2 По таблице Orders найти количество заказов, которые еще не были доставлены (т.е. в колонке ShippedDate нет значения даты доставки).  
Использовать при этом запросе только оператор COUNT. Не использовать предложения WHERE и GROUP.*/

select count(*) - count(ShippedDate)
from dbo.Orders

/*5.3 По таблице Orders найти количество различных покупателей (CustomerID), сделавших заказы. 
Использовать функцию COUNT и не использовать предложения WHERE и GROUP.*/

select count(distinct CustomerID)
from dbo.Orders

/*6 Явное соединение таблиц, самосоединения, использование агрегатных функций и предложений GROUP BY и HAVING*/

/*6.1 По таблице Orders найти количество заказов с группировкой по годам.  
В результатах запроса надо высвечивать две колонки c названиями Year и Total. 
Написать проверочный запрос, который вычисляет количество всех заказов.*/

select year(OrderDate) as 'Year', count(OrderDate) as 'Total'
from dbo.Orders
group by year(OrderDate);

/*Проверочный запрос, который вычисляет количество всех заказов.*/
select count(OrderDate)
from dbo.Orders

/*6.2 По таблице Orders найти количество заказов, cделанных каждым продавцом. 
Заказ для указанного продавца – это любая запись в таблице Orders, где в колонке EmployeeID задано значение для данного продавца.  
В результатах запроса надо высвечивать колонку с именем продавца (Должно высвечиваться имя полученное конкатенацией LastName & FirstName. 
Эта строка LastName & FirstName должна быть получена отдельным запросом в колонке основного запроса. 
Также основной запрос должен использовать группировку по EmployeeID.) с названием колонки ‘Seller’ и колонку c количеством заказов высвечивать с названием 'Amount'.  
Результаты  запроса  должны  быть  упорядочены  по убыванию количества заказов. */

select 
Seller = (select LastName + ' ' + FirstName from dbo.Employees as e where o.EmployeeID = e.EmployeeID), 
count(*) as 'Amount'
from dbo.Orders as o
group by EmployeeID
order by count(*) desc;

/*6.3 По таблице Orders найти количество заказов, cделанных каждым продавцом и для каждого покупателя. 
Необходимо определить это только для заказов сделанных в 1998 году. 
В результатах запроса надо высвечивать колонку с именем продавца (название колонки ‘Seller’), 
колонку с именем покупателя (название  колонки ‘Customer’) и колонку c количеством заказов высвечивать с названием 'Amount'.
В запросе необходимо использовать специальный оператор языка T-SQL для работы с выражением GROUP 
(Этот же оператор поможет выводить строку “ALL” в результатах запроса). 
Группировки должны быть сделаны по ID продавца и покупателя. 
Результаты запроса должны быть упорядочены по продавцу, покупателю и по убыванию количества продаж. 
В  результатах  должна  быть  сводная  информация  по продажам. 
Т.е. в резульирующем наборе должны присутствовать дополнительно к информации о продажах продавца для каждого покупателя следующие строчки:
Seller	Customer	Amount
ALL		ALL			<общее число продаж>
<имя>	ALL			<число продаж для данного продавца>
ALL		<имя>		<число продаж для данного покупателя>
<имя>	<имя>		<число продаж данного продавца для данного покупателя>*/

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

/*6.4 Найти покупателей и продавцов, которые живут в одном городе. 
Если в городе живут только один или несколько продавцов или только один или несколько покупателей, 
то информация о таких покупателя и продавцах не должна попадать в результирующий набор. Не использовать конструкцию JOIN. 
В результатах запроса необходимо вывести следующие заголовки для результатов запроса: ‘Person’, ‘Type’ 
(здесь надо выводить строку ‘Customer’ или ‘Seller’ в завимости от типа записи), ‘City’. Отсортировать результаты запроса по колонке ‘City’ и по ‘Person’.*/

select * 
from (select Person = LastName + ' ' + FirstName, "Type" = 'Seller', e.City
	  from dbo.Employees as e, dbo.Customers as c
	  where e.City = c.City
	  union
	  select Person = ContactName, "Type" = 'Customer', c.City
	  from dbo.Customers as c, dbo.Employees as e
	  where c.City = e.City) as UnitedTable
order by City, Person;

/*6.5 Найти всех покупателей, которые живут в одном городе. В запросе использовать соединение таблицы Customers c собой - самосоединение. 
Высветить колонки CustomerID и City. Запрос не должен высвечивать дублируемые записи. 
Для проверки написать запрос, который высвечивает города, которые встречаются более одного раза в таблице Customers. 
Это позволит проверить правильность запроса.*/

select c1.CustomerID , c2.CustomerID, c1.City
from dbo.Customers as c1 
join dbo.Customers as c2 
	on c1.CustomerID > c2.CustomerID AND c1.City = c2.City;

/*Проверочный запрос, высвечивающий города, которые встречаются более одного раза в таблице Customers.*/
select City, count(City) as 'Amount'
from dbo.Customers
group by City
having count(City) > 1;

/*6.6 По таблице Employees найти для каждого продавца его руководителя, т.е. кому он делает репорты. 
Высветить колонки с именами 'UserName' (LastName) и 'Boss'. В колонках должны быть высвечены имена из колонки LastName. 
Высвечены ли все продавцы в этом запросе?*/

select e1.LastName as UserName, e2.LastName as Boss
from dbo.Employees as e1 
join dbo.Employees as e2 
	on e1.ReportsTo = e2.EmployeeID;

/*В запросе не были высвечены продавцы, которые не имеют начальников.*/

/*7 Использование Inner JOIN*/

/*7.1 Определить продавцов, которые обслуживают регион 'Western' (таблица Region). 
Результаты запроса должны высвечивать два поля: 'LastName' продавца и название обслуживаемой территории 
('TerritoryDescription' из таблицы Territories). Запрос должен использовать JOIN в предложении FROM. 
Для определения связей между таблицами Employees и Territories надо использовать графические диаграммы для базы Northwind.*/

select e.LastName, t.TerritoryDescription
from dbo.Employees as e
join dbo.EmployeeTerritories as et
	on et.EmployeeID = e.EmployeeID
join dbo.Territories as t
	on t.TerritoryID = et.TerritoryID
join dbo.Region as r
	on r.RegionID = t.RegionID
where r.RegionDescription = 'Western';

/*8 Использование Outer JOIN*/

/*8.1 Высветить в результатах запроса имена всех заказчиков из таблицы Customers и суммарное количество их заказов из таблицы Orders. 
Принять во внимание, что у некоторых заказчиков нет заказов, но они также должны быть выведены в результатах запроса.  
Упорядочить результаты запроса по возрастанию количества заказов.*/

select c.ContactName, count(o.CustomerID) as 'Orders'
from dbo.Customers as c 
left outer join dbo.Orders as o
	on c.CustomerID = o.CustomerID
group by c.ContactName;

/*9 Использование подзапросов*/

/*9.1 Высветить всех поставщиков колонка CompanyName в таблице Suppliers, у которых нет хотя бы одного продукта на складе 
(UnitsInStock в таблице Products равно 0). Использовать вложенный SELECT для этого запроса с использованием оператора IN.  
Можно ли использовать вместо оператора IN оператор '=' ?*/

select s.CompanyName
from dbo.Suppliers as s
where s.SupplierID in
	(select p.SupplierID
	 from dbo.Products as p
	 where p.UnitsInStock = 0);

/*10 Коррелированный запрос*/

/*10.1 Высветить всех продавцов, которые имеют более 150 заказов. 
Использовать вложенный коррелированный SELECT.*/

select LastName + ' ' + FirstName as 'Sellers with over 150 orders.'
from dbo.Employees as e
where 150 < (select count(EmployeeID)
			 from dbo.Orders as o
			 where o.EmployeeID = e.EmployeeID);

/*11 Использование EXISTS*/

/*11.1 Высветить всех заказчиков (таблица Customers), которые не имеют ни одного заказа (подзапрос по таблице Orders).
Использовать коррелированный SELECT и оператор EXISTS.*/

select ContactName as "Don`t have orders"
from dbo.Customers as c
where not exists (select o.CustomerID 
				  from dbo.Orders as o 
				  where o.CustomerID = c.CustomerID);

/*12 Использование строковых функций*/

/*12.1 Для формирования алфавитного указателя Employees высветить из таблицы Employees список только тех букв алфавита, 
с которых начинаются фамилии Employees (колонка LastName ) из этой таблицы. Алфавитный список должен быть отсортирован по возрастанию.*/

select distinct left(LastName, 1) as Letters
from Employees
order by Letters;

/*13 Разработка функций и процедур*/

/*13.1*/
exec dbo.GreatestOrders 1996;

/*Проверочный запрос, показывающий заказы заданного продавца на заданный год.*/
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

/*Запуск процедуры с параметром по-умолчанию.*/
exec dbo.ShippedOrdersDiff;

/*Запуск процедуры с заданным параметром.*/
exec dbo.ShippedOrdersDiff 20;

/*13.3*/

exec SubordinationInfo 2;

/*13.4*/
declare @EmployeeID int;
set @EmployeeID = 1;
declare @IsBossValue bit;
exec dbo.IsBoss @EmployeeID, @IsBossValue output;
print 'Employee with ID ' + convert(nvarchar, @EmployeeID) + case when @IsBossValue = 1 then ' is' else ' isn`t' end + ' a boss.';