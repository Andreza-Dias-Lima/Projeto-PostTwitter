USE master
GO

IF NOT EXISTS (
  SELECT [name]
    FROM sys.databases
    WHERE [name] = N'PostTwitter'
)
CREATE DATABASE PostTwitter
GO

--------------------------------------------------
USE PostTwitter
GO

CREATE SCHEMA Twitter;
GO

----------- [Twitter].[Post] ---------------------
IF OBJECT_ID('[Twitter].[Post]', 'U') IS NOT NULL
DROP TABLE [Twitter].[Post]
GO
CREATE TABLE [Twitter].[Post] (
    Id         INT PRIMARY KEY IDENTITY(1,1),
    Descricao  VARCHAR(MAX),
	Data       DATETIME,
	Ativo      BIT DEFAULT 1,
	IdUsuario  INT NOT NULL,
	Tag        VARCHAR(100),
	Local      VARCHAR(100)
);
GO
----------- [Twitter].[Usuario] ---------------------
IF OBJECT_ID('[Twitter].[Usuario]', 'U') IS NOT NULL
DROP TABLE [Twitter].[Usuario]
GO
CREATE TABLE [Twitter].[Usuario] (
    Id             INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    Nome           VARCHAR(100),
	Login          VARCHAR(100),
    QtdeSeguidores INT,
	Ativo          BIT DEFAULT 1,
	Cd_Twitter     VARCHAR(MAX)
);
GO

ALTER TABLE [Twitter].[Post]
ADD FOREIGN KEY(IdUsuario) REFERENCES Twitter.Usuario

GO

-- a. Quais são os 5 (cinco) usuários, da amostra coletada, que possuem mais seguidores?
SELECT TOP 5 
        t.Nome
	   ,t.Login
	   ,t.QtdeSeguidores
FROM Twitter.usuario AS t (NOLOCK)
GROUP BY Nome, QtdeSeguidores, Login
ORDER BY QtdeSeguidores DESC

---b. Qual o total de postagens, agrupadas por hora do dia (independentemente da #hashtag)? 
SELECT COUNT(p.Id) 'Total', convert(varchar,p.Data,103) 'Data'
FROM Twitter.Post AS p (NOLOCK)
GROUP BY convert(varchar,p.Data,103)
ORDER BY Data

-- c. Qual o total de postagens para cada uma das #tag por idioma/país do usuário que postou;
SELECT COUNT(p.Id) 'Total'
      ,p.Tag
	  ,CASE WHEN len(p.Local) = 0 THEN 'NÃO INFORMADO' ELSE p.Local END Local 
FROM Twitter.Post AS p (NOLOCK)
GROUP BY p.Tag, p.Local
ORDER BY p.Tag