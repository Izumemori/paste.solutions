CREATE DATABASE paste_solutions_dev;

CREATE TABLE snippets (
	id VARCHAR(20) PRIMARY KEY,
	content VARCHAR(1000000) NOT NULL,
	language VARCHAR(50),
	last_access timestamptz
);