# BankSystem

### short description

The goal of the project is to create an API for the banking system, using which users will be able to transfer funds to their own or other accounts and use an ATM.

System managers will be able to view different types of reports.

The project includes several modules. Each module can be considered as an independent system, although some modules depend on the existence of other modules. Therefore, they should be implemented in sequence.

### modules

- Internet bank
     - operator
     - the user
- ATM client
     - Card authorization
     - ATM operations
- Reports
     - User statistics
     - Statistics of transactions

### Internet bank

The internet bank should be a web application, through which bank operators will be able to register users, create bank accounts for them, and add cards to bills.

**operator**

The operator must be able to register individuals, during which he will enter the following data:

- name *
- last name *
- personal number *
- date of birth *
- E-mail mail *
- password *

When creating a bank account for the user, the operator must be able to specify the following data:

- IBAN (the IBAN must be validated during registration. The operator cannot create an account with an invalid IBAN)
- Amount (amount on the account)
- Currency (Currency. Must be a selectable field. Possible values: GEL, USD, EUR)

During card registration, the operator must provide the following data:

- number of card
- name and surname
- card term (year, month)
- 3 digit CVV code (for online payments)
- 4-digit PIN code (to withdraw money from an ATM)

**user**

Registered users should be able to view the accounts and cards created for them by the operator.

The user should be able to perform two types of transactions from the internet bank:

- money transfer between own accounts, during which the transfer fee will be 0%
- transfer to another bank account, during which the transfer fee will be 1% + 0.5 (GEL/USD/EUR)

When transferring money, the currency exchange rates of the accounts must be taken into account. If the currency of one account is different from the currency of the other account, the amount must be converted according to the predetermined exchange rate.

### ATM client

An API needs to be created for the ATM client, where the user will be able to perform various operations after card authorization.

**Card Authorization**

Card authorization is also required to perform any ATM operation.

For this, the user must specify the card number and PIN code.

Authorization should not be successful if the card has expired.

**ATM Operations**

After authorization, it should be possible to perform the following operations:

- view the balance
- Withdraw money in GEL, USD or EUR currency
- Change the pin code

ATM withdrawal fee should be 2% and within 24 hours it should be possible to withdraw a maximum of 10,000 GEL.

### reports

Bank managers should be able to view the following types of reports (the API should return results in JSON format):

- User statistics
     - Number of users registered in the current year
     - Number of users registered in the last year
     - Number of registered users in the last 30 days
- Statistics of transactions
     - Number of transactions made in the last 1 month/6 months/1 year
     - Amount of revenue received from transactions in the last 1 month/6 months/1 year (in GEL/USD/EUR)
     - average revenue from one transaction (in GEL/USD/EUR)
     - Number of transactions in the last month by days (chart)
     - Total amount of money withdrawn from the ATM
