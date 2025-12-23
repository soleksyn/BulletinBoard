-- ============================================= 
-- Stored Procedure: [dbo].[sp_GetSubCategoriesByCategoryId] 
-- Description: Gets all subcategories for a specific category
-- ============================================= 

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_GetSubCategoriesByCategoryId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_GetSubCategoriesByCategoryId]
GO

CREATE PROCEDURE [dbo].[sp_GetSubCategoriesByCategoryId]
    @CategoryId INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT [Id], [CategoryId], [Name] FROM [dbo].[SubCategories] WHERE [CategoryId] = @CategoryId ORDER BY [Name];
END
GO
