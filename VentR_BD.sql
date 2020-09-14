PGDMP                          x            VentR    12.4    12.4 �    �           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            �           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            �           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            �           1262    16395    VentR    DATABASE     �   CREATE DATABASE "VentR" WITH TEMPLATE = template0 ENCODING = 'UTF8' LC_COLLATE = 'Spanish_Spain.1252' LC_CTYPE = 'Spanish_Spain.1252';
    DROP DATABASE "VentR";
                postgres    false            �           0    0    DATABASE "VentR"    COMMENT     4   COMMENT ON DATABASE "VentR" IS 'Products Database';
                   postgres    false    2977            �           0    0    VentR    DATABASE PROPERTIES     -   ALTER DATABASE "VentR" CONNECTION LIMIT = 6;
                     postgres    false            	            2615    16425    products    SCHEMA        CREATE SCHEMA products;
    DROP SCHEMA products;
                postgres    false            �           0    0    SCHEMA products    COMMENT     9   COMMENT ON SCHEMA products IS 'All related to products';
                   postgres    false    9                        2615    16426    users    SCHEMA        CREATE SCHEMA users;
    DROP SCHEMA users;
                postgres    false            �            1255    16628 0   agregar_busquedas_prod_usuario(integer, integer) 	   PROCEDURE     8  CREATE PROCEDURE products.agregar_busquedas_prod_usuario(idusuario integer, idproducto integer)
    LANGUAGE plpgsql
    AS $$
begin
	IF NOT EXISTS(SELECT * FROM products.sugerencia_producto_usuario WHERE id_usuario = idUsuario AND  id_producto = idProducto) THEN
	INSERT INTO products.sugerencia_producto_usuario (id_usuario,id_producto,cantidad_busqueda) VALUES (idUsuario,idProducto,1);
	ELSE
	UPDATE products.sugerencia_producto_usuario
		SET cantidad_busqueda = cantidad_busqueda + 1
	WHERE id_usuario = idUsuario AND id_producto = idProducto;
	END IF;
end; 
$$;
 _   DROP PROCEDURE products.agregar_busquedas_prod_usuario(idusuario integer, idproducto integer);
       products          postgres    false    9                       1255    32814    pa_activar_producto(integer) 	   PROCEDURE     �   CREATE PROCEDURE products.pa_activar_producto(p_idproducto integer)
    LANGUAGE plpgsql
    AS $$
begin
	UPDATE products.producto set Activo=True where id_producto=p_idProducto;
end $$;
 C   DROP PROCEDURE products.pa_activar_producto(p_idproducto integer);
       products          postgres    false    9            �            1255    24619    pa_activar_proveedor(integer) 	   PROCEDURE       CREATE PROCEDURE products.pa_activar_proveedor(p_idproveedor integer)
    LANGUAGE plpgsql
    AS $$
begin
		UPDATE products.proveedor set Activo=True where id_proveedor=p_idProveedor;
	update products.producto set activo=True where id_proveedor=p_idProveedor;
end;
$$;
 E   DROP PROCEDURE products.pa_activar_proveedor(p_idproveedor integer);
       products          postgres    false    9                       1255    32825    pa_actualizar_sync(integer) 	   PROCEDURE     �   CREATE PROCEDURE products.pa_actualizar_sync(p_id_proveedor integer)
    LANGUAGE plpgsql
    AS $$
begin
	DELETE FROM products.sincronizacion
	WHERE id_proveedor = p_id_proveedor;
end;
$$;
 D   DROP PROCEDURE products.pa_actualizar_sync(p_id_proveedor integer);
       products          postgres    false    9            	           1255    32818 .   pa_agregar_cantidad(integer, integer, integer) 	   PROCEDURE     ;  CREATE PROCEDURE products.pa_agregar_cantidad(p_id_producto_proveedor integer, p_id_proveedor integer, p_cantidad integer)
    LANGUAGE plpgsql
    AS $$
begin
	UPDATE products.producto set cantidad=cantidad+p_cantidad 
	where id_producto_proveedor=p_id_producto_proveedor and id_proveedor=p_id_proveedor;
end;
$$;
 z   DROP PROCEDURE products.pa_agregar_cantidad(p_id_producto_proveedor integer, p_id_proveedor integer, p_cantidad integer);
       products          postgres    false    9                       1255    32811 5   pa_agregar_carrito_compras(integer, integer, integer) 	   PROCEDURE     �  CREATE PROCEDURE products.pa_agregar_carrito_compras(idusuario integer, idproducto integer, cantidad_agregada integer)
    LANGUAGE plpgsql
    AS $$
declare
idCarritoCompras int;
idProveedor int;
idProductoProveedor int;
begin
IF NOT EXISTS(select * from products.carrito_compras where id_usuario=idUsuario) then
insert into products.carrito_compras(id_usuario) values(idUsuario);
idCarritoCompras:= (Select id_carrito_compras from products.carrito_compras where id_usuario=idUsuario);
 call products.pa_insertar_carrito_compras_producto(idCarritoCompras,idProducto,cantidad_agregada);
else 
idCarritoCompras:= (Select id_carrito_compras from products.carrito_compras where id_usuario=idUsuario);
 call products.pa_insertar_carrito_compras_producto(idCarritoCompras,idProducto,cantidad_agregada);
END IF;

idProveedor:= (select  id_proveedor from products.producto where id_producto=idProducto);
idProductoProveedor:=(select  id_producto_proveedor from products.producto where id_producto=idProducto);

INSERT INTO products.sincronizacion(
	 id_proveedor, id_producto_proveedor, cantidad)
	VALUES (idProveedor, idProductoProveedor, cantidad_agregada);

end;
$$;
 v   DROP PROCEDURE products.pa_agregar_carrito_compras(idusuario integer, idproducto integer, cantidad_agregada integer);
       products          postgres    false    9                       1255    32817 n   pa_agregar_producto(character varying, money, character varying, character varying, integer, integer, integer) 	   PROCEDURE     �  CREATE PROCEDURE products.pa_agregar_producto(p_nombre character varying, p_precio money, p_url character varying, p_detalle character varying, p_cantidad integer, p_id_proveedor integer, p_id_producto_proveedor integer)
    LANGUAGE plpgsql
    AS $$
begin
	INSERT INTO products.producto(
	nombre, precio, url_img, detalle, cantidad, activo, id_proveedor, id_producto_proveedor)
	VALUES (p_nombre, p_precio, p_url, p_detalle, p_cantidad,
			true, p_id_proveedor, p_id_producto_proveedor);
