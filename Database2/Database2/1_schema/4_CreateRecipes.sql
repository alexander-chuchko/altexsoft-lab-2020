--Создаем таблицу с рецептами
create table Recipes
(
Id int not null identity(1,1) primary key,
RecipeName nvarchar(128) not null Unique,
RecipeDescription nvarchar(2048) not null,
NameCategoryId int foreign key(NameCategoryId) references Categories(id)
)