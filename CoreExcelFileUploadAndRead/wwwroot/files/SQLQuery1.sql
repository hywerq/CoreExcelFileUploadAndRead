DECLARE @Sql NVARCHAR(500) DECLARE @Cursor CURSOR

SET @Cursor = CURSOR FAST_FORWARD FOR
SELECT DISTINCT sql = 'ALTER TABLE [' + tc2.TABLE_SCHEMA + '].[' +  tc2.TABLE_NAME + '] DROP [' + rc1.CONSTRAINT_NAME + '];'
FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS rc1
LEFT JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc2 ON tc2.CONSTRAINT_NAME =rc1.CONSTRAINT_NAME

OPEN @Cursor FETCH NEXT FROM @Cursor INTO @Sql

WHILE (@@FETCH_STATUS = 0)
BEGIN
	Exec sp_executesql @Sql
	FETCH NEXT FROM @Cursor INTO @Sql
END

CLOSE @Cursor DEALLOCATE @Cursor
GO

EXEC sp_MSforeachtable 'DROP TABLE ?'
GO

DROP TABLE [file]
DROP TABLE [class]
DROP TABLE [class_group]
DROP TABLE [balance_account]
DROP TABLE [m2m_file_class_balance_account]

DELETE FROM [class]
DELETE FROM [balance_account]

CREATE TABLE [file] (
    [fl_id] INT NOT NULL IDENTITY (1, 1),
    [fl_name] VARCHAR(256) NOT NULL,
    [fl_title] VARCHAR(512) NOT NULL,
    [fl_date_start] DATE NOT NULL,
    [fl_date_end] DATE NOT NULL,
    [fl_created_dt] DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    [fl_bank_name] VARCHAR(256) NOT NULL,
    [fl_subject] VARCHAR(256) NOT NULL, 
    [fl_currency] VARCHAR(256) NOT NULL, 
    [fl_print_dt] DATETIME NOT NULL,
    [fl_opening_balance_active_sum] DECIMAL(20, 2) NULL,
    [fl_opening_balance_passive_sum] DECIMAL(20, 2) NULL,
    [fl_closing_balance_active_sum] DECIMAL(20, 2) NULL,
    [fl_closing_balance_passive_sum] DECIMAL(20, 2) NULL,
    [fl_turnover_debit_sum] DECIMAL(20, 2) NULL,
    [fl_turnover_credit_sum] DECIMAL(20, 2) NULL,
    CONSTRAINT [PK_file] PRIMARY KEY([fl_id])
);

CREATE TABLE [class] (
    [cl_id] INT NOT NULL IDENTITY (1, 1),
    [cl_number] TINYINT NOT NULL,
    [cl_title] VARCHAR(512) NOT NULL,
    [cl_opening_balance_active_sum] DECIMAL(20, 2) NULL,
    [cl_opening_balance_passive_sum] DECIMAL(20, 2) NULL,
    [cl_closing_balance_active_sum] DECIMAL(20, 2) NULL,
    [cl_closing_balance_passive_sum] DECIMAL(20, 2) NULL,
    [cl_turnover_debit_sum] DECIMAL(20, 2) NULL,
    [cl_turnover_credit_sum] DECIMAL(20, 2) NULL,
    CONSTRAINT [PK_class] PRIMARY KEY([cl_id])
);

CREATE TABLE [class_group] (
    [cg_id] INT NOT NULL IDENTITY (1, 1),
    [cg_number] TINYINT NOT NULL,
    [cg_opening_balance_active_sum] DECIMAL(20, 2) NULL,
    [cg_opening_balance_passive_sum] DECIMAL(20, 2) NULL,
    [cg_closing_balance_active_sum] DECIMAL(20, 2) NULL,
    [cg_closing_balance_passive_sum] DECIMAL(20, 2) NULL,
    [cg_turnover_debit_sum] DECIMAL(20, 2) NULL,
    [cg_turnover_credit_sum] DECIMAL(20, 2) NULL,
    CONSTRAINT [PK_class_group] PRIMARY KEY([cg_id])
);

