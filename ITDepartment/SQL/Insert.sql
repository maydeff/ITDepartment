INSERT INTO Resource VALUES
('Project'),
('ProjectUserRole'),
('Sprint'),
('Task'),
('Release')

INSERT INTO Role VALUES
('Software Developer'),
('Manager'),
('Leader'),
('Tester')

INSERT INTO [User] VALUES
('kjarzyna', HASHBYTES('SHA2_512','kjarzyna'), 'Krzysztof', 'Jarzyna'),
('jpsikuta', HASHBYTES('SHA2_512','jpsikuta'), 'Jaros³aw', 'Psikuta'),
('agolota', HASHBYTES('SHA2_512','agolota'), 'Andrzej', 'Go³ota'),
('amalysz', HASHBYTES('SHA2_512', 'amalysz'), 'Adam', 'Ma³ysz')

INSERT INTO UserRole VALUES
(1, 4),
(1, 2),
(2, 3),
(3, 1),
(4, 4)

--ResourceId, RoleId, CanAdd, CanView, CanEdit, CanDelete
INSERT INTO ResourceRole VALUES
--Project
(1, 1, 0, 1, 0, 0),
(1, 2, 1, 1, 1, 1),
(1, 3, 0, 1, 1, 0),
(1, 4, 0, 0, 0, 0),

--ProjectUserRole - assigning someone to project
(2, 1, 0, 1, 0, 0),
(2, 2, 1, 1, 1, 1),
(2, 3, 1, 1, 0, 0),
(2, 4, 0, 0, 0, 0),

--Sprint 
(3, 1, 0, 1, 0, 0),
(3, 2, 1, 1, 0, 0),
(3, 3, 1, 1, 1, 0),
(3, 4, 0, 1, 0, 0),

--ResourceId, RoleId, CanAdd, CanView, CanEdit, CanDelete
--Task
(4, 1, 0, 1, 1, 0),
(4, 2, 1, 1, 1, 1),
(4, 3, 1, 1, 1, 0),
(4, 4, 0, 1, 0, 0),

--ResourceId, RoleId, CanAdd, CanView, CanEdit, CanDelete
--Release
(5, 1, 0, 1, 0, 0),
(5, 2, 1, 1, 1, 0),
(5, 3, 0, 1, 0, 0),
(5, 4, 0, 0, 0, 0)


