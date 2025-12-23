-- ============================================= 
-- Stored Procedure: [dbo].[sp_CreateAnnouncement] 
-- Description: Creates a new announcement 
-- Created: 2025-12-21 
-- Author: Assistant 
-- ============================================= 
 
 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_CreateAnnouncement]') AND type in (N'P', N'PC')) 
 DROP PROCEDURE [dbo].[sp_CreateAnnouncement] 
 GO 
 
 CREATE PROCEDURE [dbo].[sp_CreateAnnouncement] 
    @Title NVARCHAR(200),
    @Description NVARCHAR(MAX),
    @IsActive BIT,
    @CategoryId INT,
    @SubCategoryId INT,
    @Price DECIMAL(18, 2) = NULL
AS 
BEGIN 
    SET NOCOUNT ON; 
    
    BEGIN TRY 
        BEGIN TRANSACTION; 
        
        INSERT INTO [dbo].[Announcements] ([Title], [Description], [CreatedDate], [IsActive], [CategoryId], [SubCategoryId], [Price])
        VALUES (@Title, @Description, GETDATE(), @IsActive, @CategoryId, @SubCategoryId, @Price);
        
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
