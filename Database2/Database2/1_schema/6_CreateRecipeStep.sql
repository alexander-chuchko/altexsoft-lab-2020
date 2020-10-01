--Создаем таблицу RecipeStep
create table RecipeStep
(
	id int not null identity(1,1) primary key,
	RecipeId int foreign key(RecipeId) references Recipes(id),
	StepId int foreign key(StepId) references Steps(id)
)