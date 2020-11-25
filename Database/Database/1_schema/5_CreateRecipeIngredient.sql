--Создаем таблицу RecipeIngredient
create table RecipeIngredient
(
	RecipeId int foreign key(RecipeId) references Recipes(id) ON DELETE SET NULL,
	IngredientId int foreign key(IngredientId) references Ingredients(id)
    primary key(RecipeId, IngredientId)
)