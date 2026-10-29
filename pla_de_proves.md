# Pla de proves - TallerVeu

## P-01 · Funcional
- **Objectiu:** comprovar que l'ordre "nova reparació" afegeix una reparació pendent.
- **Passos:** crear un `GestorReparacions`; cridar `ProcessaOrdre("nova reparació")`.
- **Dades:** cap.
- **Resultat esperat:** la llista de reparacions té 1 element, no acabat.
- **Resultat obtingut:** ✅ Correcte (prova automatitzada `NovaReparacio_AfegeixUnaReparacioPendent`, executada amb `dotnet test`).

## P-02 · Funcional
- **Objectiu:** comprovar que "marca acabada" marca la primera reparació pendent.
- **Passos:** crear dues reparacions; cridar `ProcessaOrdre("marca acabada")`.
- **Dades:** dues reparacions noves.
- **Resultat esperat:** la primera queda acabada, la segona continua pendent.
- **Resultat obtingut:** ✅ Correcte (prova automatitzada `MarcaAcabada_MarcaLaPrimeraPendent`).

## P-03 · Error / robustesa
- **Objectiu:** comprovar que "marca acabada" sense cap reparació no provoca un error.
- **Passos:** crear un gestor buit; cridar `ProcessaOrdre("marca acabada")`.
- **Dades:** cap.
- **Resultat esperat:** missatge "No hi ha cap reparació pendent.", sense excepció.
- **Resultat obtingut:** ✅ Correcte (prova automatitzada `MarcaAcabada_SenseCapReparacio_NoFallaINoDispara`).

## P-04 · Error / robustesa
- **Objectiu:** comprovar que una ordre no reconeguda no fa fallar l'aplicació.
- **Passos:** cridar `ProcessaOrdre("balla la conga")`.
- **Dades:** text arbitrari que no coincideix amb cap ordre.
- **Resultat esperat:** missatge "Ordre no reconeguda.", sense excepció.
- **Resultat obtingut:** ✅ Correcte (prova automatitzada `OrdreDesconeguda_RetornaMissatgeGenéric`).

## P-05 · Funcional (síntesi de veu)
- **Objectiu:** comprovar que la síntesi de veu genera àudio real a partir del text de reparacions pendents.
- **Passos:** crear un `SpeechSynthesizer`; `SetOutputToWaveFile`; `Speak(gestor.TextPendents())`.
- **Dades:** dues reparacions pendents.
- **Resultat esperat:** es genera un fitxer `.wav` vàlid, de durada superior a 0 segons.
- **Resultat obtingut:** ✅ Correcte. Fitxer WAV de 5,07 s, PCM mono 22050 Hz, verificat amb la biblioteca `wave` de Python.
- **Incidència detectada i corregida:** la primera versió utilitzava la veu per defecte del sistema sense comprovar-ne l'idioma, i llegia el text en català amb una veu **anglesa** (Microsoft Zira, en-US). Forçar una veu en català amb `SelectVoiceByHints(..., new CultureInfo("ca-ES"))` no serveix: aquest mètode **no llança cap excepció** quan no troba cap veu de la cultura demanada, sinó que es queda amb la veu anterior en silenci. Correcció: comprovar explícitament amb `GetInstalledVoices(new CultureInfo("ca-ES"))` (retorna una llista, buida si no n'hi ha cap) i, si no hi ha cap veu en català, seleccionar una veu en castellà (per exemple, Microsoft Helena, es-ES) i informar-ne a l'estat de l'aplicació. Verificat generant de nou el `.wav` amb la veu castellana seleccionada explícitament.

## P-06 · Accessibilitat
- **Objectiu:** comprovar que l'usuari pot aturar la lectura en veu alta en qualsevol moment.
- **Passos:** iniciar "llegeix pendents"; mentre llegeix, dir "atura la lectura".
- **Dades:** cap.
- **Resultat esperat:** la síntesi s'atura immediatament (`SpeakAsyncCancelAll`), l'estat mostra "lectura aturada".
- **Resultat obtingut:** ⚠️ Pendent de prova manual amb altaveus. El codi que ho implementa (`sintetitzador.SpeakAsyncCancelAll()`) està verificat per revisió, no per execució.

## P-07 · Funcional (reconeixement real)
- **Objectiu:** comprovar que dir "nova reparació" en veu alta crea una reparació visible a la llista.
- **Passos:** prémer "Comença a escoltar"; dir "nova reparació" en veu alta.
- **Dades:** cap.
- **Resultat esperat:** apareix "Reparació 1 (pendent)" a la llista.
- **Resultat obtingut:** ✅ Correcte. L'estat passa a "escoltant..." i la llista mostra "Reparació 1 (pendent)". Reconeixement també verificat sobre un `.wav` sintetitzat: les 6 ordres es reconeixen amb el reconeixedor es-ES (confiança entre 0,61 i 0,70).
- **Incidència detectada i corregida:** en prémer "Comença a escoltar" apareixia `COMException 0x80050022`. El reconeixedor per defecte de Windows (en-US) estava registrat però no era utilitzable, i l'error no apareixia en crear `SpeechRecognitionEngine()` sinó més tard, en inicialitzar-lo. Correcció: triar explícitament amb `InstalledRecognizers()` un reconeixedor ca-ES o, si no n'hi ha, es-ES, i fixar `GrammarBuilder.Culture` a la cultura del reconeixedor (sense això, `LoadGrammar` llança `InvalidOperationException`).

## P-08 · Distribució
- **Objectiu:** comprovar que l'executable publicat s'executa en un equip sense el runtime de .NET instal·lat per separat.
- **Passos:** `dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true`; executar el `.exe` resultant.
- **Dades:** cap.
- **Resultat esperat:** l'aplicació arrenca sense demanar cap instal·lació addicional.
- **Resultat obtingut:** ✅ Correcte. Executable de 155 MB generat i executat; procés viu i estable durant almenys 3 segons abans de tancar-lo manualment.

## Resum
7 de 8 casos executats i correctes. El cas P-06 (aturar la lectura en veu alta) queda pendent d'execució manual amb altaveus.
