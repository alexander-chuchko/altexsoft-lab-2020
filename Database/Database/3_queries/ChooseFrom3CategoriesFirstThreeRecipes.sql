WITH SelectedId(RecipesId) AS
	(
		SELECT TOP 3 Recipes.Id
		FROM Recipes
		WHERE Recipes.CategoryId=3 AND Recipes.SubcategoryID=1
	)
SELECT Recipes.Name, Ingredients.Name
FROM Recipes 
JOIN RecipeIngredient 
ON RecipeIngredient.RecipeId = Recipes.Id 
JOIN Ingredients ON RecipeIngredient.IngredientId=Ingredients.Id
WHERE Recipes.Id IN(SELECT RecipesId FROM SelectedId);