--EasyBayCR

PROMPT....................................BORRADO DE TABLAS..........................................

PROMPT deseo
drop table deseo;

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

PROMPT usuario
drop table usuario;

PROMPT.................................BORRADO DE SECUENCIAS..........................................

drop sequence seq_id_empresa;
drop sequence seq_id_producto;
drop sequence seq_id_det_producto;
drop sequence seq_id_promocion;

PROMPT....................................CREACION DE TABLAS.........................................

create table usuario(
	correo varchar2(30),
	tipo char
);

PROMPT cliente
create table cliente(
	nombre_cliente   varchar2(20),
	apellido_cliente varchar2(20),
	password		 varchar2(50),
	correo_cliente   varchar2(30)
);

PROMPT empresa
create table empresa(
	id_empresa	     number,
	nombre_empresa   varchar2(30),
	password_empresa varchar2(50),
	numero_telefono  varchar2(30),
	direccion 		 varchar2(40),
	provincia        varchar2(20),
	correo_tienda	 varchar2(30)
);

PROMPT producto
create table producto(
	id_producto	     number,
	correo_tienda    varchar2(30),
	descripcion      varchar2(20),
	categoria        varchar2(20)
);

PROMPT deseo
create table deseo(
	id_producto		number,
	correo_cliente  varchar2(30)
);

PROMPT detalle_producto
create table detalle_producto(
	id_detalle       number,
	id_producto      number,
	cantidad		 number,	
	color	         varchar2(20),
	talla			 varchar2(2),
	precio			 number,
	imagen			 varchar2(100),
	promocion		 varchar2(20), 
	genero           varchar2(20) 
);

