### Customer Management

- Implemented customer registration with name, email, and phone validation.
- Added customer listing from SQL Server using EF Core.
- Created the Customer model, repository, and service layers.
- Configured `BankingDbContext` and applied the initial EF Core migration.
- Verified that customers can be added and viewed.

**Commit:** `feat: add and view customers`
**Commit hash:** 15831a10bd942ecdad255d7daf354b17d66b1907

-------------------------------------------------------------------------------------------------------
### Bank Account Management

- Implemented bank account creation for existing customers.
- Added account validation to prevent duplicate account numbers.
- Implemented viewing all bank accounts and accounts by customer ID.
- Updated `BankAccount` model with account status, balance, creation date, and row-version concurrency tracking.
- Created the bank account repository interface, repository, and service layers.
- Updated `BankingDbContext` with account relationships, unique account-number constraints, balance precision, and concurrency configuration.
- Updated `Program.cs` with bank account management menu options.
- Applied the `AddBankAccountManagement` EF Core migration.
- Verified that bank accounts can be created and viewed.

**Commit:** `feat: add bank account management`

**Commit hash:** bffc4eeb6f195b40257dc626e75cffbc204741ae

----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
### Deposit and Withdrawal Management

- Implemented deposit functionality to add money to bank accounts.
- Implemented withdrawal functionality with insufficient-balance validation.
- Added validation for account IDs, positive transaction amounts, and active account status.
- Created the `TransactionType` enum to distinguish deposits and withdrawals.
- Created the `BankTransaction` model to store transaction details, amounts, and balances after transactions.
- Created the transaction repository interface and repository for storing and retrieving transaction records.
- Implemented `BankTransactionService` to handle deposits, withdrawals, and transaction history.
- Updated `BankingDbContext` with transaction entity configuration, monetary precision, and account relationships.
- Updated `Program.cs` with deposit, withdrawal, and transaction history menu options.
- Applied the `AddBankTransactions` EF Core migration.
- Tested deposits, withdrawals, insufficient-balance validation, and transaction history.

**Commit:** `feat: add deposit and withdrawal management`

**Commit hash:** 5ec21841aa34f78cfdd571369d9154e38cf163d0

-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

