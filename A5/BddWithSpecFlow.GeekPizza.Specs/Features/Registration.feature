Feature: Registration

Rule: Should be able to register with user name and password

Scenario: Customer registers successfully
	When the client attempts to register with user name "Trillian" and password "139139"
	Then the registration should be successful

Rule: The user name is mandatory

Scenario: User name was not provided
	When the client attempts to register with
		| user name |
		|           |
	Then the registration should fail with "Name must be provided"


Rule: The password should be valid and verified

Scenario: Password was not provided
	When the client attempts to register with
		| password | password re-enter |
		|          |                   |
	Then the registration should fail with "Password and password re-enter must be provided"

Scenario: The re-entered password is different
	When the client attempts to register with
		| password | password re-enter |
		| 139139   | different         |
	Then the registration should fail with "Re-entered password is different"

Scenario: Password is too short
	When the client attempts to register with
		| password | password re-enter |
		| 123      | 123               |
	Then the registration should fail with "Password must be at least 4 characters long"
