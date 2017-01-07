
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/08/2016 21:40:20
-- Generated from EDMX file: C:\Users\Gabriel Rogala\Dropbox\INŻ\SchoolTimetableGenerator\STG website\Models\STGModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [aspnet-STG website-20161108065420];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserClaims] DROP CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserRoles_dbo_AspNetRoles_RoleId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo_AspNetUserRoles_dbo_AspNetRoles_RoleId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserRoles_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo_AspNetUserRoles_dbo_AspNetUsers_UserId];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[__MigrationHistory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[__MigrationHistory];
GO
IF OBJECT_ID(N'[dbo].[AspNetRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetRoles];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserClaims]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserClaims];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserLogins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserLogins];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserRoles];
GO
IF OBJECT_ID(N'[dbo].[AspNetUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUsers];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'C__MigrationHistory'
CREATE TABLE [dbo].[C__MigrationHistory] (
    [MigrationId] nvarchar(150)  NOT NULL,
    [ContextKey] nvarchar(300)  NOT NULL,
    [Model] varbinary(max)  NOT NULL,
    [ProductVersion] nvarchar(32)  NOT NULL
);
GO

-- Creating table 'AspNetRoles'
CREATE TABLE [dbo].[AspNetRoles] (
    [Id] nvarchar(128)  NOT NULL,
    [Name] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'AspNetUserClaims'
CREATE TABLE [dbo].[AspNetUserClaims] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(128)  NOT NULL,
    [ClaimType] nvarchar(max)  NULL,
    [ClaimValue] nvarchar(max)  NULL
);
GO

-- Creating table 'AspNetUserLogins'
CREATE TABLE [dbo].[AspNetUserLogins] (
    [LoginProvider] nvarchar(128)  NOT NULL,
    [ProviderKey] nvarchar(128)  NOT NULL,
    [UserId] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'AspNetUserRoles'
CREATE TABLE [dbo].[AspNetUserRoles] (
    [UserId] nvarchar(128)  NOT NULL,
    [RoleId] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'AspNetUsers'
CREATE TABLE [dbo].[AspNetUsers] (
    [Id] nvarchar(128)  NOT NULL,
    [Email] nvarchar(256)  NULL,
    [EmailConfirmed] bit  NOT NULL,
    [PasswordHash] nvarchar(max)  NULL,
    [SecurityStamp] nvarchar(max)  NULL,
    [PhoneNumber] nvarchar(max)  NULL,
    [PhoneNumberConfirmed] bit  NOT NULL,
    [TwoFactorEnabled] bit  NOT NULL,
    [LockoutEndDateUtc] datetime  NULL,
    [LockoutEnabled] bit  NOT NULL,
    [AccessFailedCount] int  NOT NULL,
    [UserName] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'Schools'
CREATE TABLE [dbo].[Schools] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [City] nvarchar(max)  NOT NULL,
    [Adress] nvarchar(max)  NOT NULL,
    [AspNetUsersId] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'Teachers'
CREATE TABLE [dbo].[Teachers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Lastname] nvarchar(max)  NOT NULL,
    [AspNetUsersId] nvarchar(128)  NULL
);
GO

-- Creating table 'Subjects'
CREATE TABLE [dbo].[Subjects] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [ShortName] nvarchar(max)  NOT NULL,
    [SchoolsId] int  NOT NULL
);
GO

-- Creating table 'Groups'
CREATE TABLE [dbo].[Groups] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [ShortName] nvarchar(max)  NOT NULL,
    [Amount] nvarchar(max)  NOT NULL,
    [SchoolsId] int  NOT NULL
);
GO

-- Creating table 'Lessons'
CREATE TABLE [dbo].[Lessons] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SubjectsId] int  NOT NULL,
    [TeachersId] int  NOT NULL,
    [Schedule] nvarchar(max)  NOT NULL,
    [SchoolsId] int  NOT NULL,
    [RoomTypeId] int  NOT NULL,
    [SubGroupsId] int  NOT NULL
);
GO

-- Creating table 'TeachersSchoolsSet'
CREATE TABLE [dbo].[TeachersSchoolsSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TeachersId] int  NOT NULL,
    [SchoolsId] int  NOT NULL
);
GO

