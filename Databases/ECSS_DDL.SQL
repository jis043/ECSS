CREATE TABLE `TRANSFORMER` (
  `PartID` VARCHAR(50) PRIMARY KEY, 
  Manufacturer VARCHAR(100),
  Certificates1 VARCHAR(50),
  Certificates2 VARCHAR(50),
  Height DOUBLE,
  Width DOUBLE,
  Depth DOUBLE,
  Power INTEGER,
  Frequency INTEGER,
  Phase1 INTEGER,
  Pri_Voltage INTEGER,
  Phase2 INTEGER,
  Sec_Voltage INTEGER,
  Keywords VARCHAR(200),
  Description TEXT,
);

CREATE TABLE THEROMOSTAT(
    PartID         VARCHAR (50)  PRIMARY KEY,
    Monut        VARCHAR (100),
	Rated_Vol_Max INTEGER,
	Rated_Current INTEGER,
	`Function`  VARCHAR (100),
	AdjustMin INTEGER,
	AdjustMax INTEGER,
	Opera_Temp_Min INTEGER,
	Opera_Temp_Max INTEGER,
	Weight       DOUBLE,
	Height         DOUBLE,
    Width          DOUBLE,
    Depth          DOUBLE,
	 Manufacturer   VARCHAR (100),
	Description  TEXT,
	 Certificates   VARCHAR (200),
	 Area_Class     VARCHAR (100),
    Class          VARCHAR (50),
    Gas_Group      VARCHAR (50),
    Temp_Code      VARCHAR (50),
    Link           TEXT,
    Keywords       VARCHAR (200)	
);

CREATE TABLE TEMP_SWITCH(
    PartID         VARCHAR (50)  PRIMARY KEY,
    Monut        VARCHAR (100),
	Rated_Vol_Max INTEGER,
	Rated_Current INTEGER,
	`Function`  VARCHAR (100),
	Adjustable INTEGER,
	Switch_Temp_ON INTEGER,
	Switch_Temp_OFF INTEGER,
	Opera_Temp_Min INTEGER,
	Opera_Temp_Max INTEGER,
	Weight       DOUBLE,
	Height         DOUBLE,
    Width          DOUBLE,
    Depth          DOUBLE,
	 Manufacturer   VARCHAR (100),
	Description  TEXT,
	 Certificates   VARCHAR (200),
	 Area_Class     VARCHAR (100),
    Class          VARCHAR (50),
    Gas_Group      VARCHAR (50),
    Temp_Code      VARCHAR (50),
    Link           TEXT,
    Keywords       VARCHAR (200)	
);


CREATE TABLE POWER_SUPPLY (
    PartID         VARCHAR (50)  PRIMARY KEY,
    Manufacturer   VARCHAR (100),
    Certificates   VARCHAR (200),
    Height         DOUBLE,
    Width          DOUBLE,
    Depth          DOUBLE,
    Output_Power   INTEGER,
    Output_Current DOUBLE,
    Output_Vol     INTEGER,
    Input_Vol      VARCHAR (100),
    Opera_temp_min INTEGER,
    Opera_temp_max INTEGER,
    Stor_temp_min  INTEGER,
    Stor_temp_max  INTEGER,
    Area_Class     VARCHAR (100),
    Class          VARCHAR (50),
    Division       VARCHAR (10),
    Gas_Group      VARCHAR (50),
    Temp_Code      VARCHAR (50),
    Link           TEXT,
    Keywords       VARCHAR (200),
    Description    TEXT
);

CREATE TABLE ENCLOSURE (
    PartID       VARCHAR (50)  PRIMARY KEY,
    Manufacturer VARCHAR (100),
    Material     VARCHAR (50),
    Certificates VARCHAR (300),
    Height       DOUBLE,
    Width        DOUBLE,
    Depth        DOUBLE,
    NEMA_Type    VARCHAR (50),
    Monut        VARCHAR (100),
    Color        VARCHAR (100),
    MountID      VARCHAR (300),
    MountHeight  DOUBLE,
    MountWidth   DOUBLE,
    Window       VARCHAR (20),
    PDF          VARCHAR (200),
    Link         TEXT,
    Description  TEXT,
    Keywords     VARCHAR (200) 
);


CREATE TABLE `SERVIT_POST` (
  `PartID` VARCHAR(50) PRIMARY KEY, 
  Manufacturer VARCHAR(100),
  Certificates1 VARCHAR(50),
  Certificates2 VARCHAR(50),
  Part_type VARCHAR(50),
  Range_Stranded VARCHAR(50),
  Range_Soild VARCHAR(50),
  Material  VARCHAR(50),
  Link Text, 
  Keywords VARCHAR(200),
  Description TEXT
);

CREATE TABLE `BREATHER_DRAIN` (
  `PartID` VARCHAR(50) PRIMARY KEY, 
  Manufacturer VARCHAR(100),
  Certificates1 VARCHAR(50),
  Certificates2 VARCHAR(50),
  Part_size VARCHAR(50),
  NEMA_Type VARCHAR(50),
  Material  VARCHAR(50),
  Link Text, 
  Keywords VARCHAR(200),
  Description TEXT
);

