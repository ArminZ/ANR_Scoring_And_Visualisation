
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/16/2015 01:26:28
-- Generated from EDMX file: C:\workspace\VS2015\Anrl\anrl\AirNavigationRaceLive\AirNavigationRaceLive\AnrlModel2.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [anrl];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CompetitionSubscriber]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SubscriberSet] DROP CONSTRAINT [FK_CompetitionSubscriber];
GO
IF OBJECT_ID(N'[dbo].[FK_PictureSubscriber]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SubscriberSet] DROP CONSTRAINT [FK_PictureSubscriber];
GO
IF OBJECT_ID(N'[dbo].[FK_PictureMap]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MapSet] DROP CONSTRAINT [FK_PictureMap];
GO
IF OBJECT_ID(N'[dbo].[FK_CompetitionMap]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MapSet] DROP CONSTRAINT [FK_CompetitionMap];
GO
IF OBJECT_ID(N'[dbo].[FK_SubscriberTeam]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TeamSet] DROP CONSTRAINT [FK_SubscriberTeam];
GO
IF OBJECT_ID(N'[dbo].[FK_SubscriberTeam1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TeamSet] DROP CONSTRAINT [FK_SubscriberTeam1];
GO
IF OBJECT_ID(N'[dbo].[FK_CompetitionQualificationRound]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QualificationRoundSet] DROP CONSTRAINT [FK_CompetitionQualificationRound];
GO
IF OBJECT_ID(N'[dbo].[FK_PointLine]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LineSet] DROP CONSTRAINT [FK_PointLine];
GO
IF OBJECT_ID(N'[dbo].[FK_PointLine1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LineSet] DROP CONSTRAINT [FK_PointLine1];
GO
IF OBJECT_ID(N'[dbo].[FK_PointLine2]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LineSet] DROP CONSTRAINT [FK_PointLine2];
GO
IF OBJECT_ID(N'[dbo].[FK_LineQualificationRound]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QualificationRoundSet] DROP CONSTRAINT [FK_LineQualificationRound];
GO
IF OBJECT_ID(N'[dbo].[FK_MapParcour]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ParcourSet] DROP CONSTRAINT [FK_MapParcour];
GO
IF OBJECT_ID(N'[dbo].[FK_ParcourLine]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LineSet] DROP CONSTRAINT [FK_ParcourLine];
GO
IF OBJECT_ID(N'[dbo].[FK_ParcourQualificationRound]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QualificationRoundSet] DROP CONSTRAINT [FK_ParcourQualificationRound];
GO
IF OBJECT_ID(N'[dbo].[FK_QualificationRoundFlight]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FlightSet] DROP CONSTRAINT [FK_QualificationRoundFlight];
GO
IF OBJECT_ID(N'[dbo].[FK_TeamFlight]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FlightSet] DROP CONSTRAINT [FK_TeamFlight];
GO
IF OBJECT_ID(N'[dbo].[FK_FlightPenalty]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PenaltySet] DROP CONSTRAINT [FK_FlightPenalty];
GO
IF OBJECT_ID(N'[dbo].[FK_CompetitionTeam]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TeamSet] DROP CONSTRAINT [FK_CompetitionTeam];
GO
IF OBJECT_ID(N'[dbo].[FK_CompetitionParcour]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ParcourSet] DROP CONSTRAINT [FK_CompetitionParcour];
GO
IF OBJECT_ID(N'[dbo].[FK_FlightPoint]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PointSet] DROP CONSTRAINT [FK_FlightPoint];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[CompetitionSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CompetitionSet];
GO
IF OBJECT_ID(N'[dbo].[MapSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MapSet];
GO
IF OBJECT_ID(N'[dbo].[PictureSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PictureSet];
GO
IF OBJECT_ID(N'[dbo].[SubscriberSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SubscriberSet];
GO
IF OBJECT_ID(N'[dbo].[TeamSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TeamSet];
GO
IF OBJECT_ID(N'[dbo].[QualificationRoundSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QualificationRoundSet];
GO
IF OBJECT_ID(N'[dbo].[LineSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LineSet];
GO
IF OBJECT_ID(N'[dbo].[ParcourSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ParcourSet];
GO
IF OBJECT_ID(N'[dbo].[FlightSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FlightSet];
GO
IF OBJECT_ID(N'[dbo].[PenaltySet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PenaltySet];
GO
IF OBJECT_ID(N'[dbo].[PointSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PointSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'CompetitionSet'
CREATE TABLE [dbo].[CompetitionSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'MapSet'
CREATE TABLE [dbo].[MapSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [XSize] float  NOT NULL,
    [YSize] float  NOT NULL,
    [XRot] float  NOT NULL,
    [YRot] float  NOT NULL,
    [XTopLeft] float  NOT NULL,
    [YTopLeft] float  NOT NULL,
    [Picture_Id] int  NOT NULL,
    [Competition_Id] int  NOT NULL
);
GO

-- Creating table 'PictureSet'
CREATE TABLE [dbo].[PictureSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Data] varbinary(max)  NOT NULL
);
GO

-- Creating table 'SubscriberSet'
CREATE TABLE [dbo].[SubscriberSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [Competition_Id] int  NOT NULL,
    [Picture_Id] int  NULL
);
GO

-- Creating table 'TeamSet'
CREATE TABLE [dbo].[TeamSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CNumber] varchar(50)  NULL,
    [Color] varchar(10)  NULL,
    [Nationality] nvarchar(max)  NOT NULL,
    [AC] nvarchar(max)  NOT NULL,
    [Pilot_Id] int  NOT NULL,
    [Navigator_Id] int  NULL,
    [Competition_Id] int  NOT NULL
);
GO

-- Creating table 'QualificationRoundSet'
CREATE TABLE [dbo].[QualificationRoundSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(50)  NOT NULL,
    [Competition_Id] int  NOT NULL,
    [TakeOffLine_Id] int  NOT NULL,
    [Parcour_Id] int  NOT NULL
);
GO

-- Creating table 'LineSet'
CREATE TABLE [dbo].[LineSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] int  NOT NULL,
    [A_Id] int  NOT NULL,
    [B_Id] int  NOT NULL,
    [O_Id] int  NOT NULL,
    [ParcourLine_Line_Id] int  NULL
);
GO

-- Creating table 'ParcourSet'
CREATE TABLE [dbo].[ParcourSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(50)  NOT NULL,
    [Alpha] int  NOT NULL,
    [Map_Id] int  NOT NULL,
    [Competition_Id] int  NOT NULL
);
GO

-- Creating table 'FlightSet'
CREATE TABLE [dbo].[FlightSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Route] int  NOT NULL,
    [TimeTakeOff] bigint  NOT NULL,
    [TimeStartLine] bigint  NOT NULL,
    [TimeEndLine] bigint  NOT NULL,
    [StartID] int  NOT NULL,
    [QualificationRound_Id] int  NOT NULL,
    [Team_Id] int  NOT NULL
);
GO

-- Creating table 'PenaltySet'
CREATE TABLE [dbo].[PenaltySet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Points] int  NOT NULL,
    [Reason] varchar(500)  NOT NULL,
    [Flight_Id] int  NOT NULL
);
GO

-- Creating table 'PointSet'
CREATE TABLE [dbo].[PointSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [altitude] float  NOT NULL,
    [longitude] float  NOT NULL,
    [latitude] float  NOT NULL,
    [Timestamp] bigint  NOT NULL,
    [Flight_Id] int  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'CompetitionSet'
ALTER TABLE [dbo].[CompetitionSet]
ADD CONSTRAINT [PK_CompetitionSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MapSet'
ALTER TABLE [dbo].[MapSet]
ADD CONSTRAINT [PK_MapSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PictureSet'
ALTER TABLE [dbo].[PictureSet]
ADD CONSTRAINT [PK_PictureSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SubscriberSet'
ALTER TABLE [dbo].[SubscriberSet]
ADD CONSTRAINT [PK_SubscriberSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TeamSet'
ALTER TABLE [dbo].[TeamSet]
ADD CONSTRAINT [PK_TeamSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'QualificationRoundSet'
ALTER TABLE [dbo].[QualificationRoundSet]
ADD CONSTRAINT [PK_QualificationRoundSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LineSet'
ALTER TABLE [dbo].[LineSet]
ADD CONSTRAINT [PK_LineSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ParcourSet'
ALTER TABLE [dbo].[ParcourSet]
ADD CONSTRAINT [PK_ParcourSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'FlightSet'
ALTER TABLE [dbo].[FlightSet]
ADD CONSTRAINT [PK_FlightSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PenaltySet'
ALTER TABLE [dbo].[PenaltySet]
ADD CONSTRAINT [PK_PenaltySet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PointSet'
ALTER TABLE [dbo].[PointSet]
ADD CONSTRAINT [PK_PointSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Competition_Id] in table 'SubscriberSet'
ALTER TABLE [dbo].[SubscriberSet]
ADD CONSTRAINT [FK_CompetitionSubscriber]
    FOREIGN KEY ([Competition_Id])
    REFERENCES [dbo].[CompetitionSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CompetitionSubscriber'
CREATE INDEX [IX_FK_CompetitionSubscriber]
ON [dbo].[SubscriberSet]
    ([Competition_Id]);
GO

-- Creating foreign key on [Picture_Id] in table 'SubscriberSet'
ALTER TABLE [dbo].[SubscriberSet]
ADD CONSTRAINT [FK_PictureSubscriber]
    FOREIGN KEY ([Picture_Id])
    REFERENCES [dbo].[PictureSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PictureSubscriber'
CREATE INDEX [IX_FK_PictureSubscriber]
ON [dbo].[SubscriberSet]
    ([Picture_Id]);
GO

-- Creating foreign key on [Picture_Id] in table 'MapSet'
ALTER TABLE [dbo].[MapSet]
ADD CONSTRAINT [FK_PictureMap]
    FOREIGN KEY ([Picture_Id])
    REFERENCES [dbo].[PictureSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PictureMap'
CREATE INDEX [IX_FK_PictureMap]
ON [dbo].[MapSet]
    ([Picture_Id]);
GO

-- Creating foreign key on [Competition_Id] in table 'MapSet'
ALTER TABLE [dbo].[MapSet]
ADD CONSTRAINT [FK_CompetitionMap]
    FOREIGN KEY ([Competition_Id])
    REFERENCES [dbo].[CompetitionSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CompetitionMap'
CREATE INDEX [IX_FK_CompetitionMap]
ON [dbo].[MapSet]
    ([Competition_Id]);
GO

-- Creating foreign key on [Pilot_Id] in table 'TeamSet'
ALTER TABLE [dbo].[TeamSet]
ADD CONSTRAINT [FK_SubscriberTeam]
    FOREIGN KEY ([Pilot_Id])
    REFERENCES [dbo].[SubscriberSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SubscriberTeam'
CREATE INDEX [IX_FK_SubscriberTeam]
ON [dbo].[TeamSet]
    ([Pilot_Id]);
GO

-- Creating foreign key on [Navigator_Id] in table 'TeamSet'
ALTER TABLE [dbo].[TeamSet]
ADD CONSTRAINT [FK_SubscriberTeam1]
    FOREIGN KEY ([Navigator_Id])
    REFERENCES [dbo].[SubscriberSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SubscriberTeam1'
CREATE INDEX [IX_FK_SubscriberTeam1]
ON [dbo].[TeamSet]
    ([Navigator_Id]);
GO

-- Creating foreign key on [Competition_Id] in table 'QualificationRoundSet'
ALTER TABLE [dbo].[QualificationRoundSet]
ADD CONSTRAINT [FK_CompetitionQualificationRound]
    FOREIGN KEY ([Competition_Id])
    REFERENCES [dbo].[CompetitionSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CompetitionQualificationRound'
CREATE INDEX [IX_FK_CompetitionQualificationRound]
ON [dbo].[QualificationRoundSet]
    ([Competition_Id]);
GO

-- Creating foreign key on [A_Id] in table 'LineSet'
ALTER TABLE [dbo].[LineSet]
ADD CONSTRAINT [FK_PointLine]
    FOREIGN KEY ([A_Id])
    REFERENCES [dbo].[PointSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PointLine'
CREATE INDEX [IX_FK_PointLine]
ON [dbo].[LineSet]
    ([A_Id]);
GO

-- Creating foreign key on [B_Id] in table 'LineSet'
ALTER TABLE [dbo].[LineSet]
ADD CONSTRAINT [FK_PointLine1]
    FOREIGN KEY ([B_Id])
    REFERENCES [dbo].[PointSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PointLine1'
CREATE INDEX [IX_FK_PointLine1]
ON [dbo].[LineSet]
    ([B_Id]);
GO

-- Creating foreign key on [O_Id] in table 'LineSet'
ALTER TABLE [dbo].[LineSet]
ADD CONSTRAINT [FK_PointLine2]
    FOREIGN KEY ([O_Id])
    REFERENCES [dbo].[PointSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PointLine2'
CREATE INDEX [IX_FK_PointLine2]
ON [dbo].[LineSet]
    ([O_Id]);
GO

-- Creating foreign key on [TakeOffLine_Id] in table 'QualificationRoundSet'
ALTER TABLE [dbo].[QualificationRoundSet]
ADD CONSTRAINT [FK_LineQualificationRound]
    FOREIGN KEY ([TakeOffLine_Id])
    REFERENCES [dbo].[LineSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LineQualificationRound'
CREATE INDEX [IX_FK_LineQualificationRound]
ON [dbo].[QualificationRoundSet]
    ([TakeOffLine_Id]);
GO

-- Creating foreign key on [Map_Id] in table 'ParcourSet'
ALTER TABLE [dbo].[ParcourSet]
ADD CONSTRAINT [FK_MapParcour]
    FOREIGN KEY ([Map_Id])
    REFERENCES [dbo].[MapSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MapParcour'
CREATE INDEX [IX_FK_MapParcour]
ON [dbo].[ParcourSet]
    ([Map_Id]);
GO

-- Creating foreign key on [ParcourLine_Line_Id] in table 'LineSet'
ALTER TABLE [dbo].[LineSet]
ADD CONSTRAINT [FK_ParcourLine]
    FOREIGN KEY ([ParcourLine_Line_Id])
    REFERENCES [dbo].[ParcourSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ParcourLine'
CREATE INDEX [IX_FK_ParcourLine]
ON [dbo].[LineSet]
    ([ParcourLine_Line_Id]);
GO

-- Creating foreign key on [Parcour_Id] in table 'QualificationRoundSet'
ALTER TABLE [dbo].[QualificationRoundSet]
ADD CONSTRAINT [FK_ParcourQualificationRound]
    FOREIGN KEY ([Parcour_Id])
    REFERENCES [dbo].[ParcourSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ParcourQualificationRound'
CREATE INDEX [IX_FK_ParcourQualificationRound]
ON [dbo].[QualificationRoundSet]
    ([Parcour_Id]);
GO

-- Creating foreign key on [QualificationRound_Id] in table 'FlightSet'
ALTER TABLE [dbo].[FlightSet]
ADD CONSTRAINT [FK_QualificationRoundFlight]
    FOREIGN KEY ([QualificationRound_Id])
    REFERENCES [dbo].[QualificationRoundSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_QualificationRoundFlight'
CREATE INDEX [IX_FK_QualificationRoundFlight]
ON [dbo].[FlightSet]
    ([QualificationRound_Id]);
GO

-- Creating foreign key on [Team_Id] in table 'FlightSet'
ALTER TABLE [dbo].[FlightSet]
ADD CONSTRAINT [FK_TeamFlight]
    FOREIGN KEY ([Team_Id])
    REFERENCES [dbo].[TeamSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TeamFlight'
CREATE INDEX [IX_FK_TeamFlight]
ON [dbo].[FlightSet]
    ([Team_Id]);
GO

-- Creating foreign key on [Flight_Id] in table 'PenaltySet'
ALTER TABLE [dbo].[PenaltySet]
ADD CONSTRAINT [FK_FlightPenalty]
    FOREIGN KEY ([Flight_Id])
    REFERENCES [dbo].[FlightSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FlightPenalty'
CREATE INDEX [IX_FK_FlightPenalty]
ON [dbo].[PenaltySet]
    ([Flight_Id]);
GO

-- Creating foreign key on [Competition_Id] in table 'TeamSet'
ALTER TABLE [dbo].[TeamSet]
ADD CONSTRAINT [FK_CompetitionTeam]
    FOREIGN KEY ([Competition_Id])
    REFERENCES [dbo].[CompetitionSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CompetitionTeam'
CREATE INDEX [IX_FK_CompetitionTeam]
ON [dbo].[TeamSet]
    ([Competition_Id]);
GO

-- Creating foreign key on [Competition_Id] in table 'ParcourSet'
ALTER TABLE [dbo].[ParcourSet]
ADD CONSTRAINT [FK_CompetitionParcour]
    FOREIGN KEY ([Competition_Id])
    REFERENCES [dbo].[CompetitionSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CompetitionParcour'
CREATE INDEX [IX_FK_CompetitionParcour]
ON [dbo].[ParcourSet]
    ([Competition_Id]);
GO

-- Creating foreign key on [Flight_Id] in table 'PointSet'
ALTER TABLE [dbo].[PointSet]
ADD CONSTRAINT [FK_FlightPoint]
    FOREIGN KEY ([Flight_Id])
    REFERENCES [dbo].[FlightSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FlightPoint'
CREATE INDEX [IX_FK_FlightPoint]
ON [dbo].[PointSet]
    ([Flight_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------