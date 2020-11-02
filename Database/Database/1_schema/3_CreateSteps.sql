--Создаем таблицу с шаги приготовления рецептов
create table Steps
(
	Id int not null identity(1,1) primary key,
	Name nvarchar(1024) not null
)