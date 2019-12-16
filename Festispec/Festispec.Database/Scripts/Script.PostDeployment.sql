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
INSERT INTO CaseStatus VALUES ('Gestart')
INSERT INTO CaseStatus VALUES ('Bezig')
INSERT INTO CaseStatus VALUES ('Klaar')
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

BEGIN TRANSACTION
INSERT INTO InspectorPlanningStatus VALUES ('Ingepland')
INSERT INTO InspectorPlanningStatus VALUES ('Afgerond')
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
-------------------------
BEGIN TRANSACTION
INSERT INTO Employee (Department, [Status], Firstname, Prefix, Lastname, Street, HouseNumber, [HouseNumber Addition], Birthday, PostalCode, City, Phone, Email, [Password])
VALUES ('Sales', 'Actief', 'Bart', NULL, 'Fransen', 'Burgemeester de leeuwstraat', 5, NULL, CAST('02-21-2012' AS DATETIME),'6651BW', 'Druten', '0642346706', 'Bartfransen66@gmail.com', 'JanSmitLover69')
INSERT INTO Employee (Department, [Status], Firstname, Prefix, Lastname, Street, HouseNumber, [HouseNumber Addition], Birthday, PostalCode, City, Phone, Email, [Password])
VALUES ('Inspectie', 'Actief', 'Jesse', NULL, 'Kuijpers', 'Roodhekkenpas', 16, 'a', CAST('02-21-2012' AS DATETIME),'6651CZ','Druten', '0487515968', 'j.Kuijpers@gmail.com', 'AnneJesse15')
INSERT INTO Employee (Department, [Status], Firstname, Prefix, Lastname, Street, HouseNumber, [HouseNumber Addition], Birthday, PostalCode, City, Phone, Email, [Password])
VALUES ('Inspectie', 'Actief', 'Thijs', NULL, 'Deenik', 'Hegakker', 74, NULL, CAST('02-21-2012' AS DATETIME),'6652BC', 'Druten','0487516859', 't.deenik@gmail.com', 'a???k*ix??t????;iL%???g?????$?')
INSERT INTO Employee (Department, [Status], Firstname, Prefix, Lastname, Street, HouseNumber, [HouseNumber Addition], Birthday, PostalCode, City, Phone, Email, [Password])
VALUES ('Marketing', 'Actief', 'Martina', 'van', 'Dinteren', 'De Tolboom', 15, NULL,CAST('02-21-2012' AS DATETIME),'6654BT','Affereden', '0685463584', 'MvDinteren@dinter.nl', 'bunny952')
INSERT INTO Employee (Department, [Status], Firstname, Prefix, Lastname, Street, HouseNumber, [HouseNumber Addition], Birthday, PostalCode, City, Phone, Email, [Password])
VALUES ('Planning', 'Actief', 'Martijn', NULL, 'Huisman', 'Grasakker', 76, 'a', CAST('02-21-2012' AS DATETIME),'6652BC', 'Druten','0695438736', 'M.Huisman@hotmail.com', 'DafTrucks2006')
INSERT INTO Employee (Department, [Status], Firstname, Prefix, Lastname, Street, HouseNumber, [HouseNumber Addition], Birthday, PostalCode, City, Phone, Email, [Password])
VALUES ('Directie', 'Actief', 'Mark', NULL, 'Peeters', 'Grasakker', 5, 'b', CAST('02-21-1960' AS DATETIME),'6652BC', 'Eindhoven','0695438736', 'm.peeters@gmail.com', 'a???k*ix??t????;iL%???g?????$?')
COMMIT TRANSACTION
-------- END EMPLOYEE DATA ------------

-------- BEGIN CUSTOMER DATA ------------
BEGIN TRANSACTION
INSERT INTO Customer ([Name], COC, BranchNumber, Street, HouseNumber, [HouseNumber Addition], PostalCode, City, Phone, Email, Website)
VALUES ('Qdance', 34212891, 000019745419, 'Noordeinde', 124, 'B', '1121AL', 'Landsmeer', '0642346760', 'info@qdance.nl', 'q-dance.com')
INSERT INTO Customer ([Name], COC, BranchNumber, Street, HouseNumber, [HouseNumber Addition], PostalCode, City, Phone, Email, Website)
VALUES ('ZiggoDome', 27284868, 000016205022, 'Holterbergweg', 3, NULL, '1101CE', 'Amsterdam', '0900-2353663', 'info@musicdome.nl', 'ziggodome.nl')
INSERT INTO Customer ([Name], COC, BranchNumber, Street, HouseNumber, [HouseNumber Addition], PostalCode, City, Phone, Email, Website)
VALUES ('AFAS live', 27173176, 000018225950, 'ArenA Boulevard', 590, NULL, '1101DS', 'Amsterdam Zuidoost', '0900-6874242', 'info@afaslive.nl', 'afaslive.nl')
COMMIT TRANSACTION

