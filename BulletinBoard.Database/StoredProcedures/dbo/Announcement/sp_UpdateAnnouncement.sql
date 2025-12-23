-- ============================================= 
-- Stored Procedure: [dbo].[sp_UpdateAnnouncement] 
-- Description: Updates an existing announcement 
-- Created: 2025-12-21 
-- Author: Assistant 
-- ============================================= 
 
 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_UpdateAnnouncement]') AND type in (N'P', N'PC')) 
 DROP PROCEDURE [dbo].[sp_UpdateAnnouncement] 
 GO 
 
 CREATE PROCEDURE [dbo].[sp_UpdateAnnouncement] 
    @Id INT,
    @Title NVARCHAR(200),
    @Description NVARCHAR(MAX),
    @IsActive BIT,
    @CategoryId INT,
    @SubCategoryId INT,
    @Price DECIMAL(18, 2) = NULL,
    @UserId INT
AS 
BEGIN 
    SET NOCOUNT ON; 
    
    BEGIN TRY 
        BEGIN TRANSACTION; 
        
        UPDATE [dbo].[Announcements]
        SET [Title] = @Title,
            [Description] = @Description,
            [IsActive] = @IsActive,
            [CategoryId] = @CategoryId,
            [SubCategoryId] = @SubCategoryId,
            [Price] = @Price,
            [UserId] = @UserId
        WHERE [Id] = @Id;
        
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
