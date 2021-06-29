Feature: GIR

Scenario: GIR Image Verification
	Given When I connect to the database and execute query to get the non used partNumber
	When I verify the partnumber images are not present in Image directory
	Then Create a folder for in local directory with format payyyy-mm-dd_hhmmss
	And Copy 24 image files from ImagesToUse folder to the newly created folder
	And Rename the files with the partnumber in the format PN-360-01
	And Zip created folder
	And Find sftp directory to upload the zip file
	And Copy zip file to the ctsftp.gir2qc directory
	And Run spPRODDailyDownload procedure to Upload zip file in Image directory
	And Verify uploaded images are present in the Image directory
	And Verify uploaded images are present in the Image folder using query
	And Verify the created zip file is removed from sftp path
	And Verify Images Successfully loaded into the repository mail in outlook 




