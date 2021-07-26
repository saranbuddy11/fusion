Feature: GIR

Scenario: 1 GIR Image Verification
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

Scenario: 2 Reload proper 24 of 360 type images that has 360 images assigned already
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
	And verify the Date and time attribute of the newly uploaded images is greater than the old images
	And Verify the created zip file is removed from sftp path

Scenario: 3 Upload 24 images of 360 type for non-existing part number
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
	And Verify the created zip file is removed from sftp path

Scenario: 4 Reload 1 image of 360 type images
	Given When I connect to the database
	When I execute query to get already used partNumber
	Then I verify the partnumber image files are present in Image directory
	And Get the latest date and time attribute of the images present in Image directory
	And Create a folder in local directory with format payyyy-mm-dd_hhmmss
	And Copy only one image file from ImagesToUse folder to the newly created folder
	And Rename the files with already used partnumber in the format PN-360-01
	And Zip created folder
	And Find sftp directory to upload the zip file
	And Copy zip file to the ctsftp.gir2qc directory
	And Run spPRODDailyDownload procedure to Upload zip file in Image directory
	And Verify the partnumber images are present in Image directory
	And Verify the partnumber images are present in Image directory using Query
	And verify the Date and time attribute of the newly uploaded images is not greater than the old images
	And Verify the created zip file is removed from sftp path

Scenario: 5 Reload 25 images from 360 type images for 1 part
	Given When I connect to the database
	When I execute query to get already used partNumber
	Then I verify the partnumber image files are present in Image directory
	And Get the latest date and time attribute of the images present in Image directory
	And Create a folder in local directory with format payyyy-mm-dd_hhmmss
	And Copy 25 image files from ImagesToUse folder to the newly created folder
	And Rename the files with already used partnumber in the format PN-360-01 to PN-360-25
	And Zip created folder
	And Find sftp directory to upload the zip file
	And Copy zip file to the ctsftp.gir2qc directory
	And Run spPRODDailyDownload procedure to Upload zip file in Image directory
	And Verify the partnumber images are present in Image directory
	And Verify the partnumber images are present in Image directory using Query
	And verify the Date and time attribute of the newly uploaded images is not greater than the old images
	And Verify the created zip file is removed from sftp path

Scenario Outline: 6 Upload 24 images of 360 type with improper extension
	Given When I connect to the database
	When I execute query to get the non used partNumber
	Then I verify the partnumber images are not present in Image directory
	And Create a folder in local directory with format payyyy-mm-dd_hhmmss
	And Copy 24 image files from ImagesToUse folder to the newly created folder
	And Change the file extension from .jpg to <extension>
	And Rename the files with the partnumber in the format PN-360-01
	And Zip created folder
	And Find sftp directory to upload the zip file
	And Copy zip file to the ctsftp.gir2qc directory
	And Run spPRODDailyDownload procedure to Upload zip file in Image directory
	And Verify the partnumber images are not present in Image directory
	And Verify the partnumber images are not present in Image directory using Query
	And Verify the created zip file is removed from sftp path

	Examples:
		| extension |
		| .bmp      |
		| .jp       |
		| .txt      |
	
Scenario: 7 Reload 360 images for a different region
	Given When I connect to the database
	When I execute query to get already used partNumber
	Then I verify the partnumber image files are present in Image directory
	And Get the latest date and time attribute of the images present in Image directory
	And Create a folder in local directory with format payyyy-mm-dd_hhmmss
	And Copy 24 image files from ImagesToUse folder to the newly created folder
	And Rename the files with already used partnumber in the format PN-360-01
	And Zip created folder
	And Find sftp directory with different region to upload the zip file
	And Copy zip file to different region directory
	And Run spPRODDailyDownload procedure to Upload zip file in Image directory
	And Verify the partnumber images are present in Image directory
	And Verify the partnumber images are present in Image directory using Query
	And verify the Date and time attribute of the newly uploaded images is not greater than the old images
	And Verify the created zip file is removed from sftp path

