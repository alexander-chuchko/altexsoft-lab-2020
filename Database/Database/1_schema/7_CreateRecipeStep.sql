--Создаем таблицу RecipeStep
create table RecipeStep
(
	RecipeId int foreign key(RecipeId) references Recipes(id),
	StepId int foreign key(StepId) references Steps(id)
	primary key(RecipeId, StepId)
)