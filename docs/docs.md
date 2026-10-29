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
