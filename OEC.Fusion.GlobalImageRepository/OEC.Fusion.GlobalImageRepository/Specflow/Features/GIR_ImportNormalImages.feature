Feature: Normal Images
	Import Images for normal type


Scenario: 1 Upload a new proper normal type image for a part
	Given When I connect to the database
	When I execute query to get the non used partNumber
	Then I verify the partnumber images are not present in Image directory
	And Create a folder in local directory with format payyyy-mm-dd_hhmmss
	And Copy only one image file from ImagesToUse folder to the newly created folder
	And Rename the Image file with the non used partnumber and '<properImageView>' in the format PN-ProperImageView
	And Zip created folder
	And Find sftp directory to upload the zip file
	And Copy zip file to the ctsftp.gir2qc directory
	And Run spPRODDailyDownload procedure to Upload zip file in Image directory
	And Verify uploaded proper images are present in the Image directory with '<properImageView>'
	And Verify uploaded proper images are present in the Image folder using query
	And Verify the created zip file is removed from sftp path

	Examples:
				| properImageView |
				| LIF             |
				| FRO             |
				| TIN             |
				| ANG             |
				| BAC             |
				| LEF             |
				| RIT             |
				| TOP             |
				| BOT             |
				| SID             |
				| CON             |
				| KIT             |
				| OTH             |
				| ZZ1             |
				| ZZ2             |
				| ZZ3             |
				| ZZ4             |
				| ZZ5             |
				| ZZ6             |
				| ZZ7             |
				| ZZ8             |
				| ZZ9             |
				| ZZ2             |
				| ZZ2             |





	
	

	