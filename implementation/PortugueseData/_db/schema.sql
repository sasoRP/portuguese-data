create database portuguese_data;
use portuguese_data;

create table distritos (
	id INTEGER NOT NULL AUTO_INCREMENT,
   codigo VARCHAR(20) not null,
   designacao VARCHAR(150) not null,
   primary key (id)
);

create table concelhos (
	id INTEGER NOT NULL AUTO_INCREMENT,
   codigo VARCHAR(20) not null,
   designacao VARCHAR(150) not null,
   distrito_id INTEGER,
   primary key (id)
);

create table freguesias (
	id INTEGER NOT NULL AUTO_INCREMENT,
   codigo VARCHAR(20) not null,
   designacao VARCHAR(150) not null,
   concelho_id INTEGER,
   primary key (id)
);

alter table concelhos 
	add index (distrito_id), 
	add constraint FKC1D64572644078C4 
	foreign key (distrito_id) 
	references distritos (id);

alter table freguesias 
	add index (concelho_id), 
	add constraint FK66C7D26861383A08 
	foreign key (concelho_id) 
	references concelhos (id);
