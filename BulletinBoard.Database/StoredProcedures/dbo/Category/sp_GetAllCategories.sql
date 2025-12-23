-- ============================================= 
-- Stored Procedure: [dbo].[sp_GetAllCategories] 
-- Description: Gets all categories ordered by name
-- ============================================= 

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_GetAllCategories]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_GetAllCategories]
GO

CREATE PROCEDURE [dbo].[sp_GetAllCategories]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT [Id], [Name] FROM [dbo].[Categories] ORDER BY [Name];
END
GO
