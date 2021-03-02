# BlockLoader:
Aplikace zobrazuje vzorové reklamní bloky, které byly odvysílané v televizi.
Bloky se načítají stisknutím tlačítka "Načíst bloky".
Jedná se o jednoduchou aplikaci, která představuje víceméně běžný úkol - vzít data, transformovat je, spočítat nějaké hodnoty a poté je zobrazit v GUI.

### Požadavky:
1) Do tabulky přidat sloupec, který bude určovat, kolik diváků daný blok měl. Tento sloupec by se měl zobrazit až po načtení diváků (respondentů).
2) Hodnoty se vypočtou po stisknutí příslušného tlačítka (=> přidat tlačítko).
3) Přidat testy na novou funkcionalitu (určení počtu diváků a zobrazení této hodnoty).

### Definice:
- **Divák** - diváci jsou respondenti (viz soubor Data/Respondents.xml), kteří byli zasaženi daným blokem.
- **Zásah** daným blokem - u každého respondenta je v datech seznam bloků, které ho zasáhly (= byl jejich divákem).

### Smysl:
Smyslem je ověřit základní znalosti v C# a schopnost zorientovat se ve WPF. Vzhledem k tomu, že nevyžadujeme větší znalosti WPF, připravili jsme funkční aplikaci, na které se dá stavět a rozšiřovat jí (nebo upravovat jí, představivosti se meze nekladou). Zajímá nás přístup k zadání a schopnost pracovat s existujícím kódem, takže výsledek není "správně" nebo "nesprávně".
