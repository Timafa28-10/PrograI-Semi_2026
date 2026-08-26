using System;

namespace Conversores {
    /// <summary>
    /// Representa una categoria de conversion (Moneda, Masa, Volumen, etc.)
    /// Guarda un array de nombres de unidades y un array paralelo de factores
    /// que indican cuantas "unidades base" equivale cada unidad.
    /// </summary>
    public class Categoria {
        public string Nombre { get; }
        public string[] Unidades { get; }
        public double[] Factores { get; }

        public Categoria(string nombre, string[] unidades, double[] factores) {
            if (unidades.Length != factores.Length)
                throw new ArgumentException("Unidades y Factores deben tener la misma longitud.");

            Nombre = nombre;
            Unidades = unidades;
            Factores = factores;
        }

        /// <summary>
        /// Convierte un valor desde una unidad de origen hacia una unidad destino,
        /// usando el arreglo de factores como puente (todo pasa por la unidad base).
        /// </summary>
        public double Convertir(double valor, int indiceDesde, int indiceHacia) {
            double valorEnBase = valor * Factores[indiceDesde];
            double resultado = valorEnBase / Factores[indiceHacia];
            return resultado;
        }
    }

    /// <summary>
    /// Fabrica estatica que construye, mediante un bucle, las 6 categorias
    /// solicitadas, cada una con 10 unidades.
    /// </summary>
    public static class DatosConversores {
        public static Categoria[] Construir() {
            var definiciones = new (string nombre, string[] unidades, double[] factores)[] {
                (
                    "Moneda",
                    new[] { "Dolar (USD)", "Euro (EUR)", "Libra (GBP)", "Yen (JPY)", "Peso Mexicano (MXN)",
                            "Quetzal (GTQ)", "Lempira (HNL)", "Colon (CRC)", "Balboa (PAB)", "Peso Colombiano (COP)" },
                    new[]  { 1.0, 0.92, 0.79, 149.50, 18.20, 7.75, 24.70, 520.0, 1.0, 4050.0 }
                ),
                (
                    "Masa",
                    new[] { "Miligramo (mg)", "Gramo (g)", "Kilogramo (kg)", "Tonelada (t)", "Onza (oz)",
                            "Libra (lb)", "Stone (st)", "Quintal (q)", "Arroba (@)", "Grano (gr)" },
                    new[]  { 0.000001, 0.001, 1.0, 1000.0, 0.0283495, 0.453592, 6.35029, 46.0, 11.5028, 0.0000648 }
                ),
                (
                    "Volumen",
                    new[] { "Mililitro (ml)", "Litro (l)", "Metro cubico (m3)", "Galon US", "Galon UK",
                            "Cuarto (qt)", "Pinta (pt)", "Taza (cup)", "Onza liquida (fl oz)", "Barril" },
                    new[]  { 0.001, 1.0, 1000.0, 3.78541, 4.54609, 0.946353, 0.473176, 0.24, 0.0295735, 158.987 }
                ),
                (
                    "Longitud",
                    new[] { "Milimetro (mm)", "Centimetro (cm)", "Metro (m)", "Kilometro (km)", "Pulgada (in)",
                            "Pie (ft)", "Yarda (yd)", "Milla (mi)", "Milla nautica (nmi)", "Vara" },
                    new[]  { 0.001, 0.01, 1.0, 1000.0, 0.0254, 0.3048, 0.9144, 1609.34, 1852.0, 0.835905 }
                ),
                (
                    "Almacenamiento",
                    new[] { "Bit", "Byte (B)", "Kilobyte (KB)", "Megabyte (MB)", "Gigabyte (GB)",
                            "Terabyte (TB)", "Petabyte (PB)", "Kibibyte (KiB)", "Mebibyte (MiB)", "Gibibyte (GiB)" },
                    new[]  { 0.125, 1.0, 1000.0, 1000000.0, 1000000000.0, 1e12, 1e15, 1024.0, 1048576.0, 1073741824.0 }
                ),
                (
                    "Tiempo",
                    new[] { "Milisegundo (ms)", "Segundo (s)", "Minuto (min)", "Hora (h)", "Dia",
                            "Semana", "Mes", "Anio", "Decada", "Siglo" },
                    new[]  { 0.001, 1.0, 60.0, 3600.0, 86400.0, 604800.0, 2629800.0, 31557600.0, 315576000.0, 3155760000.0 }
                ),
            };

            // Bucle que construye el array final de objetos Categoria.
            var categorias = new Categoria[definiciones.Length];
            for (int i = 0; i < definiciones.Length; i++) {
                var def = definiciones[i];
                categorias[i] = new Categoria(def.nombre, def.unidades, def.factores);
            }

            return categorias;
        }
    }
}
