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

-- Announcements Seed (20 items)
-- Using hardcoded IDs based on order of insertion (1-4 for categories, 1-18 for subcategories)
INSERT INTO [dbo].[Announcements] ([Title], [Description], [CreatedDate], [IsActive], [CategoryId], [SubCategoryId], [Price]) VALUES
('Samsung Bespoke Refrigerator', 'Premium smart refrigerator with customizable panels and Family Hub.', GETDATE(), 1, 1, 1, 1200.00),
('LG Front Load Washer', 'Energy efficient washing machine with AI DD technology.', GETDATE(), 1, 1, 2, 850.00),
('MacBook Pro M3 Max', 'The most powerful laptop for pros with 16-inch Liquid Retina XDR display.', GETDATE(), 1, 2, 8, 3500.00),
('iPhone 15 Pro', 'Titanium design, A17 Pro chip, and advanced camera system.', GETDATE(), 1, 3, 13, 999.00),
('Dell UltraSharp 32" 4K', 'Professional monitor with IPS Black technology and high color accuracy.', GETDATE(), 1, 2, 9, 1100.00),
('Sony PlayStation 5', 'Next-gen gaming console with ultra-high speed SSD and haptic feedback.', GETDATE(), 1, 4, 18, 499.00),
('Nike Air Max 270', 'Comfortable lifestyle sneakers with large Air unit.', GETDATE(), 1, 4, 15, 150.00),
('Bosh Built-in Oven', 'Modern oven with 4D HotAir and AutoPilot programs.', GETDATE(), 1, 1, 4, 750.00),
('ASUS ROG Gaming Desktop', 'High-performance gaming PC with RTX 4090 and Intel Core i9.', GETDATE(), 1, 2, 7, 4500.00),
('Google Pixel 8 Pro', 'The most advanced Pixel yet with AI-powered camera features.', GETDATE(), 1, 3, 12, 899.00),
('HP LaserJet Enterprise', 'Fast and secure laser printer for large workgroups.', GETDATE(), 1, 2, 10, 600.00),
('Adidas Ultraboost Light', 'Lightweight running shoes with maximum energy return.', GETDATE(), 1, 4, 15, 180.00),
('Epson Perfection V600', 'Professional photo scanner with 6400 dpi resolution.', GETDATE(), 1, 2, 11, 250.00),
('North Face Nuptse Jacket', 'Classic down jacket for extreme cold weather.', GETDATE(), 1, 4, 14, 320.00),
('Dyson Purifier Cool', 'Smart air purifier and fan with HEPA filtration.', GETDATE(), 1, 1, 5, 550.00),
('LEGO Star Wars Millennium Falcon', 'Ultimate collector series set with over 7500 pieces.', GETDATE(), 1, 4, 18, 850.00),
('Wilson Basketball', 'Official size and weight basketball for indoor/outdoor play.', GETDATE(), 1, 4, 17, 30.00),
('Ariston 80L Water Heater', 'Eco-friendly electric water heater with digital display.', GETDATE(), 1, 1, 3, 220.00),
('Ray-Ban Wayfarer Sunglasses', 'Iconic eyewear design with polarized lenses.', GETDATE(), 1, 4, 16, 160.00),
('Panasonic Microwave Oven', 'Inverter technology for even cooking and defrosting.', GETDATE(), 1, 1, 6, 180.00);