-- Creating table 'Rooms'
CREATE TABLE [dbo].[Rooms] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Number] nvarchar(max)  NOT NULL,
    [Amount] nvarchar(max)  NOT NULL,
    [SchoolsId] int  NOT NULL,
    [RoomTypeId] int  NOT NULL
);
GO

-- Creating table 'RoomTypes'
CREATE TABLE [dbo].[RoomTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [SchoolsId] int  NOT NULL
);
GO

-- Creating table 'SubGroups'
CREATE TABLE [dbo].[SubGroups] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [GroupsId] int  NOT NULL,
    [SchoolsId] int  NOT NULL,
    [Number] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Timetable'
CREATE TABLE [dbo].[Timetable] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Day] smallint  NOT NULL,
    [Hour] smallint  NOT NULL,
    [SchoolsId] int  NOT NULL,
    [RoomsId] int  NOT NULL,
    [GroupsId] int  NOT NULL,
    [TeachersId] int  NOT NULL,
    [LessonsId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [MigrationId], [ContextKey] in table 'C__MigrationHistory'
ALTER TABLE [dbo].[C__MigrationHistory]
ADD CONSTRAINT [PK_C__MigrationHistory]
    PRIMARY KEY CLUSTERED ([MigrationId], [ContextKey] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetRoles'
ALTER TABLE [dbo].[AspNetRoles]
ADD CONSTRAINT [PK_AspNetRoles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [PK_AspNetUserClaims]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [LoginProvider], [ProviderKey], [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [PK_AspNetUserLogins]
    PRIMARY KEY CLUSTERED ([LoginProvider], [ProviderKey], [UserId] ASC);
GO

-- Creating primary key on [UserId], [RoleId] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [PK_AspNetUserRoles]
    PRIMARY KEY CLUSTERED ([UserId], [RoleId] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUsers'
ALTER TABLE [dbo].[AspNetUsers]
ADD CONSTRAINT [PK_AspNetUsers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Schools'
ALTER TABLE [dbo].[Schools]
ADD CONSTRAINT [PK_Schools]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Teachers'
ALTER TABLE [dbo].[Teachers]
ADD CONSTRAINT [PK_Teachers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Subjects'
ALTER TABLE [dbo].[Subjects]
ADD CONSTRAINT [PK_Subjects]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Groups'
ALTER TABLE [dbo].[Groups]
ADD CONSTRAINT [PK_Groups]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Lessons'
ALTER TABLE [dbo].[Lessons]
ADD CONSTRAINT [PK_Lessons]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TeachersSchoolsSet'
ALTER TABLE [dbo].[TeachersSchoolsSet]
ADD CONSTRAINT [PK_TeachersSchoolsSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Rooms'
ALTER TABLE [dbo].[Rooms]
ADD CONSTRAINT [PK_Rooms]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RoomTypes'
ALTER TABLE [dbo].[RoomTypes]
ADD CONSTRAINT [PK_RoomTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SubGroups'
ALTER TABLE [dbo].[SubGroups]
ADD CONSTRAINT [PK_SubGroups]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Timetable'
ALTER TABLE [dbo].[Timetable]
ADD CONSTRAINT [PK_Timetable]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [RoleId] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [FK_dbo_AspNetUserRoles_dbo_AspNetRoles_RoleId]
    FOREIGN KEY ([RoleId])
    REFERENCES [dbo].[AspNetRoles]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserRoles_dbo_AspNetRoles_RoleId'
CREATE INDEX [IX_FK_dbo_AspNetUserRoles_dbo_AspNetRoles_RoleId]
ON [dbo].[AspNetUserRoles]
    ([RoleId]);
GO

-- Creating foreign key on [UserId] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserClaims]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserLogins]
    ([UserId]);
GO

-- Creating foreign key on [AspNetUsersId] in table 'Schools'
ALTER TABLE [dbo].[Schools]
ADD CONSTRAINT [FK_AspNetUsersSchool]
    FOREIGN KEY ([AspNetUsersId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AspNetUsersSchool'
CREATE INDEX [IX_FK_AspNetUsersSchool]
ON [dbo].[Schools]
    ([AspNetUsersId]);
GO

-- Creating foreign key on [AspNetUsersId] in table 'Teachers'
ALTER TABLE [dbo].[Teachers]
ADD CONSTRAINT [FK_AspNetUsersTeacher]
    FOREIGN KEY ([AspNetUsersId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AspNetUsersTeacher'
CREATE INDEX [IX_FK_AspNetUsersTeacher]
ON [dbo].[Teachers]
    ([AspNetUsersId]);
GO

-- Creating foreign key on [SubjectsId] in table 'Lessons'
ALTER TABLE [dbo].[Lessons]
ADD CONSTRAINT [FK_SubjectsLessons]
    FOREIGN KEY ([SubjectsId])
    REFERENCES [dbo].[Subjects]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SubjectsLessons'
CREATE INDEX [IX_FK_SubjectsLessons]
ON [dbo].[Lessons]
    ([SubjectsId]);
GO

-- Creating foreign key on [TeachersId] in table 'Lessons'
ALTER TABLE [dbo].[Lessons]
ADD CONSTRAINT [FK_TeachersLessons]
    FOREIGN KEY ([TeachersId])
    REFERENCES [dbo].[Teachers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TeachersLessons'
CREATE INDEX [IX_FK_TeachersLessons]
ON [dbo].[Lessons]
    ([TeachersId]);
GO

-- Creating foreign key on [TeachersId] in table 'TeachersSchoolsSet'
ALTER TABLE [dbo].[TeachersSchoolsSet]
ADD CONSTRAINT [FK_TeachersTeachersSchools]
    FOREIGN KEY ([TeachersId])
    REFERENCES [dbo].[Teachers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TeachersTeachersSchools'
CREATE INDEX [IX_FK_TeachersTeachersSchools]
ON [dbo].[TeachersSchoolsSet]
    ([TeachersId]);
GO

-- Creating foreign key on [SchoolsId] in table 'TeachersSchoolsSet'
ALTER TABLE [dbo].[TeachersSchoolsSet]
ADD CONSTRAINT [FK_SchoolsTeachersSchools]
    FOREIGN KEY ([SchoolsId])
    REFERENCES [dbo].[Schools]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SchoolsTeachersSchools'
CREATE INDEX [IX_FK_SchoolsTeachersSchools]
ON [dbo].[TeachersSchoolsSet]
    ([SchoolsId]);
GO

-- Creating foreign key on [SchoolsId] in table 'Groups'
ALTER TABLE [dbo].[Groups]
ADD CONSTRAINT [FK_SchoolsGroups]
    FOREIGN KEY ([SchoolsId])
    REFERENCES [dbo].[Schools]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SchoolsGroups'
CREATE INDEX [IX_FK_SchoolsGroups]
ON [dbo].[Groups]
    ([SchoolsId]);
GO

-- Creating foreign key on [SchoolsId] in table 'Subjects'
ALTER TABLE [dbo].[Subjects]
ADD CONSTRAINT [FK_SchoolsSubjects]
    FOREIGN KEY ([SchoolsId])
    REFERENCES [dbo].[Schools]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SchoolsSubjects'
CREATE INDEX [IX_FK_SchoolsSubjects]
ON [dbo].[Subjects]
    ([SchoolsId]);
GO

-- Creating foreign key on [SchoolsId] in table 'Lessons'
ALTER TABLE [dbo].[Lessons]
ADD CONSTRAINT [FK_SchoolsLessons]
    FOREIGN KEY ([SchoolsId])
    REFERENCES [dbo].[Schools]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SchoolsLessons'
CREATE INDEX [IX_FK_SchoolsLessons]
ON [dbo].[Lessons]
    ([SchoolsId]);
GO

-- Creating foreign key on [SchoolsId] in table 'Rooms'
ALTER TABLE [dbo].[Rooms]
ADD CONSTRAINT [FK_SchoolsRooms]
    FOREIGN KEY ([SchoolsId])
    REFERENCES [dbo].[Schools]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SchoolsRooms'
CREATE INDEX [IX_FK_SchoolsRooms]
ON [dbo].[Rooms]
    ([SchoolsId]);
GO

-- Creating foreign key on [SchoolsId] in table 'RoomTypes'
ALTER TABLE [dbo].[RoomTypes]
ADD CONSTRAINT [FK_SchoolsRoomType]
    FOREIGN KEY ([SchoolsId])
    REFERENCES [dbo].[Schools]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SchoolsRoomType'
CREATE INDEX [IX_FK_SchoolsRoomType]
ON [dbo].[RoomTypes]
    ([SchoolsId]);
GO

-- Creating foreign key on [RoomTypeId] in table 'Rooms'
ALTER TABLE [dbo].[Rooms]
ADD CONSTRAINT [FK_RoomTypeRooms]
    FOREIGN KEY ([RoomTypeId])
    REFERENCES [dbo].[RoomTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoomTypeRooms'
CREATE INDEX [IX_FK_RoomTypeRooms]
ON [dbo].[Rooms]
    ([RoomTypeId]);
GO

-- Creating foreign key on [RoomTypeId] in table 'Lessons'
ALTER TABLE [dbo].[Lessons]
ADD CONSTRAINT [FK_RoomTypeLessons]
    FOREIGN KEY ([RoomTypeId])
    REFERENCES [dbo].[RoomTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoomTypeLessons'
CREATE INDEX [IX_FK_RoomTypeLessons]
ON [dbo].[Lessons]
    ([RoomTypeId]);
GO

-- Creating foreign key on [GroupsId] in table 'SubGroups'
ALTER TABLE [dbo].[SubGroups]
ADD CONSTRAINT [FK_GroupsSubGroups]
    FOREIGN KEY ([GroupsId])
    REFERENCES [dbo].[Groups]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GroupsSubGroups'
CREATE INDEX [IX_FK_GroupsSubGroups]
ON [dbo].[SubGroups]
    ([GroupsId]);
GO

-- Creating foreign key on [SchoolsId] in table 'SubGroups'
ALTER TABLE [dbo].[SubGroups]
ADD CONSTRAINT [FK_SchoolsSubGroups]
    FOREIGN KEY ([SchoolsId])
    REFERENCES [dbo].[Schools]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SchoolsSubGroups'
CREATE INDEX [IX_FK_SchoolsSubGroups]
ON [dbo].[SubGroups]
    ([SchoolsId]);
GO

-- Creating foreign key on [SubGroupsId] in table 'Lessons'
ALTER TABLE [dbo].[Lessons]
ADD CONSTRAINT [FK_SubGroupsLessons]
    FOREIGN KEY ([SubGroupsId])
    REFERENCES [dbo].[SubGroups]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SubGroupsLessons'
CREATE INDEX [IX_FK_SubGroupsLessons]
ON [dbo].[Lessons]
    ([SubGroupsId]);
GO

-- Creating foreign key on [SchoolsId] in table 'Timetable'
ALTER TABLE [dbo].[Timetable]
ADD CONSTRAINT [FK_SchoolsTimetable]
    FOREIGN KEY ([SchoolsId])
    REFERENCES [dbo].[Schools]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SchoolsTimetable'
CREATE INDEX [IX_FK_SchoolsTimetable]
ON [dbo].[Timetable]
    ([SchoolsId]);
GO

-- Creating foreign key on [RoomsId] in table 'Timetable'
ALTER TABLE [dbo].[Timetable]
ADD CONSTRAINT [FK_RoomsTimetable]
    FOREIGN KEY ([RoomsId])
    REFERENCES [dbo].[Rooms]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoomsTimetable'
CREATE INDEX [IX_FK_RoomsTimetable]
ON [dbo].[Timetable]
    ([RoomsId]);
GO

-- Creating foreign key on [GroupsId] in table 'Timetable'
ALTER TABLE [dbo].[Timetable]
ADD CONSTRAINT [FK_GroupsTimetable]
    FOREIGN KEY ([GroupsId])
    REFERENCES [dbo].[Groups]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GroupsTimetable'
CREATE INDEX [IX_FK_GroupsTimetable]
ON [dbo].[Timetable]
    ([GroupsId]);
GO

-- Creating foreign key on [TeachersId] in table 'Timetable'
ALTER TABLE [dbo].[Timetable]
ADD CONSTRAINT [FK_TeachersTimetable]
    FOREIGN KEY ([TeachersId])
    REFERENCES [dbo].[Teachers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TeachersTimetable'
CREATE INDEX [IX_FK_TeachersTimetable]
ON [dbo].[Timetable]
    ([TeachersId]);
GO

-- Creating foreign key on [LessonsId] in table 'Timetable'
ALTER TABLE [dbo].[Timetable]
ADD CONSTRAINT [FK_LessonsTimetable]
    FOREIGN KEY ([LessonsId])
    REFERENCES [dbo].[Lessons]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LessonsTimetable'
CREATE INDEX [IX_FK_LessonsTimetable]
ON [dbo].[Timetable]
    ([LessonsId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------