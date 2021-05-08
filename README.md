# Eksamensprojekt @ Skak
_... med lidt ændringer_

Dette projekt er lavet som et **eksamensprojekt i programmering.**
Spillet virker præcist ligesom skak, men har ændringer i form af:
* Nogle felter, der ikke kan bruges
* 2 nye skakbrikker
* Større skakbræt

## Generelle ændringer

**Blokkerede felter**
* Måden dette virker på er, at der er 8 felter placeret mod midten af brættet, hvor det ikke er muligt at placere sig.
> Dette skal ændre, hvordan spillet skal spilles, da man så ikke kan bruge specifikke felter i sit angreb, men det vil måske hjælpe en i sit forsvar.


**Større skakbræt**
* Skakbrættet har normalt en størrelse på 8x8, hvor vi så har ændret det til 12x8.
> I takt med at vi har ændret mængden af brikker samt tilføjet blokkerede felter, har vi været nødsaget til at gøre skakbrættet større. Dette giver igen flere muligheder for, hvordan man spiller spillet, samt sørger for at de andre ændringer ikke virker "mast ind" i spillet.
 
## De nye skakbrikker

**Royal Guard**
* Royal Guard kan rykke sig 1 gang skråt frem til hver side eller rykke en tilbage.
* Disse brikker er placeret under de blokkerede felter da bønder ville være ligegyldige i denne position.
> Royal Guard er en slags "extension" af bonden, der bare kan bevæge sig lidt anderledes og har fået muligheden for at rykke sig tilbage. Dens bevægelighed skal gøre det muligt at undgå blokkerede felter, da bonden ikke kan dette.


**Kyllingen**
* Kyllingen kan rykke sig lige frem helt til en fjende eller en til hver side. Den kan _ikke_ rykke tilbage.
* Kyllingen skal virke som en "hovedløs" kylling, der egentlig bare kan gøre meget lidt, men alligevel kan straffe folk, der laver en fejl. 
> Kyllingen skal bruges som en brik, der kan straffe folk, der eventuelt skulle lave et fejlryk. Kyllingen skal virke lidt som en brik, man rykker én gang, hvorefter den dør, da  den ikke kan rykke tilbage.
