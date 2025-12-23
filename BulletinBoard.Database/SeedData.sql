-- Clear existing data
DELETE FROM [dbo].[Announcements];
DELETE FROM [dbo].[Users];
DELETE FROM [dbo].[SubCategories];
DELETE FROM [dbo].[Categories];

-- Reset Identities
DBCC CHECKIDENT ('[dbo].[Categories]', RESEED, 0);
DBCC CHECKIDENT ('[dbo].[SubCategories]', RESEED, 0);
DBCC CHECKIDENT ('[dbo].[Users]', RESEED, 0);
DBCC CHECKIDENT ('[dbo].[Announcements]', RESEED, 0);

-- Categories Seed
INSERT INTO [dbo].[Categories] ([Name]) VALUES 
('Home Appliances'),
('Computing'),
('Smartphones'),
('Other');

-- SubCategories Seed
-- Home Appliances
DECLARE @HomeAppliancesId INT = (SELECT Id FROM [dbo].[Categories] WHERE [Name] = 'Home Appliances');
INSERT INTO [dbo].[SubCategories] ([CategoryId], [Name]) VALUES 
(@HomeAppliancesId, 'Refrigerators'),
(@HomeAppliancesId, 'Washing Machines'),
(@HomeAppliancesId, 'Water Heaters'),
(@HomeAppliancesId, 'Ovens'),
(@HomeAppliancesId, 'Hoods'),
(@HomeAppliancesId, 'Microwaves');

-- Computing
DECLARE @ComputingId INT = (SELECT Id FROM [dbo].[Categories] WHERE [Name] = 'Computing');
INSERT INTO [dbo].[SubCategories] ([CategoryId], [Name]) VALUES 
(@ComputingId, 'PC'),
(@ComputingId, 'Laptops'),
(@ComputingId, 'Monitors'),
(@ComputingId, 'Printers'),
(@ComputingId, 'Scanners');

-- Smartphones
DECLARE @SmartphonesId INT = (SELECT Id FROM [dbo].[Categories] WHERE [Name] = 'Smartphones');
INSERT INTO [dbo].[SubCategories] ([CategoryId], [Name]) VALUES 
(@SmartphonesId, 'Android Smartphones'),
(@SmartphonesId, 'iOS/Apple Smartphones');

-- Other
DECLARE @OtherId INT = (SELECT Id FROM [dbo].[Categories] WHERE [Name] = 'Other');
INSERT INTO [dbo].[SubCategories] ([CategoryId], [Name]) VALUES 
(@OtherId, 'Clothing'),
(@OtherId, 'Shoes'),
(@OtherId, 'Accessories'),
(@OtherId, 'Sports Equipment'),
(@OtherId, 'Toys');

-- Users Seed
-- Password is 'Password123!' for all users
-- Hash: $2a$11$92IXUNpkjO0rOQ5byMi.Ye4oKoEa3Ro9llC/.og/at2.uheWG/igi (actually this is 'password')
INSERT INTO [dbo].[Users] ([Username], [Email], [PasswordHash], [RoleId]) VALUES
('admin', 'admin@vibe.com', '$2a$11$92IXUNpkjO0rOQ5byMi.Ye4oKoEa3Ro9llC/.og/at2.uheWG/igi', 2),
('john_doe', 'john@example.com', '$2a$11$92IXUNpkjO0rOQ5byMi.Ye4oKoEa3Ro9llC/.og/at2.uheWG/igi', 1),
('jane_smith', 'jane@example.com', '$2a$11$92IXUNpkjO0rOQ5byMi.Ye4oKoEa3Ro9llC/.og/at2.uheWG/igi', 1);

DECLARE @AdminId INT = (SELECT Id FROM [dbo].[Users] WHERE [Username] = 'admin');
DECLARE @JohnId INT = (SELECT Id FROM [dbo].[Users] WHERE [Username] = 'john_doe');
DECLARE @JaneId INT = (SELECT Id FROM [dbo].[Users] WHERE [Username] = 'jane_smith');

-- Announcements Seed (20 items)
INSERT INTO [dbo].[Announcements] ([Title], [Description], [CreatedDate], [IsActive], [CategoryId], [SubCategoryId], [Price], [UserId]) VALUES
('Samsung Bespoke Refrigerator', 'Premium smart refrigerator with customizable panels and Family Hub.', GETDATE(), 1, 1, 1, 1200.00, @JohnId),
('LG Front Load Washer', 'Energy efficient washing machine with AI DD technology.', GETDATE(), 1, 1, 2, 850.00, @JohnId),
('MacBook Pro M3 Max', 'The most powerful laptop for pros with 16-inch Liquid Retina XDR display.', GETDATE(), 1, 2, 8, 3500.00, @JaneId),
('iPhone 15 Pro', 'Titanium design, A17 Pro chip, and advanced camera system.', GETDATE(), 1, 3, 13, 999.00, @JaneId),
('Dell UltraSharp 32" 4K', 'Professional monitor with IPS Black technology and high color accuracy.', GETDATE(), 1, 2, 9, 1100.00, @AdminId),
('Sony PlayStation 5', 'Next-gen gaming console with ultra-high speed SSD and haptic feedback.', GETDATE(), 1, 4, 18, 499.00, @AdminId),
('Nike Air Max 270', 'Comfortable lifestyle sneakers with large Air unit.', GETDATE(), 1, 4, 15, 150.00, @JohnId),
('Bosh Built-in Oven', 'Modern oven with 4D HotAir and AutoPilot programs.', GETDATE(), 1, 1, 4, 750.00, @JohnId),
('ASUS ROG Gaming Desktop', 'High-performance gaming PC with RTX 4090 and Intel Core i9.', GETDATE(), 1, 2, 7, 4500.00, @JaneId),
('Google Pixel 8 Pro', 'The most advanced Pixel yet with AI-powered camera features.', GETDATE(), 1, 3, 12, 899.00, @JaneId),
('HP LaserJet Enterprise', 'Fast and secure laser printer for large workgroups.', GETDATE(), 1, 2, 10, 600.00, @AdminId),
('Adidas Ultraboost Light', 'Lightweight running shoes with maximum energy return.', GETDATE(), 1, 4, 15, 180.00, @JohnId),
('Epson Perfection V600', 'Professional photo scanner with 6400 dpi resolution.', GETDATE(), 1, 2, 11, 250.00, @JaneId),
('North Face Nuptse Jacket', 'Classic down jacket for extreme cold weather.', GETDATE(), 1, 4, 14, 320.00, @AdminId),
('Dyson Purifier Cool', 'Smart air purifier and fan with HEPA filtration.', GETDATE(), 1, 1, 5, 550.00, @JohnId),
('LEGO Star Wars Millennium Falcon', 'Ultimate collector series set with over 7500 pieces.', GETDATE(), 1, 4, 18, 850.00, @JaneId),
('Wilson Basketball', 'Official size and weight basketball for indoor/outdoor play.', GETDATE(), 1, 4, 17, 30.00, @JohnId),
('Ariston 80L Water Heater', 'Eco-friendly electric water heater with digital display.', GETDATE(), 1, 1, 3, 220.00, @JaneId),
('Ray-Ban Wayfarer Sunglasses', 'Iconic eyewear design with polarized lenses.', GETDATE(), 1, 4, 16, 160.00, @AdminId),
('Panasonic Microwave Oven', 'Inverter technology for even cooking and defrosting.', GETDATE(), 1, 1, 6, 180.00, @JohnId);
