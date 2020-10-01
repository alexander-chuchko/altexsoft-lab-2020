--Создаем таблицу RecipeIngredient
create table RecipeIngredient
(
	id int not null identity(1,1) primary key,
	RecipeId int foreign key(RecipeId) references Recipes(id) ON DELETE SET NULL,
	IngredientId int foreign key(IngredientId) references Ingredients(id)
)