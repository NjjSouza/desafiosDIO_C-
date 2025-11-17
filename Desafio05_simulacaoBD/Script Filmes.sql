-- Devido a impossibilidade de instalar o SQL Server em minha máquina, utilizei o SQL Fiddle para as consultas, tendo que remover alguns comandos de ambiente e cláusulas de otimização que não são suportadas ou são redundantes no simulador, e simplificar a forma de inserção de dados.


-- REMOVIDOS: CREATE DATABASE [Filmes] e USE [Filmes]
-- REMOVIDOS: SET ANSI_NULLS ON e SET QUOTED_IDENTIFIER ON

CREATE TABLE [Atores](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PrimeiroNome] [varchar](20) NULL,
	[UltimoNome] [varchar](20) NULL,
	[Genero] [varchar](1) NULL,
 CONSTRAINT [PK_actor] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)

-- REMOVIDAS: Cláusulas de otimização (WITH...) e ON [PRIMARY]
);
-- GO removido

CREATE TABLE [ElencoFilme](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdAtor] [int] NOT NULL,
	[IdFilme] [int] NULL,
	[Papel] [varchar](30) NULL,
 CONSTRAINT [PK_ElencoFilme] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)
-- REMOVIDAS: Cláusulas de otimização (WITH...) e ON [PRIMARY]
);
-- GO removido

CREATE TABLE [Filmes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](50) NULL,
	[Ano] [int] NULL,
	[Duracao] [int] NULL,
 CONSTRAINT [PK_movie] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)
-- REMOVIDAS: Cláusulas de otimização (WITH...) e ON [PRIMARY]
);
-- GO removido

CREATE TABLE [FilmesGenero](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdGenero] [int] NULL,
	[IdFilme] [int] NULL,
 CONSTRAINT [PK_FilmesGenero] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)
-- REMOVIDAS: Cláusulas de otimização (WITH...) e ON [PRIMARY]
);
-- GO removido

CREATE TABLE [Generos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Genero] [varchar](20) NULL,
 CONSTRAINT [PK_genres] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)
-- REMOVIDAS: Cláusulas de otimização (WITH...) e ON [PRIMARY]
);
-- GO removido
-- Cláusula SET IDENTITY_INSERT e comandos GO entre as inserções removidos


-- Inserção de dados

INSERT INTO [Atores] ([PrimeiroNome], [UltimoNome], [Genero]) VALUES 
(N'James', N'Stewart', N'M'),
(N'Deborah', N'Kerr', N'F'),
(N'Peter', N'OToole', N'M'),
(N'Robert', N'DeNiro', N'M'),
(N'Harrison', N'Ford', N'M'),
(N'Stephen', N'Baldwin', N'M'),
(N'Jack', N'Nicholson', N'M'),
(N'Mark', N'Wahlberg', N'M'),
(N'Woody', N'Allen', N'M'),
(N'Claire', N'Danes', N'F'),
(N'Tim', N'Robbins', N'M'),
(N'Kevin', N'Spacey', N'M'),
(N'Kate', N'Winslet', N'F'),
(N'Robin', N'Williams', N'M'),
(N'Jon', N'Voight', N'M'),
(N'Ewan', N'McGregor', N'M'),
(N'Christian', N'Bale', N'M'),
(N'Maggie', N'Gyllenhaal', N'F'),
(N'Dev', N'Patel', N'M'),
(N'Sigourney', N'Weaver', N'F'),
(N'David', N'Aston', N'M'),
(N'Ali', N'Astin', N'F');


INSERT INTO [Filmes] ([Nome], [Ano], [Duracao]) VALUES 
(N'Um Corpo que Cai', 1958, 128),
(N'Os Inocentes', 1961, 100),
(N'Lawrence da Arábia', 1962, 216),
(N'O Franco Atirador', 1978, 183),
(N'Amadeus', 1984, 160),
(N'Blade Runner', 1982, 117),
(N'De Olhos Bem Fechados', 1999, 159),
(N'Os Suspeitos', 1995, 106),
(N'Chinatown', 1974, 130),
(N'Boogie Nights - Prazer Sem Limites', 1997, 155),
(N'Noivo Neurótico, Noiva Nervosa', 1977, 93),
(N'Princesa Mononoke', 1997, 134),
(N'Um Sonho de Liberdade', 1994, 142),
(N'Beleza Americana', 1999, 122),
(N'Titanic', 1997, 194),
(N'Gênio Indomável', 1997, 126),
(N'Amargo pesadelo', 1972, 109),
(N'Trainspotting - Sem Limites', 1996, 94),
(N'O Grande Truque', 2006, 130),
(N'Donnie Darko', 2001, 113),
(N'Quem Quer Ser um Milionário?', 2008, 120),
(N'Aliens, O Resgate', 1986, 137),
(N'Uma Vida sem Limites', 2004, 118),
(N'Avatar', 2009, 162),
(N'Coração Valente', 1995, 178),
(N'Os Sete Samurais', 1954, 207),
(N'A Viagem de Chihiro', 2001, 125),
(N'De Volta para o Futuro', 1985, 116);


