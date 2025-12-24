-- ============================================= 
-- Stored Procedure: [dbo].[sp_CreateUser] 
-- Description: Creates a new user 
-- Created: 2025-12-23 
-- Author: Assistant 
-- ============================================= 
 
 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_CreateUser]') AND type in (N'P', N'PC')) 
 DROP PROCEDURE [dbo].[sp_CreateUser] 
 GO 
 
 CREATE PROCEDURE [dbo].[sp_CreateUser] 
    @Username NVARCHAR(100),
    @Email NVARCHAR(256),
    @PasswordHash NVARCHAR(MAX),
    @RoleId INT = 0
AS 
BEGIN 
    SET NOCOUNT ON; 
    
    BEGIN TRY 
        BEGIN TRANSACTION; 
        
        INSERT INTO [dbo].[Users] ([Username], [Email], [PasswordHash], [RoleId])
        VALUES (@Username, @Email, @PasswordHash, @RoleId);
        
        SELECT SCOPE_IDENTITY();
        
        COMMIT TRANSACTION; 
        
    END TRY 
     BEGIN CATCH 
         IF @@TRANCOUNT > 0 
             ROLLBACK TRANSACTION; 
         
         DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE(); 
         DECLARE @ErrorSeverity INT = ERROR_SEVERITY(); 
         DECLARE @ErrorState INT = ERROR_STATE(); 
         
         RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState); 
     END CATCH 
 END 
 GO
