using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composite_Compu
{
    public abstract class Nodo
    {
        string _nombre;

        public Nodo(string nombre)
        {
            _nombre = nombre;
        }

        public string Nombre => _nombre;

        public abstract void Agregar(Nodo n);
        public abstract IList<Nodo> Obtener();
        public abstract int Precio { get; }
    }

    public class Categoria : Nodo
    {
        private List<Nodo> _hijos = new List<Nodo>();

        public Categoria(string nombre) : base(nombre) { }

        public override void Agregar(Nodo n)
        {
            _hijos.Add(n);
        }

        public override IList<Nodo> Obtener()
        {
            return _hijos.ToArray();
        }

        public override int Precio
        {
            get
            {
                int total = 0;
                foreach (var item in _hijos)
                {
                    total += item.Precio;
                }
                return total;
            }
        }
    }

    public class Componente : Nodo
    {
        int _precio;

        public Componente(string nombre, int precio) : base(nombre)
        {
            _precio = precio;
        }

        public override void Agregar(Nodo n) { }

        public override IList<Nodo> Obtener()
        {
            return null;
        }

        public override int Precio => _precio;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Nodo economico = CrearPC("Económico", 800, 1200, 900, 1500, 200);
            Nodo estandar = CrearPC("Estándar", 1200, 2000, 1500, 2500, 400);
            Nodo lujo = CrearPC("De Lujo", 2500, 4000, 3000, 5000, 900);

            Console.WriteLine("Selecciona tu paquete:");
            Console.WriteLine("1. Económico");
            Console.WriteLine("2. Estándar");
            Console.WriteLine("3. De Lujo");

            int opcion = int.Parse(Console.ReadLine());

            Nodo seleccionado = null;

            switch (opcion)
            {
                case 1: seleccionado = economico; break;
                case 2: seleccionado = estandar; break;
                case 3: seleccionado = lujo; break;
                default:
                    Console.WriteLine("Opción inválida");
                    return;
            }

            List<Nodo> elegidos = new List<Nodo>();
            int total = 0;

            Console.WriteLine($"\nConfigurando PC: {seleccionado.Nombre}\n");

            foreach (var categoria in seleccionado.Obtener())
            {
                Console.WriteLine($"Elige {categoria.Nombre}:");

                var opciones = categoria.Obtener().ToList();

                for (int i = 0; i < opciones.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {opciones[i].Nombre} - ${opciones[i].Precio}");
                }

                int eleccion = int.Parse(Console.ReadLine());
                var elegido = opciones[eleccion - 1];

                elegidos.Add(elegido);
                total += elegido.Precio;

                Console.WriteLine($"Seleccionaste: {elegido.Nombre}\n");
            }

            Console.WriteLine("\n===== RESUMEN =====\n");

            int indice = 0;
            foreach (var categoria in seleccionado.Obtener())
            {
                Console.WriteLine($"{categoria.Nombre}: {elegidos[indice].Nombre} - ${elegidos[indice].Precio}");
                indice++;
            }

            Console.WriteLine($"\nTOTAL A PAGAR: ${total}");
            Console.ReadLine();
        }

        static Nodo CrearPC(string nombre, int precioRam, int precioCPU, int precioAlmacen, int precioMonitor, int precioMouse)
        {
            Nodo pc = new Categoria(nombre);

            // RAM
            Nodo ram = new Categoria("RAM");
            ram.Agregar(new Componente("Kingston 8GB", precioRam));
            ram.Agregar(new Componente("Corsair 16GB", precioRam + 400));
            ram.Agregar(new Componente("G.Skill 32GB", precioRam + 800));

            // Procesador
            Nodo cpu = new Categoria("Procesador");
            cpu.Agregar(new Componente("Intel i3", precioCPU));
            cpu.Agregar(new Componente("AMD Ryzen 5", precioCPU + 600));
            cpu.Agregar(new Componente("Intel i7", precioCPU + 1200));

            // Almacenamiento
            Nodo almacenamiento = new Categoria("Almacenamiento");
            almacenamiento.Agregar(new Componente("WD 1TB HDD", precioAlmacen));
            almacenamiento.Agregar(new Componente("Kingston SSD 480GB", precioAlmacen + 500));
            almacenamiento.Agregar(new Componente("Samsung SSD 1TB", precioAlmacen + 1000));

            // Monitor
            Nodo monitor = new Categoria("Monitor");
            monitor.Agregar(new Componente("Acer 21\"", precioMonitor));
            monitor.Agregar(new Componente("LG 24\" Full HD", precioMonitor + 800));
            monitor.Agregar(new Componente("Samsung 27\" Curvo", precioMonitor + 2000));

            // Mouse
            Nodo mouse = new Categoria("Mouse");
            mouse.Agregar(new Componente("Genius Básico", precioMouse));
            mouse.Agregar(new Componente("Logitech Óptico", precioMouse + 200));
            mouse.Agregar(new Componente("Razer Gamer", precioMouse + 600));

            pc.Agregar(ram);
            pc.Agregar(cpu);
            pc.Agregar(almacenamiento);
            pc.Agregar(monitor);
            pc.Agregar(mouse);

            return pc;
        }
    }
}