end;
$$;
 �   DROP PROCEDURE products.pa_agregar_producto(p_nombre character varying, p_precio money, p_url character varying, p_detalle character varying, p_cantidad integer, p_id_proveedor integer, p_id_producto_proveedor integer);
       products          postgres    false    9                       1255    32821 }   pa_agregar_productos_proveedores(character varying, numeric, character varying, character varying, integer, integer, integer) 	   PROCEDURE     +  CREATE PROCEDURE products.pa_agregar_productos_proveedores(p_nombre character varying, p_precio numeric, p_url character varying, p_detalle character varying, p_cantidad integer, p_id_proveedor integer, p_id_producto_proveedor integer)
    LANGUAGE plpgsql
    AS $$

declare
   precio2 money:=CAST (p_precio AS money);

begin
if exists(select * from products.producto where id_producto_proveedor=p_id_producto_proveedor 
AND id_proveedor=p_id_proveedor  )then
update products.producto
	set nombre=p_nombre,
	precio=precio2,
	url_img=p_url,
	detalle=p_detalle, cantidad=p_cantidad
	where  id_proveedor=p_id_proveedor and id_producto_proveedor=p_id_producto_proveedor;
else
call products.pa_agregar_producto(p_nombre,precio2,p_url,p_detalle,p_cantidad,p_id_proveedor,p_id_producto_proveedor);
 end if;
	
end
$$;
 �   DROP PROCEDURE products.pa_agregar_productos_proveedores(p_nombre character varying, p_precio numeric, p_url character varying, p_detalle character varying, p_cantidad integer, p_id_proveedor integer, p_id_producto_proveedor integer);
       products          postgres    false    9            �            1255    24617 '   pa_agregar_proveedor(character varying) 	   PROCEDURE     �   CREATE PROCEDURE products.pa_agregar_proveedor(p_nombre character varying)
    LANGUAGE plpgsql
    AS $$
begin
 INSERT INTO products.proveedor(nombre) VALUES(p_nombre);
end; $$;
 J   DROP PROCEDURE products.pa_agregar_proveedor(p_nombre character varying);
       products          postgres    false    9                       1255    32816 &   pa_aumentar_cantidad(integer, integer) 	   PROCEDURE     �  CREATE PROCEDURE products.pa_aumentar_cantidad(idusuario integer, idproducto integer)
    LANGUAGE plpgsql
    AS $$
declare
idCarritoCompras int;
begin
idCarritoCompras:= (select id_carrito_compras from products.carrito_compras where id_usuario=idUsuario);
update products.carrito_compras_producto
set cantidad= cantidad+1 where id_producto=idProducto and id_carrito_compras=idCarritoCompras;  
end;
$$;
 U   DROP PROCEDURE products.pa_aumentar_cantidad(idusuario integer, idproducto integer);
       products          postgres    false    9                       1255    24638 D   pa_crear_llave(integer, character varying, timestamp with time zone) 	   PROCEDURE        CREATE PROCEDURE products.pa_crear_llave(idproveedor integer, llave character varying, fechavencimiento timestamp with time zone)
    LANGUAGE plpgsql
    AS $$
begin
INSERT INTO products.llave (id_proveedor,llave,fecha_vencimiento) 
VALUES (idProveedor,Llave,fechaVencimiento);
end;
$$;
 �   DROP PROCEDURE products.pa_crear_llave(idproveedor integer, llave character varying, fechavencimiento timestamp with time zone);
       products          postgres    false    9                       1255    32813    pa_desactivar_producto(integer) 	   PROCEDURE       CREATE PROCEDURE products.pa_desactivar_producto(p_idproducto integer)
    LANGUAGE plpgsql
    AS $$
begin
	UPDATE products.producto set Activo=False where id_producto=p_idProducto;
	DELETE FROM carrito_compras_producto WHERE id_producto = p_idProducto;
end $$;
 F   DROP PROCEDURE products.pa_desactivar_producto(p_idproducto integer);
       products          postgres    false    9            �            1255    24618     pa_desactivar_proveedor(integer) 	   PROCEDURE       CREATE PROCEDURE products.pa_desactivar_proveedor(p_idproveedor integer)
    LANGUAGE plpgsql
    AS $$
begin
	UPDATE products.proveedor set Activo=False where id_proveedor=p_idProveedor;
	update products.producto set activo=false where id_proveedor=p_idProveedor;
end;
$$;
 H   DROP PROCEDURE products.pa_desactivar_proveedor(p_idproveedor integer);
       products          postgres    false    9                       1255    32815 '   pa_disminuir_cantidad(integer, integer) 	   PROCEDURE     �  CREATE PROCEDURE products.pa_disminuir_cantidad(idusuario integer, idproducto integer)
    LANGUAGE plpgsql
    AS $$
declare
idCarritoCompras int;
begin
idCarritoCompras:= (select id_carrito_compras from products.carrito_compras where id_usuario=idUsuario);
update products.carrito_compras_producto
set cantidad= cantidad-1 where id_producto=idProducto and id_carrito_compras=idCarritoCompras;
   
end;
$$;
 V   DROP PROCEDURE products.pa_disminuir_cantidad(idusuario integer, idproducto integer);
       products          postgres    false    9                        1255    32810    pa_eliminar_carrito(integer) 	   PROCEDURE       CREATE PROCEDURE products.pa_eliminar_carrito(idcarritocompras integer)
    LANGUAGE plpgsql
    AS $$
begin
delete from products.carrito_compras_producto where id_carrito_compras=idCarritoCompras;
delete from products.carrito_compras where id_carrito_compras=idCarritoCompras;
end $$;
 G   DROP PROCEDURE products.pa_eliminar_carrito(idcarritocompras integer);
       products          postgres    false    9            �            1255    24642 )   pa_eliminar_del_carrito(integer, integer) 	   PROCEDURE     �  CREATE PROCEDURE products.pa_eliminar_del_carrito(idusuario integer, idproducto integer)
    LANGUAGE plpgsql
    AS $$
declare
idCarritoCompras int;
begin
idCarritoCompras:= (select id_carrito_compras from products.carrito_compras where id_usuario=idUsuario);
delete from products.carrito_compras_producto
where id_producto=idProducto and id_carrito_compras=idCarritoCompras;  
end;
$$;
 X   DROP PROCEDURE products.pa_eliminar_del_carrito(idusuario integer, idproducto integer);
       products          postgres    false    9            �            1255    24666 3   pa_eliminar_del_carrito(integer, character varying) 	   PROCEDURE     �  CREATE PROCEDURE products.pa_eliminar_del_carrito(idproveedor integer, llave character varying)
    LANGUAGE plpgsql
    AS $$
declare

begin

IF EXISTS (SELECT * FROM products.llave 
		   WHERE id_proveedor=idProveedor 
		   AND  llave=Llave 
		   AND fecha_vencimiento < now())THEN
SELECT id_producto_proveedor,cantidad FROM products.sincronizacion
WHERE id_proveedor=idProveedor;
END IF;
end;
$$;
 _   DROP PROCEDURE products.pa_eliminar_del_carrito(idproveedor integer, llave character varying);
       products          postgres    false    9                       1255    24639 #   pa_eliminar_llave_inactiva(integer) 	   PROCEDURE     �   CREATE PROCEDURE products.pa_eliminar_llave_inactiva(idproveedor integer)
    LANGUAGE plpgsql
    AS $$
