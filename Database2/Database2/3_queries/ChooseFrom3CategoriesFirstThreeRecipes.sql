SELECT Recipes.RecipeName, Ingredients.NameIngredient
FROM Recipes 
JOIN RecipeIngredient 
ON RecipeIngredient.RecipeId = Recipes.Id 
JOIN Ingredients on RecipeIngredient.IngredientId=Ingredients.Id
WHERE Recipes.Id IN (select TOP 3 Recipes.Id from Recipes
where Recipes.NameCategoryId=3)