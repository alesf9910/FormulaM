Aqu� tienes el contenido completo del archivo `README.md` listo para descargar, sin la secci�n de gram�tica:

---

```markdown
# Formula Calculator / Calculadora de F�rmulas

Este repositorio contiene una biblioteca en C# que permite evaluar expresiones matem�ticas basadas en una gram�tica definida. La biblioteca soporta operaciones aritm�ticas, funciones integradas, asignaci�n de variables y la definici�n de funciones personalizadas. Adem�s, cuenta con un robusto manejo de errores mediante la excepci�n `CalculatorException`.

---

## English

### Introduction

**Formula Calculator** is a C# library for evaluating mathematical expressions defined by a custom grammar. It supports arithmetic operations, built-in functions, variable assignments, and custom function definitions. Error handling is implemented using the custom exception class `CalculatorException`.

### Features

- **Arithmetic Operations:**  
  Supports addition (`+`), subtraction (`-`), multiplication (`*`), division (both `/` and `//`), remainder (`%`), and exponentiation (`^`).

- **Built-in Functions:**  
  Includes functions such as:
  - `abs`, `acos`, `asin`, `atan`, `atan2`
  - `ceiling`, `cos`, `cosh`, `exp`, `floor`
  - `log`, `log10`, `max`, `min`, `pow`
  - `round`, `sign`, `sin`, `sinh`
  - `sqrt`, `tan`, `tanh`
  - `ieeeremainder`, and `truncate`

- **Variables and Constants:**  
  Variables are assigned using the syntax `ID = formula`. Constants like `pi`, `e`, and `tau` are predefined and cannot be changed.

- **Custom Functions:**  
  You can add your own functions by inserting entries into the static `functions` dictionary of the `Calculator` class.

- **Error Handling:**  
  Errors in syntax, an incorrect number of parameters for a function, or attempts to modify constants will throw a `CalculatorException` with a clear message.

### Usage and Examples

#### Evaluating Basic Expressions

Create an instance of `Calculator` and call the `Calculate()` method:

```csharp
using System;
using Formula00;

public class Program 
{
    public static void Main()
    {
        try 
        {
            // Basic arithmetic evaluation:
            Calculator calc = new Calculator("2+3");
            double result = calc.Calculate();
            Console.WriteLine("2+3 = " + result);

            // Variable assignment:
            // Write the assignments inside parentheses, then the expression outside.
            Calculator calc2 = new Calculator("(x=5) 2*x+3");
            double result2 = calc2.Calculate();
            Console.WriteLine("(x=5) 2*x+3 = " + result2);
        }
        catch (CalculatorException e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
    }
}
```

#### Defining Custom Functions

You can add custom functions by adding entries to the `Calculator.functions` dictionary. For example, to define a custom function that adds two numbers:

```csharp
using System;
using Formula00;

public class CustomFunctions
{
    public static void Register()
    {
        // Define a custom function "customAdd" that returns the sum of two parameters.
        Calculator.functions.Add("customAdd", parameters =>
        {
            // Ensure exactly 2 parameters are provided.
            Calculator.ExpectParameters("customAdd", parameters, 2);
            return parameters[0] + parameters[1];
        });
    }
}

public class Program 
{
    public static void Main()
    {
        try 
        {
            // Register the custom function before using it.
            CustomFunctions.Register();

            // Use the custom function in an expression:
            Calculator calc = new Calculator("customAdd(10,20)");
            double result = calc.Calculate();
            Console.WriteLine("customAdd(10,20) = " + result);
        }
        catch (CalculatorException e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
    }
}
```

#### Error Handling

When an error occurs�such as a syntax error, a wrong number of parameters in a function, or attempting to modify constants�the library throws a `CalculatorException`. For example:

```csharp
try
{
    // Attempting to modify a constant will throw an exception.
    Calculator calc = new Calculator("pi=3.14");
    calc.Calculate();
}
catch (CalculatorException e)
{
    Console.WriteLine("Error: " + e.Message);
}
```

### Installation

1. **Clone the Repository:**

   ```bash
   git clone https://github.com/your-username/your-repo.git
   ```

2. **Open the Project:**  
   Import the project into your preferred IDE (e.g., Visual Studio, VS Code). Ensure you have .NET 6.0 or higher installed.

3. **Build and Run:**  
   Compile the project and run the provided examples or integrate the library into your project.

### Contributing

Contributions are welcome! If you encounter any issues or want to enhance functionality, please open an issue or submit a pull request.

### License

This project is distributed under the MIT License. See the `LICENSE` file for details.

### Contact

For questions, suggestions, or contributions, please open an issue on GitHub or contact us directly.

---

## Espa�ol

### Introducci�n

**Calculadora de F�rmulas** es una librer�a en C# dise�ada para evaluar expresiones matem�ticas basadas en una gram�tica definida. Soporta operaciones aritm�ticas, funciones integradas, asignaci�n de variables y la definici�n de funciones personalizadas. El manejo de errores se implementa a trav�s de la excepci�n personalizada `CalculatorException`.