begin
DELETE FROM products.llave
WHERE id_proveedor=idProveedor;
end;
$$;
 I   DROP PROCEDURE products.pa_eliminar_llave_inactiva(idproveedor integer);
       products          postgres    false    9            �            1255    24620    pa_eliminar_proveedor(integer) 	   PROCEDURE     �   CREATE PROCEDURE products.pa_eliminar_proveedor(p_idproveedor integer)
    LANGUAGE plpgsql
    AS $$
begin
delete from products.proveedor where id_proveedor=p_idproveedor;

end;
$$;
 F   DROP PROCEDURE products.pa_eliminar_proveedor(p_idproveedor integer);
       products          postgres    false    9                       1255    24640    pa_get_llave_proveedor(integer)    FUNCTION     K  CREATE FUNCTION products.pa_get_llave_proveedor(idproveedor integer) RETURNS TABLE(id_llave integer, llave character varying, id_proveedor integer, fecha_vencimiento timestamp with time zone)
    LANGUAGE sql
    AS $$
	SELECT id_llave,llave,id_proveedor, fecha_vencimiento
FROM products.llave 
WHERE id_proveedor=idProveedor;
$$;
 D   DROP FUNCTION products.pa_get_llave_proveedor(idproveedor integer);
       products          postgres    false    9                       1255    32812 ?   pa_insertar_carrito_compras_producto(integer, integer, integer) 	   PROCEDURE     �  CREATE PROCEDURE products.pa_insertar_carrito_compras_producto(idcarritocompras integer, idproducto integer, cantidad_agregada integer)
    LANGUAGE plpgsql
    AS $$
begin
if exists (select * from products.carrito_compras_producto where id_carrito_compras=idCarritoCompras 
		   and id_producto=idProducto) then
update products.carrito_compras_producto
set cantidad=cantidad+cantidad_agregada
where id_producto=idProducto and id_carrito_compras=idCarritoCompras;

    ELSE 
INSERT INTO products.carrito_compras_producto(
	 id_carrito_compras, id_producto, cantidad)
	VALUES (idCarritoCompras,idProducto,cantidad_agregada);
	
	end if;
end;
$$;
 �   DROP PROCEDURE products.pa_insertar_carrito_compras_producto(idcarritocompras integer, idproducto integer, cantidad_agregada integer);
       products          postgres    false    9            �            1255    16593 ?   pa_insertar_producto_carrito_compras(integer, integer, integer) 	   PROCEDURE     +  CREATE PROCEDURE products.pa_insertar_producto_carrito_compras(idcarrito integer, idproducto integer, cantidad integer)
    LANGUAGE plpgsql
    AS $$
begin
INSERT INTO products.carrito_compras_producto(
	 id_carrito_compras, id_producto, cantidad)
	VALUES (idCarrito,idProducto,cantidad);
end; $$;
 w   DROP PROCEDURE products.pa_insertar_producto_carrito_compras(idcarrito integer, idproducto integer, cantidad integer);
       products          postgres    false    9            �            1255    16634 %   pa_listarproductosporusuario(integer)    FUNCTION     <  CREATE FUNCTION products.pa_listarproductosporusuario(idusuario integer) RETURNS TABLE(idproducto integer, nombre character varying, precio money, urlimg character varying, detalle character varying, activo boolean, cantidad integer, idproveedor integer, busquedas integer)
    LANGUAGE sql
    AS $$

SELECT id_producto,nombre,precio,url_img,detalle,activo,cantidad,id_proveedor,
0 as cant_busquedas
FROM products.producto
UNION
SELECT pr.id_producto,nombre,precio,url_img,detalle,activo,cantidad,id_proveedor,
max(coalesce(cantidad_busqueda,0)) as cant_busquedas
FROM products.producto as pr 
	FULL OUTER JOIN products.sugerencia_producto_usuario as spu 
	ON pr.id_producto  = spu.id_producto
WHERE pr.activo<>false and pr.cantidad > 0 and spu.id_usuario = idUsuario 
GROUP BY pr.id_producto
ORDER BY cant_busquedas DESC

$$;
 H   DROP FUNCTION products.pa_listarproductosporusuario(idusuario integer);
       products          postgres    false    9            �            1255    16591    pa_listarproveedor()    FUNCTION     �   CREATE FUNCTION products.pa_listarproveedor() RETURNS TABLE(id_proveedor integer, nombre character varying, activo boolean)
    LANGUAGE sql
    AS $$

	select id_proveedor,nombre,activo from products.proveedor where activo<>false;

$$;
 -   DROP FUNCTION products.pa_listarproveedor();
       products          postgres    false    9            �            1255    24621 2   pa_modificar_proveedor(integer, character varying) 	   PROCEDURE     �   CREATE PROCEDURE products.pa_modificar_proveedor(p_idproveedor integer, p_nombre character varying)
    LANGUAGE plpgsql
    AS $$
begin
update  products.proveedor
set nombre=p_nombre
where id_proveedor=p_idproveedor;
end;
$$;
 c   DROP PROCEDURE products.pa_modificar_proveedor(p_idproveedor integer, p_nombre character varying);
       products          postgres    false    9                       1255    32823 -   pa_sincronizacion(integer, character varying)    FUNCTION     �  CREATE FUNCTION products.pa_sincronizacion(idproveedor integer, codllave character varying) RETURNS TABLE(id_producto_proveedor integer, cantidad integer)
    LANGUAGE sql
    AS $$
SELECT DISTINCT S.id_producto_proveedor,S.cantidad 
FROM products.sincronizacion S JOIN  products.llave cod ON S.id_proveedor = cod.id_proveedor
 WHERE cod.id_proveedor=idProveedor 
		   AND  cod.llave=codLlave
		   AND cod.fecha_vencimiento > now();