PROMPT promocion
create table promocion(
	id_promocion 	 number,
	id_detalle       number,
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
alter table empresa add constraint empresa_pk primary key(correo_tienda);
alter table producto add constraint producto_pk primary key(id_producto);
alter table detalle_producto add constraint detalle_producto_pk primary key(id_detalle);
alter table promocion add constraint promocion_pk primary key(id_promocion);
alter table deseo add constraint deseo_pk primary key (id_producto,correo_cliente);
alter table usuario add constraint usuario_pk primary key (correo);

PROMPT....................................LLAVES FORANEAS.........................................
alter table producto add constraint producto_fk1 foreign key (correo_tienda) references empresa;
alter table detalle_producto add constraint detalle_producto_fk2 foreign key (id_producto) references producto;
alter table cliente add constraint cliente_fk foreign key (correo_cliente) references usuario;
alter table empresa add constraint empresa_fk foreign key (correo_tienda) references usuario;
alter table deseo add constraint deseo_fk1 foreign key (correo_cliente) references cliente;
alter table deseo add constraint deseo_fk2 foreign key (id_producto) references producto;

PROMPT.................................FUNCIONES DE REGISTRO............................................
PROMPT inserto funcion empresa
CREATE OR REPLACE FUNCTION fun_insertar_empresa(Pnombre_empresa varchar2, Ppassword_empresa varchar2,
Pnumero_telefono  varchar2, Pdireccion varchar2, Pcorreo_tienda	varchar2, Pprovincia varchar2)
RETURN number
IS
   BEGIN	
	insert into empresa(nombre_empresa, password_empresa, numero_telefono, direccion, correo_tienda,provincia) 
	values(Pnombre_empresa, Ppassword_empresa, Pnumero_telefono, Pdireccion, Pcorreo_tienda, Pprovincia);

	return seq_id_empresa.currval;
	
   END;
/
show error

PROMPT inserto funcion producto
CREATE OR REPLACE FUNCTION fun_insertar_producto(Pcorreo_tienda varchar2, 
Pdescripcion varchar2, Pcategoria varchar2)
RETURN number
IS
   BEGIN	
	insert into producto(correo_tienda, descripcion, categoria) values(Pcorreo_tienda, Pdescripcion, Pcategoria);

	return seq_id_producto.currval;
	
   END;
/
show error

PROMPT inserto funcion detalle_producto
CREATE OR REPLACE FUNCTION fun_insertar_detalle(Pid_producto number, 
Pcantidad number, Pcolor varchar2, Ptalla varchar2, Pprecio number, Pimagen varchar2, Ppromocion char,Pgenero varchar2)
RETURN number
IS
   BEGIN	
	insert into detalle_producto(id_producto, cantidad, color, talla, precio, imagen, promocion,genero)
	values(Pid_producto, Pcantidad, Pcolor, Ptalla, Pprecio, Pimagen, Ppromocion, Pgenero);

	return seq_id_det_producto.currval;
	
   END;
/
show error

CREATE OR REPLACE FUNCTION ObtenerDetalle(id_det number)
     RETURN SYS_REFCURSOR
  IS
     deta SYS_REFCURSOR;
  BEGIN
           OPEN deta FOR
                SELECT * from detalle_producto where id_detalle = id_det;
     RETURN deta;
END ObtenerDetalle;
/


PROMPT inserto funcion promocion
CREATE OR REPLACE FUNCTION fun_insertar_promocion(Pid_detalle number, Pnuevo_precio number,
Pfecha_inicio date, Pfecha_final  date)
RETURN number
IS
   BEGIN	
	insert into promocion(id_detalle, nuevo_precio, fecha_inicio, fecha_final)
	values(Pid_detalle, Pnuevo_precio, Pfecha_inicio, Pfecha_final);

	return seq_id_promocion.currval;
	
   END;
/
show error

PROMPT................................FUNCIONES DE ACTUALIZACION.......................................

PROMPT actualizar funcion empresa
CREATE OR REPLACE FUNCTION fun_actualizar_empresa(Pid_empresa number, Pnombre_empresa varchar2,
Ppassword_empresa varchar2, Pnumero_telefono  varchar2, Pdireccion varchar2, Pcorreo_tienda	 varchar2)
RETURN INT
IS
   BEGIN	
	update empresa set  nombre_empresa = PNombre_empresa,
						password_empresa = Ppassword_empresa,
						numero_telefono  = Pnumero_telefono,
						direccion = Pdireccion
	where correo_tienda = Pcorreo_tienda;
  
   return seq_id_empresa.currval;
   END fun_actualizar_empresa;
/
show error

PROMPT actualizar funcion producto
CREATE OR REPLACE FUNCTION fun_actualizar_producto(Pid_producto number, Pdescripcion varchar2)
RETURN INT
IS
   BEGIN	
	update producto set  descripcion = Pdescripcion
	where id_producto = Pid_producto;
  
   return seq_id_producto.currval;
   END fun_actualizar_producto;
/
show error

PROMPT actualizar funcion detalle_producto
CREATE OR REPLACE FUNCTION fun_actualizar_detalle(Pid_detalle number, Pcantidad number,
Pcolor varchar2, Ptalla	varchar2, Pprecio number, Pimagen	varchar2, Ppromocion char, Pgenero varchar2)
RETURN INT
IS
   BEGIN	
	update detalle_producto set  cantidad = Pcantidad,
								 color = Pcolor,
								 talla = Ptalla,
								 precio = Pprecio,
								 imagen = Pimagen,
								 promocion = Ppromocion,
								 genero = Pgenero
								 
	where id_detalle = Pid_detalle;
  
   return seq_id_det_producto.currval;
   END fun_actualizar_detalle;
/
show error

CREATE OR REPLACE PROCEDURE PRC_actualizar_detalle(Pid_detalle number, Pcantidad number,
Pcolor varchar2, Ptalla	varchar2, Pprecio number, Pimagen	varchar2, Ppromocion char, Pgenero varchar2)
IS
   BEGIN	
	update detalle_producto set  cantidad = Pcantidad,
								 color = Pcolor,
								 talla = Ptalla,
								 precio = Pprecio,
								 imagen = Pimagen,
								 promocion = Ppromocion,
								 genero = Pgenero
								 
	where id_detalle = Pid_detalle;
  
   END PRC_actualizar_detalle;
/
show error


PROMPT actualizar funcion promocion
CREATE OR REPLACE FUNCTION fun_actualizar_promocion(Pid_promocion number, Pnuevo_precio number,
Pfecha_inicio date,
Pfecha_final	date)
RETURN INT
IS
   BEGIN	
	update promocion set nuevo_precio = PNuevo_precio,
						 fecha_inicio = PFecha_inicio,
						 fecha_final  = PFecha_final
	where id_promocion = Pid_promocion;
  
   return seq_id_promocion.currval;
   END fun_actualizar_promocion;
/
show error

PROMPT.................................TRIGGERS DE REGISTRO............................................
PROMPT inserto triger insertar_empresa
CREATE OR REPLACE TRIGGER trig_insertar_empresa
  BEFORE INSERT ON empresa
  FOR EACH ROW
  BEGIN
    SELECT seq_id_empresa.nextval INTO :new.id_empresa FROM dual;
  END
;
/

PROMPT inserto triger producto
CREATE OR REPLACE TRIGGER trig_insertar_producto
  BEFORE INSERT ON producto
  FOR EACH ROW
  BEGIN
    SELECT seq_id_producto.nextval INTO :new.id_producto FROM dual;
  END
;
/

PROMPT inserto triger detalle_producto
CREATE OR REPLACE TRIGGER trig_insertar_det_producto
  BEFORE INSERT ON detalle_producto
  FOR EACH ROW
  BEGIN
    SELECT seq_id_det_producto.nextval INTO :new.id_detalle FROM dual;
  END
;
/

PROMPT inserto triger insertar_promocion
CREATE OR REPLACE TRIGGER trig_insertar_promocion
  BEFORE INSERT ON promocion
  FOR EACH ROW
  BEGIN
    SELECT seq_id_promocion.nextval INTO :new.id_promocion FROM dual;
  END
;
/

PROMPT.............................PROCEDIMIENTO ALMACENADOS.........................................

PROMPT procedimiento registrar cliente
create or replace procedure prc_insertar_cliente
(PNombre_cliente in varchar2, PApellido_cliente in varchar2, 
PPassword in varchar2,PCorreo_cliente in varchar2)is

begin

		insert into cliente (nombre_cliente, apellido_cliente, password, correo_cliente)
		values (PNombre_cliente, PApellido_cliente, PPassword, PCorreo_cliente);
		commit;
		
end prc_insertar_cliente;
/
show error

PROMPT procedimiento registrar empresa
create or replace procedure prc_insertar_empresa
(PId_empresa in number, PNombre_empresa in varchar2, 
PNumero_telefono in varchar2, PDireccion in varchar2, PPassword_empresa in varchar2, PCorreo_tienda in varchar2,Pprovincia in varchar2)is

begin

		insert into empresa (id_empresa, nombre_empresa, numero_telefono, direccion, password_empresa, correo_tienda,provincia)
		values (PId_empresa, PNombre_empresa, PNumero_telefono, PDireccion, PPassword_empresa, PCorreo_tienda, Pprovincia);
		commit;
		
end prc_insertar_empresa;
/
show error

PROMPT procedimiento registrar producto
create or replace procedure prc_insertar_producto
(Pcorreo_tienda in varchar2, PDescripcion in varchar2)is

begin

		insert into producto (correo_tienda, descripcion)
		values (Pcorreo_tienda, PDescripcion);
		commit;
		
end prc_insertar_producto;
/
show error

PROMPT procedimiento registrar detalle_producto
create or replace procedure prc_insertar_det_producto
(PId_producto in number, 
PCantidad in number, PColor in varchar2, PTalla in varchar2, PPrecio in number, PImagen in varchar2,PPromocion in varchar2,Pgenero in varchar2)is

begin

		insert into detalle_producto (id_producto, cantidad, color, talla, precio, imagen, promocion,genero)
		values (PId_producto, PCantidad, PColor, PTalla, PPrecio, PImagen,PPromocion,Pgenero);
		commit;
		
end prc_insertar_det_producto;
/
show error




PROMPT procedimiento registrar promocion
create or replace procedure prc_insertar_promocion
(PId_promocion in number, 
PNuevo_precio in number, PFecha_inicio in date, PFecha_final in date)is

begin

		insert into promocion (id_promocion, nuevo_precio, fecha_inicio, fecha_final)
		values (PId_promocion, PNuevo_precio, PFecha_inicio, PFecha_final);
		commit;
		
end prc_insertar_promocion;
/
show error

PROMPT procedimiento registrar deseos
create or replace procedure prc_insertar_deseos
(PId_producto in number, PCorreo_cliente in varchar2)is

begin

		insert into deseo (id_producto, correo_cliente)
		values (PId_producto, PCorreo_cliente);
		commit;
		
end prc_insertar_deseos;
/
show error

PROMPT procedimiento registrar usuario
create or replace procedure prc_insertar_usuario
(Pcorreo in varchar2, PTipo in char)is

begin

		insert into usuario (correo, tipo)
		values (Pcorreo, PTipo);
		commit;
		
end prc_insertar_usuario;
/
show error

PROMPT.............................PROCEDIMIENTO ALMACENADOS PARA ELIMINAR...........................................
PROMPT Procedimiento para eliminar deseo
create or replace procedure prc_eliminar_deseo(Pid_producto in number, Pcorreo_cliente in varchar2) is
  
begin
	
	  delete deseo where id_producto = Pid_producto or correo_cliente = PCorreo_cliente;
	  commit;
	  
end prc_eliminar_deseo;
/
show error

PROMPT Procedimiento para eliminar detalle_producto
create or replace procedure prc_eliminar_detalle(Pid_detalle in number) is
  
begin
	
	  delete detalle_producto where id_detalle = Pid_detalle;
	  commit;
	  
end prc_eliminar_detalle;
/
show error

PROMPT Procedimiento para eliminar promocion
create or replace procedure prc_eliminar_promocion(Pid_promocion in number) is
  
begin
	
	  delete promocion where id_promocion = Pid_promocion;
	  commit;
	  
end prc_eliminar_promocion;
/
show error

PROMPT Procedimiento para eliminar producto
create or replace procedure prc_eliminar_producto(Pid_producto in number) is
  
begin
	
	  delete producto where id_producto = Pid_producto;
	  commit;
	  
end prc_eliminar_producto;
/
show error

PROMPT Procedimiento para eliminar producto
create or replace procedure prc_eliminar_empresa(Pcorreo_tienda in varchar2) is
  
begin
	
	  delete empresa where correo_tienda = Pcorreo_tienda;
	  commit;
	  
end prc_eliminar_empresa;
/
show error

PROMPT Procedimiento para eliminar cliente
create or replace procedure prc_eliminar_cliente(Pcorreo_cliente in varchar2) is
  
begin
	
	  delete cliente where correo_cliente = Pcorreo_cliente;
	  commit;
	  
end prc_eliminar_cliente;
/
show error

PROMPT Procedimiento para eliminar usuario
create or replace procedure prc_eliminar_usuario(Pcorreo in varchar2) is
  
begin
	
	  delete usuario where correo = Pcorreo;
	  commit;
	  
end prc_eliminar_usuario;
/

show error

commit;
