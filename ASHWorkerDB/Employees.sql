CREATE TABLE [dbo].[Employees.sql] (
    [EmployeeId] AS             1,
    [PayPerHour] DECIMAL (5, 2) NOT NULL,
    [WorkerId]   INT            NOT NULL,
    CONSTRAINT [FK_Employees_Workers] FOREIGN KEY ([WorkerId]) REFERENCES [dbo].[Workers] ([WorkerId])
);
