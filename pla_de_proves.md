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
- **Incidència detectada i corregida:** el primer intent va utilitzar la veu per defecte del sistema sense comprovar-ne l'idioma, i va llegir el text en català amb una veu **anglesa** (Microsoft Zira, en-US), l'única marcada com a "per defecte" en aquest equip. Es va provar de forçar una veu en català amb `SelectVoiceByHints(..., new CultureInfo("ca-ES"))`, però aquest mètode **no llança cap excepció** quan no troba cap veu de la cultura demanada: es queda amb la veu anglesa en silenci. La correcció aplicada: comprovar explícitament amb `GetInstalledVoices(new CultureInfo("ca-ES"))` (retorna una llista, buida si no n'hi ha cap) i, en no haver-hi cap veu en català en aquest equip, seleccionar la veu en castellà (Microsoft Helena, es-ES) com a alternativa i informar-ne l'estat de l'aplicació. Verificat generant de nou el `.wav` amb la veu castellana seleccionada explícitament.

## P-06 · Accessibilitat
- **Objectiu:** comprovar que l'usuari pot aturar la lectura en veu alta en qualsevol moment.
- **Passos:** iniciar "llegeix pendents"; mentre llegeix, dir "atura la lectura".
- **Dades:** cap.
- **Resultat esperat:** la síntesi s'atura immediatament (`SpeakAsyncCancelAll`), l'estat mostra "lectura aturada".
- **Resultat obtingut:** ⚠️ Pendent de prova manual. Aquest entorn de desenvolupament no té micròfon ni altaveus reals; cal executar-la en un equip amb maquinari d'àudio complet. El codi que ho implementa (`sintetitzador.SpeakAsyncCancelAll()`) està verificat per revisió, no per execució.

## P-07 · Funcional (reconeixement real)
- **Objectiu:** comprovar que dir "nova reparació" en veu alta crea una reparació visible a la llista.
- **Passos:** prémer "Comença a escoltar"; dir "nova reparació" en veu alta.
- **Dades:** cap.
- **Resultat esperat:** apareix "Reparació 1 (pendent)" a la llista.
- **Resultat obtingut:** ⚠️ Pendent de prova manual, pel mateix motiu que P-06. En intentar instanciar `SpeechRecognitionEngine()` en aquest entorn s'obté `COMException 0x80050022` (no hi ha cap motor de reconeixement de veu registrat al sistema): la part de negoci ja s'ha verificat per separat a P-01, però la captura real de veu requereix maquinari i un equip Windows complet.

## P-08 · Distribució
- **Objectiu:** comprovar que l'executable publicat s'executa en un equip sense el runtime de .NET instal·lat per separat.
- **Passos:** `dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true`; executar el `.exe` resultant.
- **Dades:** cap.
- **Resultat esperat:** l'aplicació arrenca sense demanar cap instal·lació addicional.
- **Resultat obtingut:** ✅ Correcte. Executable de 155 MB generat i executat; procés viu i estable durant almenys 3 segons abans de tancar-lo manualment.

## Resum
6 de 8 casos completament executats i correctes. 2 casos (P-06, P-07) requereixen maquinari d'àudio real i queden documentats com a pendents d'execució manual, amb la causa tècnica exacta que ho impedeix en aquest entorn.
