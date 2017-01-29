insert into usuario (correo, tipo) values ('dacorcam@hotmail.com','C');
insert into cliente (nombre_cliente,apellido_cliente,password,correo_cliente) 
values ('Jose','Cordero','4a7d1ed414474e4033ac29ccb8653d9b','dacorcam@hotmail.com');

insert into usuario (correo, tipo) values ('carcamaron@gmail.com','C');
insert into cliente (nombre_cliente,apellido_cliente,password,correo_cliente) 
values ('Carlos','Camaron','4a7d1ed414474e4033ac29ccb8653d9b','carcamaron@gmail.com');


insert into usuario (correo, tipo) values ('arenascr@gmail.com','E');
insert into empresa (nombre_empresa,password_empresa,numero_telefono,direccion,correo_tienda,provincia)
values ('Arenas','4a7d1ed414474e4033ac29ccb8653d9b','88654355','Heredia Centro','arenascr@gmail.com','Heredia');

insert into usuario (correo, tipo) values ('prueba@gmail.com','E');
insert into empresa (nombre_empresa,password_empresa,numero_telefono,direccion,correo_tienda,provincia)
values ('Prueba','123','88654355','Heredia','prueba@gmail.com','San jose');


--Clave 0000: 4a7d1ed414474e4033ac29ccb8653d9b

insert into producto 
(correo_tienda, descripcion,categoria)
values
('arenascr@gmail.com','jacket tela','abrigos');

insert into producto 
(correo_tienda, descripcion,categoria)
values
('arenascr@gmail.com','jacket cuero','abrigos');

insert into producto 
(correo_tienda, descripcion,categoria)
values
('arenascr@gmail.com','jacket botones','jeans');

insert into producto 
(correo_tienda, descripcion,categoria)
values
('arenascr@gmail.com','jacket largo','abrigos');

insert into producto 
(correo_tienda, descripcion,categoria)
values
('arenascr@gmail.com','jacket gorro','abrigos');

insert into producto 
(correo_tienda, descripcion,categoria)
values
('arenascr@gmail.com','abrigo','abrigos');

insert into producto 
(correo_tienda, descripcion,categoria)
values
('arenascr@gmail.com','abrigo largo','abrigos');

insert into producto 
(correo_tienda, descripcion,categoria)
values
('arenascr@gmail.com','abrigo gorro','abrigos');

insert into producto 
(correo_tienda, descripcion,categoria)
values
('arenascr@gmail.com','jacket cuero','abrigos');

insert into detalle_producto
(id_detalle,id_producto,cantidad,color,talla,precio,imagen,promocion,genero)
values
(1,1,3,'azul','S',25000,'../images/Abrigos/abrigo1.jpg','N','Hombre');

insert into detalle_producto
(id_detalle,id_producto,cantidad,color,talla,precio,imagen,promocion,genero)
values
(2,2,2,'cafe','M',20000,'../images/Abrigos/abrigo3.jpg','N','Hombre');

insert into detalle_producto
(id_detalle,id_producto,cantidad,color,talla,precio,imagen,promocion,genero)
values
(3,3,2,'negro','M',20000,'../images/Abrigos/abrigo2.jpg','N','Hombre');

insert into detalle_producto
(id_detalle,id_producto,cantidad,color,talla,precio,imagen,promocion,genero)
values
(4,4,2,'azul','M',20000,'../images/Abrigos/abrigo4.jpg','N','Hombre');

insert into detalle_producto
(id_detalle,id_producto,cantidad,color,talla,precio,imagen,promocion,genero)
values
(5,5,2,'azul','M',20000,'../images/Abrigos/abrigo5.jpg','N','Hombre');

insert into detalle_producto
(id_detalle,id_producto,cantidad,color,talla,precio,imagen,promocion,genero)
values
(6,6,2,'gris','M',20000,'../images/Abrigos/abrigo6.jpg','N','Hombre');

insert into detalle_producto
(id_detalle,id_producto,cantidad,color,talla,precio,imagen,promocion,genero)
values
(7,7,2,'cafe','M',20000,'../images/Abrigos/abrigo8.jpg','N','Hombre');

insert into detalle_producto
(id_detalle,id_producto,cantidad,color,talla,precio,imagen,promocion,genero)
values
(8,8,2,'cafe','M',20000,'../images/Abrigos/abrigo9.jpg','N','Hombre');

insert into detalle_producto
(id_detalle,id_producto,cantidad,color,talla,precio,imagen,promocion,genero)
values
(9,9,2,'cafe','M',20000,'../images/Abrigos/abrigo10.jpg','N','Hombre');


insert into producto(correo_tienda,descripcion) values ('prueba@gmail.com','Camisa tirantes');

PROMPT eliminó cliente
execute prc_eliminar_cliente('carcamaron@gmail.com');
PROMPT eliminó usuario
execute prc_eliminar_usuario('carcamaron@gmail.com');

commit;