BEGIN TRANSACTION
INSERT INTO ContactPerson VALUES ('Stephan', 'de', 'Vries', '0684523495', 's.devries@qdance.com', 'vergunning regelaar', 1)
INSERT INTO ContactPerson VALUES ('Jan', 'van de', 'Berg', '065846852', 'jvd.Berg@qdance.com', 'vergunning regelaar', 1)
INSERT INTO ContactPerson VALUES ('Mark', NULL, 'Kampen', '065468435', 'm.Kampen@musicdome.com', 'vergunning regelaar', 2)
INSERT INTO ContactPerson VALUES ('Harrie', NULL, 'Manen', '068434834', 'H.Manen@musicdome.com', 'vergunning regelaar', 3)
COMMIT TRANSACTION

BEGIN TRANSACTION
INSERT INTO [Event] VALUES (1,1,'Defqon 1','Spijkweg', 30, NULL, '8256RJ', 'Biddinghuizen', '20190628 11:00:00', '20190630 11:00:00', 30000, 20000, 'Dance evenement')
INSERT INTO [Event] VALUES (1,2,'Xqlusive Holland','Holterbergweg', 3, NULL, '1101CE', 'Amsterdam', '20200919 23:00:00', '20200920 07:00:00', 8100, 6300, 'Hollands dance evenement')
INSERT INTO [Event] VALUES (2,3,'Ed Sheeran in Concert','Holterbergweg', 3, NULL, '1101CE', 'Amsterdam', '20210216 17:00:00', '20210216 23:00:00', 8100, 17000, 'Concert van Ed Sheeran')
INSERT INTO [Event] VALUES (3,4,'Snollebollekes','ArenA Boulevard', 590, NULL, '1101DS', 'Amsterdam Zuidoost', '20200420 17:00:00', '20200421 01:30:00', 3000, 6000, 'Snollebollekes in concert')
COMMIT TRANSACTION

BEGIN TRANSACTION
INSERT INTO Quotation VALUES (1,1,1, 20000, 9, '20190708 10:00:00', 'Geaccepteerd', 'Rapportage Defqon 1 2019')
INSERT INTO Quotation VALUES (1,1,2, 12000, 9, '20201001 10:00:00', 'Geaccepteerd', 'Rapportage Xqlusive Holland 2020')
INSERT INTO Quotation VALUES (2,1,3, 6000, 9, '20210302 10:00:00', 'Geaccepteerd', 'Rapportage Ed Sheeran in Concert 2021')
INSERT INTO Quotation VALUES (3,1,4, 9000, 9, '20200501 10:00:00', 'Geaccepteerd', 'Rapportage Snollebollekes 2020')
COMMIT TRANSACTION

BEGIN TRANSACTION
INSERT INTO Note VALUES (1, 'alleen bellen tussen 08:00 en 17:00', '20190626 10:00:00')
INSERT INTO Note VALUES (2, 'alleen bellen tussen 10:00 en 15:00', '20200917 10:00:00')
INSERT INTO Note VALUES (3, 'Kan de hele dag gebeld worden', '20210214 10:00:00')
INSERT INTO Note VALUES (4, 'Bereikbaar op maandag, dinsdag, donderdag tussen 09:00 en 15:00', '20200418 10:00:00')
COMMIT TRANSACTION
-------- END CUSTOMER DATA ------------

-------- BEGIN ORDER DATA ------------
BEGIN TRANSACTION
INSERT INTO [Order] VALUES (1,1,4,1, 'Order is afgerond')
INSERT INTO [Order] VALUES (1,2,4,2, 'Order is opgenomen en in behandeling')
INSERT INTO [Order] VALUES (2,3,4,3, 'Order momenteel in aanvraag')
INSERT INTO [Order] VALUES (3,4,4,4, 'Order momenteel in aanvraag')
COMMIT TRANSACTION
-------- END ORDER DATA ------------

