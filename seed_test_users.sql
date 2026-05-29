-- Test seed data for AdminManage testing.
-- All passwords = "password123" (BCrypt hash, work factor 11).
-- Safe to re-run: skips any usernames already present.

DECLARE @pwd nvarchar(MAX) = '$2a$11$hz367tdr73sOfH6MRlJgeelcCt6cBQeIpHeMfxd6rSwLvb.FvAnpW';

INSERT INTO Users (Username, Password, FullName, Age, Gender, Role, IsActive, IsDeleted)
SELECT * FROM (VALUES
    ('sroge',     @pwd, 'Stephanie Rogers',  22, 'Female', 'Student', 1, 0),
    ('mchen',     @pwd, 'Marcus Chen',       28, 'Male',   'Teacher', 1, 0),
    ('priyas',    @pwd, 'Priya Sharma',      17, 'Female', 'Student', 1, 0),
    ('ahassan',   @pwd, 'Ahmad Hassan',      19, 'Male',   'Student', 1, 0),
    ('obennett',  @pwd, 'Olivia Bennett',    16, 'Female', 'Student', 0, 0),  -- disabled
    ('jwhit',     @pwd, 'James Whitaker',    45, 'Male',   'Teacher', 1, 0),
    ('sophiet',   @pwd, 'Sophie Tran',       15, 'Female', 'Student', 1, 0),
    ('dkow',      @pwd, 'Daniel Kowalski',   18, 'Male',   'Student', 1, 0),
    ('msantos',   @pwd, 'Maria Santos',      33, 'Female', 'Teacher', 1, 0),
    ('kenjiy',    @pwd, 'Kenji Yamamoto',    16, 'Male',   'Student', 1, 0),
    ('aishamo',   @pwd, 'Aisha Mohammed',    17, 'Female', 'Student', 0, 0),  -- disabled
    ('rklein',    @pwd, 'Robert Klein',      38, 'Male',   'Teacher', 1, 0),
    ('emmaw',     @pwd, 'Emma Wilson',       15, 'Female', 'Student', 1, 0),
    ('charlier',  @pwd, 'Charlie Reeves',    24, 'Male',   'Admin',   1, 0),
    ('lindap',    @pwd, 'Linda Park',        16, 'Female', 'Student', 1, 1)   -- soft-deleted
) AS v(Username, Password, FullName, Age, Gender, Role, IsActive, IsDeleted)
WHERE NOT EXISTS (SELECT 1 FROM Users u WHERE u.Username = v.Username);
