USE [SoccerBase]
GO
/****** Object:  Table [dbo].[SportRadar]    Script Date: 09/09/2013 12:42:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SportRadar](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Country] [nvarchar](60) NOT NULL,
	[League] [nvarchar](60) NOT NULL,
	[Season] [nvarchar](10) NOT NULL,
	[Round] [tinyint] NOT NULL,
	[Date] [date] NOT NULL,
	[Time] [time](7) NULL,
	[HomeTeam] [nvarchar](50) NOT NULL,
	[AwayTeam] [nvarchar](50) NOT NULL,
	[HomeOdds] [float] NOT NULL,
	[DrawOdds] [float] NOT NULL,
	[AwayOdds] [float] NOT NULL,
	[ScoreH] [tinyint] NULL,
	[ScoreA] [tinyint] NULL,
 CONSTRAINT [PK_SportRadar] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Leagues]    Script Date: 09/09/2013 12:42:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Leagues](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Country] [nvarchar](60) NOT NULL,
	[League] [nvarchar](60) NOT NULL,
	[Season] [nvarchar](10) NOT NULL,
	[StartDate] [date] NULL,
	[EndDate] [date] NULL,
	[Teams] [tinyint] NOT NULL,
	[Matches] [int] NOT NULL,
	[Guessed] [int] NULL,
	[GuessedPercent] [tinyint] NULL,
	[HomeWinsPercent] [tinyint] NULL,
	[DrawsPercent] [tinyint] NULL,
	[AwayWinsPercent] [tinyint] NULL,
	[PyramidLevel] [tinyint] NOT NULL,
 CONSTRAINT [PK_Leagues] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ivnet]    Script Date: 09/09/2013 12:42:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ivnet](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Country] [nvarchar](60) NOT NULL,
	[League] [nvarchar](60) NOT NULL,
	[Season] [nvarchar](10) NOT NULL,
	[Round] [tinyint] NOT NULL,
	[Date] [nvarchar](10) NOT NULL,
	[Time] [time](0) NULL,
	[HomeTeam] [nvarchar](50) NOT NULL,
	[AwayTeam] [nvarchar](50) NOT NULL,
	[ForecastH] [tinyint] NOT NULL,
	[ForecastD] [tinyint] NOT NULL,
	[ForecastA] [tinyint] NOT NULL,
	[TipGoalsH] [tinyint] NULL,
	[TipGoalsA] [tinyint] NULL,
	[ScoreH] [tinyint] NULL,
	[ScoreA] [tinyint] NULL,
	[Guessed] [bit] NULL,
	[SportRadarID] [bigint] NULL,
 CONSTRAINT [PK_Matches] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[history]    Script Date: 09/09/2013 12:42:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[history](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Country] [nvarchar](60) NOT NULL,
	[League] [nvarchar](60) NOT NULL,
	[Season] [nvarchar](10) NOT NULL,
	[Round] [tinyint] NOT NULL,
	[Date] [date] NOT NULL,
	[Time] [time](7) NULL,
	[Field] [nvarchar](10) NOT NULL,
	[Team] [nvarchar](50) NOT NULL,
	[ScoredH] [tinyint] NOT NULL,
	[ReceivedH] [tinyint] NOT NULL,
	[ScoredA] [tinyint] NOT NULL,
	[ReceivedA] [tinyint] NOT NULL,
	[Scored] [tinyint] NOT NULL,
	[Received] [tinyint] NOT NULL,
	[Points] [tinyint] NOT NULL,
	[Position] [tinyint] NULL,
	[grLevel] [tinyint] NULL,
	[grStrength] [tinyint] NULL,
	[OtherTeam] [nvarchar](50) NOT NULL,
	[PrevAvgStrength] [tinyint] NULL,
 CONSTRAINT [PK_history] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[archive]    Script Date: 09/09/2013 12:42:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[archive](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Country] [nvarchar](60) NOT NULL,
	[League] [nvarchar](60) NOT NULL,
	[Season] [nvarchar](10) NOT NULL,
	[Round] [tinyint] NOT NULL,
	[Date] [date] NOT NULL,
	[Time] [time](7) NULL,
	[HomeTeam] [nvarchar](50) NOT NULL,
	[AwayTeam] [nvarchar](50) NOT NULL,
	[HomeOdds] [float] NOT NULL,
	[DrawOdds] [float] NOT NULL,
	[AwayOdds] [float] NOT NULL,
	[ScoreH] [tinyint] NULL,
	[ScoreA] [tinyint] NULL,
 CONSTRAINT [PK_archive] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
