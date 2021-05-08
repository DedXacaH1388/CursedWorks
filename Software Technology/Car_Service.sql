DROP TABLE IF EXISTS clients CASCADE;
DROP TABLE IF EXISTS services CASCADE;

CREATE TABLE clients(
	client_id 			SERIAL 		PRIMARY KEY GENERATE ALWAYS,
	surname 			VARCHAR(20) NOT NULL,
	first_name 			VARCHAR(20) NOT NULL,
	second_name 		VARCHAR(20) NOT NULL,
	car_brand 			VARCHAR(20) NOT NULL,
	address 			VARCHAR(40) NOT NULL,
	type_of_crash 		VARCHAR(60) NOT NULL);
	
CREATE TABLE services(
	service_id 			SERIAL 		PRIMARY KEY GENERATE ALWAYS,
	type_of_services 	VARCHAR(40) NOT NULL,
	price 				INT 		NOT NULL,
	apprximate_work 	VARCHAR(20) NOT NULL,
	description 		TEXT		NOT NULL);
