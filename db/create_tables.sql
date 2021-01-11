use dbAlien;

Go

/*
If OBJECT_ID('EventoSorteado') is not null Drop table EventoSorteado

If OBJECT_ID('GrupoEvento') is not null Drop table GrupoEvento

If OBJECT_ID('Evento') is not null Drop table Evento
If OBJECT_ID('TipoEvento') is not null Drop table TipoEvento

If OBJECT_ID('GrupoUsuario') is not null Drop table GrupoUsuario

If OBJECT_ID('Usuario') is not null Drop table Usuario
If OBJECT_ID('Grupo') is not null Drop table Grupo
*/

-- Usuario
Create Table Usuario
(
	CdUsuario		int identity(1,1)
	,NmEmail		varchar(80)	
	,NmUsuario		varchar(80)
	,NmSenha		varchar(80)
	,DvAtivo		bit
	,DtInclusao	datetime
	,Constraint pk_Usuario primary key(CdUsuario)
)
go

-- Grupo
Create Table Grupo
(
	CdGrupo		int identity(1,1)
	,NmGrupo	varchar(60)
	,DtInclusao	datetime
	,Constraint pk_Grupo primary key(CdGrupo)
)
go

-- Grupo_usuario
Create Table GrupoUsuario
(
	 CdUsuario	int
	,CdGrupo	int
	,NrVoto	smallint
	,Constraint pk_GrupoUsuario primary key(CdUsuario, CdGrupo)
	,Constraint fk_GrupoUsuario_Usuario foreign key(CdUsuario) references Usuario
	,Constraint fk_GrupoUsuario_Grupo foreign key(CdGrupo) references Grupo 
)
go

-- Tipo_evento
Create Table TipoEvento
(
	CdTipoEvento	smallint identity(1,1)
	,NmTipoEvento varchar(60)
	Constraint pk_TipoEvento primary key(CdTipoEvento)
)
go

-- Evento
Create table Evento
(
	CdEvento	int identity(1,1)
	,CdTipoEvento smallint
	,NmEvento	varchar(60)
	,NmEndereco varchar(100)
	,VlEvento float
	,VlNota float
	,DvParticular bit
	,Constraint pk_Evento primary key(CdEvento)
	,Constraint fk_Evento_TipoEvento foreign key(CdTipoEvento) references TipoEvento
)
go 

-- Grupo_evento
Create Table GrupoEvento
(
	IdGrupoEvento int identity(1, 1)
	,CdGrupo		int
	,CdEvento		int
	,NmDescricao	varchar(80)
	,DtCadastro	datetime
	,DtInicio		datetime
	,DvRecorrente	bit
	,VlRecorrencia int
	,VlDiasRecorrencia int
	,Constraint pk_GrupoEvento primary key(IdGrupoEvento)
	,Constraint fk_GrupoEvento_Grupo foreign key(CdGrupo) references Grupo
	,Constraint fk_GrupoEvento_Evento foreign key(CdEvento) references Evento
)

Create Table EventoSorteado
(
	IdEventoSorteado int identity(1,1)
	,IdGrupoEvento int not null
	,DtEvento datetime not null
	,Constraint PK_EventoSorteado Primary Key(IdEventoSorteado)
	,CONSTRAINT FK_EventoSorteado_GrupoEvento FOREIGN KEY (IdGrupoEvento) REFERENCES GrupoEvento(IdGrupoEvento)
)
