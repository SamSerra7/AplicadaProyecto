import { PrecioModel } from "./precio.model";
import { ProveedorModel } from './proveedor.model';
export class ProductModel{

    activo:boolean;
    cantidad: number;
    detalle: string;
    idProducto: number;
    idProductoProveedor: number;    
    nombre: string;
    precio: PrecioModel=new PrecioModel();
    proveedor: ProveedorModel=new ProveedorModel();
    urlImg: string;   
    
    
    
}