Scenario: 8 Upload 24 images of 360 type with no extension
	Given When I connect to the database
	When I execute query to get the non used partNumber
	Then I verify the partnumber images are not present in Image directory
	And Create a folder in local directory with format payyyy-mm-dd_hhmmss
	And Copy 24 image files from ImagesToUse folder to the newly created folder
	And Remove the file extension from .jpg to null
	And Rename the files with the partnumber in the format PN-360-01
	And Zip created folder
	And Find sftp directory to upload the zip file
	And Copy zip file to the ctsftp.gir2qc directory
	And Run spPRODDailyDownload procedure to Upload zip file in Image directory
	And Verify the partnumber images are not present in Image directory
	And Verify the partnumber images are not present in Image directory using Query
	And Verify the created zip file is removed from sftp path

Scenario: 9 Upload 1 image with no extension and 23 images with proper extension
	Given When I connect to the database
	When I execute query to get the non used partNumber
	Then I verify the partnumber images are not present in Image directory
	And Create a folder in local directory with format payyyy-mm-dd_hhmmss
	And Copy 24 image files from ImagesToUse folder to the newly created folder
	And Remove the file extension from .jpg to null only for one image file
	And Rename the files with the partnumber in the format PN-360-01
	And Zip created folder
	And Find sftp directory to upload the zip file
	And Copy zip file to the ctsftp.gir2qc directory
	And Run spPRODDailyDownload procedure to Upload zip file in Image directory
	And Verify the partnumber images are not present in Image directory
	And Verify the partnumber images are not present in Image directory using Query
	And Verify the created zip file is removed from sftp path

Scenario: 10 Upload 1 image with no extension and 24 images with proper extension
	Given When I connect to the database
	When I execute query to get the non used partNumber
	Then I verify the partnumber images are not present in Image directory
	And Create a folder in local directory with format payyyy-mm-dd_hhmmss
	And Copy 25 image files from ImagesToUse folder to the newly created folder with new partNumber
	And Rename the files with non used partnumber in the format PN-360-01 to PN-360-25
	And Remove the file extension from .jpg to null only for one image file
	And Zip created folder
	And Find sftp directory to upload the zip file
	And Copy zip file to the ctsftp.gir2qc directory
	And Run spPRODDailyDownload procedure to Upload zip file in Image directory
	And Verify the partnumber images are not present in Image directory
	And Verify the partnumber images are not present in Image directory using Query
	And Verify the created zip file is removed from sftp path

Scenario: 11 Upload 1 image with improper extension and 24 images with proper extension
	Given When I connect to the database
	When I execute query to get the non used partNumber
	Then I verify the partnumber images are not present in Image directory
	And Create a folder in local directory with format payyyy-mm-dd_hhmmss
	And Copy 25 image files from ImagesToUse folder to the newly created folder with new partNumber
	And Rename the files with non used partnumber in the format PN-360-01 to PN-360-25
	And Change the file extension for only one file from .jpg to <extension>
	And Zip created folder
	And Find sftp directory to upload the zip file
	And Copy zip file to the ctsftp.gir2qc directory
	And Run spPRODDailyDownload procedure to Upload zip file in Image directory
	And Verify the partnumber images are not present in Image directory
	And Verify the partnumber images are not present in Image directory using Query
	And Verify the created zip file is removed from sftp path

	Examples:
		| extension |
		| .bmp      |
		| .jp       |
		| .txt      |

Scenario: 12 Upload 1 image with improper extension and 23 images with proper extension
	Given When I connect to the database
	When I execute query to get the non used partNumber
	Then I verify the partnumber images are not present in Image directory
	And Create a folder in local directory with format payyyy-mm-dd_hhmmss
	And Copy 24 image files from ImagesToUse folder to the newly created folder
	And Rename the files with the partnumber in the format PN-360-01
	And Change the file extension for only one file from .jpg to <extension>
	And Zip created folder
	And Find sftp directory to upload the zip file
	And Copy zip file to the ctsftp.gir2qc directory
	And Run spPRODDailyDownload procedure to Upload zip file in Image directory
	And Verify the partnumber images are not present in Image directory
	And Verify the partnumber images are not present in Image directory using Query
	And Verify the created zip file is removed from sftp path

	Examples:
		| extension |
		| .bmp      |
		| .jp       |
		| .txt      |


