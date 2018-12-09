/***********

Basic Data insertion script for new system setup
Prepared BY : Jigar Joshi

************/

DECLARE @UserId UNIQUEIDENTIFIER = (Select TOP 1 Id FROM AspNetUsers WHERE UserName = 'Admin')

/* Lookup Types */

INSERT INTO [LookupType]	([Name], [Description], CreatedBy, CreatedDate, UpdatedBy, UpdatedDate, Deleted)
VALUES						('Country', 'Country', @UserId, GETDATE(), @UserId, GETDATE(), 0),
							('State', 'State', @UserId, GETDATE(), @UserId, GETDATE(), 0),
							('City', 'City', @UserId, GETDATE(), @UserId, GETDATE(), 0),
							('Enquiry Status', 'Enquiry Status', @UserId, GETDATE(), @UserId, GETDATE(), 0),
							('Source', 'Source', @UserId, GETDATE(), @UserId, GETDATE(), 0),
							('Expense Type', 'Expense Type', @UserId, GETDATE(), @UserId, GETDATE(), 0)

/*** Basic Lookup Data ***/

--Country
INSERT INTO [LookUp]	([Name], TypeId, [Order], [ParentTypeId], [ParentId], [Description], CreatedBy, CreatedDate, UpdatedBy, UpdatedDate, Deleted)
VALUES					('India', (Select TOP 1 Id FROM [LookupType] WHERE [Name] = 'Country'), 1, NULL, NULL, 'India', @UserId, GETDATE(), @UserId, GETDATE(), 0)

--State

DECLARE @StateTypeId UNIQUEIDENTIFIER = (Select TOP 1 Id FROM [LookupType] WHERE [Name] = 'State');
DECLARE @StateParentTypeId UNIQUEIDENTIFIER = (Select TOP 1 Id FROM [LookupType] WHERE [Name] = 'Country');
DECLARE @StatePrentId UNIQUEIDENTIFIER = (Select TOP 1 Id FROM [LookUp] WHERE [Name] = 'India' AND [TypeId] = @StateParentTypeId);

INSERT INTO [LookUp]	([Name], TypeId, [Order], [ParentTypeId], [ParentId], [Description], CreatedBy, CreatedDate, UpdatedBy, UpdatedDate, Deleted)
VALUES					('Gujarat', @StateTypeId, 1, @StateParentTypeId, @StatePrentId, 'Gujarat', @UserId, GETDATE(), @UserId, GETDATE(), 0)

--City

DECLARE @CityTypeId UNIQUEIDENTIFIER = (Select TOP 1 Id FROM [LookupType] WHERE [Name] = 'City');
DECLARE @CityParentTypeId UNIQUEIDENTIFIER = (Select TOP 1 Id FROM [LookupType] WHERE [Name] = 'State');
DECLARE @CityParentId UNIQUEIDENTIFIER = (Select TOP 1 Id FROM [LookUp] WHERE [Name] = 'Gujarat' AND [TypeId] = @CityParentTypeId); 

INSERT INTO [LookUp]	([Name], TypeId, [Order], [ParentTypeId], [ParentId], [Description], CreatedBy, CreatedDate, UpdatedBy, UpdatedDate, Deleted)
VALUES					('Vadodara', @CityTypeId, 1, @CityParentTypeId, @CityParentId, 'Vadodara', @UserId, GETDATE(), @UserId, GETDATE(), 0),
						('Ahmedabad', @CityTypeId, 2, @CityParentTypeId, @CityParentId, 'Ahmedabad', @UserId, GETDATE(), @UserId, GETDATE(), 0),
						('Surat', @CityTypeId, 3, @CityParentTypeId, @CityParentId, 'Surat', @UserId, GETDATE(), @UserId, GETDATE(), 0)
						
--Source
INSERT INTO [LookUp]	([Name], TypeId, [Order], [ParentTypeId], [ParentId], [Description], CreatedBy, CreatedDate, UpdatedBy, UpdatedDate, Deleted)
VALUES					('News Paper', (Select TOP 1 Id FROM [LookupType] WHERE [Name] = 'Source'), 1, NULL, NULL, 'News Paper', @UserId, GETDATE(), @UserId, GETDATE(), 0),
						('Google', (Select TOP 1 Id FROM [LookupType] WHERE [Name] = 'Source'), 1, NULL, NULL, 'Google', @UserId, GETDATE(), @UserId, GETDATE(), 0),
						('Hoardings', (Select TOP 1 Id FROM [LookupType] WHERE [Name] = 'Source'), 1, NULL, NULL, 'Hoardings', @UserId, GETDATE(), @UserId, GETDATE(), 0),
						('Friends', (Select TOP 1 Id FROM [LookupType] WHERE [Name] = 'Source'), 1, NULL, NULL, 'Friends', @UserId, GETDATE(), @UserId, GETDATE(), 0),
						('Other', (Select TOP 1 Id FROM [LookupType] WHERE [Name] = 'Source'), 1, NULL, NULL, 'Other', @UserId, GETDATE(), @UserId, GETDATE(), 0)

--Enquiry Status
INSERT INTO [LookUp]	([Name], TypeId, [Order], [ParentTypeId], [ParentId], [Description], CreatedBy, CreatedDate, UpdatedBy, UpdatedDate, Deleted)
VALUES					('Open', (Select TOP 1 Id FROM [LookupType] WHERE [Name] = 'Enquiry Status'), 1, NULL, NULL, 'Open', @UserId, GETDATE(), @UserId, GETDATE(), 0),
						('Won', (Select TOP 1 Id FROM [LookupType] WHERE [Name] = 'Enquiry Status'), 1, NULL, NULL, 'Won', @UserId, GETDATE(), @UserId, GETDATE(), 0),
						('Lost', (Select TOP 1 Id FROM [LookupType] WHERE [Name] = 'Enquiry Status'), 1, NULL, NULL, 'Lost', @UserId, GETDATE(), @UserId, GETDATE(), 0)

--Expense Type
INSERT INTO [LookUp]	([Name], TypeId, [Order], [ParentTypeId], [ParentId], [Description], CreatedBy, CreatedDate, UpdatedBy, UpdatedDate, Deleted)
VALUES					('Tea', (Select TOP 1 Id FROM [LookupType] WHERE [Name] = 'Expense Type'), 1, NULL, NULL, 'Tea', @UserId, GETDATE(), @UserId, GETDATE(), 0),
						('Stationary', (Select TOP 1 Id FROM [LookupType] WHERE [Name] = 'Expense Type'), 1, NULL, NULL, 'Stationary', @UserId, GETDATE(), @UserId, GETDATE(), 0),
						('Other', (Select TOP 1 Id FROM [LookupType] WHERE [Name] = 'Expense Type'), 1, NULL, NULL, 'Other', @UserId, GETDATE(), @UserId, GETDATE(), 0)