$$;
 [   DROP FUNCTION products.pa_sincronizacion(idproveedor integer, codllave character varying);
       products          postgres    false    9            
           1255    32819 0   verificadorproveedor(integer, character varying)    FUNCTION     �  CREATE FUNCTION products.verificadorproveedor(idproveedor integer, llave_proveedor character varying) RETURNS boolean
    LANGUAGE plpgsql
    AS $$
declare
salida boolean;
begin
if exists (select * from products.llave where
		   id_proveedor=idproveedor and llave=llave_proveedor and fecha_vencimiento>now())then
 salida:=TRUE;
else
 salida:= FALSE;
 end if;

 return salida;
end
$$;
 e   DROP FUNCTION products.verificadorproveedor(idproveedor integer, llave_proveedor character varying);
       products          postgres    false    9            �            1255    32809    pa_buscarproveedor(integer) 	   PROCEDURE     �   CREATE PROCEDURE public.pa_buscarproveedor(idprovedor integer)
    LANGUAGE plpgsql
    AS $$
begin
select id_proveedor,nombre,activo from products.Proveedor where activo<>false and id_proveedor=idProvedor;
end; $$;
 >   DROP PROCEDURE public.pa_buscarproveedor(idprovedor integer);
       public          postgres    false            �            1255    24625    pa_eliminar_carrito(integer) 	   PROCEDURE       CREATE PROCEDURE public.pa_eliminar_carrito(idcarritocompras integer)
    LANGUAGE plpgsql
    AS $$
begin
delete from products.carrito_compras_producto where id_carrito_compras=idCarritoCompras;
delete from products.carrito_compras where id_carrito_compras=idCarritoCompras;
end $$;
 E   DROP PROCEDURE public.pa_eliminar_carrito(idcarritocompras integer);
       public          postgres    false            �            1255    16592 ?   pa_insertar_producto_carrito_compras(integer, integer, integer) 	   PROCEDURE     )  CREATE PROCEDURE public.pa_insertar_producto_carrito_compras(idcarrito integer, idproducto integer, cantidad integer)
    LANGUAGE plpgsql
    AS $$
begin
INSERT INTO products.carrito_compras_producto(
	 id_carrito_compras, id_producto, cantidad)
	VALUES (idCarrito,idProducto,cantidad);
end; $$;
 u   DROP PROCEDURE public.pa_insertar_producto_carrito_compras(idcarrito integer, idproducto integer, cantidad integer);
       public          postgres    false            �            1255    16590    pa_listarproveedor() 	   PROCEDURE     �   CREATE PROCEDURE public.pa_listarproveedor()
    LANGUAGE plpgsql
    AS $$
begin
select id_proveedor,nombre,activo from products.Proveedor where activo<>false;
end; $$;
 ,   DROP PROCEDURE public.pa_listarproveedor();
       public          postgres    false            �            1255    24626 $   pa_registrar_venta(integer, numeric) 	   PROCEDURE       CREATE PROCEDURE public.pa_registrar_venta(idusuario integer, totalfactura numeric)
    LANGUAGE plpgsql
    AS $$
declare
   facturaTotal money:=CAST (totalFactura AS money);
begin
INSERT INTO products.venta(id_usuario, total_venta)
	VALUES (idUsuario,facturaTotal);
end $$;
 S   DROP PROCEDURE public.pa_registrar_venta(idusuario integer, totalfactura numeric);
       public          postgres    false            �            1255    24624 !   pa_venderproductocarrito(integer)    FUNCTION     �  CREATE FUNCTION public.pa_venderproductocarrito(idusuario integer) RETURNS money
    LANGUAGE plpgsql
    AS $$

declare
	registro Record;
	precioTotal money;
	
	idCarritoCompras int:=(select id_carrito_compras from products.carrito_compras where id_usuario=idUsuario);

	curl CURSOR FOR  SELECT * FROM products.carrito_compras_producto  where id_carrito_compras=idCarritoCompras;
begin
precioTotal:= (select  SUM(p.precio*c.cantidad)as total_venta from products.carrito_compras_producto c FULL OUTER JOIN 
products.producto p on  c.id_producto=p.id_producto where c.id_carrito_compras=idCarritoCompras); 
Open curl;
fetch curl into registro;

while(FOUND)loop
update products.producto
set cantidad=(cantidad-registro.cantidad)
where id_producto=registro.id_producto;

Raise notice 'Se ha modificado';

fetch curl into registro;

end loop;
call pa_eliminar_carrito(idCarritoCompras);

return precioTotal;
end $$;
 B   DROP FUNCTION public.pa_venderproductocarrito(idusuario integer);
       public          postgres    false            �            1255    24623 M   pa_actualizar_usuario(integer, character varying, character varying, integer) 	   PROCEDURE       CREATE PROCEDURE users.pa_actualizar_usuario(u_id integer, u_correo character varying, u_contras character varying, u_rol integer)
    LANGUAGE plpgsql
    AS $$
begin
update users.usuario set correo=u_correo,
contrasennia=u_contras,id_rol=u_rol where id_usuario=u_id;
end$$;
 �   DROP PROCEDURE users.pa_actualizar_usuario(u_id integer, u_correo character varying, u_contras character varying, u_rol integer);
       users          postgres    false    4            �            1255    24622    pa_eliminar_usuario(integer) 	   PROCEDURE     �   CREATE PROCEDURE users.pa_eliminar_usuario(u_id integer)
    LANGUAGE plpgsql
    AS $$
begin
delete from users.usuario where id_usuario=u_id;
end$$;
 8   DROP PROCEDURE users.pa_eliminar_usuario(u_id integer);
       users          postgres    false    4            �            1255    16620 B   pa_insertar_usuario(character varying, character varying, integer) 	   PROCEDURE       CREATE PROCEDURE users.pa_insertar_usuario(u_correo character varying, u_contrasennia character varying, u_idrol integer)
    LANGUAGE plpgsql
    AS $$
begin
	insert into users.usuario(correo,contrasennia,id_rol)
	values(u_correo,u_contrasennia,u_idRol);
