import { ProductModel } from './products.model';
import { ProveedorModel } from './proveedor.model';
import { PrecioModel } from './precio.model';

export class ShopCartModel{

    id_producto:number;
    id_usuario:number;
    cantidad:number=1;
    nombre: string="";
    urlImg: string="";
    detalle: string="";
    precio: number=0;

    
    
}