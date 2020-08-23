import { PrecioModel } from "./precio.model";
import { ProveedorModel } from './proveedor.model';
export class ProductModel{

    idProducto: number;
    cantidad: number;
    nombre: string;
    urlImg: string;
    detalle: string;
    precio: PrecioModel;
    proveedor: ProveedorModel;
    
}