### Caracter�sticas

- **Operaciones Aritm�ticas:**  
  Soporta suma (`+`), resta (`-`), multiplicaci�n (`*`), divisi�n (tanto `/` como `//`), resto (`%`) y potenciaci�n (`^`).

- **Funciones Integradas:**  
  Incluye funciones como:
  - `abs`, `acos`, `asin`, `atan`, `atan2`
  - `ceiling`, `cos`, `cosh`, `exp`, `floor`
  - `log`, `log10`, `max`, `min`, `pow`
  - `round`, `sign`, `sin`, `sinh`
  - `sqrt`, `tan`, `tanh`
  - `ieeeremainder`, `truncate`

- **Variables y Constantes:**  
  Las variables se asignan con la sintaxis `ID = f�rmula`. Las constantes `pi`, `e` y `tau` se inicializan por defecto y no se pueden modificar.

- **Funciones Personalizadas:**  
  Puedes agregar funciones propias insertando entradas en el diccionario est�tico `functions` de la clase `Calculator`.

- **Manejo de Errores:**  
  Los errores de sintaxis, el n�mero incorrecto de par�metros o el intento de modificar constantes disparan una `CalculatorException` con un mensaje claro.

### Uso y Ejemplos

#### Evaluaci�n de Expresiones B�sicas

Crea una instancia de `Calculator` y utiliza el m�todo `Calculate()` para evaluar la expresi�n:

```csharp
using System;
using Formula00;

public class Program 
{
    public static void Main()
    {
        try 
        {
            // Evaluaci�n aritm�tica simple:
            Calculator calc = new Calculator("2+3");
            double resultado = calc.Calculate();
            Console.WriteLine("2+3 = " + resultado);

            // Asignaci�n de variable:
            // Escribe la asignaci�n entre par�ntesis y a continuaci�n la expresi�n a evaluar.
            Calculator calc2 = new Calculator("(x=5) 2*x+3");
            double resultado2 = calc2.Calculate();
            Console.WriteLine("(x=5) 2*x+3 = " + resultado2);
        }
        catch (CalculatorException e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
    }
}
```

#### Definiendo Funciones Personalizadas

Puedes agregar tus propias funciones a�adi�ndolas al diccionario `functions`. Por ejemplo, para definir una funci�n que sume dos n�meros:

```csharp
using System;
using Formula00;

public class FuncionesPersonalizadas
{
    public static void Registrar()
    {
        // Define la funci�n "sumaPersonal" que devuelve la suma de dos par�metros.
        Calculator.functions.Add("sumaPersonal", parameters =>
        {
            // Aseg�rate de que se reciben exactamente 2 par�metros.
            Calculator.ExpectParameters("sumaPersonal", parameters, 2);
            return parameters[0] + parameters[1];
        });
    }
}

public class Program 
{
    public static void Main()
    {
        try 
        {
            // Registra la funci�n personalizada antes de usarla.
            FuncionesPersonalizadas.Registrar();

            // Ahora, eval�a una expresi�n usando la funci�n personalizada:
            Calculator calc = new Calculator("sumaPersonal(10,20)");
            double resultado = calc.Calculate();
            Console.WriteLine("sumaPersonal(10,20) = " + resultado);
        }
        catch (CalculatorException e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
    }
}
```

#### Manejo de Errores

Cuando ocurre un error�por ejemplo, una expresi�n con sintaxis incorrecta, una llamada a una funci�n con un n�mero inadecuado de par�metros o el intento de modificar constantes�se lanza un `CalculatorException`. Ejemplo:

```csharp
try
{
    // Intentar modificar una constante produce un error:
    Calculator calc = new Calculator("pi=3.14");
    calc.Calculate();
}
catch (CalculatorException e)
{
    Console.WriteLine("Error: " + e.Message);
}
```

### Instalaci�n

1. **Clona el repositorio:**

   ```bash
   git clone https://github.com/tu-usuario/tu-repo.git
   ```

2. **Abre el proyecto:**  
   Importa el proyecto en tu IDE favorito (Visual Studio, VS Code, etc.) y aseg�rate de tener instalada la versi�n adecuada de .NET (por ejemplo, .NET 6.0).

3. **Compila y Ejecuta:**  
   Compila el proyecto y ejecuta el c�digo de ejemplo o integra la librer�a en tu propio proyecto.

### Contribuciones

�Las contribuciones son bienvenidas! Si encuentras alg�n problema o deseas mejorar la funcionalidad, por favor abre un *issue* o env�a un *pull request*.

### Licencia

Este proyecto se distribuye bajo la Licencia MIT. Revisa el archivo `LICENSE` para m�s detalles.

### Contacto

Para dudas, sugerencias o comentarios, abre un *issue* en GitHub o cont�ctanos directamente.

---

Feel free to explore, use, and contribute to make this formula evaluation library even more powerful!
```