end;$$;
 y   DROP PROCEDURE users.pa_insertar_usuario(u_correo character varying, u_contrasennia character varying, u_idrol integer);
       users          postgres    false    4            �            1259    16487    carrito_compras    TABLE     t   CREATE TABLE products.carrito_compras (
    id_carrito_compras integer NOT NULL,
    id_usuario integer NOT NULL
);
 %   DROP TABLE products.carrito_compras;
       products         heap    postgres    false    9            �            1259    16485 &   carrito_compras_id_carrito_compras_seq    SEQUENCE     �   CREATE SEQUENCE products.carrito_compras_id_carrito_compras_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 ?   DROP SEQUENCE products.carrito_compras_id_carrito_compras_seq;
       products          postgres    false    213    9            �           0    0 &   carrito_compras_id_carrito_compras_seq    SEQUENCE OWNED BY     u   ALTER SEQUENCE products.carrito_compras_id_carrito_compras_seq OWNED BY products.carrito_compras.id_carrito_compras;
          products          postgres    false    212            �            1259    16604    carrito_compras_producto    TABLE     �   CREATE TABLE products.carrito_compras_producto (
    id_carrito_compras_producto integer NOT NULL,
    id_carrito_compras integer NOT NULL,
    id_producto integer NOT NULL,
    cantidad integer NOT NULL
);
 .   DROP TABLE products.carrito_compras_producto;
       products         heap    postgres    false    9            �            1259    16602 8   carrito_compras_producto_id_carrito_compras_producto_seq    SEQUENCE     �   CREATE SEQUENCE products.carrito_compras_producto_id_carrito_compras_producto_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 Q   DROP SEQUENCE products.carrito_compras_producto_id_carrito_compras_producto_seq;
       products          postgres    false    221    9            �           0    0 8   carrito_compras_producto_id_carrito_compras_producto_seq    SEQUENCE OWNED BY     �   ALTER SEQUENCE products.carrito_compras_producto_id_carrito_compras_producto_seq OWNED BY products.carrito_compras_producto.id_carrito_compras_producto;
          products          postgres    false    220            �            1259    16532    llave    TABLE     �   CREATE TABLE products.llave (
    id_llave integer NOT NULL,
    llave character varying NOT NULL,
    fecha_vencimiento timestamp with time zone NOT NULL,
    id_proveedor integer NOT NULL
);
    DROP TABLE products.llave;
       products         heap    postgres    false    9            �           0    0    TABLE llave    COMMENT     A   COMMENT ON TABLE products.llave IS 'llave para los proveedores';
          products          postgres    false    215            �            1259    16530    llave_id_llave_seq    SEQUENCE     �   CREATE SEQUENCE products.llave_id_llave_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 +   DROP SEQUENCE products.llave_id_llave_seq;
       products          postgres    false    9    215            �           0    0    llave_id_llave_seq    SEQUENCE OWNED BY     M   ALTER SEQUENCE products.llave_id_llave_seq OWNED BY products.llave.id_llave;
          products          postgres    false    214            �            1259    16448    producto    TABLE     _  CREATE TABLE products.producto (
    id_producto integer NOT NULL,
    nombre character varying(100) NOT NULL,
    precio money NOT NULL,
    url_img character varying(500) NOT NULL,
    detalle character varying(500),
    cantidad integer NOT NULL,
    activo boolean NOT NULL,
    id_proveedor integer NOT NULL,
    id_producto_proveedor integer
);
    DROP TABLE products.producto;
       products         heap    postgres    false    9            �           0    0    TABLE producto    COMMENT     J   COMMENT ON TABLE products.producto IS 'Tabla que contiene los productos';
          products          postgres    false    207            �            1259    16446    producto_id_producto_seq    SEQUENCE     �   CREATE SEQUENCE products.producto_id_producto_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 1   DROP SEQUENCE products.producto_id_producto_seq;
       products          postgres    false    207    9            �           0    0    producto_id_producto_seq    SEQUENCE OWNED BY     Y   ALTER SEQUENCE products.producto_id_producto_seq OWNED BY products.producto.id_producto;
          products          postgres    false    206            �            1259    16440 	   proveedor    TABLE     �   CREATE TABLE products.proveedor (
    id_proveedor integer NOT NULL,
    nombre character varying(100) NOT NULL,
    activo boolean DEFAULT true NOT NULL
);
    DROP TABLE products.proveedor;
       products         heap    postgres    false    9            �            1259    16438    proveedor_id_proveedor_seq    SEQUENCE     �   CREATE SEQUENCE products.proveedor_id_proveedor_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 3   DROP SEQUENCE products.proveedor_id_proveedor_seq;
       products          postgres    false    205    9            �           0    0    proveedor_id_proveedor_seq    SEQUENCE OWNED BY     ]   ALTER SEQUENCE products.proveedor_id_proveedor_seq OWNED BY products.proveedor.id_proveedor;
          products          postgres    false    204            �            1259    24645    sincronizacion    TABLE     �   CREATE TABLE products.sincronizacion (
    id_sincronizacion integer NOT NULL,
    id_proveedor integer,
    id_producto_proveedor integer,
    id_producto integer,
    cantidad integer
);
 $   DROP TABLE products.sincronizacion;
       products         heap    postgres    false    9            �            1259    24643 $   sincronizacion_id_sincronizacion_seq    SEQUENCE     �   CREATE SEQUENCE products.sincronizacion_id_sincronizacion_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 =   DROP SEQUENCE products.sincronizacion_id_sincronizacion_seq;
       products          postgres    false    223    9            �           0    0 $   sincronizacion_id_sincronizacion_seq    SEQUENCE OWNED BY     q   ALTER SEQUENCE products.sincronizacion_id_sincronizacion_seq OWNED BY products.sincronizacion.id_sincronizacion;
          products          postgres    false    222            �            1259    16573    sugerencia_producto_usuario    TABLE     �   CREATE TABLE products.sugerencia_producto_usuario (
    id_sugerencia_producto_usuario integer NOT NULL,
    id_usuario integer NOT NULL,
    id_producto integer NOT NULL,
    cantidad_busqueda integer DEFAULT 0 NOT NULL
);
 1   DROP TABLE products.sugerencia_producto_usuario;
       products         heap    postgres    false    9            �           0    0 !   TABLE sugerencia_producto_usuario    COMMENT     d   COMMENT ON TABLE products.sugerencia_producto_usuario IS 'Sugerencias personalizadas, por usuario';
          products          postgres    false    219            �            1259    16571 >   sugerencia_producto_usuario_id_sugerencia_producto_usuario_seq    SEQUENCE     �   CREATE SEQUENCE products.sugerencia_producto_usuario_id_sugerencia_producto_usuario_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 W   DROP SEQUENCE products.sugerencia_producto_usuario_id_sugerencia_producto_usuario_seq;
       products          postgres    false    9    219            �           0    0 >   sugerencia_producto_usuario_id_sugerencia_producto_usuario_seq    SEQUENCE OWNED BY     �   ALTER SEQUENCE products.sugerencia_producto_usuario_id_sugerencia_producto_usuario_seq OWNED BY products.sugerencia_producto_usuario.id_sugerencia_producto_usuario;
          products          postgres    false    218            �            1259    16556    venta    TABLE     �   CREATE TABLE products.venta (
    id_venta integer NOT NULL,
    id_usuario integer NOT NULL,
    total_venta money NOT NULL
);
    DROP TABLE products.venta;
       products         heap    postgres    false    9            �           0    0    TABLE venta    COMMENT     G   COMMENT ON TABLE products.venta IS 'Ventas que realizan los usuarios';
          products          postgres    false    217            �            1259    16554    venta_id_venta_seq    SEQUENCE     �   CREATE SEQUENCE products.venta_id_venta_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 +   DROP SEQUENCE products.venta_id_venta_seq;
       products          postgres    false    217    9            �           0    0    venta_id_venta_seq    SEQUENCE OWNED BY     M   ALTER SEQUENCE products.venta_id_venta_seq OWNED BY products.venta.id_venta;
          products          postgres    false    216            �            1259    16464    rol    TABLE     Z   CREATE TABLE users.rol (
    id_rol integer NOT NULL,
    nombre character varying(30)
);
    DROP TABLE users.rol;
       users         heap    postgres    false    4            �            1259    16462    rol_id_rol_seq    SEQUENCE     �   CREATE SEQUENCE users.rol_id_rol_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 $   DROP SEQUENCE users.rol_id_rol_seq;
       users          postgres    false    209    4            �           0    0    rol_id_rol_seq    SEQUENCE OWNED BY     ?   ALTER SEQUENCE users.rol_id_rol_seq OWNED BY users.rol.id_rol;
          users          postgres    false    208            �            1259    16472    usuario    TABLE     �   CREATE TABLE users.usuario (
    id_usuario integer NOT NULL,
    correo character varying(60) NOT NULL,
    contrasennia character varying(20) NOT NULL,
    id_rol integer NOT NULL
);
    DROP TABLE users.usuario;
       users         heap    postgres    false    4            �            1259    16470    usuario_id_usuario_seq    SEQUENCE     �   CREATE SEQUENCE users.usuario_id_usuario_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 ,   DROP SEQUENCE users.usuario_id_usuario_seq;
       users          postgres    false    4    211            �           0    0    usuario_id_usuario_seq    SEQUENCE OWNED BY     O   ALTER SEQUENCE users.usuario_id_usuario_seq OWNED BY users.usuario.id_usuario;
          users          postgres    false    210            �
           2604    16490 "   carrito_compras id_carrito_compras    DEFAULT     �   ALTER TABLE ONLY products.carrito_compras ALTER COLUMN id_carrito_compras SET DEFAULT nextval('products.carrito_compras_id_carrito_compras_seq'::regclass);
 S   ALTER TABLE products.carrito_compras ALTER COLUMN id_carrito_compras DROP DEFAULT;
       products          postgres    false    213    212    213            �
           2604    16607 4   carrito_compras_producto id_carrito_compras_producto    DEFAULT     �   ALTER TABLE ONLY products.carrito_compras_producto ALTER COLUMN id_carrito_compras_producto SET DEFAULT nextval('products.carrito_compras_producto_id_carrito_compras_producto_seq'::regclass);
 e   ALTER TABLE products.carrito_compras_producto ALTER COLUMN id_carrito_compras_producto DROP DEFAULT;
       products          postgres    false    220    221    221            �
           2604    16535    llave id_llave    DEFAULT     t   ALTER TABLE ONLY products.llave ALTER COLUMN id_llave SET DEFAULT nextval('products.llave_id_llave_seq'::regclass);
 ?   ALTER TABLE products.llave ALTER COLUMN id_llave DROP DEFAULT;
       products          postgres    false    215    214    215            �
           2604    16451    producto id_producto    DEFAULT     �   ALTER TABLE ONLY products.producto ALTER COLUMN id_producto SET DEFAULT nextval('products.producto_id_producto_seq'::regclass);
 E   ALTER TABLE products.producto ALTER COLUMN id_producto DROP DEFAULT;
       products          postgres    false    206    207    207            �
           2604    16443    proveedor id_proveedor    DEFAULT     �   ALTER TABLE ONLY products.proveedor ALTER COLUMN id_proveedor SET DEFAULT nextval('products.proveedor_id_proveedor_seq'::regclass);
 G   ALTER TABLE products.proveedor ALTER COLUMN id_proveedor DROP DEFAULT;
       products          postgres    false    204    205    205            �
           2604    24648     sincronizacion id_sincronizacion    DEFAULT     �   ALTER TABLE ONLY products.sincronizacion ALTER COLUMN id_sincronizacion SET DEFAULT nextval('products.sincronizacion_id_sincronizacion_seq'::regclass);
 Q   ALTER TABLE products.sincronizacion ALTER COLUMN id_sincronizacion DROP DEFAULT;
       products          postgres    false    223    222    223            �
           2604    16576 :   sugerencia_producto_usuario id_sugerencia_producto_usuario    DEFAULT     �   ALTER TABLE ONLY products.sugerencia_producto_usuario ALTER COLUMN id_sugerencia_producto_usuario SET DEFAULT nextval('products.sugerencia_producto_usuario_id_sugerencia_producto_usuario_seq'::regclass);
 k   ALTER TABLE products.sugerencia_producto_usuario ALTER COLUMN id_sugerencia_producto_usuario DROP DEFAULT;
       products          postgres    false    219    218    219            �
           2604    16559    venta id_venta    DEFAULT     t   ALTER TABLE ONLY products.venta ALTER COLUMN id_venta SET DEFAULT nextval('products.venta_id_venta_seq'::regclass);
 ?   ALTER TABLE products.venta ALTER COLUMN id_venta DROP DEFAULT;
       products          postgres    false    216    217    217            �
           2604    16467 
   rol id_rol    DEFAULT     f   ALTER TABLE ONLY users.rol ALTER COLUMN id_rol SET DEFAULT nextval('users.rol_id_rol_seq'::regclass);
 8   ALTER TABLE users.rol ALTER COLUMN id_rol DROP DEFAULT;
       users          postgres    false    208    209    209            �
           2604    16475    usuario id_usuario    DEFAULT     v   ALTER TABLE ONLY users.usuario ALTER COLUMN id_usuario SET DEFAULT nextval('users.usuario_id_usuario_seq'::regclass);
 @   ALTER TABLE users.usuario ALTER COLUMN id_usuario DROP DEFAULT;
       users          postgres    false    210    211    211            �          0    16487    carrito_compras 
   TABLE DATA           K   COPY products.carrito_compras (id_carrito_compras, id_usuario) FROM stdin;
    products          postgres    false    213   N�       �          0    16604    carrito_compras_producto 
   TABLE DATA           |   COPY products.carrito_compras_producto (id_carrito_compras_producto, id_carrito_compras, id_producto, cantidad) FROM stdin;
    products          postgres    false    221   o�       �          0    16532    llave 
   TABLE DATA           S   COPY products.llave (id_llave, llave, fecha_vencimiento, id_proveedor) FROM stdin;
    products          postgres    false    215   ��       �          0    16448    producto 
   TABLE DATA           �   COPY products.producto (id_producto, nombre, precio, url_img, detalle, cantidad, activo, id_proveedor, id_producto_proveedor) FROM stdin;
    products          postgres    false    207   D�       �          0    16440 	   proveedor 
   TABLE DATA           C   COPY products.proveedor (id_proveedor, nombre, activo) FROM stdin;
    products          postgres    false    205   ��       �          0    24645    sincronizacion 
   TABLE DATA           y   COPY products.sincronizacion (id_sincronizacion, id_proveedor, id_producto_proveedor, id_producto, cantidad) FROM stdin;
    products          postgres    false    223   �       �          0    16573    sugerencia_producto_usuario 
   TABLE DATA           �   COPY products.sugerencia_producto_usuario (id_sugerencia_producto_usuario, id_usuario, id_producto, cantidad_busqueda) FROM stdin;
    products          postgres    false    219   A�       �          0    16556    venta 
   TABLE DATA           D   COPY products.venta (id_venta, id_usuario, total_venta) FROM stdin;
    products          postgres    false    217   ��       �          0    16464    rol 
   TABLE DATA           ,   COPY users.rol (id_rol, nombre) FROM stdin;
    users          postgres    false    209   ��       �          0    16472    usuario 
   TABLE DATA           J   COPY users.usuario (id_usuario, correo, contrasennia, id_rol) FROM stdin;
    users          postgres    false    211   �       �           0    0 &   carrito_compras_id_carrito_compras_seq    SEQUENCE SET     V   SELECT pg_catalog.setval('products.carrito_compras_id_carrito_compras_seq', 2, true);
          products          postgres    false    212            �           0    0 8   carrito_compras_producto_id_carrito_compras_producto_seq    SEQUENCE SET     i   SELECT pg_catalog.setval('products.carrito_compras_producto_id_carrito_compras_producto_seq', 33, true);
          products          postgres    false    220            �           0    0    llave_id_llave_seq    SEQUENCE SET     B   SELECT pg_catalog.setval('products.llave_id_llave_seq', 8, true);
          products          postgres    false    214            �           0    0    producto_id_producto_seq    SEQUENCE SET     I   SELECT pg_catalog.setval('products.producto_id_producto_seq', 22, true);
          products          postgres    false    206            �           0    0    proveedor_id_proveedor_seq    SEQUENCE SET     J   SELECT pg_catalog.setval('products.proveedor_id_proveedor_seq', 6, true);
          products          postgres    false    204            �           0    0 $   sincronizacion_id_sincronizacion_seq    SEQUENCE SET     T   SELECT pg_catalog.setval('products.sincronizacion_id_sincronizacion_seq', 2, true);
          products          postgres    false    222            �           0    0 >   sugerencia_producto_usuario_id_sugerencia_producto_usuario_seq    SEQUENCE SET     o   SELECT pg_catalog.setval('products.sugerencia_producto_usuario_id_sugerencia_producto_usuario_seq', 17, true);
          products          postgres    false    218            �           0    0    venta_id_venta_seq    SEQUENCE SET     B   SELECT pg_catalog.setval('products.venta_id_venta_seq', 1, true);
          products          postgres    false    216            �           0    0    rol_id_rol_seq    SEQUENCE SET     ;   SELECT pg_catalog.setval('users.rol_id_rol_seq', 2, true);
          users          postgres    false    208            �           0    0    usuario_id_usuario_seq    SEQUENCE SET     C   SELECT pg_catalog.setval('users.usuario_id_usuario_seq', 9, true);
          users          postgres    false    210            �
           2606    16492 $   carrito_compras carrito_compras_pkey 
   CONSTRAINT     t   ALTER TABLE ONLY products.carrito_compras
    ADD CONSTRAINT carrito_compras_pkey PRIMARY KEY (id_carrito_compras);
 P   ALTER TABLE ONLY products.carrito_compras DROP CONSTRAINT carrito_compras_pkey;
       products            postgres    false    213            �
           2606    16609 6   carrito_compras_producto carrito_compras_producto_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY products.carrito_compras_producto
    ADD CONSTRAINT carrito_compras_producto_pkey PRIMARY KEY (id_carrito_compras_producto);
 b   ALTER TABLE ONLY products.carrito_compras_producto DROP CONSTRAINT carrito_compras_producto_pkey;
       products            postgres    false    221            �
           2606    16540    llave pk_llave 
   CONSTRAINT     T   ALTER TABLE ONLY products.llave
    ADD CONSTRAINT pk_llave PRIMARY KEY (id_llave);
 :   ALTER TABLE ONLY products.llave DROP CONSTRAINT pk_llave;
       products            postgres    false    215            �
           2606    16579 :   sugerencia_producto_usuario pk_sugerencia_producto_usuario 
   CONSTRAINT     �   ALTER TABLE ONLY products.sugerencia_producto_usuario
    ADD CONSTRAINT pk_sugerencia_producto_usuario PRIMARY KEY (id_sugerencia_producto_usuario);
 f   ALTER TABLE ONLY products.sugerencia_producto_usuario DROP CONSTRAINT pk_sugerencia_producto_usuario;
       products            postgres    false    219            �
           2606    16561    venta pk_venta 
   CONSTRAINT     T   ALTER TABLE ONLY products.venta
    ADD CONSTRAINT pk_venta PRIMARY KEY (id_venta);
 :   ALTER TABLE ONLY products.venta DROP CONSTRAINT pk_venta;
       products            postgres    false    217            �
           2606    16456    producto producto_primary_key 
   CONSTRAINT     f   ALTER TABLE ONLY products.producto
    ADD CONSTRAINT producto_primary_key PRIMARY KEY (id_producto);
 I   ALTER TABLE ONLY products.producto DROP CONSTRAINT producto_primary_key;
       products            postgres    false    207            �
           2606    16445    proveedor proveedor_pkey 
   CONSTRAINT     b   ALTER TABLE ONLY products.proveedor
    ADD CONSTRAINT proveedor_pkey PRIMARY KEY (id_proveedor);
 D   ALTER TABLE ONLY products.proveedor DROP CONSTRAINT proveedor_pkey;
       products            postgres    false    205            �
           2606    24650 )   sincronizacion sincronizacion_primary_key 
   CONSTRAINT     x   ALTER TABLE ONLY products.sincronizacion
    ADD CONSTRAINT sincronizacion_primary_key PRIMARY KEY (id_sincronizacion);
 U   ALTER TABLE ONLY products.sincronizacion DROP CONSTRAINT sincronizacion_primary_key;
       products            postgres    false    223            �
           2606    16469    rol rol_pkey 
   CONSTRAINT     M   ALTER TABLE ONLY users.rol
    ADD CONSTRAINT rol_pkey PRIMARY KEY (id_rol);
 5   ALTER TABLE ONLY users.rol DROP CONSTRAINT rol_pkey;
       users            postgres    false    209            �
           2606    16479    usuario usuario_correo_key 
   CONSTRAINT     V   ALTER TABLE ONLY users.usuario
    ADD CONSTRAINT usuario_correo_key UNIQUE (correo);
 C   ALTER TABLE ONLY users.usuario DROP CONSTRAINT usuario_correo_key;
       users            postgres    false    211            �
           2606    16477    usuario usuario_pkey 
   CONSTRAINT     Y   ALTER TABLE ONLY users.usuario
    ADD CONSTRAINT usuario_pkey PRIMARY KEY (id_usuario);
 =   ALTER TABLE ONLY users.usuario DROP CONSTRAINT usuario_pkey;
       users            postgres    false    211            	           2606    24656    sincronizacion fk_producto    FK CONSTRAINT     �   ALTER TABLE ONLY products.sincronizacion
    ADD CONSTRAINT fk_producto FOREIGN KEY (id_producto) REFERENCES products.producto(id_producto);
 F   ALTER TABLE ONLY products.sincronizacion DROP CONSTRAINT fk_producto;
       products          postgres    false    207    2796    223            �
           2606    16457    producto fk_producto_proveedor    FK CONSTRAINT     �   ALTER TABLE ONLY products.producto
    ADD CONSTRAINT fk_producto_proveedor FOREIGN KEY (id_proveedor) REFERENCES products.proveedor(id_proveedor);
 J   ALTER TABLE ONLY products.producto DROP CONSTRAINT fk_producto_proveedor;
       products          postgres    false    2794    205    207                       2606    16585 2   sugerencia_producto_usuario fk_producto_sugerencia    FK CONSTRAINT     �   ALTER TABLE ONLY products.sugerencia_producto_usuario
    ADD CONSTRAINT fk_producto_sugerencia FOREIGN KEY (id_producto) REFERENCES products.producto(id_producto);
 ^   ALTER TABLE ONLY products.sugerencia_producto_usuario DROP CONSTRAINT fk_producto_sugerencia;
       products          postgres    false    2796    207    219                       2606    16615 ,   carrito_compras_producto fk_products_carrito    FK CONSTRAINT     �   ALTER TABLE ONLY products.carrito_compras_producto
    ADD CONSTRAINT fk_products_carrito FOREIGN KEY (id_carrito_compras) REFERENCES products.carrito_compras(id_carrito_compras);
 X   ALTER TABLE ONLY products.carrito_compras_producto DROP CONSTRAINT fk_products_carrito;
       products          postgres    false    221    213    2804                       2606    16610 -   carrito_compras_producto fk_products_producto    FK CONSTRAINT     �   ALTER TABLE ONLY products.carrito_compras_producto
    ADD CONSTRAINT fk_products_producto FOREIGN KEY (id_producto) REFERENCES products.producto(id_producto);
 Y   ALTER TABLE ONLY products.carrito_compras_producto DROP CONSTRAINT fk_products_producto;
       products          postgres    false    207    221    2796                       2606    24651    sincronizacion fk_proveedor    FK CONSTRAINT     �   ALTER TABLE ONLY products.sincronizacion
    ADD CONSTRAINT fk_proveedor FOREIGN KEY (id_proveedor) REFERENCES products.proveedor(id_proveedor);
 G   ALTER TABLE ONLY products.sincronizacion DROP CONSTRAINT fk_proveedor;
       products          postgres    false    2794    223    205                       2606    16541    llave fk_proveedor_llave    FK CONSTRAINT     �   ALTER TABLE ONLY products.llave
    ADD CONSTRAINT fk_proveedor_llave FOREIGN KEY (id_proveedor) REFERENCES products.proveedor(id_proveedor);
 D   ALTER TABLE ONLY products.llave DROP CONSTRAINT fk_proveedor_llave;
       products          postgres    false    2794    215    205                       2606    16493     carrito_compras fk_users_usuario    FK CONSTRAINT     �   ALTER TABLE ONLY products.carrito_compras
    ADD CONSTRAINT fk_users_usuario FOREIGN KEY (id_usuario) REFERENCES users.usuario(id_usuario);
 L   ALTER TABLE ONLY products.carrito_compras DROP CONSTRAINT fk_users_usuario;
       products          postgres    false    213    211    2802                       2606    16580 1   sugerencia_producto_usuario fk_usuario_sugerencia    FK CONSTRAINT     �   ALTER TABLE ONLY products.sugerencia_producto_usuario
    ADD CONSTRAINT fk_usuario_sugerencia FOREIGN KEY (id_usuario) REFERENCES users.usuario(id_usuario);
 ]   ALTER TABLE ONLY products.sugerencia_producto_usuario DROP CONSTRAINT fk_usuario_sugerencia;
       products          postgres    false    2802    219    211                       2606    16562    venta fk_usuario_venta    FK CONSTRAINT     �   ALTER TABLE ONLY products.venta
    ADD CONSTRAINT fk_usuario_venta FOREIGN KEY (id_usuario) REFERENCES users.usuario(id_usuario);
 B   ALTER TABLE ONLY products.venta DROP CONSTRAINT fk_usuario_venta;
       products          postgres    false    2802    217    211                        2606    16480    usuario fk_rol    FK CONSTRAINT     l   ALTER TABLE ONLY users.usuario
    ADD CONSTRAINT fk_rol FOREIGN KEY (id_rol) REFERENCES users.rol(id_rol);
 7   ALTER TABLE ONLY users.usuario DROP CONSTRAINT fk_rol;
       users          postgres    false    211    209    2798            �      x�3�4����� ]      �   '   x�3�4B.C m�i�e��8�@| �+F��� bN�      �   �   x���;�0�Y>E.`CZ�|�.�e+���A�~��H�*�t83��r�\��&�ihp���#��f�j��8��yC�k3X�~!Puc���/�S��"O��)��ň�4�$s��>��7ɹ�H�8wN!XS��Э�R^�CB�      �   |  x��Q��6���S� �ȡD�*P�w���w��v�
