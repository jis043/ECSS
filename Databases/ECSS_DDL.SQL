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


