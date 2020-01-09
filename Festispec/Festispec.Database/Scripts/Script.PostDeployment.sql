/*
Author:			Bart Fransen
Date:			11/11/2019
Assignment:		Project (DeploymentScript)
*/

-------- BEGIN STATUS DATA ------------
BEGIN TRANSACTION
INSERT INTO EmployeeStatus VALUES ('Actief')
INSERT INTO EmployeeStatus VALUES ('Non-Actief')
INSERT INTO EmployeeStatus VALUES ('Ontslagen')
COMMIT TRANSACTION

BEGIN TRANSACTION
INSERT INTO QuotationStatus VALUES ('Open')
INSERT INTO QuotationStatus VALUES ('Geaccepteerd')
INSERT INTO QuotationStatus VALUES ('Geweigerd')
COMMIT TRANSACTION

BEGIN TRANSACTION
INSERT INTO SurveyStatus VALUES ('Concept')
INSERT INTO SurveyStatus VALUES ('Definitief')
COMMIT TRANSACTION

BEGIN TRANSACTION
INSERT INTO ReportStatus VALUES ('Concept')
INSERT INTO ReportStatus VALUES ('Definitief')
COMMIT TRANSACTION
-------- END STATUS DATA ------------

-------- BEGIN EMPLOYEE DATA ------------
BEGIN TRANSACTION
INSERT INTO Department VALUES ('Sales', 'Verkoop afdeling')
INSERT INTO Department VALUES ('Marketing', 'Zorgt voor reclame en sponsoren')
INSERT INTO Department VALUES ('Inspectie', 'Mensen die de daadwerkelijke inspectie doen')
INSERT INTO Department VALUES ('Planning', 'Zorgt voor het inplannen van inspecties en inspecteurs')
INSERT INTO Department VALUES ('Directie', 'Directie die overal bij kan')
COMMIT TRANSACTION
--- Password of users ---
-- Inspectie: t.deenik@gmail.com, GamerBoy95
-- Directie:  m.peeters@gmail.com, GamerBoy95
-- Iedere gebruiker heeft als wachtwoord: GamerBoy95
-------------------------
BEGIN TRANSACTION
INSERT INTO Employee (Department, [Status], Firstname, Prefix, Lastname, Street, HouseNumber, [HouseNumber Addition], Birthday, PostalCode, City, Phone, Email, [Password])
VALUES ('Sales', 'Actief', 'Bart', NULL, 'Fransen', 'Burgemeester de leeuwstraat', 5, NULL, CAST('02-21-2012' AS DATETIME),'6651BW', 'Druten', '0642346706', 'Bartfransen66@gmail.com', 'a???k*ix??t????;iL%???g?????$?')
INSERT INTO Employee (Department, [Status], Firstname, Prefix, Lastname, Street, HouseNumber, [HouseNumber Addition], Birthday, PostalCode, City, Phone, Email, [Password])
VALUES ('Inspectie', 'Actief', 'Jesse', NULL, 'Kuijpers', 'Roodhekkenpas', 16, 'a', CAST('02-21-2012' AS DATETIME),'6651CZ','Druten', '0487515968', 'j.Kuijpers@gmail.com', 'a???k*ix??t????;iL%???g?????$?')
INSERT INTO Employee (Department, [Status], Firstname, Prefix, Lastname, Street, HouseNumber, [HouseNumber Addition], Birthday, PostalCode, City, Phone, Email, [Password])
VALUES ('Inspectie', 'Actief', 'Thijs', NULL, 'Deenik', 'Hegakker', 74, NULL, CAST('02-21-2012' AS DATETIME),'6652BC', 'Druten','0487516859', 't.deenik@gmail.com', 'a???k*ix??t????;iL%???g?????$?')
INSERT INTO Employee (Department, [Status], Firstname, Prefix, Lastname, Street, HouseNumber, [HouseNumber Addition], Birthday, PostalCode, City, Phone, Email, [Password])
VALUES ('Marketing', 'Actief', 'Martina', 'van', 'Dinteren', 'De Tolboom', 15, NULL,CAST('02-21-2012' AS DATETIME),'6654BT','Affereden', '0685463584', 'MvDinteren@dinter.nl', 'a???k*ix??t????;iL%???g?????$?')
INSERT INTO Employee (Department, [Status], Firstname, Prefix, Lastname, Street, HouseNumber, [HouseNumber Addition], Birthday, PostalCode, City, Phone, Email, [Password])
VALUES ('Planning', 'Actief', 'Martijn', NULL, 'Huisman', 'Grasakker', 76, 'a', CAST('02-21-2012' AS DATETIME),'6652BC', 'Druten','0695438736', 'M.Huisman@hotmail.com', 'a???k*ix??t????;iL%???g?????$?')
INSERT INTO Employee (Department, [Status], Firstname, Prefix, Lastname, Street, HouseNumber, [HouseNumber Addition], Birthday, PostalCode, City, Phone, Email, [Password])
VALUES ('Directie', 'Actief', 'Mark', NULL, 'Peeters', 'Grasakker', 5, 'b', CAST('02-21-1960' AS DATETIME),'6652BC', 'Eindhoven','0695438736', 'm.peeters@gmail.com', 'a???k*ix??t????;iL%???g?????$?')
COMMIT TRANSACTION
-------- END EMPLOYEE DATA ------------

-------- BEGIN QUESTION DATA ------------
BEGIN TRANSACTION
INSERT INTO QuestionType VALUES ('Afbeelding galerij vraag')
INSERT INTO QuestionType VALUES ('Gesloten vraag')
INSERT INTO QuestionType VALUES ('Meerkeuze vraag')
INSERT INTO QuestionType VALUES ('Open vraag')
INSERT INTO QuestionType VALUES ('Opmerking vraag')
INSERT INTO QuestionType VALUES ('Schuifbalk vraag')
INSERT INTO QuestionType VALUES ('Tabel vraag')
INSERT INTO QuestionType VALUES ('Teken vraag')
COMMIT TRANSACTION
-------- END INSPECTOR PLANNING DATA ------------

-------- BEGIN REPORT DATA ------------
BEGIN TRANSACTION
INSERT INTO ElementType VALUES ('Table')
INSERT INTO ElementType VALUES ('Linechart')
INSERT INTO ElementType VALUES ('Piechart')
INSERT INTO ElementType VALUES ('Barchart')
INSERT INTO ElementType VALUES ('Image')
INSERT INTO ElementType VALUES ('Text')
INSERT INTO ElementType VALUES ('Draw')
COMMIT TRANSACTION
-------- END REPORT DATA ------------