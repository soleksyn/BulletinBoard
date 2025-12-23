-- ============================================= 
-- Stored Procedure: [dbo].[sp_GetAnnouncementById] 
-- Description: Gets an announcement by Id 
-- Created: 2025-12-21 
-- Author: Assistant 
-- ============================================= 
 
 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_GetAnnouncementById]') AND type in (N'P', N'PC')) 
 DROP PROCEDURE [dbo].[sp_GetAnnouncementById] 
 GO 
 
 CREATE PROCEDURE [dbo].[sp_GetAnnouncementById] 
     @Id INT
 AS 
 BEGIN 
     SET NOCOUNT ON; 
     
     BEGIN TRY 
         SELECT 
            A.[Id], A.[Title], A.[Description], A.[CreatedDate], A.[IsActive], 
            A.[CategoryId], A.[SubCategoryId], A.[Price],
            C.[Name] AS [CategoryName],
             SC.[Name] AS [SubCategoryName]
         FROM [dbo].[Announcements] A
         INNER JOIN [dbo].[Categories] C ON A.[CategoryId] = C.[Id]
         INNER JOIN [dbo].[SubCategories] SC ON A.[SubCategoryId] = SC.[Id]
         WHERE A.[Id] = @Id;
         
     END TRY 
     BEGIN CATCH 
         DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE(); 
         DECLARE @ErrorSeverity INT = ERROR_SEVERITY(); 
         DECLARE @ErrorState INT = ERROR_STATE(); 
         
         RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState); 
     END CATCH 
 END 
 GO
