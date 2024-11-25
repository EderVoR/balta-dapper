create database blog;

create table usuario(
	id serial,
	name varchar(80) not null, 
	email varchar(200) not null,
	passwordhash varchar(255) not null,
	bio varchar(200) not null,
	image varchar(2000) not null,
	slug varchar(80) not null,
	
	constraint pk_user primary key(id)
)

create table grupo(
	id serial, 
	name varchar(80) not null,
	slug varchar(80) not null,
	
	constraint pk_grupo primary key (id)
)

create table userrole(
	usuarioid int not null,
	grupoid int not null,
	
	constraint pk_userrole primary key (usuarioid, grupoid)
)

create table category(
	id serial, 
	name varchar(80), 
	slug varchar(80),
	
	constraint pk_category primary key(id) 
)

create table post(
	id serial, 
	categoryid int not null, 
	authorid int not null,
	title varchar(160),
	summary varchar(255),
	body text,
	slug varchar(80),
	createdate timestamp,
	lastupdate timestamp,
	
	constraint pk_post primary key(id),
	constraint fk_post_category foreign key (categoryid) references category(id),
	constraint fk_post_usuario foreign key(authorid) references usuario(id)
)

create table tag (
	id serial, 
	name varchar(80),
	slug varchar(80),
	
	constraint pk_tag primary key (id)
)

create table posttag(
	postid int not null,
	tagid int not null,
	
	constraint pk_posttag primary key (postid, tagid)
)