CREATE TABLE [balance_account] (
    [ba_id] BIGINT NOT NULL IDENTITY (1, 1),
    [ba_number] SMALLINT NOT NULL,
    [ba_opening_balance_active] DECIMAL(20, 2) NOT NULL,
    [ba_opening_balance_passive] DECIMAL(20, 2) NOT NULL,
    [ba_closing_balance_active] DECIMAL(20, 2) NOT NULL,
    [ba_closing_balance_passive] DECIMAL(20, 2) NOT NULL,
    [ba_turnover_debit] DECIMAL(20, 2) NOT NULL,
    [ba_turnover_credit] DECIMAL(20, 2) NOT NULL,
    CONSTRAINT [PK_balance_account] PRIMARY KEY([ba_id])
);

CREATE TABLE [m2m_file_class_balance_account] (
    [fcb_file_id] INT NOT NULL,
    [fcb_class_id] INT NOT NULL,
    [fcb_class_group_id] INT NULL,
    [fcb_balance_account_id] BIGINT NOT NULL,
    CONSTRAINT [FK_m2m_fcb_file] FOREIGN KEY([fcb_file_id]) REFERENCES [file]([fl_id]) ON DELETE CASCADE,
    CONSTRAINT [FK_m2m_fcb_class] FOREIGN KEY([fcb_class_id]) REFERENCES [class]([cl_id]) ON DELETE CASCADE,
    CONSTRAINT [FK_m2m_fcb_class_group] FOREIGN KEY([fcb_class_group_id]) REFERENCES [class_group]([cg_id]) ON DELETE CASCADE,
    CONSTRAINT [FK_m2m_fcb_balance_account] FOREIGN KEY([fcb_balance_account_id]) REFERENCES [balance_account]([ba_id])  ON DELETE CASCADE
);
GO

CREATE OR ALTER PROCEDURE add_balance_account (
    @number SMALLINT,
    @opening_balance_active DECIMAL(20, 2),
    @opening_balance_passive DECIMAL(20, 2), 
    @closing_balance_active DECIMAL(20, 2),
    @closing_balance_passive DECIMAL(20, 2), 
    @turnover_debit DECIMAL(20, 2),
    @turnover_credit DECIMAL(20, 2),
    @class_id INT,
    @file_id INT
)
AS
BEGIN
    DECLARE @result TABLE(id BIGINT);

    INSERT INTO [balance_account] (
        [ba_number], 
        [ba_opening_balance_active], 
        [ba_opening_balance_passive], 
        [ba_closing_balance_active], 
        [ba_closing_balance_passive],
        [ba_turnover_debit],
        [ba_turnover_credit]
    ) 
    OUTPUT INSERTED.ba_id INTO @result
    VALUES 
    (@number, @opening_balance_active, @opening_balance_passive, @closing_balance_active, 
    @closing_balance_passive, @turnover_debit, @turnover_credit);

    DECLARE @account_id BIGINT = (SELECT TOP 1 * FROM @result);

    INSERT INTO [m2m_file_class_balance_account] (
        [fcb_file_id],
        [fcb_class_id],
        [fcb_balance_account_id]
    )
    VALUES 
    (@file_id, @class_id, @account_id);

    RETURN;
END
GO

