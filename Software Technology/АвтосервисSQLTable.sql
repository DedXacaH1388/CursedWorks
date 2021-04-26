DROP TABLE IF EXISTS clients CASCADE;
DROP TABLE IF EXISTS services CASCADE;

CREATE TABLE clients (
	client_id			SERIAL PRIMARY KEY,
	surname				VARCHAR(30) NOT NULL,
	first_name			VARCHAR(30) NOT NULL,
	patronymic			VARCHAR(40) NOT NULL,
	car_brand			VARCHAR(40) NOT NULL,
	address				VARCHAR(60) NOT NULL,
	failure_mode		VARCHAR(40) NOT NULL);
CREATE TABLE services (
	service_id			SERIAL PRIMARY KEY,
	name_of_services	VARCHAR(20) NOT NULL,
    service_description	TEXT NOT NULL,
    price				INT NOT NULL,
    turnaround_time		VARCHAR(20) NOT NULL);