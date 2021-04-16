USE cwfinal;

-- CREATE TABLE profiles
-- (
--     id VARCHAR(255) NOT NULL,
--     email VARCHAR(255) NOT NULL,
--     name VARCHAR(255),
--     picture VARCHAR(255),

--     PRIMARY KEY(id)
-- );

-- CREATE TABLE vaults
-- (
--     id int NOT NULL AUTO_INCREMENT,
--     creatorId VARCHAR(255) NOT NULL,
--     name VARCHAR(255),
--     description VARCHAR(255),
--     isPrivate TINYINT,

--     PRIMARY KEY (id),

--     FOREIGN KEY (creatorId)
--     REFERENCES profiles (id)
--     ON DELETE CASCADE
-- );

-- CREATE TABLE keeps
-- (
--     id int NOT NULL AUTO_INCREMENT,
--     creatorId VARCHAR(255) NOT NULL,
--     name VARCHAR(255),
--     description VARCHAR(255),
--     img VARCHAR(255),
--     views int,
--     shares int,
--     keeps int,

--     PRIMARY KEY (id),

--     FOREIGN KEY (creatorId)
--     REFERENCES profiles (id)
--     ON DELETE CASCADE
-- );

-- CREATE TABLE vaultkeeps
-- (
--   id INT NOT NULL AUTO_INCREMENT, 
--   creatorId VARCHAR(255),
--   vaultId INT,
--   KeepId INT,
--   PRIMARY KEY (id),

--    FOREIGN KEY (creatorId)
--    REFERENCES profiles (id)
--    ON DELETE CASCADE,

--   FOREIGN KEY (vaultId)
--    REFERENCES vaults (id)
--    ON DELETE CASCADE,

--   FOREIGN KEY (keepId)
--    REFERENCES keeps (id)
--    ON DELETE CASCADE
-- );