CREATE OR ALTER PROCEDURE add_class_group (
    @number SMALLINT,
    @opening_balance_active DECIMAL(20, 2),
    @opening_balance_passive DECIMAL(20, 2), 
    @closing_balance_active DECIMAL(20, 2),
    @closing_balance_passive DECIMAL(20, 2), 
    @turnover_debit DECIMAL(20, 2),
    @turnover_credit DECIMAL(20, 2),
    @file_id INT
)
AS
BEGIN
    DECLARE @result TABLE(id INT);

    INSERT INTO [class_group] (
        [cg_number], 
        [cg_opening_balance_active_sum], 
        [cg_opening_balance_passive_sum], 
        [cg_closing_balance_active_sum], 
        [cg_closing_balance_passive_sum],
        [cg_turnover_debit_sum],
        [cg_turnover_credit_sum]
    ) 
    OUTPUT INSERTED.cg_id INTO @result
    VALUES 
    (@number, @opening_balance_active, @opening_balance_passive, @closing_balance_active, 
    @closing_balance_passive, @turnover_debit, @turnover_credit);

    DECLARE @group_id INT = (SELECT TOP 1 * FROM @result);

    UPDATE [m2m_file_class_balance_account]
    SET [fcb_class_group_id] = @group_id
    FROM [m2m_file_class_balance_account] AS m2m
        JOIN [balance_account] AS ba ON m2m.fcb_balance_account_id = ba.ba_id
    WHERE ba.ba_number LIKE CONVERT(VARCHAR, @number) + '%' 
        AND m2m.fcb_file_id = @file_id

    RETURN;
END
GO

--CREATE OR ALTER FUNCTION add_new_file (
--    @name VARCHAR(256),
--    @title VARCHAR(512),
--    @date_start DATE,
--    @date_end DATE
--)
--RETURNS INT
--AS 
--BEGIN 
--    DECLARE @result TABLE(id INT);

--    INSERT INTO [file] (
--        [fl_name], 
--        [fl_title], 
--        [fl_date_start], 
--        [fl_date_end], 
--        [fl_created_dt]
--    ) 
--    OUTPUT INSERTED.fl_id INTO @result
--    VALUES 
--    (@name, @title, @date_start, @date_end, CURRENT_TIMESTAMP);

--    RETURN (SELECT TOP 1 * FROM @result)
--END
--GO

EXEC add_balance_account
    @number,
    @opening_balance_active,
    @opening_balance_passive, 
    @closing_balance_active,
    @closing_balance_passive, 
    @turnover_debit,
    @turnover_credit,
    @class_id,
    @file_id

EXEC add_class_group
    @number,
    @opening_balance_active_sum,
    @opening_balance_passive_sum, 
    @closing_balance_active_sum,
    @closing_balance_passive_sum, 
    @turnover_debit_sum,
    @turnover_credit_sum,
    @file_id

INSERT INTO [file] ([fl_name], [fl_title], [fl_date_start], [fl_date_end], 
    [fl_created_dt], [fl_bank_name], [fl_subject], [fl_currency], [fl_print_dt])
OUTPUT INSERTED.fl_id 
VALUES 
('file1', 'title1', GETDATE(), GETDATE(), CURRENT_TIMESTAMP, 
    'bank1', 'subject1', 'currency1', CURRENT_TIMESTAMP)

INSERT INTO [class] ([cl_number], [cl_title])
OUTPUT INSERTED.cl_id
VALUES 
('class1', 'class title 1')

UPDATE [class]
SET 
    [cl_opening_balance_active_sum] = @opening_balance_active_sum,
    [cl_opening_balance_passive_sum] = @opening_balance_passive_sum,
    [cl_closing_balance_active_sum] = @closing_balance_active_sum,
    [cl_closing_balance_passive_sum] = @closing_balance_passive_sum,
    [cl_turnover_debit_sum] = @turnover_debit_sum,
    [cl_turnover_credit_sum] = @turnover_credit_sum
WHERE cl_id = @current_id

SELECT 
    [ba_number] AS 'Б/сч',
    [ba_opening_balance_active] AS 'Актив',
    [ba_opening_balance_passive] AS 'Пассив',
    [ba_turnover_debit] AS 'Дебет',
    [ba_turnover_credit] AS 'Кредит',
    [ba_closing_balance_active] AS 'Актив',
    [ba_closing_balance_passive] AS 'Пассив'
FROM m2m_file_class_balance_account AS m2m
    JOIN balance_account ON m2m.fcb_balance_account_id = balance_account.ba_id
WHERE m2m.fcb_file_id = @id