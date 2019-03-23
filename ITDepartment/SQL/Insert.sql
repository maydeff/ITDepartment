INSERT INTO Resource VALUES
('Project'),
('ProjectUserRole'),
('Resource'),
('ResourceRole'),
('Role'),
('Sprint'),
('Task'),
('Test'),
('User'),
('UserRole')

INSERT INTO Role VALUES
('Software Developer'),
('Manager'),
('Leader'),
('Tester')
INSERT INTO [User] VALUES
('jpsikuta', HASHBYTES('SHA2_512','jpsikuta'), 'Jaros³aw', 'Psikuta'),
('agolota', HASHBYTES('SHA2_512','agolota'), 'Andrzej', 'Go³ota'),
('amalysz', HASHBYTES('SHA2_512', 'amalysz'), 'Adam', 'Ma³ysz'),
('mzegarek', HASHBYTES('SHA2_512','mzegarek'), 'Marek', 'Zegarek'),
('wwhite', HASHBYTES('SHA2_512','wwhite'), 'Walter', 'White'),
('mcorleone', HASHBYTES('SHA2_512','mcorleone'), 'Michael', 'Corleone')

