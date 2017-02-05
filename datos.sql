insert into usuario (correo, tipo) values ('dacorcam@hotmail.com','C');
insert into cliente (nombre_cliente,apellido_cliente,password,correo_cliente) 
values ('Jose','Cordero','4a7d1ed414474e4033ac29ccb8653d9b','dacorcam@hotmail.com');

insert into usuario (correo, tipo) values ('carcamaron@gmail.com','C');
insert into cliente (nombre_cliente,apellido_cliente,password,correo_cliente) 
values ('Carlos','Camaron','4a7d1ed414474e4033ac29ccb8653d9b','carcamaron@gmail.com');

insert into usuario (correo, tipo) values ('zocorro@gmail.com','C');
insert into cliente (nombre_cliente,apellido_cliente,password,correo_cliente) 
values ('Carlos','Camaron','1234567uiop','zocorro@gmail.com');

insert into usuario (correo, tipo) values ('arenascr@gmail.com','E');
insert into empresa (nombre_empresa,password_empresa,numero_telefono,direccion,correo_tienda,provincia)
values ('Arenas','4a7d1ed414474e4033ac29ccb8653d9b','88654355','Heredia Centro','arenascr@gmail.com','Heredia');

insert into usuario (correo, tipo) values ('prueba@gmail.com','E');
insert into empresa (nombre_empresa,password_empresa,numero_telefono,direccion,correo_tienda,provincia)
values ('Prueba','123','88654355','Heredia','prueba@gmail.com','San jose');


--Clave 0000: 4a7d1ed414474e4033ac29ccb8653d9b
-------------------------producto hombres-------------------------------------------

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

---------------------Mujer---------------------------------
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
('arenascr@gmail.com','abrigo gorro','abrigos');

insert into producto 
(correo_tienda, descripcion,categoria)
values
('arenascr@gmail.com','abrigo corto','abrigos');

insert into producto 
(correo_tienda, descripcion,categoria)
values
('arenascr@gmail.com','abrigo largo','abrigos');

insert into producto 
(correo_tienda, descripcion,categoria)
values
('arenascr@gmail.com','abrigo verano','abrigos');

insert into producto 
(correo_tienda, descripcion,categoria)
values
('arenascr@gmail.com','abrigo corto','abrigos');

insert into producto 
(correo_tienda, descripcion,categoria)
values
('arenascr@gmail.com','abrigo corto','abrigos');

-------------vestidos-----------------------------------
insert into producto 
(correo_tienda, descripcion,categoria)
values
('arenascr@gmail.com','vestido largo','camisetas');

insert into producto 
(correo_tienda, descripcion,categoria)
values
('arenascr@gmail.com','vestido largo','camisetas');

insert into producto 
(correo_tienda, descripcion,categoria)
values
('arenascr@gmail.com','vestido largo','camisetas');

insert into producto 
(correo_tienda, descripcion,categoria)
values
('arenascr@gmail.com','vestido corto','camisetas');

-------------blusas------------------------------------
insert into producto 
(correo_tienda, descripcion,categoria)
values
('arenascr@gmail.com','blusa manga larga','camisas');

insert into producto 
(correo_tienda, descripcion,categoria)
values
('arenascr@gmail.com','blusa corta','camisas');

insert into producto 
(correo_tienda, descripcion,categoria)
values
('arenascr@gmail.com','blusa larga','camisas');

insert into producto 
(correo_tienda, descripcion,categoria)
values
('arenascr@gmail.com','blusa corta','camisas');

-----------jeans mujer-----------------------------------
insert into producto 
(correo_tienda, descripcion,categoria)
values
('arenascr@gmail.com','jeans manga larga','jeans');

insert into producto 
(correo_tienda, descripcion,categoria)
values
('arenascr@gmail.com','jeans con cuadros','jeans');

insert into producto 
(correo_tienda, descripcion,categoria)
values
('arenascr@gmail.com','jeans sin mangas','jeans');

insert into producto 
(correo_tienda, descripcion,categoria)
values
('arenascr@gmail.com','jeans manga larga','jeans');

