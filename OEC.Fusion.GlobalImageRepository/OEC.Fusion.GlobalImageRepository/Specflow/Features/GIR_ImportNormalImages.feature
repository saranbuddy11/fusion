Feature: Normal Images
	Import Images for normal type


Scenario: 1 Upload a new proper normal type image for a part
	Given When I connect to the database
	When I execute query to get the non used partNumber
	Then I verify the partnumber images are not present in Image directory
	And Create a folder in local directory with format payyyy-mm-dd_hhmmss
	And Copy only one image file from ImagesToUse folder to the newly created folder
	And Rename the Image file with the non used partnumber and <properImageView> in the format PN-ProperImageView

	Examples:
				| properImageView |  
				| LIF             |   
				| FRO             |   
				| TIN             |   



	
	

	