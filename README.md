# Testare Unitara in C#
## Descriere
Aplicatia pune la dispozitie o interfata simpla la nivel de consola, ce ajuta la calcularea si planificarea unei calatorii in functie de distanta, numarul de pasageri si costuri suplimentare.
## 1. Partitionarea in clase de echivalenta
Domeniul intrarilor:
* distanceInKm $\rightarrow$ numar real mai mare sau egal cu $5$ km. Astfel, avem 2 clase de echivalenta:
    - $D_1$ = $(-\infty, 5)$.
    - $D_2$ = $[5, +\infty)$.
* passengers $\rightarrow$ numar intreg pozitiv, strict mai mare ca $0$ si mai mic sau egal cu $25$. Astfel, avem 3 clase de echivalenta:
    - $P_1$ = $[1, 25]$.
    - $P_2$ = $\{n | n < 1\}$.
    - $P_3$ = $\{n | n > 25\}$.
* includeRests $\rightarrow$ boolean $(True | False)$. Deci avem 2 clase de echivalenta:
    - $R_1$ = $\{True\}$.
    - $R_2$ = $\{False\}$.

Domeniul iesirilor:\
Se returneaza costul total. Daca datele sunt corecte, totalul va fi mereu calculat si returnat, altfel se va afisa o eroare prin intermediul unei exceptii.\
Clasele de echivalenta globale astfel obtinute sunt:
* $Eq_{1} = \{(d, p^{-}, r^{-}) | d \in D_1\}$.
    - $T_{1} = (4, \_, \_)$.
* $Eq_{211} = \{(d, p, r) | d \in D_2, p \in P_1, r \in R_1\}$.
    - $T_{211} = (100, 3, True)$.
* $Eq_{212} = \{(d, p, r) | d \in D_2, p \in P_1, r \in R_2\}$.
    - $T_{212} = (100, 3, False)$.
* $Eq_{22} = \{(d, p, r^{-}) | d \in D_2, p \in P_2\}$.
    - $T_{22} = (100, 0, \_)$.
* $Eq_{23} = \{(d, p, r^{-}) | d \in D_2, p \in P_3\}$.
    - $T_{23} = (100, 30, \_)$.

|   Intrari (d, p, r)   |   Expected    |
| :---------: | :-----------: |
| $(4, \_, \_)$ | Cere ca distanta sa fie macar 5 kilometrii. |
| $(100, 3, True)$ | Se returneaza totalul de $88,4$. |
| $(100, 3, False)$ | Se returneaza totalul de $60,4$. |
| $(100, 0, \_)$ | Cere ca numarul de persoane sa fie minim 1. |
| $(100, 30, \_)$ | Cere ca numarul de persoane sa fie maxim 25. |

## 2. Analiza valorilor de frontiera
* Valorile de frontiera pentru clasa distantei sunt:
    - $D_1 \rightarrow 5 - \epsilon$.
    - $D_2 \rightarrow 5$.
* Valorile de frontiera pentru clasa pasagerilor sunt:
    - $P_1 \rightarrow 1, 25$.
    - $P_2 \rightarrow 0$.
    - $P_3 \rightarrow 26$.

Iar $includeRests$ poate lua 2 valori, deci vom avea urmatoarele 7 teste:
* $Eq_{1} \rightarrow (5 - \epsilon, \_, \_)$.
* $Eq_{211} \rightarrow (5, 1, True), (5, 25, True)$.
* $Eq_{212} \rightarrow (5, 1, False), (5, 25, False)$.
* $Eq_{22} \rightarrow (5, 0, \_)$.
* $Eq_{23} \rightarrow (5, 26, \_)$.

|   Intrari (d, p, r)   |   Expected    |
| :---------: | :-----------: |
| $(5 - \epsilon, \_, \_)$ | Cere ca distanta sa fie macar 5 kilometrii. |
| $(5, 1, True)$ | Se returneaza totalul de $4,05$. |
| $(5, 25, True)$ | Se returneaza totalul de $3,55$. |
| $(5, 1, False)$ | Se returneaza totalul de $4,05$. |
| $(5, 25, False)$ | Se returneaza totalul de $3,55$. |
| $(5, 0, \_)$ | Cere ca numarul de persoane sa fie minim 1. |
| $(5, 26, \_)$ | Cere ca numarul de persoane sa fie maxim 25. |

