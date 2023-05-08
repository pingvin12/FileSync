---
marp: true
theme: border
paginate: true
title: Mutation testing in dotnet
transition: slide-up
style: |
  @keyframes marp-outgoing-transition-slide-up {
    from { transform: translateY(0%); }
    to { transform: translateY(-100%); }
  }
  @keyframes marp-incoming-transition-slide-up {
    from { transform: translateY(100%); }
    to { transform: translateY(0%); }
  }
---
<!-- _class: title -->

# Mutációs tesztelés dotnetben
## Avagy hogyan teszteljük a tesztjeinket

---
# Mi az a mutációs tesztelés?

<p>Egy folyamat ami változásokat hoz létre a kódunkban, és lefuttatja a teszteket.
Ebben az esetben a teszteknek el kéne bukniuk. Ha nem buknak el, az jelentheti azt hogy a tesztek nem teljesen 
fedik le a kódot amit írtunk.</p>

---

# Példa eset

<p>
Például van egy online kaszinót építünk. A felhasználók csak 18 évesen tudnak belépni.
Ezért írunk egy pár sornyi kódot hogy korlátozzuk hogy ki tudjon hozzáférni az online kaszinóhoz:
</p>

<code>
public bool IsOldEnough(int age) => age > 18;
</code>

<p> Ezt a metódust Stryker meg fogja találni és a következők alapján fog rajta módosításokat végezni:</p>
<code>
/* 1 */ return age > 18; // teszt megfelelően lefut
/* 2 */ return age < 18; // teszt elhasal
/* 3 */ return false; // teszt elhasal
/* 4 */ return true; // teszt megfelelően lefut
</code>

---

# Mit csinál tömören

```
dotnet stryker --solution "/my-solution-dir/mysolution.sln"
```
Parancs futtatást követően stryker átmegy a projektünkön, a teszt eseteinken és bugokat helyez el.
Bugok elhelyesével információt kapunk arról, hogy az elhelyezett részletek nem-megléte/módosítása hogyan változtat a programunk futásán - teszt eseteink eredményein.


---
# Nézzünk egy példát egy egyszerű projekten

* A projekt két mappa között csinál ellenőrzést, és kiegészít ha a célpont mappából hiányzik az ami a kezdő mappában van.
* A projekthez írtunk unit teszteket (xUnit) ezek a tesztek hiba nélkül lefutnak.

