CREATE TABLE [dbo].[Managers] (
    [ManagerId]        AS              1,
    [AnnualSalary]     DECIMAL (10, 2) NOT NULL,
    [MaxExpenseAmount] DECIMAL (10, 2) NOT NULL,
    [WorkerId]         INT             NOT NULL,
    CONSTRAINT [FK_Managers_Workers] FOREIGN KEY ([WorkerId]) REFERENCES [dbo].[Workers] ([WorkerId])
);
