CREATE TABLE Customer (
    customerId INT IDENTITY(1,1) PRIMARY KEY,
    name VARCHAR(100),
    email VARCHAR(100),
    contactInfo VARCHAR(150)
);

CREATE TABLE Account (
    accountId INT IDENTITY(1,1) PRIMARY KEY,
    customerId INT,
    accountType VARCHAR(20) CHECK (accountType IN ('SAVINGS','CURRENT')),
    balance DECIMAL(12,2),
    FOREIGN KEY (customerId) REFERENCES Customer(customerId)
);

CREATE TABLE [Transaction] (
    transactionId INT IDENTITY(1,1) PRIMARY KEY,
    accountId INT,
    transactionType VARCHAR(20) CHECK (transactionType IN ('DEPOSIT','WITHDRAWAL','TRANSFER')),
    amount DECIMAL(12,2),
    transactionDate DATE,
    FOREIGN KEY (accountId) REFERENCES Account(accountId)
);

CREATE TABLE Loan (
    loanId INT IDENTITY(1,1) PRIMARY KEY,
    customerId INT,
    loanAmount DECIMAL(12,2),
    interestRate DECIMAL(5,2),
    loanStatus VARCHAR(20) CHECK (loanStatus IN ('APPLIED','APPROVED','REJECTED')),
    FOREIGN KEY (customerId) REFERENCES Customer(customerId)
);

CREATE TABLE Repayment (
    repaymentId INT IDENTITY(1,1) PRIMARY KEY,
    loanId INT,
    repaymentDate DATE,
    amountPaid DECIMAL(12,2),
    balanceRemaining DECIMAL(12,2),
    FOREIGN KEY (loanId) REFERENCES Loan(loanId)
);

CREATE TABLE AuditLog (
    logId INT IDENTITY(1,1) PRIMARY KEY,
    transactionId INT NULL,
    logDate DATE,
    actionPerformed VARCHAR(100),
    performedBy VARCHAR(100),
    FOREIGN KEY (transactionId) REFERENCES [Transaction](transactionId)
);
