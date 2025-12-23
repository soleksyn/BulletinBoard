CREATE TABLE dbo.SubCategories (
   Id INT PRIMARY KEY IDENTITY(1,1),
   CategoryId INT NOT NULL,
   Name NVARCHAR(100) NOT NULL,
   CONSTRAINT FK_SubCategories_Categories FOREIGN KEY (CategoryId) REFERENCES dbo.Categories(Id) ON DELETE CASCADE,
   CONSTRAINT UQ_SubCategory_Name UNIQUE (CategoryId, Name)
);