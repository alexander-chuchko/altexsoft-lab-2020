create table Categories
(
	Id int not null identity(1,1) primary key,
	Name nvarchar(256) not null Unique,
	ParentId int null references Categories(Id)
)