-------- BEGIN SURVEY DATA ------------
BEGIN TRANSACTION
INSERT INTO Survey VALUES (1, 'Definitief', 'Survey voor Defqon 1')
INSERT INTO Survey VALUES (2, 'Concept', 'Survey voor Xqlusive Holland')
INSERT INTO Survey VALUES (3, 'Definitief', 'Survey voor Ed Sheeran in Concert')
INSERT INTO Survey VALUES (4, 'Concept', 'Survey voor Snollebollekes')
COMMIT TRANSACTION
-------- END SURVEY DATA ------------

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

BEGIN TRANSACTION
INSERT INTO Question VALUES (1, 'Gesloten vraag', '{"Question":"Bent u vaker op dit festival geweest?","Choices":{"Cols":["Ja","Nee"],"Options":[]},"Description":"Vind ik gewoon interessant","Images":[]}', 1, 'V1Defqon1')
INSERT INTO Question VALUES (1, 'Meerkeuze vraag', '{"Question":"Welke acts vond u een van de betere","Choices":{"Cols":["Act 1","Act 2"],"Options":[]},"Description":"Vind ik gewoon interessant","Images":[]}', 2, 'V2Defqon1')
INSERT INTO Question VALUES (1, 'Open vraag', '{"Question":"Wat vond u van de kaartjes controlen?","Choices":{"Cols":[],"Options":[]},"Description":"Gewoon om te testen","Images":[]}', 3, 'V3Defqon1')
INSERT INTO Question VALUES (2, 'Gesloten vraag', '{"Question":"Heeft u het naar uw zin?","Choices":{"Cols":["Ja","Nee"],"Options":[]},"Description":"Vind ik gewoon interessant","Images":[]}', 1, 'V1Xqlusive')
INSERT INTO Question VALUES (2, 'Meerkeuze vraag', '{"Question":"Welke acts zou u liever gezien hebben?","Choices":{"Cols":["Ran-D","Bassmodulators", "Sefa"],"Options":[]},"Description":"Vind ik gewoon interessant","Images":[]}', 2, 'V2Xqlusive')
INSERT INTO Question VALUES (2, 'Open vraag', '{"Question":"Wat vond u van de afsluiting en is hier verbetering?","Choices":{"Cols":[],"Options":[]},"Description":"Gewoon om te testen","Images":[]}', 3, 'V3Xqlusive')
INSERT INTO Question VALUES (3, 'Gesloten vraag', '{"Question":"Hoe vaak heeft u een concert van deze artiest bezocht?","Choices":{"Cols":["Eerste keer","al een paar keer"],"Options":[]},"Description":"Vind ik gewoon interessant","Images":[]}', 1, 'V1EdSheeran')
INSERT INTO Question VALUES (3, 'Open vraag', '{"Question":"Hoe verliep de kaartjes controlen?","Choices":{"Cols":[],"Options":[]},"Description":"Gewoon om te testen","Images":[]}', 2, 'V2EdSheeran')
INSERT INTO Question VALUES (3, 'Open vraag', '{"Question":"Zou u het concert aanraden aan anderen?","Choices":{"Cols":[],"Options":[]},"Description":"Gewoon om te testen","Images":[]}', 3, 'V2EdSheeran')
INSERT INTO Question VALUES (4, 'Gesloten vraag', '{"Question":"Hoe vaak heeft u dit festival bezocht","Choices":{"Cols":["Eerste keer","al een paar keer", "ik kom al sinds het begin"],"Options":[]},"Description":"Vind ik gewoon interessant","Images":[]}', 1, 'V1Snollebollekes')
INSERT INTO Question VALUES (4, 'Schuifbalk vraag', '{"Question":"Geef tussen de 1 en de 10 aan wat u van de sfeer vind.","Choices":{"Cols":["0","10"],"Options":[]},"Description":"","Images":[]}', 2, 'V2Snollebollekes')
INSERT INTO Question VALUES (4, 'Open vraag', '{"Question":"Ben je al bij veel optredens geweest van Snollebollekes?","Choices":{"Cols":[],"Options":[]},"Description":"Gewoon om te testen","Images":[]}', 3, 'V3Snollebollekes')
COMMIT TRANSACTION

