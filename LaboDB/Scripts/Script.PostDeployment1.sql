/*
Modèle de script de post-déploiement							
--------------------------------------------------------------------------------------
 Ce fichier contient des instructions SQL qui seront ajoutées au script de compilation.		
 Utilisez la syntaxe SQLCMD pour inclure un fichier dans le script de post-déploiement.			
 Exemple :      :r .\monfichier.sql								
 Utilisez la syntaxe SQLCMD pour référencer une variable dans le script de post-déploiement.		
 Exemple :      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/


INSERT INTO Country([name]) VALUES
    ('Belgique'),
    ('France'),
    ('Angleterre'),
    ('Etat-Unis'),
    ('Italy'),
    ('Luxembourg');

INSERT INTO City([name],postCode,country_Id) VALUES 
    ('Villers la ville',1495,1),
    ('Nivelles',1400,1),
    ('Wavre',1300,1),
    ('Californie',0000,4),
    ('Colorado',0000,4),
    ('Anderlecht',1070,1),
    ('Marseille',13284 ,2),
    ('Roseto',64026,5),
    ('Walhain',1457,1),
    ('Jodoigne',1370,1),
    ('Elouges',7370,1),
    ('Ekeren ',2030 ,1),
    ('Lultzhausen',9666,6),
    ('Floreffe',5150,1),
    ('Esneux',4130,1),
    ('Froidchapelle',6440,1),
    ('Lessines',7860,1),
    ('Sprimont',4140,1),
    ('Incourt',1315,1),
    ('Philippeville',5600,1),
    ('Cerfontaine',5630,1),
    ('Doische',5680,1);





INSERT INTO Adress(street,number,city_Id) VALUES
    ('rue du coin',5,1),
    ('rue de la bas',3,2),
    ('rue d ici',10,3),
    ('Rancho Santa Margarita',0,4),
    ('Fort Collins',0,5),
    ('Rue Jules Broeren',38,6),
    ('Quai de Rive Neuve',24,7),
    ('C/da Padune',11,8),
    ('Rue des Trois Fontaines',102,9),
    ('Rue des Carrières',1,10),
    ('Rue des Andrieux',174,11),
    ('Ekersedijk',1,12),
    ('Autour du lac',1,13),
    ('Rue Euriette',1,14),
    ('route Chera de la Gombe',2,15),
    ('Rue d oupiae',1,16),
    ('Rue de la Loge',80,17),
    ('Rue de Presseux',20,18),
    ('Rue de la Bruyère',1,19),
    ('Carrière de Rochefontaine',1,20),
    ('Rue du Traigneau',104,21),
    ('Route de Gimnée',1,22),
    ('Rue de Merlemont',26,20);






INSERT INTO [User]( firstname,lastname, email, passwd, phone, [role], birthDate, createdAt, isActive, adress_Id) VALUES
    ('Benjamin','Sterckx','ben@mail.be','Test1234=','0471/000000','super admin','1980-12-10', GETDATE(), 1, 1),
    ('Tom','Tom','tom@mail.be','Test1234=','0471/000000','admin','1980-12-10',GETDATE(),1,2),
    ('Jean','Jean','jean@mail.be','Test1234=','0471/000000','user','1980-12-10',GETDATE(),1,3);
    










    