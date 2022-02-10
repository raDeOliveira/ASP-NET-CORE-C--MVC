CREATE TABLE Project_TEN.dbo.Criticisms (
	Id int IDENTITY(1,1) NOT NULL,
	VideoId int NOT NULL,
	Criticism nvarchar COLLATE Latin1_General_CI_AS NOT NULL,
	Classification int NOT NULL,
	CONSTRAINT PK_Criticisms PRIMARY KEY (Id)
);

CREATE TABLE Project_TEN.dbo.Users (
	Id int IDENTITY(1,1) NOT NULL,
	Name nvarchar COLLATE Latin1_General_CI_AS NOT NULL,
	Email nvarchar COLLATE Latin1_General_CI_AS NOT NULL,
	Password nvarchar COLLATE Latin1_General_CI_AS NOT NULL,
	[Type] int NOT NULL,
	Description nvarchar COLLATE Latin1_General_CI_AS NOT NULL,
	Linkedin nvarchar COLLATE Latin1_General_CI_AS NOT NULL,
	Phone int NOT NULL,
	CONSTRAINT PK_Users PRIMARY KEY (Id)
);

CREATE TABLE Project_TEN.dbo.VideoLinks (
	Id int IDENTITY(1,1) NOT NULL,
	Title nvarchar COLLATE Latin1_General_CI_AS NOT NULL,
	Link nvarchar COLLATE Latin1_General_CI_AS NOT NULL,
	Tag nvarchar COLLATE Latin1_General_CI_AS NOT NULL,
	Description nvarchar COLLATE Latin1_General_CI_AS NOT NULL,
	UserId int NOT NULL,
	CONSTRAINT PK_VideoLinks PRIMARY KEY (Id)
);


INSERT INTO Project_TEN.dbo.Criticisms (VideoId,Criticism,Classification) VALUES
	 (3,N'Psy Trance de alto nível',3),
	 (7,N'Demasiado barulho para os meus ouvidos',5),
	 (1,N'Excelente música',5),
	 (6,N'Grandes dicas',5),
	 (8,N'Boas memórias',4),
	 (6,N'Good to learn',4),
	 (4,N'Old classic',2),
	 (5,N'Bom tutorial',1),
	 (7,N'Psicadellic music',4),
	 (2,N'Hardcore',2);
INSERT INTO Project_TEN.dbo.Criticisms (VideoId,Criticism,Classification) VALUES
	 (2,N'Esperava melhor',2);

INSERT INTO Project_TEN.dbo.Users (Name,Email,Password,[Type],Description,Linkedin,Phone) VALUES
	 (N'Admin',N'admin@admin.com',N'a',0,N'Administrador',N'https://www.linkedin.com/',99999999),
	 (N'User1',N'user1@user.com',N'a',5,N'Empreendedor',N'https://www.linkedin.com/',88888888),
	 (N'User2',N'user2@user2.com',N'a',5,N'Empreendedor',N'https://www.linkedin.com/',777777777),
	 (N'User3',N'user2@user2.com',N'a',5,N'Empreendedor',N'https://www.linkedin.com/',666666666),
	 (N'Investidor',N'investidor@investidor1.com',N'a',4,N'Investidor',N'https://www.linkedin.com/',555555555),
	 (N'Investidor2',N'investidor@investidor2.com',N'a',4,N'Investidor',N'https://www.linkedin.com/',444444444),
	 (N'Investidor3',N'investidor@investidor3.com',N'a',4,N'Investidor',N'https://www.linkedin.com/',333333333),
	 (N'raOliveira',N'raOliveira@mail.com',N'a',4,N'Investidor',N'https://www.linkedin.com/',11111111);

INSERT INTO Project_TEN.dbo.VideoLinks (Title,Link,Tag,Description,UserId) VALUES
	 (N'Nirvana - The Man Who Sold The World (MTV Unplugged)',N'https://www.youtube.com/watch?v=fregObNcHC8',N'Pop Rock',N'Boa música tocada pelos Nirvana.',2),
	 (N'Carl Cox - 9 Hour Marathon DJ Set @ Space Ibiza',N'https://www.youtube.com/watch?v=Dsp0o3N1lKM',N'Techno',N'9 hour set by Carl Cox @ Space Ibiza, Close party',2),
	 (N'Tsuyoshi Suzuki BD 4h set',N'https://www.youtube.com/watch?v=xz4gROcJ6iQ',N'Psy Trance',N'Psy Trance played by Tsuyoshi in 4 hour set @ Koenji',2),
	 (N'2000s Classic House',N'https://www.youtube.com/watch?v=2zoer613g-U',N'House',N'DJ Mika with old classic musics from the 2000s',2),
	 (N'Aprenda como criar uma TELA de LOGIN',N'https://www.youtube.com/watch?v=lP-XV2wXXQM',N'IT',N'Como criar tela de login',3),
	 (N'How To Create Live Search/Filter',N'https://www.youtube.com/watch?v=sT6TCWt1YP8',N'Computador',N'Como criar live search',3),
	 (N'Astrix @ Universo',N'https://www.youtube.com/watch?v=8eYEW4q42y8',N'Psy Trance',N'Psy Trance played by Tsuyoshi in 4 hour set @ Koenji',3),
	 (N'2000s Classic House',N'https://www.youtube.com/watch?v=2zoer613g-U',N'House',N'DJ Mika with old classic musics from the 2000s',2),
	 (N'Aprenda como criar uma TELA de LOGIN',N'https://www.youtube.com/watch?v=lP-XV2wXXQM',N'IT',N'Como criar tela de login',5),
	 (N'How To Create Live Search/Filter',N'https://www.youtube.com/watch?v=sT6TCWt1YP8',N'Computador',N'Como criar live search',5);
INSERT INTO Project_TEN.dbo.VideoLinks (Title,Link,Tag,Description,UserId) VALUES
	 (N'Astrix @ Universo',N'https://www.youtube.com/watch?v=8eYEW4q42y8',N'Psy Trance',N'Psy Trance played by Tsuyoshi in 4 hour set @ Koenji',5),
	 (N'2000s Classic House',N'https://www.youtube.com/watch?v=2zoer613g-U',N'House',N'DJ Mika with old classic musics from the 2000s',5),
	 (N'Nirvana - The Man Who Sold The World (MTV Unplugged)',N'https://www.youtube.com/watch?v=fregObNcHC8',N'Pop Rock',N'Boa música tocada pelos Nirvana.',6),
	 (N'Carl Cox - 9 Hour Marathon DJ Set @ Space Ibiza',N'https://www.youtube.com/watch?v=Dsp0o3N1lKM',N'Techno',N'9 hour set by Carl Cox @ Space Ibiza, Close party',6),
	 (N'Tsuyoshi Suzuki BD 4h set',N'https://www.youtube.com/watch?v=xz4gROcJ6iQ',N'Psy Trance',N'Psy Trance played by Tsuyoshi in 4 hour set @ Koenji',6),
	 (N'2000s Classic House',N'https://www.youtube.com/watch?v=2zoer613g-U',N'House',N'DJ Mika with old classic musics from the 2000s',6);

