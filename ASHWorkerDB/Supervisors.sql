CREATE TABLE [dbo].[Supervisor] (
    [SupervisorId] AS              1,
    [AnnualSalary] DECIMAL (10, 2) NOT NULL,
    [WorkerId]     INT             NULL,
    CONSTRAINT [FK_Supervisor_Workers] FOREIGN KEY ([WorkerId]) REFERENCES [dbo].[Workers] ([WorkerId])
);