## 3. Category Partitioning
1. Descompune specificatia în unități: avem o singură unitate.
2. Identifică parametrii: distanceInKm (d), passengers (p), includeRests (r).
3. Categorii:
    * d $\rightarrow$ daca este in intervalul valid $[5, +\infty)$.
    * p $\rightarrow$ daca este in intervalul valid $[1, 25]$.
    * r $\rightarrow$ daca este $True$ sau $False$.
4. Partiționeaza fiecare categorie în alternative:
    * d: $<5 - \epsilon, 5, 5 + \epsilon, 10, 100, 750, ...>$.
    * p: $<0, 1, 2, 3, ..., 25, 26>$.
    * r: $True$ sau $False$.
5. Scrie specificația de testare:
    * distanceInKm:
        - $\{d | d < 5\}$.
        - $5 - \epsilon$.
        - 5.
        - 100 (distanta mica).
        - 750 (distanta mare/costuri suplimentare).
    * passengers:
        - $\{p | p < 0\}$.
        - 0.
        - $1$ (minim + se aplica taxe suplimentare).
        - $2 .. 24$ (mediu).
        - $6$ (se aplica discount).
        - $25$ (maxim).
        - 26.
        - $\{p | p > 26\}$.
    * includeRests:
        - $True$.
        - $False$.

Se observa ca pentru a testa toate categoriile vom avea nevoie de 5 $\cdot$ 8 $\cdot$ 2 = 80 de teste. Putem elimina cazurile care nu au sens, cum ar fi distanta prea mica $< 5$, distantele prea mari, numar negativ de pasageri, numar prea mare de pasageri. Astfel putem analiza 23 teste:

|   Intrari (d, p, r)   |   Expected    |
| :---------: | :-----------: |
| $(5 - \epsilon, \_, \_)$ | Cere ca distanta sa fie macar 5 kilometrii. |
| $(5, 0, \_)$ | Cere ca numarul de persoane sa fie minim 1. |
| $(100, 0, \_)$ | Cere ca numarul de persoane sa fie minim 1. |
| $(750, 0, \_)$ | Cere ca numarul de persoane sa fie minim 1. |
| $(5, 26, \_)$ | Cere ca numarul de persoane sa fie maxim 25. |
| $(100, 26, \_)$ | Cere ca numarul de persoane sa fie maxim 25. |
| $(750, 26, \_)$ | Cere ca numarul de persoane sa fie maxim 25. |
| $(5, 1, True)$ | Se returneaza totalul de $4,05$. |
| $(5, 1, False)$ | Se returneaza totalul de $4,05$. |
| $(5, 3, True)$ | Se returneaza totalul de $3,8$. |
| $(5, 3, False)$ | Se returneaza totalul de $3,8$. |
| $(5, 6, True)$ | Se returneaza totalul de $3,55$. |
| $(5, 6, False)$ | Se returneaza totalul de $3,55$. |
| $(5, 25, True)$ | Se returneaza totalul de $3,55$. |
| $(5, 25, False)$ | Se returneaza totalul de $3,55$. |
| $(750, 1, True)$ | Se returneaza totalul de $714,8$. |
| $(750, 1, False)$ | Se returneaza totalul de $504,8$. |
| $(750, 3, True)$ | Se returneaza totalul de $677,3$. |
| $(750, 3, False)$ | Se returneaza totalul de $467,3$. |
| $(750, 6, True)$ | Se returneaza totalul de $671,79$. |
| $(750, 6, False)$ | Se returneaza totalul de $451,29$. |
| $(750, 25, True)$ | Se returneaza totalul de $671,79$. |
| $(750, 25, False)$ | Se returneaza totalul de $451,29$. |