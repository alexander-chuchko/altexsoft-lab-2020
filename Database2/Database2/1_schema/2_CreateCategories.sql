--Создаем таблицу с категориями
create table Categories
(
Id int not null identity(1,1) primary key,
NameCategory nvarchar(256) not null Unique
)