--EasyBayCR

PROMPT....................................BORRADO DE TABLAS..........................................
PROMPT detalle_producto
drop table detalle_producto;

PROMPT promocion
drop table promocion;

PROMPT producto
drop table producto;

PROMPT tienda
drop table empresa;

PROMPT cliente
drop table cliente;

PROMPT.................................BORRADO DE SECUENCIAS..........................................

drop sequence seq_id_empresa;
drop sequence seq_id_producto;
drop sequence seq_id_det_producto;
drop sequence seq_id_promocion;

PROMPT....................................CREACION DE TABLAS.........................................

PROMPT cliente
create table cliente(
	nombre_cliente   varchar2(20),
	apellido_cliente varchar2(20),
	password		 varchar2(20),
	correo_cliente   varchar2(30)
);

PROMPT empresa
create table empresa(
	id_empresa	     number,
	nombre_empresa   varchar2(30),
	numero_telefono  varchar2(30),
	direccion 		 varchar2(40),
	correo_tienda	 varchar2(30)
);

PROMPT producto
create table producto(
	id_producto	     number,
	id_empresa       number,
	descripcion      varchar2(20)
);

PROMPT detalle_producto
create table detalle_producto(
	id_detalle       number,
	id_producto      number,
	cantidad		 number,	
	color	         varchar2(20),
	talla			 varchar2(2),
	precio			 varchar2(20),
	imagen			 varchar2(100)
);

PROMPT promocion
create table promocion(
	id_promocion 	 number,
	id_producto      number,
	nuevo_precio	 number,
	fecha_inicio     date,
	fecha_final		 date
);

PROMPT.........................................SECUENCIAS.........................................
create sequence seq_id_empresa start with 1 increment by 1
maxvalue 999999999;
create sequence seq_id_producto start with 1 increment by 1
maxvalue 999999999;
create sequence seq_id_det_producto start with 1 increment by 1
maxvalue 999999999;
create sequence seq_id_promocion start with 1 increment by 1
maxvalue 999999999;

PROMPT....................................LLAVES PRIMARIAS.........................................
alter table cliente add constraint cliente_pk primary key(correo_cliente);
alter table empresa add constraint empresa_pk primary key(id_empresa);
alter table producto add constraint producto_pk primary key(id_producto);
alter table detalle_producto add constraint detalle_producto_pk primary key(id_detalle);
alter table promocion add constraint promocion_pk primary key(id_promocion);

PROMPT....................................LLAVES FORANEAS.........................................
alter table producto add constraint producto_fk1 foreign key (id_empresa) references empresa;
alter table detalle_producto add constraint detalle_producto_fk2 foreign key (id_producto) references producto;
alter table promocion add constraint promocion_fk3 foreign key (id_producto) references producto;