<footer> FileSync (https://github.com/pingvin12/FileSync) </footer>

---

# Jelentések és mutációk

<footer>Következő diákon</footer>

---

# Aritmetikai mutációk

<table><thead><tr><th>Eredeti</th><th>Mutáció alatt</th></tr></thead><tbody><tr><td><code>+</code></td><td><code>-</code></td></tr><tr><td><code>-</code></td><td><code>+</code></td></tr><tr><td><code>*</code></td><td><code>/</code></td></tr><tr><td><code>/</code></td><td><code>*</code></td></tr><tr><td><code>%</code></td><td><code>*</code></td></tr></tbody></table>

---
# Egyenlőség operátorok

<table><thead><tr><th>Eredeti</th><th>Mutáció alatt</th></tr></thead><tbody><tr><td><code>&gt;</code></td><td><code>&lt;</code></td></tr><tr><td><code>&gt;</code></td><td><code>&gt;=</code></td></tr><tr><td><code>&gt;=</code></td><td><code>&lt;</code></td></tr><tr><td><code>&gt;=</code></td><td><code>&gt;</code></td></tr><tr><td><code>&lt;</code></td><td><code>&gt;</code></td></tr><tr><td><code>&lt;</code></td><td><code>&lt;=</code></td></tr><tr><td><code>&lt;=</code></td><td><code>&gt;</code></td></tr><tr><td><code>&lt;=</code></td><td><code>&lt;</code></td></tr><tr><td><code>==</code></td><td><code>!=</code></td></tr><tr><td><code>!=</code></td><td><code>==</code></td></tr><tr><td><code>is</code></td><td><code>is not</code></td></tr><tr><td><code>is not</code></td><td><code>is</code></td></tr></tbody></table>

---
# Linq
<table><thead><tr><th>Eredeti</th><th>Mutáció alatt</th></tr></thead><tbody><tr><td><code>SingleOrDefault()</code></td><td><code>Single()</code></td></tr><tr><td><code>Single()</code></td><td><code>SingleOrDefault()</code></td></tr><tr><td><code>FirstOrDefault()</code></td><td><code>First()</code></td></tr><tr><td><code>First()</code></td><td><code>FirstOrDefault()</code></td></tr><tr><td><code>Last()</code></td><td><code>First()</code></td></tr><tr><td><code>All()</code></td><td><code>Any()</code></td></tr><tr><td><code>Any()</code></td><td><code>All()</code></td></tr><tr><td><code>Skip()</code></td><td><code>Take()</code></td></tr><tr><td><code>Take()</code></td><td><code>Skip()</code></td></tr><tr><td><code>SkipWhile()</code></td><td><code>TakeWhile()</code></td></tr><tr><td><code>TakeWhile()</code></td><td><code>SkipWhile()</code></td></tr><tr><td><code>Min()</code></td><td><code>Max()</code></td></tr><tr><td><code>Max()</code></td><td><code>Min()</code></td></tr><tr><td><code>Sum()</code></td><td><code>Max()</code></td></tr><tr><td><code>Count()</code></td><td><code>Sum()</code></td></tr><tr><td><code>Average()</code></td><td><code>Min()</code></td></tr><tr><td><code>OrderBy()</code></td><td><code>OrderByDescending()</code></td></tr><tr><td><code>OrderByDescending()</code></td><td><code>OrderBy()</code></td></tr><tr><td><code>ThenBy()</code></td><td><code>ThenByDescending()</code></td></tr><tr><td><code>ThenByDescending()</code></td><td><code>ThenBy()</code></td></tr><tr><td><code>Reverse()</code></td><td><code>AsEnumerable()</code></td></tr><tr><td><code>AsEnumerable()</code></td><td><code>Reverse()</code></td></tr><tr><td><code>Union()</code></td><td><code>Intersect()</code></td></tr><tr><td><code>Intersect()</code></td><td><code>Union()</code></td></tr><tr><td><code>Concat()</code></td><td><code>Except()</code></td></tr><tr><td><code>Except()</code></td><td><code>Concat()</code></td></tr><tr><td><code>MinBy()</code></td><td><code>MaxBy()</code></td></tr><tr><td><code>MaxBy()</code></td><td><code>MinBy()</code></td></tr><tr><td><code>SkipLast()</code></td><td><code>TakeLast()</code></td></tr><tr><td><code>TakeLast()</code></td><td><code>SkipLast()</code></td></tr><tr><td><code>Order()</code></td><td><code>OrderDescending()</code></td></tr><tr><td><code>OrderDescending()</code></td><td><code>Order()</code></td></tr><tr><td><code>UnionBy()</code></td><td><code>IntersectBy()</code></td></tr><tr><td><code>IntersectBy()</code></td><td><code>UnionBy()</code></td></tr></tbody></table>

---
# Még több mutációs művelet
Még több mutációval kapcsolatos művelet <a href="https://stryker-mutator.io/docs/stryker-net/mutations/">itt</a> érhető el.
---
---
# Eredmények

Minden egyes futtatásnál stryker egy html reportot készít nekünk amit meg tudunk tekinteni.

<img src="https://stryker-mutator.io/assets/images/html-report-net-5d95a90c798dfc78504a2c4bc307348f.png" />

---

# Értékekre magyarázat

<table>
  <tr>
    <th>Státusz</th>
    <th>Magyarázat</th>
  </tr>
  <tr>
    <td>Pending</td>
    <td>A mutáns generálva lett, de még nem futtatva. Ez egy átmeneti állapot, amely megváltozik, amint a mutánst teszteljük.</td>
  </tr>
  <tr>
    <td>Killed</td>
    <td>Ha legalább egy teszt sikertelen volt, amikor a mutáns aktív volt, akkor az meg van ölve. Ez az, amit szeretnél, jó munka!</td>
  </tr>
  <tr>
    <td>Survived</td>
    <td>Ha az összes teszt sikeres volt, amikor a mutáns aktív volt, akkor a mutáns túlélte. Hiányzik egy teszt a mutánsra nézve.</td>
  </tr>
  <tr>
    <td>No coverage</td>
    <td>A mutánst nem tesztelik le egyik teszted sem, így túlélte. Egy teszt hiányzik rá.</td>
  </tr>
  <tr>
    <td>Timeout</td>
    <td>A tesztek futtatása ezzel a mutánssal aktív állapotban időtúllépéshez vezetett. Például a mutáns végtelen ciklust okoz a kódodban. Ne fordíts túl sok figyelmet erre a mutánsra. "Észleltnek" számít. A logika az, hogy ha ez a mutáns be lenne injektálva a kódodba, a CI build észlelné, mert a tesztek sosem teljesülnének.</td>
  </tr>
  <tr>
    <td>Runtime error</td>
    <td>A tesztek futtatása hibával zárult (nem sikertelen teszttel). Ez előfordulhat, ha a tesztfuttató hiba lép fel. Például, ha a tesztfuttató OutOfMemoryError hibát dob vagy a mutáns miatt nem értelmezhető kód született. Ne fordíts túl sok figyelmet erre a mutánsra. Nem számít bele a mutációs pontszámodba.</td>
  </tr>
  <tr>
    <td>Compile error</td>
    <td>A mutáns fordítási hibát okozott. Ez előfordulhat fordított nyelveknél. Ne fordíts túl sok figyelmet erre a mutánsra. Nem számít bele a mutációs pontszámodba.</td>
  </tr>
  <tr>
    <td>Ignored</td>
    <td>A mutánst nem tesztelték, mert figyelmen kívül hagyták. Vagy felhasználói intézkedés vagy más ok miatt. Ez nem számít bele a mutációs pontszámodba.</td>
    </tr>
</table>

---
# Reportban mi-hogyan van kiszámítva
<table>
  <tr>
    <th>Állapot</th>
    <th>Magyarázat</th>
  </tr>
  <tr>
    <td>Detected killed + timeout</td>
    <td>A tesztek által észlelt mutánsok száma.</td>
  </tr>
  <tr>
    <td>Undetected survived + no coverage</td>
    <td>A tesztek által nem észlelt mutánsok száma.</td>
  </tr>
  <tr>
    <td>Covered detected + survived</td>
    <td>Azoknak a mutánsoknak a száma, amelyekre a tesztek lefedettséget biztosítanak.</td>
  </tr>
  <tr>
    <td>Valid detected + undetected</td>
    <td>Az érvényes mutánsok száma. Ezek nem okoztak fordítási hibát vagy futási hibát.</td>
  </tr>
  <tr>
    <td>Invalid runtime errors + compile errors</td>
    <td>Az érvénytelen mutánsok száma. Ezeket nem lehet tesztelni, mert fordítási vagy futási hibát okoznak.</td>
  </tr>
  <tr>
    <td>Total mutants valid + invalid + ignored + pending</td>
    <td>Az összes mutáns száma.</td>
  </tr>
  <tr>
    <td>Mutation score detected / valid * 100</td>
    <td>Az észlelt mutánsok százalékos aránya az összes érvényes mutánshoz képest. Minél magasabb, annál jobb!</td>
  </tr>
  <tr>
    <td>Mutation score based on covered code detected / covered * 100</td>
    <td>Az észlelt mutánsok százalékos aránya a kódfedettségi eredmények alapján. </td>
  </tr>
</table>

---

# Köszönöm a figyelmet!

---

# Források

- Nick Chapsas videója - https://www.youtube.com/watch?v=sGwfwtkaDfk&ab_channel=NickChapsas
- Stryker dotnet docs - https://stryker-mutator.io/docs/stryker-net/introduction/