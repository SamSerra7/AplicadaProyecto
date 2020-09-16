create database ClienteUnoVentR
use ClienteUnoVentR



create table producto(
id_producto int identity(1,1)primary key,
nombre varchar(20),
precio float,
url_img varchar(300),
cantidad int,
estado bit,
descripcion varchar(max),
id_proveedor int
)



create procedure SelectProduct
as
select * from producto;


create procedure InsertProduct(
@nombre varchar(20),
@precio float,
@url_img varchar(300),
@cantidad int,
@estado bit,
@descripcion varchar(500),
@id_proveedor int )
as
INSERT INTO [dbo].[producto]([nombre],[precio],[url_img],[cantidad],[estado],[descripcion],[id_proveedor])VALUES(@nombre,@precio,@url_img,@cantidad,@estado,@descripcion,@id_proveedor)


create procedure ActualizarProducto(
@id_producto int,
@nombre varchar(20),
@precio float,
@url_img varchar(300),
@cantidad int,
@estado bit,
@descripcion varchar(500))
as
UPDATE [dbo].[producto]
   SET [nombre] = @nombre,[precio] = @precio,[url_img] = @url_img,[cantidad] = @cantidad,[estado] = @estado,[descripcion] = @descripcion
 WHERE id_producto = @id_producto
GO


create procedure EliminarProducto(
@id_producto int)
as
DELETE FROM [dbo].[producto] WHERE id_producto = @id_producto



create procedure SelectProductoID(
@id int
)
as
select * from producto where id_producto=@id;

create procedure ActualizarCantidad(
@id_producto int,
@id_proveedor int,
@cantidad int)
as
UPDATE [dbo].[producto]
   SET [cantidad] = [cantidad]-@cantidad WHERE id_producto = @id_producto and id_proveedor = @id_proveedor
GO
