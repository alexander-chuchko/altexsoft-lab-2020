--Создаем таблицу с ингредиентами--
create table Ingredients
(
	Id int not null identity(1,1) primary key,
	Name nvarchar(128) not null Unique
)