INSERT INTO [Generos] ([Genero]) VALUES 
(N'Ação'),
(N'Aventura'),
(N'Animação'),
(N'Biografia'),
(N'Comédia'),
(N'Crime'),
(N'Drama'),
(N'Horror'),
(N'Musical'),
(N'Mistério'),
(N'Romance'),
(N'Suspense'),
(N'Guerra');


-- Inserções com referências: o SQL Fiddle irá correlacionar os IDs sequenciais

INSERT INTO [ElencoFilme] ([IdAtor], [IdFilme], [Papel]) VALUES 
(1, 1, N'John Scottie Ferguson'), -- James Stewart (Id 1), Um Corpo que Cai (Id 1)
(2, 2, N'Miss Giddens'), -- Deborah Kerr (Id 2), Os Inocentes (Id 2)
(3, 3, N'T.E. Lawrence'), -- Peter OToole (Id 3), Lawrence da Arábia (Id 3)
(4, 4, N'Michael'), -- Robert DeNiro (Id 4), O Franco Atirador (Id 4)
(5, 6, N'Rick Deckard'), -- Harrison Ford (Id 5), Blade Runner (Id 6)
(6, 8, N'McManus'), -- Stephen Baldwin (Id 6), Os Suspeitos (Id 8)
(7, 10, N'Eddie Adams'), -- Mark Wahlberg (Id 7), Boogie Nights (Id 10)
(8, 11, N'Alvy Singer'), -- Woody Allen (Id 8), Noivo Neurótico (Id 11)
(9, 12, N'San'), -- Claire Danes (Id 9), Princesa Mononoke (Id 12)
(10, 13, N'Andy Dufresne'), -- Tim Robbins (Id 10), Um Sonho de Liberdade (Id 13)
(11, 14, N'Lester Burnham'), -- Kevin Spacey (Id 11), Beleza Americana (Id 14)
(12, 15, N'Rose DeWitt Bukater'), -- Kate Winslet (Id 12), Titanic (Id 15)
(13, 16, N'Sean Maguire'), -- Robin Williams (Id 13), Gênio Indomável (Id 16)
(14, 17, N'Ed'), -- Jon Voight (Id 14), Amargo pesadelo (Id 17)
(15, 18, N'Renton'), -- Ewan McGregor (Id 15), Trainspotting (Id 18)
(16, 20, N'Elizabeth Darko'), -- Maggie Gyllenhaal (Id 16), Donnie Darko (Id 20)
(17, 21, N'Older Jamal'), -- Dev Patel (Id 17), Quem Quer Ser um Milionário? (Id 21)
(18, 22, N'Ripley'), -- Sigourney Weaver (Id 18), Aliens (Id 22)
(19, 23, N'Bobby Darin'), -- Kevin Spacey (Id 11), Uma Vida sem Limites (Id 23)
(20, 9, N'J.J. Gittes'), -- Jack Nicholson (Id 7), Chinatown (Id 9)
(21, 19, N'Alfred Borden'); -- Christian Bale (Id 17), O Grande Truque (Id 19)


