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

Scenario Outline: The password should be valid and verified
	When the client attempts to register with
		| password   | password re-enter   |
		| <password> | <password re-enter> |
	Then the registration should fail with "<error message>"
Examples: 
	| description                          | password | password re-enter | error message                                   |
	| Password was not provided            |          |                   | Password and password re-enter must be provided |
	| The re-entered password is different | 139139   | different         | Re-entered password is different                |
	| Password is too short                | 123      | 123               | Password must be at least 4 characters long     |

# Alternative approach: this is more suitable if the entiry has many fields and you don't want to list
#                       all fields in the examples table. 
# Note: "But" works in a same way as "And".

Rule: Registration data should be validated

Scenario Outline: Registration data should be validated
	Given the client provides registration details as 
		| user name | password | password re-enter |
		| Trillian  | 139139   | 139139            |
	But the field "<field>" is set to "<value>"
	When the client attempts to register
	Then the registration should fail with "<error message>"
Examples: 
	| description                          | field             | value     | error message                                   |
	| User name was not provided           | user name         |           | Name must be provided                           |
	| Password was not provided            | password          |           | Password and password re-enter must be provided |
	| The re-entered password is different | password re-enter | different | Re-entered password is different                |
	| Password is too short                | passwords         | 123       | Password must be at least 4 characters long     |
