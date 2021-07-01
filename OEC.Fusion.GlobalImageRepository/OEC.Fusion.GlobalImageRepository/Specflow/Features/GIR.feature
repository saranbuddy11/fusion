Feature: GIR

Scenario: GIR Image Verification
	Given When I connect to the database 
	When I execute query to get the non used partNumber
	Then I verify the partnumber images are not present in Image directory
	And Create a folder in local directory with format payyyy-mm-dd_hhmmss
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

Scenario: Reload proper 24 of 360 type images that has 360 images assigned already
	Given When I connect to the database 
	When I execute query to get already used partNumber
	Then I verify the partnumber image files are present in Image directory
	And Get the latest date and time attribute of the images present in Image directory
	And Create a folder in local directory with format payyyy-mm-dd_hhmmss
	And Copy 24 image files from ImagesToUse folder to the newly created folder
	And Rename the files with already used partnumber in the format PN-360-01
	And Zip created folder
	And Find sftp directory to upload the zip file
	And Copy zip file to the ctsftp.gir2qc directory
	And Run spPRODDailyDownload procedure to Upload zip file in Image directory
	And Verify re-uploaded images are present in the Image directory
	And Verify re-uploaded images are present in the Image folder using query
	And verify the Date and time attribute of the newly uploaded images id greater than the old images
	And Verify the created zip file is removed from sftp path
	And Verify Images Successfully loaded into the repository mail in outlook 

Scenario: Upload 24 images of 360 type for non-existing part number
	Given When I connect to the database
	When I execute query with non-existing partnumber and verify the partnumber does not exist in Images folder
	Then Create a folder in local directory with format payyyy-mm-dd_hhmmss
	And Copy 24 image files from ImagesToUse folder to the newly created folder
	And Rename the files with the non-existing partnumber in the format PN-360-01
	And Zip created folder
	And Find sftp directory to upload the zip file
	And Copy zip file to the ctsftp.gir2qc directory
	And Run spPRODDailyDownload procedure to Upload zip file in Image directory
	And Verify the partnumber images are not present in Image directory
	And Verify the partnumber images are not present in Image directory using Query
	And Verify Global Image Repository - Image Import Failed mail in outlook 










