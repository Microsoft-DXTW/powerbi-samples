CREATE TABLE [dbo].[HVAC]
(
	[DeviceType] VARCHAR(50) NOT NULL , 
    [ReadingDateTime] DATETIME NOT NULL, 
    [RoomNumber] INT NOT NULL, 
    [Reading] FLOAT NOT NULL
)
