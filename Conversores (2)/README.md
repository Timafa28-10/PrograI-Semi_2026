# Conversores

Aplicación de escritorio (Windows Forms, C#, .NET Framework 4.7.2) que
convierte entre unidades de **Moneda, Masa, Volumen, Longitud,
Almacenamiento y Tiempo**, con 10 unidades por categoría, convirtiendo
cualquier valor a cualquier valor.

Estructura basada en la plantilla de la cátedra (proyecto clásico con
`Form1.cs` / `Form1.Designer.cs` separados, igual que en `miprimeraApp`).

## Contenido

```
Conversores.sln
Conversores/
├── Conversores.csproj
├── Categoria.cs             # arrays de unidades/factores + función Convertir()
├── Form1.cs                 # eventos (botones, combos)
├── Form1.Designer.cs        # controles generados por el diseñador
├── Form1.resx
├── Program.cs                # punto de entrada (Main)
├── App.config
└── Properties/
    ├── AssemblyInfo.cs
    ├── Resources.resx / Resources.Designer.cs
    └── Settings.settings / Settings.Designer.cs
```

## Cómo ejecutarlo en Visual Studio 2026 (Windows)

1. Descomprime la carpeta `Conversores` en tu PC.
2. Doble clic en `Conversores.sln`.
3. Presiona **F5** para compilar y ejecutar. Debería abrirse la ventana
   "Conversor Universal".

Requiere la carga de trabajo **".NET desktop development"** en Visual
Studio (te la ofrece instalar si no la tienes).

## Cómo funciona (arrays, funciones y bucle)

- `DatosConversores.Construir()` (en `Categoria.cs`) arma un **array** de
  6 objetos `Categoria` con un **bucle `for`**.
- Cada `Categoria` guarda dos **arrays paralelos**: `Unidades` y `Factores`.
- La **función** `Categoria.Convertir(valor, desde, hacia)` pasa el valor
  por una unidad base, permitiendo convertir entre **cualquier par de
  unidades** de la misma categoría.
- En `Form1.cs`, `CargarCategorias()` y `CargarUnidades()` recorren esos
  arrays con **bucles `for`** para llenar los `ComboBox` dinámicamente.

## Publicar en GitHub

```bash
git init
git add .
git commit -m "Conversores: moneda, masa, volumen, longitud, almacenamiento y tiempo"
git branch -M main
git remote add origin https://github.com/TU_USUARIO/Conversores.git
git push -u origin main
```
