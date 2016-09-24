
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/12/2015 00:43:48
-- Generated from EDMX file: C:\workspace\VS2015\Anrl\anrl\AirNavigationRaceLive\AirNavigationRaceLive\AnrlModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_t_Competition_t_Competition_Team]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[t_Competition_Team] DROP CONSTRAINT [FK_t_Competition_t_Competition_Team];
GO
IF OBJECT_ID(N'[dbo].[FK_t_CompetitionSet_t_Competition]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[t_Competition] DROP CONSTRAINT [FK_t_CompetitionSet_t_Competition];
GO
IF OBJECT_ID(N'[dbo].[FK_t_Line_t_Competition]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[t_Competition] DROP CONSTRAINT [FK_t_Line_t_Competition];
GO
IF OBJECT_ID(N'[dbo].[FK_t_Tracker_t_Daten]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[t_Daten] DROP CONSTRAINT [FK_t_Tracker_t_Daten];
GO
IF OBJECT_ID(N'[dbo].[FK_t_GPSPoint_t_Line]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[t_Line] DROP CONSTRAINT [FK_t_GPSPoint_t_Line];
GO
IF OBJECT_ID(N'[dbo].[FK_t_GPSPoint_t_Line1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[t_Line] DROP CONSTRAINT [FK_t_GPSPoint_t_Line1];
GO
IF OBJECT_ID(N'[dbo].[FK_t_GPSPoint_t_Line2]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[t_Line] DROP CONSTRAINT [FK_t_GPSPoint_t_Line2];
GO
IF OBJECT_ID(N'[dbo].[FK_t_CompetitionSet_t_Map]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[t_Map] DROP CONSTRAINT [FK_t_CompetitionSet_t_Map];
GO
IF OBJECT_ID(N'[dbo].[FK_t_Map_t_Parcour]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[t_Parcour] DROP CONSTRAINT [FK_t_Map_t_Parcour];
GO
IF OBJECT_ID(N'[dbo].[FK_t_Picture_t_Map]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[t_Map] DROP CONSTRAINT [FK_t_Picture_t_Map];
GO
IF OBJECT_ID(N'[dbo].[FK_t_CompetitionSet_t_Parcour]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[t_Parcour] DROP CONSTRAINT [FK_t_CompetitionSet_t_Parcour];
GO
IF OBJECT_ID(N'[dbo].[FK_t_Competition_Team_t_Penalty]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[t_Penalty] DROP CONSTRAINT [FK_t_Competition_Team_t_Penalty];
GO
IF OBJECT_ID(N'[dbo].[FK_t_CompetitionSet_t_Penalty]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[t_Penalty] DROP CONSTRAINT [FK_t_CompetitionSet_t_Penalty];
GO
IF OBJECT_ID(N'[dbo].[FK_t_CompetitionSet_t_Pilot]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[t_Pilot] DROP CONSTRAINT [FK_t_CompetitionSet_t_Pilot];
GO
IF OBJECT_ID(N'[dbo].[FK_t_Picture_t_Pilot]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[t_Pilot] DROP CONSTRAINT [FK_t_Picture_t_Pilot];
GO
IF OBJECT_ID(N'[dbo].[FK_t_Pilot_t_Team]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[t_Team] DROP CONSTRAINT [FK_t_Pilot_t_Team];
GO
IF OBJECT_ID(N'[dbo].[FK_t_Pilot_t_Team1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[t_Team] DROP CONSTRAINT [FK_t_Pilot_t_Team1];
GO
IF OBJECT_ID(N'[dbo].[FK_t_Team_t_Competition_Team]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[t_Competition_Team] DROP CONSTRAINT [FK_t_Team_t_Competition_Team];
GO
IF OBJECT_ID(N'[dbo].[FK_t_CompetitionSet_t_Team]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[t_Team] DROP CONSTRAINT [FK_t_CompetitionSet_t_Team];
GO
IF OBJECT_ID(N'[dbo].[FK_t_Picture_t_Team]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[t_Team] DROP CONSTRAINT [FK_t_Picture_t_Team];
GO
IF OBJECT_ID(N'[dbo].[FK_t_Parcourt_Line]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[t_Line] DROP CONSTRAINT [FK_t_Parcourt_Line];
GO
IF OBJECT_ID(N'[dbo].[FK_t_Competition_Teamt_Tracker]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[t_Tracker] DROP CONSTRAINT [FK_t_Competition_Teamt_Tracker];
GO
IF OBJECT_ID(N'[dbo].[FK_t_Trackert_GPSPoint]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[t_GPSPoint] DROP CONSTRAINT [FK_t_Trackert_GPSPoint];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[t_Competition]', 'U') IS NOT NULL
    DROP TABLE [dbo].[t_Competition];
GO
IF OBJECT_ID(N'[dbo].[t_Competition_Team]', 'U') IS NOT NULL
    DROP TABLE [dbo].[t_Competition_Team];
GO
IF OBJECT_ID(N'[dbo].[t_CompetitionSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[t_CompetitionSet];
GO
IF OBJECT_ID(N'[dbo].[t_Daten]', 'U') IS NOT NULL
    DROP TABLE [dbo].[t_Daten];
GO
IF OBJECT_ID(N'[dbo].[t_GPS_IN]', 'U') IS NOT NULL
    DROP TABLE [dbo].[t_GPS_IN];
GO
IF OBJECT_ID(N'[dbo].[t_GPSPoint]', 'U') IS NOT NULL
    DROP TABLE [dbo].[t_GPSPoint];
GO
IF OBJECT_ID(N'[dbo].[t_Line]', 'U') IS NOT NULL
    DROP TABLE [dbo].[t_Line];
GO
IF OBJECT_ID(N'[dbo].[t_Log]', 'U') IS NOT NULL
    DROP TABLE [dbo].[t_Log];
GO
IF OBJECT_ID(N'[dbo].[t_Map]', 'U') IS NOT NULL
    DROP TABLE [dbo].[t_Map];
GO
IF OBJECT_ID(N'[dbo].[t_Parcour]', 'U') IS NOT NULL
    DROP TABLE [dbo].[t_Parcour];
GO
IF OBJECT_ID(N'[dbo].[t_Penalty]', 'U') IS NOT NULL
    DROP TABLE [dbo].[t_Penalty];
GO
IF OBJECT_ID(N'[dbo].[t_Picture]', 'U') IS NOT NULL
    DROP TABLE [dbo].[t_Picture];
GO
IF OBJECT_ID(N'[dbo].[t_Pilot]', 'U') IS NOT NULL
    DROP TABLE [dbo].[t_Pilot];
GO
IF OBJECT_ID(N'[dbo].[t_Team]', 'U') IS NOT NULL
    DROP TABLE [dbo].[t_Team];
GO
IF OBJECT_ID(N'[dbo].[t_Tracker]', 'U') IS NOT NULL
    DROP TABLE [dbo].[t_Tracker];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 't_Competition'
CREATE TABLE [dbo].[t_Competition] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [TimeTakeOff] bigint  NOT NULL,
    [TimeStartLine] bigint  NOT NULL,
    [TimeEndLine] bigint  NOT NULL,
    [ID_TakeOffLine] int  NOT NULL,
    [Name] varchar(50)  NOT NULL,
    [ID_Parcour] int  NOT NULL,
    [ID_CompetitionSet] int  NOT NULL
);
GO

-- Creating table 't_Competition_Team'
CREATE TABLE [dbo].[t_Competition_Team] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [StartID] int  NOT NULL,
    [Route] int  NOT NULL,
    [ID_Team] int  NOT NULL,
    [ID_Competition] int  NOT NULL,
    [TimeTakeOff] bigint  NOT NULL,
    [TimeStart] bigint  NOT NULL,
    [TimeEnd] bigint  NOT NULL,
    [ID_CompetitionSet] int  NOT NULL
);
GO

-- Creating table 't_CompetitionSet'
CREATE TABLE [dbo].[t_CompetitionSet] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(50)  NOT NULL
);
GO

-- Creating table 't_Daten'
CREATE TABLE [dbo].[t_Daten] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [ID_Tracker] int  NOT NULL,
    [Timestamp] bigint  NOT NULL,
    [Longitude] float  NOT NULL,
    [Latitude] float  NOT NULL,
    [Altitude] float  NOT NULL,
    [Speed] float  NOT NULL,
    [Accuracy] float  NULL,
    [Bearing] float  NULL
);
GO

-- Creating table 't_GPS_IN'
CREATE TABLE [dbo].[t_GPS_IN] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [IMEI] nchar(20)  NOT NULL,
    [Status] int  NOT NULL,
    [GPS_fix] int  NOT NULL,
    [TimestampTracker] datetime  NOT NULL,
    [longitude] nchar(12)  NOT NULL,
    [latitude] nchar(12)  NOT NULL,
    [altitude] nchar(10)  NOT NULL,
    [speed] nchar(10)  NOT NULL,
    [heading] nchar(10)  NOT NULL,
    [nr_used_sat] int  NOT NULL,
    [HDOP] nchar(10)  NOT NULL,
    [Timestamp] datetime  NOT NULL,
    [Processed] bit  NOT NULL
);
GO

-- Creating table 't_GPSPoint'
CREATE TABLE [dbo].[t_GPSPoint] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [altitude] float  NOT NULL,
    [longitude] float  NOT NULL,
    [latitude] float  NOT NULL,
    [edited] bit  NOT NULL,
    [timestampGPS] bigint  NOT NULL,
    [speed] float  NOT NULL,
    [bearing] float  NOT NULL,
    [accuracy] float  NOT NULL,
    [identifier] nvarchar(max)  NOT NULL,
    [timestampSender] bigint  NOT NULL,
    [t_Tracker_ID] int  NOT NULL
);
GO

-- Creating table 't_Line'
CREATE TABLE [dbo].[t_Line] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Type] int  NOT NULL,
    [ID_PointA] int  NOT NULL,
    [ID_PointB] int  NOT NULL,
    [ID_PointOrientation] int  NOT NULL,
    [t_Parcour_ID] int  NOT NULL
);
GO

-- Creating table 't_Log'
CREATE TABLE [dbo].[t_Log] (
    [id] int IDENTITY(1,1) NOT NULL,
    [timestamp] datetime  NOT NULL,
    [level] int  NOT NULL,
    [project] varchar(max)  NULL,
    [Text] varchar(max)  NULL
);
GO

-- Creating table 't_Map'
CREATE TABLE [dbo].[t_Map] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [ID_Picture] int  NOT NULL,
    [Name] varchar(50)  NOT NULL,
    [XSize] float  NOT NULL,
    [YSize] float  NOT NULL,
    [XRot] float  NOT NULL,
    [YRot] float  NOT NULL,
    [XTopLeft] float  NOT NULL,
    [YTopLeft] float  NOT NULL,
    [ID_CompetitionSet] int  NOT NULL
);
GO

-- Creating table 't_Parcour'
CREATE TABLE [dbo].[t_Parcour] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(50)  NOT NULL,
    [ID_Map] int  NOT NULL,
    [ID_CompetitionSet] int  NOT NULL
);
GO

-- Creating table 't_Penalty'
CREATE TABLE [dbo].[t_Penalty] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Points] int  NOT NULL,
    [Reason] varchar(500)  NOT NULL,
    [ID_Competition_Team] int  NOT NULL,
    [ID_CompetitionSet] int  NOT NULL
);
GO

-- Creating table 't_Picture'
CREATE TABLE [dbo].[t_Picture] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Data] varbinary(max)  NOT NULL,
    [Name] nchar(50)  NULL,
    [isFlag] bit  NOT NULL,
    [ID_CompetitionSet] int  NOT NULL
);
GO

-- Creating table 't_Pilot'
CREATE TABLE [dbo].[t_Pilot] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [LastName] varchar(50)  NOT NULL,
    [SureName] varchar(50)  NOT NULL,
    [ID_Picture] int  NULL,
    [ID_CompetitionSet] int  NOT NULL
);
GO

-- Creating table 't_Team'
CREATE TABLE [dbo].[t_Team] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(50)  NULL,
    [ID_Pilot] int  NOT NULL,
    [ID_Navigator] int  NULL,
    [Color] varchar(10)  NULL,
    [ID_Flag] int  NULL,
    [Description] varchar(500)  NULL,
    [ID_CompetitionSet] int  NOT NULL,
    [StartID] varchar(50)  NULL
);
GO

-- Creating table 't_Tracker'
CREATE TABLE [dbo].[t_Tracker] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [IMEI] varchar(50)  NOT NULL,
    [Name] varchar(50)  NULL,
    [Visible] bit  NOT NULL,
    [ID_Competition_Team] nvarchar(max)  NOT NULL,
    [t_Competition_Team_ID] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 't_Competition'
ALTER TABLE [dbo].[t_Competition]
ADD CONSTRAINT [PK_t_Competition]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 't_Competition_Team'
ALTER TABLE [dbo].[t_Competition_Team]
ADD CONSTRAINT [PK_t_Competition_Team]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 't_CompetitionSet'
ALTER TABLE [dbo].[t_CompetitionSet]
ADD CONSTRAINT [PK_t_CompetitionSet]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 't_Daten'
ALTER TABLE [dbo].[t_Daten]
ADD CONSTRAINT [PK_t_Daten]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 't_GPS_IN'
ALTER TABLE [dbo].[t_GPS_IN]
ADD CONSTRAINT [PK_t_GPS_IN]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 't_GPSPoint'
ALTER TABLE [dbo].[t_GPSPoint]
ADD CONSTRAINT [PK_t_GPSPoint]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 't_Line'
ALTER TABLE [dbo].[t_Line]
ADD CONSTRAINT [PK_t_Line]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [id] in table 't_Log'
ALTER TABLE [dbo].[t_Log]
ADD CONSTRAINT [PK_t_Log]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [ID] in table 't_Map'
ALTER TABLE [dbo].[t_Map]
ADD CONSTRAINT [PK_t_Map]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 't_Parcour'
ALTER TABLE [dbo].[t_Parcour]
ADD CONSTRAINT [PK_t_Parcour]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 't_Penalty'
ALTER TABLE [dbo].[t_Penalty]
ADD CONSTRAINT [PK_t_Penalty]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 't_Picture'
ALTER TABLE [dbo].[t_Picture]
ADD CONSTRAINT [PK_t_Picture]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 't_Pilot'
ALTER TABLE [dbo].[t_Pilot]
ADD CONSTRAINT [PK_t_Pilot]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 't_Team'
ALTER TABLE [dbo].[t_Team]
ADD CONSTRAINT [PK_t_Team]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 't_Tracker'
ALTER TABLE [dbo].[t_Tracker]
ADD CONSTRAINT [PK_t_Tracker]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ID_Competition] in table 't_Competition_Team'
ALTER TABLE [dbo].[t_Competition_Team]
ADD CONSTRAINT [FK_t_Competition_t_Competition_Team]
    FOREIGN KEY ([ID_Competition])
    REFERENCES [dbo].[t_Competition]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_t_Competition_t_Competition_Team'
CREATE INDEX [IX_FK_t_Competition_t_Competition_Team]
ON [dbo].[t_Competition_Team]
    ([ID_Competition]);
GO

-- Creating foreign key on [ID_CompetitionSet] in table 't_Competition'
ALTER TABLE [dbo].[t_Competition]
ADD CONSTRAINT [FK_t_CompetitionSet_t_Competition]
    FOREIGN KEY ([ID_CompetitionSet])
    REFERENCES [dbo].[t_CompetitionSet]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_t_CompetitionSet_t_Competition'
CREATE INDEX [IX_FK_t_CompetitionSet_t_Competition]
ON [dbo].[t_Competition]
    ([ID_CompetitionSet]);
GO

-- Creating foreign key on [ID_TakeOffLine] in table 't_Competition'
ALTER TABLE [dbo].[t_Competition]
ADD CONSTRAINT [FK_t_Line_t_Competition]
    FOREIGN KEY ([ID_TakeOffLine])
    REFERENCES [dbo].[t_Line]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_t_Line_t_Competition'
CREATE INDEX [IX_FK_t_Line_t_Competition]
ON [dbo].[t_Competition]
    ([ID_TakeOffLine]);
GO

-- Creating foreign key on [ID_Tracker] in table 't_Daten'
ALTER TABLE [dbo].[t_Daten]
ADD CONSTRAINT [FK_t_Tracker_t_Daten]
    FOREIGN KEY ([ID_Tracker])
    REFERENCES [dbo].[t_Tracker]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_t_Tracker_t_Daten'
CREATE INDEX [IX_FK_t_Tracker_t_Daten]
ON [dbo].[t_Daten]
    ([ID_Tracker]);
GO

-- Creating foreign key on [ID_PointA] in table 't_Line'
ALTER TABLE [dbo].[t_Line]
ADD CONSTRAINT [FK_t_GPSPoint_t_Line]
    FOREIGN KEY ([ID_PointA])
    REFERENCES [dbo].[t_GPSPoint]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_t_GPSPoint_t_Line'
CREATE INDEX [IX_FK_t_GPSPoint_t_Line]
ON [dbo].[t_Line]
    ([ID_PointA]);
GO

-- Creating foreign key on [ID_PointB] in table 't_Line'
ALTER TABLE [dbo].[t_Line]
ADD CONSTRAINT [FK_t_GPSPoint_t_Line1]
    FOREIGN KEY ([ID_PointB])
    REFERENCES [dbo].[t_GPSPoint]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_t_GPSPoint_t_Line1'
CREATE INDEX [IX_FK_t_GPSPoint_t_Line1]
ON [dbo].[t_Line]
    ([ID_PointB]);
GO

-- Creating foreign key on [ID_PointOrientation] in table 't_Line'
ALTER TABLE [dbo].[t_Line]
ADD CONSTRAINT [FK_t_GPSPoint_t_Line2]
    FOREIGN KEY ([ID_PointOrientation])
    REFERENCES [dbo].[t_GPSPoint]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_t_GPSPoint_t_Line2'
CREATE INDEX [IX_FK_t_GPSPoint_t_Line2]
ON [dbo].[t_Line]
    ([ID_PointOrientation]);
GO

-- Creating foreign key on [ID_CompetitionSet] in table 't_Map'
ALTER TABLE [dbo].[t_Map]
ADD CONSTRAINT [FK_t_CompetitionSet_t_Map]
    FOREIGN KEY ([ID_CompetitionSet])
    REFERENCES [dbo].[t_CompetitionSet]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_t_CompetitionSet_t_Map'
CREATE INDEX [IX_FK_t_CompetitionSet_t_Map]
ON [dbo].[t_Map]
    ([ID_CompetitionSet]);
GO

-- Creating foreign key on [ID_Map] in table 't_Parcour'
ALTER TABLE [dbo].[t_Parcour]
ADD CONSTRAINT [FK_t_Map_t_Parcour]
    FOREIGN KEY ([ID_Map])
    REFERENCES [dbo].[t_Map]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_t_Map_t_Parcour'
CREATE INDEX [IX_FK_t_Map_t_Parcour]
ON [dbo].[t_Parcour]
    ([ID_Map]);
GO

-- Creating foreign key on [ID_Picture] in table 't_Map'
ALTER TABLE [dbo].[t_Map]
ADD CONSTRAINT [FK_t_Picture_t_Map]
    FOREIGN KEY ([ID_Picture])
    REFERENCES [dbo].[t_Picture]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_t_Picture_t_Map'
CREATE INDEX [IX_FK_t_Picture_t_Map]
ON [dbo].[t_Map]
    ([ID_Picture]);
GO

-- Creating foreign key on [ID_CompetitionSet] in table 't_Parcour'
ALTER TABLE [dbo].[t_Parcour]
ADD CONSTRAINT [FK_t_CompetitionSet_t_Parcour]
    FOREIGN KEY ([ID_CompetitionSet])
    REFERENCES [dbo].[t_CompetitionSet]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_t_CompetitionSet_t_Parcour'
CREATE INDEX [IX_FK_t_CompetitionSet_t_Parcour]
ON [dbo].[t_Parcour]
    ([ID_CompetitionSet]);
GO

-- Creating foreign key on [ID_Competition_Team] in table 't_Penalty'
ALTER TABLE [dbo].[t_Penalty]
ADD CONSTRAINT [FK_t_Competition_Team_t_Penalty]
    FOREIGN KEY ([ID_Competition_Team])
    REFERENCES [dbo].[t_Competition_Team]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_t_Competition_Team_t_Penalty'
CREATE INDEX [IX_FK_t_Competition_Team_t_Penalty]
ON [dbo].[t_Penalty]
    ([ID_Competition_Team]);
GO

-- Creating foreign key on [ID_CompetitionSet] in table 't_Penalty'
ALTER TABLE [dbo].[t_Penalty]
ADD CONSTRAINT [FK_t_CompetitionSet_t_Penalty]
    FOREIGN KEY ([ID_CompetitionSet])
    REFERENCES [dbo].[t_CompetitionSet]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_t_CompetitionSet_t_Penalty'
CREATE INDEX [IX_FK_t_CompetitionSet_t_Penalty]
ON [dbo].[t_Penalty]
    ([ID_CompetitionSet]);
GO

-- Creating foreign key on [ID_CompetitionSet] in table 't_Pilot'
ALTER TABLE [dbo].[t_Pilot]
ADD CONSTRAINT [FK_t_CompetitionSet_t_Pilot]
    FOREIGN KEY ([ID_CompetitionSet])
    REFERENCES [dbo].[t_CompetitionSet]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_t_CompetitionSet_t_Pilot'
CREATE INDEX [IX_FK_t_CompetitionSet_t_Pilot]
ON [dbo].[t_Pilot]
    ([ID_CompetitionSet]);
GO

-- Creating foreign key on [ID_Picture] in table 't_Pilot'
ALTER TABLE [dbo].[t_Pilot]
ADD CONSTRAINT [FK_t_Picture_t_Pilot]
    FOREIGN KEY ([ID_Picture])
    REFERENCES [dbo].[t_Picture]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_t_Picture_t_Pilot'
CREATE INDEX [IX_FK_t_Picture_t_Pilot]
ON [dbo].[t_Pilot]
    ([ID_Picture]);
GO

-- Creating foreign key on [ID_Pilot] in table 't_Team'
ALTER TABLE [dbo].[t_Team]
ADD CONSTRAINT [FK_t_Pilot_t_Team]
    FOREIGN KEY ([ID_Pilot])
    REFERENCES [dbo].[t_Pilot]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_t_Pilot_t_Team'
CREATE INDEX [IX_FK_t_Pilot_t_Team]
ON [dbo].[t_Team]
    ([ID_Pilot]);
GO

-- Creating foreign key on [ID_Navigator] in table 't_Team'
ALTER TABLE [dbo].[t_Team]
ADD CONSTRAINT [FK_t_Pilot_t_Team1]
    FOREIGN KEY ([ID_Navigator])
    REFERENCES [dbo].[t_Pilot]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_t_Pilot_t_Team1'
CREATE INDEX [IX_FK_t_Pilot_t_Team1]
ON [dbo].[t_Team]
    ([ID_Navigator]);
GO

-- Creating foreign key on [ID_Team] in table 't_Competition_Team'
ALTER TABLE [dbo].[t_Competition_Team]
ADD CONSTRAINT [FK_t_Team_t_Competition_Team]
    FOREIGN KEY ([ID_Team])
    REFERENCES [dbo].[t_Team]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_t_Team_t_Competition_Team'
CREATE INDEX [IX_FK_t_Team_t_Competition_Team]
ON [dbo].[t_Competition_Team]
    ([ID_Team]);
GO

-- Creating foreign key on [ID_CompetitionSet] in table 't_Team'
ALTER TABLE [dbo].[t_Team]
ADD CONSTRAINT [FK_t_CompetitionSet_t_Team]
    FOREIGN KEY ([ID_CompetitionSet])
    REFERENCES [dbo].[t_CompetitionSet]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_t_CompetitionSet_t_Team'
CREATE INDEX [IX_FK_t_CompetitionSet_t_Team]
ON [dbo].[t_Team]
    ([ID_CompetitionSet]);
GO

-- Creating foreign key on [ID_Flag] in table 't_Team'
ALTER TABLE [dbo].[t_Team]
ADD CONSTRAINT [FK_t_Picture_t_Team]
    FOREIGN KEY ([ID_Flag])
    REFERENCES [dbo].[t_Picture]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_t_Picture_t_Team'
CREATE INDEX [IX_FK_t_Picture_t_Team]
ON [dbo].[t_Team]
    ([ID_Flag]);
GO

-- Creating foreign key on [t_Parcour_ID] in table 't_Line'
ALTER TABLE [dbo].[t_Line]
ADD CONSTRAINT [FK_t_Parcourt_Line]
    FOREIGN KEY ([t_Parcour_ID])
    REFERENCES [dbo].[t_Parcour]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_t_Parcourt_Line'
CREATE INDEX [IX_FK_t_Parcourt_Line]
ON [dbo].[t_Line]
    ([t_Parcour_ID]);
GO

-- Creating foreign key on [t_Competition_Team_ID] in table 't_Tracker'
ALTER TABLE [dbo].[t_Tracker]
ADD CONSTRAINT [FK_t_Competition_Teamt_Tracker]
    FOREIGN KEY ([t_Competition_Team_ID])
    REFERENCES [dbo].[t_Competition_Team]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_t_Competition_Teamt_Tracker'
CREATE INDEX [IX_FK_t_Competition_Teamt_Tracker]
ON [dbo].[t_Tracker]
    ([t_Competition_Team_ID]);
GO

-- Creating foreign key on [t_Tracker_ID] in table 't_GPSPoint'
ALTER TABLE [dbo].[t_GPSPoint]
ADD CONSTRAINT [FK_t_Trackert_GPSPoint]
    FOREIGN KEY ([t_Tracker_ID])
    REFERENCES [dbo].[t_Tracker]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_t_Trackert_GPSPoint'
CREATE INDEX [IX_FK_t_Trackert_GPSPoint]
ON [dbo].[t_GPSPoint]
    ([t_Tracker_ID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------