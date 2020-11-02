WITH SelectedId(RecipesId) AS
	(
		select Recipes.Id
		from Recipes, Categories
		where Recipes.CategoryId=3 and  Recipes.CategoryId=Categories.Id and Categories.ParentId is null
		union
		select Recipes.Id
		from Recipes, Categories
		where Recipes.CategoryId=Categories.Id and Categories.ParentId =3
	)
SELECT Recipes.Name as NameRecept, Ingredients.Name as NameIngredient
FROM Recipes 
JOIN RecipeIngredient 
ON RecipeIngredient.RecipeId = Recipes.Id 
JOIN Ingredients on RecipeIngredient.IngredientId=Ingredients.Id
WHERE Recipes.Id IN(SELECT TOP 3 RecipesId from SelectedId);