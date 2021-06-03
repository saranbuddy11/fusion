Feature: GIR

Scenario: GIR Image Verification
	Given When I connect to the database
	And I execute the query to get the partnumber
	When I verify the partnumber images are present in server location
	Then Create a folder for images in local
	And Test i need