# TallerVeu

Projecte d'exemple de la **Unitat 3** del mòdul DAM_0488 - Desenvolupament d'interfícies (IOC). Tots els codis i captures d'aquesta unitat dels materials provenen d'aquest projecte.

Control per veu d'un taller mecànic: reconeixement i síntesi de veu amb `System.Speech`, paquet de distribució amb instal·lador Inno Setup, projecte de proves xUnit i pla de proves.

Materials de la unitat: https://itrascastro.github.io/ioc/dam_0488/unitat-3/

## Requisits

- Windows 10/11.
- Visual Studio Community 2022 o superior amb la càrrega de treball **.NET desktop development**.
- .NET SDK 8 (`net8.0-windows`).

## Com obrir-lo

1. Cloneu el repositori o descarregueu-lo com a ZIP (Code → Download ZIP).
2. Obriu `TallerVeu.slnx` amb Visual Studio.
3. Establiu `TallerVeu` com a projecte d'inici i executeu-lo (F5). Els paquets NuGet es restauren automàticament.
4. Les proves s'executen des de l'Explorador de proves (Test → Test Explorer) o amb `dotnet test`.

## Contingut
- `TallerVeu/`: aplicació Windows Forms.
- `TallerVeu.Tests/`: proves unitàries (xUnit).
- `TallerVeu.Tests/`: projecte xUnit amb 6 proves automatitzades.
- `distribucio/`: script d'Inno Setup (`TallerVeu.iss`) i README d'instal·lació. L'instal·lador generat (`distribucio/Output/`) no s'inclou al repositori: es crea compilant l'script amb Inno Setup, tal com s'explica als materials.
- `pla_de_proves.md`: pla de proves amb 8 casos.
- `reparacions_pendents.wav`: àudio d'exemple generat amb la síntesi de veu.

No és el mateix exercici que es demana a les activitats avaluables: és un exemple complet que treballa les mateixes tècniques.