CREATE TABLE WINDOW_KIT (
    PartID       VARCHAR (50)  PRIMARY KEY,
   	NEMA_Type    VARCHAR (50),
    Material     VARCHAR (50),
   	Kit_Type VARCHAR(50),
	Color        VARCHAR (100),
	Weight DOUBLE,
    OveralHeight       INTEGER,
    OveralWidth        INTEGER,
	ViewHeight       INTEGER,
    ViewWidth        INTEGER,
	CutoutHeight       INTEGER,
    CutoutWidth        INTEGER,	
    Manufacturer VARCHAR (100),
   Description  TEXT, 
 Certificates VARCHAR (300),
   Link         TEXT,  
    Keywords     VARCHAR (200) 
);


CREATE TABLE PILOT_LIGHT(
	PartID  VARCHAR (50)  PRIMARY KEY,
	NEMA_TYPE  VARCHAR (50),
	Material  VARCHAR (50),
	Fingersafe  VARCHAR (100),
	Power_Module VARCHAR (100),
	Lamp_Test VARCHAR (100),
	Illumination VARCHAR (100),
	Voltage_Type VARCHAR (100),
	Voltage VARCHAR (100),
	Lens_Color VARCHAR (100),
	Contact_Blocks_Type VARCHAR (100),
	Contacts VARCHAR (100),
	Opera_temp_min  INTEGER,
	Opera_temp_max  INTEGER,
	Stor_temp_min  INTEGER,
	Stor_temp_max INTEGER,
	Weight DOUBLE,
	Height INTEGER,
	Width INTEGER,
	Depth INTEGER,
	Manufacturer VARCHAR (100),
	Description TEXT,
	Certificates VARCHAR (300),
	Area_Class VARCHAR (100),
	Class VARCHAR (100),
	Gas_Groups VARCHAR (100),
	Temp_Code VARCHAR (100),
	LINK TEXT,
	Keywords  VARCHAR (200) 
);

CREATE TABLE NON_ILLUMINATE(
	PartID  VARCHAR (50)  PRIMARY KEY,
	PartType INTEGER NOT NULL DEFAULT 0,
	NEMA_TYPE  VARCHAR (50),
	Material  VARCHAR (50),
	Fingersafe  VARCHAR (100),
	OperatorType VARCHAR (100),
	Functions VARCHAR (100),
	MushroomHead VARCHAR (100),
	Color VARCHAR (100),
	Contact_Blocks_Type VARCHAR (100),
	Contacts VARCHAR (100),
	Opera_temp_min  INTEGER,
	Opera_temp_max  INTEGER,
	Stor_temp_min  INTEGER,
	Stor_temp_max INTEGER,
	Manufacturer VARCHAR (100),
	Description TEXT,
	Certificates VARCHAR (300),
	Area_Class VARCHAR (100),
	Class VARCHAR (100),
	Gas_Groups VARCHAR (100),
	Temp_Code VARCHAR (100),
	LINK TEXT,
	Keywords  VARCHAR (200) 
);

CREATE TABLE HEATER(
	PartID  VARCHAR (50)  PRIMARY KEY,
	Power INTEGER,
	Rated_Vol_MIN INTEGER,
	Rated_Vol_Max INTEGER,
	Frequency VARCHAR (100),
	Weight DOUBLE,
	BuiltIn_Thermostat INTEGER,
	BuiltIn_Fan INTEGER,
	Height INTEGER,
	Width INTEGER,
	Depth INTEGER,
	Manufacturer VARCHAR (100),
	Description TEXT,
	Certificates VARCHAR (300),
	Area_Class VARCHAR (100),
	Class VARCHAR (100),
	Gas_Groups VARCHAR (100),
	Temp_Code VARCHAR (100),
	LINK TEXT,
	Datasheet VARCHAR (100),
	Keywords  VARCHAR (200) 
);


CREATE TABLE `SYSTEM_UNITS` ( 
  `Parameter` VARCHAR(100),
  Unit VARCHAR(100),
);

CREATE TABLE `BOMLIST` (
  BOMNAME VARCHAR(50) PRIMARY KEY, 
  BOMTITLE VARCHAR(200),
  CREATETIEM DATETIME, 
  STATUS VARCHAR(10) NOT NULL DEFAULT 'ENABLE'
);

CREATE TABLE `BOMITEM` (
 BOMNAME VARCHAR(50),
 PART_QTY INTEGER,
 PARTID VARCHAR(50),
 Manufacturer VARCHAR(100),
 Description TEXT,
 NOTE TEXT
);

CREATE TABLE `USERSETTINGS` (
MultiKeywords INTEGER DEFAULT -1,
MaxDisplay INTEGER DEFAULT 1000,
UnitType INTEGER DEFAULT 0
);