-----------pantalon mujer-----------------------------------
insert into producto 
(correo_tienda, descripcion,categoria)
values
('arenascr@gmail.com','pantalon licra','pantalones');

insert into producto 
(correo_tienda, descripcion,categoria)
values
('arenascr@gmail.com','pantalon largo','pantalones');

insert into producto 
(correo_tienda, descripcion,categoria)
values
('arenascr@gmail.com','pantalon verano','pantalones');

insert into producto 
(correo_tienda, descripcion,categoria)
values
('arenascr@gmail.com','pantalon largo','pantalones');

-----------calzados mujer-----------------------------------
insert into producto 
(correo_tienda, descripcion,categoria)
values
('arenascr@gmail.com','botas al tobillo','calzado');

insert into producto 
(correo_tienda, descripcion,categoria)
values
('arenascr@gmail.com','tennis nike','calzado');

insert into producto 
(correo_tienda, descripcion,categoria)
values
('arenascr@gmail.com','zapatillas oficina','calzado');

insert into producto 
(correo_tienda, descripcion,categoria)
values
('arenascr@gmail.com','tennis k-light','calzado');

----------------detalle_producto hombres-------------------

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

-----------------------------------detalle_producto Mujer------------------------------------
insert into detalle_producto
(id_detalle,id_producto,cantidad,color,talla,precio,imagen,promocion,genero)
values
(10,10,2,'gris','M',20000,'../images/Abrigos/abrigo1_mujer.jpg','N','Mujer');

insert into detalle_producto
(id_detalle,id_producto,cantidad,color,talla,precio,imagen,promocion,genero)
values
(11,11,10,'negro','S',25000,'../images/Abrigos/abrigo2_mujer.jpg','N','Mujer');

insert into detalle_producto
(id_detalle,id_producto,cantidad,color,talla,precio,imagen,promocion,genero)
values
(12,12,15,'verde','S',25000,'../images/Abrigos/abrigo3_mujer.jpg','N','Mujer');

insert into detalle_producto
(id_detalle,id_producto,cantidad,color,talla,precio,imagen,promocion,genero)
values
(13,13,10,'gris','S',20000,'../images/Abrigos/abrigo4_mujer.jpg','N','Mujer');

insert into detalle_producto
(id_detalle,id_producto,cantidad,color,talla,precio,imagen,promocion,genero)
values
(14,14,5,'gris','S',20000,'../images/Abrigos/abrigo5_mujer.jpg','N','Mujer');

insert into detalle_producto
(id_detalle,id_producto,cantidad,color,talla,precio,imagen,promocion,genero)
values
(15,15,5,'café','L',20000,'../images/Abrigos/abrigo6_mujer.jpg','N','Mujer');

insert into detalle_producto
(id_detalle,id_producto,cantidad,color,talla,precio,imagen,promocion,genero)
values
(16,16,15,'blanco','L',25000,'../images/Abrigos/abrigo7_mujer.jpg','N','Mujer');

insert into detalle_producto
(id_detalle,id_producto,cantidad,color,talla,precio,imagen,promocion,genero)
values
(17,17,15,'negro','L',25000,'../images/Abrigos/abrigo8_mujer.jpg','N','Mujer');

insert into detalle_producto
(id_detalle,id_producto,cantidad,color,talla,precio,imagen,promocion,genero)
values
(18,18,4,'blanco','S',25000,'../images/Vestidos/Vestido01.jpg','N','Mujer');

insert into detalle_producto
(id_detalle,id_producto,cantidad,color,talla,precio,imagen,promocion,genero)
values
(19,19,10,'negro','M',25000,'../images/Vestidos/Vestido02.jpg','N','Mujer');

insert into detalle_producto
(id_detalle,id_producto,cantidad,color,talla,precio,imagen,promocion,genero)
values
(20,20,12,'negro','L',25000,'../images/Vestidos/Vestido03.jpg','N','Mujer');

insert into detalle_producto
(id_detalle,id_producto,cantidad,color,talla,precio,imagen,promocion,genero)
values
(21,21,15,'blanco','S',25000,'../images/Vestidos/Vestido04.jpg','N','Mujer');