INSERT INTO [FilmesGenero] ([IdGenero], [IdFilme]) VALUES 
(1, 22), -- Ação (Id 1), Aliens, O Resgate (Id 22)
(2, 17), -- Aventura (Id 2), Amargo pesadelo (Id 17)
(2, 3), -- Aventura (Id 2), Lawrence da Arábia (Id 3)
(3, 12), -- Animação (Id 3), Princesa Mononoke (Id 12)
(5, 11), -- Comédia (Id 5), Noivo Neurótico, Noiva Nervosa (Id 11)
(6, 8), -- Crime (Id 6), Os Suspeitos (Id 8)
(6, 13), -- Crime (Id 6), Um Sonho de Liberdade (Id 13)
(7, 26), -- Drama (Id 7), Os Sete Samurais (Id 26)
(7, 28), -- Drama (Id 7), De Volta para o Futuro (Id 28)
(7, 18), -- Drama (Id 7), Trainspotting - Sem Limites (Id 18)
(7, 21), -- Drama (Id 7), Quem Quer Ser um Milionário? (Id 21)
(8, 2), -- Horror (Id 8), Os Inocentes (Id 2)
(9, 23), -- Musical (Id 9), Uma Vida sem Limites (Id 23)
(10, 7), -- Mistério (Id 10), De Olhos Bem Fechados (Id 7)
(10, 27), -- Mistério (Id 10), A Viagem de Chihiro (Id 27)
(10, 1), -- Mistério (Id 10), Um Corpo que Cai (Id 1)
(11, 14), -- Romance (Id 11), Beleza Americana (Id 14)
(12, 6), -- Suspense (Id 12), Blade Runner (Id 6)
(13, 4); -- Guerra (Id 13), O Franco Atirador (Id 4)


-- Chaves Estrangeiras (Foreign Keys)
-- WITH CHECK ADD CONSTRAINT e CHECK CONSTRAINT simplificadas

ALTER TABLE [ElencoFilme] ADD CONSTRAINT [FK_ElencoFilme_IdAtor] FOREIGN KEY([IdAtor])
REFERENCES [Atores] ([Id]);

ALTER TABLE [ElencoFilme] ADD CONSTRAINT [FK_ElencoFilme_IdFilme] FOREIGN KEY([IdFilme])
REFERENCES [Filmes] ([Id]);

ALTER TABLE [FilmesGenero] ADD CONSTRAINT [FK_FilmesGenero_IdFilme] FOREIGN KEY([IdFilme])
REFERENCES [Filmes] ([Id]);

ALTER TABLE [FilmesGenero] ADD CONSTRAINT [FK_FilmesGenero_IdGenero] FOREIGN KEY([IdGenero])
REFERENCES [Generos] ([Id]);


-- Consultas em resposta ao desafio de projeto

-- 1 
SELECT Nome, Ano
FROM Filmes;

-- 2
SELECT Nome, Ano, Duracao
FROM Filmes
ORDER BY Ano ASC;

-- 3
SELECT Nome, Ano, Duracao
FROM Filmes
WHERE Nome = 'De Volta para o Futuro';

-- 4 
SELECT Nome, Ano, Duracao
FROM Filmes
WHERE Ano = 1997;

-- 5
SELECT Nome, Ano, Duracao
FROM Filmes
WHERE Ano > 2000;

-- 6
SELECT Nome, Ano, Duracao
FROM Filmes
WHERE Duracao > 100 AND Duracao < 150
ORDER BY Duracao ASC;

-- 7 
SELECT Ano, COUNT(*) AS QuantidadeFilmes
FROM Filmes
GROUP BY Ano
ORDER BY QuantidadeFilmes DESC;

-- 8
SELECT Id, PrimeiroNome, UltimoNome, Genero
FROM Atores
WHERE Genero = 'M';

-- 9
SELECT Id, PrimeiroNome, UltimoNome, Genero
FROM Atores
WHERE Genero = 'F'
ORDER BY PrimeiroNome ASC;

-- 10
SELECT F.Nome AS Nome,
       G.Genero
FROM Filmes F
JOIN FilmesGenero FG ON F.Id = FG.IdFilme
JOIN Generos G ON FG.IdGenero = G.Id;

-- 11 
SELECT F.Nome AS Nome,
       G.Genero
FROM Filmes F
JOIN FilmesGenero FG ON F.Id = FG.IdFilme
JOIN Generos G ON FG.IdGenero = G.Id
WHERE G.Genero = 'Mistério';

-- 12 
SELECT F.Nome AS Nome,
       A.PrimeiroNome,
       A.UltimoNome,
       E.Papel
FROM ElencoFilme E
JOIN Atores A ON E.IdAtor = A.Id
JOIN Filmes F ON E.IdFilme = F.Id;

-- Através do seguinte link, https://sqlfiddle.com/sql-server/online-compiler?id=93460682-fdf7-471d-8cae-b543f537a3ea, é possível acessar estas tabelas e ver o resultado de cada uma das consultas que realizei. 
