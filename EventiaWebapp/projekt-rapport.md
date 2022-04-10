# Projektuppgift Rapporten



### Sammanfattning



I första inlämningen skapade jag databas men fanns det inte login systemet. På den här inlämningen försökte förbättra projektet. Denna gången implementerades Identity paketet med roller av användaren. Databas modellen har varit 3 tabeller in första inlämningsuppgiften men denna gång jag sänkte tabellerna till två med **InverseProperty** som deltagare och organisatör.Jag skapade tre roller också som deltagere, organisatör och admin.

Jag skrev roller med handen därför den kan vara problem med fel stavning. Jag bör förbättra den i framtiden. I identitet paketet kommer med Pages förutom det jag har använt bara MVC-controller. Pages logiken känns lite krångligt men med mer praktik blir bättre. 

Med två databas tabeller och InverseProperty känndes luddigt från början. Men när man skriver koden ser man dessa properties verkar som olika  tabeller även när man använder include metoden med EntityFramework. Utan EF hur skulle man göras det är frågatecken.

Jag kunde inte la bilder när jag skapade event i organisatör sidan, search button i admin sidan och påmminelse för kommande eventer med email. Jag måste göra mer forskning om dessa ämnen men just nu jag vill inte la mer energi på dessa. 

När databasen startas om genom admin cookies fortfarande kvar och gör det fel när man går sin myevent därför har lagt signout metoden när startas om databasen.

I Admin perspektiv av ändring users role visades med checkbox i uppgiftbeskrivningen. Jag löste den här problemet med editing användaren med nytt view istället i en lista genom clicka en checkbox knappen. Som resultat min lösning funkar också men inte som visades. Kanske finns möjligheter med ajax utan att refrasha eller nytt view sidan.

 Förbättra första inlämningen var häftigt och jag vill försöka förbättra mer.

  
