CREATE TABLE [dbo].[Announcements] (
    [Id] INT PRIMARY KEY IDENTITY(1,1),
    [Title] NVARCHAR(200) NOT NULL,
    [Description] NVARCHAR(MAX) NOT NULL,
    [CreatedDate] DATETIME NOT NULL DEFAULT GETDATE(),
    [IsActive] BIT NOT NULL DEFAULT 1,
    [CategoryId] INT NOT NULL,
    [SubCategoryId] INT NOT NULL,
    [Price] DECIMAL(18, 2) NULL,
    [UserId] INT NOT NULL,
    CONSTRAINT FK_Announcements_Categories FOREIGN KEY (CategoryId) REFERENCES dbo.Categories(Id),
    CONSTRAINT FK_Announcements_SubCategories FOREIGN KEY (SubCategoryId) REFERENCES dbo.SubCategories(Id),
    CONSTRAINT FK_Announcements_Users FOREIGN KEY (UserId) REFERENCES dbo.Users(Id)
);
