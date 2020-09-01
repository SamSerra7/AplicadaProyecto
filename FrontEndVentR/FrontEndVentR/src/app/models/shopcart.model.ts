import { ProductModel } from './products.model';
import { ProveedorModel } from './proveedor.model';
import { PrecioModel } from './precio.model';

export class ShopCartModel{

    idProducto:number;
    id_usuario:number;
    cantidad:number=1;
}