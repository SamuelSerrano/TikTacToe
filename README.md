# Tic Tac Toe

Aplicacion offline para Android creada en C# con .NET MAUI. El juego permite jugar el clasico tres en raya en dos modos: dos personas en el mismo telefono o una persona contra la IA.

## Como quedo el proyecto

- `src/TikTacToe.App`: app Android con .NET MAUI. Contiene la pantalla principal, selector de modo, tablero 3x3, estado de la partida y boton de reinicio.
- `src/TikTacToe.Core`: libreria C# con las reglas del juego, turnos, deteccion de ganador, empate y la IA.
- `tests/TikTacToe.Tests`: pruebas simples de consola para validar reglas basicas e IA.
- `TikTacToe.sln`: solucion que agrupa los proyectos.

## Funciones principales

- Modo `2 personas`: dos jugadores reales juegan por turnos en el mismo dispositivo.
- Modo `Contra IA`: el jugador usa la carita feliz y la IA usa la carita con corazones.
- Simbolos del tablero:
  - Carita feliz: `🙂`
  - Carita con corazones: `😍`
- La app no requiere internet para jugar.
- El tablero bloquea casillas ocupadas y detecta victoria o empate.
- La IA usa minimax para elegir buenas jugadas.

## Requisitos

- .NET SDK 8.
- Workload de Android para .NET MAUI.
- Android SDK configurado en la maquina para compilar o publicar el APK.

Si falta MAUI Android, instalarlo con:

```powershell
dotnet workload install maui-android
```

## Comandos utiles

Ejecutar las pruebas de reglas e IA:

```powershell
dotnet run --project tests\TikTacToe.Tests\TikTacToe.Tests.csproj
```

Compilar la libreria principal:

```powershell
dotnet build src\TikTacToe.Core\TikTacToe.Core.csproj
```

Compilar la app Android:

```powershell
dotnet build src\TikTacToe.App\TikTacToe.App.csproj
```

Generar APK en modo Release:

```powershell
dotnet publish src\TikTacToe.App\TikTacToe.App.csproj -c Release -f net8.0-android
```
