--Создаем таблицу с подкатегориями--
create table Subcategory
(
Id int not null identity(1,1) primary key,
Name nvarchar(128) not null Unique,
CategoryId int foreign key(CategoryId) references Categories(id) not null
)