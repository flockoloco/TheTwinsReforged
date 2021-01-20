CREATE DATABASE TheTwins;
USE TheTwins;
CREATE TABLE User(
    UserID int auto_increment not null, 
    UserName varchar(255),
    UserPassword varchar(255),
    primary key (UserID)
);
CREATE TABLE MainGame(
    UserID_FK_MainGame int,
    eSwordID int,
    eArmorID int,
    currentLvl int,
    currentHP int,
    oreArrowAmount int,
    normalArrowAmount int,
    potsAmount int,
    gold int,
    constraint foreign key (UserID_FK_MainGame) references User(UserID)
    );
CREATE TABLE CurrentEnchant(
    UserID_FK_Enchant int,
    e0tier int,
    e1tier int,
    e2tier int,
    e3tier int,
    e4tier int,
    e5tier int,
    e6tier int,
    e7tier int,
    constraint foreign key (UserID_FK_Enchant) references User(UserID)
    );
create table GameCurrency(
    UserID_Fk_GameCurrency int,
    Ores int,
    Bars int,
    constraint foreign key (UserID_FK_GameCurrency) references User(UserID)
);
CREATE TABLE Delivery(
    UserID_FK_Delivery int,
    BarsAmount int,
    OresAmount int,
    DeliveryType int,
    constraint foreign key (UserID_FK_Delivery) references User(UserID)
);
CREATE TABLE CApp(
    UserID_FK_CApp int,
    Gold int,
    Nuggets int,
    Bars int,
    MineSpd int,
    MineHarvest int,
    PermUpgrade int,
    FirstTime int,
    constraint foreign key(UserID_FK_CApp) references User(UserID)
);