Feature: GIR

Scenario: GIR Image Verification
	Given When I connect to the database
	And I execute the query to get the partnumber
	When I verify the partnumber images are not present in working directory
	Then Create a folder for images in local in the format payyyymmdd_hhmmss
	And Copy 24 files from parts folder to the newly created folder
	And Rename the files with the given partnumber
	And Zip the folder
	And Copy the zip file to the Images folder
	And Unzip the copied folder in the Images
	And Verify the partnumber is present in the image folder




