WITH RecursiveQuery(id, ParentId, Name) AS
	(
		select categories.Id, categories.ParentId, categories.Name 
		from Categories categories
		where categories.Id=3
		union all
		select categories.Id, categories.ParentId, categories.Name 
		from Categories categories
		join RecursiveQuery rq on categories.ParentId=rq.Id
	)		
SELECT categories.Name as Category, recieps.Name as NameReciept, ingredient.Name as Ingredient 
FROM  Recipes recieps
JOIN RecipeIngredient ri ON recieps.Id = ri.RecipeId
JOIN Categories categories ON recieps.CategoryId = categories.Id
JOIN Ingredients ingredient ON ri.IngredientId = ingredient.Id
WHERE recieps.Id IN (SELECT TOP 3 recieps1.Id FROM Recipes recieps1 INNER JOIN RecursiveQuery rq1 ON recieps1.CategoryId = rq1.Id)
ORDER BY recieps.Name ASC;