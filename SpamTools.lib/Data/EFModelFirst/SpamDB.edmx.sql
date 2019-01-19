
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/19/2019 17:42:37
-- Generated from EDMX file: C:\Users\shmac\src\SpamTools\SpamTools.lib\Data\EFModelFirst\SpamDB.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SpamDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_SchedulerTsaskEmailList]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmailList] DROP CONSTRAINT [FK_SchedulerTsaskEmailList];
GO
IF OBJECT_ID(N'[dbo].[FK_EmailListEmail_EmailList]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmailListEmail] DROP CONSTRAINT [FK_EmailListEmail_EmailList];
GO
IF OBJECT_ID(N'[dbo].[FK_EmailListEmail_Email]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmailListEmail] DROP CONSTRAINT [FK_EmailListEmail_Email];
GO
IF OBJECT_ID(N'[dbo].[FK_SchedulerTsaskServer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SchedulerTsaskSet] DROP CONSTRAINT [FK_SchedulerTsaskServer];
GO
IF OBJECT_ID(N'[dbo].[FK_EmailRecipientEmailList_EmailRecipient]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmailRecipientEmailList] DROP CONSTRAINT [FK_EmailRecipientEmailList_EmailRecipient];
GO
IF OBJECT_ID(N'[dbo].[FK_EmailRecipientEmailList_EmailList]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmailRecipientEmailList] DROP CONSTRAINT [FK_EmailRecipientEmailList_EmailList];
GO
IF OBJECT_ID(N'[dbo].[FK_EmailSenderSchedulerTsask]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SchedulerTsaskSet] DROP CONSTRAINT [FK_EmailSenderSchedulerTsask];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[EmailRecipients]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmailRecipients];
GO
IF OBJECT_ID(N'[dbo].[EmailSenders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmailSenders];
GO
IF OBJECT_ID(N'[dbo].[Servers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Servers];
GO
IF OBJECT_ID(N'[dbo].[EmailSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmailSet];
GO
IF OBJECT_ID(N'[dbo].[SchedulerTsaskSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SchedulerTsaskSet];
GO
IF OBJECT_ID(N'[dbo].[EmailList]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmailList];
GO
IF OBJECT_ID(N'[dbo].[EmailListEmail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmailListEmail];
GO
IF OBJECT_ID(N'[dbo].[EmailRecipientEmailList]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmailRecipientEmailList];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'EmailRecipients'
CREATE TABLE [dbo].[EmailRecipients] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(200)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'EmailSenders'
CREATE TABLE [dbo].[EmailSenders] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Login] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Servers'
CREATE TABLE [dbo].[Servers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Address] nvarchar(max)  NOT NULL,
    [Port] int  NOT NULL,
    [UseSSL] bit  NOT NULL
);
GO

-- Creating table 'EmailSet'
CREATE TABLE [dbo].[EmailSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Subject] nvarchar(max)  NOT NULL,
    [Body] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'SchedulerTsaskSet'
CREATE TABLE [dbo].[SchedulerTsaskSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Time] datetime  NOT NULL,
    [Name] nvarchar(max)  NULL,
    [Server_Id] int  NOT NULL,
    [EmailSender_Id] int  NOT NULL
);
GO

-- Creating table 'EmailList'
CREATE TABLE [dbo].[EmailList] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NULL,
    [Description] nvarchar(max)  NULL,
    [SchedulerTsask_Id] int  NOT NULL
);
GO

-- Creating table 'EmailListEmail'
CREATE TABLE [dbo].[EmailListEmail] (
    [EmailList_Id] int  NOT NULL,
    [Email_Id] int  NOT NULL
);
GO

-- Creating table 'EmailRecipientEmailList'
CREATE TABLE [dbo].[EmailRecipientEmailList] (
    [EmailRecipient_Id] int  NOT NULL,
    [EmailList_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'EmailRecipients'
ALTER TABLE [dbo].[EmailRecipients]
ADD CONSTRAINT [PK_EmailRecipients]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EmailSenders'
ALTER TABLE [dbo].[EmailSenders]
ADD CONSTRAINT [PK_EmailSenders]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Servers'
ALTER TABLE [dbo].[Servers]
ADD CONSTRAINT [PK_Servers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EmailSet'
ALTER TABLE [dbo].[EmailSet]
ADD CONSTRAINT [PK_EmailSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SchedulerTsaskSet'
ALTER TABLE [dbo].[SchedulerTsaskSet]
ADD CONSTRAINT [PK_SchedulerTsaskSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EmailList'
ALTER TABLE [dbo].[EmailList]
ADD CONSTRAINT [PK_EmailList]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [EmailList_Id], [Email_Id] in table 'EmailListEmail'
ALTER TABLE [dbo].[EmailListEmail]
ADD CONSTRAINT [PK_EmailListEmail]
    PRIMARY KEY CLUSTERED ([EmailList_Id], [Email_Id] ASC);
GO

-- Creating primary key on [EmailRecipient_Id], [EmailList_Id] in table 'EmailRecipientEmailList'
ALTER TABLE [dbo].[EmailRecipientEmailList]
ADD CONSTRAINT [PK_EmailRecipientEmailList]
    PRIMARY KEY CLUSTERED ([EmailRecipient_Id], [EmailList_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [SchedulerTsask_Id] in table 'EmailList'
ALTER TABLE [dbo].[EmailList]
ADD CONSTRAINT [FK_SchedulerTsaskEmailList]
    FOREIGN KEY ([SchedulerTsask_Id])
    REFERENCES [dbo].[SchedulerTsaskSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SchedulerTsaskEmailList'
CREATE INDEX [IX_FK_SchedulerTsaskEmailList]
ON [dbo].[EmailList]
    ([SchedulerTsask_Id]);
GO

-- Creating foreign key on [EmailList_Id] in table 'EmailListEmail'
ALTER TABLE [dbo].[EmailListEmail]
ADD CONSTRAINT [FK_EmailListEmail_EmailList]
    FOREIGN KEY ([EmailList_Id])
    REFERENCES [dbo].[EmailList]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Email_Id] in table 'EmailListEmail'
ALTER TABLE [dbo].[EmailListEmail]
ADD CONSTRAINT [FK_EmailListEmail_Email]
    FOREIGN KEY ([Email_Id])
    REFERENCES [dbo].[EmailSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmailListEmail_Email'
CREATE INDEX [IX_FK_EmailListEmail_Email]
ON [dbo].[EmailListEmail]
    ([Email_Id]);
GO

-- Creating foreign key on [Server_Id] in table 'SchedulerTsaskSet'
ALTER TABLE [dbo].[SchedulerTsaskSet]
ADD CONSTRAINT [FK_SchedulerTsaskServer]
    FOREIGN KEY ([Server_Id])
    REFERENCES [dbo].[Servers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SchedulerTsaskServer'
CREATE INDEX [IX_FK_SchedulerTsaskServer]
ON [dbo].[SchedulerTsaskSet]
    ([Server_Id]);
GO

-- Creating foreign key on [EmailRecipient_Id] in table 'EmailRecipientEmailList'
ALTER TABLE [dbo].[EmailRecipientEmailList]
ADD CONSTRAINT [FK_EmailRecipientEmailList_EmailRecipient]
    FOREIGN KEY ([EmailRecipient_Id])
    REFERENCES [dbo].[EmailRecipients]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [EmailList_Id] in table 'EmailRecipientEmailList'
ALTER TABLE [dbo].[EmailRecipientEmailList]
ADD CONSTRAINT [FK_EmailRecipientEmailList_EmailList]
    FOREIGN KEY ([EmailList_Id])
    REFERENCES [dbo].[EmailList]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmailRecipientEmailList_EmailList'
CREATE INDEX [IX_FK_EmailRecipientEmailList_EmailList]
ON [dbo].[EmailRecipientEmailList]
    ([EmailList_Id]);
GO

-- Creating foreign key on [EmailSender_Id] in table 'SchedulerTsaskSet'
ALTER TABLE [dbo].[SchedulerTsaskSet]
ADD CONSTRAINT [FK_EmailSenderSchedulerTsask]
    FOREIGN KEY ([EmailSender_Id])
    REFERENCES [dbo].[EmailSenders]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmailSenderSchedulerTsask'
CREATE INDEX [IX_FK_EmailSenderSchedulerTsask]
ON [dbo].[SchedulerTsaskSet]
    ([EmailSender_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------