BEGIN TRANSACTION
INSERT INTO [Case] VALUES (2, 3, 'Klaar')
INSERT INTO [Case] VALUES (2, 2, 'Bezig')
INSERT INTO [Case] VALUES (2, 3, 'Gestart')
COMMIT TRANSACTION

BEGIN TRANSACTION
INSERT INTO Answer VALUES (1, 1, 'al 2 keer')
INSERT INTO Answer VALUES (2, 1, 'Poeh niet zoveel eigenlijk heb mijn lievelings artiesten wel gezien!')
INSERT INTO Answer VALUES (3, 1, 'Afsluiting was gaaf! hoeft niks aan toegevoegd te worden')
INSERT INTO Answer VALUES (1, 2, 'Dit is mijn eerste keer')
INSERT INTO Answer VALUES (2, 2, 'Ik miste toch wel Ran-D')
INSERT INTO Answer VALUES (3, 2, '')
INSERT INTO Answer VALUES (1, 3, '')
INSERT INTO Answer VALUES (2, 3, '')
INSERT INTO Answer VALUES (3, 3, '')
COMMIT TRANSACTION
-------- END QUESTION DATA ------------

-------- BEGIN INSPECTOR PLANNING DATA ------------
BEGIN TRANSACTION
INSERT INTO AvailabilityInspector VALUES (2, '20190501', '20190531')
INSERT INTO AvailabilityInspector VALUES (2, '20190601', '20190630')
INSERT INTO AvailabilityInspector VALUES (2, '20190701', '20190731')
INSERT INTO AvailabilityInspector VALUES (3, '20190701', '20190731')
INSERT INTO AvailabilityInspector VALUES (3, '20190801', '20190830')
INSERT INTO AvailabilityInspector VALUES (3, '20190901', '20190731')
COMMIT TRANSACTION

BEGIN TRANSACTION
INSERT INTO [Day] VALUES (1, '20190628 10:00:00', '20190628 17:00:00')
INSERT INTO [Day] VALUES (1, '20190628 17:00:00', '20190629 01:00:00')
INSERT INTO [Day] VALUES (1, '20190629 10:00:00', '20190629 17:00:00')
INSERT INTO [Day] VALUES (1, '20190629 17:00:00', '20190630 01:00:00')
INSERT INTO [Day] VALUES (1, '20190630 10:00:00', '20190630 16:00:00')
INSERT INTO [Day] VALUES (1, '20190630 16:00:00', '20190630 23:00:00')
INSERT INTO [Day] VALUES (2, '20200919 23:00:00', '20200920 07:00:00')
COMMIT TRANSACTION

BEGIN TRANSACTION
INSERT INTO InspectorPlanning VALUES (2,1,1, '20190628 10:00:00', '20190628 17:00:00', 'Afgerond', NULL, NULL)
INSERT INTO InspectorPlanning VALUES (3,1,1, '20190628 10:00:00', '20190628 17:00:00', 'Afgerond', NULL, NULL)
INSERT INTO InspectorPlanning VALUES (3,2,1, '20190628 17:00:00', '20190629 01:00:00', 'Afgerond', NULL, NULL)
INSERT INTO InspectorPlanning VALUES (2,3,1, '20190629 10:00:00', '20190628 17:00:00', 'Afgerond', NULL, NULL)
INSERT INTO InspectorPlanning VALUES (3,4,1, '20190628 17:00:00', '20190629 01:00:00', 'Afgerond', NULL, NULL)
INSERT INTO InspectorPlanning VALUES (2,5,1, '20190628 10:00:00', '20190628 16:00:00', 'Afgerond', NULL, NULL)
INSERT INTO InspectorPlanning VALUES (3,6,1, '20190628 16:00:00', '20190629 23:00:00', 'Afgerond', NULL, NULL)
INSERT INTO InspectorPlanning VALUES (2,7,2, '20200919 23:00:00', '20200920 07:00:00', 'Ingepland', NULL, NULL)
INSERT INTO InspectorPlanning VALUES (3,7,2, '20200919 23:00:00', '20200920 07:00:00', 'Ingepland', NULL, NULL)
COMMIT TRANSACTION

