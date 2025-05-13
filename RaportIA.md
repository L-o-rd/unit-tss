# Raport Inteligență Artificială

## Introducere

Folosirea unui model sau unui utilitar ce foloseste Inteligenta Artificiala poate fi folositor in cazul testarii unitare a unui serviciu sau librarii. Totusi, eficienta unei astfel de testari consta, cum vom observa in acest raport, doar in rapiditatea prototiparii testelor si nu atat de mult in corectitudinea si minimalitatea testelor generate.

| Categorie                       | Teste Generate AI                   | Teste Generate Manual                     |
| ----------------------------- | ------------------------------------ | ------------------------------------------ |
| ✅ **Coverage**          | Minimal                                  | Total                                        |
| 🧠 **Cazuri speciale**  | Cazuri comune    | Cazuri mai diverse         |
| 🎯 **Asertiuni** | Scrise complet  | Scrise complet        |
| ⏱️ **Rapiditate**      | Rapid (generate automat)                | Încet (analiza complexa)            |
| 🎓 **Experienta**          | Nu contine cunostinte contextuale       | Includ error-guessing si alte experiente    |

## Functional Testing
### 1. Partitionarea in clase de echivalenta

Partitionarea manuala a domeniului intrarilor:
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

Partitionarea domeniului intrarilor facuta de AI (este echivalenta).
Input valid:
* Distance: [5, ∞)
* Passengers: (0, 25]
* includeRests: {true, false}

Input invalid:
* Distance: < 5
* Passengers: ≤ 0 or > 25

| Teste Generate AI | Teste Generate Manual |
| :---------------: | :-------------------: |
| [TestCase(100, 1, true)]  | $(4, \textunderscore, \textunderscore)$   |
| [TestCase(10, 5, false)]  | $(100, 3, True)$  |
| [TestCase(500, 25, true)] | $(100, 3, False)$  |
| [TestCase(4.9, 3, false)] | $(100, 0, \textunderscore)$ |
| [TestCase(100, 0, false)] | $(100, 30, \textunderscore)$  |
| [TestCase(100, 26, true)] | $\epsilon$ |

Dupa cate putem observa testele generate automat sunt bune, dar nu sunt minimale fiind un test in plus.

### 2. Analiza valorilor de frontiera

Valorile de frontiera analizate manual:
* Valorile de frontiera pentru clasa distantei sunt:
    - $D_1 \rightarrow 5 - \epsilon$.
    - $D_2 \rightarrow 5$.
* Valorile de frontiera pentru clasa pasagerilor sunt:
    - $P_1 \rightarrow 1, 25$.
    - $P_2 \rightarrow 0$.
    - $P_3 \rightarrow 26$.

Valorile de frontiera generate AI:
* Clasa distantei:
    - $5$ si $4.999$.
* Clasa Pasagerilor:
    - $1$, $0$, $25$, $26$.

Doar 2 dintre testele generate AI au forma corecta si anume (distanta, pasageri, includeStopuri):
* [TestCase(5.0, 1, false)]  
* [TestCase(4.999, 1, false)]

Restul au formatul (pasageri, `?`, includeStopuri), `?` deoarece daca ar reprezenta distanta atunci nu ar ajunge niciodata sa verifice numarul de pasageri deci sunt complet invalide:

* [TestCase(1, 1, false)] 
* [TestCase(0, 1, false)] 
* [TestCase(25, 1, false)]
* [TestCase(26, 1, false)]

| Teste Generate AI | Teste Generate Manual |
| :---------------: | :-------------------: |
| [TestCase(5.0, 1, false)]  | $(5 - \epsilon, \textunderscore, \textunderscore)$   |
| [TestCase(4.999, 1, false)]  | $(5, 1, True)$  |
| $\epsilon$ | $(5, 25, True)$  |
| $\epsilon$ | $(5, 1, False)$ |
| $\epsilon$ | $(5, 25, False)$  |
| $\epsilon$ | $(5, 0, \textunderscore)$ |
| $\epsilon$ | $(5, 26, \textunderscore)$ |

In plus, AI-ul nu a luat in considerare cele doua valori posibile pentru $includeRests$ deci set-ul de teste ar fi fost prea mic oricum.

### 3. Category Partitioning

Analiza manuala in acest caz este extrem de complexa si include toate categoriile ce pot fi extrase din codul sursa. In cazul AI-ului, categoriile sunt doar 3 si se bazeaza doar pe o simpla impartire a distantei parcurse.\
Analiza manuala:
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

Se observa ca pentru a testa toate categoriile vom avea nevoie de 5 $\cdot$ 8 $\cdot$ 2 = 80 de teste. Putem elimina cazurile care nu au sens, cum ar fi distanta prea mica $< 5$, distantele prea mari, numar negativ de pasageri, numar prea mare de pasageri.

Analiza generata AI:
| Distance        | Passengers | includeRests |
| --------------- | ---------- | ------------ |
| Short (5–50)    | 1          | true         |
| Medium (51–499) | 3          | false        |
| Long (500+)     | 6          | true         |

Rezultand setul de teste:
* [TestCase(20, 1, true)]    
* [TestCase(200, 3, false)]  
* [TestCase(600, 6, true)] 

Analiza manuala rezulta in $23$ de teste valide si cu importanta pentru serviciul testat:

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

## Mutation Testing

Proiectul foloseste libraria Stryker .NET pentru generarea automata a mutantilor de diferite tipuri, astfel avem 47 de mutanti. Mutantii generati de AI sunt urmatorii 5:

1. $M_{AI_1}$ $\rightarrow$

| Original | Mutant |
| :---------: | :---------: |
| `if (distanceInKm >= 5.0)` | `if (distanceInKm > 5.0)` |

Dar acea secventa de cod nu exista.

2. $M_{AI_2}$ $\rightarrow$
   
| Original | Mutant |
| :---------: | :---------: |
| `if (passengers <= 0)` | `if (passengers < 0)` |

Cu testul aferent [TestCase(0, 1, false)].

3. $M_{AI_3}$ $\rightarrow$

Stergerea blocului ce calculeaza costul extra adus de fuel.

Cu testul aferent [TestCase(100, 5, false)].

4. $M_{AI_4}$ $\rightarrow$

| Original | Mutant |
| :---------: | :---------: |
| `if (passengers > DistanceService.MinimumPeopleForDiscount)` | `if (passengers < DistanceService.MinimumPeopleForDiscount)` |

Cu testul aferent [TestCase(100, 6, false)].

5. $M_{AI_5}$ $\rightarrow$

| Original | Mutant |
| :---------: | :---------: |
| `if (includeRests)` | `if (!includeRests)` |

Cu testele aferente: [TestCase(100, 6, true)] si [TestCase(100, 6, false)].

Deci doar mutantii 2-5 generati de AI sunt valizi.
Setul de teste generat de noi este minimal si obtine un scor al mutantilor de $100$\% deci nu mai exista mutanti in viata.

|   Intrari (d, p, r)   |   Expected    |
| :---------: | :-----------: |
| $(750, 5, True)$ | Se returneaza totalul de $677,3$. |
| $(20, 5, True)$ | Se returneaza totalul de $11,3$. |
| $(500, 7, False)$ | Se returneaza totalul de $284,8$. |

Setul de teste generat de AI obtine $100$\% pe cei $4$ mutanti si cele $4$ teste, dar sunt prea putini mutanti pentru a fi relevant. Pe cei $47$ de mutanti generati de Stryker, testele generate de AI obtine $95.74$\%.
