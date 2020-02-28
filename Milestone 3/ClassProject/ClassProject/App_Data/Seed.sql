INSERT INTO [dbo].[Athletes] (Name, DOB) VALUES
	('Testy McTesty', '2000-11-11'),
	('Testy McTesty The Second', '2000-2-3')

INSERT INTO [dbo].[EventTypes] (EventName) VALUES
	('Kendama Jam Sesh')

INSERT INTO [dbo].[Locations] (LocationName) VALUES
	('Creepy Warehouse')

INSERT INTO [dbo].[Events] (LocationID, EventTypeID, EventDate) VALUES
	('1', '1', '2020-1-25')

INSERT INTO [dbo].[Coaches] (Name) VALUES 
	('Maya tiger')

INSERT INTO [dbo].[Teams] (Name, CoachID) VALUES
	('Sweets Kendama', '1')

INSERT INTO [dbo].[Times] (AthleteID, EventID, Time) VALUES
	('1', '1', '0:0:1'),
	('2', '1', '0:1:1')

INSERT INTO [dbo].[AthleteTeams] (AthleteID, TeamID) VALUES 
	('1', '1')

INSERT INTO [dbo].[EventTeams] (TeamID, EventID) VALUES 
	('1', '1')
GO