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
    - $P_2$ = $\lbrace n | n < 1 \rbrace$.
    - $P_3$ = $\lbrace n | n > 25 \rbrace$.
* includeRests $\rightarrow$ boolean $(True | False)$. Deci avem 2 clase de echivalenta:
    - $R_1$ = $\lbrace True \rbrace$.
    - $R_2$ = $\lbrace False \rbrace$.

Domeniul iesirilor:\
Se returneaza costul total. Daca datele sunt corecte, totalul va fi mereu calculat si returnat, altfel se va afisa o eroare prin intermediul unei exceptii.\
Clasele de echivalenta globale astfel obtinute sunt:
* $Eq_{1} = \lbrace (d, p^{-}, r^{-}) | d \in D_{1} \rbrace$.
    - $T_{1} = (4, \textunderscore, \textunderscore)$.
* $Eq_{211} = \lbrace (d, p, r) | d \in D_2, p \in P_1, r \in R_1 \rbrace$.
    - $T_{211} = (100, 3, True)$.
* $Eq_{212} = \lbrace (d, p, r) | d \in D_2, p \in P_1, r \in R_2 \rbrace$.
    - $T_{212} = (100, 3, False)$.
* $Eq_{22} = \lbrace (d, p, r^{-}) | d \in D_2, p \in P_2 \rbrace$.
    - $T_{22} = (100, 0, \textunderscore)$.
* $Eq_{23} = \lbrace (d, p, r^{-}) | d \in D_2, p \in P_3 \rbrace$.
    - $T_{23} = (100, 30, \textunderscore)$.

|   Intrari (d, p, r)   |   Expected    |
| :---------: | :-----------: |
| $(4, \textunderscore, \textunderscore)$ | Cere ca distanta sa fie macar 5 kilometrii. |
| $(100, 3, True)$ | Se returneaza totalul de $88,4$. |
| $(100, 3, False)$ | Se returneaza totalul de $60,4$. |
| $(100, 0, \textunderscore)$ | Cere ca numarul de persoane sa fie minim 1. |
| $(100, 30, \textunderscore)$ | Cere ca numarul de persoane sa fie maxim 25. |

## 2. Analiza valorilor de frontiera
* Valorile de frontiera pentru clasa distantei sunt:
    - $D_1 \rightarrow 5 - \epsilon$.
    - $D_2 \rightarrow 5$.
* Valorile de frontiera pentru clasa pasagerilor sunt:
    - $P_1 \rightarrow 1, 25$.
    - $P_2 \rightarrow 0$.
    - $P_3 \rightarrow 26$.

Iar $includeRests$ poate lua 2 valori, deci vom avea urmatoarele 7 teste:
* $Eq_{1} \rightarrow (5 - \epsilon, \textunderscore, \textunderscore)$.
* $Eq_{211} \rightarrow (5, 1, True), (5, 25, True)$.
* $Eq_{212} \rightarrow (5, 1, False), (5, 25, False)$.
* $Eq_{22} \rightarrow (5, 0, \textunderscore)$.
* $Eq_{23} \rightarrow (5, 26, \textunderscore)$.

|   Intrari (d, p, r)   |   Expected    |
| :---------: | :-----------: |
| $(5 - \epsilon, \textunderscore, \textunderscore)$ | Cere ca distanta sa fie macar 5 kilometrii. |
| $(5, 1, True)$ | Se returneaza totalul de $4,05$. |
| $(5, 25, True)$ | Se returneaza totalul de $3,55$. |
| $(5, 1, False)$ | Se returneaza totalul de $4,05$. |
| $(5, 25, False)$ | Se returneaza totalul de $3,55$. |
| $(5, 0, \textunderscore)$ | Cere ca numarul de persoane sa fie minim 1. |
| $(5, 26, \textunderscore)$ | Cere ca numarul de persoane sa fie maxim 25. |

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
        - $\lbrace d | d < 5 \rbrace$.
        - $5 - \epsilon$.
        - 5.
        - 100 (distanta mica).
        - 750 (distanta mare/costuri suplimentare).
    * passengers:
        - $\lbrace p | p < 0 \rbrace$.
        - 0.
        - $1$ (minim + se aplica taxe suplimentare).
        - $2 .. 24$ (mediu).
        - $6$ (se aplica discount).
        - $25$ (maxim).
        - 26.
        - $\lbrace p | p > 26 \rbrace$.
    * includeRests:
        - $True$.
        - $False$.

Se observa ca pentru a testa toate categoriile vom avea nevoie de 5 $\cdot$ 8 $\cdot$ 2 = 80 de teste. Putem elimina cazurile care nu au sens, cum ar fi distanta prea mica $< 5$, distantele prea mari, numar negativ de pasageri, numar prea mare de pasageri. Astfel putem analiza 23 teste:

|   Intrari (d, p, r)   |   Expected    |
| :---------: | :-----------: |
| $(5 - \epsilon, \textunderscore, \textunderscore)$ | Cere ca distanta sa fie macar 5 kilometrii. |
| $(5, 0, \textunderscore)$ | Cere ca numarul de persoane sa fie minim 1. |
| $(100, 0, \textunderscore)$ | Cere ca numarul de persoane sa fie minim 1. |
| $(750, 0, \textunderscore)$ | Cere ca numarul de persoane sa fie minim 1. |
| $(5, 26, \textunderscore)$ | Cere ca numarul de persoane sa fie maxim 25. |
| $(100, 26, \textunderscore)$ | Cere ca numarul de persoane sa fie maxim 25. |
| $(750, 26, \textunderscore)$ | Cere ca numarul de persoane sa fie maxim 25. |
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

```c#
1. public double TotalTripCost(double distanceInKm, int passengers, bool includeRests) {
2.     if (distanceInKm < 5.0)
3.         throw new ArgumentOutOfRangeException(nameof(distanceInKm), "Distance should be positive and at least five kilometers.");
4.     if (passengers <= 0)
5.         throw new ArgumentOutOfRangeException(nameof(passengers), "Number of passengers should be at least one.");
6.     else if (passengers > 25)
7.         throw new ArgumentOutOfRangeException(nameof(passengers), "Number of passengers should be maximum 25.");
8.     double total = distanceInKm * DistanceService.BasePerKm;
9.     if (passengers > DistanceService.MinimumPeopleForDiscount) {
10.         total *= 0.9;
11.     } else {
12.         if (passengers < DistanceService.MaximumPeopleForBase) {
13.             total *= 1.1;
14.         }
15.     }
16.     if (includeRests) {
17.         int stops = (int) Math.Floor(distanceInKm / DistanceService.DistancePerStop);
18.         for (int i = 0; i < stops; ++i) {
19.             total += DistanceService.CostPerStop;
20.         }
21.     }
22.     double efficiency = 10;
23.     double remaining = distanceInKm;
24.     double fuelNeeded = 0.0;
25.     while (remaining > 0.0) {
26.         fuelNeeded += 1.0;
27.         remaining -= efficiency * (1.0 + (1.0 / fuelNeeded));
28.     }
29.     total += fuelNeeded * 1.3;
30.     if ((passengers > DistanceService.MinimumPeopleForDiscount) && (distanceInKm > 500)){
31.         total *= 1.05;
32.     }
33.     return total;
34. }
```