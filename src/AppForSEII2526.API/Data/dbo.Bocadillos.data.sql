SET IDENTITY_INSERT [dbo].[Bocadillos] ON
INSERT INTO [dbo].[Bocadillos] ([Id], [nombre], [PVP], [stock], [tipoPanId], [tamaño]) VALUES (1, N'Politecnico', CAST(3.00 AS Decimal(10, 2)), 100, 5, 1)
INSERT INTO [dbo].[Bocadillos] ([Id], [nombre], [PVP], [stock], [tipoPanId], [tamaño]) VALUES (2, N'Completo2', CAST(5.00 AS Decimal(10, 2)), 100, 4, 0)
INSERT INTO [dbo].[Bocadillos] ([Id], [nombre], [PVP], [stock], [tipoPanId], [tamaño]) VALUES (3, N'Bacon', CAST(2.00 AS Decimal(10, 2)), 100, 6, 0)
SET IDENTITY_INSERT [dbo].[Bocadillos] OFF