c����EU���>�&},�[�&=I)�)P`ѷ�u����#�;lqp=��,�D(�x!���od=m���m3TX���>V�2�n.;����j�z^��G@�� �7� 	T�d������I��,�X�Hdb|��Y,�$")��۴�����vD�g�������'���kF����;��իv������E>-3�!��̪]��O4�K��oI�|��յ�Ι�춭�6��i����i0�H{��5r�^�+�����Y�-7	$�D-�GY�
+WF�X'I����o��X���|�q��ړ�|���ј,��Y�]��zDY��3�K�Ja��ʠjr�eV�$����������U��vE�{T�n��{�����B*	����^�^^�˙�������ڢ�?>?a�\zz~��Q͛7<�2��킗��<�i��1I�}��i[���|#*埡�P�M���dw�~ύ�5v%��̽P�^0��݀$ڇ��"��ۦd���zM �T'��+�e|��?I>����z��|�ÜA(�5W4%o��ln{���cUWؑ$et?Zsp�5��ϑ6����g��#�A�p0���NWq큵�D;��)0.X��hW�C�_�����};˯��|���d����(kB32�~�e��ݍr=Z�@z2��q��B�,
|��8J�y�k=y�d��*�,\æ��^����z�ϑ$^ȩ�3��@����0e��7� N�������������L�(�?�&��}	������������l1
a�/�S���7G����<����uB᜖'����&�|�BF�yoP)�9Y8�C'���R�:+�=      �   9   x�3�K�+	�,�2�t,M�L��I�K�L
s:�&e�$�L9��`�4�=... z9�      �      x�3�4�4�42�4����� �       �   T   x����P�PLf�}�K��#��E�1*�*x�'���\�����$��Uh�qU�j����:��t��u�Ǜ�{l��F�      �       x�3�4�44�г40�10PxԴ�+F��� =�I      �   '   x�3�t��L�+I�2�tL����,.)JL�/����� �	G      �   y   x�u�=
�0@�Y:����&[�so�E-�V��Bo�B�&��ǳp��p��*���h����������Tb4ko���.�Gx���y�_m�$�rf��ms8r8@�TNp�̥���� �R�>     