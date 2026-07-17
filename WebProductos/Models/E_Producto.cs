namespace WebProductos.Models
{
    public class E_Producto
    {
        //propiedades simples
        public int idProducto { get; set; }
        public string  Descripcion { get; set; }
        public decimal Precio { get; set; }
        public DateTime FechaIngreso { get; set; }
        public bool Disponible { get; set; }

        //propiedades full o de solo lectura
        public string DisponibleTexto 
        { 
            get 
            {
                if (Disponible == true)
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            } 
        }


        public DateTime FechaCaducidad
        {
            get
            {
                return FechaIngreso.AddMonths(2);
            }
        }

    }
}