--BEGIN TRANSACTION
--INSERT INTO InspectorPlanning VALUES (2,1,1, 'Afgerond', NULL, NULL)
--INSERT INTO InspectorPlanning VALUES (3,1,1, 'Aanvraag', NULL, NULL)
--INSERT INTO InspectorPlanning VALUES (3,2,1, 'Afgerond', NULL, NULL)
--INSERT INTO InspectorPlanning VALUES (2,3,1, 'Afgerond', NULL, NULL)
--INSERT INTO InspectorPlanning VALUES (3,4,1, 'Afgerond', NULL, NULL)
--INSERT INTO InspectorPlanning VALUES (2,5,1, 'Afgerond', NULL, NULL)
--INSERT INTO InspectorPlanning VALUES (3,6,1, 'Afgerond', NULL, NULL)
--INSERT INTO InspectorPlanning VALUES (2,7,2, 'Ingepland', NULL, NULL)
--INSERT INTO InspectorPlanning VALUES (3,7,2, 'Aanvraag', NULL, NULL)
--COMMIT TRANSACTION

BEGIN TRANSACTION
INSERT INTO SickReportInspector VALUES (1,3,1,1, 'Ziekenhuis bezoek')
COMMIT TRANSACTION

BEGIN TRANSACTION
INSERT INTO BetterReportInspector VALUES ('20190628 16:00:00', 3)
COMMIT TRANSACTION

BEGIN TRANSACTION
INSERT INTO CertificateInspector VALUES (2, '20150615', '20200615')
INSERT INTO CertificateInspector VALUES (3, '20180620', '20230620')
COMMIT TRANSACTION

-------- END INSPECTOR PLANNING DATA ------------

-------- BEGIN REPORT DATA ------------
BEGIN TRANSACTION
INSERT INTO Report VALUES (1, 'Definitief', 'Defqon Rapport')
INSERT INTO Report VALUES (2, 'Concept', 'Xqlusive Holland Rapport')
INSERT INTO Report VALUES (3, 'Concept', 'Ed Sheeran in Concert Rapport')
INSERT INTO Report VALUES (4, 'Concept', 'Snollebollekes Rapport')
COMMIT TRANSACTION

BEGIN TRANSACTION
INSERT INTO ElementType VALUES ('table')
INSERT INTO ElementType VALUES ('linechart')
INSERT INTO ElementType VALUES ('piechart')
INSERT INTO ElementType VALUES ('barchart')
INSERT INTO ElementType VALUES ('image')
INSERT INTO ElementType VALUES ('text')
COMMIT TRANSACTION


BEGIN TRANSACTION
INSERT INTO ReportElement VALUES (1,'text', 'Begin tekst', 'Inleiding', 1, NULL ,NULL , NULL)
INSERT INTO ReportElement VALUES (1,'image', 'Loop Route', 'Inleiding', 2, NULL ,NULL , NULL)
INSERT INTO ReportElement VALUES (1,'linechart', 'Tevredenheid', 'Inleiding', 3, NULL , 'Mensen ','Aantal')
INSERT INTO ReportElement VALUES (2,'text', 'Begin tekst', 'Inleiding', 1, NULL, NULL , NULL)
INSERT INTO ReportElement VALUES (2,'image', 'Loop Route', 'Inleiding', 2, NULL, NULL , NULL)
INSERT INTO ReportElement VALUES (2,'table', 'Artiest Lineup Rating', 'Inleiding', 3, NULL, NULL , NULL)
INSERT INTO ReportElement VALUES (3,'text', 'Begin tekst', 'Inleiding', 1, NULL, NULL , NULL)
INSERT INTO ReportElement VALUES (3,'image', 'Loop Route', 'Inleiding', 2, NULL, NULL , NULL)
INSERT INTO ReportElement VALUES (3,'piechart', 'Genuttigde dranken', 'Inleiding', 3, NULL , NULL , NULL)
INSERT INTO ReportElement VALUES (4,'text', 'Begin tekst', 'Inleiding', 1, NULL , NULL , NULL)
INSERT INTO ReportElement VALUES (4,'image', 'Loop Route', 'Inleiding', 2, NULL , NULL , NULL)
INSERT INTO ReportElement VALUES (4,'barchart', 'Beste act', 'Inleiding', 3,NULL , 'Act','Aantal')
COMMIT TRANSACTION
-------- END REPORT DATA ------------
