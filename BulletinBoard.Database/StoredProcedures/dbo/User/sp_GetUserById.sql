-- ============================================= 
-- Stored Procedure: [dbo].[sp_GetUserById] 
-- Description: Gets a user by Id 
-- Created: 2025-12-23 
-- Author: Assistant 
-- ============================================= 
 
 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_GetUserById]') AND type in (N'P', N'PC')) 
 DROP PROCEDURE [dbo].[sp_GetUserById] 
 GO 
 
 CREATE PROCEDURE [dbo].[sp_GetUserById] 
     @Id INT 
 AS 
 BEGIN 
     SET NOCOUNT ON; 
     
     BEGIN TRY 
         SELECT [Id], [Username], [Email], [PasswordHash], [RoleId], [CreatedDate], [IsActive]
         FROM [dbo].[Users]
         WHERE [Id] = @Id;
         
     END TRY 
     BEGIN CATCH 
         DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE(); 
         DECLARE @ErrorSeverity INT = ERROR_SEVERITY(); 
         DECLARE @ErrorState INT = ERROR_STATE(); 
         
         RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState); 
     END CATCH 
 END 
 GO
