Feature: CustomerCrud

Background: 
	Given system erro codes are following
		| Code | Description                                               |
		| 101  | invalid mobile number                                     |
		| 102  | invalid email address                                     |
		| 103  | Invalid Bank Account Number                               |
		| 201  | Duplicate customer by first-name, last-name,Date-of-birth |
		| 202  | Duplicate Customer by Email address                       |


Scenario: Create Read Edit Delete Customer
	When user creates a customer with following data
		| ID | Firstname | Lastname | Email        | Phonenumber   | DateOfBirth | BankAccountNumber          |
		| 1  | John      | Doe      | john@doe.com | +989121234567 | 01-JAN-2000 | NL91ABNA0417164300		  |
	Then user can lookup all customers and filter by email of "john@doe.com" and get "1" records
	When user edit customer with new email of "new@gmail.com"
	Then user can lookup all customers and filter by email of "john@doe.com" and get "0" records
	And user can lookup all customers and filter by email of "new@gmail.com" and get "1" records
	When user delete customer by Email of "new@gmail.com"
	Then user can lookup all customers and filter by email of "john@doe.com" and get "0" records
	And user can lookup all customers and filter by email of "new@gmail.com" and get "0" records