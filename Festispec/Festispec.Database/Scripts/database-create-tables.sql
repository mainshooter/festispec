USE [festispec]
GO
/****** Object:  Table [dbo].[Answer]    Script Date: 5-11-2019 21:03:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Answer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[QuestionId] [int] NOT NULL,
	[CaseId] [int] NOT NULL,
	[Answer] [text] NOT NULL,
 CONSTRAINT [PK_Answer] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AvailabilityInspector]    Script Date: 5-11-2019 21:03:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AvailabilityInspector](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[AvailableFrom] [datetime] NOT NULL,
	[AvailableTill] [datetime] NOT NULL,
 CONSTRAINT [PK_AvailabilityInspector] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BetterReportInspector]    Script Date: 5-11-2019 21:03:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BetterReportInspector](
	[DateTime] [datetime] NOT NULL,
	[EmployeeId] [int] NOT NULL,
 CONSTRAINT [PK_BetterReportInspector] PRIMARY KEY CLUSTERED
(
	[DateTime] ASC,
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Case]    Script Date: 5-11-2019 21:03:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Case](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SurveyId] [int] NOT NULL,
	[EmployeeId] [int] NULL,
	[Status] [nvarchar](45) NOT NULL,
 CONSTRAINT [PK_Case] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CaseStatus]    Script Date: 5-11-2019 21:03:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CaseStatus](
	[Status] [nvarchar](45) NOT NULL,
 CONSTRAINT [PK_CaseStatus] PRIMARY KEY CLUSTERED
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CertificateInspector]    Script Date: 5-11-2019 21:03:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CertificateInspector](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[DateFrom] [datetime] NOT NULL,
	[DateTill] [datetime] NOT NULL,
 CONSTRAINT [PK_CertificateInspector] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ContactPerson]    Script Date: 5-11-2019 21:03:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContactPerson](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Firstname] [nvarchar](45) NOT NULL,
	[Prefix] [nvarchar](45) NULL,
	[Lastname] [nvarchar](45) NOT NULL,
	[Phone] [nvarchar](15) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Function] [nvarchar](45) NOT NULL,
	[CustomerId] [int] NOT NULL,
 CONSTRAINT [PK_ContactPerson] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 5-11-2019 21:03:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[COC] [int] NOT NULL,
	[BranchNumber] [int] NOT NULL,
	[Street] [nvarchar](100) NOT NULL,
	[HouseNumber] [int] NOT NULL,
	[HouseNumber Addition] [VARCHAR](5) NULL,
	[PostalCode] [nvarchar](6) NOT NULL,
	[City] [nvarchar](45) NOT NULL,
	[Phone] [nvarchar](15) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Website] [nvarchar](100) NOT NULL,
	[Logo] [text] NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Day]    Script Date: 5-11-2019 21:03:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Day](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[BeginTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Day] PRIMARY KEY CLUSTERED
(
	[Id] ASC,
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 5-11-2019 21:03:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[Name] [nvarchar](50) NOT NULL,
	[Description] [text] NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ElementType]    Script Date: 5-11-2019 21:03:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ElementType](
	[Type] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_ElementType] PRIMARY KEY CLUSTERED
(
	[Type] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 5-11-2019 21:03:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Department] [nvarchar](50) NOT NULL,
	[Status] [nvarchar](45) NOT NULL,
	[Firstname] [nvarchar](45) NOT NULL,
	[Prefix] [nvarchar](45) NULL,
	[Lastname] [nvarchar](45) NOT NULL,
	[Birthday] [datetime] NOT NULL,
	[Street] [nvarchar](100) NOT NULL,
	[HouseNumber] [int] NOT NULL,
	[HouseNumber Addition] [VARCHAR](5) NULL,
	[PostalCode] [nvarchar](6) NOT NULL,
	[City] [nvarchar](45) NOT NULL,
	[Phone] [nvarchar](15) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](255) NOT NULL,
	[PasswordResetToken] [nvarchar](255) NULL,
	[ResetTokenEndTime] [datetime] NULL,
	[Iban] [nvarchar](27) NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeStatus]    Script Date: 5-11-2019 21:03:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeStatus](
	[Status] [nvarchar](45) NOT NULL,
 CONSTRAINT [PK_EmployeeStatus] PRIMARY KEY CLUSTERED
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Event]    Script Date: 5-11-2019 21:03:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Event](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[ContactPersonId] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Street] [NVARCHAR](100) NOT NULL,
	[HouseNumber] [INT] NOT NULL,
	[HouseNumber Addition] [NVARCHAR](5) NULL,
	[PostalCode] [NVARCHAR](6) NOT NULL,
	[City] [NVARCHAR](45) NOT NULL,
	[BeginDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[AmountVisitors] [int] NOT NULL,
	[SurfaceM2] [int] NOT NULL,
	[Description] [text] NULL,
 CONSTRAINT [PK_Event] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InspectorPlanning]    Script Date: 5-11-2019 21:03:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InspectorPlanning](
	[EmployeeId] [int] NOT NULL,
	[DayId] [int] NOT NULL,
	[OrderId] [int] NOT NULL,
	[PlannedFrom] [datetime] NOT NULL,
	[PlannedTill] [datetime] NOT NULL,
	[Status] [nvarchar](45) NOT NULL,
	[WorkedFrom] [datetime] NULL,
	[WorkedTill] [datetime] NULL,
 CONSTRAINT [PK_InspectorPlanning] PRIMARY KEY CLUSTERED
(
	[EmployeeId] ASC,
	[DayId] ASC,
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InspectorPlanningStatus]    Script Date: 5-11-2019 21:03:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InspectorPlanningStatus](
	[Status] [nvarchar](45) NOT NULL,
 CONSTRAINT [PK_InspectorPlanningStatus] PRIMARY KEY CLUSTERED
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Note]    Script Date: 5-11-2019 21:03:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Note](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ContactPersonId] [int] NOT NULL,
	[Note] [text] NOT NULL,
	[Time] [datetime] NOT NULL,
 CONSTRAINT [PK_Note] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 5-11-2019 21:03:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[EventId] [int] NOT NULL,
	[EmployeeId] [int] NULL,
	[QuotationId] [int] NOT NULL,
	[Description] [text] NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QueryTemplate]    Script Date: 5-11-2019 21:03:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QueryTemplate](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Query] [text] NOT NULL,
	[Description] [text] NOT NULL,
 CONSTRAINT [PK_QueryTemplate] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Question]    Script Date: 5-11-2019 21:03:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Question](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SurveyId] [int] NOT NULL,
	[Type] [nvarchar](45) NOT NULL,
	[Question] [text] NOT NULL,
	[Order] [int] NOT NULL,
	[Variables] [nvarchar](45) NOT NULL,
 CONSTRAINT [PK_Question] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuestionType]    Script Date: 5-11-2019 21:03:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuestionType](
	[Type] [nvarchar](45) NOT NULL,
 CONSTRAINT [PK_QuestionType] PRIMARY KEY CLUSTERED
(
	[Type] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Quotation]    Script Date: 5-11-2019 21:03:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Quotation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[EmployeeId] [int] NULL,
	[EventId] [int] NOT NULL,
	[Price] [decimal](8, 2) NOT NULL,
	[BtwPercentage] [int] NOT NULL,
	[TimeSend] [datetime] NULL,
	[Status] [nvarchar](45) NOT NULL,
	[Content] [text] NOT NULL,
 CONSTRAINT [PK_Quotation] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuotationStatus]    Script Date: 5-11-2019 21:03:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuotationStatus](
	[Status] [nvarchar](45) NOT NULL,
 CONSTRAINT [PK_QuotationStatus] PRIMARY KEY CLUSTERED
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Report]    Script Date: 5-11-2019 21:03:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Report](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[Status] [nvarchar](45) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Report] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReportElement]    Script Date: 5-11-2019 21:03:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReportElement](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ReportId] [int] NOT NULL,
	[ElementType] [nvarchar](100) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[Content] [text] NOT NULL,
	[Order] [int] NOT NULL,
 CONSTRAINT [PK_ReportElement] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReportStatus]    Script Date: 5-11-2019 21:03:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReportStatus](
	[Status] [nvarchar](45) NOT NULL,
 CONSTRAINT [PK_ReportStatus] PRIMARY KEY CLUSTERED
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SickReportInspector]    Script Date: 5-11-2019 21:03:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SickReportInspector](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[InspectorPlanningEmployeeId] [int] NOT NULL,
	[InspoctorPlanningDayId] [int] NOT NULL,
	[InspectorPlanningOrderId] [int] NOT NULL,
	[Reason] [text] NOT NULL,
 CONSTRAINT [PK_SickReportInspector] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Survey]    Script Date: 5-11-2019 21:03:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Survey](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[Status] [nvarchar](45) NOT NULL,
	[Description] [text] NOT NULL,
 CONSTRAINT [PK_Survey] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SurveyStatus]    Script Date: 5-11-2019 21:03:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SurveyStatus](
	[Status] [nvarchar](45) NOT NULL,
 CONSTRAINT [PK_SurveyStatus] PRIMARY KEY CLUSTERED
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Answer]  WITH CHECK ADD  CONSTRAINT [FK_Answer_Case] FOREIGN KEY([CaseId])
REFERENCES [dbo].[Case] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Answer] CHECK CONSTRAINT [FK_Answer_Case]
GO
ALTER TABLE [dbo].[Answer]  WITH CHECK ADD  CONSTRAINT [FK_Answer_Question] FOREIGN KEY([QuestionId])
REFERENCES [dbo].[Question] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Answer] CHECK CONSTRAINT [FK_Answer_Question]
GO
ALTER TABLE [dbo].[AvailabilityInspector]  WITH CHECK ADD  CONSTRAINT [FK_AvailabilityInspector_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AvailabilityInspector] CHECK CONSTRAINT [FK_AvailabilityInspector_Employee]
GO
ALTER TABLE [dbo].[BetterReportInspector]  WITH CHECK ADD  CONSTRAINT [FK_BetterReportInspector_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BetterReportInspector] CHECK CONSTRAINT [FK_BetterReportInspector_Employee]
GO
ALTER TABLE [dbo].[Case]  WITH CHECK ADD  CONSTRAINT [FK_Case_CaseStatus] FOREIGN KEY([Status])
REFERENCES [dbo].[CaseStatus] ([Status])
GO
ALTER TABLE [dbo].[Case] CHECK CONSTRAINT [FK_Case_CaseStatus]
GO
ALTER TABLE [dbo].[Case]  WITH CHECK ADD  CONSTRAINT [FK_Case_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[Case] CHECK CONSTRAINT [FK_Case_Employee]
GO
ALTER TABLE [dbo].[Case]  WITH CHECK ADD  CONSTRAINT [FK_Case_Survey] FOREIGN KEY([SurveyId])
REFERENCES [dbo].[Survey] ([Id])
GO
ALTER TABLE [dbo].[Case] CHECK CONSTRAINT [FK_Case_Survey]
GO
ALTER TABLE [dbo].[CertificateInspector]  WITH CHECK ADD  CONSTRAINT [FK_CertificateInspector_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CertificateInspector] CHECK CONSTRAINT [FK_CertificateInspector_Employee]
GO
ALTER TABLE [dbo].[ContactPerson]  WITH CHECK ADD  CONSTRAINT [FK_ContactPerson_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
GO
ALTER TABLE [dbo].[ContactPerson] CHECK CONSTRAINT [FK_ContactPerson_Customer]
GO
ALTER TABLE [dbo].[Day]  WITH CHECK ADD  CONSTRAINT [FK_Day_Order] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Day] CHECK CONSTRAINT [FK_Day_Order]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Department] FOREIGN KEY([Department])
REFERENCES [dbo].[Department] ([Name])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Department]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_EmployeeStatus] FOREIGN KEY([Status])
REFERENCES [dbo].[EmployeeStatus] ([Status])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_EmployeeStatus]
GO
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Event_ContactPerson] FOREIGN KEY([ContactPersonId])
REFERENCES [dbo].[ContactPerson] ([Id])
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_ContactPerson]
GO
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Event_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_Customer]
GO
ALTER TABLE [dbo].[InspectorPlanning]  WITH CHECK ADD  CONSTRAINT [FK_InspectorPlanning_Day] FOREIGN KEY([DayId], [OrderId])
REFERENCES [dbo].[Day] ([Id], [OrderId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[InspectorPlanning] CHECK CONSTRAINT [FK_InspectorPlanning_Day]
GO
ALTER TABLE [dbo].[InspectorPlanning]  WITH CHECK ADD  CONSTRAINT [FK_InspectorPlanning_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[InspectorPlanning] CHECK CONSTRAINT [FK_InspectorPlanning_Employee]
GO
ALTER TABLE [dbo].[InspectorPlanning]  WITH CHECK ADD  CONSTRAINT [FK_InspectorPlanning_InspectorPlanningStatus] FOREIGN KEY([Status])
REFERENCES [dbo].[InspectorPlanningStatus] ([Status])
GO
ALTER TABLE [dbo].[InspectorPlanning] CHECK CONSTRAINT [FK_InspectorPlanning_InspectorPlanningStatus]
GO
ALTER TABLE [dbo].[Note]  WITH CHECK ADD  CONSTRAINT [FK_Note_ContactPerson] FOREIGN KEY([ContactPersonId])
REFERENCES [dbo].[ContactPerson] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Note] CHECK CONSTRAINT [FK_Note_ContactPerson]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Customer]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Employee]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Event] FOREIGN KEY([EventId])
REFERENCES [dbo].[Event] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Event]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Quotation] FOREIGN KEY([QuotationId])
REFERENCES [dbo].[Quotation] ([Id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Quotation]
GO
ALTER TABLE [dbo].[Question]  WITH CHECK ADD  CONSTRAINT [FK_Question_QuestionType] FOREIGN KEY([Type])
REFERENCES [dbo].[QuestionType] ([Type])
GO
ALTER TABLE [dbo].[Question] CHECK CONSTRAINT [FK_Question_QuestionType]
GO
ALTER TABLE [dbo].[Question]  WITH CHECK ADD  CONSTRAINT [FK_Question_Survey] FOREIGN KEY([SurveyId])
REFERENCES [dbo].[Survey] ([Id])
GO
ALTER TABLE [dbo].[Question] CHECK CONSTRAINT [FK_Question_Survey]
GO
ALTER TABLE [dbo].[Quotation]  WITH CHECK ADD  CONSTRAINT [FK_Quotation_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
GO
ALTER TABLE [dbo].[Quotation] CHECK CONSTRAINT [FK_Quotation_Customer]
GO
ALTER TABLE [dbo].[Quotation]  WITH CHECK ADD CONSTRAINT [FK_Quotation_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[Quotation] CHECK CONSTRAINT [FK_Quotation_Employee]
GO
ALTER TABLE [dbo].[Quotation]  WITH CHECK ADD  CONSTRAINT [FK_Quotation_Event] FOREIGN KEY([EventId])
REFERENCES [dbo].[Event] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Quotation] CHECK CONSTRAINT [FK_Quotation_Event]
GO
ALTER TABLE [dbo].Quotation  WITH CHECK ADD  CONSTRAINT [FK_Quotation_QuotationStatus] FOREIGN KEY([Status])
REFERENCES [dbo].[QuotationStatus] ([Status])
GO
ALTER TABLE [dbo].[Quotation] CHECK CONSTRAINT [FK_Quotation_QuotationStatus]
GO
ALTER TABLE [dbo].[Report]  WITH CHECK ADD  CONSTRAINT [FK_Report_Order] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Report] CHECK CONSTRAINT [FK_Report_Order]
GO
ALTER TABLE [dbo].[Report]  WITH CHECK ADD  CONSTRAINT [FK_Report_ReportStatus] FOREIGN KEY([Status])
REFERENCES [dbo].[ReportStatus] ([Status])
GO
ALTER TABLE [dbo].[Report] CHECK CONSTRAINT [FK_Report_ReportStatus]
GO
ALTER TABLE [dbo].[ReportElement]  WITH CHECK ADD  CONSTRAINT [FK_ReportElement_ElementType] FOREIGN KEY([ElementType])
REFERENCES [dbo].[ElementType] ([Type])
GO
ALTER TABLE [dbo].[ReportElement] CHECK CONSTRAINT [FK_ReportElement_ElementType]
GO
ALTER TABLE [dbo].[ReportElement]  WITH CHECK ADD  CONSTRAINT [FK_ReportElement_Report] FOREIGN KEY([ReportId])
REFERENCES [dbo].[Report] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ReportElement] CHECK CONSTRAINT [FK_ReportElement_Report]
GO
ALTER TABLE [dbo].[SickReportInspector]  WITH CHECK ADD  CONSTRAINT [FK_SickReportInspector_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SickReportInspector] CHECK CONSTRAINT [FK_SickReportInspector_Employee]
GO
ALTER TABLE [dbo].[SickReportInspector]  WITH CHECK ADD  CONSTRAINT [FK_SickReportInspector_InspectorPlanning] FOREIGN KEY([InspectorPlanningEmployeeId], [InspoctorPlanningDayId], [InspectorPlanningOrderId])
REFERENCES [dbo].[InspectorPlanning] ([EmployeeId], [DayId], [OrderId])
GO
ALTER TABLE [dbo].[SickReportInspector] CHECK CONSTRAINT [FK_SickReportInspector_InspectorPlanning]
GO
ALTER TABLE [dbo].[Survey]  WITH CHECK ADD  CONSTRAINT [FK_Survey_Order] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([Id])
GO
ALTER TABLE [dbo].[Survey] CHECK CONSTRAINT [FK_Survey_Order]
GO
ALTER TABLE [dbo].[Survey]  WITH CHECK ADD  CONSTRAINT [FK_Survey_SurveyStatus] FOREIGN KEY([Status])
REFERENCES [dbo].[SurveyStatus] ([Status])
GO
ALTER TABLE [dbo].[Survey] CHECK CONSTRAINT [FK_Survey_SurveyStatus]
GO