insert into detalle_producto
(id_detalle,id_producto,cantidad,color,talla,precio,imagen,promocion,genero)
values
(22,22,4,'gris','S',20000,'../images/Blusas/blusas01.jpg','N','Mujer');

insert into detalle_producto
(id_detalle,id_producto,cantidad,color,talla,precio,imagen,promocion,genero)
values
(23,23,10,'blanco','M',25000,'../images/Blusas/blusas02.jpg','N','Mujer');

insert into detalle_producto
(id_detalle,id_producto,cantidad,color,talla,precio,imagen,promocion,genero)
values
(24,24,12,'negro','L',20000,'../images/Blusas/blusas03.jpg','N','Mujer');

insert into detalle_producto
(id_detalle,id_producto,cantidad,color,talla,precio,imagen,promocion,genero)
values
(25,25,15,'azul','S',25000,'../images/Blusas/blusas04.jpg','N','Mujer');

--
insert into detalle_producto
(id_detalle,id_producto,cantidad,color,talla,precio,imagen,promocion,genero)
values
(26,26,4,'azul','S',20000,'../images/Jeans/jeans01.jpg','N','Mujer');

insert into detalle_producto
(id_detalle,id_producto,cantidad,color,talla,precio,imagen,promocion,genero)
values
(27,27,10,'azul','M',25000,'../images/Jeans/jeans02.jpg','N','Mujer');

insert into detalle_producto
(id_detalle,id_producto,cantidad,color,talla,precio,imagen,promocion,genero)
values
(28,28,12,'blanco','L',20000,'../images/Jeans/jeans03.jpg','N','Mujer');

insert into detalle_producto
(id_detalle,id_producto,cantidad,color,talla,precio,imagen,promocion,genero)
values
(29,29,15,'negro','S',25000,'../images/Jeans/jeans04.jpg','N','Mujer');

---
insert into detalle_producto
(id_detalle,id_producto,cantidad,color,talla,precio,imagen,promocion,genero)
values
(30,30,4,'negro','S',20000,'../images/Pantalones/pantalon01negro.jpg','N','Mujer');

insert into detalle_producto
(id_detalle,id_producto,cantidad,color,talla,precio,imagen,promocion,genero)
values
(31,31,10,'azul','M',25000,'../images/Pantalones/pantalon02azul.jpg','N','Mujer');

insert into detalle_producto
(id_detalle,id_producto,cantidad,color,talla,precio,imagen,promocion,genero)
values
(32,32,12,'cafe','L',20000,'../images/Pantalones/pantalon03cafe.jpg','N','Mujer');

insert into detalle_producto
(id_detalle,id_producto,cantidad,color,talla,precio,imagen,promocion,genero)
values
(33,33,15,'azul','S',25000,'../images/Pantalones/pantalon04azul.jpg','N','Mujer');

---
insert into detalle_producto
(id_detalle,id_producto,cantidad,color,talla,precio,imagen,promocion,genero)
values
(34,34,4,'negro','S',20000,'../images/Calzados/calzadomujer01.jpg','N','Mujer');

insert into detalle_producto
(id_detalle,id_producto,cantidad,color,talla,precio,imagen,promocion,genero)
values
(35,35,10,'blanco','M',25000,'../images/Calzados/calzadomujer02.jpg','N','Mujer');

insert into detalle_producto
(id_detalle,id_producto,cantidad,color,talla,precio,imagen,promocion,genero)
values
(36,36,12,'negro','L',20000,'../images/Calzados/calzadomujer03.jpg','N','Mujer');

insert into detalle_producto
(id_detalle,id_producto,cantidad,color,talla,precio,imagen,promocion,genero)
values
(37,37,15,'gris','S',25000,'../images/Calzados/calzadomujer04.jpg','N','Mujer');

insert into producto(correo_tienda,descripcion) values ('prueba@gmail.com','Camisa tirantes');

PROMPT eliminó cliente
execute prc_eliminar_cliente('carcamaron@gmail.com');
PROMPT eliminó usuario
execute prc_eliminar_usuario('carcamaron@gmail.com');

commit;