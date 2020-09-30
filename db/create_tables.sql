use dbAlien;

Go

/*
If OBJECT_ID('Evento_sorteado') is not null Drop table Evento_sorteado

If OBJECT_ID('Grupo_evento') is not null Drop table Grupo_evento

If OBJECT_ID('Evento') is not null Drop table Evento
If OBJECT_ID('Tipo_evento') is not null Drop table Tipo_evento

If OBJECT_ID('Grupo_usuario') is not null Drop table Grupo_usuario

If OBJECT_ID('Usuario') is not null Drop table Usuario
If OBJECT_ID('Grupo') is not null Drop table Grupo
*/

-- Usuario
Create Table Usuario
(
	Cd_usuario		int identity(1,1)
	,Nm_email		varchar(80)	
	,Nm_usuario		varchar(80)
	,Nm_senha		varchar(80)
	,Dv_ativo		bit
	,Dt_inclusao	datetime
	Constraint pk_cd_usuario primary key(Cd_usuario)
)
go

-- Grupo
Create Table Grupo
(
	Cd_grupo		int identity(1,1)
	,Nm_grupo		varchar(60)
	,Dt_inclusao	datetime
	Constraint pk_cd_grupo primary key(Cd_grupo)
)
go

-- Grupo_usuario
Create Table Grupo_usuario
(
	Cd_usuario	int
	,Cd_grupo	int
	,Nr_voto	smallint
	Constraint pk_cd_usuario_grupo primary key(Cd_usuario, Cd_grupo)
	,Constraint fk_Grupo_usuario_Usuario_cd_usuario foreign key(Cd_usuario) references Usuario
	,Constraint fk_Grupo_usuario_Grupo_cd_grupo foreign key(Cd_grupo) references Grupo 
)
go

-- Tipo_evento
Create Table Tipo_evento
(
	Cd_tipo_evento	smallint identity(1,1)
	,Nm_tipo_evento varchar(60)
	Constraint pk_cd_tipo_evento primary key(Cd_tipo_evento)
)
go

-- Evento
Create table Evento
(
	Cd_evento	int identity(1,1)
	,Cd_tipo_evento smallint
	,Nm_evento	varchar(60)
	,Nm_endereco varchar(100)
	,Vl_evento float
	,Vl_nota float
	,Dv_particular bit
	,Cd_grupo int
	Constraint pk_cd_evento primary key(Cd_evento)
	,Constraint fk_Evento_Tipo_evento_cd_tipo_evento foreign key(Cd_tipo_evento) references Tipo_evento
	,Constraint fk_Evento_Grupo_cd_grupo foreign key(Cd_grupo) references Grupo
)
go 

-- Grupo_evento
Create Table Grupo_evento
(
	Id_grupo_evento int identity(1, 1)
	,Cd_grupo		int
	,Nm_descricao	varchar(80)
	,Dt_cadastro	datetime
	,Dt_inicio		datetime
	,Dv_recorrente	bit
	,Vl_recorrencia int
	,Vl_dias_recorrencia int
	Constraint pk_id_grupo_evento primary key(Id_grupo_evento)
	,Constraint fk_Grupo_evento_Grupo_cd_grupo foreign key(Cd_grupo) references Grupo
)

Create Table Evento_sorteado
(
	Id_evento_sorteado int identity(1,1)
	,Id_grupo_evento int not null
	,Cd_evento int
	,Dt_evento datetime not null
	,Constraint PK_Evento_sorteado Primary Key(Id_evento_sorteado)
	,CONSTRAINT FK_Evento_sorteado_Grupo_evento FOREIGN KEY (Id_grupo_evento) REFERENCES Grupo_evento(Id_grupo_evento)
	,Constraint fk_Evento_sorteado_cd_evento foreign key(Cd_evento) references Evento(Cd_evento)
)
