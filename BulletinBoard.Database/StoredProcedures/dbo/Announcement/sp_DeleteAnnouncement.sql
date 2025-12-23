-- ============================================= 
-- Stored Procedure: [dbo].[sp_DeleteAnnouncement] 
-- Description: Deletes an announcement by Id 
-- Created: 2025-12-21 
-- Author: Assistant 
-- ============================================= 
 
 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_DeleteAnnouncement]') AND type in (N'P', N'PC')) 
 DROP PROCEDURE [dbo].[sp_DeleteAnnouncement] 
 GO 
 
 CREATE PROCEDURE [dbo].[sp_DeleteAnnouncement] 
     -- Input Parameters 
     @Id INT
 AS 
 BEGIN 
     SET NOCOUNT ON; 
     
     BEGIN TRY 
         BEGIN TRANSACTION; 
         
         -- Main logic here 
         DELETE FROM [dbo].[Announcements]
         WHERE [Id] = @Id;
         
         COMMIT TRANSACTION; 
         
     END TRY 
     BEGIN CATCH 
         -- Rollback transaction on error 
         IF @@TRANCOUNT > 0 
             ROLLBACK TRANSACTION; 
         
         -- Rethrow error 
         DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE(); 
         DECLARE @ErrorSeverity INT = ERROR_SEVERITY(); 
         DECLARE @ErrorState INT = ERROR_STATE(); 
         
         RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState); 
     END CATCH 
 END 
 GO
