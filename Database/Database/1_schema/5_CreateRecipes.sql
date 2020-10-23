--Создаем таблицу с рецептами
create table Recipes
(
Id int not null identity(1,1) primary key,
Name nvarchar(128) not null Unique,
Specification nvarchar(2048) not null,
CategoryId int foreign key(CategoryId) references Categories(id) not null,
SubcategoryID int foreign key(SubcategoryID) references Subcategory(id)
)