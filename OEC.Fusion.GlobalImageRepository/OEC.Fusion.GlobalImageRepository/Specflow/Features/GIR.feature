Feature: GIR

Scenario: GIR Image Verification
	Given When I connect to the database to execute query to get the partNumber
	When I verify the partnumber images are not present in working directory
	Then Create a folder for images in local in the format payyyymmdd_hhmmss
	And Copy 24 files from parts folder to the newly created folder
	And Rename the files with the given partnumber
	And Zip the folder
	And Find the sftp dirctory to upload the zip file
	And Copy the zip file to the ctsftp.gir2qc folder
	And Run spPRODDailyDownload procedure to Upload zip file in Images folder
	And Verify the partnumber is present in the image folder
	And Verify the partnumber is present in the image folder using query




