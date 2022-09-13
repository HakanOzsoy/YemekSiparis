CREATE DATABASE YemekSiparis;

CREATE TABLE [dbo].[Sehir] (
    [PlakaKodu] INT           NOT NULL,
    [Ad]        VARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([PlakaKodu] ASC)
);

CREATE TABLE [dbo].[Ilce] (
    [IlceKodu]  INT           IDENTITY (1, 1) NOT NULL,
    [SehirKodu] INT           NOT NULL,
    [Ad]        VARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([IlceKodu] ASC),
    FOREIGN KEY ([SehirKodu]) REFERENCES [dbo].[Sehir] ([PlakaKodu])
);

CREATE TABLE [dbo].[Adres] (
    [AdresKodu]    INT           IDENTITY (1, 1) NOT NULL,
    [DetayliAdres] VARCHAR (255) NULL,
    [SehirKodu]    INT           NULL,
    [IlceKodu]     INT           NULL,
    PRIMARY KEY CLUSTERED ([AdresKodu] ASC),
    FOREIGN KEY ([SehirKodu]) REFERENCES [dbo].[Sehir] ([PlakaKodu]),
    FOREIGN KEY ([IlceKodu]) REFERENCES [dbo].[Ilce] ([IlceKodu])
);

CREATE TABLE [dbo].[Kullanici] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [kullaniciAdi] NVARCHAR (50) NOT NULL,
    [sifre]        NVARCHAR (50) NOT NULL,
    [rol]          NVARCHAR (50) NOT NULL,
    [AdresKodu]    INT           NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([kullaniciAdi] ASC),
    FOREIGN KEY ([AdresKodu]) REFERENCES [dbo].[Adres] ([AdresKodu])
);

CREATE TABLE [dbo].[Urun] (
    [UrunKodu] INT           IDENTITY (1, 1) NOT NULL,
    [UrunAdi]  VARCHAR (255) NULL,
    [UrunTuru] VARCHAR (255) NULL,
    [Tanim]    VARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([UrunKodu] ASC)
);

CREATE TABLE [dbo].[Menu] (
    [MenuKodu] INT IDENTITY (1, 1) NOT NULL,
    PRIMARY KEY CLUSTERED ([MenuKodu] ASC)
);

CREATE TABLE [dbo].[UrunMenu] (
    [UrunMenuNo] INT IDENTITY (1, 1) NOT NULL,
    [Fiyat]      INT NULL,
    [MenuKodu]   INT NULL,
    [UrunKodu]   INT NULL,
    PRIMARY KEY CLUSTERED ([UrunMenuNo] ASC),
    FOREIGN KEY ([MenuKodu]) REFERENCES [dbo].[Menu] ([MenuKodu]),
    FOREIGN KEY ([UrunKodu]) REFERENCES [dbo].[Urun] ([UrunKodu])
);

CREATE TABLE [dbo].[Restoran] (
    [RestoranKodu] INT            IDENTITY (1, 1) NOT NULL,
    [RestoranAdi]  VARCHAR (255)  NULL,
    [AdresKodu]    INT            NULL,
    [MenuKodu]     INT            NULL,
    [Tanim]        NVARCHAR (255) NULL,
    [DetayliBilgi] NVARCHAR (255) NULL,
    [YetkiliID]    INT            NULL,
    PRIMARY KEY CLUSTERED ([RestoranKodu] ASC),
    FOREIGN KEY ([YetkiliID]) REFERENCES [dbo].[Kullanici] ([Id]),
    FOREIGN KEY ([AdresKodu]) REFERENCES [dbo].[Adres] ([AdresKodu]),
    FOREIGN KEY ([MenuKodu]) REFERENCES [dbo].[Menu] ([MenuKodu])
);

CREATE TABLE [dbo].[Siparis] (
    [SiparisKodu]  INT IDENTITY (1, 1) NOT NULL,
    [ToplamTutar]  INT NULL,
    [RestoranKodu] INT NULL,
    [KullaniciID]  INT NULL,
    [UMKodu]       INT NULL,
    [Tamamlandi]   INT DEFAULT ((0)) NULL,
    PRIMARY KEY CLUSTERED ([SiparisKodu] ASC),
    FOREIGN KEY ([KullaniciID]) REFERENCES [dbo].[Kullanici] ([Id]),
    FOREIGN KEY ([UMKodu]) REFERENCES [dbo].[UrunMenu] ([UrunMenuNo]),
    FOREIGN KEY ([RestoranKodu]) REFERENCES [dbo].[Restoran] ([RestoranKodu])
);

CREATE VIEW RestoranListele2 AS
SELECT RestoranKodu, RestoranAdi, Tanim, DetayliBilgi, Sehir.Ad AS SehirAdi, Ilce.Ad AS IlceAdi, MenuKodu, YetkiliID
FROM Restoran, Sehir, Adres, Ilce
WHERE Adres.SehirKodu = Sehir.PlakaKodu
AND Adres.IlceKodu = Ilce.IlceKodu
AND Restoran.AdresKodu = Adres.AdresKodu
AND Sehir.PlakaKodu = Ilce.SehirKodu

CREATE VIEW SiparisListe AS
SELECT Siparis.SiparisKodu, Siparis.RestoranKodu, Siparis.KullaniciID, Siparis.UMKodu,
Siparis.Tamamlandi, UrunMenu.UrunKodu, UrunMenu.MenuKodu, Restoran.YetkiliID, Restoran.RestoranAdi, Urun.UrunAdi, Siparis.ToplamTutar
FROM Siparis, Urun, Restoran, UrunMenu
WHERE Restoran.RestoranKodu = Siparis.RestoranKodu
AND Siparis.UMKodu = UrunMenu.UrunMenuNo
AND UrunMenu.UrunKodu = Urun.UrunKodu

CREATE VIEW UrunMenusu AS
SELECT r.RestoranKodu, r.RestoranAdi, u.UrunAdi, um.Fiyat, u.Tanim, u.UrunTuru , u.UrunKodu
FROM Urun u
INNER JOIN UrunMenu um ON u.UrunKodu = um.UrunKodu
INNER JOIN Menu m ON  m.MenuKodu = um.MenuKodu
INNER JOIN Restoran R ON r.MenuKodu = m.MenuKodu

CREATE PROC UrunMenusuL AS
SELECT * FROM UrunMenusu

CREATE PROC UrunMenusuP AS
SELECT r.RestoranKodu, r.RestoranAdi, u.UrunAdi, um.Fiyat, u.Tanim, u.UrunTuru , u.UrunKodu
FROM Urun u
INNER JOIN UrunMenu um ON u.UrunKodu = um.UrunKodu
INNER JOIN Menu m ON  m.MenuKodu = um.MenuKodu
INNER JOIN Restoran R ON r.MenuKodu = m.MenuKodu