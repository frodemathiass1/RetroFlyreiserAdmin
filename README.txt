Localhost:
Opprett ny bruker for å logge inn på løsningen.
(brukere kan ikke seedes pga problemer med å få salt i seperat kolonne i db)
tips: bruk pepper istedet..

Azure:
URL: retroflyreiser2017.azurewebsites.net

Login:
Brukernavn: Testbruker
Passord: Test1234
Alternativ: Opprett en bruker selv.

Dbchangelogg - fulgt denne guiden:
https://www.exceptionnotfound.net/entity-change-tracking-using-dbcontext-in-entity-framework-6/

_____________________________
Bugs (It's not a bug, it's a feature!):

Ønsket funskjonalitet: Ending av postnr. Kan ikke endres pga PK.

Problemer med validering fra modell, workaround med validering(required) i Razorview.

Enhetstester på HomeController ikke utført pga mye merarbeid for få metoder som ikke utfører 
noen operasjoner annet enn å liste ut data fra db. 
Tilsvarende metoder som er brukt andre steder er testet.

Forfattere:
Frode Kristian Mathiassen (s30549)
Thien Minh Truong (S161922)
